using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Plume.Persistence.Identity;

/// <summary>
/// Design-time factory for EF Core migrations.
/// </summary>
public class PlumeIdentityDbContextFactory : IDesignTimeDbContextFactory<PlumeIdentityDbContext>
{
    public PlumeIdentityDbContext CreateDbContext(string[] args)
    {
        var connectionString = "Server=localhost;Port=3306;Database=plume;User=root;Password=root;";

        var optionsBuilder = new DbContextOptionsBuilder<PlumeIdentityDbContext>();
        optionsBuilder.UseMySQL(connectionString);

        return new PlumeIdentityDbContext(optionsBuilder.Options);
    }
}
