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
    public class DanhMucSanPhamsController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public DanhMucSanPhamsController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/DanhMucSanPhams
        //[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<DanhMucSanPhamDTO>>> GetDanhMucSanPham()
        {
            var dmsp = await _context.DanhMucSanPham.ToListAsync();
            return _mapper.Map<List<DanhMucSanPhamDTO>>(dmsp);
        }

        // GET: api/DanhMucSanPhams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DanhMucSanPhamDTO>> GetDanhMucSanPham(int id)
        {
            var dmsp = await _context.DanhMucSanPham.FindAsync(id);

            if (dmsp == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<DanhMucSanPhamDTO>(dmsp);
            return mapper;
        }

        // PUT: api/DanhMucSanPhams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDanhMucSanPham(int id, DanhMucSanPhamDTO danhMucSanPhamDTO)
        {
            var dmsp = await _context.DanhMucSanPham.FindAsync(id);
            dmsp.TenDM = danhMucSanPhamDTO.TenDM;
            dmsp.Description = danhMucSanPhamDTO.Description;
            dmsp.DMSPId = danhMucSanPhamDTO.DMSPId;

            if (id != danhMucSanPhamDTO.DMSPId)
            {
                return BadRequest();
            }

            //var boNhoRam = await _context.BoXuLy.FindAsync(id);

            //DanhMucSanPham dmsp = _mapper.Map<DanhMucSanPham>(danhMucSanPhamDTO);
            _context.Entry(dmsp).State = EntityState.Modified;
            if (dmsp == null)
            {
                return NotFound();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhMucSanPhamExists(id))
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

        // POST: api/DanhMucSanPhams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DanhMucSanPham>> PostDanhMucSanPham(DanhMucSanPham danhMucSanPham)
        {
            /*var danhMucSanPham = new DanhMucSanPham
            {
                TenDM = danhMucSanPhamDTO.TenDM,

            };*/

            _context.DanhMucSanPham.Add(danhMucSanPham);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDanhMucSanPham", new { id = danhMucSanPham.DMSPId }, danhMucSanPham);
        }

        // DELETE: api/DanhMucSanPhams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDanhMucSanPham(int id)
        {
            var danhMucSanPham = await _context.DanhMucSanPham.FindAsync(id);
            if (danhMucSanPham == null)
            {
                return NotFound();
            }

            _context.DanhMucSanPham.Remove(danhMucSanPham);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DanhMucSanPhamExists(int id)
        {
            return _context.DanhMucSanPham.Any(e => e.DMSPId == id);
        }
    }
}
