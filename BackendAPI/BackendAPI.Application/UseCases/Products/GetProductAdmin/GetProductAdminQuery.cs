using BackendAPI.Application.Common.Results;
using MediatR;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Products.GetProductAdmin;

public sealed record GetProductAdminQuery(int ProductId) : IRequest<Result<ProductAdminDTO>>;
