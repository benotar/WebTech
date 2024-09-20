using WebTech.Domain.Enums;

namespace WebTech.Application.Interfaces.Providers;

public interface IJwtProvider
{
    string GenerateToken(Guid userId, JwtTokenType jwtTokenType);
    
    Guid GetUserIdFromRefreshToken(string refreshToken);
    
    bool IsTokenValid(string refreshToken, JwtTokenType tokenType);
}