using Sasd.PersonalDesktopDashboard.Core.Configuration;

namespace Sasd.PersonalDesktopDashboard.Core.Abstractions;

/// <summary>
/// Loads and saves dashboard configuration.
/// </summary>
public interface IDashboardSettingsService
{
    /// <summary>
    /// Loads the settings. Implementations should return safe defaults if no settings file exists yet.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    /// <returns>The loaded or default settings.</returns>
    Task<DashboardSettings> LoadAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves the given settings.
    /// </summary>
    /// <param name="settings">The settings object to persist.</param>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    Task SaveAsync(DashboardSettings settings, CancellationToken cancellationToken = default);
}
