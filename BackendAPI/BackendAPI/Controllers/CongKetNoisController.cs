using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareView.DTO;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongKetNoisController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public CongKetNoisController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/CongKetNois
        [HttpGet]
        public async Task<ActionResult<List<CongKetNoiDTO>>> GetCongKetNoi()
        {
            var congketNoi = await _context.CongKetNoi.ToListAsync();
            return _mapper.Map<List<CongKetNoiDTO>>(congketNoi);
        }

        // GET: api/CongKetNois/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CongKetNoiDTO>> GetCongKetNoi(int id)
        {
            var congKetNoi = await _context.CongKetNoi.FindAsync(id);

            if (congKetNoi == null)
            {
                return NotFound();
            }

            return _mapper.Map<CongKetNoiDTO>(congKetNoi);
        }
    }
}
