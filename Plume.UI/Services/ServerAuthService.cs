using System.Security.Claims;
using Plume.UI.Client.Auth;
using Plume.UI.Client.Models;

namespace Plume.UI.Services;

/// <summary>
/// Server-side implementation of IClientAuthService for prerendering.
/// Uses HttpContext for authentication state during server-side rendering.
/// </summary>
public class ServerAuthService : IClientAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ServerAuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<string?> GetAccessTokenAsync()
    {
        // Server-side doesn't store tokens in localStorage
        // Return null - the client will handle token management
        return Task.FromResult<string?>(null);
    }

    public Task<AuthResponse> RegisterAsync(RegisterModel model)
    {
        // Registration should happen via API call, not during prerender
        return Task.FromResult(new AuthResponse
        {
            Success = false,
            Error = "Registration not available during server rendering"
        });
    }

    public Task<AuthResponse> LoginAsync(string email, string password)
    {
        // Login should happen via API call, not during prerender
        return Task.FromResult(new AuthResponse
        {
            Success = false,
            Error = "Login not available during server rendering"
        });
    }

    public Task<AuthResponse> RefreshTokenAsync()
    {
        return Task.FromResult(new AuthResponse
        {
            Success = false,
            Error = "Token refresh not available during server rendering"
        });
    }

    public Task LogoutAsync()
    {
        // Logout handled by client-side redirect to API
        return Task.CompletedTask;
    }

    public Task<UserInfo?> GetCurrentUserAsync()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true)
        {
            return Task.FromResult<UserInfo?>(null);
        }

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var id))
        {
            return Task.FromResult<UserInfo?>(null);
        }

        return Task.FromResult<UserInfo?>(new UserInfo
        {
            Id = id,
            Username = user.FindFirst("username")?.Value ?? "",
            Email = user.FindFirst(ClaimTypes.Email)?.Value ?? "",
            DisplayName = user.FindFirst("displayName")?.Value,
            Role = user.FindFirst(ClaimTypes.Role)?.Value ?? "User",
            EmailVerified = user.FindFirst("emailVerified")?.Value == "true"
        });
    }

    public Task CheckOAuthCallbackAsync()
    {
        // OAuth callback handled by server controller
        return Task.CompletedTask;
    }
}
