using Plume.UI.Client.Contracts.Local;

namespace Plume.UI.Services;

public class ServerThemePreferenceService(IHttpContextAccessor httpContextAccessor) : IThemePreferenceService
{
    public Task<bool> GetDarkModeAsync()
    {
        var cookie = httpContextAccessor.HttpContext?.Request.Cookies["darkMode"];
        return Task.FromResult(cookie == "true");
    }

    public Task SetDarkModeAsync(bool isDark) => Task.CompletedTask;
}
