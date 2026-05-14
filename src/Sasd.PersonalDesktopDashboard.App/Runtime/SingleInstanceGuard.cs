using System;
using System.Threading;
using Sasd.PersonalDesktopDashboard.Core.Logging;

namespace Sasd.PersonalDesktopDashboard.App.Runtime;

/// <summary>
/// Guards the desktop dashboard against multiple concurrently running application instances.
/// </summary>
/// <remarks>
/// <para>
/// The guard uses a named operating-system mutex. The first process that starts the
/// application acquires the mutex and keeps it until shutdown. A later process can
/// detect that the mutex is already owned and can then exit before creating a second
/// WPF window, a second tray icon, or competing settings/log file access.
/// </para>
/// <para>
/// This class intentionally does not activate the already running instance yet. That
/// would require a small inter-process communication mechanism and is better added in
/// a later, separate step if needed.
/// </para>
/// </remarks>
public sealed class SingleInstanceGuard : IDisposable
{
    /// <summary>
    /// Default mutex name used by the SASD Personal Desktop Dashboard.
    /// </summary>
    /// <remarks>
    /// The <c>Local\</c> prefix keeps the mutex scoped to the current Windows logon
    /// session. That is usually the right behavior for a desktop tray application:
    /// one instance per signed-in user session, not one instance for the whole machine.
    /// </remarks>
    private const string DefaultMutexName = @"Local\SASD.PersonalDesktopDashboard.SingleInstance";

    private readonly string mutexName;
    private readonly IAppLogger logger;

    private Mutex? mutex;
    private bool ownsMutex;
    private bool disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="SingleInstanceGuard"/> class.
    /// </summary>
    /// <param name="mutexName">The operating-system mutex name used to identify the running application.</param>
    /// <param name="logger">The logger used for diagnostic startup and shutdown information.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="mutexName"/> is empty.</exception>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="logger"/> is <see langword="null"/>.</exception>
    public SingleInstanceGuard(string mutexName, IAppLogger logger)
    {
        if (string.IsNullOrWhiteSpace(mutexName))
        {
            throw new ArgumentException("The mutex name must not be empty.", nameof(mutexName));
        }

        this.mutexName = mutexName;
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Gets a value indicating whether this process currently owns the single-instance mutex.
    /// </summary>
    public bool IsAcquired => ownsMutex;

    /// <summary>
    /// Creates the default single-instance guard for the desktop dashboard application.
    /// </summary>
    /// <param name="logger">The logger used for diagnostic startup and shutdown information.</param>
    /// <returns>A configured guard using the default dashboard mutex name.</returns>
    public static SingleInstanceGuard CreateDefault(IAppLogger logger)
    {
        return new SingleInstanceGuard(DefaultMutexName, logger);
    }

    /// <summary>
    /// Attempts to acquire the single-instance mutex for the current process.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> when this process is the primary application instance;
    /// otherwise, <see langword="false"/> when another instance is already running.
    /// </returns>
    public bool TryAcquire()
    {
        ThrowIfDisposed();

        if (ownsMutex)
        {
            // Calling TryAcquire more than once should be harmless for the owner.
            return true;
        }

        // The mutex object is created lazily. This keeps construction side-effect free
        // and makes the class easier to understand and test later.
        mutex ??= new Mutex(initiallyOwned: false, name: mutexName);

        try
        {
            ownsMutex = mutex.WaitOne(TimeSpan.Zero, exitContext: false);

            if (ownsMutex)
            {
                logger.Info($"Single-instance guard acquired mutex '{mutexName}'.");
            }
            else
            {
                logger.Warning($"Another application instance is already running; mutex '{mutexName}' is owned by another process.");
            }

            return ownsMutex;
        }
        catch (AbandonedMutexException exception)
        {
            // An abandoned mutex means a previous process ended without releasing it.
            // Windows transfers ownership to this process in that case, so we can safely
            // continue while logging the unusual condition for diagnostics.
            ownsMutex = true;
            logger.Warning($"Single-instance mutex '{mutexName}' was abandoned. Continuing with this process as the active instance. Details: {exception.Message}");
            return true;
        }
    }

    /// <summary>
    /// Releases the single-instance mutex when this process owns it.
    /// </summary>
    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        try
        {
            if (ownsMutex && mutex is not null)
            {
                mutex.ReleaseMutex();
                logger.Info($"Single-instance guard released mutex '{mutexName}'.");
            }
        }
        catch (ApplicationException exception)
        {
            // ReleaseMutex can throw if ownership was already lost. Logging is enough;
            // shutdown should not fail because the guard could not be released cleanly.
            logger.Error("Failed to release the single-instance mutex.", exception);
        }
        finally
        {
            ownsMutex = false;
            mutex?.Dispose();
            mutex = null;
            disposed = true;
        }
    }

    /// <summary>
    /// Throws an exception when the guard has already been disposed.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown when this guard has already been disposed.</exception>
    private void ThrowIfDisposed()
    {
        if (disposed)
        {
            throw new ObjectDisposedException(nameof(SingleInstanceGuard));
        }
    }
}
