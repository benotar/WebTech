using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebTech.Application.Configurations;
using WebTech.Application.Interfaces.Providers;
using WebTech.Application.Interfaces.Services;
using WebTech.Application.Providers;
using WebTech.Application.Services;

namespace WebTech.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped(typeof(IQueryProvider<>), typeof(QueryProvider<>));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRefreshTokenSessionService, RefreshTokenSessionService>();
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IEncryptionProvider, HmacSha256Provider>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddSingleton<ICookiesProvider, CookiesProvider>();
        
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = new JwtConfiguration();
        
        configuration.Bind(JwtConfiguration.ConfigurationKey, jwtConfig);
        
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig.Issuer,
            ValidAudience = jwtConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey))
        };

        services.AddSingleton(tokenValidationParameters);
        
        return services;
    }

    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConfig = new RedisConfiguration();

        configuration.Bind(RedisConfiguration.ConfigurationKey, redisConfig);

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConfig.ConnectionString;
        });

        return services;
    }
}