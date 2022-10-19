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
using System.Runtime.Intrinsics.X86;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoNhoRamsController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;   

        public BoNhoRamsController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/BoNhoRams
        [HttpGet]
        public async Task<ActionResult<List<BoNhoRamDTO>>> GetBoNhoRam()
        {
            var boNhoRam = await _context.BoNhoRam.ToListAsync();
            return _mapper.Map<List<BoNhoRamDTO>>(boNhoRam);
        }

        // GET: api/BoNhoRams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoNhoRamDTO>> GetBoNhoRam(int id)
        {
            var boNhoRam = await _context.BoNhoRam.FindAsync(id);

            if (boNhoRam == null)
            {
                return NotFound();
            }

            return _mapper.Map<BoNhoRamDTO>(boNhoRam);
        }

        // PUT: api/BoNhoRams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBoNhoRam(int id, BoNhoRamDTO boNhoRamDTO)
        {
            if (id != boNhoRamDTO.RamId)
            {
                return BadRequest();
            }

            //var boNhoRam = await _context.BoXuLy.FindAsync(id);

            BoNhoRam boNhoRam = _mapper.Map<BoNhoRam>(boNhoRamDTO);
            /*var a = result;*/
            //Console.WriteLine(result);
            /*       boNhoRam.BoXuLyId = id;
                   boNhoRam.CongNgheCPU = boXuLyDTO.CongNgheCPU;
                   boNhoRam.SoNhan = boXuLyDTO.SoNhan;
                   boNhoRam.SoLuong = boXuLyDTO.SoLuong;
                   boNhoRam.ToCDoToiDa = boXuLyDTO.ToCDoToiDa;
                   boNhoRam.TocDoCPU = boXuLyDTO.TocDoCPU;
                   boNhoRam.BoNhoDem = boXuLyDTO.BoNhoDem;*/
            _context.Entry(boNhoRam).State = EntityState.Modified;
            if (boNhoRam == null)
            {
                return NotFound();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoNhoRamExists(id))
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

        // POST: api/BoNhoRams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BoNhoRamDTO>> PostBoNhoRam(BoNhoRamDTO boNhoRamDTO)
        {
            var boNhoRam = new BoNhoRam
            {
                DungLuongRam = boNhoRamDTO.DungLuongRam,
                LoaiRam = boNhoRamDTO.LoaiRam,
                BusRam = boNhoRamDTO.BusRam,
                HoTroToiDa = boNhoRamDTO.HoTroToiDa,
            };
            _context.BoNhoRam.Add(boNhoRam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoNhoRam", new { id = boNhoRam.RamId }, boNhoRam);
        }

        // DELETE: api/BoNhoRams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoNhoRam(int id)
        {
            var boNhoRam = await _context.BoNhoRam.FindAsync(id);
            if (boNhoRam == null)
            {
                return NotFound();
            }

            _context.BoNhoRam.Remove(boNhoRam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoNhoRamExists(int id)
        {
            return _context.BoNhoRam.Any(e => e.RamId == id);
        }
    }
}
