namespace Plume.Domain.Enums;

/// <summary>
/// Indicates how a user's topic interest was determined.
/// Used for weighting interests in recommendations.
/// </summary>
public enum InterestSource
{
    /// <summary>
    /// User explicitly selected this topic during onboarding or in settings.
    /// Highest confidence signal.
    /// </summary>
    Explicit = 0,

    /// <summary>
    /// Interest inferred from reading patterns and engagement.
    /// </summary>
    InferredFromReading = 1,

    /// <summary>
    /// Interest inferred from search queries.
    /// </summary>
    InferredFromSearch = 2,

    /// <summary>
    /// Interest inferred from articles the user has written.
    /// </summary>
    InferredFromWriting = 3,

    /// <summary>
    /// Interest inferred from followed authors' topics.
    /// </summary>
    InferredFromFollows = 4,

    /// <summary>
    /// Interest inferred from bookmarked articles.
    /// </summary>
    InferredFromBookmarks = 5
}
