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
    public class InvoiceDetailController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceDetailController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/CTHDs
        [HttpGet]
        public async Task<ActionResult<List<InvoiceDetailDTO>>> GetCTHD()
        {
            var cthd = await _context.InvoiceDetail.ToListAsync();
            return _mapper.Map<List<InvoiceDetailDTO>>(cthd);
        }

        // GET: api/CTHDs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDetailDTO>> GetCTHD(int id)
        {
            var cTHD = await _context.InvoiceDetail.FindAsync(id);

            if (cTHD == null)
            {
                return NotFound();
            }

            return _mapper.Map<InvoiceDetailDTO>(cTHD);
        }

        // PUT: api/CTHDs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCTHD(int id, InvoiceDetailDTO cTHD_DTO)
        {
            if (id != cTHD_DTO.ProductId)
            {
                return BadRequest();
            }

            InvoiceDetail congKetNoi = _mapper.Map<InvoiceDetail>(cTHD_DTO);
            _context.Entry(cTHD_DTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CTHDExists(id))
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

        // POST: api/CTHDs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InvoiceDetailDTO>> PostCTHD(InvoiceDetailDTO cTHD_DTO)
        {
            var product = await _context.Product.FindAsync(cTHD_DTO.ProductId);
            product.Quantity = product.Quantity - cTHD_DTO.Quantity;
            var cTHD = new InvoiceDetail
            {
                ProductId = cTHD_DTO.ProductId,
                InvoiceId = cTHD_DTO.InvoiceId,
                Quantity = cTHD_DTO.Quantity,
            };
            _context.InvoiceDetail.Add(cTHD);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CTHDExists(cTHD.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok("Toa chi tiet hoa don thanh cong");
            //return CreatedAtAction("GetCTHD", new { id = cTHD.SanPhamId }, cTHD);
        }

        // DELETE: api/CTHDs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCTHD(int id)
        {
            var cTHD = await _context.InvoiceDetail.FindAsync(id);
            if (cTHD == null)
            {
                return NotFound();
            }

            _context.InvoiceDetail.Remove(cTHD);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CTHDExists(int id)
        {
            return _context.InvoiceDetail.Any(e => e.ProductId == id);
        }
    }
}
