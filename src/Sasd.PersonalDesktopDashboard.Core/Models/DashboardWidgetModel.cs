namespace Sasd.PersonalDesktopDashboard.Core.Models;

/// <summary>
/// Represents one visual card on the dashboard.
/// </summary>
/// <remarks>
/// This model is deliberately generic. Real modules can later produce richer data,
/// but the first shell only needs a common card format that can be shown by the WPF UI.
/// </remarks>
public sealed class DashboardWidgetModel
{
    /// <summary>
    /// Gets or initializes the stable widget identifier.
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Gets or initializes the semantic widget type.
    /// </summary>
    public DashboardWidgetType Type { get; init; }

    /// <summary>
    /// Gets or initializes the title shown at the top of the widget card.
    /// </summary>
    public string Title { get; init; } = string.Empty;

    /// <summary>
    /// Gets or initializes a small subtitle that gives the card context.
    /// </summary>
    public string Subtitle { get; init; } = string.Empty;

    /// <summary>
    /// Gets or initializes the main value shown prominently in the card.
    /// </summary>
    public string PrimaryValue { get; init; } = string.Empty;

    /// <summary>
    /// Gets or initializes a short explanatory text under the primary value.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets or initializes detailed bullet-like lines displayed inside the widget.
    /// </summary>
    public IReadOnlyList<string> Details { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Gets or initializes a footer line, typically used for source or update information.
    /// </summary>
    public string Footer { get; init; } = string.Empty;

    /// <summary>
    /// Gets or initializes the current widget status.
    /// </summary>
    public WidgetStatus Status { get; init; } = WidgetStatus.Normal;
}
