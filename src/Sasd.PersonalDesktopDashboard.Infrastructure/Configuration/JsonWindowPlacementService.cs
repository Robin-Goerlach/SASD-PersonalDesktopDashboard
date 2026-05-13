using System.Text.Json;
using Sasd.PersonalDesktopDashboard.Core.Abstractions;
using Sasd.PersonalDesktopDashboard.Core.Configuration;
using Sasd.PersonalDesktopDashboard.Core.Services;

namespace Sasd.PersonalDesktopDashboard.Infrastructure.Configuration;

/// <summary>
/// Stores dashboard window placement as a JSON file in the user's AppData directory.
/// </summary>
/// <remarks>
/// The service deliberately validates loaded placement data before returning it. This prevents
/// the common multi-monitor problem where an application opens on a disconnected display after
/// the user undocks a laptop.
/// </remarks>
public sealed class JsonWindowPlacementService : IWindowPlacementService
{
    private readonly string _filePath;
    private readonly IDisplayService _displayService;
    private readonly JsonSerializerOptions _serializerOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="JsonWindowPlacementService"/> class.
    /// </summary>
    /// <param name="filePath">The JSON file path used for persistence.</param>
    /// <param name="displayService">Service that reports currently connected displays.</param>
    public JsonWindowPlacementService(string filePath, IDisplayService displayService)
    {
        _filePath = filePath;
        _displayService = displayService;
    }

    /// <inheritdoc />
    public async Task<WindowPlacementSettings> LoadOrCreateValidPlacementAsync(
        double defaultWidth,
        double defaultHeight,
        CancellationToken cancellationToken = default)
    {
        WindowPlacementSettings? loadedPlacement = null;

        if (File.Exists(_filePath))
        {
            try
            {
                await using var stream = File.OpenRead(_filePath);
                loadedPlacement = await JsonSerializer.DeserializeAsync<WindowPlacementSettings>(
                    stream,
                    _serializerOptions,
                    cancellationToken);
            }
            catch (JsonException)
            {
                // A corrupted placement file should not prevent the dashboard from starting.
                // We simply ignore the file and let the validator create a safe default.
                loadedPlacement = null;
            }
            catch (IOException)
            {
                // IO errors are treated the same way in V0.2: start visibly, do not crash.
                loadedPlacement = null;
            }
        }

        var displays = _displayService.GetDisplays();

        return WindowPlacementValidator.NormalizeOrCreateDefault(
            loadedPlacement,
            displays,
            defaultWidth,
            defaultHeight);
    }

    /// <inheritdoc />
    public async Task SavePlacementAsync(
        WindowPlacementSettings placement,
        CancellationToken cancellationToken = default)
    {
        var displays = _displayService.GetDisplays();
        var matchingDisplay = WindowPlacementValidator.FindBestMatchingDisplay(placement, displays);

        // Store monitor metadata together with the coordinates. This is useful for diagnostics
        // and prepares the later monitor-profile feature without implementing it prematurely.
        placement.DisplayDeviceName = matchingDisplay?.DeviceName;
        placement.DisplayFingerprint = matchingDisplay?.BuildFingerprint();
        placement.SavedAtUtc = DateTime.UtcNow;

        var directory = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        await using var stream = File.Create(_filePath);
        await JsonSerializer.SerializeAsync(
            stream,
            placement,
            _serializerOptions,
            cancellationToken);
    }
}
