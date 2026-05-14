using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Sasd.PersonalDesktopDashboard.App.Logging;
using Sasd.PersonalDesktopDashboard.App.ViewModels;
using Sasd.PersonalDesktopDashboard.Core.Abstractions;
using Sasd.PersonalDesktopDashboard.Core.Configuration;
using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.App;

/// <summary>
/// Main WPF window for the SASD Personal Desktop Dashboard.
/// </summary>
/// <remarks>
/// The window currently contains the technical dashboard shell. It receives its
/// view model and infrastructure services from <see cref="App" /> and keeps the
/// UI-specific lifecycle handling in one place.
/// </remarks>
public partial class MainWindow : Window
{
    private const double NormalMinimumWidth = 960;
    private const double NormalMinimumHeight = 600;
    private const double NormalSidebarWidth = 230;
    private const double NormalContentMargin = 28;

    private const double CompactMinimumWidth = 360;
    private const double CompactMinimumHeight = 420;
    private const double CompactDefaultWidth = 430;
    private const double CompactDefaultHeight = 560;
    private const double CompactContentMargin = 14;

    private const bool HideWindowToTrayWhenClosedByUser = true;

    private readonly DashboardViewModel _viewModel;
    private readonly IWindowPlacementService _windowPlacementService;

    private DashboardDisplayMode _displayMode = DashboardDisplayMode.Dashboard;
    private WindowPlacementSettings? _normalPlacementBeforeCompactMode;
    private bool _isExplicitApplicationExitRequested;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow" /> class.
    /// </summary>
    /// <param name="viewModel">The dashboard view model created in the application composition root.</param>
    /// <param name="windowPlacementService">Service used to restore and save the window position.</param>
    public MainWindow(
        DashboardViewModel viewModel,
        IWindowPlacementService windowPlacementService)
    {
        InitializeComponent();

        _viewModel = viewModel;
        _windowPlacementService = windowPlacementService;

        DataContext = _viewModel;

        ApplicationLogger.Current.Info("Main window initialized.");

        // SourceInitialized is fired after WPF has created the native window
        // handle but before the user has meaningfully interacted with the window.
        // This is a good moment to apply a saved position without the dashboard
        // first appearing on the wrong monitor.
        SourceInitialized += MainWindow_SourceInitialized;

        // Closing is used instead of Closed because the WPF properties are still
        // available and represent the final user-visible window state.
        Closing += MainWindow_Closing;

        // The dashboard starts in normal dashboard mode. The visual update keeps
        // the button labels and mode hint consistent even before the user clicks
        // the Compact Mode button for the first time.
        UpdateDashboardDisplayModeVisuals();
    }

    /// <summary>
    /// Restores the last valid window placement after the native WPF window has been created.
    /// </summary>
    /// <param name="sender">The window that raised the event.</param>
    /// <param name="e">The event arguments.</param>
    private async void MainWindow_SourceInitialized(object? sender, EventArgs e)
    {
        try
        {
            ApplicationLogger.Current.Info("Loading or creating valid main window placement.");

            var placement = await _windowPlacementService.LoadOrCreateValidPlacementAsync(
                Width,
                Height);

            ApplicationLogger.Current.Info("Applying main window placement.");
            ApplyWindowPlacement(placement);
        }
        catch (Exception exception)
        {
            // Window placement must never prevent the dashboard from starting.
            // If something goes wrong, WPF will keep the default CenterScreen
            // behavior defined in XAML.
            ApplicationLogger.Current.Error("Failed to restore main window placement.", exception);
        }
    }

