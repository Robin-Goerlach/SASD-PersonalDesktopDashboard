using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Sasd.PersonalDesktopDashboard.Core.Logging;

namespace Sasd.PersonalDesktopDashboard.Infrastructure.Logging;

/// <summary>
/// Writes application log messages to a plain text file.
/// </summary>
/// <remarks>
/// This implementation is intentionally small and dependency-free. It is designed
/// for the early technical foundation of the desktop dashboard, where we want
/// useful diagnostics without introducing Serilog, NLog, Microsoft.Extensions.Logging
/// configuration, or another larger logging stack.
/// </remarks>
public sealed class FileAppLogger : IAppLogger
{
    private readonly object syncRoot = new();
    private readonly FileAppLoggerOptions options;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileAppLogger"/> class.
    /// </summary>
    /// <param name="options">The file logger configuration.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="options"/> is <see langword="null"/>.</exception>
    public FileAppLogger(FileAppLoggerOptions options)
    {
        this.options = options ?? throw new ArgumentNullException(nameof(options));

        if (string.IsNullOrWhiteSpace(this.options.LogFilePath))
        {
            throw new ArgumentException("The log file path must not be empty.", nameof(options));
        }
    }

    /// <inheritdoc />
    public void Info(string message)
    {
        Write("INFO", message, exception: null);
    }

    /// <inheritdoc />
    public void Warning(string message)
    {
        Write("WARN", message, exception: null);
    }

    /// <inheritdoc />
    public void Error(string message)
    {
        Write("ERROR", message, exception: null);
    }

    /// <inheritdoc />
    public void Error(string message, Exception exception)
    {
        Write("ERROR", message, exception);
    }

    /// <summary>
    /// Writes one log entry to the configured log file.
    /// </summary>
    /// <param name="level">The textual log level, for example INFO, WARN or ERROR.</param>
    /// <param name="message">The message to write.</param>
    /// <param name="exception">Optional exception details.</param>
    private void Write(string level, string message, Exception? exception)
    {
        try
        {
            // Logging can be called from different UI or background operations later.
            // The lock keeps individual log entries together and prevents concurrent
            // file write conflicts inside this small logger implementation.
            lock (syncRoot)
            {
                EnsureLogDirectoryExists();
                RotateLogFileIfRequired();

                string logEntry = FormatLogEntry(level, message, exception);

                // UTF-8 without BOM keeps the log file readable in modern editors.
                File.AppendAllText(options.LogFilePath, logEntry, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            }
        }
        catch (Exception writeException)
        {
            // Logging must never crash the actual desktop application. If the log
            // file cannot be written, we only emit a debug message for Visual Studio.
            Debug.WriteLine($"Logging failed: {writeException}");
        }
    }

    /// <summary>
    /// Ensures that the target directory for the log file exists.
    /// </summary>
    private void EnsureLogDirectoryExists()
    {
        string? logDirectory = Path.GetDirectoryName(options.LogFilePath);

        if (!string.IsNullOrWhiteSpace(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }
    }

    /// <summary>
    /// Rotates the active log file when it grows beyond the configured maximum size.
    /// </summary>
    private void RotateLogFileIfRequired()
    {
        if (options.MaxFileSizeBytes <= 0)
        {
            // A value of zero or below disables rotation explicitly.
            return;
        }

        if (!File.Exists(options.LogFilePath))
        {
            return;
        }

        FileInfo logFileInfo = new(options.LogFilePath);

        if (logFileInfo.Length < options.MaxFileSizeBytes)
        {
            return;
        }

        int retainedFileCount = Math.Max(1, options.RetainedFileCount);

        // Shift old rotated files upwards:
        // app.2.log becomes app.3.log, app.1.log becomes app.2.log, and so on.
        for (int index = retainedFileCount - 1; index >= 1; index--)
        {
            string sourcePath = BuildRotatedLogFilePath(index);
            string targetPath = BuildRotatedLogFilePath(index + 1);

            if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, targetPath, overwrite: true);
                File.Delete(sourcePath);
            }
        }

        // Move the active log to app.1.log and start with a fresh app.log file.
        string firstRotatedLogFile = BuildRotatedLogFilePath(1);
        File.Copy(options.LogFilePath, firstRotatedLogFile, overwrite: true);
        File.Delete(options.LogFilePath);
    }

    /// <summary>
    /// Builds the file path for a rotated log file.
    /// </summary>
    /// <param name="rotationIndex">The rotation index, where 1 is the newest rotated file.</param>
    /// <returns>The full path of the rotated log file.</returns>
    private string BuildRotatedLogFilePath(int rotationIndex)
    {
        string directory = Path.GetDirectoryName(options.LogFilePath) ?? string.Empty;
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(options.LogFilePath);
        string extension = Path.GetExtension(options.LogFilePath);
        string rotatedFileName = $"{fileNameWithoutExtension}.{rotationIndex}{extension}";

        return Path.Combine(directory, rotatedFileName);
    }

    /// <summary>
    /// Formats one complete log entry.
    /// </summary>
    /// <param name="level">The log level text.</param>
    /// <param name="message">The log message.</param>
    /// <param name="exception">Optional exception details.</param>
    /// <returns>A formatted multi-line log entry ending with a line break.</returns>
    private static string FormatLogEntry(string level, string message, Exception? exception)
    {
        // Local time with offset is easier to read during manual desktop debugging
        // than pure UTC, while the offset still makes the timestamp unambiguous.
        string timestamp = DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff zzz");
        string safeMessage = NormalizeMessage(message);

        StringBuilder builder = new();
        builder.Append(timestamp);
        builder.Append(" [");
        builder.Append(level);
        builder.Append("] ");
        builder.AppendLine(safeMessage);

        if (exception is not null)
        {
            builder.AppendLine(IndentMultilineText(exception.ToString()));
        }

        return builder.ToString();
    }

    /// <summary>
    /// Normalizes a possibly empty or multi-line message for log output.
    /// </summary>
    /// <param name="message">The original log message.</param>
    /// <returns>A safe message string.</returns>
    private static string NormalizeMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return "(empty log message)";
        }

        return message.Trim();
    }

    /// <summary>
    /// Indents multi-line text so exception details remain visually grouped below the log entry.
    /// </summary>
    /// <param name="text">The text that should be indented.</param>
    /// <returns>The indented text.</returns>
    private static string IndentMultilineText(string text)
    {
        string normalizedText = text.Replace("\r\n", "\n").Replace("\r", "\n");
        string[] lines = normalizedText.Split('\n');

        StringBuilder builder = new();

        foreach (string line in lines)
        {
            builder.Append("    ");
            builder.AppendLine(line);
        }

        return builder.ToString();
    }
}
