using Sasd.PersonalDesktopDashboard.Core.Configuration;

namespace Sasd.PersonalDesktopDashboard.Core.Abstractions;

/// <summary>
/// Loads, validates and saves the dashboard window placement.
/// </summary>
public interface IWindowPlacementService
{
    /// <summary>
    /// Loads the last placement and guarantees that the returned placement is visible on a current display.
    /// </summary>
    /// <param name="defaultWidth">Preferred fallback width when no valid placement exists.</param>
    /// <param name="defaultHeight">Preferred fallback height when no valid placement exists.</param>
    /// <param name="cancellationToken">Token used to cancel asynchronous file IO.</param>
    /// <returns>A placement that can safely be applied to the dashboard window.</returns>
    Task<WindowPlacementSettings> LoadOrCreateValidPlacementAsync(
        double defaultWidth,
        double defaultHeight,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves the current dashboard window placement.
    /// </summary>
    /// <param name="placement">The placement to persist.</param>
    /// <param name="cancellationToken">Token used to cancel asynchronous file IO.</param>
    Task SavePlacementAsync(
        WindowPlacementSettings placement,
        CancellationToken cancellationToken = default);
}
