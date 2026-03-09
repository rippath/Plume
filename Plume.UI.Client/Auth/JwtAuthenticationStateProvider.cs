using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Plume.UI.Client.Models;

namespace Plume.UI.Client.Auth;

/// <summary>
/// Custom authentication state provider that reads JWT tokens from local storage.
/// </summary>
public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private const string AccessTokenKey = "accessToken";
    private const string AccessTokenExpirationKey = "accessTokenExpiration";

    public JwtAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var token = await _localStorage.GetItemAsync<string>(AccessTokenKey);
            var expiration = await _localStorage.GetItemAsync<DateTime?>(AccessTokenExpirationKey);

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // Check if token is expired
            if (expiration.HasValue && expiration.Value <= DateTime.UtcNow)
            {
                await _localStorage.RemoveItemAsync(AccessTokenKey);
                await _localStorage.RemoveItemAsync(AccessTokenExpirationKey);
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwt");
            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationState(principal);
        }
        catch
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }

    public void NotifyAuthenticationStateChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public UserInfo? GetUserFromToken(string token)
    {
        try
        {
            var claims = ParseClaimsFromJwt(token);

            return new UserInfo
            {
                Id = Guid.Parse(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? Guid.Empty.ToString()),
                Username = claims.FirstOrDefault(c => c.Type == "username")?.Value ?? "",
                Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "",
                DisplayName = claims.FirstOrDefault(c => c.Type == "displayName")?.Value,
                Role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? "User",
                EmailVerified = claims.FirstOrDefault(c => c.Type == "emailVerified")?.Value == "true"
            };
        }
        catch
        {
            return null;
        }
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }
}
