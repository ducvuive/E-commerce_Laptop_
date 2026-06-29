using AutoMapper;
using BackendAPI.Application.Common.Errors;
using BackendAPI.Application.Common.Results;
using BackendAPI.Application.UseCases.Orders.CancelOrder;
using BackendAPI.Application.UseCases.Orders.CheckoutOrder;
using BackendAPI.Persistence.Data;
using BackendAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
        private readonly ISender sender;

        public InvoiceController(UserDbContext context, IMapper mapper, ISender sender)
        {
            _context = context;
            _mapper = mapper;
            this.sender = sender;
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
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await sender.Send(new CheckoutOrderCommand(request, customerId), cancellationToken);
            return ToActionResult(result);
        }

        [Authorize]
        [HttpPut("{id}/cancel")]
        public async Task<ActionResult<CheckoutOrderResponseDTO>> CancelInvoice(
            int id,
            CancellationToken cancellationToken)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await sender.Send(new CancelOrderCommand(
                id,
                customerId,
                User.IsInRole("Admin")), cancellationToken);

            return ToActionResult(result);
        }

        // DELETE: api/Invoices/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await sender.Send(new CancelOrderCommand(
                id,
                customerId,
                User.IsInRole("Admin")));

            return result.IsSuccess ? NoContent() : ToActionResult((Result)result);
        }

        //private bool InvoiceExists(int id)
        //{
        //    return _context.Invoice.Any(e => e.InvoiceId == id);
        //}

        private ActionResult<TValue> ToActionResult<TValue>(Result<TValue> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return ToActionResult((Result)result);
        }

        private ObjectResult ToActionResult(Result result)
        {
            var firstError = result.FirstError;
            var statusCode = firstError?.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status400BadRequest
            };

            return StatusCode(statusCode, result.Errors.Select(error => error.Message));
        }
    }
}
