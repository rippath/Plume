using Microsoft.AspNetCore.Identity;

namespace Plume.Persistence.Identity;

/// <summary>
/// ASP.NET Core Identity user linked to the domain User entity via UserId.
/// Handles authentication credentials while domain User handles business logic.
/// Note: Navigation to User is intentionally omitted to keep Identity DbContext separate.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    /// <summary>
    /// Foreign key to the domain User entity.
    /// This links to the Users table managed by PlumeDbContext.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Google OAuth subject identifier.
    /// </summary>
    public string? GoogleId { get; set; }
}
