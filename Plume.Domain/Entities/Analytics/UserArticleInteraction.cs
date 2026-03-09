using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Articles;
using Plume.Domain.Entities.Users;

namespace Plume.Domain.Entities.Analytics;

/// <summary>
/// Aggregated interaction record between a user and an article.
/// Combines all engagement signals for efficient ML feature extraction.
/// Single row per user-article pair, updated on each interaction.
/// </summary>
public class UserArticleInteraction : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [Required]
    public Guid ArticleId { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; } = null!;

    // View data
    public int ViewCount { get; set; }
    public DateTime FirstViewedAt { get; set; }
    public DateTime LastViewedAt { get; set; }

    // Reading data
    public int TotalReadingTimeSeconds { get; set; }
    public decimal MaxScrollDepth { get; set; }
    public decimal MaxReadingProgress { get; set; }
    public bool HasCompleted { get; set; }

    // Engagement data
    public bool HasReacted { get; set; }
    public int ReactionCount { get; set; }
    public int ClapCount { get; set; }
    public bool HasBookmarked { get; set; }
    public bool HasCommented { get; set; }
    public int CommentCount { get; set; }
    public bool HasShared { get; set; }

    // Text interaction
    public bool HasSelectedText { get; set; }
    public bool HasCopiedText { get; set; }

    /// <summary>
    /// Computed engagement score for this user-article pair.
    /// Used for implicit feedback in recommendations.
    /// </summary>
    public decimal EngagementScore { get; set; }

    /// <summary>
    /// Whether this was a positive interaction overall.
    /// Derived from engagement signals.
    /// </summary>
    public bool IsPositiveInteraction { get; set; }
}
