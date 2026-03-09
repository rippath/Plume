using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Articles;

namespace Plume.Domain.Entities.Analytics;

/// <summary>
/// Aggregated engagement statistics for articles.
/// Denormalized data for efficient recommendation queries.
/// Updated periodically by background jobs.
/// </summary>
public class ArticleEngagementStats : BaseEntity
{
    [Required]
    public Guid ArticleId { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; } = null!;

    // View metrics
    public long TotalViews { get; set; }
    public long UniqueViewers { get; set; }
    public long ViewsLast24Hours { get; set; }
    public long ViewsLast7Days { get; set; }
    public long ViewsLast30Days { get; set; }

    // Reading metrics
    public decimal AvgReadingTimeSeconds { get; set; }
    public decimal AvgScrollDepth { get; set; }
    public decimal CompletionRate { get; set; }
    public decimal BounceRate { get; set; }

    // Engagement metrics
    public int TotalReactions { get; set; }
    public int TotalClaps { get; set; }
    public int UniqueReactors { get; set; }
    public int TotalComments { get; set; }
    public int UniqueCommenters { get; set; }
    public int TotalBookmarks { get; set; }
    public int TotalShares { get; set; }

    // Source attribution
    public int ViewsFromFeed { get; set; }
    public int ViewsFromSearch { get; set; }
    public int ViewsFromRecommendation { get; set; }
    public int ViewsFromExternal { get; set; }
    public int ViewsFromDirect { get; set; }

    // Quality signals
    /// <summary>
    /// Ratio of completed reads to total views.
    /// High value = engaging content.
    /// </summary>
    public decimal EngagementQualityScore { get; set; }

    /// <summary>
    /// Ratio of reactions to views.
    /// </summary>
    public decimal ReactionRate { get; set; }

    /// <summary>
    /// Ratio of bookmarks to views.
    /// High bookmark rate = valuable reference content.
    /// </summary>
    public decimal BookmarkRate { get; set; }

    /// <summary>
    /// Ratio of comments to views.
    /// </summary>
    public decimal CommentRate { get; set; }

    /// <summary>
    /// Velocity of engagement (engagements per hour since publish).
    /// </summary>
    public decimal EngagementVelocity { get; set; }

    /// <summary>
    /// Combined score for ranking.
    /// Weighted combination of all metrics.
    /// </summary>
    public decimal OverallScore { get; set; }

    /// <summary>
    /// When stats were last computed.
    /// </summary>
    public DateTime LastComputedAt { get; set; }
}
