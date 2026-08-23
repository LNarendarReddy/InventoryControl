using System;
using System.Threading;
using System.Threading.Tasks;

namespace WarehouseCloudSync.HomeDelivery
{
    public class HomeDeliveryExportWorker
    {
        private readonly HomeDeliveryExportRepository repository;
        private readonly HomeDeliveryExportService exportService;
        private int isRunning;

        public HomeDeliveryExportWorker()
            : this(new HomeDeliveryExportRepository(), new HomeDeliveryExportService())
        {
        }

        public HomeDeliveryExportWorker(HomeDeliveryExportRepository repository, HomeDeliveryExportService exportService)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (exportService == null) throw new ArgumentNullException(nameof(exportService));

            this.repository = repository;
            this.exportService = exportService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => RunAsync(cancellationToken), cancellationToken);
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            HomeDeliveryExportOptions options = LoadConfigSafely();

            if (options.RunOnStartup)
            {
                await TryExecuteAsync(options, cancellationToken).ConfigureAwait(false);
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromMinutes(options.IntervalMinutes), cancellationToken).ConfigureAwait(false);
                    options = LoadConfigSafely();
                    await TryExecuteAsync(options, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    SyncData.WriteHomeDeliveryLine($"Home Delivery worker error: {ex.Message}");
                }
            }
        }

        private HomeDeliveryExportOptions LoadConfigSafely()
        {
            try
            {
                HomeDeliveryExportOptions options = repository.GetHomeDeliveryConfig();
                SyncData.WriteHomeDeliveryLine($"Home Delivery configuration loaded. Enabled: {options.Enabled}, Interval minutes: {options.IntervalMinutes}");
                return options;
            }
            catch (Exception ex)
            {
                SyncData.WriteHomeDeliveryLine($"Home Delivery configuration load failed: {ex.Message}");
                return new HomeDeliveryExportOptions
                {
                    Enabled = false,
                    IntervalMinutes = 180,
                    RunOnStartup = false,
                    WorkingFolder = @"D:\HomeDelivery\Exports",
                    MainProcedureName = "USP_R_HOMEDELIVERY_EXPORT_DEFINITIONS",
                    CommandTimeoutSeconds = 3600,
                    DropboxRootFolder = "/HomeDelivery"
                };
            }
        }

        private async Task TryExecuteAsync(HomeDeliveryExportOptions options, CancellationToken cancellationToken)
        {
            if (!options.Enabled)
            {
                SyncData.WriteHomeDeliveryLine("Home Delivery worker is disabled by DB configuration.");
                return;
            }

            if (Interlocked.Exchange(ref isRunning, 1) == 1)
            {
                SyncData.WriteHomeDeliveryLine("Home Delivery scheduled run skipped because a previous execution is still active.");
                return;
            }

            try
            {
                await exportService.ExecuteAsync(options, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                SyncData.WriteHomeDeliveryLine("Home Delivery export cancellation requested.");
            }
            catch (Exception ex)
            {
                SyncData.WriteHomeDeliveryLine($"Home Delivery export worker isolated failure: {ex.Message}");
            }
            finally
            {
                Interlocked.Exchange(ref isRunning, 0);
            }
        }
    }
}
