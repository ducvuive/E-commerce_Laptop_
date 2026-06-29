using FluentValidation;

namespace BackendAPI.Application.UseCases.Products.DisableProduct;

public sealed class DisableProductCommandValidator : AbstractValidator<DisableProductCommand>
{
    public DisableProductCommandValidator()
    {
        RuleFor(command => command.ProductId).GreaterThan(0);
    }
}
