using Microsoft.AspNetCore.Mvc;
using WebTech.Application.Common;
using WebTech.Application.Interfaces.Providers;
using WebTech.Application.Interfaces.Services;
using WebTech.Domain.Enums;

namespace WebTech.WebApi.Controllers;

public class TokenController : BaseController
{
    private readonly IJwtProvider _jwtProvider;

    private readonly ICookiesProvider _cookieProvider;

    private readonly IRefreshTokenSessionService _refreshTokenSessionService;

    public TokenController(IJwtProvider jwtProvider, ICookiesProvider cookieProvider, IRefreshTokenSessionService refreshTokenSessionService)
    {
        _jwtProvider = jwtProvider;
        
        _cookieProvider = cookieProvider;
        
        _refreshTokenSessionService = refreshTokenSessionService;
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(Result<None>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<None>), StatusCodes.Status400BadRequest)]
    public async Task<Result<None>> Refresh()
    {
        var refreshToken = _cookieProvider.GetTokensFromCookies(HttpContext.Request).RefreshToken;

        if (refreshToken is null)
        {
            return Result<None>.Error(ErrorCode.RefreshCookieNotFound);
        }

        if (!_jwtProvider.IsTokenValid(refreshToken, JwtTokenType.Refresh))
        {
            return Result<None>.Error(ErrorCode.InvalidRefreshToken);
        }
        
        var fingerprint = _cookieProvider.GetFingerprintFromCookies(HttpContext.Request);

        if (string.IsNullOrEmpty(fingerprint))
        {
            return Result<None>.Error(ErrorCode.FingerprintCookieNotFound);
        }

        var userId = _jwtProvider.GetUserIdFromRefreshToken(refreshToken);

        if (userId == Guid.Empty)
        {
            return Result<None>.Error(ErrorCode.UserIdNotValid);
        }
        
        var sessionExistsResult = await _refreshTokenSessionService
            .IsSessionKeyExistsAsync(userId, fingerprint);

        if (!sessionExistsResult.IsSucceed)
        {
            return Result<None>.Error(sessionExistsResult.ErrorCode);
        }

        var accessToken = _jwtProvider.GenerateToken(userId, JwtTokenType.Access);
        
        refreshToken = _jwtProvider.GenerateToken(userId, JwtTokenType.Refresh);

        await _refreshTokenSessionService.CreateOrUpdateAsync(userId, fingerprint, refreshToken);

        _cookieProvider.AddTokensCookiesToResponse(HttpContext.Response, accessToken, refreshToken);

        return Result<None>.Success();
    }
}