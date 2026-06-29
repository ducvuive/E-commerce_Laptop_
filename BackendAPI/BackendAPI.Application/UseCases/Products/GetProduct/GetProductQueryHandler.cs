using BackendAPI.Application.Abstractions;
using BackendAPI.Application.Common.Errors;
using BackendAPI.Application.Common.Results;
using MediatR;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Products.GetProduct;

public sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<ProductDTO>>
{
    private readonly IProductRepository productRepository;

    public GetProductQueryHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Result<ProductDTO>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, includeRatings: true, cancellationToken);
        if (product is null)
        {
            return Result<ProductDTO>.Failure(Error.NotFound(
                "Product.NotFound",
                $"Product {request.ProductId} was not found."));
        }

        var productDto = product.ToProductDto();
        var customerIds = product.Ratings
            .Select(rating => rating.CustomerId)
            .Where(customerId => customerId is not null)
            .Distinct()
            .Cast<string>()
            .ToList();

        if (productDto.Ratings.Count > 0 && customerIds.Count > 0)
        {
            var customers = await productRepository.GetCustomersByIdsAsync(customerIds, cancellationToken);
            for (var index = 0; index < product.Ratings.Count && index < productDto.Ratings.Count; index++)
            {
                var customerId = product.Ratings[index].CustomerId;
                if (customerId is not null && customers.TryGetValue(customerId, out var customer))
                {
                    productDto.Ratings[index].Customer = customer;
                }
            }
        }

        return Result<ProductDTO>.Success(productDto);
    }
}
