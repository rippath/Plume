using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Articles;
using Plume.Domain.Entities.Users;

namespace Plume.Domain.Entities.Social;

/// <summary>
/// User bookmarks/saved articles.
/// Strong signal of interest for recommendations.
/// </summary>
public class Bookmark : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [Required]
    public Guid ArticleId { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; } = null!;

    /// <summary>
    /// Optional user note about why they saved this.
    /// </summary>
    [MaxLength(500)]
    public string? Note { get; set; }

    /// <summary>
    /// Optional folder/list for organizing bookmarks.
    /// </summary>
    public Guid? FolderId { get; set; }

    [ForeignKey(nameof(FolderId))]
    public BookmarkFolder? Folder { get; set; }
}
