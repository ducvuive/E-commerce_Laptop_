using CustomerSite.Authentication;
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
            if (ModelState.IsValid)
            {
                //var client = clientFactory.CreateClient();
                //var a = 1;
                var jsonInString = JsonConvert.SerializeObject(model);

                //var response = await clientFactory.PostAsync("login", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                var response = await clientFactory.PostAsync("/Auth/login", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                var contents = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    TempData["Error"] = "Username or password is incorrect";
                    return View();
                }
                //var contents = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LoginResponseModel>(contents);

                if (data != null && data.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    AuthCookieHelper.AppendAuthCookies(HttpContext, data);

                    return RedirectToAction("Index", "Home");
                }

            }

            return View(model);
        }
        public IActionResult Logout()
        {
            var session = HttpContext.Session;
            AuthCookieHelper.DeleteAuthCookies(HttpContext);
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

            var response = await clientFactory.PostAsync("/Auth/register", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
            {
                TempData["ErrorRegister"] = "Email is already registered.";
                return View();
            }
            var contents = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<RegisterResponseModel>(contents);
            if (data != null && data.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

    }
}
