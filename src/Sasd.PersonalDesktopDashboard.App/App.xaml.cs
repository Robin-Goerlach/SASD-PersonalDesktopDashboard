using System.Windows;
using Sasd.PersonalDesktopDashboard.App.ViewModels;
using Sasd.PersonalDesktopDashboard.Core.Abstractions;
using Sasd.PersonalDesktopDashboard.Infrastructure.Configuration;
using Sasd.PersonalDesktopDashboard.Infrastructure.Windows;
using Sasd.PersonalDesktopDashboard.Modules.MockData;

namespace Sasd.PersonalDesktopDashboard.App;

/// <summary>
/// WPF application entry point and manual composition root.
/// </summary>
/// <remarks>
/// V0.2 still avoids a dependency injection framework. This keeps the project easy to follow
/// while we establish the technical foundation. When the application grows, this class can be
/// replaced by a HostBuilder and a proper DI container without changing the Core abstractions.
/// </remarks>
public partial class App : Application
{
    /// <inheritdoc />
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Manual dependency wiring for the first technical shell.
        // The App project depends on abstractions from Core and concrete services from
        // Infrastructure and Modules.
        IDashboardSettingsService settingsService = new JsonDashboardSettingsService(
            DefaultDashboardPaths.GetSettingsFilePath());

        IDashboardDataService dataService = new MockDashboardDataService();

        // V0.2 adds the first Windows-specific infrastructure services. The display service
        // detects the current monitor setup; the placement service uses it to keep the window
        // visible when the laptop is undocked or monitor order changes.
        IDisplayService displayService = new WindowsDisplayService();
        IWindowPlacementService windowPlacementService = new JsonWindowPlacementService(
            DefaultDashboardPaths.GetWindowPlacementFilePath(),
            displayService);

        var viewModel = new DashboardViewModel(dataService, settingsService);
        var mainWindow = new MainWindow(viewModel, windowPlacementService);

        MainWindow = mainWindow;
        mainWindow.Show();
    }
}
