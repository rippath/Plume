using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Plume.Identity.DTOs;
using Plume.Identity.Services;
using Plume.Persistence.Identity;

namespace Plume.UI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(
        IAuthService authService,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration)
    {
        _authService = authService;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var ipAddress = GetIpAddress();
        var result = await _authService.LoginAsync(request, ipAddress);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        // Set refresh token in HTTP-only cookie
        SetRefreshTokenCookie(result.RefreshToken!);

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest? request = null)
    {
        // Try to get refresh token from cookie first, then from body
        var refreshToken = Request.Cookies["refreshToken"] ?? request?.RefreshToken;

        if (string.IsNullOrEmpty(refreshToken))
        {
            return BadRequest(new AuthResponse
            {
                Success = false,
                Error = "Refresh token is required."
            });
        }

        var ipAddress = GetIpAddress();
        var result = await _authService.RefreshTokenAsync(refreshToken, ipAddress);

        if (!result.Success)
        {
            // Clear invalid cookie
            Response.Cookies.Delete("refreshToken");
            return BadRequest(result);
        }

        // Set new refresh token in cookie
        SetRefreshTokenCookie(result.RefreshToken!);

        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if (!string.IsNullOrEmpty(refreshToken))
        {
            var ipAddress = GetIpAddress();
            await _authService.RevokeTokenAsync(refreshToken, ipAddress, "User logout");
        }

        // Clear the cookie
        Response.Cookies.Delete("refreshToken");

        // Sign out of external cookie
        await _signInManager.SignOutAsync();

        return Ok(new { Success = true, Message = "Logged out successfully." });
    }

    [HttpGet("google-login")]
    public IActionResult GoogleLogin([FromQuery] string? redirectUrl = "/")
    {
        var callbackUrl = Url.Action(nameof(GoogleCallback), "Auth", new { redirectUrl }, Request.Scheme);
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(
            GoogleDefaults.AuthenticationScheme,
            callbackUrl);

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("google-callback")]
    public async Task<IActionResult> GoogleCallback([FromQuery] string? redirectUrl = "/")
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();

        if (info == null)
        {
            return Redirect($"/login?error=google_auth_failed");
        }

        var claims = info.Principal.Claims.ToList();

        var googleData = new GoogleCallbackData
        {
            GoogleId = info.ProviderKey,
            Email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? "",
            Name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value,
            GivenName = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value,
            FamilyName = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value,
            Picture = claims.FirstOrDefault(c => c.Type == "urn:google:picture")?.Value
                      ?? claims.FirstOrDefault(c => c.Type == "picture")?.Value,
            EmailVerified = true // Google verifies emails
        };

        var ipAddress = GetIpAddress();
        var result = await _authService.GoogleAuthAsync(googleData, ipAddress);

        // Clear external cookie
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        if (!result.Success)
        {
            return Redirect($"/login?error={Uri.EscapeDataString(result.Error ?? "Authentication failed")}");
        }

        // Set refresh token in cookie
        SetRefreshTokenCookie(result.RefreshToken!);

        // For Google OAuth, we need to pass the access token to the client
        // We'll use a temporary cookie that the client can read
        Response.Cookies.Append("accessToken", result.AccessToken!, new CookieOptions
        {
            HttpOnly = false, // Client needs to read this
            Secure = false, // Allow HTTP for development
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddMinutes(5) // Short-lived for security
        });

        return Redirect(redirectUrl ?? "/");
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult GetCurrentUser()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = User.FindFirst("username")?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var displayName = User.FindFirst("displayName")?.Value;
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        var emailVerified = User.FindFirst("emailVerified")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        return Ok(new UserInfo
        {
            Id = Guid.Parse(userId),
            Username = username ?? "",
            Email = email ?? "",
            DisplayName = displayName,
            Role = role ?? "User",
            EmailVerified = emailVerified == "true"
        });
    }

    private void SetRefreshTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = false, // Allow HTTP for development
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        };

        Response.Cookies.Append("refreshToken", token, cookieOptions);
    }

    private string? GetIpAddress()
    {
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            return Request.Headers["X-Forwarded-For"].FirstOrDefault();
        }

        return HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
    }
}
