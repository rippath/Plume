namespace Plume.UI.Client.Models;

public class EditorJsConfig
{
    public string? Placeholder { get; set; }
    public Dictionary<string, EditorToolConfig>? Tools { get; set; }
    public bool? RightToLeft { get; set; }
    public I18nConfig? I18n { get; set; }
    public object? InlineToolbar { get; set; }
    public bool? Autofocus { get; set; }
    public object? Data { get; set; }
}

public class EditorToolConfig
{
    public string? Class { get; set; }
    public bool? InlineToolbar { get; set; }
    public object? Config { get; set; }
}

public class I18nConfig
{
    public string? Direction { get; set; } // "ltr" or "rtl"
    public Dictionary<string, object>? Messages { get; set; }
}
