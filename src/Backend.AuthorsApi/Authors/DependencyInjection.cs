using Authors.Data;
using Authors.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Authors;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();

        var connectionString = config.GetConnectionString("PostgresConnection");

        services.AddDbContext<AuthorsDbContext>(options => { options.UseNpgsql(connectionString); });

        services.AddScoped<IAuthorsDbContext>(provider =>
            provider.GetService<AuthorsDbContext>());
        
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
        
        return services;
    }
}