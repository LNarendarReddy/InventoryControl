using log4net;

namespace WarehouseCloudSync
{
    internal static class LoggerUtility
    {
        public static ILog Logger { get; private set; }

        public static void InitializeLogger()
        {
            if (LogManager.GetCurrentLoggers().Length == 0)
            {
                log4net.Config.BasicConfigurator.Configure();
            }
            Logger = LogManager.GetLogger(typeof(Program)); // OR logger = log4net.LogManager.GetLogger(typeof(LoggerUtility));
            Logger.Info("Begin processing");
        }
    }
}
