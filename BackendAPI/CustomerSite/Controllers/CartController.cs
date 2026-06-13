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
        private readonly IInvoiceClient invoiceClient;
        private readonly IUserClient userClient;
        /*public const string CARTKEY = "cart";*/

        public CartController(ILogger<HomeController> logger, IProductClient productClient, IInvoiceClient invoiceClient, IUserClient userClient)
        {
            _logger = logger;
            this.productClient = productClient;
            this.invoiceClient = invoiceClient;
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
        //// Remove cart from session
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

        // Add product to cart.
        public async Task<ActionResult> AddToCart([FromRoute] int Id)
        {

            var products = await productClient.GetProduct(Id);

            if (products == null)
                return NotFound("Product not found");

            // Add or increment the product in the cart.
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


            // Save cart to session.
            SaveCartSession(cart);

            // Redirect to the cart page.
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> AddToCartQuantity([FromForm] int Id, [FromForm] int quantity)
        {
            var products = await productClient.GetProduct(Id);

            if (products == null)
                return NotFound("Product not found");

            // Add or increment the product in the cart.
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

            // Save cart to session.
            SaveCartSession(cart);

            // Redirect to the cart page.
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
        public async Task<IActionResult> CheckOut([FromForm] string fullName, [FromForm] string shippingAddress, [FromForm] string phone, [FromForm] string email)
        {
            var cart = GetCartItems();
            if (!string.IsNullOrEmpty(email))
            {
                InvoiceDTO invoice = new InvoiceDTO();
                long? total = 0;
                invoice.Receiver = fullName;
                invoice.Address = shippingAddress;
                invoice.Phone = phone;
                invoice.DateReceived = DateTime.Now;
                foreach (var item in cart)
                {
                    total += item.Product.Price * item.Quantity;
                }
                invoice.Total = total;
                await invoiceClient.AddInvoice(invoice, email);
                var _invoice = await invoiceClient.GetInvoice();
                int temp;
                if (_invoice == null)
                {
                    temp = 1;
                }
                else
                {
                    temp = _invoice.InvoiceId;
                }
                foreach (var item in cart)
                {
                    InvoiceDetailDTO invoiceDetail = new InvoiceDetailDTO();
                    invoiceDetail.InvoiceId = temp;
                    invoiceDetail.ProductId = item.Product.ProductId;
                    invoiceDetail.Quantity = item.Quantity;
                    await invoiceClient.AddInvoiceDetail(invoiceDetail);
                }
                ClearCart();
                RedirectToAction(nameof(Index));
            }
            return Redirect("/Home/Index");
        }
    }
}
