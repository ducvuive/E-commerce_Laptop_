using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            /*            var session = Request.HttpContext.Session.GetString(Variable.JWT);
                        var email = "";
                        if (session == null)
                        {
                            return Redirect("/Account/Login");
                        }
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var userInfo = tokenHandler.ReadJwtToken(session);
                        email = (userInfo.Claims.FirstOrDefault(o => o.Type == "sub")?.Value);
                        *//*var user = await userClient.GetUser(email);*/
            var user = await _context.UserIdentity.FirstOrDefaultAsync(i => i.Email == userName);
            var sanPham = await _context.SanPham.FirstOrDefaultAsync(i => i.SanPhamId == ratingDTO.sanPhamId);
            Rating rating = _mapper.Map<Rating>(ratingDTO);
            rating.KhachHang = user;
            rating.sanPham = sanPham;

            _context.Rating.Add(rating);
            await _context.SaveChangesAsync();
            return await _context.Rating.ProjectTo<RatingDTO>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
            //return CreatedAtAction("GetRating", new { id = rating.RatingID }, rating);
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
