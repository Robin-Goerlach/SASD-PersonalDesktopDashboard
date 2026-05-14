using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.App.Converters;

/// <summary>
/// Converts a <see cref="WidgetStatus"/> value into a subtle WPF border brush for a dashboard card.
/// </summary>
/// <remarks>
/// The application project now also enables Windows Forms support for the tray icon. Windows Forms brings
/// <c>System.Drawing.Color</c> into the project, while WPF uses <see cref="System.Windows.Media.Color"/>.
/// This converter intentionally aliases the WPF color type so the code remains unambiguous and easy to read.
/// </remarks>
public sealed class WidgetStatusToBrushConverter : IValueConverter
{
    /// <summary>
    /// Converts a widget status value into a WPF <see cref="Brush"/> used by the dashboard card border.
    /// </summary>
    /// <param name="value">The value supplied by WPF data binding. Expected type: <see cref="WidgetStatus"/>.</param>
    /// <param name="targetType">The target binding type requested by WPF.</param>
    /// <param name="parameter">Optional converter parameter. Currently not used.</param>
    /// <param name="culture">The culture supplied by WPF. Currently not used.</param>
    /// <returns>
    /// A <see cref="SolidColorBrush"/> that visually represents the status. Unknown values fall back to a neutral brush.
    /// </returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not WidgetStatus status)
        {
            // WPF binding can pass unset or unexpected values during startup or designer rendering.
            // Returning a neutral brush keeps the UI robust instead of throwing an exception.
            return CreateBrush(51, 65, 85);
        }

        return status switch
        {
            WidgetStatus.Info => CreateBrush(56, 189, 248),
            WidgetStatus.Warning => CreateBrush(250, 204, 21),
            WidgetStatus.Critical => CreateBrush(248, 113, 113),
            WidgetStatus.Disabled => CreateBrush(71, 85, 105),
            _ => CreateBrush(51, 65, 85)
        };
    }

    /// <summary>
    /// Converts a border brush back into a widget status.
    /// </summary>
    /// <param name="value">The value supplied by WPF data binding.</param>
    /// <param name="targetType">The target binding type requested by WPF.</param>
    /// <param name="parameter">Optional converter parameter.</param>
    /// <param name="culture">The culture supplied by WPF.</param>
    /// <returns>This converter never returns a value because it is intentionally one-way only.</returns>
    /// <exception cref="NotSupportedException">Always thrown because converting a brush back into a status is not supported.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // The dashboard UI only needs status -> brush conversion.
        // Supporting the reverse direction would make the converter more complex without adding value.
        throw new NotSupportedException("Widget status border conversion is one-way only.");
    }

    /// <summary>
    /// Creates a WPF brush from RGB byte values.
    /// </summary>
    /// <param name="red">The red color component.</param>
    /// <param name="green">The green color component.</param>
    /// <param name="blue">The blue color component.</param>
    /// <returns>A new immutable-looking WPF brush instance for use in bindings.</returns>
    private static SolidColorBrush CreateBrush(byte red, byte green, byte blue)
    {
        // Fully qualify the WPF color type to avoid ambiguity with System.Drawing.Color,
        // which becomes available after enabling Windows Forms for the tray icon.
        return new SolidColorBrush(System.Windows.Media.Color.FromRgb(red, green, blue));
    }
}
