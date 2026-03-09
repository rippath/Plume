namespace Plume.Identity.Configuration;

/// <summary>
/// Google OAuth configuration settings.
/// </summary>
public class GoogleSettings
{
    public const string SectionName = "Authentication:Google";

    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
}
