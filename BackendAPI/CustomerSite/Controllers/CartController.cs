using CustomerSite.Clients;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShareView.Constants;
using ShareView.DTO;

namespace CustomerSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductClient productClient;
        /*public const string CARTKEY = "cart";*/

        public CartController(ILogger<HomeController> logger, IProductClient productClient)
        {
            _logger = logger;
            this.productClient = productClient;
        }

        /*        public async Task<IActionResult> Index()
                {
                    var userId = User.FindFirstValue(ClaimTypes.Email);
                    return Ok(userId);
                }*/
        List<CartDTO> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString(Variable.CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartDTO>>(jsoncart);
            }
            return new List<CartDTO>();
        }
        //// Xóa giỏ khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(Variable.CARTKEY);
        }

        void SaveCartSession(List<CartDTO> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(Variable.CARTKEY, jsoncart);
        }
        public async Task<IActionResult> Index()
        {
            return View(GetCartItems());
        }

        // Thêm sản phẩm vô cart
        public async Task<ActionResult> AddToCart([FromRoute] int Id)
        {

            var products = await productClient.GetSanPham(Id);

            if (products == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Sanpham.SanPhamId == Id);
            if (cartitem != null)
            {
                cartitem.SL++;
            }
            else
            {
                cart.Add(new CartDTO() { SL = 1, Sanpham = products });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);

            // Chuyển đến trang hiện thị Cart
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> AddToCartQuantity([FromForm] int Id, [FromForm] int quantity)
        {
            var products = await productClient.GetSanPham(Id);

            if (products == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Sanpham.SanPhamId == Id);
            if (cartitem != null)
            {
                cartitem.SL += quantity;
            }
            else
            {
                cart.Add(new CartDTO() { SL = quantity, Sanpham = products });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);

            // Chuyển đến trang hiện thị Cart
            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart([FromRoute] int id)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Sanpham.SanPhamId == id);
            if (cartitem != null)
            {

                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> CheckOut(/*string userName*/)
        {
            if (Request.HttpContext.Session.GetString(Variable.JWT) == null)
            {
                return Redirect("/Account/Login");
            }
            /*if (userName == null)
            {
                return Redirect("/Account/Login");
            }*/
            var cart = GetCartItems();

            return View(cart);
        }
    }
}