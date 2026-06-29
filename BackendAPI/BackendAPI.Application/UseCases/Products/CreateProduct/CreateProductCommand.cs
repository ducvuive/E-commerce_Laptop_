using BackendAPI.Application.Common.Results;
using MediatR;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Products.CreateProduct;

public sealed record CreateProductCommand(ProductAdminDTO Product) : IRequest<Result<ProductAdminDTO>>;
