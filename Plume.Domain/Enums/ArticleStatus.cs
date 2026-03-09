namespace Plume.Domain.Enums;

/// <summary>
/// Represents the lifecycle state of an article.
/// </summary>
public enum ArticleStatus
{
    /// <summary>
    /// Article is being written and not visible to readers.
    /// </summary>
    Draft = 0,

    /// <summary>
    /// Article is published and visible to readers.
    /// </summary>
    Published = 1,

    /// <summary>
    /// Article was published but has been unpublished by the author.
    /// Preserved for historical references but not discoverable.
    /// </summary>
    Unlisted = 2,

    /// <summary>
    /// Article has been archived by the author.
    /// Hidden from feeds but accessible via direct link.
    /// </summary>
    Archived = 3,

    /// <summary>
    /// Article has been soft-deleted.
    /// </summary>
    Deleted = 4
}
