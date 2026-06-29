using BackendAPI.Application.Common.Results;
using MediatR;

namespace BackendAPI.Application.UseCases.Products.DisableProduct;

public sealed record DisableProductCommand(int ProductId) : IRequest<Result>;
