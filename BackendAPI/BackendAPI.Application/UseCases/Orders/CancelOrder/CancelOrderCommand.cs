using BackendAPI.Application.Common.Results;
using MediatR;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Orders.CancelOrder;

public sealed record CancelOrderCommand(
    int InvoiceId,
    string? CustomerId,
    bool IsAdmin) : IRequest<Result<CheckoutOrderResponseDTO>>;
