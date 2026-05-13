using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.Core.Configuration;

/// <summary>
/// Stores the last known position and size of the dashboard window.
/// </summary>
/// <remarks>
/// The values are stored in virtual desktop coordinates. That is important for multi-monitor
/// systems because a secondary monitor can have negative coordinates when it is placed to the
/// left or above the primary display in Windows display settings.
/// </remarks>
public sealed class WindowPlacementSettings
{
    /// <summary>
    /// Gets or sets the left coordinate of the saved window rectangle.
    /// </summary>
    public double Left { get; set; }

    /// <summary>
    /// Gets or sets the top coordinate of the saved window rectangle.
    /// </summary>
    public double Top { get; set; }

    /// <summary>
    /// Gets or sets the saved window width.
    /// </summary>
    public double Width { get; set; } = 1280;

    /// <summary>
    /// Gets or sets the saved window height.
    /// </summary>
    public double Height { get; set; } = 760;

    /// <summary>
    /// Gets or sets the window state to restore.
    /// </summary>
    public DashboardWindowState WindowState { get; set; } = DashboardWindowState.Normal;

    /// <summary>
    /// Gets or sets the Windows display device name on which the window was last seen.
    /// </summary>
    public string? DisplayDeviceName { get; set; }

    /// <summary>
    /// Gets or sets a display fingerprint to help diagnose or later match monitor profiles.
    /// </summary>
    public string? DisplayFingerprint { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when this placement was saved.
    /// </summary>
    public DateTime SavedAtUtc { get; set; } = DateTime.UtcNow;
}
