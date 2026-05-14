using System;
using System.IO;
using Sasd.PersonalDesktopDashboard.Core.Logging;
using Sasd.PersonalDesktopDashboard.Infrastructure.Configuration;
using Sasd.PersonalDesktopDashboard.Infrastructure.Logging;

namespace Sasd.PersonalDesktopDashboard.App.Diagnostics;

/// <summary>
/// Writes startup diagnostics for the dashboard's user-specific application data files.
/// </summary>
/// <remarks>
/// This helper is intentionally placed in the WPF App project because it is mainly a
/// startup diagnostic concern. It does not change settings, logging, or window-placement
/// behavior. It only records the effective paths that the already configured services use.
/// </remarks>
public static class AppDataDiagnostics
{
    /// <summary>
    /// Logs the most important AppData, settings, placement, and log file paths.
    /// </summary>
    /// <param name="logger">The application logger used for diagnostic output.</param>
    /// <param name="loggerOptions">The effective file logger options used by the application.</param>
    /// <exception cref="ArgumentNullException">
    /// Thrown when <paramref name="logger" /> or <paramref name="loggerOptions" /> is <c>null</c>.
    /// </exception>
    public static void LogStartupPaths(IAppLogger logger, FileAppLoggerOptions loggerOptions)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(loggerOptions);

        // Keep the individual log lines simple. That makes them easy to search in app.log
        // and avoids creating a large multi-line block every time the application starts.
        logger.Info("Application data diagnostics started.");
        logger.Info($"Process ID: {Environment.ProcessId}.");
        logger.Info($"Runtime version: {Environment.Version}.");
        logger.Info($"Operating system: {Environment.OSVersion.VersionString}.");
        logger.Info($"User interactive session: {Environment.UserInteractive}.");

        string roamingAppDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string applicationDataDirectory = DefaultDashboardPaths.GetApplicationDataDirectory();
        string settingsFilePath = DefaultDashboardPaths.GetSettingsFilePath();
        string windowPlacementFilePath = DefaultDashboardPaths.GetWindowPlacementFilePath();
        string logFilePath = loggerOptions.LogFilePath;
        string logDirectoryPath = Path.GetDirectoryName(logFilePath) ?? string.Empty;

        logger.Info($"Roaming AppData directory: {FormatPath(roamingAppDataDirectory)}");
        logger.Info($"Dashboard AppData directory: {FormatPath(applicationDataDirectory)}");
        logger.Info($"Dashboard settings file: {FormatPath(settingsFilePath)}");
        logger.Info($"Dashboard window placement file: {FormatPath(windowPlacementFilePath)}");
        logger.Info($"Dashboard log directory: {FormatPath(logDirectoryPath)}");
        logger.Info($"Dashboard log file: {FormatPath(logFilePath)}");

        // These existence checks are useful when diagnosing first-start behavior on a
        // new machine. They are deliberately informational only and do not create files.
        logger.Info($"Dashboard AppData directory exists: {Directory.Exists(applicationDataDirectory)}.");
        logger.Info($"Dashboard settings file exists: {File.Exists(settingsFilePath)}.");
        logger.Info($"Dashboard window placement file exists: {File.Exists(windowPlacementFilePath)}.");
        logger.Info($"Dashboard log file exists before current write completes: {File.Exists(logFilePath)}.");
        logger.Info("Application data diagnostics completed.");
    }

    /// <summary>
    /// Formats a filesystem path for log output.
    /// </summary>
    /// <param name="path">The path that should be written to the log file.</param>
    /// <returns>A readable path value or a clear placeholder for missing values.</returns>
    private static string FormatPath(string path)
    {
        // Empty values are unlikely here, but a visible placeholder is easier to
        // understand in a log file than a blank line after the colon.
        return string.IsNullOrWhiteSpace(path) ? "(empty path)" : path;
    }
}
