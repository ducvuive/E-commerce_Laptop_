using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareView.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        public UserController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<UserController>
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<List<UserIdentityDTO>>> GetUser()
        {
            var user = await _context.UserIdentity.ToListAsync();
            return _mapper.Map<List<UserIdentityDTO>>(user);
        }

        // GET api/<UserController>/5
        [HttpGet("{email}")]
        public async Task<ActionResult<UserIdentityDTO>> Get(string email)
        {
            var user = await _context.UserIdentity.FirstOrDefaultAsync(i => i.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            return _mapper.Map<UserIdentityDTO>(user);
        }
    }
}
