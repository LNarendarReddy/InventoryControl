using System.ServiceProcess;

namespace WarehouseCloudSync
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new NSRetailWareHouseCloudSync()
            };
            ServiceBase.Run(ServicesToRun);

            //new SyncData().StartSync();
        }
    }
}
