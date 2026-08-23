using System;
using System.Threading;
using System.Threading.Tasks;

namespace WarehouseCloudSync.Workers
{
    public class WarehouseSyncWorker
    {
        private readonly SyncData syncData;
        private readonly TimeSpan interval;

        public WarehouseSyncWorker()
            : this(new SyncData(), TimeSpan.FromMinutes(5))
        {
        }

        public WarehouseSyncWorker(SyncData syncData, TimeSpan interval)
        {
            if (syncData == null) throw new ArgumentNullException(nameof(syncData));
            this.syncData = syncData;
            this.interval = interval;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() => RunAsync(cancellationToken), cancellationToken);
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                syncData.ExecuteSyncCycle();

                try
                {
                    await Task.Delay(interval, cancellationToken).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}
