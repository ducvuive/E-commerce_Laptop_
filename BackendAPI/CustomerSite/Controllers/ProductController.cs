using CustomerSite.Clients;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShareView.Constants;
using ShareView.DTO;
using System.IdentityModel.Tokens.Jwt;

namespace CustomerSite.Controllers
{
    [Route("products")]
    public class ProductController : Controller
    {

        //protected readonly HttpClient productService;
        /*private readonly ScreenController productService;*/
        /* public ProductController(HttpClient productService)
         {
             this.productService = productService;
         }*/
        private readonly ILogger<ProductController> _logger;
        private readonly IProductClient productClient;
        private readonly ICategoryClient categoryClient;
        private readonly IScreenClient screenClient;
        private readonly IRamClient ramClient;
        private readonly IProcessorClient processorClient;
        private readonly IUserClient userClient;
        /*public const string CARTKEY = "cart";*/
        public ProductController(ILogger<ProductController> logger, IProductClient productClient, ICategoryClient categoryClient, IScreenClient screenClient, IRamClient ramClient, IProcessorClient processorClient, IUserClient userClient)
        {
            _logger = logger;
            this.productClient = productClient;
            this.categoryClient = categoryClient;
            this.screenClient = screenClient;
            this.ramClient = ramClient;
            this.processorClient = processorClient;
            this.userClient = userClient;
        }
        public async Task<IActionResult> Index(int category, string searchString, int page = 1)
        {
            int totalPage;
            List<ProductDTO> products = new List<ProductDTO>();
            var categoryList = await categoryClient.GetCategories();
            ViewBag.CategoryList = categoryList;
            ViewBag.category = category;
            ViewBag.searchString = searchString;
            if (string.IsNullOrEmpty(searchString))
            {
                if (category <= 0)
                {
                    products = await productClient.GetAllProducts();
                    float temp = products.Count() / (float)12;
                    totalPage = (int)Math.Ceiling(temp);
                    products = await productClient.GetProductsByPage(page);
                }
                else
                {
                    products = await productClient.GetProductsByCategory(category);
                    float temp = products.Count() / (float)12;
                    totalPage = (int)Math.Ceiling(temp);
                    products = await productClient.GetProductsByCategoryPage(category, page);
                }
            }
            else
            {
                products = await productClient.GetProductsByName(searchString);
                float temp = products.Count() / (float)12;
                totalPage = (int)Math.Ceiling(temp);
                products = await productClient.GetProductsByNamePage(searchString, page);
            }
            ViewBag.totalPage = totalPage;
            ViewBag.page = page;
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Rating([FromForm] string comment, [FromForm] int ratingsValue, [FromForm] int ProductId)
        {
            //var session = Request.HttpContext.Session.GetString(Variable.JWT);
            var token = Request.Cookies[Variable.JWT_Token];
            var email = "";
            if (token == null)
            {
                return Redirect("/Account/Login");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var userInfo = tokenHandler.ReadJwtToken(token);
            email = (userInfo.Claims.FirstOrDefault(o => o.Type == "sub")?.Value);

            var rating = new RatingDTO();
            rating.Comments = comment;
            rating.Rate = ratingsValue;
            rating.PublishedDate = DateTime.Now;
            rating.ProductId = ProductId;

            await productClient.CreateRating(rating, email);

            return Redirect("/Product/pd?id=" + ProductId);
        }

        [Route("pd")]
        public async Task<IActionResult> ProductSingle(int Id)
        {
            var products = await productClient.GetProduct(Id);
            var screen = await screenClient.GetScreen(products.ScreenId);
            var ram = await ramClient.GetRam(products.RamId);
            var processor = await processorClient.GetProcessor(products.ProcessorId);
            ViewBag.screen = screen;
            ViewBag.ram = ram;
            ViewBag.processor = processor;

            var session = HttpContext.Session;
            string jsoncart = session.GetString(Variable.CARTKEY);
            if (jsoncart != null)
            {
                List<CartDTO> cart = JsonConvert.DeserializeObject<List<CartDTO>>(jsoncart);
                foreach (var item in cart)
                {
                    if (item.Product.ProcessorId == Id)
                    {
                        products.Quantity = products.Quantity - item.Quantity;
                    }
                }
            }

            return View(products);
        }
    }
}
