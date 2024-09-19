using Microsoft.Extensions.DependencyInjection;
using WebTech.Application.Interfaces.Providers;
using WebTech.Application.Providers;

namespace WebTech.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        return services;
    }
}