using Microsoft.EntityFrameworkCore;
using Users.Data;
using Users.Interfaces;

namespace Users;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();

        var connectionString = config.GetConnectionString("PostgresConnection");

        services.AddDbContext<UsersDbContext>(options => { options.UseNpgsql(connectionString); });

        services.AddScoped<IUsersDbContext>(provider =>
            provider.GetService<UsersDbContext>());
        
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });
        
        return services;
    }
}