using Sasd.PersonalDesktopDashboard.Core.Configuration;
using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.Core.Services;

/// <summary>
/// Validates and normalizes saved dashboard window positions.
/// </summary>
/// <remarks>
/// This class solves an important multi-monitor problem: a user may save the dashboard on a
/// docking-station monitor and later start the laptop without that monitor. In that case the
/// old coordinates may be outside the visible desktop. The validator moves such windows back
/// to a safe area instead of letting the application start invisibly.
/// </remarks>
public static class WindowPlacementValidator
{
    private const double MinimumWindowWidth = 640;
    private const double MinimumWindowHeight = 420;
    private const double MinimumVisibleWidth = 160;
    private const double MinimumVisibleHeight = 120;

    /// <summary>
    /// Normalizes a saved placement for the currently connected displays.
    /// </summary>
    /// <param name="savedPlacement">The placement read from disk, or <c>null</c> when no file exists.</param>
    /// <param name="displays">The currently connected displays.</param>
    /// <param name="defaultWidth">Preferred fallback width.</param>
    /// <param name="defaultHeight">Preferred fallback height.</param>
    /// <returns>A safe placement that is visible on one of the current displays.</returns>
    public static WindowPlacementSettings NormalizeOrCreateDefault(
        WindowPlacementSettings? savedPlacement,
        IReadOnlyList<DisplayInfo> displays,
        double defaultWidth,
        double defaultHeight)
    {
        IReadOnlyList<DisplayInfo> displayList = displays.Count > 0
            ? displays
            : new[] { CreateFallbackDisplay() };

        var primaryDisplay = GetPrimaryDisplay(displayList);

        if (savedPlacement is null)
        {
            return CenterOnDisplay(primaryDisplay, defaultWidth, defaultHeight);
        }

        var normalized = NormalizeSize(savedPlacement, displayList, primaryDisplay, defaultWidth, defaultHeight);

        if (IsReasonablyVisible(normalized, displayList))
        {
            // The saved window is still usable. Keep it where the user left it.
            return normalized;
        }

        // The old position is probably on a disconnected monitor. Move the dashboard to a
        // predictable position on the primary display so the user can see it immediately.
        return CenterOnDisplay(primaryDisplay, defaultWidth, defaultHeight);
    }

    /// <summary>
    /// Finds the display that contains the largest part of a window placement.
    /// </summary>
    /// <param name="placement">The placement to match.</param>
    /// <param name="displays">The available displays.</param>
    /// <returns>The best matching display, or <c>null</c> when no display is available.</returns>
    public static DisplayInfo? FindBestMatchingDisplay(
        WindowPlacementSettings placement,
        IReadOnlyList<DisplayInfo> displays)
    {
        DisplayInfo? bestDisplay = null;
        var bestArea = 0.0;

        foreach (var display in displays)
        {
            var area = display.WorkingArea.GetIntersectionArea(
                placement.Left,
                placement.Top,
                placement.Width,
                placement.Height);

            if (area > bestArea)
            {
                bestArea = area;
                bestDisplay = display;
            }
        }

        return bestDisplay ?? displays.FirstOrDefault(display => display.IsPrimary) ?? displays.FirstOrDefault();
    }

    private static WindowPlacementSettings NormalizeSize(
        WindowPlacementSettings placement,
        IReadOnlyList<DisplayInfo> displays,
        DisplayInfo fallbackDisplay,
        double defaultWidth,
        double defaultHeight)
    {
        var targetDisplay = FindBestMatchingDisplay(placement, displays) ?? fallbackDisplay;
        var workingArea = targetDisplay.WorkingArea;

        // Keep the size within sensible bounds. This prevents corrupted settings from creating
        // a tiny or enormous window. The WPF window still has its own MinWidth/MinHeight as a
        // second line of defense.
        var width = Clamp(
            IsUsableNumber(placement.Width) ? placement.Width : defaultWidth,
            MinimumWindowWidth,
            Math.Max(MinimumWindowWidth, workingArea.Width));

        var height = Clamp(
            IsUsableNumber(placement.Height) ? placement.Height : defaultHeight,
            MinimumWindowHeight,
            Math.Max(MinimumWindowHeight, workingArea.Height));

        return new WindowPlacementSettings
        {
            Left = IsUsableNumber(placement.Left) ? placement.Left : workingArea.X,
            Top = IsUsableNumber(placement.Top) ? placement.Top : workingArea.Y,
            Width = width,
            Height = height,
            WindowState = placement.WindowState,
            DisplayDeviceName = placement.DisplayDeviceName,
            DisplayFingerprint = placement.DisplayFingerprint,
            SavedAtUtc = placement.SavedAtUtc
        };
    }

    private static WindowPlacementSettings CenterOnDisplay(
        DisplayInfo display,
        double defaultWidth,
        double defaultHeight)
    {
        var workingArea = display.WorkingArea;
        var width = Math.Min(Math.Max(defaultWidth, MinimumWindowWidth), workingArea.Width);
        var height = Math.Min(Math.Max(defaultHeight, MinimumWindowHeight), workingArea.Height);

        return new WindowPlacementSettings
        {
            Left = workingArea.X + (workingArea.Width - width) / 2.0,
            Top = workingArea.Y + (workingArea.Height - height) / 2.0,
            Width = width,
            Height = height,
            WindowState = DashboardWindowState.Normal,
            DisplayDeviceName = display.DeviceName,
            DisplayFingerprint = display.BuildFingerprint(),
            SavedAtUtc = DateTime.UtcNow
        };
    }

    private static bool IsReasonablyVisible(
        WindowPlacementSettings placement,
        IReadOnlyList<DisplayInfo> displays)
    {
        foreach (var display in displays)
        {
            var visibleWidth = display.WorkingArea.GetIntersectionWidth(placement.Left, placement.Width);
            var visibleHeight = display.WorkingArea.GetIntersectionHeight(placement.Top, placement.Height);

            if (visibleWidth >= MinimumVisibleWidth && visibleHeight >= MinimumVisibleHeight)
            {
                return true;
            }
        }

        return false;
    }

    private static DisplayInfo GetPrimaryDisplay(IReadOnlyList<DisplayInfo> displays)
    {
        return displays.FirstOrDefault(display => display.IsPrimary) ?? displays[0];
    }

    private static DisplayInfo CreateFallbackDisplay()
    {
        return new DisplayInfo
        {
            DeviceName = "FallbackDisplay",
            FriendlyName = "Fallback display",
            IsPrimary = true,
            Bounds = new DisplayBounds(0, 0, 1280, 720),
            WorkingArea = new DisplayBounds(0, 0, 1280, 720)
        };
    }

    private static bool IsUsableNumber(double value)
    {
        return !double.IsNaN(value) && !double.IsInfinity(value);
    }

    private static double Clamp(double value, double min, double max)
    {
        return Math.Min(Math.Max(value, min), max);
    }
}
