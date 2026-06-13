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
    public class ProcessorController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public ProcessorController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/BoXuLies
        [HttpGet]
        public async Task<ActionResult<List<ProcessorDTO>>> GetProcessor()
        {
            var processor = await _context.Processor.ToListAsync();
            return _mapper.Map<List<ProcessorDTO>>(processor);
        }

        // GET: api/BoXuLies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessorDTO>> GetProcessor(int id)
        {
            var processor = await _context.Processor.FindAsync(id);

            if (processor == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProcessorDTO>(processor);
        }
    }
}
