namespace Sasd.PersonalDesktopDashboard.Core.Models;

/// <summary>
/// Represents one complete data snapshot that can be rendered by the dashboard UI.
/// </summary>
public sealed class DashboardSnapshot
{
    /// <summary>
    /// Gets or initializes the local timestamp at which this snapshot was generated.
    /// </summary>
    public DateTime GeneratedAtLocal { get; init; } = DateTime.Now;

    /// <summary>
    /// Gets or initializes the display mode for which this snapshot was prepared.
    /// </summary>
    public DashboardDisplayMode DisplayMode { get; init; } = DashboardDisplayMode.Dashboard;

    /// <summary>
    /// Gets or initializes the widget cards that should be shown.
    /// </summary>
    public IReadOnlyList<DashboardWidgetModel> Widgets { get; init; } = Array.Empty<DashboardWidgetModel>();
}
