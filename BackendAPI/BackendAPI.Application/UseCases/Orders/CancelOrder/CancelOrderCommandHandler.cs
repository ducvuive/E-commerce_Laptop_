using BackendAPI.Application.Abstractions;
using BackendAPI.Application.Common.Errors;
using BackendAPI.Application.Common.Results;
using MediatR;
using ShareView.Constants;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Orders.CancelOrder;

public sealed class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Result<CheckoutOrderResponseDTO>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IUnitOfWork unitOfWork;

    public CancelOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        this.orderRepository = orderRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<CheckoutOrderResponseDTO>> Handle(
        CancelOrderCommand request,
        CancellationToken cancellationToken)
    {
        CheckoutOrderResponseDTO? response = null;

        try
        {
            await unitOfWork.ExecuteInTransactionAsync(async token =>
            {
                var invoice = await orderRepository.GetOrderWithDetailsAsync(request.InvoiceId, token);
                if (invoice is null)
                {
                    response = null;
                    return;
                }

                if (!request.IsAdmin && invoice.CustomerId != request.CustomerId)
                {
                    throw new UnauthorizedAccessException("Only the owner can cancel this order.");
                }

                if (invoice.Status == InvoiceStatus.Cancelled)
                {
                    throw new InvalidOperationException("Order is already cancelled.");
                }

                if (invoice.Status != InvoiceStatus.Pending && invoice.Status != InvoiceStatus.Confirmed)
                {
                    throw new InvalidOperationException("Order cannot be cancelled in the current status.");
                }

                foreach (var detail in invoice.InvoiceDetail)
                {
                    if (detail.Product is not null)
                    {
                        detail.Product.Quantity = detail.Product.Quantity +
                            detail.Quantity.GetValueOrDefault();
                    }
                }

                invoice.Status = InvoiceStatus.Cancelled;
                await unitOfWork.SaveChangesAsync(token);

                response = new CheckoutOrderResponseDTO
                {
                    InvoiceId = invoice.InvoiceId,
                    Status = InvoiceStatus.Cancelled,
                    Total = invoice.Total.GetValueOrDefault(),
                    Message = "Order cancelled successfully."
                };
            }, cancellationToken);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Result<CheckoutOrderResponseDTO>.Failure(Error.Forbidden(
                "Order.Forbidden",
                ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            return Result<CheckoutOrderResponseDTO>.Failure(Error.Conflict(
                "Order.CancelFailed",
                ex.Message));
        }

        if (response is null)
        {
            return Result<CheckoutOrderResponseDTO>.Failure(Error.NotFound(
                "Order.NotFound",
                $"Order {request.InvoiceId} was not found."));
        }

        return Result<CheckoutOrderResponseDTO>.Success(response);
    }
}
