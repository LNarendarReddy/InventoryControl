using Serilog;
using System;
using System.Diagnostics;
using System.IO;

namespace NSRetailPOS.Logging
{
    internal static class LoginTiming
    {
        internal static void Measure(string stage, Action action) => Measure(stage, () =>
        {
            action();
            return true;
        });

        internal static T Measure<T>(string stage, Func<T> action)
        {
            var timer = Stopwatch.StartNew();
            Log.Information("POS login stage {Stage} started", stage);
            try
            {
                T result = action();
                Log.Information("POS login stage {Stage} finished in {ElapsedMs} ms", stage, timer.ElapsedMilliseconds);
                return result;
            }
            catch (Exception ex)
            {
                Log.Warning("POS login stage {Stage} failed after {ElapsedMs} ms ({ErrorType})",
                    stage, timer.ElapsedMilliseconds, ex.GetType().Name);
                throw;
            }
        }
    }

    public static class Logger
    {
        public static void Configure()
        {
            string logFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "NSRetailPOS",
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
