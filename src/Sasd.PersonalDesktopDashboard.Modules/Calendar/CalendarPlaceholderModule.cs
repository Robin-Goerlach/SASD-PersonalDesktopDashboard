using Sasd.PersonalDesktopDashboard.Core.Models;
using Sasd.PersonalDesktopDashboard.Core.Modules;
using Sasd.PersonalDesktopDashboard.Modules.Abstractions;

namespace Sasd.PersonalDesktopDashboard.Modules.Calendar;

/// <summary>
/// Provides the built-in placeholder calendar card.
/// </summary>
/// <remarks>
/// The module keeps calendar-related UI space visible without connecting to a real
/// calendar provider in the early technical shell.
/// </remarks>
public sealed class CalendarPlaceholderModule : DashboardModuleBase
{
    /// <inheritdoc />
    public override string Id => "calendar.placeholder";

    /// <inheritdoc />
    public override string DisplayName => "Calendar Placeholder";

    /// <inheritdoc />
    public override int SortOrder => 300;

    /// <inheritdoc />
    protected override bool SupportsCompactMode => true;

    /// <inheritdoc />
    public override Task<DashboardWidgetModel?> BuildWidgetAsync(
        DashboardModuleContext context,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        context.Logger.Info("Building calendar placeholder dashboard widget.");

        return Completed(new DashboardWidgetModel
        {
            Id = "calendar.next",
            Type = DashboardWidgetType.Calendar,
            Title = "Kalender",
            Subtitle = "Nächster Termin",
            PrimaryValue = "14:30",
            Description = "Projektplanung SASD Dashboard.",
            Details =
            [
                "Dauer: 45 Minuten",
                "Modus: lokal / Platzhalter",
                "Privacy Mode soll später Details ausblenden",
            ],
            Footer = "Internes Modul · Kalenderintegration folgt später",
            Status = WidgetStatus.Normal,
        });
    }
}
