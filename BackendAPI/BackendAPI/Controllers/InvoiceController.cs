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
    public class InvoiceController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceController(UserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/HoaDons
        [HttpGet]
        public async Task<ActionResult<InvoiceDTO>> GetHoaDon()
        {
            //List<Invoice> hd = new List<Invoice>();
            var invoice = _context.Invoice.OrderByDescending(s => s.HoaDonId).FirstOrDefault();
            return Ok(invoice);
        }

        // GET: api/HoaDons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDTO>> GetHoaDon(int id)
        {
            var invoice = await _context.Invoice.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }
            return _mapper.Map<InvoiceDTO>(invoice);
        }
        // POST: api/HoaDons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{email}")]
        public async Task<ActionResult<InvoiceDTO>> PostHoaDon([FromBody] InvoiceDTO hoaDonDTO, string email)
        {
            var user = await _context.UserIdentity.FirstOrDefaultAsync(i => i.Email == email);
            Invoice invoice = _mapper.Map<Invoice>(hoaDonDTO);
            invoice.KhachHang = user;
            invoice.TrangThai = 1;
            _context.Invoice.Add(invoice);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetHoaDon", new { id = hoaDon.HoaDonId }, hoaDon);
            return Ok("Toa hoa don thanh cong");
        }

        // DELETE: api/HoaDons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHoaDon(int id)
        {
            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoice.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HoaDonExists(int id)
        {
            return _context.Invoice.Any(e => e.HoaDonId == id);
        }
    }
}
