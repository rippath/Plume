using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Plume.Persistence;

/// <summary>
/// Design-time factory for creating PlumeDbContext.
/// Used by EF Core tools for migrations when the application isn't running.
/// </summary>
public class PlumeDbContextFactory : IDesignTimeDbContextFactory<PlumeDbContext>
{
    public PlumeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PlumeDbContext>();

        // Default connection string for migrations
        var connectionString = "Server=localhost;Port=3306;Database=plume;User=root;Password=root;";

        optionsBuilder.UseMySQL(
            connectionString,
            options => options.MigrationsAssembly(typeof(PlumeDbContext).Assembly.FullName));

        return new PlumeDbContext(optionsBuilder.Options);
    }
}
