using CSharpFunctionalExtensions;

namespace TgYandexBot.Core.Results;

public class Result<T>(bool haveValue, T value = default)
{
    public T Value { get; private set; } = value;
    public bool HaveValue { get; private set; } = haveValue;
}

public static class Result
{
    public static Result<T> AsResult<T>(this T? value)
    {
        return value == null ? Fail<T>() : Ok(value);
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(true, value);
    }

    public static Result<T> Fail<T>()
    {
        return new Result<T>(false, default);
    }
}