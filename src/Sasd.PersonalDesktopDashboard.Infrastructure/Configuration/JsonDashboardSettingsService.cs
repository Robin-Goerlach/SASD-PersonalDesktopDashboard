using System.Text.Json;
using Sasd.PersonalDesktopDashboard.Core.Abstractions;
using Sasd.PersonalDesktopDashboard.Core.Configuration;

namespace Sasd.PersonalDesktopDashboard.Infrastructure.Configuration;

/// <summary>
/// Stores dashboard settings as a small JSON file in the user's profile.
/// </summary>
public sealed class JsonDashboardSettingsService : IDashboardSettingsService
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true
    };

    private readonly string _settingsFilePath;

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonDashboardSettingsService"/> class.
    /// </summary>
    /// <param name="settingsFilePath">The full path to the JSON settings file.</param>
    public JsonDashboardSettingsService(string settingsFilePath)
    {
        _settingsFilePath = settingsFilePath;
    }

    /// <inheritdoc />
    public async Task<DashboardSettings> LoadAsync(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(_settingsFilePath))
        {
            // Return safe defaults if the user has not configured the dashboard yet.
            return new DashboardSettings();
        }

        await using var stream = File.OpenRead(_settingsFilePath);
        var settings = await JsonSerializer.DeserializeAsync<DashboardSettings>(
            stream,
            SerializerOptions,
            cancellationToken);

        return settings ?? new DashboardSettings();
    }

    /// <inheritdoc />
    public async Task SaveAsync(DashboardSettings settings, CancellationToken cancellationToken = default)
    {
        var directory = Path.GetDirectoryName(_settingsFilePath);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await using var stream = File.Create(_settingsFilePath);
        await JsonSerializer.SerializeAsync(stream, settings, SerializerOptions, cancellationToken);
    }
}
