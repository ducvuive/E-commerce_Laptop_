using AutoMapper;
using BackendAPI.Areas.Identity.Data;
using BackendAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareView.DTO;
using System.Data;

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
        [HttpGet]
        [Route("month")]
        /*[Authorize(Roles = "Admin")]*/
        public async Task<ActionResult<List<SanPhamDTO>>> GetSanPhamTopRaMat()
        {
            var results = _context.SanPham.OrderByDescending(x => x.RaMat).Take(6);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        // GET: api/SanPhams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SanPhamDTO>> GetSanPham(int id)
        {
            var sanPham = await _context.SanPham.Where(p => p.SanPhamId == id)
                                                .Include(p => p.Rating)
                                                .ThenInclude(r => r.KhachHang)
                                                /*.Where(p => p.SanPhamId == id)*/
                                                /*.Select(o => new
                                                {
                                                    sanpham = o,
                                                    Rating = o.Rating.OrderByDescending(i => i.RatingID)
                                                })*/
                                                .FirstOrDefaultAsync();

            if (sanPham == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<SanPhamDTO>(sanPham);

            /*            foreach(var item in sanPham.Rating)
                        {
                           _mapper.Map<UserIdentityDTO>(item);

                        }*/
            return mapper;
        }
        // GET: api/SanPhams/5
        [HttpGet("GetSanPhamTheoTrang/{page}")]
        public async Task<ActionResult> GetSanPhamTheoTrang(int page = 1)
        {
            //var sanPham = await _context.SanPham.FindAsync(id);
            var skip = 12 * (page - 1);
            //var sanPham = await _context.SanPham.FindAsync(id);
            var results = _context.SanPham.OrderByDescending(x => x.DonGia).Skip(skip).Take(12);
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        [HttpGet("GetSanPhamTheoDmTheoTrang/{dm}/{page}")]
        public async Task<ActionResult> GetSanPhamTheoDmTheoTrang(int dm, int page)
        {
            var skip = 12 * (page - 1);
            var results = _context.SanPham.Where(s => s.DMSPId == dm).Skip(skip).Take(12);
            ;
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        [HttpGet("GetSanPhamTheoDm/{dm}/")]
        public async Task<ActionResult> GetSanPhamTheoDm(int dm)
        {
            var results = _context.SanPham.Where(s => s.DMSPId == dm);
            ;
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        [HttpGet("GetSanPhamTheoTen/{ten}/")]
        public async Task<ActionResult> GetSanPhamTheoTen(string ten)
        {
            var results = _context.SanPham.Where(s => s.TenSP.Contains(ten));
            ;
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
        }

        [HttpGet("GetSanPhamTheoTenTheoTrang/{ten}/{page}")]
        public async Task<ActionResult> GetSanPhamTheoTenTheoTrang(string ten, int page)
        {
            var skip = 12 * (page - 1);
            var results = _context.SanPham.Where(s => s.TenSP.Contains(ten)).Skip(skip).Take(12);
            ;
            if (results == null)
            {
                return NotFound();
            }

            return Ok(results);
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
