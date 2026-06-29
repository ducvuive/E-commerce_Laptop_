using FluentValidation;

namespace BackendAPI.Application.UseCases.Orders.CheckoutOrder;

public sealed class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(command => command.CustomerId).NotEmpty();
        RuleFor(command => command.Request).NotNull();
        When(command => command.Request is not null, () =>
        {
            RuleFor(command => command.Request.IdempotencyKey)
                .NotEmpty()
                .MaximumLength(80);
            RuleFor(command => command.Request.Receiver)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(command => command.Request.Address)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(command => command.Request.Phone)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(command => command.Request.Email)
                .EmailAddress()
                .When(command => !string.IsNullOrWhiteSpace(command.Request.Email));
            RuleFor(command => command.Request.PaymentMethod)
                .MaximumLength(30);
            RuleFor(command => command.Request.Items)
                .NotEmpty();
            RuleForEach(command => command.Request.Items).ChildRules(item =>
            {
                item.RuleFor(x => x.ProductId).GreaterThan(0);
                item.RuleFor(x => x.Quantity).GreaterThan(0);
            });
        });
    }
}
