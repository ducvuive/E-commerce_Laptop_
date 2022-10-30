using CustomerSite.Clients;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public const string CARTKEY = "cart";
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
        /*[HttpGet("Index/{tensp}")]*/
        public async Task<IActionResult> Index(int dm, int page = 1, [FromForm] string a = "")
        {
            int totalPage;
            List<SanPhamDTO> products = new List<SanPhamDTO>();
            /*if (string.IsNullOrEmpty(tensp))
            {*/
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
            ViewBag.totalPage = totalPage;
            ViewBag.page = page;
            /*}
            else
            {
                tensp = tensp.Trim().ToLower();
            }*/
            var dmsp = await dMClient.GetDMSP();
            ViewBag.Dmsp = dmsp;
            ViewBag.dm = dm;
            return View(products);
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
            string jsoncart = session.GetString(CARTKEY);
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
