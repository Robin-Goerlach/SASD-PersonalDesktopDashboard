namespace Sasd.PersonalDesktopDashboard.Core.Models;

/// <summary>
/// Describes one detected display monitor.
/// </summary>
/// <remarks>
/// The model lives in the Core project so the window placement logic can be tested without
/// depending directly on WPF, Windows Forms or operating-system APIs. The concrete detection
/// happens in the Infrastructure project.
/// </remarks>
public sealed class DisplayInfo
{
    /// <summary>
    /// Gets or initializes the technical device name reported by Windows.
    /// </summary>
    /// <example><c>\\.\DISPLAY1</c></example>
    public string DeviceName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or initializes a human-readable display name.
    /// </summary>
    public string FriendlyName { get; init; } = string.Empty;

    /// <summary>
    /// Gets or initializes a value indicating whether this is the primary Windows display.
    /// </summary>
    public bool IsPrimary { get; init; }

    /// <summary>
    /// Gets or initializes the full display bounds in virtual desktop coordinates.
    /// </summary>
    public DisplayBounds Bounds { get; init; } = new(0, 0, 0, 0);

    /// <summary>
    /// Gets or initializes the usable display area excluding taskbars and docked system bars.
    /// </summary>
    public DisplayBounds WorkingArea { get; init; } = new(0, 0, 0, 0);

    /// <summary>
    /// Gets or initializes the currently known scale factor.
    /// </summary>
    /// <remarks>
    /// V0.2 keeps this value at <c>1.0</c>. Real per-monitor DPI handling can be added later.
    /// Keeping the property now gives the model room to evolve without changing callers.
    /// </remarks>
    public double ScaleFactor { get; init; } = 1.0;

    /// <summary>
    /// Builds a simple fingerprint from stable monitor characteristics.
    /// </summary>
    /// <returns>A string that can help recognize a display setup across application starts.</returns>
    public string BuildFingerprint()
    {
        return string.Join(
            '|',
            DeviceName,
            Bounds.X,
            Bounds.Y,
            Bounds.Width,
            Bounds.Height,
            WorkingArea.X,
            WorkingArea.Y,
            WorkingArea.Width,
            WorkingArea.Height,
            IsPrimary);
    }
}
