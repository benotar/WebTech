using WebTech.Application.Common;

namespace WebTech.Application.Interfaces.Services;

public interface IRefreshTokenSessionService
{
    Task<Result<None>> CreateOrUpdateAsync(Guid userId, string fingerprint, string refreshToken);

    Task<Result<None>> DeleteAsync(Guid userId, string fingerprint);

    Task<Result<bool>> IsSessionKeyExistsAsync(Guid userId, string fingerprint);
}