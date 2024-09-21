using WebTech.Application.Common;
using WebTech.Application.Interfaces.Services;

namespace WebTech.Application.Services;

public class RefreshTokenSessionService : IRefreshTokenSessionService
{
    public Task<Result<None>> CreateOrUpdateAsync(Guid userId, string fingerprint, string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<None>> DeleteAsync(Guid userId, string fingerprint)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> IsSessionKeyExistsAsync(Guid userId, string fingerprint)
    {
        throw new NotImplementedException();
    }
}