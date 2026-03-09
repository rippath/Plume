using Plume.Domain.Entities.Users;

namespace Plume.Identity.Services;

/// <summary>
/// Service for generating and validating JWT tokens.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generates an access token for the specified user.
    /// </summary>
    (string Token, DateTime Expiration) GenerateAccessToken(User user);

    /// <summary>
    /// Generates a refresh token.
    /// </summary>
    string GenerateRefreshToken();

    /// <summary>
    /// Validates an access token and returns the user ID if valid.
    /// </summary>
    Guid? ValidateAccessToken(string token);
}
