using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Articles;
using Plume.Domain.Entities.Users;

namespace Plume.Domain.Entities.Analytics;

/// <summary>
/// Records recommendation system events for model training and A/B testing.
/// Tracks what was recommended, why, and whether it was successful.
/// </summary>
public class RecommendationEvent : BaseEntity
{
    /// <summary>
    /// User who received the recommendation.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    /// <summary>
    /// Article that was recommended.
    /// </summary>
    [Required]
    public Guid ArticleId { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; } = null!;

    /// <summary>
    /// Position in the recommendation list (1-based).
    /// </summary>
    public int Position { get; set; }

    /// <summary>
    /// Where the recommendation was shown.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Placement { get; set; } = string.Empty; // "feed", "article_footer", "sidebar", etc.

    /// <summary>
    /// Recommendation algorithm/model used.
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Algorithm { get; set; } = string.Empty;

    /// <summary>
    /// Model version for A/B testing.
    /// </summary>
    [MaxLength(50)]
    public string? ModelVersion { get; set; }

    /// <summary>
    /// Predicted score/probability from the model.
    /// </summary>
    public decimal PredictedScore { get; set; }

    /// <summary>
    /// Whether the recommendation was displayed (impression).
    /// </summary>
    public bool WasDisplayed { get; set; }

    /// <summary>
    /// Whether user clicked on the recommendation.
    /// </summary>
    public bool WasClicked { get; set; }

    /// <summary>
    /// Time spent on article if clicked (seconds).
    /// </summary>
    public int? TimeSpentSeconds { get; set; }

    /// <summary>
    /// Whether user engaged positively (completed read, reacted, etc.).
    /// </summary>
    public bool? WasSuccessful { get; set; }

    /// <summary>
    /// Primary reason for recommendation (for explainability).
    /// </summary>
    [MaxLength(200)]
    public string? RecommendationReason { get; set; }

    /// <summary>
    /// Feature values used for this prediction (JSON).
    /// For model debugging and feature importance analysis.
    /// </summary>
    public string? FeatureSnapshot { get; set; }

    /// <summary>
    /// A/B test group identifier.
    /// </summary>
    [MaxLength(50)]
    public string? ExperimentGroup { get; set; }

    public DateTime RecommendedAt { get; set; }
    public DateTime? ClickedAt { get; set; }
}
