using Microsoft.AspNetCore.Http;
using WebTech.Application.Configurations;
using WebTech.Application.Interfaces.Providers;

namespace WebTech.Application.Providers;

public class CookiesProvider : ICookiesProvider
{
    private readonly IDateTimeProvider _dateTimeProvider;

    private readonly RefreshTokenSessionConfiguration _refreshTokenSessionConfiguration;

    private readonly JwtConfiguration _jwtConfiguration;

    private readonly CookiesConfiguration _cookiesConfiguration;

    public CookiesProvider(IDateTimeProvider dateTimeProvider,
        RefreshTokenSessionConfiguration refreshTokenSessionConfiguration, JwtConfiguration jwtConfiguration,
        CookiesConfiguration cookiesConfiguration)
    {
        _dateTimeProvider = dateTimeProvider;

        _refreshTokenSessionConfiguration = refreshTokenSessionConfiguration;

        _jwtConfiguration = jwtConfiguration;

        _cookiesConfiguration = cookiesConfiguration;
    }
    
    public void AddRefreshTokenCookiesToResponse(HttpResponse response, string refreshToken)
    {
        response.Cookies.Append(_cookiesConfiguration.RefreshTokenCookiesKey, refreshToken,
            CreateCookieOptionsWithDays(_jwtConfiguration.RefreshExpirationDays));
    }

    public void AddFingerprintCookiesToResponse(HttpResponse response, string fingerprint)
    {
        response.Cookies.Append(_cookiesConfiguration.FingerprintCookiesKey, fingerprint,
            CreateCookieOptionsWithDays(_refreshTokenSessionConfiguration.ExpirationDays));
    }

    public string? GetRefreshTokenFromCookies(HttpRequest request)
    {
        request.Cookies.TryGetValue(_cookiesConfiguration.RefreshTokenCookiesKey, out var refreshToken);

        return refreshToken;
    }

    public string? GetFingerprintFromCookies(HttpRequest request)
    {
        request.Cookies.TryGetValue(_cookiesConfiguration.FingerprintCookiesKey, out var fingerprint);
        
        return fingerprint;
    }

    public void DeleteCookiesFromResponse(HttpResponse response)
    {
        response.Cookies.Delete(_cookiesConfiguration.RefreshTokenCookiesKey);
        
        response.Cookies.Delete(_cookiesConfiguration.FingerprintCookiesKey);
    }

    private CookieOptions CreateCookieOptionsWithDays(int expirationDays)
        => new()
        {
            Secure = false,
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
            Expires = new DateTimeOffset(
                _dateTimeProvider.UtcNow.AddDays(expirationDays))
        };
}