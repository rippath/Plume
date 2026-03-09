namespace Plume.Domain.Enums;

/// <summary>
/// Types of reactions users can give to articles or comments.
/// Designed to be extensible for future reaction types.
/// </summary>
public enum ReactionType
{
    /// <summary>
    /// Medium-style clap. Can be given multiple times.
    /// </summary>
    Clap = 0,

    /// <summary>
    /// Standard like reaction.
    /// </summary>
    Like = 1,

    /// <summary>
    /// Love/heart reaction for exceptional content.
    /// </summary>
    Love = 2,

    /// <summary>
    /// Marks content as particularly insightful or thought-provoking.
    /// </summary>
    Insightful = 3,

    /// <summary>
    /// Marks content as funny or entertaining.
    /// </summary>
    Funny = 4
}
