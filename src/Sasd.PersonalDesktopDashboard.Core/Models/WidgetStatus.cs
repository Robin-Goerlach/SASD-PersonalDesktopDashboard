namespace Sasd.PersonalDesktopDashboard.Core.Models;

/// <summary>
/// Describes the current health or attention level of a dashboard widget.
/// </summary>
public enum WidgetStatus
{
    /// <summary>Everything is normal.</summary>
    Normal = 0,

    /// <summary>The widget contains useful information but no problem.</summary>
    Info = 1,

    /// <summary>The widget needs attention, but it is not urgent.</summary>
    Warning = 2,

    /// <summary>The widget reports a critical condition.</summary>
    Critical = 3,

    /// <summary>The widget is configured but currently disabled or unavailable.</summary>
    Disabled = 4
}
