using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Users;

namespace Plume.Domain.Entities.Social;

/// <summary>
/// Likes on comments. Separate from article reactions.
/// </summary>
public class CommentLike : BaseEntity
{
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [Required]
    public Guid CommentId { get; set; }

    [ForeignKey(nameof(CommentId))]
    public Comment Comment { get; set; } = null!;
}
