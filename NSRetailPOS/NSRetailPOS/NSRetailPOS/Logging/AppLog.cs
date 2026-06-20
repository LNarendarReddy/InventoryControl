using Serilog;
using System;

namespace NSRetailPOS.Logging
{
    public static class AppLog
    {
        public static void Info(string message)
        {
            Log.Information(message);
        }

        public static void Warn(string message)
        {
            Log.Warning(message);
        }

        public static void Error(Exception ex, string message)
        {
            Log.Error(ex, message);
        }

        public static void Debug(string message)
        {
            Log.Debug(message);
        }
    }
}
