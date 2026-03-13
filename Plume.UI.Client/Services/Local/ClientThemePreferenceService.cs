using Microsoft.JSInterop;
using Plume.UI.Client.Contracts.Local;

namespace Plume.UI.Client.Services.Local;

public class ClientThemePreferenceService(IJSRuntime js) : IThemePreferenceService
{
    public async Task<bool> GetDarkModeAsync()
    {
        var value = await js.InvokeAsync<string?>("getCookie", "darkMode");
        return value == "true";
    }

    public async Task SetDarkModeAsync(bool isDark)
    {
        await js.InvokeVoidAsync("setCookie", "darkMode", isDark.ToString().ToLower(), 365);
    }
}
