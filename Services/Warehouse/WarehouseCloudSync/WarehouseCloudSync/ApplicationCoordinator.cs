using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WarehouseCloudSync.HomeDelivery;
using WarehouseCloudSync.Workers;

namespace WarehouseCloudSync
{
    public class ApplicationCoordinator : IDisposable
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly IList<Task> runningTasks = new List<Task>();
        private bool started;

        public void Start()
        {
            if (started) return;
            started = true;

            runningTasks.Add(new WarehouseSyncWorker().StartAsync(cancellationTokenSource.Token));

            // Home Delivery scheduling is temporarily disabled; retain startup code for re-enabling.
            // runningTasks.Add(new HomeDeliveryExportWorker().StartAsync(cancellationTokenSource.Token));
            // SyncData.WriteHomeDeliveryLine("Home Delivery worker started. Configuration will be loaded from DB.");
            SyncData.WriteHomeDeliveryLine("Home Delivery scheduled worker is disabled.");
        }

        public void Stop(TimeSpan timeout)
        {
            if (!started) return;

            cancellationTokenSource.Cancel();

            try
            {
                Task.WaitAll(runningTasks.Where(task => task != null).ToArray(), timeout);
            }
            catch (AggregateException ex)
            {
                foreach (Exception innerException in ex.Flatten().InnerExceptions)
                {
                    if (!(innerException is OperationCanceledException))
                    {
                        SyncData.WriteLine($"Application coordinator stop error: {innerException.Message}");
                    }
                }
            }
        }

        public void Dispose()
        {
            cancellationTokenSource.Dispose();
        }
    }
}
