using BackendAPI.Application.Abstractions;
using BackendAPI.Application.Common.Errors;
using BackendAPI.Application.Common.Results;
using BackendAPI.Application.UseCases.Orders.Events;
using BackendAPI.Domain.Entities;
using MediatR;
using ShareView.Constants;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Orders.CheckoutOrder;

public sealed class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, Result<CheckoutOrderResponseDTO>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IUnitOfWork unitOfWork;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        this.orderRepository = orderRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<CheckoutOrderResponseDTO>> Handle(
        CheckoutOrderCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await orderRepository.GetCustomerAsync(request.CustomerId!, cancellationToken);
        if (customer is null)
        {
            return Result<CheckoutOrderResponseDTO>.Failure(Error.Unauthorized(
                "Order.CustomerNotFound",
                "Authenticated customer does not exist."));
        }

        var items = request.Request.Items
            .GroupBy(item => item.ProductId)
            .Select(group => new CheckoutOrderItemDTO
            {
                ProductId = group.Key,
                Quantity = group.Sum(item => item.Quantity)
            })
            .OrderBy(item => item.ProductId)
            .ToList();

        CheckoutOrderResponseDTO? response = null;
        try
        {
            var idempotencyKey = request.Request.IdempotencyKey!.Trim();
            await unitOfWork.ExecuteInTransactionAsync(async token =>
            {
                var existingOrder = await orderRepository.GetByIdempotencyKeyAsync(
                    customer.Id,
                    idempotencyKey,
                    token);

                if (existingOrder is not null)
                {
                    response = ToCheckoutResponse(
                        existingOrder,
                        "Order already exists for this checkout request.");
                    return;
                }

                var productIds = items.Select(item => item.ProductId).ToList();
                var products = await orderRepository.GetProductsByIdsAsync(productIds, token);

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

                    var stockDecreaseResult = await orderRepository.TryDecreaseStockAsync(
                        item.ProductId,
                        item.Quantity,
                        token);

                    if (stockDecreaseResult != StockDecreaseResult.Success)
                    {
                        throw new InvalidOperationException(GetStockDecreaseFailureMessage(
                            product.NameProduct,
                            item.ProductId,
                            stockDecreaseResult));
                    }

                    total += product.Price.Value * item.Quantity;
                }

                var invoice = new Invoice
                {
                    Receiver = request.Request.Receiver?.Trim(),
                    Address = request.Request.Address?.Trim(),
                    Phone = request.Request.Phone?.Trim(),
                    DateReceived = DateTime.UtcNow,
                    Total = total,
                    Status = InvoiceStatus.Pending,
                    CustomerId = customer.Id,
                    IdempotencyKey = idempotencyKey,
                    InvoiceDetail = items.Select(item => new InvoiceDetail
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    }).ToList()
                };

                await orderRepository.AddInvoiceAsync(invoice, token);
                await unitOfWork.SaveChangesAsync(token);

                var orderPlaced = new OrderPlacedEvent
                {
                    InvoiceId = invoice.InvoiceId,
                    CustomerId = customer.Id,
                    CustomerEmail = customer.Email ?? customer.UserName ?? request.Request.Email ?? string.Empty,
                    Receiver = invoice.Receiver ?? string.Empty,
                    Address = invoice.Address ?? string.Empty,
                    Phone = invoice.Phone ?? string.Empty,
                    Total = total,
                    PaymentMethod = string.IsNullOrWhiteSpace(request.Request.PaymentMethod)
                        ? "COD"
                        : request.Request.PaymentMethod.Trim(),
                    CorrelationId = Guid.NewGuid().ToString("N")
                };

                await orderRepository.AddOrderPlacedEventAsync(orderPlaced, token);
                await unitOfWork.SaveChangesAsync(token);

                response = new CheckoutOrderResponseDTO
                {
                    InvoiceId = invoice.InvoiceId,
                    Status = invoice.Status.GetValueOrDefault(),
                    Total = total,
                    Message = "Order created successfully and is waiting for confirmation."
                };
            }, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Result<CheckoutOrderResponseDTO>.Failure(Error.Conflict(
                "Order.CheckoutFailed",
                ex.Message));
        }

        return Result<CheckoutOrderResponseDTO>.Success(response!);
    }

    private static CheckoutOrderResponseDTO ToCheckoutResponse(Invoice invoice, string message)
    {
        return new CheckoutOrderResponseDTO
        {
            InvoiceId = invoice.InvoiceId,
            Status = invoice.Status.GetValueOrDefault(),
            Total = invoice.Total.GetValueOrDefault(),
            Message = message
        };
    }

    private static string GetStockDecreaseFailureMessage(
        string? productName,
        int productId,
        StockDecreaseResult result)
    {
        var displayName = string.IsNullOrWhiteSpace(productName)
            ? $"Product {productId}"
            : productName;

        return result switch
        {
            StockDecreaseResult.ProductNotFound => $"Product {productId} does not exist.",
            StockDecreaseResult.ProductUnavailable => $"Product {displayName} is not available.",
            _ => $"Product {displayName} does not have enough stock."
        };
    }
}
