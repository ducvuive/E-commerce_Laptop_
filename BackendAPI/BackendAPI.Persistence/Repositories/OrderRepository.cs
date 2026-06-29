using System.Text.Json;
using BackendAPI.Application.Abstractions;
using BackendAPI.Application.UseCases.Orders.Events;
using BackendAPI.Domain.Entities;
using BackendAPI.Persistence.Data;
using BackendAPI.Persistence.Messaging;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Persistence.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly UserDbContext context;

    public OrderRepository(UserDbContext context)
    {
        this.context = context;
    }

    public async Task<CustomerSnapshot?> GetCustomerAsync(string customerId, CancellationToken cancellationToken)
    {
        return await context.UserIdentity
            .AsNoTracking()
            .Where(customer => customer.Id == customerId)
            .Select(customer => new CustomerSnapshot(customer.Id, customer.Email, customer.UserName))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyDictionary<int, Product>> GetProductsByIdsAsync(
        IReadOnlyCollection<int> productIds,
        CancellationToken cancellationToken)
    {
        return await context.Product
            .Where(product => productIds.Contains(product.ProductId))
            .ToDictionaryAsync(product => product.ProductId, cancellationToken);
    }

    public Task AddInvoiceAsync(Invoice invoice, CancellationToken cancellationToken)
    {
        return context.Invoice.AddAsync(invoice, cancellationToken).AsTask();
    }

    public Task AddOrderPlacedEventAsync(OrderPlacedEvent orderPlaced, CancellationToken cancellationToken)
    {
        return context.OutboxMessages.AddAsync(new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = nameof(OrderPlacedEvent),
            Content = JsonSerializer.Serialize(orderPlaced),
            CorrelationId = orderPlaced.CorrelationId,
            OccurredOnUtc = DateTime.UtcNow
        }, cancellationToken).AsTask();
    }

    public Task<Invoice?> GetOrderWithDetailsAsync(int invoiceId, CancellationToken cancellationToken)
    {
        return context.Invoice
            .Include(invoice => invoice.InvoiceDetail)
            .ThenInclude(detail => detail.Product)
            .SingleOrDefaultAsync(invoice => invoice.InvoiceId == invoiceId, cancellationToken);
    }
}
