using System.ComponentModel.DataAnnotations;
using Plume.Domain.Common;
using Plume.Domain.Entities.Analytics;

namespace Plume.Domain.Entities.Articles;

/// <summary>
/// Reusable tags/topics for article categorization.
/// Central to content discovery and recommendations.
/// </summary>
public class Tag : BaseEntity
{
    /// <summary>
    /// Tag name (e.g., "Programming", "Machine Learning").
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// URL-friendly slug.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Description of the tag/topic.
    /// </summary>
    [MaxLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// URL to tag's icon or image.
    /// </summary>
    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Hex color code for visual representation.
    /// </summary>
    [MaxLength(7)]
    public string? ColorHex { get; set; }

    /// <summary>
    /// Whether this is a featured/promoted tag.
    /// </summary>
    public bool IsFeatured { get; set; }

    /// <summary>
    /// Parent tag for hierarchical organization.
    /// </summary>
    public Guid? ParentTagId { get; set; }
    public Tag? ParentTag { get; set; }

    // Denormalized counts
    public int ArticleCount { get; set; }
    public int FollowerCount { get; set; }

    // Navigation: Articles with this tag
    public ICollection<ArticleTag> ArticleTags { get; set; } = new List<ArticleTag>();

    // Navigation: Child tags
    public ICollection<Tag> ChildTags { get; set; } = new List<Tag>();

    // Navigation: Users interested in this tag
    public ICollection<UserTopicInterest> UserInterests { get; set; } = new List<UserTopicInterest>();
}
