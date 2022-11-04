using CustomerSite.Clients;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShareView.Constants;
using ShareView.DTO;
using System.IdentityModel.Tokens.Jwt;

namespace CustomerSite.Controllers
{
    [Route("sanpham")]
    public class SanPhamController : Controller
    {

        //protected readonly HttpClient productService;
        /*private readonly ManHinhController productService;*/
        /* public SanPhamController(HttpClient productService)
         {
             this.productService = productService;
         }*/
        private readonly ILogger<SanPhamController> _logger;
        private readonly IProductClient productClient;
        private readonly IDMClient dMClient;
        private readonly IManHinhClient manHinhlient;
        private readonly IBoNhoRamClient boNhoRamClient;
        private readonly IBoXuLyClient boXuLyClient;
        private readonly IUserClient userClient;
        /*public const string CARTKEY = "cart";*/
        public SanPhamController(ILogger<SanPhamController> logger, IProductClient productClient, IDMClient dMClient, IManHinhClient manHinhlient, IBoNhoRamClient boNhoRamClient, IBoXuLyClient boXuLyClient, IUserClient userClient)
        {
            _logger = logger;
            this.productClient = productClient;
            this.dMClient = dMClient;
            this.manHinhlient = manHinhlient;
            this.boNhoRamClient = boNhoRamClient;
            this.boXuLyClient = boXuLyClient;
            this.userClient = userClient;
        }
        public async Task<IActionResult> Index(int category, string searchString, int page = 1)
        {
            int totalPage;
            List<SanPhamDTO> products = new List<SanPhamDTO>();
            var categoryList = await dMClient.GetDMSP();
            ViewBag.CategoryList = categoryList;
            ViewBag.category = category;
            ViewBag.searchString = searchString;
            if (string.IsNullOrEmpty(searchString))
            {
                if (category <= 0)
                {
                    products = await productClient.GetTatCaSanPham();
                    float temp = products.Count() / (float)12;
                    totalPage = (int)Math.Ceiling(temp);
                    products = await productClient.GetSanPhamTheoTrang(page);
                }
                else
                {
                    products = await productClient.GetSanPhamTheoDM(category);
                    float temp = products.Count() / (float)12;
                    totalPage = (int)Math.Ceiling(temp);
                    products = await productClient.GetSanPhamTheoDmTheoTrang(category, page);
                }
            }
            else
            {
                products = await productClient.GetSanPhamTheoTen(searchString);
                float temp = products.Count() / (float)12;
                totalPage = (int)Math.Ceiling(temp);
                products = await productClient.GetSanPhamTheoTenTheoTrang(searchString, page);
            }
            ViewBag.totalPage = totalPage;
            ViewBag.page = page;
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Rating([FromForm] string comment, [FromForm] int ratingsValue, [FromForm] int SanPhamId)
        {
            var session = Request.HttpContext.Session.GetString(Variable.JWT);
            var token = Request.Cookies["JwtToken"];
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
            rating.sanPhamId = SanPhamId;

            await productClient.CreateRating(rating, email);

            return Redirect("/SanPham/pd?id=" + SanPhamId);
        }

        [Route("pd")]
        public async Task<IActionResult> ProductSingle(int Id)
        {
            var products = await productClient.GetSanPham(Id);
            var manhinh = await manHinhlient.GetManHinh(products.ManHinhId);
            var boNhoRam = await boNhoRamClient.GetBoNhoRam(products.RamId);
            var boXuLy = await boXuLyClient.GetBoXuLy(products.ManHinhId);
            ViewBag.manHinh = manhinh;
            ViewBag.boNhoRam = boNhoRam;
            ViewBag.boXuLy = boXuLy;

            var session = HttpContext.Session;
            string jsoncart = session.GetString(Variable.CARTKEY);
            if (jsoncart != null)
            {
                List<CartDTO> cart = JsonConvert.DeserializeObject<List<CartDTO>>(jsoncart);
                foreach (var item in cart)
                {
                    if (item.Sanpham.SanPhamId == Id)
                    {
                        products.SoLuong = products.SoLuong - item.SL;
                    }
                }
            }

            return View(products);
        }
    }
}
