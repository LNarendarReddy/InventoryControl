using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WarehouseCloudSync
{
    public partial class NSRetailWareHouseCloudSync : ServiceBase
    {
        private ApplicationCoordinator coordinator;

        public NSRetailWareHouseCloudSync()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            coordinator = new ApplicationCoordinator();
            coordinator.Start();
        }

        protected override void OnStop()
        {
            coordinator?.Stop(TimeSpan.FromSeconds(30));
            coordinator?.Dispose();
            coordinator = null;
        }

    }
}
