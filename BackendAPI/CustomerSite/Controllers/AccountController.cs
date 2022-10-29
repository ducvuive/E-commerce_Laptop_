using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

                var response = await clientFactory.PostAsync("login", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
                var contents = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LoginResponseModel>(contents);

                if (data != null && data.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Request.HttpContext.Session.SetString("JWT", data.Value);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            //var client = clientFactory.CreateClient();

            var jsonInString = JsonConvert.SerializeObject(model);

            var response = await clientFactory.PostAsync("register", new StringContent(jsonInString, Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<RegisterResponseModel>(contents);

            if (data != null && data.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                /*                foreach (var error in contents)
                                {
                                    ModelState.AddModelError(string.Empty, error.Description);
                                }*/
            }

            return View(model);
        }

    }
}