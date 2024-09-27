using Microsoft.AspNetCore.Mvc;
using WebTech.Application.Common;
using WebTech.Application.Interfaces.Providers;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Enums;

namespace WebTech.WebApi.Controllers;

public class TokenController : BaseController
{
    private readonly IJwtProvider _jwtProvider;

    private readonly ICookiesProvider _cookiesProvider;

    private readonly IRefreshTokenSessionService _refreshTokenSessionService;

    public TokenController(IJwtProvider jwtProvider, ICookiesProvider cookiesProvider, IRefreshTokenSessionService refreshTokenSessionService)
    {
        _jwtProvider = jwtProvider;
        
        _cookiesProvider = cookiesProvider;
        
        _refreshTokenSessionService = refreshTokenSessionService;
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    public async Task<Result<string>> Refresh()
    {
        var refreshToken = _cookiesProvider.GetRefreshTokenFromCookies(HttpContext.Request);
        
        if (refreshToken is null)
        {
            return Result<string>.Error(ErrorCode.RefreshCookieNotFound);
        }

        if (!_jwtProvider.IsTokenValid(refreshToken, JwtTokenType.Refresh))
        {
            return Result<string>.Error(ErrorCode.InvalidRefreshToken);
        }
        
        var fingerprint = _cookiesProvider.GetFingerprintFromCookies(HttpContext.Request);

        if (string.IsNullOrEmpty(fingerprint))
        {
            return Result<string>.Error(ErrorCode.FingerprintCookieNotFound);
        }

        var userId = _jwtProvider.GetUserIdFromRefreshToken(refreshToken);

        if (userId == Guid.Empty)
        {
            return Result<string>.Error(ErrorCode.UserIdNotValid);
        }
        
        var sessionExistsResult = await _refreshTokenSessionService
            .IsSessionKeyExistsAsync(userId, fingerprint);

        if (!sessionExistsResult.IsSucceed)
        {
            return Result<string>.Error(sessionExistsResult.ErrorCode);
        }

        var accessToken = _jwtProvider.GenerateToken(userId, JwtTokenType.Access);
        
        refreshToken = _jwtProvider.GenerateToken(userId, JwtTokenType.Refresh);

        await _refreshTokenSessionService.CreateOrUpdateAsync(userId, fingerprint, refreshToken);
        
        _cookiesProvider.AddRefreshTokenCookiesToResponse(HttpContext.Response, refreshToken);

        return Result<string>.Success(accessToken);
    }
}