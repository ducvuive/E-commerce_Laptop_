using BackendAPI.Application.Abstractions;
using BackendAPI.Application.Common.Results;
using MediatR;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Products.SearchProducts;

public sealed class SearchProductsQueryHandler : IRequestHandler<SearchProductsQuery, Result<ProductPagingDTO>>
{
    private readonly IProductRepository productRepository;

    public SearchProductsQueryHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Result<ProductPagingDTO>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        var criteria = new ProductSearchCriteria(
            request.Page,
            request.Limit,
            request.CategoryId,
            request.Name,
            request.IncludeDisabled,
            request.Sort);

        var total = await productRepository.CountAsync(criteria, cancellationToken);
        var products = await productRepository.SearchAsync(criteria, cancellationToken);

        return Result<ProductPagingDTO>.Success(new ProductPagingDTO
        {
            Products = products.Select(product => product.ToProductDto()).ToList(),
            TotalItem = total,
            Page = request.Page,
            LastPage = total == 0 ? 0 : (int)Math.Ceiling(decimal.Divide(total, request.Limit))
        });
    }
}
