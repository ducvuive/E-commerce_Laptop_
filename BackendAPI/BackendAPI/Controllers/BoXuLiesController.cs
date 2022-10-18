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
        public async Task<IActionResult> PutBoXuLy(int id, BoXuLy boXuLy)
        {
            if (id != boXuLy.BoXuLyId)
            {
                return BadRequest();
            }

            _context.Entry(boXuLy).State = EntityState.Modified;

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
        public async Task<ActionResult<BoXuLy>> PostBoXuLy(BoXuLy boXuLy)
        {
            _context.BoXuLy.Add(boXuLy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoXuLy", new { id = boXuLy.BoXuLyId }, boXuLy);
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
