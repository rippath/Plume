namespace Plume.UI.Client.Contracts.Local;

public interface IThemePreferenceService
{
    Task<bool> GetDarkModeAsync();
    Task SetDarkModeAsync(bool isDark);
}
