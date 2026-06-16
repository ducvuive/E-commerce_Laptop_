namespace BackendAPI.Services.Orders;

public interface IOrderEventPublisher
{
    Task PublishOrderPlacedAsync(OrderPlacedEvent orderPlaced, CancellationToken cancellationToken);
}
