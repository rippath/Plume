using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Plume.Domain.Entities.Users;
using Plume.Identity.Configuration;
using Plume.Identity.DTOs;
using Plume.Persistence;
using Plume.Persistence.Identity;

namespace Plume.Identity.Services;

/// <summary>
/// Implementation of authentication operations.
/// </summary>
public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly PlumeDbContext _dbContext;
    private readonly PlumeIdentityDbContext _identityContext;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        PlumeDbContext dbContext,
        PlumeIdentityDbContext identityContext,
        IJwtTokenService jwtTokenService,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _identityContext = identityContext;
        _jwtTokenService = jwtTokenService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        // Check if email already exists in domain
        var existingUser = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (existingUser != null)
        {
            return new AuthResponse
            {
                Success = false,
                Error = "A user with this email already exists."
            };
        }

        // Check if username already exists
        var existingUsername = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Username == request.Username);

        if (existingUsername != null)
        {
            return new AuthResponse
            {
                Success = false,
                Error = "This username is already taken."
            };
        }

        // Create domain user
        var domainUser = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            DisplayName = request.DisplayName ?? request.Username,
            EmailVerified = false,
            HasLocalCredentials = true,
            Role = "User"
        };

        _dbContext.Users.Add(domainUser);
        await _dbContext.SaveChangesAsync();

        // Create Identity user
        var identityUser = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = request.Email,
            Email = request.Email,
            UserId = domainUser.Id,
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(identityUser, request.Password);

        if (!result.Succeeded)
        {
            // Rollback domain user
            _dbContext.Users.Remove(domainUser);
            await _dbContext.SaveChangesAsync();

            return new AuthResponse
            {
                Success = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        return new AuthResponse
        {
            Success = true,
            User = MapToUserInfo(domainUser)
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, string? ipAddress = null)
    {
        var identityUser = await _userManager.FindByEmailAsync(request.Email);

        if (identityUser == null)
        {
            return new AuthResponse
            {
                Success = false,
                Error = "Invalid email or password."
            };
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

        if (!isValidPassword)
        {
            // Increment failed login count
            await _userManager.AccessFailedAsync(identityUser);

            if (await _userManager.IsLockedOutAsync(identityUser))
            {
                return new AuthResponse
                {
                    Success = false,
                    Error = "Account is locked. Please try again later."
                };
            }

            return new AuthResponse
            {
                Success = false,
                Error = "Invalid email or password."
            };
        }

        // Reset failed count on successful login
        await _userManager.ResetAccessFailedCountAsync(identityUser);

        var domainUser = await _dbContext.Users.FindAsync(identityUser.UserId);

        if (domainUser == null || !domainUser.IsActive)
        {
            return new AuthResponse
            {
                Success = false,
                Error = "Account not found or is disabled."
            };
        }

        // Update last login
        domainUser.LastLoginAt = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();

        return await GenerateAuthResponseAsync(domainUser, ipAddress);
    }

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken, string? ipAddress = null)
    {
        var storedToken = await _dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (storedToken == null)
        {
            return new AuthResponse
            {
                Success = false,
                Error = "Invalid refresh token."
            };
        }

        if (!storedToken.IsActive)
        {
            return new AuthResponse
            {
                Success = false,
                Error = "Refresh token has expired or been revoked."
            };
        }

        var user = storedToken.User;

        if (user == null || !user.IsActive)
        {
            return new AuthResponse
            {
                Success = false,
                Error = "User not found or is disabled."
            };
        }

        // Revoke old token (token rotation)
        storedToken.RevokedAt = DateTime.UtcNow;
        storedToken.RevokedByIp = ipAddress;
        storedToken.RevokedReason = "Replaced by new token";

        // Generate new tokens
        var response = await GenerateAuthResponseAsync(user, ipAddress);

        // Link old token to new
        storedToken.ReplacedByToken = response.RefreshToken;
        await _dbContext.SaveChangesAsync();

        return response;
    }

    public async Task<bool> RevokeTokenAsync(string refreshToken, string? ipAddress = null, string? reason = null)
    {
        var storedToken = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (storedToken == null || !storedToken.IsActive)
        {
            return false;
        }

        storedToken.RevokedAt = DateTime.UtcNow;
        storedToken.RevokedByIp = ipAddress;
        storedToken.RevokedReason = reason ?? "Revoked by user";

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<AuthResponse> GoogleAuthAsync(GoogleCallbackData googleData, string? ipAddress = null)
    {
        // Check if user exists by Google ID
        var identityUser = await _identityContext.Users
            .FirstOrDefaultAsync(u => u.GoogleId == googleData.GoogleId);

        User? domainUser = null;

        if (identityUser != null)
        {
            // Existing user - login
            domainUser = await _dbContext.Users.FindAsync(identityUser.UserId);

            if (domainUser == null || !domainUser.IsActive)
            {
                return new AuthResponse
                {
                    Success = false,
                    Error = "Account not found or is disabled."
                };
            }

            domainUser.LastLoginAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            // Check if email exists (link accounts)
            var existingByEmail = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == googleData.Email);

            if (existingByEmail != null)
            {
                // Link Google to existing account
                domainUser = existingByEmail;
                domainUser.ExternalId = googleData.GoogleId;
                domainUser.ExternalProvider = "Google";
                domainUser.EmailVerified = googleData.EmailVerified;
                domainUser.LastLoginAt = DateTime.UtcNow;

                // Find or create identity user
                var existingIdentity = await _identityContext.Users
                    .FirstOrDefaultAsync(u => u.UserId == domainUser.Id);

                if (existingIdentity != null)
                {
                    existingIdentity.GoogleId = googleData.GoogleId;
                }
                else
                {
                    identityUser = new ApplicationUser
                    {
                        Id = Guid.NewGuid(),
                        UserName = googleData.Email,
                        Email = googleData.Email,
                        UserId = domainUser.Id,
                        GoogleId = googleData.GoogleId,
                        EmailConfirmed = googleData.EmailVerified
                    };
                    await _userManager.CreateAsync(identityUser);
                }

                await _dbContext.SaveChangesAsync();
            }
            else
            {
                // New user - register
                var username = await GenerateUniqueUsernameAsync(googleData.Email);

                domainUser = new User
                {
                    Id = Guid.NewGuid(),
                    Username = username,
                    Email = googleData.Email,
                    DisplayName = googleData.Name ?? googleData.GivenName ?? username,
                    EmailVerified = googleData.EmailVerified,
                    ExternalId = googleData.GoogleId,
                    ExternalProvider = "Google",
                    HasLocalCredentials = false,
                    Role = "User",
                    LastLoginAt = DateTime.UtcNow
                };

                _dbContext.Users.Add(domainUser);
                await _dbContext.SaveChangesAsync();

                identityUser = new ApplicationUser
                {
                    Id = Guid.NewGuid(),
                    UserName = googleData.Email,
                    Email = googleData.Email,
                    UserId = domainUser.Id,
                    GoogleId = googleData.GoogleId,
                    EmailConfirmed = googleData.EmailVerified
                };

                await _userManager.CreateAsync(identityUser);
            }
        }

        return await GenerateAuthResponseAsync(domainUser!, ipAddress);
    }

    private async Task<AuthResponse> GenerateAuthResponseAsync(User user, string? ipAddress)
    {
        var (accessToken, expiration) = _jwtTokenService.GenerateAccessToken(user);
        var refreshTokenValue = _jwtTokenService.GenerateRefreshToken();

        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = refreshTokenValue,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            CreatedByIp = ipAddress
        };

        _dbContext.RefreshTokens.Add(refreshToken);
        await _dbContext.SaveChangesAsync();

        return new AuthResponse
        {
            Success = true,
            AccessToken = accessToken,
            AccessTokenExpiration = expiration,
            RefreshToken = refreshTokenValue,
            User = MapToUserInfo(user)
        };
    }

    private async Task<string> GenerateUniqueUsernameAsync(string email)
    {
        var baseUsername = email.Split('@')[0]
            .Replace(".", "")
            .Replace("-", "")
            .Replace("_", "");

        if (baseUsername.Length > 80)
        {
            baseUsername = baseUsername[..80];
        }

        var username = baseUsername;
        var counter = 1;

        while (await _dbContext.Users.AnyAsync(u => u.Username == username))
        {
            username = $"{baseUsername}{counter}";
            counter++;
        }

        return username;
    }

    private static UserInfo MapToUserInfo(User user)
    {
        return new UserInfo
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            DisplayName = user.DisplayName,
            Role = user.Role,
            EmailVerified = user.EmailVerified
        };
    }
}
