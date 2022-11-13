using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareView.DTO;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public RamController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/BoNhoRams
        [HttpGet]
        public async Task<ActionResult<List<RamDTO>>> GetBoNhoRam()
        {
            var boNhoRam = await _context.Ram.ToListAsync();
            return _mapper.Map<List<RamDTO>>(boNhoRam);
        }

        // GET: api/BoNhoRams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RamDTO>> GetBoNhoRam(int id)
        {
            var boNhoRam = await _context.Ram.FindAsync(id);

            if (boNhoRam == null)
            {
                return NotFound();
            }

            return _mapper.Map<RamDTO>(boNhoRam);
        }
    }
}
