using Sasd.PersonalDesktopDashboard.Core.Models;
using Sasd.PersonalDesktopDashboard.Core.Modules;
using Sasd.PersonalDesktopDashboard.Modules.Abstractions;

namespace Sasd.PersonalDesktopDashboard.Modules.News;

/// <summary>
/// Provides the built-in placeholder news card.
/// </summary>
/// <remarks>
/// The real RSS or curated news module will be a later step. For now this module
/// documents the intended dashboard area without performing any network access.
/// </remarks>
public sealed class NewsPlaceholderModule : DashboardModuleBase
{
    /// <inheritdoc />
    public override string Id => "news.placeholder";

    /// <inheritdoc />
    public override string DisplayName => "News Placeholder";

    /// <inheritdoc />
    public override int SortOrder => 400;

    /// <inheritdoc />
    public override Task<DashboardWidgetModel?> BuildWidgetAsync(
        DashboardModuleContext context,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        context.Logger.Info("Building news placeholder dashboard widget.");

        return Completed(new DashboardWidgetModel
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
                "Wissenschaft: optionaler Feed",
            ],
            Footer = "Internes Modul · noch keine externen Abrufe",
            Status = WidgetStatus.Disabled,
        });
    }
}
