using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Plume.Persistence.Identity;

namespace Plume.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<PlumeDbContext>(options =>
            options.UseMySQL(connectionString, mySqlOptions =>
            {
                mySqlOptions.MigrationsAssembly(typeof(PlumeDbContext).Assembly.FullName);
            }));

        services.AddDbContext<PlumeIdentityDbContext>(options =>
            options.UseMySQL(connectionString));

        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> configureOptions)
    {
        services.AddDbContext<PlumeDbContext>(configureOptions);
        return services;
    }
}
