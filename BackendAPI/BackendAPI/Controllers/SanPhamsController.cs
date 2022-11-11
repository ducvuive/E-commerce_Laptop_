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
        public async Task<ActionResult<List<SanPhamDTO>>> GetProduct()
        {
            var sp = await _context.SanPham.ToListAsync();
            return Ok(_mapper.Map<List<SanPhamDTO>>(sp));
        }

        [HttpGet]
        public async Task<ActionResult<List<SanPham>>> GetSanPhamAdmin()
        {
            var sp = await _context.SanPham.ToListAsync();
            return sp;
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
            var mapper = _mapper.Map<List<SanPhamDTO>>(results);
            return Ok(mapper);
        }

        // GET: api/SanPhams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SanPhamDTO>> GetSanPham(int id)
        {
            var sanPham = await _context.SanPham.Where(p => p.SanPhamId == id)
                                                .Include(p => p.Rating)
                                                .ThenInclude(r => r.KhachHang)
                                                .FirstOrDefaultAsync();
            if (sanPham == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<SanPhamDTO>(sanPham);
            return mapper;
        }
        // GET: api/SanPhams/5
        //[Route("api/SanPhams/admin_product")]
        [HttpGet("admin_product/{id}")]
        public async Task<ActionResult<SanPhamDTO_Admin>> GetSanPhamAdmin(int id)
        {
            /*var sanPham = await _context.SanPham
                                                .Where(p => p.SanPhamId == id)
                                                .Include(p => p.Rating)
                                                .ThenInclude(r => r.KhachHang)
                                                .FirstOrDefaultAsync();*/
            var sanPham = await _context.SanPham.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            /*var mapper = new SanPhamDTO_Admin()
            {
                SanPhamId = sanPham.SanPhamId,
                TenDM = sanPham.DMSPId.tenDM,

            }*/
            var mapper = _mapper.Map<SanPhamDTO_Admin>(sanPham);
            //var mapper = new 
            return Ok(sanPham);
        }
        // GET: api/SanPhams/5
        [HttpGet("GetSanPhamTheoTrang/{page}")]
        public async Task<ActionResult> GetSanPhamTheoTrang(int page = 1)
        {
            var skip = 12 * (page - 1);
            var results = _context.SanPham.OrderByDescending(x => x.DonGia).Skip(skip).Take(12);
            if (results == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<List<SanPhamDTO>>(results);
            return Ok(mapper);
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
            var mapper = _mapper.Map<List<SanPhamDTO>>(results);
            return Ok(mapper);
        }

        [HttpGet("GetSanPhamTheoDm/{dm}/")]
        public async Task<ActionResult<SanPhamDTO>> GetSanPhamTheoDm(int dm)
        {
            var results = _context.SanPham.Where(s => s.DMSPId == dm);
            ;
            if (results == null)
            {
                return NotFound();
            }
            var mapper = _mapper.Map<List<SanPhamDTO>>(results);
            return Ok(mapper);
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
            var mapper = _mapper.Map<List<SanPhamDTO>>(results);
            return Ok(mapper);
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
            var mapper = _mapper.Map<List<SanPhamDTO>>(results);
            return Ok(mapper);
        }

        // PUT: api/SanPhams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSanPham(int id, SanPhamDTO_Admin sanPhamDTO)
        {
            if (id != sanPhamDTO.SanPhamId)
            {
                return BadRequest();
            }
            var sanPham = new SanPham
            {
                SanPhamId = sanPhamDTO.SanPhamId,
                BoXuLyId = sanPhamDTO.BoXuLyId,
                CongKetNoiId = sanPhamDTO.CongKetNoiId,
                DMSPId = sanPhamDTO.DMSPId,
                RamId = sanPhamDTO.RamId,
                ManHinhId = sanPhamDTO.ManHinhId,
                TenSP = sanPhamDTO.TenSP,
                DonGia = sanPhamDTO.DonGia,
                NgayCapNhat = sanPhamDTO.NgayCapNhat,
                NgayTao = sanPhamDTO.NgayTao,
                SoLuong = sanPhamDTO.SoLuong,
                DanhGia = sanPhamDTO.DanhGia,
            };
            //SanPham sanPham = _mapper.Map<SanPham>(sanPhamDTO);
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
        public async Task<ActionResult<SanPham>> PostSanPham(SanPhamDTO_Admin sanPhamDTO)
        {
            var sanPham = new SanPham
            {
                BoXuLyId = sanPhamDTO.BoXuLyId,
                CongKetNoiId = sanPhamDTO.CongKetNoiId,
                DMSPId = sanPhamDTO.DMSPId,
                RamId = sanPhamDTO.RamId,
                TenSP = sanPhamDTO.TenSP,
                ManHinhId = sanPhamDTO.ManHinhId,
                DonGia = sanPhamDTO.DonGia,
                NgayCapNhat = sanPhamDTO.NgayCapNhat,
                NgayTao = sanPhamDTO.NgayTao,
                SoLuong = sanPhamDTO.SoLuong,
                DanhGia = 0,
            };
            //SanPham sanPham = _mapper.Map<SanPham>(sanPhamDTO);
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
