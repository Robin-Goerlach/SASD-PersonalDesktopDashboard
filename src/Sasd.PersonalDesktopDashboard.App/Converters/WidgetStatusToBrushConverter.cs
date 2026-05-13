using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.App.Converters;

/// <summary>
/// Converts a widget status into a subtle border brush for the dashboard card.
/// </summary>
public sealed class WidgetStatusToBrushConverter : IValueConverter
{
    /// <inheritdoc />
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not WidgetStatus status)
        {
            return new SolidColorBrush(Color.FromRgb(51, 65, 85));
        }

        return status switch
        {
            WidgetStatus.Info => new SolidColorBrush(Color.FromRgb(56, 189, 248)),
            WidgetStatus.Warning => new SolidColorBrush(Color.FromRgb(250, 204, 21)),
            WidgetStatus.Critical => new SolidColorBrush(Color.FromRgb(248, 113, 113)),
            WidgetStatus.Disabled => new SolidColorBrush(Color.FromRgb(71, 85, 105)),
            _ => new SolidColorBrush(Color.FromRgb(51, 65, 85))
        };
    }

    /// <inheritdoc />
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotSupportedException("Widget status border conversion is one-way only.");
}
