namespace Plume.Identity.Configuration;

/// <summary>
/// JWT token configuration settings.
/// </summary>
public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = "Plume";
    public string Audience { get; set; } = "PlumeUsers";
    public int AccessTokenExpirationMinutes { get; set; } = 60;
    public int RefreshTokenExpirationDays { get; set; } = 7;
}
