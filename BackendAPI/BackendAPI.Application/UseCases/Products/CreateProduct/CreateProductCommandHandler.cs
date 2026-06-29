using BackendAPI.Application.Abstractions;
using BackendAPI.Application.Common.Results;
using BackendAPI.Domain.Entities;
using MediatR;
using ShareView.DTO;

namespace BackendAPI.Application.UseCases.Products.CreateProduct;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductAdminDTO>>
{
    private readonly IProductRepository productRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        this.productRepository = productRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<ProductAdminDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Rating = 0,
            PublishedDate = request.Product.PublishedDate ?? DateTime.UtcNow,
            UpdatedDate = request.Product.UpdatedDate ?? DateTime.UtcNow
        };
        product.ApplyAdminDto(request.Product);
        product.Rating = 0;
        product.IsDisable = false;

        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ProductAdminDTO>.Success(product.ToProductAdminDto());
    }
}
