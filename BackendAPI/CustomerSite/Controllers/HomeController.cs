using CustomerSite.Clients;
using CustomerSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductClient productClient;

        public HomeController(ILogger<HomeController> logger, IProductClient productClient)
        {
            _logger = logger;
            this.productClient = productClient;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productClient.GetSanPhamTopRaMat();
            return View(products);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}