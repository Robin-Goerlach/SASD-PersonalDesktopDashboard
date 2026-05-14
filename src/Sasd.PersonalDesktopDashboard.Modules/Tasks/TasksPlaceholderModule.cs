using Sasd.PersonalDesktopDashboard.Core.Models;
using Sasd.PersonalDesktopDashboard.Core.Modules;
using Sasd.PersonalDesktopDashboard.Modules.Abstractions;

namespace Sasd.PersonalDesktopDashboard.Modules.Tasks;

/// <summary>
/// Provides the built-in placeholder task overview card.
/// </summary>
/// <remarks>
/// Later versions can replace this placeholder with a local task database, TaskHost data
/// or another private task source without changing the WPF card rendering.
/// </remarks>
public sealed class TasksPlaceholderModule : DashboardModuleBase
{
    /// <inheritdoc />
    public override string Id => "tasks.placeholder";

    /// <inheritdoc />
    public override string DisplayName => "Tasks Placeholder";

    /// <inheritdoc />
    public override int SortOrder => 200;

    /// <inheritdoc />
    protected override bool SupportsCompactMode => true;

    /// <inheritdoc />
    public override Task<DashboardWidgetModel?> BuildWidgetAsync(
        DashboardModuleContext context,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        context.Logger.Info("Building tasks placeholder dashboard widget.");

        return Completed(new DashboardWidgetModel
        {
            Id = "tasks.today",
            Type = DashboardWidgetType.Tasks,
            Title = "Aufgaben",
            Subtitle = "Heute im Fokus",
            PrimaryValue = "3 wichtig",
            Description = "Die wichtigsten Aufgaben des Tages werden später aus lokalen Tasks oder TaskHost geladen.",
            Details =
            [
                "Technical Shell stabil halten",
                "Interne Modulstruktur einführen",
                "Echte Datenquellen später gezielt ergänzen",
            ],
            Footer = "Internes Modul · lokale Dummy-Daten · keine Cloud-Verbindung",
            Status = WidgetStatus.Normal,
        });
    }
}
