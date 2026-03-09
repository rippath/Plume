namespace Plume.UI.Client.Models;

/// <summary>
/// Authentication response from the API.
/// </summary>
public record AuthResponse
{
    public bool Success { get; init; }
    public string? AccessToken { get; init; }
    public DateTime? AccessTokenExpiration { get; init; }
    public string? RefreshToken { get; init; }
    public UserInfo? User { get; init; }
    public string? Error { get; init; }
    public IEnumerable<string>? Errors { get; init; }
}

/// <summary>
/// User information from auth responses.
/// </summary>
public record UserInfo
{
    public Guid Id { get; init; }
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? DisplayName { get; init; }
    public string Role { get; init; } = "User";
    public bool EmailVerified { get; init; }
}
