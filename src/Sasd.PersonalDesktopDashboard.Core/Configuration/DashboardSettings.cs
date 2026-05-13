using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.Core.Configuration;

/// <summary>
/// Represents user-configurable dashboard settings.
/// </summary>
public sealed class DashboardSettings
{
    /// <summary>
    /// Gets or sets the currently preferred display mode.
    /// </summary>
    public DashboardDisplayMode PreferredDisplayMode { get; set; } = DashboardDisplayMode.Dashboard;

    /// <summary>
    /// Gets or sets a value indicating whether privacy mode should be enabled by default.
    /// </summary>
    public bool PrivacyModeEnabled { get; set; }

    /// <summary>
    /// Gets or sets the automatic refresh interval in seconds.
    /// </summary>
    /// <remarks>
    /// V0.1 does not start a background refresh timer yet. The value is stored now so the
    /// later implementation can use the same settings format.
    /// </remarks>
    public int RefreshIntervalSeconds { get; set; } = 300;

    /// <summary>
    /// Gets or sets the UI theme name.
    /// </summary>
    public string ThemeName { get; set; } = "SASD Dark";

    /// <summary>
    /// Gets or sets the configured display profiles.
    /// </summary>
    public List<DisplayProfile> DisplayProfiles { get; set; } =
    [
        new DisplayProfile
        {
            Name = "Laptop unterwegs",
            MinMonitorCount = 1,
            MaxMonitorCount = 1,
            PreferredDisplayMode = DashboardDisplayMode.Compact,
            PreferredMonitorHint = "primary"
        },
        new DisplayProfile
        {
            Name = "Büro / Dockingstation",
            MinMonitorCount = 2,
            MaxMonitorCount = null,
            PreferredDisplayMode = DashboardDisplayMode.Wallboard,
            PreferredMonitorHint = "secondary"
        }
    ];
}
