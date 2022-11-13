using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareView.DTO;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public ConnectController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/CongKetNois
        [HttpGet]
        public async Task<ActionResult<List<CongKetNoiDTO>>> GetCongKetNoi()
        {
            var connect = await _context.CongKetNoi.ToListAsync();
            return _mapper.Map<List<CongKetNoiDTO>>(connect);
        }

        // GET: api/CongKetNois/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CongKetNoiDTO>> GetCongKetNoi(int id)
        {
            var connect = await _context.CongKetNoi.FindAsync(id);

            if (connect == null)
            {
                return NotFound();
            }

            return _mapper.Map<CongKetNoiDTO>(connect);
        }
    }
}
