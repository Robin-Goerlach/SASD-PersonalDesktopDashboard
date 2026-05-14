using Sasd.PersonalDesktopDashboard.Core.Abstractions;
using Sasd.PersonalDesktopDashboard.Core.Logging;
using Sasd.PersonalDesktopDashboard.Core.Models;
using Sasd.PersonalDesktopDashboard.Core.Modules;
using Sasd.PersonalDesktopDashboard.Modules.Registration;

namespace Sasd.PersonalDesktopDashboard.Modules.MockData;

/// <summary>
/// Builds dashboard snapshots from the internal built-in dashboard modules.
/// </summary>
/// <remarks>
/// The class name is kept for compatibility with the early project state, but the
/// implementation is no longer a single hard-coded list of dashboard cards. Instead,
/// it executes small internal modules and combines their widget results into one
/// snapshot for the WPF application.
/// </remarks>
public sealed class MockDashboardDataService : IDashboardDataService
{
    private readonly IReadOnlyList<IDashboardModule> _modules;
    private readonly IAppLogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MockDashboardDataService" /> class
    /// with the default internal modules and a no-operation logger.
    /// </summary>
    /// <remarks>
    /// This constructor keeps tests and simple manual usage easy. The real application
    /// should prefer the constructor that receives an explicit module list and logger.
    /// </remarks>
    public MockDashboardDataService()
        : this(DashboardModuleCatalog.CreateDefaultModules(), NullAppLogger.Instance)
    {
        // Delegates all setup to the main constructor.
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MockDashboardDataService" /> class.
    /// </summary>
    /// <param name="modules">The internal dashboard modules that should provide widget cards.</param>
    /// <param name="logger">The logger used by the data service and passed to the modules.</param>
    public MockDashboardDataService(
        IEnumerable<IDashboardModule> modules,
        IAppLogger logger)
    {
        ArgumentNullException.ThrowIfNull(modules);

        _logger = logger ?? NullAppLogger.Instance;

        // Materialize and sort once in the constructor. This keeps snapshot generation
        // predictable and avoids accidental differences when the caller provides an
        // enumerable that changes between calls.
        _modules = modules
            .OrderBy(module => module.SortOrder)
            .ThenBy(module => module.Id, StringComparer.Ordinal)
            .ToArray();
    }

    /// <inheritdoc />
    public async Task<DashboardSnapshot> GetDashboardSnapshotAsync(
        DashboardDisplayMode displayMode,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var generatedAtLocal = DateTime.Now;
        var context = new DashboardModuleContext(displayMode, generatedAtLocal, _logger);
        var widgets = new List<DashboardWidgetModel>();

        _logger.Info($"Building dashboard snapshot for display mode '{displayMode}' using {_modules.Count} internal modules.");

        foreach (var module in _modules)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (!module.IsVisibleIn(displayMode))
            {
                // Skipped modules are useful to see while developing display modes.
                // Later this could become Debug-level logging, but the current logger
                // intentionally only supports a small set of levels.
                _logger.Info($"Skipping dashboard module '{module.Id}' for display mode '{displayMode}'.");
                continue;
            }

            try
            {
                _logger.Info($"Executing dashboard module '{module.Id}' ({module.DisplayName}).");

                var widget = await module.BuildWidgetAsync(context, cancellationToken);

                if (widget is not null)
                {
                    widgets.Add(widget);
                    _logger.Info($"Dashboard module '{module.Id}' produced widget '{widget.Id}'.");
                }
                else
                {
                    _logger.Warning($"Dashboard module '{module.Id}' returned no widget.");
                }
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                // Cancellation is not a module error. Re-throw so the caller can stop
                // cleanly, for example during shutdown or a later refresh cancellation.
                throw;
            }
            catch (Exception exception)
            {
                // A single module should not prevent the complete dashboard from opening.
                // We log the problem and add a visible diagnostic card so the failure is
                // easy to notice during development.
                _logger.Error($"Dashboard module '{module.Id}' failed.", exception);
                widgets.Add(CreateModuleFailureWidget(module, exception));
            }
        }

        _logger.Info($"Dashboard snapshot built with {widgets.Count} widgets.");

        return new DashboardSnapshot
        {
            GeneratedAtLocal = generatedAtLocal,
            DisplayMode = displayMode,
            Widgets = widgets,
        };
    }

    /// <summary>
    /// Creates a visible diagnostic widget for a module failure.
    /// </summary>
    /// <param name="module">The module that failed.</param>
    /// <param name="exception">The exception thrown by the module.</param>
    /// <returns>A dashboard widget that describes the failure in a developer-friendly way.</returns>
    private static DashboardWidgetModel CreateModuleFailureWidget(
        IDashboardModule module,
        Exception exception)
    {
        return new DashboardWidgetModel
        {
            Id = $"module.error.{module.Id}",
            Type = DashboardWidgetType.Notes,
            Title = "Modulfehler",
            Subtitle = module.DisplayName,
            PrimaryValue = "Fehler",
            Description = $"Das interne Dashboard-Modul '{module.Id}' konnte keine Daten liefern.",
            Details =
            [
                exception.GetType().Name,
                exception.Message,
                "Details stehen zusätzlich im AppData-Log.",
            ],
            Footer = "Interne Diagnosekarte · später durch bessere Benachrichtigung ersetzen",
            Status = WidgetStatus.Critical,
        };
    }
}
