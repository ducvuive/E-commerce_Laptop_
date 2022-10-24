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
        private readonly IManHinhClient manHinhlient;
        private readonly IBoNhoRamClient boNhoRamClient;
        private readonly IBoXuLyClient boXuLyClient;
        public SanPhamController(ILogger<SanPhamController> logger, IProductClient productClient, IDMClient dMClient, IManHinhClient manHinhlient, IBoNhoRamClient boNhoRamClient, IBoXuLyClient boXuLyClient)
        {
            _logger = logger;
            this.productClient = productClient;
            this.dMClient = dMClient;
            this.manHinhlient = manHinhlient;
            this.boNhoRamClient = boNhoRamClient;
            this.boXuLyClient = boXuLyClient;
        }
        /*        [Route("Index")]*/
        public async Task<IActionResult> Index(int page = 1)
        {
            var products = await productClient.GetSanPhamTheoTrang(page);
            var totalPage = await productClient.GetPage();
            var dmsp = await dMClient.GetDMSP();
            ViewBag.Dmsp = dmsp;
            ViewBag.totalPage = totalPage;
            ViewBag.page = page;
            return View(products);
        }
        /*        public async Task<IActionResult> Page()
                {
                    var products = await productClient.GetSanPhamTheoTrang(page);
                    var totalPage = await productClient.GetPage();
                    var dmsp = await dMClient.GetDMSP();
                    ViewBag.Dmsp = dmsp;
                    ViewBag.totalPage = totalPage;
                    ViewBag.page = page;
                    return View(products);
                }*/
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

            return View(products);
        }
    }
}
