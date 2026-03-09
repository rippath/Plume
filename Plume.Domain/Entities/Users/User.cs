using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Analytics;
using Plume.Domain.Entities.Articles;
using Plume.Domain.Entities.Social;

namespace Plume.Domain.Entities.Users;

/// <summary>
/// Core user entity representing both readers and authors.
/// Central to all platform interactions and the social graph.
/// </summary>
public class User : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? DisplayName { get; set; }

    public bool EmailVerified { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime? LastActiveAt { get; set; }

    /// <summary>
    /// External identity provider ID (for OAuth).
    /// </summary>
    [MaxLength(255)]
    public string? ExternalId { get; set; }

    [MaxLength(50)]
    public string? ExternalProvider { get; set; }

    /// <summary>
    /// User role for authorization (e.g., "User", "Admin", "Moderator").
    /// </summary>
    [MaxLength(50)]
    public string Role { get; set; } = "User";

    /// <summary>
    /// Indicates whether the user has local (email/password) credentials.
    /// </summary>
    public bool HasLocalCredentials { get; set; }

    // Navigation: User's extended profile
    public UserProfile? Profile { get; set; }

    // Navigation: Articles authored by this user
    public ICollection<Article> Articles { get; set; } = new List<Article>();

    // Navigation: Users this user is following
    public ICollection<UserFollow> Following { get; set; } = new List<UserFollow>();

    // Navigation: Users who follow this user
    public ICollection<UserFollow> Followers { get; set; } = new List<UserFollow>();

    // Navigation: Reactions given by this user
    public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();

    // Navigation: Comments written by this user
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    // Navigation: Articles bookmarked by this user
    public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();

    // Navigation: Article views by this user
    public ICollection<ArticleView> ArticleViews { get; set; } = new List<ArticleView>();

    // Navigation: Reading sessions by this user
    public ICollection<ReadingSession> ReadingSessions { get; set; } = new List<ReadingSession>();

    // Navigation: Search queries by this user
    public ICollection<SearchQuery> SearchQueries { get; set; } = new List<SearchQuery>();

    // Navigation: Topic interests for this user
    public ICollection<UserTopicInterest> TopicInterests { get; set; } = new List<UserTopicInterest>();

    // Navigation: Author affinities for this user
    public ICollection<UserAuthorAffinity> AuthorAffinities { get; set; } = new List<UserAuthorAffinity>();

    // Navigation: Notifications for this user
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
