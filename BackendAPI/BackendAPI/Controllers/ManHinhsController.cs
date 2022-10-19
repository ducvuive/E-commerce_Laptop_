using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendAPI.Areas.Identity.Data;
using BackendAPI.Models;
using AutoMapper;
using ShareView.DTO;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManHinhsController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public ManHinhsController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ManHinhs
        [HttpGet]
        public async Task<ActionResult<List<ManHinhDTO>>> GetManHinh()
        {
            var mh = await _context.ManHinh.ToListAsync();
            return _mapper.Map<List<ManHinhDTO>>(mh);
        }

        // GET: api/ManHinhs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManHinhDTO>> GetManHinh(int id)
        {
            var manHinh = await _context.ManHinh.FindAsync(id);

            if (manHinh == null)
            {
                return NotFound();
            }

            return _mapper.Map<ManHinhDTO>(manHinh);
        }

        // PUT: api/ManHinhs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManHinh(int id, ManHinhDTO manHinhDTO)
        {
            if (id != manHinhDTO.ManHinhId)
            {
                return BadRequest();
            }

            ManHinh manHinh = _mapper.Map<ManHinh>(manHinhDTO);
            _context.Entry(manHinh).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManHinhExists(id))
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

        // POST: api/ManHinhs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ManHinhDTO>> PostManHinh(ManHinhDTO manHinhDTO)
        {
            var manHinh = new ManHinh
            {
                KichThuoc = manHinhDTO.KichThuoc,
                DoPhanGiai = manHinhDTO.DoPhanGiai,
                TanSoQuet = manHinhDTO.TanSoQuet,
                CongNgheMH = manHinhDTO.CongNgheMH,
                CamUng = manHinhDTO.CamUng,
            };

            _context.ManHinh.Add(manHinh);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetManHinh", new { id = manHinh.ManHinhId }, manHinh);
        }

        // DELETE: api/ManHinhs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManHinh(int id)
        {
            var manHinh = await _context.ManHinh.FindAsync(id);
            if (manHinh == null)
            {
                return NotFound();
            }

            _context.ManHinh.Remove(manHinh);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ManHinhExists(int id)
        {
            return _context.ManHinh.Any(e => e.ManHinhId == id);
        }
    }
}
