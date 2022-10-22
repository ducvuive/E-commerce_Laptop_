using CustomerSite.Clients;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.Controllers
{
    [Route("api/sanpham")]
    [ApiController]
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
        public SanPhamController(ILogger<SanPhamController> logger, IProductClient productClient)
        {
            _logger = logger;
            this.productClient = productClient;
        }
        public async Task<IActionResult> IndexAsync()
        {
            /*var response = await productService.GetAsync("api/SanPhams/all");
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<SanPhamDTO>>(contents);*/
            var products = await productClient.GetAllProduct();
            return View(products);
        }

    }
}
