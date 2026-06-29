using BackendAPI.Application.UseCases.Orders.Events;
using BackendAPI.Domain.Entities;

namespace BackendAPI.Application.Abstractions;

public interface IOrderRepository
{
    Task<CustomerSnapshot?> GetCustomerAsync(string customerId, CancellationToken cancellationToken);

    Task<IReadOnlyDictionary<int, Product>> GetProductsByIdsAsync(
        IReadOnlyCollection<int> productIds,
        CancellationToken cancellationToken);

    Task AddInvoiceAsync(Invoice invoice, CancellationToken cancellationToken);

    Task AddOrderPlacedEventAsync(OrderPlacedEvent orderPlaced, CancellationToken cancellationToken);

    Task<Invoice?> GetOrderWithDetailsAsync(int invoiceId, CancellationToken cancellationToken);
}

public sealed record CustomerSnapshot(string Id, string? Email, string? UserName);
