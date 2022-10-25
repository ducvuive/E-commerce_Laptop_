using CustomerSite.Clients;
using Microsoft.AspNetCore.Mvc;
using ShareView.DTO;

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
        public async Task<IActionResult> Index(int dm/*, string tensp = "abc"*/, int page = 1)
        {
            int totalPage;
            List<SanPhamDTO> products = new List<SanPhamDTO>();
            products = await productClient.GetTatCaSanPham();
            /*  if (string.IsNullOrEmpty(tensp))
              {*/
            products = await productClient.GetTatCaSanPham();
            if (dm <= 0)
            {
                products = await productClient.GetTatCaSanPham();
                float temp = products.Count() / (float)12;
                totalPage = (int)Math.Ceiling(temp);
                products = await productClient.GetSanPhamTheoTrang(page);

            }
            else
            {
                products = await productClient.GetSanPhamTheoDM(dm);
                float temp = products.Count() / (float)12;
                totalPage = (int)Math.Ceiling(temp);
                products = await productClient.GetSanPhamTheoDmTheoTrang(dm, page);
            }
            /*     }
                 else
                 {
                     products = await productClient.GetSanPhamTheoTen(tensp);
                     float temp = products.Count() / (float)12;
                     totalPage = (int)Math.Ceiling(temp);
                     products = await productClient.GetSanPhamTheoTenTheoTrang(tensp, page);
                 }*/
            var dmsp = await dMClient.GetDMSP();
            ViewBag.Dmsp = dmsp;
            ViewBag.totalPage = totalPage;
            ViewBag.page = page;
            ViewBag.dm = dm;
            return View(products);
        }
        /*        [Route("dm")]
                public async Task<IActionResult> GetSanPhamTheoDM(int dm)
                {
                    var products = await productClient.GetSanPhamTheoDM(dm);
                    var totalPage = (products.Count() / 12) + 1;
                    var dmsp = await dMClient.GetDMSP();
                    ViewBag.totalPage = totalPage;
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
