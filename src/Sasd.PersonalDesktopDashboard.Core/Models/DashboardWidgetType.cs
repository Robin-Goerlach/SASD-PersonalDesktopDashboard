namespace Sasd.PersonalDesktopDashboard.Core.Models;

/// <summary>
/// Identifies the functional purpose of a dashboard widget.
/// </summary>
public enum DashboardWidgetType
{
    /// <summary>Weather and forecast information.</summary>
    Weather,

    /// <summary>Tasks, to-do items and daily priorities.</summary>
    Tasks,

    /// <summary>Calendar events and upcoming appointments.</summary>
    Calendar,

    /// <summary>Local, world, technology or security news headlines.</summary>
    News,

    /// <summary>Local system state such as battery, CPU, memory or disk usage.</summary>
    SystemStatus,

    /// <summary>SASD project status, repositories, open work or release information.</summary>
    SasdProjects,

    /// <summary>General notes, daily focus or future custom widgets.</summary>
    Notes
}
