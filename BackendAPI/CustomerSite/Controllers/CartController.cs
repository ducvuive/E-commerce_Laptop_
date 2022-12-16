﻿using CustomerSite.Clients;
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
        private readonly IHoaDonClient hoaDonClient;
        private readonly IUserClient userClient;
        /*public const string CARTKEY = "cart";*/

        public CartController(ILogger<HomeController> logger, IProductClient productClient, IHoaDonClient hoaDonClient, IUserClient userClient)
        {
            _logger = logger;
            this.productClient = productClient;
            this.hoaDonClient = hoaDonClient;
            this.userClient = userClient;
        }
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
            var cartitem = cart.Find(p => p.Product.ProductId == Id);
            if (cartitem != null)
            {
                cartitem.Quantity++;
            }
            else
            {
                cart.Add(new CartDTO() { Quantity = 1, Product = products });
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
            var cartitem = cart.Find(p => p.Product.ProductId == Id);
            if (cartitem != null)
            {
                cartitem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartDTO() { Quantity = quantity, Product = products });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);

            // Chuyển đến trang hiện thị Cart
            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart([FromRoute] int id)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Product.ProductId == id);
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
            var token = Request.Cookies["JwtToken"];
            if (token == null)
            {
                return Redirect("/Account/Login");
            }
            var cart = GetCartItems();

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut([FromForm] string hoten, [FromForm] string diachi, [FromForm] string sdt, [FromForm] string email)
        {
            var cart = GetCartItems();
            if (!string.IsNullOrEmpty(email))
            {
                InvoiceDTO hd = new InvoiceDTO();
                long? total = 0;
                hd.Receiver = hoten;
                hd.Address = diachi;
                hd.Phone = sdt;
                hd.DateReceived = DateTime.Now;
                foreach (var item in cart)
                {
                    total += item.Product.Price * item.Quantity;
                }
                hd.Total = total;
                await hoaDonClient.AddHoaDon(hd, email);
                var _hoaDon = await hoaDonClient.GetHoaDon();
                int temp;
                if (_hoaDon == null)
                {
                    temp = 1;
                }
                else
                {
                    temp = _hoaDon.InvoiceId;
                }
                foreach (var item in cart)
                {
                    InvoiceDetailDTO ct = new InvoiceDetailDTO();
                    ct.InvoiceId = temp;
                    ct.ProductId = item.Product.ProductId;
                    ct.Quantity = item.Quantity;
                    await hoaDonClient.AddCTHD(ct);
                }
                ClearCart();
                RedirectToAction(nameof(Index));
            }
            return Redirect("/Home/Index");
        }
    }
}