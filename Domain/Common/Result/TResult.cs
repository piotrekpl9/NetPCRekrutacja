namespace Domain.Common.Result;

public class Result<T> : Result
{
    private Result(T value, Error error) : base(error)
    {
        Value = value;
    }

    public T? Value { get; set; }

    public static Result<T> Success(T value) => new (value, Error.None);
    public new static Result<T> Failure(Error error) => new (default, error);
}