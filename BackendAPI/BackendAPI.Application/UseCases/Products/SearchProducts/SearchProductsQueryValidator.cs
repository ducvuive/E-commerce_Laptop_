using FluentValidation;

namespace BackendAPI.Application.UseCases.Products.SearchProducts;

public sealed class SearchProductsQueryValidator : AbstractValidator<SearchProductsQuery>
{
    public SearchProductsQueryValidator()
    {
        RuleFor(query => query.Page).GreaterThan(0);
        RuleFor(query => query.Limit).InclusiveBetween(1, 100);
        RuleFor(query => query.CategoryId).GreaterThan(0).When(query => query.CategoryId.HasValue);
        RuleFor(query => query.Name).MaximumLength(200);
    }
}
