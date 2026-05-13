using System.Windows;
using Sasd.PersonalDesktopDashboard.App.ViewModels;
using Sasd.PersonalDesktopDashboard.Core.Abstractions;
using Sasd.PersonalDesktopDashboard.Infrastructure.Configuration;
using Sasd.PersonalDesktopDashboard.Modules.MockData;

namespace Sasd.PersonalDesktopDashboard.App;

/// <summary>
/// WPF application entry point and manual composition root.
/// </summary>
/// <remarks>
/// V0.1 intentionally avoids a dependency injection framework. This keeps the first version
/// easy to understand. If the application grows, this class can later be replaced by a proper
/// host builder and DI container.
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

        var viewModel = new DashboardViewModel(dataService, settingsService);
        var mainWindow = new MainWindow(viewModel);

        MainWindow = mainWindow;
        mainWindow.Show();
    }
}
