using Sasd.PersonalDesktopDashboard.Core.Abstractions;
using Sasd.PersonalDesktopDashboard.Core.Models;
using Forms = System.Windows.Forms;

namespace Sasd.PersonalDesktopDashboard.Infrastructure.Windows;

/// <summary>
/// Windows implementation of <see cref="IDisplayService"/> based on <see cref="Forms.Screen"/>.
/// </summary>
/// <remarks>
/// <see cref="Forms.Screen"/> gives us a reliable and simple first step for monitor detection.
/// More advanced DPI handling can be added later through Win32 APIs if the project needs it.
/// </remarks>
public sealed class WindowsDisplayService : IDisplayService
{
    /// <inheritdoc />
    public IReadOnlyList<DisplayInfo> GetDisplays()
    {
        var displays = Forms.Screen.AllScreens
            .Select(MapScreen)
            .OrderByDescending(display => display.IsPrimary)
            .ThenBy(display => display.Bounds.X)
            .ThenBy(display => display.Bounds.Y)
            .ToArray();

        // A normal Windows desktop should always report at least one screen. The fallback keeps
        // the rest of the application safe even if monitor detection behaves unexpectedly.
        return displays.Length > 0 ? displays : new[] { CreateFallbackDisplay() };
    }

    /// <inheritdoc />
    public DisplayInfo GetPrimaryDisplay()
    {
        var displays = GetDisplays();
        return displays.FirstOrDefault(display => display.IsPrimary) ?? displays[0];
    }

    private static DisplayInfo MapScreen(Forms.Screen screen)
    {
        return new DisplayInfo
        {
            DeviceName = screen.DeviceName,
            FriendlyName = screen.DeviceName,
            IsPrimary = screen.Primary,
            Bounds = new DisplayBounds(
                screen.Bounds.Left,
                screen.Bounds.Top,
                screen.Bounds.Width,
                screen.Bounds.Height),
            WorkingArea = new DisplayBounds(
                screen.WorkingArea.Left,
                screen.WorkingArea.Top,
                screen.WorkingArea.Width,
                screen.WorkingArea.Height),
            ScaleFactor = 1.0
        };
    }

    private static DisplayInfo CreateFallbackDisplay()
    {
        return new DisplayInfo
        {
            DeviceName = "FallbackDisplay",
            FriendlyName = "Fallback display",
            IsPrimary = true,
            Bounds = new DisplayBounds(0, 0, 1280, 720),
            WorkingArea = new DisplayBounds(0, 0, 1280, 720),
            ScaleFactor = 1.0
        };
    }
}
