using FluentValidation;

namespace BackendAPI.Application.UseCases.Products.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.ProductId).GreaterThan(0);
        RuleFor(command => command.Product).NotNull();
        When(command => command.Product is not null, () =>
        {
            RuleFor(command => command.Product.NameProduct)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(command => command.Product.Price)
                .NotNull()
                .GreaterThanOrEqualTo(0);
            RuleFor(command => command.Product.Quantity)
                .NotNull()
                .GreaterThanOrEqualTo(0);
            RuleFor(command => command.Product.CategoryId).GreaterThan(0);
            RuleFor(command => command.Product.ScreenId).GreaterThan(0);
            RuleFor(command => command.Product.ProcessorId).GreaterThan(0);
            RuleFor(command => command.Product.RamId).GreaterThan(0);
        });
    }
}
