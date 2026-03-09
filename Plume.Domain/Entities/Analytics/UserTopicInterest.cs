using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Articles;
using Plume.Domain.Entities.Users;
using Plume.Domain.Enums;

namespace Plume.Domain.Entities.Analytics;

/// <summary>
/// Tracks user interest/affinity for specific tags/topics.
/// Combines explicit preferences with inferred behavior.
/// Core entity for content-based recommendations.
/// </summary>
public class UserTopicInterest : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [Required]
    public Guid TagId { get; set; }

    [ForeignKey(nameof(TagId))]
    public Tag Tag { get; set; } = null!;

    /// <summary>
    /// How this interest was determined.
    /// </summary>
    [Required]
    public InterestSource Source { get; set; }

    /// <summary>
    /// Interest score (0.0 to 1.0).
    /// Higher = stronger interest.
    /// </summary>
    public decimal InterestScore { get; set; }

    /// <summary>
    /// Confidence in the interest score (0.0 to 1.0).
    /// Based on amount of data supporting the inference.
    /// </summary>
    public decimal Confidence { get; set; }

    /// <summary>
    /// Number of articles read in this topic.
    /// </summary>
    public int ArticlesRead { get; set; }

    /// <summary>
    /// Total reading time in this topic (seconds).
    /// </summary>
    public int TotalReadingTimeSeconds { get; set; }

    /// <summary>
    /// Number of articles reacted to in this topic.
    /// </summary>
    public int ReactionsGiven { get; set; }

    /// <summary>
    /// Number of articles bookmarked in this topic.
    /// </summary>
    public int ArticlesBookmarked { get; set; }

    /// <summary>
    /// Average reading completion rate in this topic.
    /// </summary>
    public decimal AvgCompletionRate { get; set; }

    /// <summary>
    /// When this interest was last updated.
    /// </summary>
    public DateTime LastActivityAt { get; set; }

    /// <summary>
    /// When this interest was first detected.
    /// </summary>
    public DateTime FirstActivityAt { get; set; }

    /// <summary>
    /// Decay factor applied to older activity.
    /// Score is adjusted by recency.
    /// </summary>
    public decimal RecencyWeight { get; set; } = 1.0m;
}
