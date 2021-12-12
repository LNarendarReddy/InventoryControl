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
        public NSRetailWareHouseCloudSync()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            new Thread(new ThreadStart(new SyncData().StartSync)).Start();
        }

        protected override void OnStop()
        {
        }

    }
}
