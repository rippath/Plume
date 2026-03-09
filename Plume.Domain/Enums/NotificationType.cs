namespace Plume.Domain.Enums;

/// <summary>
/// Types of notifications that can be sent to users.
/// </summary>
public enum NotificationType
{
    /// <summary>
    /// Someone followed the user.
    /// </summary>
    NewFollower = 0,

    /// <summary>
    /// Someone reacted to user's article.
    /// </summary>
    ArticleReaction = 1,

    /// <summary>
    /// Someone commented on user's article.
    /// </summary>
    ArticleComment = 2,

    /// <summary>
    /// Someone replied to user's comment.
    /// </summary>
    CommentReply = 3,

    /// <summary>
    /// A followed author published a new article.
    /// </summary>
    NewArticleFromFollowed = 4,

    /// <summary>
    /// User's article was featured or highlighted.
    /// </summary>
    ArticleFeatured = 5,

    /// <summary>
    /// User was mentioned in an article or comment.
    /// </summary>
    Mention = 6,

    /// <summary>
    /// Article milestone reached (views, reactions).
    /// </summary>
    ArticleMilestone = 7
}
