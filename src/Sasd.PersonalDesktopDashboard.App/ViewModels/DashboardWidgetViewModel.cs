using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.App.ViewModels;

/// <summary>
/// View model wrapper for one dashboard widget card.
/// </summary>
public sealed class DashboardWidgetViewModel
{
    private readonly DashboardWidgetModel _model;

    /// <summary>
    /// Initializes a new instance of the <see cref="DashboardWidgetViewModel"/> class.
    /// </summary>
    /// <param name="model">The underlying widget model.</param>
    public DashboardWidgetViewModel(DashboardWidgetModel model)
    {
        _model = model;
    }

    /// <summary>Gets the widget title.</summary>
    public string Title => _model.Title;

    /// <summary>Gets the widget subtitle.</summary>
    public string Subtitle => _model.Subtitle;

    /// <summary>Gets the primary widget value.</summary>
    public string PrimaryValue => _model.PrimaryValue;

    /// <summary>Gets the widget description.</summary>
    public string Description => _model.Description;

    /// <summary>Gets the detail lines.</summary>
    public IReadOnlyList<string> Details => _model.Details;

    /// <summary>Gets the footer text.</summary>
    public string Footer => _model.Footer;

    /// <summary>Gets the status displayed on the card.</summary>
    public WidgetStatus Status => _model.Status;
}
