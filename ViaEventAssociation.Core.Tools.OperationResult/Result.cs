namespace ViaEventAssociation.Core.Tools.OperationResult;

public class Result<T>
{
    public List<Error> errors { get; } = new List<Error>();
    public bool isFailure => errors.Count > 0;
    public bool isSuccess => !isFailure;
    public T payload { get; set; }

    protected Result(T? value, List<Error> errors)
    {
        this.errors = errors;
        payload = value;
    }

    public override string ToString()
    {
        var text = "";
        foreach (var error in errors)
            text += $"- {error}\n";

        return text;
    }

    public static Result<T> Success(T value) => new Result<T>(value, new List<Error>());

    public static Result<T> Failure(params Error[] errors)
    {
        if (errors.Length == 0)
        {
            errors = new[] { Error.Default() };
        }

        return new Result<T>(default, errors.ToList());
    }

    public static Result<None> Success() => Result<None>.Success(None.Value);

    public static Result<T> FromResult<T2>(Result<T2> result)
    {
        return result.isFailure ? Result<T>.Failure(result.errors.ToArray()) : Result<T>.Success(default!);
    }

    public Result<T> WithResult<T2>(Result<T2> result)
    {
        if (result.isFailure)
            errors.AddRange(result.errors);

        return this;
    }

    public static implicit operator Result<T>(T value) => Success(value);
    public static implicit operator Result<T>(Error error) => Failure(error);
    public static implicit operator Result<T>(Error[] errors) => Failure(errors);

    public static Result<T> Combine<T>(params Result<T>[] results)
    {
        var failures = results.Where(r => r.isFailure).SelectMany(r => r.errors).ToList();

        if (failures.Any())
            return Result<T>.Failure(failures.ToArray());

        return results.First(r => !r.isFailure);
    }
}
