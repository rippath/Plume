using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Users;

namespace Plume.Domain.Entities.Articles;

/// <summary>
/// Represents a series of related articles (e.g., tutorial series, multi-part posts).
/// </summary>
public class ArticleSeries : BaseEntity
{
    /// <summary>
    /// Author/owner of the series.
    /// </summary>
    [Required]
    public Guid AuthorId { get; set; }

    [ForeignKey(nameof(AuthorId))]
    public User Author { get; set; } = null!;

    /// <summary>
    /// Series title.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// URL-friendly slug.
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Series description/introduction.
    /// </summary>
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Cover image URL.
    /// </summary>
    [MaxLength(500)]
    public string? CoverImageUrl { get; set; }

    /// <summary>
    /// Whether the series is complete.
    /// </summary>
    public bool IsComplete { get; set; }

    // Navigation: Articles in this series
    public ICollection<Article> Articles { get; set; } = new List<Article>();
}
