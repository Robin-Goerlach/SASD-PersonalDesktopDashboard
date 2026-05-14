using System;
using Sasd.PersonalDesktopDashboard.Core.Logging;

namespace Sasd.PersonalDesktopDashboard.App.Logging;

/// <summary>
/// Provides application-wide access to the currently configured logger.
/// </summary>
/// <remarks>
/// This small static holder keeps the current patch simple. It avoids introducing
/// a full dependency injection container just for logging, but still prevents the
/// UI code from directly constructing the file logger everywhere.
/// </remarks>
public static class ApplicationLogger
{
    private static readonly object syncRoot = new();
    private static IAppLogger current = NullAppLogger.Instance;

    /// <summary>
    /// Gets the currently configured application logger.
    /// </summary>
    public static IAppLogger Current
    {
        get
        {
            lock (syncRoot)
            {
                return current;
            }
        }
    }

    /// <summary>
    /// Configures the application-wide logger instance.
    /// </summary>
    /// <param name="logger">The logger that should be used by application code.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger"/> is <see langword="null"/>.</exception>
    public static void Configure(IAppLogger logger)
    {
        if (logger is null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        lock (syncRoot)
        {
            current = logger;
        }
    }

    /// <summary>
    /// Resets the application logger to the safe no-operation fallback.
    /// </summary>
    public static void Reset()
    {
        lock (syncRoot)
        {
            current = NullAppLogger.Instance;
        }
    }
}
