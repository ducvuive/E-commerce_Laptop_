using BackendAPI.Application.Common.Results;
using MediatR;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Products.GetProduct;

public sealed record GetProductQuery(int ProductId) : IRequest<Result<ProductDTO>>;
