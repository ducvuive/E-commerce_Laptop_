using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareView.DTO;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoXuLiesController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public BoXuLiesController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/BoXuLies
        [HttpGet]
        public async Task<ActionResult<List<BoXuLyDTO>>> GetBoXuLy()
        {
            var boXuLy = await _context.BoXuLy.ToListAsync();
            return _mapper.Map<List<BoXuLyDTO>>(boXuLy);
        }

        // GET: api/BoXuLies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoXuLyDTO>> GetBoXuLy(int id)
        {
            var boXuLy = await _context.BoXuLy.FindAsync(id);

            if (boXuLy == null)
            {
                return NotFound();
            }

            return _mapper.Map<BoXuLyDTO>(boXuLy);
        }
    }
}
