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
    public class RatingController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public RatingController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/HoaDons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDTO>>> GetRating()
        {
            var hd = await _context.Rating.ToListAsync();
            return _mapper.Map<List<RatingDTO>>(hd);
        }

        // GET: api/HoaDons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RatingDTO>> GetRating(int Id)
        {
            var hoaDon = await _context.Rating.FindAsync(Id);

            if (hoaDon == null)
            {
                return NotFound();
            }

            return _mapper.Map<RatingDTO>(hoaDon);
        }

        [HttpPost("{userName}")]
        public async Task<ActionResult<RatingDTO>> PostRating([FromBody] RatingDTO ratingDTO, string userName)
        {
            float rateAvg = 0;
            var user = await _context.UserIdentity.FirstOrDefaultAsync(i => i.Email == userName);
            var sanPham = await _context.Product.FirstOrDefaultAsync(i => i.SanPhamId == ratingDTO.sanPhamId);
            var ratings = await _context.Rating.Where(s => s.sanPham.SanPhamId == ratingDTO.sanPhamId).ToListAsync();
            Rating rating = _mapper.Map<Rating>(ratingDTO);
            rating.KhachHang = user;
            rating.sanPham = sanPham;
            _context.Rating.Add(rating);
            foreach (var item in ratings)
            {
                rateAvg += (float)item.Rate;
            }
            rateAvg = (rateAvg + (float)ratingDTO.Rate) / (ratings.Count() + 1);
            int rateAvg_ = (int)Math.Ceiling(rateAvg);
            sanPham.DanhGia = rateAvg_;
            await _context.SaveChangesAsync();

            return Ok("Danh gia thanh cong");

        }

        // DELETE: api/HoaDons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoaDon(int id)
        {
            var hoaDon = await _context.Invoice.FindAsync(id);
            if (hoaDon == null)
            {
                return NotFound();
            }

            _context.Invoice.Remove(hoaDon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HoaDonExists(int id)
        {
            return _context.Invoice.Any(e => e.HoaDonId == id);
        }
    }
}
