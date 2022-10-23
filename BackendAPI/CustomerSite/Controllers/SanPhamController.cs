using CustomerSite.Clients;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.Controllers
{
    [Route("sanpham")]
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
        private readonly IDMClient dMClient;
        public SanPhamController(ILogger<SanPhamController> logger, IProductClient productClient, IDMClient dMClient)
        {
            _logger = logger;
            this.productClient = productClient;
            this.dMClient = dMClient;
        }
        public async Task<IActionResult> IndexAsync(int page = 1)
        {
            /*var response = await productService.GetAsync("api/SanPhams/all");
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<SanPhamDTO>>(contents);*/
            //var products = await productClient.GetAllProduct();
            var products1 = await productClient.GetSanPhamTheoTrang(page);
            var totalPage = await productClient.GetPage();
            var dmsp = await dMClient.GetDMSP();
            ViewBag.Dmsp = dmsp;
            ViewBag.totalPage = totalPage;
            ViewBag.page = page;
            return View(products1);
        }
        /*        [HttpGet("sanpham/GetDanhMucSanPham")]
                public async Task<IActionResult> DanhMucSanPham()
                {

                    var t = 1;
                    return Ok(dmsp);
                }*/

    }
}
