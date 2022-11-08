using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareView.DTO;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoNhoRamsController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public BoNhoRamsController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/BoNhoRams
        [HttpGet]
        public async Task<ActionResult<List<BoNhoRamDTO>>> GetBoNhoRam()
        {
            var boNhoRam = await _context.BoNhoRam.ToListAsync();
            return _mapper.Map<List<BoNhoRamDTO>>(boNhoRam);
        }

        // GET: api/BoNhoRams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoNhoRamDTO>> GetBoNhoRam(int id)
        {
            var boNhoRam = await _context.BoNhoRam.FindAsync(id);

            if (boNhoRam == null)
            {
                return NotFound();
            }

            return _mapper.Map<BoNhoRamDTO>(boNhoRam);
        }
    }
}
