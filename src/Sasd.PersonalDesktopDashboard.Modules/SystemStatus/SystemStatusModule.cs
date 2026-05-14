using Sasd.PersonalDesktopDashboard.Core.Models;
using Sasd.PersonalDesktopDashboard.Core.Modules;
using Sasd.PersonalDesktopDashboard.Modules.Abstractions;

namespace Sasd.PersonalDesktopDashboard.Modules.SystemStatus;

/// <summary>
/// Provides a small built-in card with basic local system information.
/// </summary>
/// <remarks>
/// This module only uses safe .NET environment values. Later versions can add richer
/// performance counters behind a dedicated infrastructure abstraction.
/// </remarks>
public sealed class SystemStatusModule : DashboardModuleBase
{
    /// <inheritdoc />
    public override string Id => "system.local";

    /// <inheritdoc />
    public override string DisplayName => "Local System Status";

    /// <inheritdoc />
    public override int SortOrder => 500;

    /// <inheritdoc />
    public override Task<DashboardWidgetModel?> BuildWidgetAsync(
        DashboardModuleContext context,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        context.Logger.Info("Building local system status dashboard widget.");

        return Completed(new DashboardWidgetModel
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
                $"Prozessoren: {Environment.ProcessorCount}",
            ],
            Footer = "Internes Modul · einfache .NET-Umgebungsdaten",
            Status = WidgetStatus.Info,
        });
    }
}
