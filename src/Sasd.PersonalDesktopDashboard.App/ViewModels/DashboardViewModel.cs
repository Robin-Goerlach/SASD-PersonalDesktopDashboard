using System.Collections.ObjectModel;
using Sasd.PersonalDesktopDashboard.Core.Abstractions;
using Sasd.PersonalDesktopDashboard.Core.Configuration;
using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.App.ViewModels;

/// <summary>
/// View model for the main dashboard window.
/// </summary>
/// <remarks>
/// The view model is the small bridge between the WPF window and the dashboard
/// data service. It owns the currently active dashboard display mode so refreshes
/// no longer have to rely only on the preferred mode stored in the settings file.
/// </remarks>
public sealed class DashboardViewModel : ViewModelBase
{
    private readonly IDashboardDataService _dashboardDataService;
    private readonly IDashboardSettingsService _settingsService;

    private DashboardSettings _settings = new();
    private DashboardDisplayMode _currentDisplayMode = DashboardDisplayMode.Dashboard;
    private string _headerTitle = "SASD Personal Desktop Dashboard";
    private string _headerSubtitle = "Initialisiere Dashboard …";
    private string _refreshInfo = "Noch nicht aktualisiert";
    private bool _isBusy;

    /// <summary>
    /// Initializes a new instance of the <see cref="DashboardViewModel" /> class.
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
    /// Gets the currently active display mode used for dashboard data requests.
    /// </summary>
    /// <remarks>
    /// This value is intentionally separate from <see cref="DashboardSettings.PreferredDisplayMode" />.
    /// The settings value describes the user's default preference, while this property describes
    /// the mode that is currently active in the running WPF window.
    /// </remarks>
    public DashboardDisplayMode CurrentDisplayMode
    {
        get => _currentDisplayMode;
        private set
        {
            if (SetProperty(ref _currentDisplayMode, value))
            {
                // Keep the header text in sync whenever the active mode changes.
                UpdateHeaderSubtitle();
            }
        }
    }

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
    /// <remarks>
    /// This overload preserves the original simple startup path. It uses the preferred
    /// display mode from the settings file as the initial mode.
    /// </remarks>
    public async Task LoadAsync()
    {
        await LoadAsync(_settings.PreferredDisplayMode);
    }

    /// <summary>
    /// Loads settings and then loads the first dashboard snapshot for a specific initial mode.
    /// </summary>
    /// <param name="initialDisplayMode">The display mode that should be used for the initial snapshot.</param>
    /// <remarks>
    /// The main window calls this overload so the first data snapshot matches the visual window mode.
    /// This avoids the later mismatch where the window is compact but the data service still receives
    /// <see cref="DashboardDisplayMode.Dashboard" />.
    /// </remarks>
    public async Task LoadAsync(DashboardDisplayMode initialDisplayMode)
    {
        try
        {
            IsBusy = true;

            _settings = await _settingsService.LoadAsync();

            HeaderTitle = "SASD Personal Desktop Dashboard";
            CurrentDisplayMode = initialDisplayMode;

            // Update explicitly as well, because assigning the same enum value does not raise
            // a property change. The text should still reflect the loaded theme name.
            UpdateHeaderSubtitle();

            await RefreshInternalAsync(CurrentDisplayMode);
        }
        catch (Exception ex)
        {
            // V0.x uses a simple UI-visible error message. The application-level logger is
            // currently used around the WPF lifecycle; later versions can add a user-facing
            // notification service if needed.
            HeaderSubtitle = "Fehler beim Laden der Dashboard-Daten";
            RefreshInfo = ex.Message;
        }
        finally
        {
            IsBusy = false;
        }
    }

    /// <summary>
    /// Changes the active dashboard display mode and reloads the dashboard data for that mode.
    /// </summary>
    /// <param name="displayMode">The display mode that should become active.</param>
    /// <returns>A task that completes when the data has been refreshed.</returns>
    public async Task ChangeDisplayModeAsync(DashboardDisplayMode displayMode)
    {
        // The mode is updated before refreshing so the UI and later log messages describe
        // the mode the user has just selected.
        CurrentDisplayMode = displayMode;

        await RefreshAsync();
    }

    /// <summary>
    /// Refreshes the current dashboard data.
    /// </summary>
    public async Task RefreshAsync()
    {
        try
        {
            IsBusy = true;

            await RefreshInternalAsync(CurrentDisplayMode);
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

    /// <summary>
    /// Updates the header subtitle from the active display mode and loaded settings.
    /// </summary>
    private void UpdateHeaderSubtitle()
    {
        HeaderSubtitle = $"Modus: {CurrentDisplayMode} · Theme: {_settings.ThemeName}";
    }

    /// <summary>
    /// Loads a dashboard snapshot from the data service and replaces the visible widget list.
    /// </summary>
    /// <param name="displayMode">The display mode for which the snapshot should be built.</param>
    private async Task RefreshInternalAsync(DashboardDisplayMode displayMode)
    {
        var snapshot = await _dashboardDataService.GetDashboardSnapshotAsync(displayMode);

        Widgets.Clear();

        foreach (var widget in snapshot.Widgets)
        {
            Widgets.Add(new DashboardWidgetViewModel(widget));
        }

        // Show the active mode in the refresh text. This is useful during development because
        // it immediately reveals whether Compact refreshes really use Compact data.
        RefreshInfo = $"Aktualisiert: {snapshot.GeneratedAtLocal:HH:mm:ss} · {snapshot.DisplayMode}";
    }
}
