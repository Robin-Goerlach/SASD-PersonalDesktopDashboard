using System.ComponentModel;
using System.Windows;
using Sasd.PersonalDesktopDashboard.App.ViewModels;
using Sasd.PersonalDesktopDashboard.Core.Abstractions;
using Sasd.PersonalDesktopDashboard.Core.Configuration;
using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.App;

/// <summary>
/// Main WPF window for the SASD Personal Desktop Dashboard.
/// </summary>
public partial class MainWindow : Window
{
    private readonly DashboardViewModel _viewModel;
    private readonly IWindowPlacementService _windowPlacementService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindow"/> class.
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

        // SourceInitialized is fired after WPF has created the native window handle but before
        // the user has meaningfully interacted with the window. This is a good moment to apply
        // a saved position without the dashboard first appearing on the wrong monitor.
        SourceInitialized += MainWindow_SourceInitialized;

        // Closing is used instead of Closed because the WPF properties are still available and
        // represent the final user-visible window state.
        Closing += MainWindow_Closing;
    }

    private async void MainWindow_SourceInitialized(object? sender, EventArgs e)
    {
        try
        {
            var placement = await _windowPlacementService.LoadOrCreateValidPlacementAsync(
                Width,
                Height);

            ApplyWindowPlacement(placement);
        }
        catch
        {
            // Window placement must never prevent the dashboard from starting. If something goes
            // wrong, WPF will keep the default CenterScreen behavior defined in XAML.
        }
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // WPF event handlers cannot be awaited directly by the framework. For this early shell
        // we call the async view-model method here and let the view model expose any error text.
        await _viewModel.LoadAsync();
    }

    private async void MainWindow_Closing(object? sender, CancelEventArgs e)
    {
        try
        {
            var placement = CreateCurrentWindowPlacement();
            await _windowPlacementService.SavePlacementAsync(placement);
        }
        catch
        {
            // Failure to save the window position is not critical. The next start will simply
            // fall back to a safe centered position.
        }
    }

    private void ApplyWindowPlacement(WindowPlacementSettings placement)
    {
        // Manual startup location ensures that WPF does not override the saved coordinates.
        WindowStartupLocation = WindowStartupLocation.Manual;

        Left = placement.Left;
        Top = placement.Top;
        Width = Math.Max(MinWidth, placement.Width);
        Height = Math.Max(MinHeight, placement.Height);

        // Restore maximized state only after assigning the restore rectangle. This lets Windows
        // maximize the window on the display where the restored rectangle belongs.
        WindowState = placement.WindowState == DashboardWindowState.Maximized
            ? WindowState.Maximized
            : WindowState.Normal;
    }

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
            SavedAtUtc = DateTime.UtcNow
        };
    }

    private static DashboardWindowState ToDashboardWindowState(WindowState windowState)
    {
        // Do not persist Minimized. Re-opening a dashboard minimized would feel like the
        // application did not start, so we treat it as a normal window instead.
        return windowState == WindowState.Maximized
            ? DashboardWindowState.Maximized
            : DashboardWindowState.Normal;
    }

    private static double GetSafeCoordinate(double value)
    {
        return double.IsNaN(value) || double.IsInfinity(value) ? 0 : value;
    }

    private static double GetSafeDimension(double value, double fallback)
    {
        if (double.IsNaN(value) || double.IsInfinity(value) || value <= 0)
        {
            return fallback;
        }

        return value;
    }
}
