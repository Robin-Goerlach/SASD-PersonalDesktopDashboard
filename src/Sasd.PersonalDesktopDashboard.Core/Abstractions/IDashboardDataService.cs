using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.Core.Abstractions;

/// <summary>
/// Provides the dashboard UI with a complete renderable snapshot.
/// </summary>
public interface IDashboardDataService
{
    /// <summary>
    /// Builds or loads the current dashboard snapshot for the requested display mode.
    /// </summary>
    /// <param name="displayMode">The display mode for which data should be prepared.</param>
    /// <param name="cancellationToken">Token used to cancel the asynchronous operation.</param>
    /// <returns>A snapshot containing all widgets that should be displayed.</returns>
    Task<DashboardSnapshot> GetDashboardSnapshotAsync(
        DashboardDisplayMode displayMode,
        CancellationToken cancellationToken = default);
}
