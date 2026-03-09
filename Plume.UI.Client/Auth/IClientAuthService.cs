using Plume.UI.Client.Models;

namespace Plume.UI.Client.Auth;

/// <summary>
/// Client-side authentication service interface.
/// </summary>
public interface IClientAuthService
{
    /// <summary>
    /// Gets the current access token.
    /// </summary>
    Task<string?> GetAccessTokenAsync();

    /// <summary>
    /// Registers a new user.
    /// </summary>
    Task<AuthResponse> RegisterAsync(RegisterModel model);

    /// <summary>
    /// Logs in with email and password.
    /// </summary>
    Task<AuthResponse> LoginAsync(string email, string password);

    /// <summary>
    /// Refreshes the access token.
    /// </summary>
    Task<AuthResponse> RefreshTokenAsync();

    /// <summary>
    /// Logs out the current user.
    /// </summary>
    Task LogoutAsync();

    /// <summary>
    /// Gets the current user info from the stored token.
    /// </summary>
    Task<UserInfo?> GetCurrentUserAsync();

    /// <summary>
    /// Checks if there's an access token from Google OAuth callback.
    /// </summary>
    Task CheckOAuthCallbackAsync();
}
