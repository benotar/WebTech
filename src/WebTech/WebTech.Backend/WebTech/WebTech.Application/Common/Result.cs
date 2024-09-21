using WebTech.Domain.Enums;

namespace WebTech.Application.Common;

public class Result<TData>
{
    public TData? Data { get; private set; }

    public ErrorCode? ErrorCode { get; private set; }

    public bool IsSucceed => ErrorCode is null;

    public static Result<TData> Success(TData data = default)
        => new() { Data = data };

    public static Result<TData> Error(ErrorCode? errorCode)
        => new() { ErrorCode = errorCode };
}

public record struct None;