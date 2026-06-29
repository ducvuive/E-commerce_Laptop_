using BackendAPI.Application.Common.Errors;

namespace BackendAPI.Application.Common.Results;

public class Result
{
    protected Result(bool isSuccess, IReadOnlyCollection<Error> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public IReadOnlyCollection<Error> Errors { get; }

    public Error? FirstError => Errors.FirstOrDefault();

    public static Result Success() => new(true, Array.Empty<Error>());

    public static Result Failure(Error error) => new(false, new[] { error });

    public static Result Failure(IReadOnlyCollection<Error> errors) => new(false, errors);

    public static Result ValidationFailure(IReadOnlyCollection<Error> errors) => new(false, errors);
}
