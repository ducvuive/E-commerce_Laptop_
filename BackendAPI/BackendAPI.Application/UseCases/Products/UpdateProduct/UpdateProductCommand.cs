using BackendAPI.Application.Common.Results;
using MediatR;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Products.UpdateProduct;

public sealed record UpdateProductCommand(int ProductId, ProductAdminDTO Product) : IRequest<Result<ProductAdminDTO>>;
