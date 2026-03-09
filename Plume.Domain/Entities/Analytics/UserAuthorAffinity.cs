using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Users;

namespace Plume.Domain.Entities.Analytics;

/// <summary>
/// Tracks user affinity for specific authors.
/// Captures reading patterns beyond explicit follows.
/// Used for collaborative filtering and author recommendations.
/// </summary>
public class UserAuthorAffinity : BaseEntity
{
    /// <summary>
    /// The reader/consumer.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    /// <summary>
    /// The author being tracked.
    /// </summary>
    [Required]
    public Guid AuthorId { get; set; }

    [ForeignKey(nameof(AuthorId))]
    public User Author { get; set; } = null!;

    /// <summary>
    /// Overall affinity score (0.0 to 1.0).
    /// Computed from engagement signals.
    /// </summary>
    public decimal AffinityScore { get; set; }

    /// <summary>
    /// Whether user explicitly follows this author.
    /// </summary>
    public bool IsFollowing { get; set; }

    /// <summary>
    /// Number of articles read from this author.
    /// </summary>
    public int ArticlesRead { get; set; }

    /// <summary>
    /// Number of articles completed (>90% read).
    /// </summary>
    public int ArticlesCompleted { get; set; }

    /// <summary>
    /// Total reactions given to this author's articles.
    /// </summary>
    public int TotalReactionsGiven { get; set; }

    /// <summary>
    /// Total claps given (sum of clap counts).
    /// </summary>
    public int TotalClapsGiven { get; set; }

    /// <summary>
    /// Number of this author's articles bookmarked.
    /// </summary>
    public int ArticlesBookmarked { get; set; }

    /// <summary>
    /// Number of comments on this author's articles.
    /// </summary>
    public int CommentsWritten { get; set; }

    /// <summary>
    /// Total reading time on this author's content (seconds).
    /// </summary>
    public int TotalReadingTimeSeconds { get; set; }

    /// <summary>
    /// Average completion rate for this author's articles.
    /// </summary>
    public decimal AvgCompletionRate { get; set; }

    /// <summary>
    /// When user first interacted with this author's content.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }

    /// <summary>
    /// When user last interacted with this author's content.
    /// </summary>
    public DateTime LastInteractionAt { get; set; }

    /// <summary>
    /// Recency weight for score decay.
    /// </summary>
    public decimal RecencyWeight { get; set; } = 1.0m;
}
