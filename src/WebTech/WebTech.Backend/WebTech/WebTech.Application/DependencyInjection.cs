using Microsoft.Extensions.DependencyInjection;
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
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IEncryptionProvider, HmacSha256Provider>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
        
        return services;
    }
}