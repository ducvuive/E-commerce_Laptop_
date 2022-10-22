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

        // PUT: api/BoXuLies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoXuLy(int id, BoXuLyDTO boXuLyDTO)
        {
            if (id != boXuLyDTO.BoXuLyId)
            {
                return BadRequest();
            }

            var boXuLyItem = await _context.BoXuLy.FindAsync(id);
            if (boXuLyItem == null)
            {
                return NotFound();
            }

            boXuLyItem.CongNgheCPU = boXuLyDTO.CongNgheCPU;
            boXuLyItem.SoNhan = boXuLyDTO.SoNhan;
            boXuLyItem.SoLuong = boXuLyDTO.SoLuong;
            boXuLyItem.ToCDoToiDa = boXuLyDTO.ToCDoToiDa;
            boXuLyItem.TocDoCPU = boXuLyDTO.TocDoCPU;
            boXuLyItem.BoNhoDem = boXuLyDTO.BoNhoDem;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoXuLyExists(id))
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

        // POST: api/BoXuLies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BoXuLyDTO>> PostBoXuLy(BoXuLyDTO BoXuLyDTO)
        {
            var boXuLy = new BoXuLy
            {
                CongNgheCPU = BoXuLyDTO.CongNgheCPU,
                SoNhan = BoXuLyDTO.SoNhan,
                SoLuong = BoXuLyDTO.SoLuong,
                ToCDoToiDa = BoXuLyDTO.ToCDoToiDa,
                TocDoCPU = BoXuLyDTO.TocDoCPU,
                BoNhoDem = BoXuLyDTO.BoNhoDem,
            };

            _context.BoXuLy.Add(boXuLy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoXuLy", new { id = boXuLy.BoXuLyId }, _mapper.Map<BoXuLyDTO>(boXuLy));
        }

        // DELETE: api/BoXuLies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoXuLy(int id)
        {
            var boXuLy = await _context.BoXuLy.FindAsync(id);
            if (boXuLy == null)
            {
                return NotFound();
            }

            _context.BoXuLy.Remove(boXuLy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoXuLyExists(int id)
        {
            return _context.BoXuLy.Any(e => e.BoXuLyId == id);
        }
    }
}
