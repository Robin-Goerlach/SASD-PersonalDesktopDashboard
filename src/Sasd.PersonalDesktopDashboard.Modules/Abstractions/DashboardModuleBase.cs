using Sasd.PersonalDesktopDashboard.Core.Models;
using Sasd.PersonalDesktopDashboard.Core.Modules;

namespace Sasd.PersonalDesktopDashboard.Modules.Abstractions;

/// <summary>
/// Provides common behavior for simple built-in dashboard modules.
/// </summary>
/// <remarks>
/// The base class keeps the individual placeholder modules short and readable.
/// It is intentionally small and does not hide important behavior behind a complex framework.
/// </remarks>
public abstract class DashboardModuleBase : IDashboardModule
{
    /// <inheritdoc />
    public abstract string Id { get; }

    /// <inheritdoc />
    public abstract string DisplayName { get; }

    /// <inheritdoc />
    public virtual int SortOrder => 1000;

    /// <summary>
    /// Gets a value indicating whether this module is useful in compact mode.
    /// </summary>
    /// <remarks>
    /// V0.4 introduced compact mode as a small-window view. V0.5 keeps the existing
    /// behavior that only the most relevant cards are displayed there.
    /// </remarks>
    protected virtual bool SupportsCompactMode => false;

    /// <inheritdoc />
    public virtual bool IsVisibleIn(DashboardDisplayMode displayMode)
    {
        // Compact mode is intentionally more restrictive than the normal dashboard.
        // All other modes still show all internal placeholder modules until we add
        // more advanced layout or personalization rules.
        return displayMode != DashboardDisplayMode.Compact || SupportsCompactMode;
    }

    /// <inheritdoc />
    public abstract Task<DashboardWidgetModel?> BuildWidgetAsync(
        DashboardModuleContext context,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Wraps a synchronously created widget into a completed task.
    /// </summary>
    /// <param name="widget">The widget produced by the module.</param>
    /// <returns>A completed task containing the widget.</returns>
    protected static Task<DashboardWidgetModel?> Completed(DashboardWidgetModel widget)
    {
        return Task.FromResult<DashboardWidgetModel?>(widget);
    }
}
