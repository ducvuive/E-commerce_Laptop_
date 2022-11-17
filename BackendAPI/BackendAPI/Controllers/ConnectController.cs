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
        public async Task<ActionResult<List<ConnectDTO>>> GetCongKetNoi()
        {
            var connect = await _context.Connect.ToListAsync();
            return _mapper.Map<List<ConnectDTO>>(connect);
        }

        // GET: api/CongKetNois/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConnectDTO>> GetCongKetNoi(int id)
        {
            var connect = await _context.Connect.FindAsync(id);

            if (connect == null)
            {
                return NotFound();
            }

            return _mapper.Map<ConnectDTO>(connect);
        }
    }
}
