using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShareView.Constants;
using ShareView.DTO;
using System.Text;

namespace CustomerSite.Controllers
{
    public class AccountController : Controller
    {
        //private IHttpClientFactory clientFactory;
        private HttpClient clientFactory;
        public AccountController(HttpClient clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        public IActionResult Login()
        {
            TempData["Error"] = null;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var a = 1;
            if (ModelState.IsValid)
            {
                //var client = clientFactory.CreateClient();
                //var a = 1;
                var jsonInString = JsonConvert.SerializeObject(model);

                //var response = await clientFactory.PostAsync("login", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                var response = await clientFactory.PostAsync("login", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                //var contents = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    TempData["Error"] = "Tên đăng nhập hoặc mật khẩu không chính xác";
                    return View();
                }
                var contents = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LoginResponseModel>(contents);

                if (data != null && data.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Request.HttpContext.Session.SetString(Variable.JWT, data.Value);
                    var cookieOption = new CookieOptions()
                    {
                        HttpOnly = true,
                        Expires = DateTime.UtcNow.AddMinutes(15),
                        IsEssential = true
                    };
                    Response.Cookies.Append(Variable.JWT_Token, data.Value, cookieOption);

                    return RedirectToAction("Index", "Home");
                }

            }

            return View(model);
        }
        public IActionResult Logout()
        {
            var session = HttpContext.Session;
            Response.Cookies.Delete(Variable.JWT_Token);
            //session.Remove(Variable.JWT);
            session.Remove(Variable.CARTKEY);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            var jsonInString = JsonConvert.SerializeObject(model);

            var response = await clientFactory.PostAsync("register", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                TempData["Error"] = "Tài khoản đã ";
                return View();
            }
            var contents = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<RegisterResponseModel>(contents);

            if (data.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                TempData["Error"] = data.Value;
                return View();
            }
            if (data != null && data.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Home");
            }
            //var data.Value = data.Value;
            TempData["Error"] = data;
            return View(model);
        }

    }
}