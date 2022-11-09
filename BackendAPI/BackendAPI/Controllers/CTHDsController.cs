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
    public class CTHDsController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public CTHDsController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/CTHDs
        [HttpGet]
        public async Task<ActionResult<List<CTHD_DTO>>> GetCTHD()
        {
            var cthd = await _context.CTHD.ToListAsync();
            return _mapper.Map<List<CTHD_DTO>>(cthd);
        }

        // GET: api/CTHDs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CTHD_DTO>> GetCTHD(int id)
        {
            var cTHD = await _context.CTHD.FindAsync(id);

            if (cTHD == null)
            {
                return NotFound();
            }

            return _mapper.Map<CTHD_DTO>(cTHD);
        }

        // PUT: api/CTHDs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCTHD(int id, CTHD_DTO cTHD_DTO)
        {
            if (id != cTHD_DTO.SanPhamId)
            {
                return BadRequest();
            }

            CTHD congKetNoi = _mapper.Map<CTHD>(cTHD_DTO);
            _context.Entry(cTHD_DTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CTHDExists(id))
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

        // POST: api/CTHDs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CTHD_DTO>> PostCTHD(CTHD_DTO cTHD_DTO)
        {
            var sanPham = await _context.SanPham.FindAsync(cTHD_DTO.SanPhamId);
            sanPham.SoLuong = sanPham.SoLuong - cTHD_DTO.SoLuong;
            var cTHD = new CTHD
            {
                SanPhamId = cTHD_DTO.SanPhamId,
                HoaDonId = cTHD_DTO.HoaDonId,
                SoLuong = cTHD_DTO.SoLuong,
            };
            _context.CTHD.Add(cTHD);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CTHDExists(cTHD.SanPhamId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCTHD", new { id = cTHD.SanPhamId }, cTHD);
        }

        // DELETE: api/CTHDs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCTHD(int id)
        {
            var cTHD = await _context.CTHD.FindAsync(id);
            if (cTHD == null)
            {
                return NotFound();
            }

            _context.CTHD.Remove(cTHD);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CTHDExists(int id)
        {
            return _context.CTHD.Any(e => e.SanPhamId == id);
        }
    }
}
