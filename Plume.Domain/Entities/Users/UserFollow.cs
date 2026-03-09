using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;

namespace Plume.Domain.Entities.Users;

/// <summary>
/// Represents a follower/following relationship between users.
/// Forms the social graph essential for feed personalization.
/// </summary>
public class UserFollow : BaseEntity
{
    /// <summary>
    /// The user who is following.
    /// </summary>
    [Required]
    public Guid FollowerId { get; set; }

    [ForeignKey(nameof(FollowerId))]
    public User Follower { get; set; } = null!;

    /// <summary>
    /// The user being followed.
    /// </summary>
    [Required]
    public Guid FolloweeId { get; set; }

    [ForeignKey(nameof(FolloweeId))]
    public User Followee { get; set; } = null!;

    /// <summary>
    /// Whether to receive notifications for this author's new articles.
    /// </summary>
    public bool NotifyOnNewArticle { get; set; } = true;
}
