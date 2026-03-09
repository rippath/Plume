using Plume.Identity.DTOs;

namespace Plume.Identity.Services;

/// <summary>
/// Service for handling authentication operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user with email/password.
    /// </summary>
    Task<AuthResponse> RegisterAsync(RegisterRequest request);

    /// <summary>
    /// Authenticates a user with email/password.
    /// </summary>
    Task<AuthResponse> LoginAsync(LoginRequest request, string? ipAddress = null);

    /// <summary>
    /// Refreshes an access token using a refresh token.
    /// </summary>
    Task<AuthResponse> RefreshTokenAsync(string refreshToken, string? ipAddress = null);

    /// <summary>
    /// Revokes a refresh token.
    /// </summary>
    Task<bool> RevokeTokenAsync(string refreshToken, string? ipAddress = null, string? reason = null);

    /// <summary>
    /// Authenticates or registers a user via Google OAuth.
    /// </summary>
    Task<AuthResponse> GoogleAuthAsync(GoogleCallbackData googleData, string? ipAddress = null);
}
