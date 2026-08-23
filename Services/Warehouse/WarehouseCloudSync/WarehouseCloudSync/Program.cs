using System;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using WarehouseCloudSync.HomeDelivery;

namespace WarehouseCloudSync
{
    static class Program
    {
        static Mutex singleton = new Mutex(true, "Global\\WarehouseCloudSync");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {

            if (!singleton.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("Instance already running!!");
                return;
            }

            if (args != null && args.Length > 0 && string.Equals(args[0], "HomeDeliveryOnce", StringComparison.OrdinalIgnoreCase))
            {
                RunHomeDeliveryOnce();
                return;
            }

            using (ApplicationCoordinator coordinator = new ApplicationCoordinator())
            using (ManualResetEventSlim shutdownSignal = new ManualResetEventSlim(false))
            {
                Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    eventArgs.Cancel = true;
                    shutdownSignal.Set();
                };

                coordinator.Start();
                shutdownSignal.Wait();
                coordinator.Stop(TimeSpan.FromSeconds(30));
            }
        }

        private static void RunHomeDeliveryOnce()
        {
            try
            {
                SyncData.WriteHomeDeliveryLine("Home Delivery one-time run requested.");
                SyncData.WriteHomeDeliveryLine("Step 1/5: Loading Home Delivery configuration from DB.");
                HomeDeliveryExportRepository repository = new HomeDeliveryExportRepository();
                HomeDeliveryExportOptions options = repository.GetHomeDeliveryConfig();
                SyncData.WriteHomeDeliveryLine($"Step 1/5 completed. Enabled: {options.Enabled}, Working folder: {options.WorkingFolder}, Main proc: {options.MainProcedureName}");

                if (!options.Enabled)
                {
                    SyncData.WriteHomeDeliveryLine("Home Delivery worker is disabled by DB configuration.");
                    return;
                }

                SyncData.WriteHomeDeliveryLine("Step 2/5: Starting Home Delivery export cycle.");
                new HomeDeliveryExportService(repository, new CsvExportService())
                    .ExecuteAsync(options, CancellationToken.None)
                    .Wait();
                SyncData.WriteHomeDeliveryLine("Home Delivery one-time run completed.");
            }
            catch (Exception ex)
            {
                SyncData.WriteHomeDeliveryLine($"Home Delivery one-time run failed: {ex.Message}");
                if (ex.InnerException != null)
                {
                    SyncData.WriteHomeDeliveryLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }
    }
}
