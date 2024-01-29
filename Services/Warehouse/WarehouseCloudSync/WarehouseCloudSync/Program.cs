using System;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace WarehouseCloudSync
{
    static class Program
    {
        static Mutex singleton = new Mutex(true, "Global\\WarehouseCloudSync");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            if (!singleton.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("Instance already running!!");
                return;
            }

            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new NSRetailWareHouseCloudSync()
            //};
            //ServiceBase.Run(ServicesToRun);

            new SyncData().StartSync();
        }
    }
}
