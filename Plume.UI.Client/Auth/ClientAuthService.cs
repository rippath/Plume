using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.JSInterop;
using Plume.UI.Client.Models;

namespace Plume.UI.Client.Auth;

/// <summary>
/// Client-side authentication service implementation.
/// </summary>
public class ClientAuthService : IClientAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly JwtAuthenticationStateProvider _authStateProvider;
    private readonly IJSRuntime _jsRuntime;

    private const string AccessTokenKey = "accessToken";
    private const string AccessTokenExpirationKey = "accessTokenExpiration";

    public ClientAuthService(
        HttpClient httpClient,
        ILocalStorageService localStorage,
        JwtAuthenticationStateProvider authStateProvider,
        IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authStateProvider = authStateProvider;
        _jsRuntime = jsRuntime;
    }

    public async Task<string?> GetAccessTokenAsync()
    {
        var token = await _localStorage.GetItemAsync<string>(AccessTokenKey);
        var expiration = await _localStorage.GetItemAsync<DateTime?>(AccessTokenExpirationKey);

        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        // Check if token is about to expire (within 5 minutes)
        if (expiration.HasValue && expiration.Value.AddMinutes(-5) <= DateTime.UtcNow)
        {
            // Try to refresh
            var refreshResult = await RefreshTokenAsync();
            if (refreshResult.Success)
            {
                return refreshResult.AccessToken;
            }

            // Refresh failed, clear tokens
            await ClearTokensAsync();
            return null;
        }

        return token;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterModel model)
    {
        try
        {
            var request = new
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                DisplayName = model.DisplayName
            };

            var response = await _httpClient.PostAsJsonAsync("api/auth/register", request);
            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

            return result ?? new AuthResponse { Success = false, Error = "Failed to parse response" };
        }
        catch (Exception ex)
        {
            return new AuthResponse { Success = false, Error = ex.Message };
        }
    }

    public async Task<AuthResponse> LoginAsync(string email, string password)
    {
        try
        {
            var request = new { Email = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);
            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

            if (result?.Success == true && !string.IsNullOrEmpty(result.AccessToken))
            {
                await StoreTokensAsync(result.AccessToken, result.AccessTokenExpiration);
                _authStateProvider.NotifyAuthenticationStateChanged();
            }

            return result ?? new AuthResponse { Success = false, Error = "Failed to parse response" };
        }
        catch (Exception ex)
        {
            return new AuthResponse { Success = false, Error = ex.Message };
        }
    }

    public async Task<AuthResponse> RefreshTokenAsync()
    {
        try
        {
            var response = await _httpClient.PostAsync("api/auth/refresh", null);
            var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

            if (result?.Success == true && !string.IsNullOrEmpty(result.AccessToken))
            {
                await StoreTokensAsync(result.AccessToken, result.AccessTokenExpiration);
                _authStateProvider.NotifyAuthenticationStateChanged();
            }

            return result ?? new AuthResponse { Success = false, Error = "Failed to parse response" };
        }
        catch (Exception ex)
        {
            return new AuthResponse { Success = false, Error = ex.Message };
        }
    }

    public async Task LogoutAsync()
    {
        try
        {
            await _httpClient.PostAsync("api/auth/logout", null);
        }
        catch
        {
            // Ignore logout API errors
        }

        await ClearTokensAsync();
        _authStateProvider.NotifyAuthenticationStateChanged();
    }

    public async Task<UserInfo?> GetCurrentUserAsync()
    {
        var token = await GetAccessTokenAsync();
        if (string.IsNullOrEmpty(token))
        {
            return null;
        }

        return _authStateProvider.GetUserFromToken(token);
    }

    public async Task CheckOAuthCallbackAsync()
    {
        try
        {
            // Check for accessToken cookie from Google OAuth callback
            var token = await _jsRuntime.InvokeAsync<string>("getCookie", "accessToken");

            if (!string.IsNullOrEmpty(token))
            {
                // Store the token and clear the cookie
                await StoreTokensAsync(token, DateTime.UtcNow.AddHours(1));
                await _jsRuntime.InvokeVoidAsync("deleteCookie", "accessToken");
                _authStateProvider.NotifyAuthenticationStateChanged();
            }
        }
        catch
        {
            // Cookie operations may fail, ignore
        }
    }

    private async Task StoreTokensAsync(string accessToken, DateTime? expiration)
    {
        await _localStorage.SetItemAsync(AccessTokenKey, accessToken);
        await _localStorage.SetItemAsync(AccessTokenExpirationKey, expiration ?? DateTime.UtcNow.AddHours(1));
    }

    private async Task ClearTokensAsync()
    {
        await _localStorage.RemoveItemAsync(AccessTokenKey);
        await _localStorage.RemoveItemAsync(AccessTokenExpirationKey);
    }
}
