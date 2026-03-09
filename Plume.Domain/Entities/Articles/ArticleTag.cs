using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;

namespace Plume.Domain.Entities.Articles;

/// <summary>
/// Many-to-many join table between Articles and Tags.
/// Includes ordering for primary/secondary tag distinction.
/// </summary>
public class ArticleTag : BaseEntity
{
    [Required]
    public Guid ArticleId { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; } = null!;

    [Required]
    public Guid TagId { get; set; }

    [ForeignKey(nameof(TagId))]
    public Tag Tag { get; set; } = null!;

    /// <summary>
    /// Display order (lower = more prominent).
    /// First tag (Order=0) is typically the primary category.
    /// </summary>
    public int Order { get; set; }
}
