using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Articles;
using Plume.Domain.Entities.Users;
using Plume.Domain.Enums;

namespace Plume.Domain.Entities.Social;

/// <summary>
/// Represents user reactions (claps, likes, etc.) on articles.
/// Supports Medium-style multiple claps via Count field.
/// Critical engagement signal for recommendations.
/// </summary>
public class Reaction : BaseEntity
{
    /// <summary>
    /// User who gave the reaction.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    /// <summary>
    /// Article receiving the reaction.
    /// </summary>
    [Required]
    public Guid ArticleId { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public Article Article { get; set; } = null!;

    /// <summary>
    /// Type of reaction.
    /// </summary>
    [Required]
    public ReactionType Type { get; set; }

    /// <summary>
    /// Number of times this reaction was given.
    /// For claps, users can clap multiple times (up to a max).
    /// For other reactions, typically 1.
    /// </summary>
    public int Count { get; set; } = 1;
}
