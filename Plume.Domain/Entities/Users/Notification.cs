using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Enums;

namespace Plume.Domain.Entities.Users;

/// <summary>
/// User notifications for platform events.
/// </summary>
public class Notification : BaseEntity
{
    /// <summary>
    /// The user receiving the notification.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [Required]
    public NotificationType Type { get; set; }

    /// <summary>
    /// Notification title/headline.
    /// </summary>
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Notification body text.
    /// </summary>
    [MaxLength(1000)]
    public string? Message { get; set; }

    /// <summary>
    /// URL to navigate to when notification is clicked.
    /// </summary>
    [MaxLength(500)]
    public string? ActionUrl { get; set; }

    /// <summary>
    /// Related entity ID (article, comment, user, etc.).
    /// </summary>
    public Guid? RelatedEntityId { get; set; }

    /// <summary>
    /// Type name of the related entity for polymorphic reference.
    /// </summary>
    [MaxLength(50)]
    public string? RelatedEntityType { get; set; }

    public bool IsRead { get; set; }

    public DateTime? ReadAt { get; set; }
}
