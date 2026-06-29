using BackendAPI.Application.UseCases.Orders.Events;

namespace BackendAPI.Services.Orders;

public interface IOrderEventPublisher
{
    Task PublishOrderPlacedAsync(OrderPlacedEvent orderPlaced, CancellationToken cancellationToken);
}
