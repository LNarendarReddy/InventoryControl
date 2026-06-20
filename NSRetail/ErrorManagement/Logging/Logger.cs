using Serilog;
using System;
using System.IO;

namespace ErrorManagement
{
    public static class Logger
    {
        public static void Configure()
        {
            string logFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "NSRetail",
                "Logs");

            Directory.CreateDirectory(logFolder);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithEnvironmentUserName()
                .WriteTo.File(
                    path: Path.Combine(logFolder, "Application-.log"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    shared: true,
                    flushToDiskInterval: TimeSpan.FromSeconds(1),
                    outputTemplate:
                        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} " +
                        "[{Level:u3}] " +
                        "[Thread:{ThreadId}] " +
                        "{Message:lj}" +
                        "{NewLine}{Exception}")
                .CreateLogger();

            Log.Information("Logger initialized");
        }

        public static void Shutdown()
        {
            Log.Information("Application shutting down");
            Log.CloseAndFlush();
        }
    }
}
