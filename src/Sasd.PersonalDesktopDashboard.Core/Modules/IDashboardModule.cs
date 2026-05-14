using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.Core.Modules;

/// <summary>
/// Defines one internal dashboard module that can produce one dashboard widget card.
/// </summary>
/// <remarks>
/// This is deliberately not a plugin contract yet. The interface is meant for modules that are
/// compiled into the application solution. A later plugin system can build on similar ideas,
/// but V0.x keeps the loading model explicit and easy to debug.
/// </remarks>
public interface IDashboardModule
{
    /// <summary>
    /// Gets the stable technical module identifier.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets the human-readable module name used in log messages and diagnostics.
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// Gets the order in which the module should appear on the dashboard.
    /// </summary>
    int SortOrder { get; }

    /// <summary>
    /// Determines whether this module should be shown in the requested display mode.
    /// </summary>
    /// <param name="displayMode">The display mode requested by the application.</param>
    /// <returns><see langword="true" /> if the module should be executed; otherwise <see langword="false" />.</returns>
    bool IsVisibleIn(DashboardDisplayMode displayMode);

    /// <summary>
    /// Builds the widget card produced by this module.
    /// </summary>
    /// <param name="context">Context information shared with all dashboard modules for this snapshot.</param>
    /// <param name="cancellationToken">Token used to cancel the module operation.</param>
    /// <returns>The widget card to show, or <see langword="null" /> if the module has nothing to display.</returns>
    Task<DashboardWidgetModel?> BuildWidgetAsync(
        DashboardModuleContext context,
        CancellationToken cancellationToken = default);
}
