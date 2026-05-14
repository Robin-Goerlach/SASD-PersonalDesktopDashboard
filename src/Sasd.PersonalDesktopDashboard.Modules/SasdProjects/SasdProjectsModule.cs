using Sasd.PersonalDesktopDashboard.Core.Models;
using Sasd.PersonalDesktopDashboard.Core.Modules;
using Sasd.PersonalDesktopDashboard.Modules.Abstractions;

namespace Sasd.PersonalDesktopDashboard.Modules.SasdProjects;

/// <summary>
/// Provides the built-in placeholder card for SASD project information.
/// </summary>
/// <remarks>
/// The card is a placeholder for later integrations with local repositories,
/// GitHub metadata or a dedicated SASD project list.
/// </remarks>
public sealed class SasdProjectsModule : DashboardModuleBase
{
    /// <inheritdoc />
    public override string Id => "sasd.projects";

    /// <inheritdoc />
    public override string DisplayName => "SASD Projects";

    /// <inheritdoc />
    public override int SortOrder => 600;

    /// <inheritdoc />
    public override Task<DashboardWidgetModel?> BuildWidgetAsync(
        DashboardModuleContext context,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        context.Logger.Info("Building SASD projects dashboard widget.");

        return Completed(new DashboardWidgetModel
        {
            Id = "sasd.projects",
            Type = DashboardWidgetType.SasdProjects,
            Title = "SASD Projekte",
            Subtitle = "Portfolio- und Produktstatus",
            PrimaryValue = "Dashboard V0.5",
            Description = "Diese Karte soll später GitHub, lokale Repositories oder SASD-Projektlisten zusammenfassen.",
            Details =
            [
                "TaskHost: möglicher Task-Lieferant",
                "LogSink/Mustela: spätere Statusquellen",
                "Desktop Dashboard: aktuelles Produkt",
            ],
            Footer = "Internes Modul · zeigt bewusst nur Beispielwerte",
            Status = WidgetStatus.Normal,
        });
    }
}
