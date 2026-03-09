using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;

namespace Plume.Domain.Entities.Users;

/// <summary>
/// Extended profile information for users.
/// Separated from core User entity for performance and flexibility.
/// </summary>
public class UserProfile : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    /// <summary>
    /// User's biography / about section. Supports markdown.
    /// </summary>
    [MaxLength(2000)]
    public string? Bio { get; set; }

    /// <summary>
    /// Short tagline shown on profile cards.
    /// </summary>
    [MaxLength(160)]
    public string? Tagline { get; set; }

    /// <summary>
    /// URL to user's avatar image.
    /// </summary>
    [MaxLength(500)]
    public string? AvatarUrl { get; set; }

    /// <summary>
    /// URL to user's profile cover/banner image.
    /// </summary>
    [MaxLength(500)]
    public string? CoverImageUrl { get; set; }

    /// <summary>
    /// User's personal website URL.
    /// </summary>
    [MaxLength(255)]
    public string? WebsiteUrl { get; set; }

    /// <summary>
    /// User's location (free text).
    /// </summary>
    [MaxLength(100)]
    public string? Location { get; set; }

    /// <summary>
    /// Twitter/X handle (without @).
    /// </summary>
    [MaxLength(50)]
    public string? TwitterHandle { get; set; }

    /// <summary>
    /// LinkedIn profile URL or username.
    /// </summary>
    [MaxLength(100)]
    public string? LinkedInProfile { get; set; }

    /// <summary>
    /// GitHub username.
    /// </summary>
    [MaxLength(50)]
    public string? GitHubUsername { get; set; }

    // Denormalized counts for efficient profile display
    public int FollowerCount { get; set; }
    public int FollowingCount { get; set; }
    public int ArticleCount { get; set; }
    public long TotalArticleViews { get; set; }
    public long TotalReactionsReceived { get; set; }
}
