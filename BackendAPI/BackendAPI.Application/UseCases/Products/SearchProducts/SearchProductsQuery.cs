using BackendAPI.Application.Abstractions;
using BackendAPI.Application.Common.Results;
using MediatR;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Products.SearchProducts;

public sealed record SearchProductsQuery(
    int Page = 1,
    int Limit = 12,
    int? CategoryId = null,
    string? Name = null,
    bool IncludeDisabled = false,
    ProductSort Sort = ProductSort.PublishedDateDesc) : IRequest<Result<ProductPagingDTO>>;
