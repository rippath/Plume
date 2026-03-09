using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Articles;
using Plume.Domain.Entities.Users;

namespace Plume.Domain.Entities.Analytics;

/// <summary>
/// Tracks detailed reading behavior within an article.
/// Captures time spent, scroll depth, and completion.
/// Critical signal for engagement quality and recommendations.
/// </summary>
public class ReadingSession : BaseEntity
{
    /// <summary>
    /// Article being read.
    /// </summary>
    [Required]
    public Guid ArticleId { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; } = null!;

    /// <summary>
    /// User reading the article.
    /// Null for anonymous sessions.
    /// </summary>
    public Guid? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    /// <summary>
    /// Corresponding article view event.
    /// </summary>
    public Guid? ArticleViewId { get; set; }

    [ForeignKey(nameof(ArticleViewId))]
    public ArticleView? ArticleView { get; set; }

    /// <summary>
    /// Session start time.
    /// </summary>
    public DateTime StartedAt { get; set; }

    /// <summary>
    /// Session end time.
    /// Null if session is still active or was abandoned.
    /// </summary>
    public DateTime? EndedAt { get; set; }

    /// <summary>
    /// Total time spent reading in seconds.
    /// Excludes idle time (tab in background).
    /// </summary>
    public int ActiveReadingTimeSeconds { get; set; }

    /// <summary>
    /// Total time on page in seconds (including idle).
    /// </summary>
    public int TotalTimeOnPageSeconds { get; set; }

    /// <summary>
    /// Maximum scroll depth reached (0.0 to 1.0).
    /// 1.0 means user scrolled to the bottom.
    /// </summary>
    public decimal MaxScrollDepth { get; set; }

    /// <summary>
    /// Estimated reading progress (0.0 to 1.0).
    /// Based on scroll depth adjusted for reading pace.
    /// </summary>
    public decimal ReadingProgress { get; set; }

    /// <summary>
    /// Whether user read to completion (progress > 90%).
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Whether user bounced quickly (< 10 seconds).
    /// Negative signal for recommendations.
    /// </summary>
    public bool IsBounce { get; set; }

    /// <summary>
    /// Number of times user paused scrolling (engagement indicator).
    /// </summary>
    public int ScrollPauseCount { get; set; }

    /// <summary>
    /// Whether user highlighted/selected text.
    /// Strong engagement signal.
    /// </summary>
    public bool HasTextSelection { get; set; }

    /// <summary>
    /// Whether user copied text from the article.
    /// </summary>
    public bool HasCopiedText { get; set; }

    /// <summary>
    /// Browser/session identifier for deduplication.
    /// </summary>
    [MaxLength(100)]
    public string? SessionId { get; set; }
}
