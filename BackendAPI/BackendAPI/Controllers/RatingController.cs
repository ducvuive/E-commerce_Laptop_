using AutoMapper;
using BackendAPI.Persistence.Data;
using BackendAPI.Domain.Entities;
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

        // GET: api/Ratings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDTO>>> GetRating()
        {
            var ratings = await _context.Rating.ToListAsync();
            return _mapper.Map<List<RatingDTO>>(ratings);
        }

        // GET: api/Ratings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RatingDTO>> GetRating(int id)
        {
            var rating = await _context.Rating.FindAsync(id);

            if (rating == null)
            {
                return NotFound();
            }

            return _mapper.Map<RatingDTO>(rating);
        }

        [HttpPost("{userName}")]
        public async Task<ActionResult<RatingDTO>> PostRating([FromBody] RatingDTO ratingDTO, string userName)
        {
            float rateAvg = 0;
            var user = await _context.UserIdentity.FirstOrDefaultAsync(i => i.Email == userName);
            var product = await _context.Product.FirstOrDefaultAsync(i => i.ProductId == ratingDTO.ProductId);
            if (product == null)
            {
                return NotFound();
            }
            var ratings = await _context.Rating.Where(s => s.Product.ProductId == ratingDTO.ProductId).ToListAsync();
            Rating rating = _mapper.Map<Rating>(ratingDTO);
            rating.CustomerId = user?.Id;
            rating.Product = product;
            _context.Rating.Add(rating);
            foreach (var item in ratings)
            {
                rateAvg += (float)item.Rate;
            }
            rateAvg = (rateAvg + (float)ratingDTO.Rate) / (ratings.Count() + 1);
            int rateAvg_ = (int)Math.Ceiling(rateAvg);
            product.Rating = rateAvg_;
            await _context.SaveChangesAsync();

            return Ok("Rating submitted successfully");

        }

        // DELETE: api/Ratings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }

            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingExists(int id)
        {
            return _context.Rating.Any(e => e.RatingID == id);
        }
    }
}
