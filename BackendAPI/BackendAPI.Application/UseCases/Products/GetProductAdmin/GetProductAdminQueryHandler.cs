using BackendAPI.Application.Abstractions;
using BackendAPI.Application.Common.Errors;
using BackendAPI.Application.Common.Results;
using MediatR;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Products.GetProductAdmin;

public sealed class GetProductAdminQueryHandler : IRequestHandler<GetProductAdminQuery, Result<ProductAdminDTO>>
{
    private readonly IProductRepository productRepository;

    public GetProductAdminQueryHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Result<ProductAdminDTO>> Handle(GetProductAdminQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, includeRatings: false, cancellationToken);
        if (product is null)
        {
            return Result<ProductAdminDTO>.Failure(Error.NotFound(
                "Product.NotFound",
                $"Product {request.ProductId} was not found."));
        }

        return Result<ProductAdminDTO>.Success(product.ToProductAdminDto());
    }
}
