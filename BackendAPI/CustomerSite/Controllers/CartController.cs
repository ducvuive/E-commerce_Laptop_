using CustomerSite.Clients;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly IDistributedCache distributedCache;
        /*public const string CARTKEY = "cart";*/

        public CartController(ILogger<HomeController> logger, IProductClient productClient, IInvoiceClient invoiceClient, IUserClient userClient, IDistributedCache distributedCache)
        {
            _logger = logger;
            this.productClient = productClient;
            this.invoiceClient = invoiceClient;
            this.userClient = userClient;
            this.distributedCache = distributedCache;
        }

        private const string UserCartKeyPrefix = "cart:user:";
        private static readonly DistributedCacheEntryOptions CartCacheOptions = new()
        {
            SlidingExpiration = TimeSpan.FromDays(7)
        };

        private sealed class CartSessionItem
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }

        async Task<List<CartSessionItem>> GetCartItemsForCurrentCustomer()
        {
            var userCartKey = GetUserCartKey();
            if (userCartKey is null)
            {
                return GetCartSessionItems();
            }

            var userCartItems = await GetCartCacheItems(userCartKey);
            var sessionCartItems = GetCartSessionItems();
            if (sessionCartItems.Count == 0)
            {
                return userCartItems;
            }

            var mergedCartItems = NormalizeCartItems(userCartItems.Concat(sessionCartItems));
            await SaveCartForCurrentCustomer(mergedCartItems);
            HttpContext.Session.Remove(Variable.CARTKEY);
            return mergedCartItems;
        }

        async Task SaveCartForCurrentCustomer(List<CartSessionItem> cartItems)
        {
            var normalizedCartItems = NormalizeCartItems(cartItems);
            var userCartKey = GetUserCartKey();
            if (userCartKey is null)
            {
                SaveCartSession(normalizedCartItems);
                return;
            }

            if (normalizedCartItems.Count == 0)
            {
                await distributedCache.RemoveAsync(userCartKey);
                return;
            }

            var jsoncart = JsonConvert.SerializeObject(normalizedCartItems);
            await distributedCache.SetStringAsync(userCartKey, jsoncart, CartCacheOptions);
        }

        async Task ClearCartForCurrentCustomer()
        {
            HttpContext.Session.Remove(Variable.CARTKEY);
            var userCartKey = GetUserCartKey();
            if (userCartKey is not null)
            {
                await distributedCache.RemoveAsync(userCartKey);
            }
        }

        string? GetUserCartKey()
        {
            var userId = Request.Cookies[Variable.Refresh_UserId];
            return string.IsNullOrWhiteSpace(userId) ? null : $"{UserCartKeyPrefix}{userId}";
        }

        async Task<List<CartSessionItem>> GetCartCacheItems(string key)
        {
            var jsoncart = await distributedCache.GetStringAsync(key);
            return DeserializeCartItems(jsoncart);
        }

        List<CartSessionItem> GetCartSessionItems()
        {
            var session = HttpContext.Session;
            string? jsoncart = session.GetString(Variable.CARTKEY);
            return DeserializeCartItems(jsoncart);
        }

        static List<CartSessionItem> DeserializeCartItems(string? jsoncart)
        {
            if (jsoncart != null)
            {
                var cartItems = JsonConvert.DeserializeObject<List<CartSessionItem>>(jsoncart);
                if (cartItems != null && cartItems.All(item => item.ProductId > 0))
                {
                    return NormalizeCartItems(cartItems);
                }

                var legacyCartItems = JsonConvert.DeserializeObject<List<CartDTO>>(jsoncart);
                if (legacyCartItems != null)
                {
                    return NormalizeCartItems(legacyCartItems
                        .Where(item => item.Product != null)
                        .Select(item => new CartSessionItem
                        {
                            ProductId = item.Product.ProductId,
                            Quantity = item.Quantity
                        }));
                }
            }
            return new List<CartSessionItem>();
        }

        async Task<List<CartDTO>> GetCartItems()
        {
            var cartItems = await GetCartItemsForCurrentCustomer();
            var hydratedCart = new List<CartDTO>();
            var normalizedCartItems = new List<CartSessionItem>();

            foreach (var cartItem in cartItems)
            {
                var product = await productClient.GetProduct(cartItem.ProductId);
                if (product.ProductId == 0 || product.IsDisable || product.Quantity.GetValueOrDefault() <= 0 || product.Price is null)
                {
                    continue;
                }

                var availableQuantity = product.Quantity.GetValueOrDefault();
                var quantity = Math.Min(cartItem.Quantity, availableQuantity);
                hydratedCart.Add(new CartDTO
                {
                    Product = product,
                    Quantity = quantity
                });
                normalizedCartItems.Add(new CartSessionItem
                {
                    ProductId = product.ProductId,
                    Quantity = quantity
                });
            }

            if (!CartSessionItemsEqual(cartItems, normalizedCartItems))
            {
                await SaveCartForCurrentCustomer(normalizedCartItems);
            }

            return hydratedCart;
        }

        static List<CartSessionItem> NormalizeCartItems(IEnumerable<CartSessionItem> cartItems)
        {
            return cartItems
                .Where(item => item.ProductId > 0 && item.Quantity > 0)
                .GroupBy(item => item.ProductId)
                .Select(group => new CartSessionItem
                {
                    ProductId = group.Key,
                    Quantity = group.Sum(item => item.Quantity)
                })
                .ToList();
        }

        static bool CartSessionItemsEqual(List<CartSessionItem> left, List<CartSessionItem> right)
        {
            return left.Count == right.Count
                && left.OrderBy(item => item.ProductId)
                    .Zip(right.OrderBy(item => item.ProductId))
                    .All(pair => pair.First.ProductId == pair.Second.ProductId
                        && pair.First.Quantity == pair.Second.Quantity);
        }

        void SaveCartSession(List<CartSessionItem> cartItems)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(NormalizeCartItems(cartItems));
            session.SetString(Variable.CARTKEY, jsoncart);
        }
        public async Task<IActionResult> Index()
        {
            return View(await GetCartItems());
        }

        // Add product to cart.
        public async Task<ActionResult> AddToCart([FromRoute] int Id)
        {

            var products = await productClient.GetProduct(Id);

            if (products == null)
                return NotFound("Product not found");
            if (products.ProductId == 0 || products.IsDisable || products.Quantity.GetValueOrDefault() <= 0 || products.Price is null)
                return NotFound("Product not available");

            // Add or increment the product in the cart.
            var cart = await GetCartItemsForCurrentCustomer();
            var cartitem = cart.Find(p => p.ProductId == Id);
            if (cartitem != null)
            {
                cartitem.Quantity = Math.Min(cartitem.Quantity + 1, products.Quantity.GetValueOrDefault());
            }
            else
            {
                cart.Add(new CartSessionItem() { Quantity = 1, ProductId = products.ProductId });
            }


            // Save cart to session.
            await SaveCartForCurrentCustomer(cart);

            // Redirect to the cart page.
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> AddToCartQuantity([FromForm] int Id, [FromForm] int quantity)
        {
            var products = await productClient.GetProduct(Id);

            if (products == null)
                return NotFound("Product not found");
            if (products.ProductId == 0 || products.IsDisable || products.Quantity.GetValueOrDefault() <= 0 || products.Price is null)
                return NotFound("Product not available");
            if (quantity <= 0)
                return RedirectToAction("Index");

            // Add or increment the product in the cart.
            var cart = await GetCartItemsForCurrentCustomer();
            var cartitem = cart.Find(p => p.ProductId == Id);
            if (cartitem != null)
            {
                cartitem.Quantity = Math.Min(cartitem.Quantity + quantity, products.Quantity.GetValueOrDefault());
            }
            else
            {
                cart.Add(new CartSessionItem() { Quantity = Math.Min(quantity, products.Quantity.GetValueOrDefault()), ProductId = products.ProductId });
            }

            // Save cart to session.
            await SaveCartForCurrentCustomer(cart);

            // Redirect to the cart page.
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveCart([FromRoute] int id)
        {
            var cart = await GetCartItemsForCurrentCustomer();
            var cartitem = cart.Find(p => p.ProductId == id);
            if (cartitem != null)
            {

                cart.Remove(cartitem);
            }

            await SaveCartForCurrentCustomer(cart);
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
            var cart = await GetCartItems();

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut([FromForm] string fullName, [FromForm] string shippingAddress, [FromForm] string phone, [FromForm] string email)
        {
            var cart = await GetCartItems();
            if (!string.IsNullOrEmpty(email))
            {
                if (cart.Count == 0)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (cart.Any(item => item.Product.IsDisable
                    || item.Product.Quantity.GetValueOrDefault() < item.Quantity
                    || item.Product.Price is null))
                {
                    ModelState.AddModelError(string.Empty, "Some products in your cart are no longer available.");
                    return View(cart);
                }

                var checkoutRequest = new CheckoutOrderRequestDTO
                {
                    Receiver = fullName,
                    Address = shippingAddress,
                    Phone = phone,
                    Email = email,
                    PaymentMethod = "COD",
                    Items = cart.Select(item => new CheckoutOrderItemDTO
                    {
                        ProductId = item.Product.ProductId,
                        Quantity = item.Quantity
                    }).ToList()
                };

                CheckoutOrderResponseDTO checkoutResponse;
                try
                {
                    checkoutResponse = await invoiceClient.CheckoutOrder(checkoutRequest);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(cart);
                }

                await ClearCartForCurrentCustomer();
                return RedirectToAction(nameof(OrderSuccess), new { id = checkoutResponse.InvoiceId });
            }
            return Redirect("/Home/Index");
        }

        public IActionResult OrderSuccess([FromRoute] int id)
        {
            ViewData["InvoiceId"] = id;
            return View();
        }
    }
}
