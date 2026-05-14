using Sasd.PersonalDesktopDashboard.Core.Logging;
using Sasd.PersonalDesktopDashboard.Core.Models;
using Sasd.PersonalDesktopDashboard.Core.Modules;
using Sasd.PersonalDesktopDashboard.Modules.MockData;

namespace Sasd.PersonalDesktopDashboard.Core.Tests;

/// <summary>
/// Tests for <see cref="MockDashboardDataService" /> module execution behavior.
/// </summary>
/// <remarks>
/// The service is still named "Mock" for compatibility with the first technical shell,
/// but it now acts as the internal module runner. These tests protect the important
/// rule that one broken module must not crash the whole dashboard.
/// </remarks>
public sealed class MockDashboardDataServiceTests
{
    /// <summary>
    /// Ensures that widgets are returned in module sort order, independent of the
    /// order in which the modules were passed to the constructor.
    /// </summary>
    [Fact]
    public async Task GetDashboardSnapshotAsync_ShouldReturnWidgetsInStableModuleOrder()
    {
        var logger = new RecordingAppLogger();
        var modules = new IDashboardModule[]
        {
            TestDashboardModule.Visible("module.two", "Module Two", 200, CreateWidget("widget.two")),
            TestDashboardModule.Visible("module.one", "Module One", 100, CreateWidget("widget.one")),
        };

        var service = new MockDashboardDataService(modules, logger);

        var snapshot = await service.GetDashboardSnapshotAsync(DashboardDisplayMode.Dashboard);

        Assert.Equal(DashboardDisplayMode.Dashboard, snapshot.DisplayMode);
        Assert.Equal(["widget.one", "widget.two"], snapshot.Widgets.Select(widget => widget.Id).ToArray());
        Assert.Contains(logger.InfoMessages, message => message.Contains("Dashboard snapshot built with 2 widgets.", StringComparison.Ordinal));
    }

    /// <summary>
    /// Ensures that modules which are not visible in the requested display mode are
    /// skipped and not executed.
    /// </summary>
    [Fact]
    public async Task GetDashboardSnapshotAsync_ShouldSkipModulesThatAreNotVisibleInDisplayMode()
    {
        var logger = new RecordingAppLogger();
        var modules = new IDashboardModule[]
        {
            TestDashboardModule.Visible("module.visible", "Visible Module", 100, CreateWidget("widget.visible")),
            TestDashboardModule.Invisible("module.hidden", "Hidden Module", 200),
        };

        var service = new MockDashboardDataService(modules, logger);

        var snapshot = await service.GetDashboardSnapshotAsync(DashboardDisplayMode.Compact);

        var widget = Assert.Single(snapshot.Widgets);

        Assert.Equal("widget.visible", widget.Id);
        Assert.Contains(logger.InfoMessages, message => message.Contains("Skipping dashboard module 'module.hidden'", StringComparison.Ordinal));
    }

    /// <summary>
    /// Ensures that a module returning <see langword="null" /> does not produce a card,
    /// but still creates a useful warning log entry.
    /// </summary>
    [Fact]
    public async Task GetDashboardSnapshotAsync_ShouldWarnWhenModuleReturnsNoWidget()
    {
        var logger = new RecordingAppLogger();
        var modules = new IDashboardModule[]
        {
            TestDashboardModule.ReturningNull("module.empty", "Empty Module", 100),
        };

        var service = new MockDashboardDataService(modules, logger);

        var snapshot = await service.GetDashboardSnapshotAsync(DashboardDisplayMode.Dashboard);

        Assert.Empty(snapshot.Widgets);
        Assert.Contains(logger.WarningMessages, message => message.Contains("module.empty", StringComparison.Ordinal));
    }

    /// <summary>
    /// Ensures that one failing module is isolated: the successful modules still
    /// produce their cards and the failure is represented as a diagnostic widget.
    /// </summary>
    [Fact]
    public async Task GetDashboardSnapshotAsync_ShouldConvertModuleExceptionIntoDiagnosticWidget()
    {
        var logger = new RecordingAppLogger();
        var modules = new IDashboardModule[]
        {
            TestDashboardModule.Visible("module.ok", "Working Module", 100, CreateWidget("widget.ok")),
            TestDashboardModule.Throwing("module.fail", "Failing Module", 200, new InvalidOperationException("Test failure from fake module.")),
        };

        var service = new MockDashboardDataService(modules, logger);

        var snapshot = await service.GetDashboardSnapshotAsync(DashboardDisplayMode.Dashboard);

        Assert.Equal(2, snapshot.Widgets.Count);
        Assert.Contains(snapshot.Widgets, widget => widget.Id == "widget.ok");

        var diagnosticWidget = Assert.Single(snapshot.Widgets, widget => widget.Id == "module.error.module.fail");

        Assert.Equal("Modulfehler", diagnosticWidget.Title);
        Assert.Equal("Failing Module", diagnosticWidget.Subtitle);
        Assert.Equal(WidgetStatus.Critical, diagnosticWidget.Status);
        Assert.Contains("module.fail", diagnosticWidget.Description, StringComparison.Ordinal);
        Assert.Contains(logger.ErrorMessages, message => message.Contains("module.fail", StringComparison.Ordinal));
    }

    /// <summary>
    /// Creates a simple widget for module-runner tests.
    /// </summary>
    /// <param name="id">The stable widget identifier.</param>
    /// <returns>A dashboard widget with minimal test data.</returns>
    private static DashboardWidgetModel CreateWidget(string id)
    {
        return new DashboardWidgetModel
        {
            Id = id,
            Type = DashboardWidgetType.Notes,
            Title = id,
            Subtitle = "Test widget",
            PrimaryValue = "OK",
            Description = "Created by a test module.",
            Status = WidgetStatus.Info,
        };
    }

