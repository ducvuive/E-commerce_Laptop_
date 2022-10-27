﻿using CustomerSite.Clients;
using CustomerSite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShareView.DTO;
using System.Diagnostics;

namespace CustomerSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductClient productClient;

        public CartController(ILogger<HomeController> logger, IProductClient productClient)
        {
            _logger = logger;
            this.productClient = productClient;
        }

        List<CartDTO> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString("cart");
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
            session.Remove("cart");
        }


        //// Lưu Cart (Danh sách giỏ hàng) vào session
        void SaveCartSession(List<CartDTO> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString("cart", jsoncart);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}