using CustomerSite.Clients;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.Controllers
{
    [Route("categories")]
    [ApiController]
    public class CategoryController : Controller
    {

        //protected readonly HttpClient productService;
        /*private readonly ScreenController productService;*/
        /* public ProductController(HttpClient productService)
         {
             this.productService = productService;
         }*/
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryClient categoryClient;
        public CategoryController(ILogger<CategoryController> logger, ICategoryClient categoryClient)
        {
            _logger = logger;
            this.categoryClient = categoryClient;
        }
        //[HttpGet("GetProductsByPage/{page}")]
        public async Task<IActionResult> Category()
        {
            var categories = await categoryClient.GetCategories();
            ViewBag.Categories = categories;
            return Ok(categories);
        }

    }
}
