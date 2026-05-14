using Sasd.PersonalDesktopDashboard.Core.Models;
using Sasd.PersonalDesktopDashboard.Core.Modules;
using Sasd.PersonalDesktopDashboard.Modules.Abstractions;

namespace Sasd.PersonalDesktopDashboard.Modules.Weather;

/// <summary>
/// Provides the built-in placeholder weather card.
/// </summary>
/// <remarks>
/// This module does not call external weather services yet. It keeps the dashboard usable
/// while the later real weather module is designed and implemented.
/// </remarks>
public sealed class WeatherPlaceholderModule : DashboardModuleBase
{
    /// <inheritdoc />
    public override string Id => "weather.placeholder";

    /// <inheritdoc />
    public override string DisplayName => "Weather Placeholder";

    /// <inheritdoc />
    public override int SortOrder => 100;

    /// <inheritdoc />
    protected override bool SupportsCompactMode => true;

    /// <inheritdoc />
    public override Task<DashboardWidgetModel?> BuildWidgetAsync(
        DashboardModuleContext context,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // The logger comes from the application composition root. This proves that
        // modules can now participate in diagnostics without knowing the concrete
        // file logger implementation.
        context.Logger.Info("Building weather placeholder dashboard widget.");

        return Completed(new DashboardWidgetModel
        {
            Id = "weather.now",
            Type = DashboardWidgetType.Weather,
            Title = "Wetter",
            Subtitle = "Kleve / Niederrhein · Platzhalterdaten",
            PrimaryValue = "18 °C",
            Description = "Leichter Regen möglich, später freundlicher.",
            Details =
            [
                "Nächste 2 h: wechselhaft",
                "Regenwahrscheinlichkeit: 35 %",
                "Wind: mäßig aus West",
            ],
            Footer = "Internes Modul · später Open-Meteo/DWD-Anbindung",
            Status = WidgetStatus.Info,
        });
    }
}
