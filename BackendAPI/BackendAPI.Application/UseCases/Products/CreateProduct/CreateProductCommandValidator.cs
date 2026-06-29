using FluentValidation;

namespace BackendAPI.Application.UseCases.Products.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
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
