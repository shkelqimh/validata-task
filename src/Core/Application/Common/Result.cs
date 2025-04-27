namespace Application.Common;

public class Result<T> where T : class
{
    public bool HasError { get; }
    public Dictionary<string, string[]>? Errors { get; }
    public T? Data { get; }

    private Result(T data, bool hasError, Dictionary<string, string[]>? errors)
    {
        HasError = hasError;
        Errors = errors;
        Data = data;
    }

    public static Result<T> Success(T data) => new(data, false, null);
    public static Result<T> Failure(Dictionary<string, string[]> errors) => new(default, true, errors);
}