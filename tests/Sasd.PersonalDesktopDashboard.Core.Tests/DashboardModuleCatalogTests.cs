using Sasd.PersonalDesktopDashboard.Modules.Registration;

namespace Sasd.PersonalDesktopDashboard.Core.Tests;

/// <summary>
/// Tests for the internal built-in dashboard module catalog.
/// </summary>
/// <remarks>
/// The catalog is intentionally explicit. These tests help us notice accidental
/// duplicate identifiers, unstable ordering or missing placeholder modules early.
/// </remarks>
public sealed class DashboardModuleCatalogTests
{
    /// <summary>
    /// Ensures that the default catalog contains all built-in placeholder modules
    /// expected by the early dashboard shell.
    /// </summary>
    [Fact]
    public void CreateDefaultModules_ShouldContainExpectedBuiltInModules()
    {
        var modules = DashboardModuleCatalog.CreateDefaultModules();
        var moduleIds = modules.Select(module => module.Id).ToArray();

        Assert.Contains("weather.placeholder", moduleIds);
        Assert.Contains("tasks.placeholder", moduleIds);
        Assert.Contains("calendar.placeholder", moduleIds);
        Assert.Contains("news.placeholder", moduleIds);
        Assert.Contains("system.status", moduleIds);
        Assert.Contains("sasd.projects", moduleIds);
    }

    /// <summary>
    /// Ensures that technical module identifiers are unique.
    /// </summary>
    [Fact]
    public void CreateDefaultModules_ShouldUseUniqueModuleIds()
    {
        var modules = DashboardModuleCatalog.CreateDefaultModules();

        var moduleIds = modules.Select(module => module.Id).ToArray();
        var uniqueModuleIds = moduleIds.Distinct(StringComparer.Ordinal).ToArray();

        Assert.Equal(moduleIds.Length, uniqueModuleIds.Length);
    }

    /// <summary>
    /// Ensures that the catalog already returns modules in the order in which
    /// the dashboard should execute and display them.
    /// </summary>
    [Fact]
    public void CreateDefaultModules_ShouldReturnModulesInStableDisplayOrder()
    {
        var modules = DashboardModuleCatalog.CreateDefaultModules();

        var expectedOrder = modules
            .OrderBy(module => module.SortOrder)
            .ThenBy(module => module.Id, StringComparer.Ordinal)
            .Select(module => module.Id)
            .ToArray();

        var actualOrder = modules
            .Select(module => module.Id)
            .ToArray();

        Assert.Equal(expectedOrder, actualOrder);
    }

    /// <summary>
    /// Ensures that every module has the basic metadata needed for logging,
    /// diagnostics and later module management UI.
    /// </summary>
    [Fact]
    public void CreateDefaultModules_ShouldProvideReadableModuleMetadata()
    {
        var modules = DashboardModuleCatalog.CreateDefaultModules();

        Assert.All(modules, module =>
        {
            Assert.False(string.IsNullOrWhiteSpace(module.Id));
            Assert.False(string.IsNullOrWhiteSpace(module.DisplayName));
        });
    }
}
