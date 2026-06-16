using AutoMapper;
using BackendAPI.Persistence.Data;
using BackendAPI.Domain.Entities;
using BackendAPI.Services.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShareView.Constants;
using ShareView.DTO;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOrderCheckoutService _orderCheckoutService;

        public InvoiceController(UserDbContext context, IMapper mapper, IOrderCheckoutService orderCheckoutService)
        {
            _context = context;
            _mapper = mapper;
            _orderCheckoutService = orderCheckoutService;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<InvoiceDTO>> GetInvoice()
        {
            var invoice = _context.Invoice.OrderByDescending(s => s.InvoiceId).FirstOrDefault();
            return Ok(invoice);
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDTO>> GetInvoice(int id)
        {
            var invoice = await _context.Invoice.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }
            return _mapper.Map<InvoiceDTO>(invoice);
        }
        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{email}")]
        public async Task<ActionResult<InvoiceDTO>> PostInvoice([FromBody] InvoiceDTO invoiceDTO, string email)
        {
            var user = await _context.UserIdentity.FirstOrDefaultAsync(i => i.Email == email);
            Invoice invoice = _mapper.Map<Invoice>(invoiceDTO);
            invoice.CustomerId = user?.Id;
            invoice.Status = InvoiceStatus.Pending;
            _context.Invoice.Add(invoice);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetInvoice", new { id = invoice.InvoiceId }, invoice);
            return Ok("Invoice created successfully");
        }

        [Authorize]
        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutOrderResponseDTO>> Checkout(
            [FromBody] CheckoutOrderRequestDTO request,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _orderCheckoutService.CheckoutAsync(request, User, cancellationToken);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
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

        //private bool InvoiceExists(int id)
        //{
        //    return _context.Invoice.Any(e => e.InvoiceId == id);
        //}
    }
}
