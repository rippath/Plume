using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Enums;

namespace Plume.Domain.Entities.Articles;

/// <summary>
/// Stores revision history for articles.
/// Enables version comparison, rollback, and audit trails.
/// </summary>
public class ArticleRevision : BaseEntity
{
    [Required]
    public Guid ArticleId { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; } = null!;

    /// <summary>
    /// Revision number (auto-incremented per article).
    /// </summary>
    public int RevisionNumber { get; set; }

    /// <summary>
    /// Title at this revision.
    /// </summary>
    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Content at this revision.
    /// </summary>
    [Required]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Content format at this revision.
    /// </summary>
    public ContentFormat ContentFormat { get; set; }

    /// <summary>
    /// Summary at this revision.
    /// </summary>
    [MaxLength(500)]
    public string? Summary { get; set; }

    /// <summary>
    /// Optional note about what changed.
    /// </summary>
    [MaxLength(500)]
    public string? RevisionNote { get; set; }

    /// <summary>
    /// Word count at this revision.
    /// </summary>
    public int WordCount { get; set; }
}
