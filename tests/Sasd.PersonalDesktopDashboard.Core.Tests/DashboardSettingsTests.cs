using Sasd.PersonalDesktopDashboard.Core.Configuration;
using Sasd.PersonalDesktopDashboard.Core.Models;

namespace Sasd.PersonalDesktopDashboard.Core.Tests;

/// <summary>
/// Tests for the default dashboard settings.
/// </summary>
public sealed class DashboardSettingsTests
{
    /// <summary>
    /// Ensures that a new settings object contains safe default values.
    /// </summary>
    [Fact]
    public void NewSettings_ShouldContainSafeDefaults()
    {
        var settings = new DashboardSettings();

        Assert.Equal(DashboardDisplayMode.Dashboard, settings.PreferredDisplayMode);
        Assert.False(settings.PrivacyModeEnabled);
        Assert.True(settings.RefreshIntervalSeconds >= 60);
        Assert.NotEmpty(settings.ThemeName);
        Assert.NotEmpty(settings.DisplayProfiles);
    }

    /// <summary>
    /// Ensures that the default settings already know the two most important usage scenarios.
    /// </summary>
    [Fact]
    public void NewSettings_ShouldContainLaptopAndDockingProfiles()
    {
        var settings = new DashboardSettings();

        Assert.Contains(settings.DisplayProfiles, profile => profile.PreferredDisplayMode == DashboardDisplayMode.Compact);
        Assert.Contains(settings.DisplayProfiles, profile => profile.PreferredDisplayMode == DashboardDisplayMode.Wallboard);
    }
}
