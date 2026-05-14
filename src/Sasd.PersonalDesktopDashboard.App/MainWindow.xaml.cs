using System;
using System.ComponentModel;
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

    private readonly DashboardViewModel _viewModel;
    private readonly IWindowPlacementService _windowPlacementService;

    private DashboardDisplayMode _displayMode = DashboardDisplayMode.Dashboard;
    private WindowPlacementSettings? _normalPlacementBeforeCompactMode;

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
            // For this early shell we call the async view-model method here and
            // let the view model expose any user-facing error text.
            ApplicationLogger.Current.Info("Loading dashboard data.");
            await _viewModel.LoadAsync();
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
    /// Saves the current window placement before the window is closed.
    /// </summary>
    /// <param name="sender">The window that raised the event.</param>
    /// <param name="e">The cancel event arguments.</param>
    private async void MainWindow_Closing(object? sender, CancelEventArgs e)
    {
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
    /// Handles clicks on both Compact Mode buttons in the sidebar and the header area.
    /// </summary>
    /// <param name="sender">The button that raised the click event.</param>
    /// <param name="e">The routed event arguments.</param>
    private void CompactModeButton_Click(object sender, RoutedEventArgs e)
    {
        ToggleCompactMode();
    }

    /// <summary>
    /// Switches between normal dashboard mode and compact mode.
    /// </summary>
    private void ToggleCompactMode()
    {
        var targetMode = _displayMode == DashboardDisplayMode.Compact
            ? DashboardDisplayMode.Dashboard
            : DashboardDisplayMode.Compact;

        ApplyDashboardDisplayMode(targetMode);
    }

    /// <summary>
    /// Applies the requested dashboard display mode to the WPF window.
    /// </summary>
    /// <param name="targetMode">The display mode that should become active.</param>
    private void ApplyDashboardDisplayMode(DashboardDisplayMode targetMode)
    {
        if (_displayMode == targetMode)
        {
            return;
        }

        try
        {
            ApplicationLogger.Current.Info($"Switching dashboard display mode from {_displayMode} to {targetMode}.");

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

            ApplicationLogger.Current.Info($"Dashboard display mode is now {_displayMode}.");
        }
        catch (Exception exception)
        {
            // A failed mode switch should not crash the dashboard during early
            // development. Logging the error gives us enough diagnostic data
            // while the current usable mode remains on screen.
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
            ? "V0.4 Compact Foundation"
            : "V0.4 Window + Compact Foundation";
    }

    /// <summary>
    /// Creates the placement that should be saved when the application closes.
    /// </summary>
    /// <returns>The placement snapshot that should be persisted.</returns>
    private WindowPlacementSettings CreatePlacementForShutdown()
    {
        if (_displayMode == DashboardDisplayMode.Compact && _normalPlacementBeforeCompactMode is not null)
        {
            // V0.4 does not yet persist display mode separately. To avoid
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
        return double.IsNaN(value) || double.IsInfinity(value) ? 0 : value;
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
