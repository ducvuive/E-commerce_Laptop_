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
        public async Task<ActionResult<IEnumerable<HoaDonDTO>>> GetHoaDon()
        {
            var hd = await _context.HoaDon.ToListAsync();
            return _mapper.Map<List<HoaDonDTO>>(hd);
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

        // PUT: api/HoaDons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*        [HttpPut("{id}")]
                public async Task<IActionResult> PutHoaDon(int id, HoaDon hoaDon)
                {
                    if (id != hoaDon.HoaDonId)
                    {
                        return BadRequest();
                    }

                    _context.Entry(hoaDon).State = EntityState.Modified;

                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!HoaDonExists(id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return NoContent();
                }*/

        // POST: api/HoaDons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HoaDonDTO>> PostHoaDon(HoaDonDTO hoaDonDTO)
        {
            /*var hoaDon = new HoaDon
            {
                NgayHD = hoaDonDTO.NgayHD,
                NguoiNhan = hoaDonDTO.NguoiNhan,
                SDT = hoaDonDTO.SDT,
                DiaChiGiaoHang = hoaDonDTO.DiaChiGiaoHang,
                TongTien = hoaDonDTO.TongTien,
                MaKhacHangId = hoaDonDTO.MaKhacHangId,
            };*/
            HoaDon hoaDon = _mapper.Map<HoaDon>(hoaDonDTO);
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
