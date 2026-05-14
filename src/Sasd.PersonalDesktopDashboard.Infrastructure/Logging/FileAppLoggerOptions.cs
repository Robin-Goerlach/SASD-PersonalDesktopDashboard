using System;
using System.IO;

namespace Sasd.PersonalDesktopDashboard.Infrastructure.Logging;

/// <summary>
/// Contains configuration values for <see cref="FileAppLogger"/>.
/// </summary>
/// <remarks>
/// The default configuration writes to the roaming AppData folder of the current
/// Windows user. This keeps log files out of the installation directory and works
/// well for normal desktop applications without administrator permissions.
/// </remarks>
public sealed class FileAppLoggerOptions
{
    /// <summary>
    /// Gets or sets the full path of the log file that should be written.
    /// </summary>
    public string LogFilePath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the maximum size of the active log file in bytes before rotation is attempted.
    /// </summary>
    /// <remarks>
    /// The value is intentionally modest because this dashboard should not grow
    /// unlimited diagnostic files in the user profile. Five megabytes are more
    /// than enough for the current technical shell phase.
    /// </remarks>
    public long MaxFileSizeBytes { get; set; } = 5 * 1024 * 1024;

    /// <summary>
    /// Gets or sets how many rotated log files should be retained.
    /// </summary>
    public int RetainedFileCount { get; set; } = 3;

    /// <summary>
    /// Creates the default logger options for the SASD Personal Desktop Dashboard.
    /// </summary>
    /// <returns>Default file logger options using the application's AppData log directory.</returns>
    public static FileAppLoggerOptions CreateDefault()
    {
        // Roaming AppData is appropriate for user-specific application state.
        // The path normally resolves to:
        // %APPDATA%\SASD\PersonalDesktopDashboard\logs\app.log
        string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string logDirectory = Path.Combine(appDataDirectory, "SASD", "PersonalDesktopDashboard", "logs");

        return new FileAppLoggerOptions
        {
            LogFilePath = Path.Combine(logDirectory, "app.log"),
        };
    }
}
