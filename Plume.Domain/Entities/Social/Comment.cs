using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Articles;
using Plume.Domain.Entities.Users;

namespace Plume.Domain.Entities.Social;

/// <summary>
/// User comments on articles with threading support.
/// Threaded via ParentCommentId for nested replies.
/// </summary>
public class Comment : BaseEntity
{
    /// <summary>
    /// User who wrote the comment.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    /// <summary>
    /// Article being commented on.
    /// </summary>
    [Required]
    public Guid ArticleId { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; } = null!;

    /// <summary>
    /// Parent comment for threaded replies.
    /// Null for top-level comments.
    /// </summary>
    public Guid? ParentCommentId { get; set; }

    [ForeignKey(nameof(ParentCommentId))]
    public Comment? ParentComment { get; set; }

    /// <summary>
    /// Comment content. Supports limited markdown.
    /// </summary>
    [Required]
    [MaxLength(5000)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Whether comment has been edited.
    /// </summary>
    public bool IsEdited { get; set; }

    /// <summary>
    /// When the comment was last edited.
    /// </summary>
    public DateTime? EditedAt { get; set; }

    /// <summary>
    /// Whether comment is highlighted by the article author.
    /// </summary>
    public bool IsHighlighted { get; set; }

    /// <summary>
    /// Whether comment has been hidden/moderated.
    /// </summary>
    public bool IsHidden { get; set; }

    /// <summary>
    /// Reason for hiding (if moderated).
    /// </summary>
    [MaxLength(500)]
    public string? HideReason { get; set; }

    /// <summary>
    /// Depth level in the thread (0 = top-level).
    /// Denormalized for efficient querying.
    /// </summary>
    public int Depth { get; set; }

    // Denormalized counts
    public int ReplyCount { get; set; }
    public int LikeCount { get; set; }

    // Navigation: Replies to this comment
    public ICollection<Comment> Replies { get; set; } = new List<Comment>();

    // Navigation: Likes on this comment
    public ICollection<CommentLike> Likes { get; set; } = new List<CommentLike>();
}
