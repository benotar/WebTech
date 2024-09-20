using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebTech.Application.Configurations;
using WebTech.Application.Interfaces.Providers;
using WebTech.Domain.Enums;

namespace WebTech.Application.Providers;

public class JwtProvider : IJwtProvider
{
    private readonly JwtConfiguration _jwtConfig;
    private IDateTimeProvider _dateTimeProvider;
    private readonly TokenValidationParameters _validationParameters;

    public JwtProvider(JwtConfiguration jwtConfig, IDateTimeProvider dateTimeProvider, TokenValidationParameters validationParameters)
    {
        _jwtConfig = jwtConfig;
        
        _dateTimeProvider = dateTimeProvider;
        
        _validationParameters = validationParameters;
    }

    public string GenerateToken(Guid userId, JwtTokenType jwtTokenType)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.SecretKey));

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(JwtRegisteredClaimNames.Typ, jwtTokenType.ToString())
        };

        var expires = jwtTokenType switch
        {
            JwtTokenType.Access => _dateTimeProvider.UtcNow.AddMinutes(_jwtConfig.AccessExpirationMinutes),
            JwtTokenType.Refresh => _dateTimeProvider.UtcNow.AddDays(_jwtConfig.RefreshExpirationDays),
            _ => throw new ArgumentOutOfRangeException(nameof(jwtTokenType), jwtTokenType,
                "Unknown JWT type enum value!")
        };

        var securityToken = new JwtSecurityToken(
            issuer: _jwtConfig.Issuer,
            audience: _jwtConfig.Audience,
            expires: expires,
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public Guid GetUserIdFromRefreshToken(string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = tokenHandler.ReadJwtToken(refreshToken).Claims;

        var userIdString = claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.NameIdentifier)).Value;

        return Guid.TryParse(userIdString, out var userId)
            ? userId
            : Guid.Empty;
    }

    public bool IsTokenValid(string refreshToken, JwtTokenType tokenType)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        ClaimsPrincipal? principal = null;

        try
        {
            principal = tokenHandler.ValidateToken(refreshToken, _validationParameters, out _);
        }
        catch (Exception e)
        {
            return false;
        }

        var tokenTypeClaim = principal.Claims.FirstOrDefault(claim => claim.Type.Equals(JwtRegisteredClaimNames.Typ));

        var tokenTypeValue = tokenTypeClaim?.Value;

        return tokenTypeValue is not null && tokenTypeValue.Equals(tokenType.ToString());
    }
}