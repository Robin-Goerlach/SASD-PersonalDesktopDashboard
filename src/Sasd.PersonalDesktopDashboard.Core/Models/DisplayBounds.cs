namespace Sasd.PersonalDesktopDashboard.Core.Models;

/// <summary>
/// Represents a rectangular area on the virtual Windows desktop.
/// </summary>
/// <remarks>
/// Windows treats all connected monitors as one larger virtual desktop. Coordinates may be
/// negative when a monitor is positioned left of or above the primary monitor. This small
/// value object therefore deliberately stores X/Y coordinates instead of assuming that the
/// upper-left corner is always <c>0,0</c>.
/// </remarks>
public sealed record DisplayBounds(double X, double Y, double Width, double Height)
{
    /// <summary>
    /// Gets the right edge of the rectangle.
    /// </summary>
    public double Right => X + Width;

    /// <summary>
    /// Gets the lower edge of the rectangle.
    /// </summary>
    public double Bottom => Y + Height;

    /// <summary>
    /// Gets the horizontal center coordinate.
    /// </summary>
    public double CenterX => X + Width / 2.0;

    /// <summary>
    /// Gets the vertical center coordinate.
    /// </summary>
    public double CenterY => Y + Height / 2.0;

    /// <summary>
    /// Calculates how many square pixels of another rectangle overlap with this rectangle.
    /// </summary>
    /// <param name="x">The left coordinate of the other rectangle.</param>
    /// <param name="y">The top coordinate of the other rectangle.</param>
    /// <param name="width">The width of the other rectangle.</param>
    /// <param name="height">The height of the other rectangle.</param>
    /// <returns>The overlapping area in square pixels. Returns <c>0</c> when there is no overlap.</returns>
    public double GetIntersectionArea(double x, double y, double width, double height)
    {
        var intersectionWidth = Math.Max(0, Math.Min(Right, x + width) - Math.Max(X, x));
        var intersectionHeight = Math.Max(0, Math.Min(Bottom, y + height) - Math.Max(Y, y));

        return intersectionWidth * intersectionHeight;
    }

    /// <summary>
    /// Calculates the visible width of another rectangle inside this bounds object.
    /// </summary>
    /// <param name="x">The left coordinate of the other rectangle.</param>
    /// <param name="width">The width of the other rectangle.</param>
    /// <returns>The overlapping width in pixels.</returns>
    public double GetIntersectionWidth(double x, double width)
    {
        return Math.Max(0, Math.Min(Right, x + width) - Math.Max(X, x));
    }

    /// <summary>
    /// Calculates the visible height of another rectangle inside this bounds object.
    /// </summary>
    /// <param name="y">The top coordinate of the other rectangle.</param>
    /// <param name="height">The height of the other rectangle.</param>
    /// <returns>The overlapping height in pixels.</returns>
    public double GetIntersectionHeight(double y, double height)
    {
        return Math.Max(0, Math.Min(Bottom, y + height) - Math.Max(Y, y));
    }
}
