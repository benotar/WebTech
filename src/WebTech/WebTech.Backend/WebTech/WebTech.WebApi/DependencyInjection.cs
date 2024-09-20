using Microsoft.Extensions.Options;
using WebTech.Application.Configurations;

namespace WebTech.WebApi;

public static class DependencyInjection
{
    public static void AddCustomConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<DatabaseConfiguration>(
            builder.Configuration.GetSection(DatabaseConfiguration.ConfigurationKey));
        builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<DatabaseConfiguration>>().Value);
        
        builder.Services.Configure<JwtConfiguration>(
            builder.Configuration.GetSection(JwtConfiguration.ConfigurationKey));
        builder.Services.AddSingleton(resolver =>
            resolver.GetRequiredService<IOptions<JwtConfiguration>>().Value);
    }
}