using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Plume.Persistence.Identity;

/// <summary>
/// Identity-specific DbContext with prefixed tables to avoid conflicts.
/// </summary>
public class PlumeIdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public PlumeIdentityDbContext(DbContextOptions<PlumeIdentityDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Prefix all Identity tables with "Identity_"
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Identity_Users");
            entity.Property(u => u.GoogleId).HasMaxLength(255);
            entity.HasIndex(u => u.UserId).IsUnique();
            entity.HasIndex(u => u.GoogleId).IsUnique();
        });

        builder.Entity<IdentityRole<Guid>>(entity =>
        {
            entity.ToTable("Identity_Roles");
        });

        builder.Entity<IdentityUserRole<Guid>>(entity =>
        {
            entity.ToTable("Identity_UserRoles");
        });

        builder.Entity<IdentityUserClaim<Guid>>(entity =>
        {
            entity.ToTable("Identity_UserClaims");
        });

        builder.Entity<IdentityUserLogin<Guid>>(entity =>
        {
            entity.ToTable("Identity_UserLogins");
        });

        builder.Entity<IdentityRoleClaim<Guid>>(entity =>
        {
            entity.ToTable("Identity_RoleClaims");
        });

        builder.Entity<IdentityUserToken<Guid>>(entity =>
        {
            entity.ToTable("Identity_UserTokens");
        });
    }
}
