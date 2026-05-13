namespace Sasd.PersonalDesktopDashboard.Core.Models;

/// <summary>
/// Represents the small subset of window states the dashboard persists.
/// </summary>
/// <remarks>
/// The Core project deliberately does not reference WPF. This enum mirrors the states that
/// are safe to remember across application starts. A minimized window is intentionally not
/// persisted because a dashboard should not re-open invisibly.
/// </remarks>
public enum DashboardWindowState
{
    /// <summary>
    /// The dashboard window is shown as a normal resizable window.
    /// </summary>
    Normal = 0,

    /// <summary>
    /// The dashboard window is maximized on its current display.
    /// </summary>
    Maximized = 1
}
