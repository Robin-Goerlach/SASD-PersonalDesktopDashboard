namespace Sasd.PersonalDesktopDashboard.Core.Models;

/// <summary>
/// Describes how the dashboard should behave for a class of display setups.
/// </summary>
/// <remarks>
/// V0.1 does not yet implement full monitor detection. The model is added now so the
/// settings file and later monitor-profile logic have a stable place in the domain.
/// </remarks>
public sealed class DisplayProfile
{
    /// <summary>
    /// Gets or initializes the user-facing profile name.
    /// </summary>
    public string Name { get; init; } = "Default";

    /// <summary>
    /// Gets or initializes the minimum number of connected monitors for this profile.
    /// </summary>
    public int MinMonitorCount { get; init; } = 1;

    /// <summary>
    /// Gets or initializes the maximum number of connected monitors for this profile.
    /// A value of <c>null</c> means there is no upper limit.
    /// </summary>
    public int? MaxMonitorCount { get; init; }

    /// <summary>
    /// Gets or initializes the default display mode when this profile matches.
    /// </summary>
    public DashboardDisplayMode PreferredDisplayMode { get; init; } = DashboardDisplayMode.Dashboard;

    /// <summary>
    /// Gets or initializes a simple preferred monitor hint such as <c>primary</c>,
    /// <c>secondary</c> or <c>last</c>.
    /// </summary>
    public string PreferredMonitorHint { get; init; } = "primary";
}
