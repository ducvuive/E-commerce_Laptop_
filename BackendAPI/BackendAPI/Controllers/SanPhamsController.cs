using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareView.DTO;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamsController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        public SanPhamsController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/SanPhams
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<List<SanPhamDTO>>> GetSanPham()
        {
            var sp = await _context.SanPham.ToListAsync();
            return _mapper.Map<List<SanPhamDTO>>(sp);
        }

        // GET: api/SanPhams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SanPhamDTO>> GetSanPham(int id)
        {
            var sanPham = await _context.SanPham.FindAsync(id);

            if (sanPham == null)
            {
                return NotFound();
            }

            return _mapper.Map<SanPhamDTO>(sanPham);
        }

        // PUT: api/SanPhams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanPham(int id, SanPhamDTO sanPhamDTO)
        {
            if (id != sanPhamDTO.SanPhamId)
            {
                return BadRequest();
            }

            SanPham sanPham = _mapper.Map<SanPham>(sanPhamDTO);
            _context.Entry(sanPham).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SanPhamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SanPhams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SanPham>> PostSanPham(SanPhamDTO sanPhamDTO)
        {
            /*var sanPham = new SanPham
            {
                KichThuoc = manHinhDTO.KichThuoc,
                DoPhanGiai = manHinhDTO.DoPhanGiai,
                TanSoQuet = manHinhDTO.TanSoQuet,
                CongNgheMH = manHinhDTO.CongNgheMH,
                CamUng = manHinhDTO.CamUng,
            };*/
            SanPham sanPham = _mapper.Map<SanPham>(sanPhamDTO);
            _context.SanPham.Add(sanPham);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSanPham", new { id = sanPham.SanPhamId }, sanPham);
        }

        // DELETE: api/SanPhams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSanPham(int id)
        {
            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }

            _context.SanPham.Remove(sanPham);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SanPhamExists(int id)
        {
            return _context.SanPham.Any(e => e.SanPhamId == id);
        }
    }
}
