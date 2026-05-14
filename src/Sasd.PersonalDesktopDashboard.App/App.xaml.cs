using System;
using System.Windows;
using System.Windows.Threading;
using Sasd.PersonalDesktopDashboard.App.Logging;
using Sasd.PersonalDesktopDashboard.App.ViewModels;
using Sasd.PersonalDesktopDashboard.Core.Abstractions;
using Sasd.PersonalDesktopDashboard.Infrastructure.Configuration;
using Sasd.PersonalDesktopDashboard.Infrastructure.Logging;
using Sasd.PersonalDesktopDashboard.Infrastructure.Windows;
using Sasd.PersonalDesktopDashboard.Modules.MockData;

namespace Sasd.PersonalDesktopDashboard.App;

/// <summary>
/// WPF application entry point and manual composition root.
/// </summary>
/// <remarks>
/// The application currently uses explicit manual wiring instead of a dependency
/// injection container. This keeps the technical shell easy to understand while
/// the foundation is still being built step by step.
/// </remarks>
public partial class App : Application
{
    /// <inheritdoc />
    protected override void OnStartup(StartupEventArgs e)
    {
        // Configure logging as early as possible. From this point on, all startup
        // problems can be written to the AppData log file.
        ApplicationLogger.Configure(new FileAppLogger(FileAppLoggerOptions.CreateDefault()));
        ApplicationLogger.Current.Info("Application startup started.");

        // Register a central WPF dispatcher exception hook. This catches many UI
        // thread exceptions and gives us a diagnostic log entry before WPF shows
        // its normal crash behavior.
        DispatcherUnhandledException += App_DispatcherUnhandledException;

        try
        {
            base.OnStartup(e);

            ApplicationLogger.Current.Info("Creating application services.");

            // Manual dependency wiring for the first technical shell.
            //
            // The App project depends on abstractions from Core and concrete
            // implementations from Infrastructure and Modules. This is simple,
            // explicit and sufficient for the current development phase.
            IDashboardSettingsService settingsService = new JsonDashboardSettingsService(
                DefaultDashboardPaths.GetSettingsFilePath());

            IDashboardDataService dataService = new MockDashboardDataService();

            // V0.2 added the first Windows-specific infrastructure services.
            //
            // The display service detects the current monitor setup. The placement
            // service uses it to keep the window visible when a laptop is undocked
            // or when monitor order changes.
            IDisplayService displayService = new WindowsDisplayService();

            IWindowPlacementService windowPlacementService = new JsonWindowPlacementService(
                DefaultDashboardPaths.GetWindowPlacementFilePath(),
                displayService);

            var viewModel = new DashboardViewModel(dataService, settingsService);
            var mainWindow = new MainWindow(viewModel, windowPlacementService);

            MainWindow = mainWindow;
            mainWindow.Show();

            ApplicationLogger.Current.Info("Main window shown.");
            ApplicationLogger.Current.Info("Application startup completed.");
        }
        catch (Exception exception)
        {
            // During the technical shell phase we do not hide startup errors.
            // We log the error and rethrow it so Visual Studio still shows the
            // real failure during debugging.
            ApplicationLogger.Current.Error("Application startup failed.", exception);
            throw;
        }
    }

    /// <inheritdoc />
    protected override void OnExit(ExitEventArgs e)
    {
        try
        {
            ApplicationLogger.Current.Info("Application shutdown started.");
            base.OnExit(e);
            ApplicationLogger.Current.Info("Application shutdown completed.");
        }
        catch (Exception exception)
        {
            // An exit failure is unusual, but logging it makes later shutdown or
            // tray-icon related issues easier to diagnose.
            ApplicationLogger.Current.Error("Application shutdown failed.", exception);
            throw;
        }
    }

    /// <summary>
    /// Logs unhandled exceptions that occur on the WPF dispatcher thread.
    /// </summary>
    /// <param name="sender">The WPF application instance that raised the event.</param>
    /// <param name="e">The dispatcher exception event arguments.</param>
    private static void App_DispatcherUnhandledException(
        object sender,
        DispatcherUnhandledExceptionEventArgs e)
    {
        ApplicationLogger.Current.Error("Unhandled WPF dispatcher exception.", e.Exception);

        // Do not mark the exception as handled in this early development phase.
        // Crashing visibly is better than silently continuing after an unknown
        // UI error. The log file still gives us the diagnostic details.
        e.Handled = false;
    }
}
