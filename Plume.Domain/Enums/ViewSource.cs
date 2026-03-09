namespace Plume.Domain.Enums;

/// <summary>
/// Tracks how users discover and navigate to articles.
/// Critical for recommendation engine training and attribution.
/// </summary>
public enum ViewSource
{
    /// <summary>
    /// User arrived from their personalized feed.
    /// </summary>
    Feed = 0,

    /// <summary>
    /// User arrived via search results.
    /// </summary>
    Search = 1,

    /// <summary>
    /// Direct URL access (bookmarked, shared link, typed URL).
    /// </summary>
    Direct = 2,

    /// <summary>
    /// User arrived via recommendation widget ("You might also like").
    /// </summary>
    Recommendation = 3,

    /// <summary>
    /// User arrived from an author's profile page.
    /// </summary>
    AuthorProfile = 4,

    /// <summary>
    /// User arrived from a tag/topic page.
    /// </summary>
    TagPage = 5,

    /// <summary>
    /// User arrived from external referrer (social media, other sites).
    /// </summary>
    External = 6,

    /// <summary>
    /// User arrived from email notification or newsletter.
    /// </summary>
    Email = 7,

    /// <summary>
    /// User arrived from the trending/popular articles section.
    /// </summary>
    Trending = 8,

    /// <summary>
    /// User navigated from another article (internal link).
    /// </summary>
    RelatedArticle = 9
}
