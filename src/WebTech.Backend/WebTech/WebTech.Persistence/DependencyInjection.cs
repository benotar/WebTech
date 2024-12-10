using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebTech.Application.Configurations;
using WebTech.Application.Interfaces.Persistence;

namespace WebTech.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfig = new DatabaseConfiguration();

        configuration.Bind(DatabaseConfiguration.ConfigurationKey, dbConfig);

        services.AddDbContext<WebTechDbContext>(options =>
        {
            var filledConnectionString = string.Format(dbConfig.ConnectionStringPattern,
                dbConfig.UserName, dbConfig.Password);

            options.UseNpgsql(filledConnectionString);
        });

        services.AddScoped<IWebTechDbContext>(provider =>
            provider.GetRequiredService<WebTechDbContext>());

        return services;
    }
}