using FluentValidation;

namespace BackendAPI.Application.UseCases.Orders.CancelOrder;

public sealed class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(command => command.InvoiceId).GreaterThan(0);
        RuleFor(command => command.CustomerId)
            .NotEmpty()
            .When(command => !command.IsAdmin);
    }
}
