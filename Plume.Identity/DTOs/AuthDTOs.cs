using System.ComponentModel.DataAnnotations;

namespace Plume.Identity.DTOs;

/// <summary>
/// Request to register a new user with email/password.
/// </summary>
public record RegisterRequest
{
    [Required]
    [MaxLength(100)]
    public string Username { get; init; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; init; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; init; } = string.Empty;

    [MaxLength(100)]
    public string? DisplayName { get; init; }
}

/// <summary>
/// Request to login with email/password.
/// </summary>
public record LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    public string Password { get; init; } = string.Empty;
}

/// <summary>
/// Request to refresh an access token.
/// </summary>
public record RefreshTokenRequest
{
    public string? RefreshToken { get; init; }
}

/// <summary>
/// Successful authentication response.
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
/// User information returned in auth responses.
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

/// <summary>
/// Google OAuth callback data.
/// </summary>
public record GoogleCallbackData
{
    public string GoogleId { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? Name { get; init; }
    public string? GivenName { get; init; }
    public string? FamilyName { get; init; }
    public string? Picture { get; init; }
    public bool EmailVerified { get; init; }
}
