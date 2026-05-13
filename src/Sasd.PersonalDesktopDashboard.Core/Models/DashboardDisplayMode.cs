namespace Sasd.PersonalDesktopDashboard.Core.Models;

/// <summary>
/// Describes the visual operating mode in which the dashboard is currently shown.
/// </summary>
/// <remarks>
/// The modes are intentionally business-oriented instead of implementation-oriented.
/// A later window-placement service can decide how each mode is represented on the screen.
/// </remarks>
public enum DashboardDisplayMode
{
    /// <summary>
    /// A compact sidebar-like mode for the laptop-only scenario.
    /// </summary>
    Compact = 0,

    /// <summary>
    /// The normal desktop dashboard mode for everyday work.
    /// </summary>
    Dashboard = 1,

    /// <summary>
    /// A reduced mode that shows only the most important focus information.
    /// </summary>
    Focus = 2,

    /// <summary>
    /// A large full-screen or nearly full-screen mode for a second or third monitor.
    /// </summary>
    Wallboard = 3,

    /// <summary>
    /// A quiet tray-only mode for presentations, meetings or moments where the dashboard should not be visible.
    /// </summary>
    Silent = 4
}
