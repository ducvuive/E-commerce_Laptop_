using BackendAPI.Application.Common.Results;
using MediatR;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Orders.CheckoutOrder;

public sealed record CheckoutOrderCommand(
    CheckoutOrderRequestDTO Request,
    string? CustomerId) : IRequest<Result<CheckoutOrderResponseDTO>>;
