using System.Collections.ObjectModel;
using Sasd.PersonalDesktopDashboard.Core.Abstractions;
using Sasd.PersonalDesktopDashboard.Core.Configuration;
using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.App.ViewModels;

/// <summary>
/// View model for the main dashboard window.
/// </summary>
public sealed class DashboardViewModel : ViewModelBase
{
    private readonly IDashboardDataService _dashboardDataService;
    private readonly IDashboardSettingsService _settingsService;

    private DashboardSettings _settings = new();
    private string _headerTitle = "SASD Personal Desktop Dashboard";
    private string _headerSubtitle = "Initialisiere Dashboard …";
    private string _refreshInfo = "Noch nicht aktualisiert";
    private bool _isBusy;

    /// <summary>
    /// Initializes a new instance of the <see cref="DashboardViewModel"/> class.
    /// </summary>
    /// <param name="dashboardDataService">Service that provides dashboard data.</param>
    /// <param name="settingsService">Service that loads dashboard settings.</param>
    public DashboardViewModel(
        IDashboardDataService dashboardDataService,
        IDashboardSettingsService settingsService)
    {
        _dashboardDataService = dashboardDataService;
        _settingsService = settingsService;

        RefreshCommand = new AsyncRelayCommand(RefreshAsync, () => !IsBusy);
    }

    /// <summary>
    /// Gets the widget cards currently shown in the dashboard.
    /// </summary>
    public ObservableCollection<DashboardWidgetViewModel> Widgets { get; } = [];

    /// <summary>
    /// Gets the command used by the refresh button.
    /// </summary>
    public AsyncRelayCommand RefreshCommand { get; }

    /// <summary>
    /// Gets the main header title.
    /// </summary>
    public string HeaderTitle
    {
        get => _headerTitle;
        private set => SetProperty(ref _headerTitle, value);
    }

    /// <summary>
    /// Gets the header subtitle.
    /// </summary>
    public string HeaderSubtitle
    {
        get => _headerSubtitle;
        private set => SetProperty(ref _headerSubtitle, value);
    }

    /// <summary>
    /// Gets a short text describing the last refresh.
    /// </summary>
    public string RefreshInfo
    {
        get => _refreshInfo;
        private set => SetProperty(ref _refreshInfo, value);
    }

    /// <summary>
    /// Gets a value indicating whether the dashboard is currently loading data.
    /// </summary>
    public bool IsBusy
    {
        get => _isBusy;
        private set
        {
            if (SetProperty(ref _isBusy, value))
            {
                RefreshCommand.RaiseCanExecuteChanged();
            }
        }
    }

    /// <summary>
    /// Loads settings and then loads the first dashboard snapshot.
    /// </summary>
    public async Task LoadAsync()
    {
        try
        {
            IsBusy = true;

            _settings = await _settingsService.LoadAsync();

            HeaderTitle = "SASD Personal Desktop Dashboard";
            HeaderSubtitle = $"Modus: {_settings.PreferredDisplayMode} · Theme: {_settings.ThemeName}";

            await RefreshInternalAsync(_settings.PreferredDisplayMode);
        }
        catch (Exception ex)
        {
            // V0.1 uses a simple UI-visible error message. Later versions should route this through
            // a proper logging service and a user-friendly notification system.
            HeaderSubtitle = "Fehler beim Laden der Dashboard-Daten";
            RefreshInfo = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }

    /// <summary>
    /// Refreshes the current dashboard data.
    /// </summary>
    public async Task RefreshAsync()
    {
        try
        {
            IsBusy = true;
            await RefreshInternalAsync(_settings.PreferredDisplayMode);
        }
        catch (Exception ex)
        {
            RefreshInfo = $"Aktualisierung fehlgeschlagen: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task RefreshInternalAsync(DashboardDisplayMode displayMode)
    {
        var snapshot = await _dashboardDataService.GetDashboardSnapshotAsync(displayMode);

        Widgets.Clear();

        foreach (var widget in snapshot.Widgets)
        {
            Widgets.Add(new DashboardWidgetViewModel(widget));
        }

        RefreshInfo = $"Aktualisiert: {snapshot.GeneratedAtLocal:HH:mm:ss}";
    }
}
