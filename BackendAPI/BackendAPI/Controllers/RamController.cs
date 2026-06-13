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
    public class RamController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public RamController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Rams
        [HttpGet]
        public async Task<ActionResult<List<RamDTO>>> GetRam()
        {
            var ram = await _context.Ram.ToListAsync();
            return _mapper.Map<List<RamDTO>>(ram);
        }

        // GET: api/Rams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RamDTO>> GetRam(int id)
        {
            var ram = await _context.Ram.FindAsync(id);

            if (ram == null)
            {
                return NotFound();
            }

            return _mapper.Map<RamDTO>(ram);
        }
    }
}
