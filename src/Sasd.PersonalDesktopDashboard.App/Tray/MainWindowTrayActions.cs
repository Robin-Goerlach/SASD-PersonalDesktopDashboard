using System.Threading.Tasks;
using System.Windows;
using Sasd.PersonalDesktopDashboard.App.Logging;
using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.App;

/// <summary>
/// Tray-related operations exposed by <see cref="MainWindow"/>.
/// </summary>
/// <remarks>
/// The methods are kept in a separate partial class so the main window lifecycle
/// code remains readable. They deliberately delegate to the existing compact-mode
/// implementation instead of duplicating window-mode logic in the tray controller.
/// </remarks>
public partial class MainWindow
{
    /// <summary>
    /// Gets a value indicating whether the dashboard window is currently visible for tray purposes.
    /// </summary>
    /// <remarks>
    /// A minimized window is treated as not meaningfully visible. Showing the dashboard
    /// from the tray restores it to a normal visible window.
    /// </remarks>
    public bool IsDashboardVisibleFromTray => IsVisible && WindowState != WindowState.Minimized;

    /// <summary>
    /// Gets a value indicating whether the dashboard is currently in compact mode.
    /// </summary>
    public bool IsDashboardInCompactModeFromTray => _displayMode == DashboardDisplayMode.Compact;

    /// <summary>
    /// Shows the dashboard window and brings it to the foreground.
    /// </summary>
    public void ShowDashboardFromTray()
    {
        ApplicationLogger.Current.Info("Showing dashboard window from tray.");

        if (!IsVisible)
        {
            // Show() is required after Hide(). It does nothing harmful if the
            // window is already visible.
            Show();
        }

        if (WindowState == WindowState.Minimized)
        {
            // Restoring from minimized state makes the action feel like a normal
            // Windows tray application.
            WindowState = WindowState.Normal;
        }

        // Bring the window to the foreground. Activate() can return false when
        // Windows foreground restrictions apply, but it is still the correct and
        // harmless request for this simple foundation.
        Activate();
        Focus();
    }

    /// <summary>
    /// Hides the dashboard window while keeping the application process running in the tray.
    /// </summary>
    public void HideDashboardToTray()
    {
        ApplicationLogger.Current.Info("Hiding dashboard window to tray.");

        // Hide() does not trigger the window Closing event. That is intentional:
        // hiding to tray is not application shutdown and should not save a final
        // placement snapshot yet.
        Hide();
    }

    /// <summary>
    /// Toggles compact mode from the tray menu.
    /// </summary>
    /// <returns>A task that completes after the mode switch has finished.</returns>
    public async Task ToggleCompactModeFromTrayAsync()
    {
        var targetMode = _displayMode == DashboardDisplayMode.Compact
            ? DashboardDisplayMode.Dashboard
            : DashboardDisplayMode.Compact;

        ApplicationLogger.Current.Info($"Tray requested dashboard display mode '{targetMode}'.");
        await ApplyDashboardDisplayModeAsync(targetMode);
    }

    /// <summary>
    /// Requests application shutdown from the tray menu.
    /// </summary>
    public void ExitApplicationFromTray()
    {
        ApplicationLogger.Current.Info("Tray requested application shutdown.");

        // Calling Shutdown() lets WPF close windows normally, which means the
        // existing MainWindow.Closing handler can still save the window placement.
        System.Windows.Application.Current.Shutdown();
    }
}
