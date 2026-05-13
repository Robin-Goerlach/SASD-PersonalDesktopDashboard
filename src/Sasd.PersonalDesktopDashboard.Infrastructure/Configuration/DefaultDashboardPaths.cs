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
        return Path.Combine(GetApplicationDataDirectory(), "dashboard.settings.json");
    }

    /// <summary>
    /// Gets the default window placement file path under the current user's application data folder.
    /// </summary>
    /// <returns>A full path to the window placement JSON file.</returns>
    public static string GetWindowPlacementFilePath()
    {
        // Window placement is intentionally stored in a separate file. This keeps frequently
        // changing UI state away from more stable user configuration such as theme or profiles.
        return Path.Combine(GetApplicationDataDirectory(), "window-placement.json");
    }

    /// <summary>
    /// Gets the application-specific AppData directory.
    /// </summary>
    /// <returns>The full path to the dashboard AppData folder.</returns>
    public static string GetApplicationDataDirectory()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        return Path.Combine(
            appData,
            "SASD",
            "PersonalDesktopDashboard");
    }
}
