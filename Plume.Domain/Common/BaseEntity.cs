using System.ComponentModel.DataAnnotations;

namespace Plume.Domain.Common;

/// <summary>
/// Base entity with common audit fields.
/// All domain entities inherit from this.
/// </summary>
public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public Guid? CreatedById { get; set; }

    public DateTime? UpdatedDate { get; set; }
    public Guid? UpdatedById { get; set; }

    public DateTime? DeletedDate { get; set; }
    public Guid? DeletedById { get; set; }
}