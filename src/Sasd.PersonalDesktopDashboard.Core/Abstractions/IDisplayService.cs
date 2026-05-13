using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.Core.Abstractions;

/// <summary>
/// Provides information about the currently connected displays.
/// </summary>
/// <remarks>
/// This abstraction keeps operating-system-specific monitor detection out of the Core project.
/// The WPF application can use a Windows implementation, while tests can use simple in-memory
/// display objects.
/// </remarks>
public interface IDisplayService
{
    /// <summary>
    /// Gets all currently connected displays.
    /// </summary>
    /// <returns>A list of display descriptions. Implementations should return at least one fallback display.</returns>
    IReadOnlyList<DisplayInfo> GetDisplays();

    /// <summary>
    /// Gets the primary display.
    /// </summary>
    /// <returns>The primary display, or a safe fallback if Windows reports no display.</returns>
    DisplayInfo GetPrimaryDisplay();
}
