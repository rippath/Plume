using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Plume.Domain.Common;
using Plume.Domain.Entities.Users;

namespace Plume.Domain.Entities.Analytics;

/// <summary>
/// Records user search queries for behavior analysis.
/// Search patterns are strong explicit signals for topic interests.
/// Used for cold-start mitigation and interest inference.
/// </summary>
public class SearchQuery : BaseEntity
{
    /// <summary>
    /// User who performed the search.
    /// Null for anonymous searches.
    /// </summary>
    public Guid? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    /// <summary>
    /// The search query text.
    /// </summary>
    [Required]
    [MaxLength(500)]
    public string Query { get; set; } = string.Empty;

    /// <summary>
    /// Normalized/cleaned version of the query.
    /// </summary>
    [MaxLength(500)]
    public string? NormalizedQuery { get; set; }

    /// <summary>
    /// Number of results returned.
    /// </summary>
    public int ResultCount { get; set; }

    /// <summary>
    /// Whether user clicked on any result.
    /// </summary>
    public bool HasClick { get; set; }

    /// <summary>
    /// ID of first clicked article (if any).
    /// </summary>
    public Guid? ClickedArticleId { get; set; }

    /// <summary>
    /// Position of the clicked result (1-based).
    /// Useful for search ranking analysis.
    /// </summary>
    public int? ClickedPosition { get; set; }

    /// <summary>
    /// Time spent on search results page before click.
    /// </summary>
    public int? TimeToClickSeconds { get; set; }

    /// <summary>
    /// Search filters applied (JSON serialized).
    /// </summary>
    [MaxLength(1000)]
    public string? FiltersApplied { get; set; }

    /// <summary>
    /// Browser session identifier.
    /// </summary>
    [MaxLength(100)]
    public string? SessionId { get; set; }

    /// <summary>
    /// When the search was performed.
    /// </summary>
    public DateTime SearchedAt { get; set; }
}
