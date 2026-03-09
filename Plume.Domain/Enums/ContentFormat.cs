namespace Plume.Domain.Enums;

/// <summary>
/// Defines the format of article content storage.
/// </summary>
public enum ContentFormat
{
    /// <summary>
    /// Plain markdown text.
    /// </summary>
    Markdown = 0,

    /// <summary>
    /// Raw HTML content.
    /// </summary>
    Html = 1,

    /// <summary>
    /// Editor.js JSON format - primary format for this platform.
    /// </summary>
    EditorJs = 2
}
