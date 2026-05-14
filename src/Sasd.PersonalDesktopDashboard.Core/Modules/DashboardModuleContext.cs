using Sasd.PersonalDesktopDashboard.Core.Logging;
using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.Core.Modules;

/// <summary>
/// Provides shared runtime information to internal dashboard modules while a dashboard snapshot is built.
/// </summary>
/// <remarks>
/// This context is intentionally small. It gives modules access to the requested display mode,
/// the snapshot timestamp and the application logger without coupling modules to WPF or to
/// concrete infrastructure classes.
/// </remarks>
public sealed class DashboardModuleContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DashboardModuleContext" /> class.
    /// </summary>
    /// <param name="displayMode">The display mode for which dashboard data is requested.</param>
    /// <param name="generatedAtLocal">The local timestamp used for the dashboard snapshot.</param>
    /// <param name="logger">The logger that modules may use for diagnostic messages.</param>
    public DashboardModuleContext(
        DashboardDisplayMode displayMode,
        DateTime generatedAtLocal,
        IAppLogger logger)
    {
        DisplayMode = displayMode;
        GeneratedAtLocal = generatedAtLocal;
        Logger = logger ?? NullAppLogger.Instance;
    }

    /// <summary>
    /// Gets the display mode for which the snapshot is currently being created.
    /// </summary>
    public DashboardDisplayMode DisplayMode { get; }

    /// <summary>
    /// Gets the local timestamp at which the current snapshot started being generated.
    /// </summary>
    public DateTime GeneratedAtLocal { get; }

    /// <summary>
    /// Gets the logger that modules should use for diagnostic messages.
    /// </summary>
    public IAppLogger Logger { get; }
}