    /// <summary>
    /// Small logger implementation used by the tests to inspect log messages without
    /// writing files to the developer machine.
    /// </summary>
    private sealed class RecordingAppLogger : IAppLogger
    {
        /// <summary>
        /// Gets all informational messages recorded during the test.
        /// </summary>
        public List<string> InfoMessages { get; } = [];

        /// <summary>
        /// Gets all warning messages recorded during the test.
        /// </summary>
        public List<string> WarningMessages { get; } = [];

        /// <summary>
        /// Gets all error messages recorded during the test.
        /// </summary>
        public List<string> ErrorMessages { get; } = [];

        /// <inheritdoc />
        public void Info(string message)
        {
            InfoMessages.Add(message);
        }

        /// <inheritdoc />
        public void Warning(string message)
        {
            WarningMessages.Add(message);
        }

        /// <inheritdoc />
        public void Error(string message)
        {
            ErrorMessages.Add(message);
        }

        /// <inheritdoc />
        public void Error(string message, Exception exception)
        {
            // Keep the exception type in the recorded text so tests can verify that
            // the error path really used the exception-aware logger overload.
            ErrorMessages.Add($"{message} ({exception.GetType().Name}: {exception.Message})");
        }
    }

    /// <summary>
    /// Simple configurable dashboard module used by the module-runner tests.
    /// </summary>
    private sealed class TestDashboardModule : IDashboardModule
    {
        private readonly bool _isVisible;
        private readonly Func<DashboardModuleContext, CancellationToken, Task<DashboardWidgetModel?>> _buildWidgetAsync;

        private TestDashboardModule(
            string id,
            string displayName,
            int sortOrder,
            bool isVisible,
            Func<DashboardModuleContext, CancellationToken, Task<DashboardWidgetModel?>> buildWidgetAsync)
        {
            Id = id;
            DisplayName = displayName;
            SortOrder = sortOrder;
            _isVisible = isVisible;
            _buildWidgetAsync = buildWidgetAsync;
        }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public string DisplayName { get; }

        /// <inheritdoc />
        public int SortOrder { get; }

        /// <summary>
        /// Creates a visible test module that returns the provided widget.
        /// </summary>
        /// <param name="id">The test module identifier.</param>
        /// <param name="displayName">The human-readable test module name.</param>
        /// <param name="sortOrder">The sort order used by the module runner.</param>
        /// <param name="widget">The widget returned by the test module.</param>
        /// <returns>A visible test dashboard module.</returns>
        public static TestDashboardModule Visible(
            string id,
            string displayName,
            int sortOrder,
            DashboardWidgetModel widget)
        {
            return new TestDashboardModule(
                id,
                displayName,
                sortOrder,
                isVisible: true,
                (context, cancellationToken) => Task.FromResult<DashboardWidgetModel?>(widget));
        }

        /// <summary>
        /// Creates a hidden test module that would fail if executed.
        /// </summary>
        /// <param name="id">The test module identifier.</param>
        /// <param name="displayName">The human-readable test module name.</param>
        /// <param name="sortOrder">The sort order used by the module runner.</param>
        /// <returns>A hidden test dashboard module.</returns>
        public static TestDashboardModule Invisible(
            string id,
            string displayName,
            int sortOrder)
        {
            return new TestDashboardModule(
                id,
                displayName,
                sortOrder,
                isVisible: false,
                (context, cancellationToken) => throw new InvalidOperationException("Hidden test module was executed unexpectedly."));
        }

        /// <summary>
        /// Creates a visible test module that returns no widget.
        /// </summary>
        /// <param name="id">The test module identifier.</param>
        /// <param name="displayName">The human-readable test module name.</param>
        /// <param name="sortOrder">The sort order used by the module runner.</param>
        /// <returns>A visible test dashboard module that returns <see langword="null" />.</returns>
        public static TestDashboardModule ReturningNull(
            string id,
            string displayName,
            int sortOrder)
        {
            return new TestDashboardModule(
                id,
                displayName,
                sortOrder,
                isVisible: true,
                (context, cancellationToken) => Task.FromResult<DashboardWidgetModel?>(null));
        }

        /// <summary>
        /// Creates a visible test module that throws the provided exception when executed.
        /// </summary>
        /// <param name="id">The test module identifier.</param>
        /// <param name="displayName">The human-readable test module name.</param>
        /// <param name="sortOrder">The sort order used by the module runner.</param>
        /// <param name="exception">The exception thrown by the test module.</param>
        /// <returns>A failing test dashboard module.</returns>
        public static TestDashboardModule Throwing(
            string id,
            string displayName,
            int sortOrder,
            Exception exception)
        {
            return new TestDashboardModule(
                id,
                displayName,
                sortOrder,
                isVisible: true,
                (context, cancellationToken) => Task.FromException<DashboardWidgetModel?>(exception));
        }

        /// <inheritdoc />
        public bool IsVisibleIn(DashboardDisplayMode displayMode)
        {
            return _isVisible;
        }

        /// <inheritdoc />
        public Task<DashboardWidgetModel?> BuildWidgetAsync(
            DashboardModuleContext context,
            CancellationToken cancellationToken = default)
        {
            // The fake module still respects cancellation before running its configured behavior.
            // This makes the test helper behave like a real module would behave.
            cancellationToken.ThrowIfCancellationRequested();
            return _buildWidgetAsync(context, cancellationToken);
        }
    }
}
