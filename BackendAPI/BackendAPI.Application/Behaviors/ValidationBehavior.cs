using BackendAPI.Application.Common.Errors;
using BackendAPI.Application.Common.Results;
using FluentValidation;
using MediatR;

namespace BackendAPI.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        this.validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var failures = validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .Select(failure => Error.Validation(failure.PropertyName, failure.ErrorMessage))
            .ToArray();

        if (failures.Length == 0)
        {
            return await next();
        }

        if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)(object)Result.ValidationFailure(failures);
        }

        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            var valueType = typeof(TResponse).GetGenericArguments()[0];
            var method = typeof(Result<>)
                .MakeGenericType(valueType)
                .GetMethod(nameof(Result<object>.ValidationFailure), new[] { typeof(IReadOnlyCollection<Error>) });

            if (method is not null)
            {
                return (TResponse)method.Invoke(null, new object[] { failures })!;
            }
        }

        throw new ValidationException(failures.Select(error => new FluentValidation.Results.ValidationFailure(
            error.Code,
            error.Message)));
    }
}
