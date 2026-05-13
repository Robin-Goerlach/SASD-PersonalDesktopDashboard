using Sasd.PersonalDesktopDashboard.Core.Abstractions;
using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.Modules.MockData;

/// <summary>
/// Provides deterministic example data for the first dashboard shell.
/// </summary>
/// <remarks>
/// This service deliberately does not call external APIs. The first development goal is a stable
/// and understandable desktop shell. Real modules can later replace individual cards step by step.
/// </remarks>
public sealed class MockDashboardDataService : IDashboardDataService
{
    /// <inheritdoc />
    public Task<DashboardSnapshot> GetDashboardSnapshotAsync(
        DashboardDisplayMode displayMode,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var widgets = new List<DashboardWidgetModel>
        {
            CreateWeatherWidget(),
            CreateTasksWidget(),
            CreateCalendarWidget(),
            CreateNewsWidget(),
            CreateSystemStatusWidget(),
            CreateSasdProjectsWidget()
        };

        // In a compact mode the first version shows fewer cards. Later this rule should be
        // moved into a layout policy that can be configured by the user.
        if (displayMode == DashboardDisplayMode.Compact)
        {
            widgets = widgets
                .Where(widget => widget.Type is DashboardWidgetType.Weather
                    or DashboardWidgetType.Tasks
                    or DashboardWidgetType.Calendar)
                .ToList();
        }

        var snapshot = new DashboardSnapshot
        {
            GeneratedAtLocal = DateTime.Now,
            DisplayMode = displayMode,
            Widgets = widgets
        };

        return Task.FromResult(snapshot);
    }

    private static DashboardWidgetModel CreateWeatherWidget() => new()
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
            "Wind: mäßig aus West"
        ],
        Footer = "V0.1 Mock · später Open-Meteo/DWD-Anbindung",
        Status = WidgetStatus.Info
    };

    private static DashboardWidgetModel CreateTasksWidget() => new()
    {
        Id = "tasks.today",
        Type = DashboardWidgetType.Tasks,
        Title = "Aufgaben",
        Subtitle = "Heute im Fokus",
        PrimaryValue = "3 wichtig",
        Description = "Die wichtigsten Aufgaben des Tages werden später aus lokalen Tasks oder TaskHost geladen.",
        Details =
        [
            "V0.1 Technical Shell committen",
            "Monitorprofile als nächstes planen",
            "Wettermodul für V0.4 vorbereiten"
        ],
        Footer = "Lokale Dummy-Daten · keine Cloud-Verbindung",
        Status = WidgetStatus.Normal
    };

    private static DashboardWidgetModel CreateCalendarWidget() => new()
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
            "Privacy Mode soll später Details ausblenden"
        ],
        Footer = "Kalenderintegration folgt später",
        Status = WidgetStatus.Normal
    };

    private static DashboardWidgetModel CreateNewsWidget() => new()
    {
        Id = "news.headlines",
        Type = DashboardWidgetType.News,
        Title = "Nachrichten",
        Subtitle = "Lokale, Welt- und IT-News",
        PrimaryValue = "RSS geplant",
        Description = "Später sollen kuratierte Quellen statt zufälliger Newsfeeds angezeigt werden.",
        Details =
        [
            "Lokal: Region / Niederrhein",
            "IT/Security: ausgewählte Quellen",
            "Wissenschaft: optionaler Feed"
        ],
        Footer = "Noch keine externen Abrufe in V0.1",
        Status = WidgetStatus.Disabled
    };

    private static DashboardWidgetModel CreateSystemStatusWidget() => new()
    {
        Id = "system.local",
        Type = DashboardWidgetType.SystemStatus,
        Title = "Systemstatus",
        Subtitle = Environment.MachineName,
        PrimaryValue = "OK",
        Description = "Lokaler Rechnerstatus wird später regelmäßig und ressourcenschonend aktualisiert.",
        Details =
        [
            $"Benutzer: {Environment.UserName}",
            $"64-bit OS: {(Environment.Is64BitOperatingSystem ? "ja" : "nein")}",
            $"Prozessoren: {Environment.ProcessorCount}"
        ],
        Footer = "Nur einfache .NET-Umgebungsdaten in V0.1",
        Status = WidgetStatus.Info
    };

    private static DashboardWidgetModel CreateSasdProjectsWidget() => new()
    {
        Id = "sasd.projects",
        Type = DashboardWidgetType.SasdProjects,
        Title = "SASD Projekte",
        Subtitle = "Portfolio- und Produktstatus",
        PrimaryValue = "Dashboard V0.1",
        Description = "Diese Karte soll später GitHub, lokale Repositories oder SASD-Projektlisten zusammenfassen.",
        Details =
        [
            "TaskHost: möglicher Task-Lieferant",
            "LogSink/Mustela: spätere Statusquellen",
            "Desktop Dashboard: aktuelles Produkt"
        ],
        Footer = "V0.1 zeigt bewusst nur Beispielwerte",
        Status = WidgetStatus.Normal
    };
}
