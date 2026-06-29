using FluentValidation;

namespace BackendAPI.Application.UseCases.Products.GetProductAdmin;

public sealed class GetProductAdminQueryValidator : AbstractValidator<GetProductAdminQuery>
{
    public GetProductAdminQueryValidator()
    {
        RuleFor(query => query.ProductId).GreaterThan(0);
    }
}
