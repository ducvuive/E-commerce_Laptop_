using CustomerSite.Clients;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.Controllers
{
    [Route("dmsp")]
    [ApiController]
    public class DanhMucSanPhamController : Controller
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
        public DanhMucSanPhamController(ILogger<SanPhamController> logger, IDMClient dMClient)
        {
            _logger = logger;
            this.dMClient = dMClient;
        }
        //[HttpGet("GetSanPhamTheoTrang/{page}")]
        public async Task<IActionResult> DanhMucSanPham()
        {
            var dmsp = await dMClient.GetDMSP();
            ViewBag.Dmsp = dmsp;
            return Ok(dmsp);
        }

    }
}
