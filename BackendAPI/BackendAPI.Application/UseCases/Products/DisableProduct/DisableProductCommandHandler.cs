using BackendAPI.Application.Abstractions;
using BackendAPI.Application.Common.Errors;
using BackendAPI.Application.Common.Results;
using MediatR;

namespace BackendAPI.Application.UseCases.Products.DisableProduct;

public sealed class DisableProductCommandHandler : IRequestHandler<DisableProductCommand, Result>
{
    private readonly IProductRepository productRepository;
    private readonly IUnitOfWork unitOfWork;

    public DisableProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DisableProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, includeRatings: false, cancellationToken);
        if (product is null)
        {
            return Result.Failure(Error.NotFound(
                "Product.NotFound",
                $"Product {request.ProductId} was not found."));
        }

        product.IsDisable = true;
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
