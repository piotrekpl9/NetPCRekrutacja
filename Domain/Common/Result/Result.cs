namespace Domain.Common.Result;

public class Result
{
    protected Result(Error error)
    {
        IsSuccess = error.Equals(Error.None);
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new (Error.None);
    public static Result Failure(Error error) => new (error);
}