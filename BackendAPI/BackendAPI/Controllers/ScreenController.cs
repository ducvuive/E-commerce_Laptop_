using AutoMapper;
using BackendAPI.Persistence.Identity;
using BackendAPI.Persistence.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareView.DTO;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public ScreenController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Screens
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<ScreenDTO>>> GetScreen()
        {
            var mh = await _context.Screen.ToListAsync();
            return _mapper.Map<List<ScreenDTO>>(mh);
        }

        // GET: api/Screens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScreenDTO>> GetScreen(int id)
        {
            var screen = await _context.Screen.FindAsync(id);

            if (screen == null)
            {
                return NotFound();
            }

            return _mapper.Map<ScreenDTO>(screen);
        }
    }
}
