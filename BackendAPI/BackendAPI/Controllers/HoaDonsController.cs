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
    public class HoaDonsController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public HoaDonsController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/HoaDons
        [HttpGet]
        public async Task<ActionResult<HoaDonDTO>> GetHoaDon()
        {
            var hd = _context.HoaDon.OrderByDescending(s => s.HoaDonId).FirstOrDefault();
            return Ok(hd);
        }

        // GET: api/HoaDons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HoaDonDTO>> GetHoaDon(int Id)
        {
            var hoaDon = await _context.HoaDon.FindAsync(Id);

            if (hoaDon == null)
            {
                return NotFound();
            }

            return _mapper.Map<HoaDonDTO>(hoaDon);
        }
        // POST: api/HoaDons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{userName}")]
        public async Task<ActionResult<HoaDonDTO>> PostHoaDon([FromBody] HoaDonDTO hoaDonDTO, string email)
        {
            var user = await _context.UserIdentity.FirstOrDefaultAsync(i => i.Email == email);
            HoaDon hoaDon = _mapper.Map<HoaDon>(hoaDonDTO);
            hoaDon.MaKhacHangId = user;
            _context.HoaDon.Add(hoaDon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHoaDon", new { id = hoaDon.HoaDonId }, hoaDon);
        }

        // DELETE: api/HoaDons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoaDon(int id)
        {
            var hoaDon = await _context.HoaDon.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            _context.HoaDon.Remove(hoaDon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HoaDonExists(int id)
        {
            return _context.HoaDon.Any(e => e.HoaDonId == id);
        }
    }
}
