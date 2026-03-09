using System.ComponentModel.DataAnnotations;
using Plume.Domain.Common;

namespace Plume.Domain.Entities.Users;

/// <summary>
/// Represents a refresh token for JWT authentication.
/// Used to obtain new access tokens without re-authentication.
/// </summary>
public class RefreshToken : BaseEntity
{
    [Required]
    [MaxLength(500)]
    public string Token { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    [MaxLength(100)]
    public string? ReplacedByToken { get; set; }

    [MaxLength(50)]
    public string? CreatedByIp { get; set; }

    [MaxLength(50)]
    public string? RevokedByIp { get; set; }

    [MaxLength(200)]
    public string? RevokedReason { get; set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public bool IsRevoked => RevokedAt != null;

    public new bool IsActive => !IsRevoked && !IsExpired;

    // Foreign key to User
    public Guid UserId { get; set; }

    // Navigation property
    public User User { get; set; } = null!;
}
