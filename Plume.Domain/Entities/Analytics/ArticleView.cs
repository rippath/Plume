using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Articles;
using Plume.Domain.Entities.Users;
using Plume.Domain.Enums;

namespace Plume.Domain.Entities.Analytics;

/// <summary>
/// Records individual article view events.
/// First-class entity capturing how users discover and access content.
/// Essential for attribution, recommendation training, and analytics.
/// </summary>
public class ArticleView : BaseEntity
{
    /// <summary>
    /// Article being viewed.
    /// </summary>
    [Required]
    public Guid ArticleId { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; } = null!;

    /// <summary>
    /// User viewing the article.
    /// Null for anonymous views.
    /// </summary>
    public Guid? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    /// <summary>
    /// How the user arrived at this article.
    /// Critical for recommendation attribution.
    /// </summary>
    [Required]
    public ViewSource Source { get; set; }

    /// <summary>
    /// Related entity that led to this view.
    /// For Recommendation: the source article ID.
    /// For TagPage: the tag ID.
    /// For AuthorProfile: the author ID.
    /// For Search: the SearchQuery ID.
    /// </summary>
    public Guid? SourceEntityId { get; set; }

    /// <summary>
    /// External referrer URL (for External source).
    /// </summary>
    [MaxLength(1000)]
    public string? ReferrerUrl { get; set; }

    /// <summary>
    /// Session identifier for grouping views.
    /// </summary>
    [MaxLength(100)]
    public string? SessionId { get; set; }

    /// <summary>
    /// User agent string for device analytics.
    /// </summary>
    [MaxLength(500)]
    public string? UserAgent { get; set; }

    /// <summary>
    /// Country code derived from IP (for geo analytics).
    /// </summary>
    [MaxLength(2)]
    public string? CountryCode { get; set; }

    /// <summary>
    /// Device type (desktop, mobile, tablet).
    /// </summary>
    [MaxLength(20)]
    public string? DeviceType { get; set; }

    /// <summary>
    /// Whether this is the user's first view of this article.
    /// </summary>
    public bool IsFirstView { get; set; }

    /// <summary>
    /// Timestamp of the view.
    /// Separate from CreatedDate for explicit event time.
    /// </summary>
    public DateTime ViewedAt { get; set; }
}
