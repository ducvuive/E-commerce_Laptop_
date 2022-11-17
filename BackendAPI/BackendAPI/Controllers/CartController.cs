using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public CartController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        List<Cart> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString("cart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<Cart>>(jsoncart);
            }
            return new List<Cart>();
        }
    }
}
