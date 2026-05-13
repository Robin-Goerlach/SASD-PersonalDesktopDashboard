namespace Sasd.PersonalDesktopDashboard.Infrastructure.Configuration;

/// <summary>
/// Provides filesystem paths used by the dashboard application.
/// </summary>
public static class DefaultDashboardPaths
{
    /// <summary>
    /// Gets the default settings file path under the current user's application data folder.
    /// </summary>
    /// <returns>A full path to the dashboard settings JSON file.</returns>
    public static string GetSettingsFilePath()
    {
        // Use AppData/Roaming instead of the repository directory so personal settings
        // are not accidentally committed to GitHub.
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        return Path.Combine(
            appData,
            "SASD",
            "PersonalDesktopDashboard",
            "dashboard.settings.json");
    }
}
