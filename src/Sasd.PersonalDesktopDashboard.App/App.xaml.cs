using System;
using System.Windows;
using System.Windows.Threading;
using Sasd.PersonalDesktopDashboard.App.Diagnostics;
using Sasd.PersonalDesktopDashboard.App.Logging;
using Sasd.PersonalDesktopDashboard.App.Runtime;
using Sasd.PersonalDesktopDashboard.App.Tray;
using Sasd.PersonalDesktopDashboard.App.ViewModels;
using Sasd.PersonalDesktopDashboard.Core.Abstractions;
using Sasd.PersonalDesktopDashboard.Infrastructure.Configuration;
using Sasd.PersonalDesktopDashboard.Infrastructure.Logging;
using Sasd.PersonalDesktopDashboard.Infrastructure.Windows;
using Sasd.PersonalDesktopDashboard.Modules.MockData;
using Sasd.PersonalDesktopDashboard.Modules.Registration;

namespace Sasd.PersonalDesktopDashboard.App;

/// <summary>
/// WPF application entry point and manual composition root.
/// </summary>
/// <remarks>
/// The application currently uses explicit manual wiring instead of a dependency
/// injection container. This keeps the technical shell easy to understand while
/// the foundation is still being built step by step.
/// </remarks>
public partial class App : System.Windows.Application
{
    private SingleInstanceGuard? _singleInstanceGuard;
    private TrayIconController? _trayIconController;

    /// <summary>
    /// Starts the WPF application and wires the first technical services.
    /// </summary>
    /// <param name="e">Startup arguments provided by WPF.</param>
    protected override void OnStartup(StartupEventArgs e)
    {
        // Configure logging as early as possible. From this point on, all startup
        // problems can be written to the AppData log file.
        FileAppLoggerOptions loggerOptions = FileAppLoggerOptions.CreateDefault();
        ApplicationLogger.Configure(new FileAppLogger(loggerOptions));
        ApplicationLogger.Current.Info("Application startup started.");

        // V0.11 diagnostics: log the effective paths directly after the logger exists.
        // This makes it much easier to diagnose AppData and settings issues on another
        // machine without guessing where the dashboard stored its files.
        AppDataDiagnostics.LogStartupPaths(ApplicationLogger.Current, loggerOptions);

        // V0.10 single-instance guard: acquire a named mutex before creating any
        // WPF windows or tray icons. This prevents duplicate dashboard processes.
        _singleInstanceGuard = SingleInstanceGuard.CreateDefault(ApplicationLogger.Current);
        if (!_singleInstanceGuard.TryAcquire())
        {
            ApplicationLogger.Current.Warning("Application startup cancelled because another instance is already running.");

            // The second process has no UI yet, so it can shut down immediately.
            // The existing primary process keeps its window and tray icon.
            _singleInstanceGuard.Dispose();
            _singleInstanceGuard = null;
            Shutdown(0);
            return;
        }

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

            // V0.5 introduced an internal module foundation. The dashboard data
            // service still presents one simple IDashboardDataService interface to
            // the view model, but internally it collects widgets from several
            // small built-in modules. The same application logger is passed down so
            // modules can write diagnostics without knowing the file logger.
            var dashboardModules = DashboardModuleCatalog.CreateDefaultModules();
            IDashboardDataService dataService = new MockDashboardDataService(
                dashboardModules,
                ApplicationLogger.Current);
            ApplicationLogger.Current.Info($"Registered {dashboardModules.Count} internal dashboard modules.");

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

            // V0.8 tray foundation: create the tray icon after the main window has
            // been constructed. The controller delegates all UI operations back to
            // the WPF dispatcher and logs every tray action.
            _trayIconController = new TrayIconController(mainWindow, ApplicationLogger.Current);
            ApplicationLogger.Current.Info("Tray icon controller initialized.");

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

            // Startup failed after the mutex was acquired. Release it explicitly so
            // a later fixed application start is not blocked by this failed process.
            _trayIconController?.Dispose();
            _trayIconController = null;
            _singleInstanceGuard?.Dispose();
            _singleInstanceGuard = null;

            throw;
        }
    }

    /// <summary>
    /// Releases application-wide resources before WPF shuts down.
    /// </summary>
    /// <param name="e">Exit arguments provided by WPF.</param>
    protected override void OnExit(ExitEventArgs e)
    {
        try
        {
            ApplicationLogger.Current.Info("Application shutdown started.");

            // Dispose the tray icon before WPF tears down the application. This is
            // important because otherwise Windows can keep a stale notification icon
            // visible until the user moves the mouse over the notification area.
            _trayIconController?.Dispose();
            _trayIconController = null;
            ApplicationLogger.Current.Info("Tray icon controller disposed.");

            // Release the single-instance mutex as late as possible, after all normal
            // shutdown work is finished. This keeps a second process from starting
            // while the first process is still disposing its tray icon and settings.
            _singleInstanceGuard?.Dispose();
            _singleInstanceGuard = null;
            ApplicationLogger.Current.Info("Single-instance guard disposed.");

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
