using BackendAPI.Application.Common.Errors;

namespace BackendAPI.Application.Common.Results;

public sealed class Result<T> : Result
{
    private Result(T? value, bool isSuccess, IReadOnlyCollection<Error> errors)
        : base(isSuccess, errors)
    {
        Value = value;
    }

    public T? Value { get; }

    public static Result<T> Success(T value) => new(value, true, Array.Empty<Error>());

    public new static Result<T> Failure(Error error) => new(default, false, new[] { error });

    public new static Result<T> Failure(IReadOnlyCollection<Error> errors) => new(default, false, errors);

    public new static Result<T> ValidationFailure(IReadOnlyCollection<Error> errors) => new(default, false, errors);
}
