using FluentValidation;

namespace BackendAPI.Application.UseCases.Products.GetProduct;

public sealed class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(query => query.ProductId).GreaterThan(0);
    }
}
