using Microsoft.AspNetCore.Http;
using WebTech.Application.DTOs;

namespace WebTech.Application.Interfaces.Providers;

public interface ICookiesProvider
{
    void AddRefreshTokenCookiesToResponse(HttpResponse response,string refreshToken);

    
    void AddFingerprintCookiesToResponse(HttpResponse response, string fingerprint);
    
    string? GetRefreshTokenFromCookies(HttpRequest request);

    
    string? GetFingerprintFromCookies(HttpRequest request);
    
    void DeleteCookiesFromResponse(HttpResponse response);
}