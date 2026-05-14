using Sasd.PersonalDesktopDashboard.Core.Modules;
using Sasd.PersonalDesktopDashboard.Modules.Calendar;
using Sasd.PersonalDesktopDashboard.Modules.News;
using Sasd.PersonalDesktopDashboard.Modules.SasdProjects;
using Sasd.PersonalDesktopDashboard.Modules.SystemStatus;
using Sasd.PersonalDesktopDashboard.Modules.Tasks;
using Sasd.PersonalDesktopDashboard.Modules.Weather;

namespace Sasd.PersonalDesktopDashboard.Modules.Registration;

/// <summary>
/// Creates the list of built-in dashboard modules for the current application version.
/// </summary>
/// <remarks>
/// This is a simple internal catalog, not a dynamic plugin loader. The explicit list makes
/// the early application easy to understand and debug in Visual Studio.
/// </remarks>
public static class DashboardModuleCatalog
{
    /// <summary>
    /// Creates the default internal dashboard modules.
    /// </summary>
    /// <returns>The built-in modules in their intended display order.</returns>
    public static IReadOnlyList<IDashboardModule> CreateDefaultModules()
    {
        // Keep this list explicit. When a module is added, removed or reordered,
        // it should be visible in one small place during code review.
        var modules = new IDashboardModule[]
        {
            new WeatherPlaceholderModule(),
            new TasksPlaceholderModule(),
            new CalendarPlaceholderModule(),
            new NewsPlaceholderModule(),
            new SystemStatusModule(),
            new SasdProjectsModule(),
        };

        return modules
            .OrderBy(module => module.SortOrder)
            .ThenBy(module => module.Id, StringComparer.Ordinal)
            .ToArray();
    }
}
