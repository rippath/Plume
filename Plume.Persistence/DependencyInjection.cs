using Contract.Persistence.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Plume.Persistence.Identity;
using Plume.Persistence.Repositories;

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

        services.AddScoped<IArticleRepository, ArticleRepository>();

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