    /// <summary>
    /// Loads the initial dashboard data after the visual tree has been created.
    /// </summary>
    /// <param name="sender">The window that raised the event.</param>
    /// <param name="e">The routed event arguments.</param>
    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            // WPF event handlers cannot be awaited directly by the framework.
            // The current display mode is passed explicitly so the very first
            // dashboard snapshot matches the visual window mode.
            ApplicationLogger.Current.Info($"Loading dashboard data for display mode '{_displayMode}'.");
            await _viewModel.LoadAsync(_displayMode);
            ApplicationLogger.Current.Info("Dashboard data loaded.");
        }
        catch (Exception exception)
        {
            // A real data-loading crash should be visible during development, but
            // the log entry gives us more detail if the exception is reported later.
            ApplicationLogger.Current.Error("Failed to load dashboard data.", exception);
            throw;
        }
    }

    /// <summary>
    /// Handles close requests for the dashboard window.
    /// </summary>
    /// <param name="sender">The window that raised the event.</param>
    /// <param name="e">The cancel event arguments.</param>
    /// <remarks>
    /// In V0.9 the normal window close button no longer exits the application.
    /// Instead, the dashboard is hidden to the notification area so it can be
    /// restored from the tray icon. A real application shutdown is still possible
    /// through the tray menu. That explicit shutdown path sets
    /// <see cref="_isExplicitApplicationExitRequested" /> before WPF closes the window.
    /// </remarks>
    private async void MainWindow_Closing(object? sender, CancelEventArgs e)
    {
        if (ShouldHideToTrayInsteadOfClosing())
        {
            ApplicationLogger.Current.Info("Main window close requested by user; hiding dashboard to tray instead of exiting.");

            // Cancel the close operation first. After this point the window remains
            // alive, and Hide() simply removes it from the taskbar and desktop.
            e.Cancel = true;
            HideDashboardToTray();
            return;
        }

        try
        {
            ApplicationLogger.Current.Info("Saving main window placement.");

            var placement = CreatePlacementForShutdown();
            await _windowPlacementService.SavePlacementAsync(placement);

            ApplicationLogger.Current.Info("Main window placement saved.");
        }
        catch (Exception exception)
        {
            // Failure to save the window position is not critical. The next start
            // will simply fall back to a safe centered position.
            ApplicationLogger.Current.Error("Failed to save main window placement.", exception);
        }
    }

    /// <summary>
    /// Determines whether the current close request should hide the window to the tray.
    /// </summary>
    /// <returns>
    /// <see langword="true" /> when the close request should be converted into a
    /// tray-hide operation; otherwise, <see langword="false" /> so WPF can continue
    /// shutting the application down.
    /// </returns>
    private bool ShouldHideToTrayInsteadOfClosing()
    {
        if (!HideWindowToTrayWhenClosedByUser)
        {
            // This constant is intentionally kept in one place so it can later be
            // replaced by a user setting without changing the closing algorithm.
            return false;
        }

        if (_isExplicitApplicationExitRequested)
        {
            // The tray menu explicitly requested an application exit. In that case
            // we must not cancel Closing, otherwise the application could no longer
            // be terminated through its own tray menu.
            return false;
        }

        return true;
    }

    /// <summary>
    /// Handles clicks on both Compact Mode buttons in the sidebar and the header area.
    /// </summary>
    /// <param name="sender">The button that raised the click event.</param>
    /// <param name="e">The routed event arguments.</param>
    private async void CompactModeButton_Click(object sender, RoutedEventArgs e)
    {
        await ToggleCompactModeAsync();
    }

    /// <summary>
    /// Switches between normal dashboard mode and compact mode.
    /// </summary>
    /// <returns>A task that completes when the visual mode and data mode have been updated.</returns>
    private async Task ToggleCompactModeAsync()
    {
        var targetMode = _displayMode == DashboardDisplayMode.Compact
            ? DashboardDisplayMode.Dashboard
            : DashboardDisplayMode.Compact;

        await ApplyDashboardDisplayModeAsync(targetMode);
    }

    /// <summary>
    /// Applies the requested dashboard display mode to the WPF window and the dashboard data context.
    /// </summary>
    /// <param name="targetMode">The display mode that should become active.</param>
    /// <returns>A task that completes when the mode switch has finished.</returns>
    private async Task ApplyDashboardDisplayModeAsync(DashboardDisplayMode targetMode)
    {
        if (_displayMode == targetMode)
        {
            return;
        }

        var previousMode = _displayMode;

        try
        {
            ApplicationLogger.Current.Info($"Switching dashboard display mode from {previousMode} to {targetMode}.");

            if (targetMode == DashboardDisplayMode.Compact)
            {
                EnterCompactMode();
            }
            else
            {
                LeaveCompactMode();
            }

            _displayMode = targetMode;
            UpdateDashboardDisplayModeVisuals();

            // This is the important V0.7 connection: the view model now receives
            // the same display mode that the window is visually using. Therefore
            // internal modules can adapt their widgets to Dashboard or Compact.
            ApplicationLogger.Current.Info($"Reloading dashboard data for display mode '{_displayMode}'.");
            await _viewModel.ChangeDisplayModeAsync(_displayMode);

            ApplicationLogger.Current.Info($"Dashboard display mode is now {_displayMode}.");
        }
        catch (Exception exception)
        {
            // A failed mode switch should not crash the dashboard during early
            // development. Logging the error gives us diagnostic data while the
            // current usable mode remains on screen.
            ApplicationLogger.Current.Error("Failed to switch dashboard display mode.", exception);
        }
    }

    /// <summary>
    /// Applies the visual and size changes for compact mode.
    /// </summary>
    private void EnterCompactMode()
    {
        // Remember the current normal window rectangle before changing the size.
        // This lets the user return to the previous working position later.
        _normalPlacementBeforeCompactMode = CreateCurrentWindowPlacement();

        // Width and height changes do not make sense while the window is maximized.
        // Therefore compact mode first returns the window to normal state.
        if (WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
        }

        MinWidth = CompactMinimumWidth;
        MinHeight = CompactMinimumHeight;

        // Hide the navigation sidebar. The header button stays visible so the
        // user can always return to normal dashboard mode.
        SidebarPanel.Visibility = Visibility.Collapsed;
        SidebarColumn.Width = new GridLength(0);
        ContentRoot.Margin = new Thickness(CompactContentMargin);

        Width = CompactDefaultWidth;
        Height = CompactDefaultHeight;

        Title = "SASD Personal Desktop Dashboard - Compact Mode";
    }

    /// <summary>
    /// Restores the normal dashboard layout after compact mode.
    /// </summary>
    private void LeaveCompactMode()
    {
        MinWidth = NormalMinimumWidth;
        MinHeight = NormalMinimumHeight;

        SidebarColumn.Width = new GridLength(NormalSidebarWidth);
        SidebarPanel.Visibility = Visibility.Visible;
        ContentRoot.Margin = new Thickness(NormalContentMargin);

        Title = "SASD Personal Desktop Dashboard";

        if (_normalPlacementBeforeCompactMode is not null)
        {
            // Restore the exact normal window placement that was active before
            // compact mode was entered. This is more pleasant than returning to
            // a generic default size.
            ApplyWindowPlacement(_normalPlacementBeforeCompactMode);
        }
        else
        {
            // Fallback path for later scenarios where compact mode might be
            // restored directly from settings without an in-memory normal size.
            Width = Math.Max(Width, NormalMinimumWidth);
            Height = Math.Max(Height, NormalMinimumHeight);
        }
    }

    /// <summary>
    /// Updates labels and status text after a mode change.
    /// </summary>
    private void UpdateDashboardDisplayModeVisuals()
    {
        var isCompactMode = _displayMode == DashboardDisplayMode.Compact;

        CompactModeButton.Content = isCompactMode ? "Normal Mode" : "Compact Mode";
        HeaderCompactModeButton.Content = isCompactMode ? "Normal" : "Compact";
        ModeStatusText.Text = isCompactMode
            ? "V0.9 Compact + Tray"
            : "V0.9 Dashboard + Tray";
    }

    /// <summary>
    /// Creates the placement that should be saved when the application closes.
    /// </summary>
    /// <returns>The placement snapshot that should be persisted.</returns>
    private WindowPlacementSettings CreatePlacementForShutdown()
    {
        if (_displayMode == DashboardDisplayMode.Compact && _normalPlacementBeforeCompactMode is not null)
        {
            // V0.x does not yet persist display mode separately. To avoid
            // surprising the user on the next start, closing from compact mode
            // saves the remembered normal dashboard placement instead of the
            // small compact rectangle.
            ApplicationLogger.Current.Info("Saving remembered normal placement because the window is currently in compact mode.");

            return new WindowPlacementSettings
            {
                Left = _normalPlacementBeforeCompactMode.Left,
                Top = _normalPlacementBeforeCompactMode.Top,
                Width = _normalPlacementBeforeCompactMode.Width,
                Height = _normalPlacementBeforeCompactMode.Height,
                WindowState = _normalPlacementBeforeCompactMode.WindowState,
                DisplayDeviceName = _normalPlacementBeforeCompactMode.DisplayDeviceName,
                DisplayFingerprint = _normalPlacementBeforeCompactMode.DisplayFingerprint,
                SavedAtUtc = DateTime.UtcNow,
            };
        }

        return CreateCurrentWindowPlacement();
    }

    /// <summary>
    /// Applies a persisted window placement to the WPF window.
    /// </summary>
    /// <param name="placement">The placement settings that should be applied.</param>
    private void ApplyWindowPlacement(WindowPlacementSettings placement)
    {
        // Manual startup location ensures that WPF does not override the saved
        // coordinates with the XAML startup behavior.
        WindowStartupLocation = WindowStartupLocation.Manual;

        Left = placement.Left;
        Top = placement.Top;
        Width = Math.Max(MinWidth, placement.Width);
        Height = Math.Max(MinHeight, placement.Height);

        // Restore maximized state only after assigning the restore rectangle.
        // This lets Windows maximize the window on the display where the restored
        // rectangle belongs.
        WindowState = placement.WindowState == DashboardWindowState.Maximized
            ? WindowState.Maximized
            : WindowState.Normal;
    }

    /// <summary>
    /// Creates a serializable placement snapshot from the current WPF window state.
    /// </summary>
    /// <returns>The current window placement settings.</returns>
    private WindowPlacementSettings CreateCurrentWindowPlacement()
    {
        var bounds = WindowState == WindowState.Maximized
            ? RestoreBounds
            : new Rect(Left, Top, ActualWidth, ActualHeight);

        return new WindowPlacementSettings
        {
            Left = GetSafeCoordinate(bounds.Left),
            Top = GetSafeCoordinate(bounds.Top),
            Width = GetSafeDimension(bounds.Width, Width),
            Height = GetSafeDimension(bounds.Height, Height),
            WindowState = ToDashboardWindowState(WindowState),
            SavedAtUtc = DateTime.UtcNow,
        };
    }

    /// <summary>
    /// Converts a WPF window state to the application's serializable window state model.
    /// </summary>
    /// <param name="windowState">The WPF window state.</param>
    /// <returns>The serializable dashboard window state.</returns>
    private static DashboardWindowState ToDashboardWindowState(WindowState windowState)
    {
        // Do not persist Minimized. Re-opening a dashboard minimized would feel
        // like the application did not start, so we treat it as a normal window.
        return windowState == WindowState.Maximized
            ? DashboardWindowState.Maximized
            : DashboardWindowState.Normal;
    }

    /// <summary>
    /// Replaces invalid coordinate values with a safe fallback.
    /// </summary>
    /// <param name="value">The coordinate value to validate.</param>
    /// <returns>A safe coordinate value.</returns>
    private static double GetSafeCoordinate(double value)
    {
        return double.IsNaN(value) || double.IsInfinity(value)
            ? 0
            : value;
    }

    /// <summary>
    /// Replaces invalid dimensions with a fallback dimension.
    /// </summary>
    /// <param name="value">The dimension value to validate.</param>
    /// <param name="fallback">The fallback value used when the dimension is invalid.</param>
    /// <returns>A safe dimension value.</returns>
    private static double GetSafeDimension(double value, double fallback)
    {
        if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0)
        {
            return fallback;
        }

        return value;
    }
}
