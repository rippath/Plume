using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Analytics;
using Plume.Domain.Entities.Social;
using Plume.Domain.Entities.Users;
using Plume.Domain.Enums;

namespace Plume.Domain.Entities.Articles;

/// <summary>
/// Core article entity representing blog posts.
/// Supports draft/publish lifecycle, rich content, and engagement tracking.
/// </summary>
public class Article : BaseEntity
{
    /// <summary>
    /// The author of the article.
    /// </summary>
    [Required]
    public Guid AuthorId { get; set; }

    [ForeignKey(nameof(AuthorId))]
    public User Author { get; set; } = null!;

    /// <summary>
    /// Article title.
    /// </summary>
    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// URL-friendly slug derived from title.
    /// </summary>
    [Required]
    [MaxLength(350)]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Short summary/excerpt for cards and SEO.
    /// </summary>
    [MaxLength(500)]
    public string? Summary { get; set; }

    /// <summary>
    /// The main content body. Format determined by ContentFormat.
    /// For EditorJs, this stores the JSON structure.
    /// </summary>
    [Required]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Format of the content field.
    /// </summary>
    public ContentFormat ContentFormat { get; set; } = ContentFormat.EditorJs;

    /// <summary>
    /// Featured/hero image URL.
    /// </summary>
    [MaxLength(500)]
    public string? FeaturedImageUrl { get; set; }

    /// <summary>
    /// Alt text for the featured image.
    /// </summary>
    [MaxLength(300)]
    public string? FeaturedImageAlt { get; set; }

    /// <summary>
    /// Publishing lifecycle status.
    /// </summary>
    public ArticleStatus Status { get; set; } = ArticleStatus.Draft;

    /// <summary>
    /// When the article was first published.
    /// Null if never published.
    /// </summary>
    public DateTime? PublishedAt { get; set; }

    /// <summary>
    /// Estimated reading time in minutes.
    /// Computed from content length.
    /// </summary>
    public int EstimatedReadingTimeMinutes { get; set; }

    /// <summary>
    /// Word count of the article.
    /// </summary>
    public int WordCount { get; set; }

    /// <summary>
    /// Whether this article is featured/highlighted by editors.
    /// </summary>
    public bool IsFeatured { get; set; }

    /// <summary>
    /// Whether this article allows comments.
    /// </summary>
    public bool AllowComments { get; set; } = true;

    /// <summary>
    /// Whether this article is part of a series.
    /// </summary>
    public Guid? SeriesId { get; set; }

    [ForeignKey(nameof(SeriesId))]
    public ArticleSeries? Series { get; set; }

    /// <summary>
    /// Order within the series (1-based).
    /// </summary>
    public int? SeriesOrder { get; set; }

    /// <summary>
    /// Canonical URL if republished from elsewhere.
    /// </summary>
    [MaxLength(500)]
    public string? CanonicalUrl { get; set; }

    /// <summary>
    /// Language code (e.g., "en", "dv" for Dhivehi).
    /// </summary>
    [MaxLength(10)]
    public string LanguageCode { get; set; } = "dv";

    /// <summary>
    /// Whether the article content is RTL.
    /// </summary>
    public bool IsRightToLeft { get; set; } = true;

    // Denormalized engagement counts for efficient queries
    public long ViewCount { get; set; }
    public int ReactionCount { get; set; }
    public int CommentCount { get; set; }
    public int BookmarkCount { get; set; }
    public int ShareCount { get; set; }

    /// <summary>
    /// Computed engagement score for ranking/recommendations.
    /// Updated periodically by background jobs.
    /// </summary>
    public double EngagementScore { get; set; }

    // Navigation: Tags associated with this article
    public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();

    // Navigation: Reactions on this article
    public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();

    // Navigation: Comments on this article
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    // Navigation: Bookmarks of this article
    public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();

    // Navigation: View records for this article
    public ICollection<ArticleView> Views { get; set; } = new List<ArticleView>();

    // Navigation: Reading sessions for this article
    public ICollection<ReadingSession> ReadingSessions { get; set; } = new List<ReadingSession>();

    // Navigation: Engagement statistics snapshot
    public ArticleEngagementStats? EngagementStats { get; set; }
}
