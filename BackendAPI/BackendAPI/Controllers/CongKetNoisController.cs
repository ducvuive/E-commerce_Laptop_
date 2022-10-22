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

        // PUT: api/CongKetNois/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCongKetNoi(int id, CongKetNoiDTO congKetNoiDTO)
        {
            if (id != congKetNoiDTO.CongKetNoiId)
            {
                return BadRequest();
            }

            //var boNhoRam = await _context.BoXuLy.FindAsync(id);

            CongKetNoi congKetNoi = _mapper.Map<CongKetNoi>(congKetNoiDTO);
            _context.Entry(congKetNoi).State = EntityState.Modified;
            if (congKetNoi == null)
            {
                return NotFound();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CongKetNoiExists(id))
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

        // POST: api/CongKetNois
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CongKetNoiDTO>> PostCongKetNoi(CongKetNoiDTO congKetNoiDTO)
        {
            var congKetNoi = new CongKetNoi
            {
                CongGiaoTiep = congKetNoiDTO.CongGiaoTiep,
                KetNoiKhongDay = congKetNoiDTO.KetNoiKhongDay,
                KheDocTheNho = congKetNoiDTO.KheDocTheNho,
                Webcam = congKetNoiDTO.Webcam,
                TinhNangKhac = congKetNoiDTO.TinhNangKhac,
                DenBanPhim = congKetNoiDTO.DenBanPhim,
            };
            _context.CongKetNoi.Add(congKetNoi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCongKetNoi", new { id = congKetNoi.CongKetNoiId }, congKetNoi);
        }

        // DELETE: api/CongKetNois/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCongKetNoi(int id)
        {
            var congKetNoi = await _context.CongKetNoi.FindAsync(id);
            if (congKetNoi == null)
            {
                return NotFound();
            }

            _context.CongKetNoi.Remove(congKetNoi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CongKetNoiExists(int id)
        {
            return _context.CongKetNoi.Any(e => e.CongKetNoiId == id);
        }
    }
}
