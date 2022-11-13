using AutoMapper;
using BackendAPI.Areas.Identity.Data;
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

        // GET: api/ManHinhs
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<ManHinhDTO>>> GetManHinh()
        {
            var mh = await _context.ManHinh.ToListAsync();
            return _mapper.Map<List<ManHinhDTO>>(mh);
        }

        // GET: api/ManHinhs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManHinhDTO>> GetManHinh(int id)
        {
            var manHinh = await _context.ManHinh.FindAsync(id);

            if (manHinh == null)
            {
                return NotFound();
            }

            return _mapper.Map<ManHinhDTO>(manHinh);
        }
    }
}
