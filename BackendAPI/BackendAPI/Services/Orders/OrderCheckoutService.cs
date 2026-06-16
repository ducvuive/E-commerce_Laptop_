using System.Security.Claims;
using System.Text.Json;
using BackendAPI.Domain.Entities;
using BackendAPI.Persistence.Data;
using BackendAPI.Persistence.Messaging;
using Microsoft.EntityFrameworkCore;
using ShareView.Constants;
using ShareView.DTO;

namespace BackendAPI.Services.Orders;

public sealed class OrderCheckoutService : IOrderCheckoutService
{
    private readonly UserDbContext context;

    public OrderCheckoutService(UserDbContext context)
    {
        this.context = context;
    }

    public async Task<CheckoutOrderResponseDTO> CheckoutAsync(
        CheckoutOrderRequestDTO request,
        ClaimsPrincipal user,
        CancellationToken cancellationToken)
    {
        var customerId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(customerId))
        {
            throw new UnauthorizedAccessException("Missing authenticated customer.");
        }

        var customer = await context.UserIdentity
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == customerId, cancellationToken);

        if (customer == null)
        {
            throw new UnauthorizedAccessException("Customer does not exist.");
        }

        var items = request.Items
            .Where(item => item.ProductId > 0 && item.Quantity > 0)
            .GroupBy(item => item.ProductId)
            .Select(group => new CheckoutOrderItemDTO
            {
                ProductId = group.Key,
                Quantity = group.Sum(item => item.Quantity)
            })
            .ToList();

        if (items.Count == 0)
        {
            throw new InvalidOperationException("Cart is empty.");
        }

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var productIds = items.Select(item => item.ProductId).ToList();
        var products = await context.Product
            .Where(product => productIds.Contains(product.ProductId))
            .ToDictionaryAsync(product => product.ProductId, cancellationToken);

        long total = 0;
        foreach (var item in items)
        {
            if (!products.TryGetValue(item.ProductId, out var product))
            {
                throw new InvalidOperationException($"Product {item.ProductId} does not exist.");
            }

            if (product.IsDisable || product.Price is null)
            {
                throw new InvalidOperationException($"Product {product.NameProduct} is not available.");
            }

            var availableQuantity = product.Quantity.GetValueOrDefault();
            if (availableQuantity < item.Quantity)
            {
                throw new InvalidOperationException($"Product {product.NameProduct} does not have enough stock.");
            }

            product.Quantity = availableQuantity - item.Quantity;
            total += product.Price.Value * item.Quantity;
        }

        var invoice = new Invoice
        {
            Receiver = request.Receiver?.Trim(),
            Address = request.Address?.Trim(),
            Phone = request.Phone?.Trim(),
            DateReceived = DateTime.Now,
            Total = total,
            Status = InvoiceStatus.Pending,
            CustomerId = customer.Id,
            InvoiceDetail = items.Select(item => new InvoiceDetail
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            }).ToList()
        };

        var orderPlaced = new OrderPlacedEvent
        {
            CustomerId = customer.Id,
            CustomerEmail = customer.Email ?? customer.UserName ?? request.Email ?? string.Empty,
            Receiver = invoice.Receiver ?? string.Empty,
            Address = invoice.Address ?? string.Empty,
            Phone = invoice.Phone ?? string.Empty,
            Total = total,
            PaymentMethod = string.IsNullOrWhiteSpace(request.PaymentMethod) ? "COD" : request.PaymentMethod.Trim(),
            CorrelationId = Guid.NewGuid().ToString("N")
        };

        context.Invoice.Add(invoice);
        await context.SaveChangesAsync(cancellationToken);

        orderPlaced.InvoiceId = invoice.InvoiceId;
        context.OutboxMessages.Add(new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = nameof(OrderPlacedEvent),
            Content = JsonSerializer.Serialize(orderPlaced),
            CorrelationId = orderPlaced.CorrelationId,
            OccurredOnUtc = DateTime.UtcNow
        });

        await context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return new CheckoutOrderResponseDTO
        {
            InvoiceId = invoice.InvoiceId,
            Status = invoice.Status.GetValueOrDefault(),
            Total = total,
            Message = "Order created successfully and is waiting for confirmation."
        };
    }
}
