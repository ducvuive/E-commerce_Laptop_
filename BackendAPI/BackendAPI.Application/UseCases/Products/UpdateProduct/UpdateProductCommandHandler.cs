using BackendAPI.Application.Abstractions;
using BackendAPI.Application.Common.Errors;
using BackendAPI.Application.Common.Results;
using MediatR;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Products.UpdateProduct;

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductAdminDTO>>
{
    private readonly IProductRepository productRepository;
    private readonly IUnitOfWork unitOfWork;

    public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<ProductAdminDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, includeRatings: false, cancellationToken);
        if (product is null)
        {
            return Result<ProductAdminDTO>.Failure(Error.NotFound(
                "Product.NotFound",
                $"Product {request.ProductId} was not found."));
        }

        product.ApplyAdminDto(request.Product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ProductAdminDTO>.Success(product.ToProductAdminDto());
    }
}
