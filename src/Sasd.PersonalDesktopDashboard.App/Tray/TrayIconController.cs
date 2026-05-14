using System;
using System.Drawing;
using System.Threading.Tasks;
using Sasd.PersonalDesktopDashboard.Core.Logging;
using Forms = System.Windows.Forms;

namespace Sasd.PersonalDesktopDashboard.App.Tray;

/// <summary>
/// Owns the Windows notification-area icon for the dashboard application.
/// </summary>
/// <remarks>
/// WPF does not provide a built-in tray icon control. For this first technical
/// foundation we intentionally use <see cref="Forms.NotifyIcon"/> from Windows
/// Forms because it is part of the .NET Windows desktop stack and does not require
/// an additional NuGet dependency.
/// </remarks>
public sealed class TrayIconController : IDisposable
{
    private readonly MainWindow _mainWindow;
    private readonly IAppLogger _logger;
    private readonly Forms.NotifyIcon _notifyIcon;
    private readonly Forms.ContextMenuStrip _contextMenu;
    private readonly Forms.ToolStripMenuItem _showWindowMenuItem;
    private readonly Forms.ToolStripMenuItem _hideWindowMenuItem;
    private readonly Forms.ToolStripMenuItem _toggleCompactModeMenuItem;
    private readonly Forms.ToolStripMenuItem _exitMenuItem;
    private bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="TrayIconController"/> class.
    /// </summary>
    /// <param name="mainWindow">The WPF main window controlled by the tray icon.</param>
    /// <param name="logger">Logger used for tray diagnostics.</param>
    public TrayIconController(MainWindow mainWindow, IAppLogger logger)
    {
        _mainWindow = mainWindow;
        _logger = logger;

        _showWindowMenuItem = new Forms.ToolStripMenuItem("Dashboard anzeigen");
        _hideWindowMenuItem = new Forms.ToolStripMenuItem("Dashboard ausblenden");
        _toggleCompactModeMenuItem = new Forms.ToolStripMenuItem("Compact Mode");
        _exitMenuItem = new Forms.ToolStripMenuItem("Beenden");

        _contextMenu = new Forms.ContextMenuStrip();
        _contextMenu.Items.Add(_showWindowMenuItem);
        _contextMenu.Items.Add(_hideWindowMenuItem);
        _contextMenu.Items.Add(_toggleCompactModeMenuItem);
        _contextMenu.Items.Add(new Forms.ToolStripSeparator());
        _contextMenu.Items.Add(_exitMenuItem);

        // The menu is updated right before it is shown. This keeps the labels and
        // enabled states consistent after the user toggles the window or compact mode.
        _contextMenu.Opening += (_, _) => UpdateMenuStateSafely();

        _showWindowMenuItem.Click += (_, _) => _ = ExecuteOnUiThreadAsync(
            () =>
            {
                _mainWindow.ShowDashboardFromTray();
                return Task.CompletedTask;
            },
            "show dashboard from tray");

        _hideWindowMenuItem.Click += (_, _) => _ = ExecuteOnUiThreadAsync(
            () =>
            {
                _mainWindow.HideDashboardToTray();
                return Task.CompletedTask;
            },
            "hide dashboard to tray");

        _toggleCompactModeMenuItem.Click += (_, _) => _ = ExecuteOnUiThreadAsync(
            _mainWindow.ToggleCompactModeFromTrayAsync,
            "toggle compact mode from tray");

        _exitMenuItem.Click += (_, _) => _ = ExecuteOnUiThreadAsync(
            () =>
            {
                _mainWindow.ExitApplicationFromTray();
                return Task.CompletedTask;
            },
            "exit application from tray");

        _notifyIcon = new Forms.NotifyIcon
        {
            Text = "SASD Personal Desktop Dashboard",
            Icon = SystemIcons.Application,
            ContextMenuStrip = _contextMenu,
            Visible = true,
        };

        // Double-clicking the tray icon follows the common Windows convention:
        // it brings the main window back to the foreground.
        _notifyIcon.DoubleClick += (_, _) => _ = ExecuteOnUiThreadAsync(
            () =>
            {
                _mainWindow.ShowDashboardFromTray();
                return Task.CompletedTask;
            },
            "show dashboard from tray double click");

        _logger.Info("Tray icon created and made visible.");
    }

    /// <summary>
    /// Releases the tray icon and its context menu.
    /// </summary>
    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }

        _isDisposed = true;

        // Hide the icon before disposing it. This prevents a stale icon from
        // remaining in the Windows notification area until the mouse hovers over it.
        _notifyIcon.Visible = false;
        _notifyIcon.Dispose();
        _contextMenu.Dispose();

        _logger.Info("Tray icon disposed.");
    }

    /// <summary>
    /// Executes a tray action on the WPF dispatcher and logs possible failures.
    /// </summary>
    /// <param name="action">The action that should run on the WPF UI thread.</param>
    /// <param name="actionName">Human-readable action name used in log messages.</param>
    /// <returns>A task that completes after the action has been executed.</returns>
    private async Task ExecuteOnUiThreadAsync(Func<Task> action, string actionName)
    {
        try
        {
            _logger.Info($"Tray action started: {actionName}.");

            if (_mainWindow.Dispatcher.CheckAccess())
            {
                await action();
            }
            else
            {
                // Dispatcher.InvokeAsync(Func<Task>) returns a Task<Task>. The first
                // await waits until WPF has started the delegate; the second await
                // waits until the asynchronous tray action itself has completed.
                await await _mainWindow.Dispatcher.InvokeAsync(action);
            }

            UpdateMenuStateSafely();
            _logger.Info($"Tray action completed: {actionName}.");
        }
        catch (Exception exception)
        {
            // Tray actions are convenience operations. A failure should be logged,
            // but it should not terminate the whole dashboard process.
            _logger.Error($"Tray action failed: {actionName}.", exception);
        }
    }

    /// <summary>
    /// Updates tray menu labels and enabled states without throwing to the caller.
    /// </summary>
    private void UpdateMenuStateSafely()
    {
        try
        {
            if (_mainWindow.Dispatcher.CheckAccess())
            {
                UpdateMenuState();
            }
            else
            {
                _mainWindow.Dispatcher.Invoke(UpdateMenuState);
            }
        }
        catch (Exception exception)
        {
            _logger.Error("Failed to update tray menu state.", exception);
        }
    }

    /// <summary>
    /// Updates the tray context menu from the current WPF window state.
    /// </summary>
    private void UpdateMenuState()
    {
        _showWindowMenuItem.Enabled = !_mainWindow.IsDashboardVisibleFromTray;
        _hideWindowMenuItem.Enabled = _mainWindow.IsDashboardVisibleFromTray;
        _toggleCompactModeMenuItem.Text = _mainWindow.IsDashboardInCompactModeFromTray
            ? "Normal Mode"
            : "Compact Mode";
    }
}
