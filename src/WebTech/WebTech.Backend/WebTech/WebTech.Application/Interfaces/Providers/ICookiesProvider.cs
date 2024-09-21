using Microsoft.AspNetCore.Http;
using WebTech.Application.DTOs;

namespace WebTech.Application.Interfaces.Providers;

public interface ICookiesProvider
{
    void AddTokensCookiesToResponse(HttpResponse response, string accessToken, string refreshToken);
    
    void AddFingerprintCookiesToResponse(HttpResponse response, string fingerprint);
    
    TokensDto GetTokensFromCookies(HttpRequest request);
    
    string? GetFingerprintFromCookies(HttpRequest request);
    
    void DeleteCookiesFromResponse(HttpResponse response);
}