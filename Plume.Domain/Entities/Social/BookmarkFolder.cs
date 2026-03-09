using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Users;

namespace Plume.Domain.Entities.Social;

/// <summary>
/// Folders/lists for organizing bookmarks.
/// </summary>
public class BookmarkFolder : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    /// <summary>
    /// Folder name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Optional description.
    /// </summary>
    [MaxLength(500)]
    public string? Description { get; set; }

    /// <summary>
    /// Whether this folder is private or public.
    /// </summary>
    public bool IsPrivate { get; set; } = true;

    /// <summary>
    /// Display order.
    /// </summary>
    public int Order { get; set; }

    // Navigation: Bookmarks in this folder
    public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
}
