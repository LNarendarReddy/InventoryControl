using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using WarehouseCloudSync.Data;

namespace WarehouseCloudSync
{
    public class SyncData
    {
        static string logPath = null;

        public SyncData() 
        {
            logPath = ConfigurationManager.AppSettings.Keys.Cast<string>().Any(x => x == "TelemetryLogFile")
                ? ConfigurationManager.AppSettings["TelemetryLogFile"].ToString()
                : null;

            if(!string.IsNullOrEmpty(logPath) && !File.Exists(logPath))
            {
                File.AppendAllText(logPath, "File created \n");
            }
        }
        public void StartSync()
        {
            while (true)
            {
                ExecuteSyncCycle();
                Thread.Sleep(5 * 60 * 1000);
            }
        }

        public void ExecuteSyncCycle()
        {
            try
            {
                int BranchID = 45;
                DateTime syncStartTime = DateTime.Now.AddMinutes(-5);
                WriteWarehouseLine($"Warehouse sync started at {syncStartTime.ToLongTimeString()}");
                WarehouseRepository warehouseRepository = new WarehouseRepository();
                CloudRepository cloudRepository = new CloudRepository();
                DataTable dtEntity = cloudRepository.GetEntityData(BranchID, "ToCloud");

                //do stock moves so that item price changes can be picked before sync starts
                WriteWarehouseLine("Proccesing stock move started");
                warehouseRepository.ProccessStockMove();
                WriteWarehouseLine("Proccesing stock move completed");

                foreach (DataRow entityRow in dtEntity.Rows)
                {
                    string entityName = entityRow["ENTITYNAME"].ToString();
                    DataTable dtEntityWiseData = warehouseRepository.GetEntityWiseData(entityName, entityRow["SYNCDATE"]);
                    WriteWarehouseLine($"Found {dtEntityWiseData.Rows.Count} records to up sync in entity : {entityName} ");
                    if (dtEntityWiseData?.Rows.Count > 0)
                    {
                        cloudRepository.SaveData(entityName, dtEntityWiseData);
                        cloudRepository.UpdateEntitySyncStatus(entityRow["ENTITYSYNCSTATUSID"], syncStartTime);
                    }
                }

                // start down sync from cloud
                dtEntity = cloudRepository.GetEntityData(BranchID, "FromCloud");
                foreach (DataRow entityRow in dtEntity.Rows)
                {
                    string entityName = entityRow["ENTITYNAME"].ToString();
                    DataTable dtEntityWiseData = cloudRepository.GetEntityWiseData(entityName, entityRow["SYNCDATE"]);
                    WriteWarehouseLine($"Found {dtEntityWiseData.Rows.Count} records to down sync in entity : {entityName} ");
                    if (dtEntityWiseData?.Rows.Count > 0)
                    {
                        warehouseRepository.SaveData(entityName, dtEntityWiseData);
                        cloudRepository.UpdateEntitySyncStatus(entityRow["ENTITYSYNCSTATUSID"], syncStartTime);
                    }
                }
                
                WriteWarehouseLine("Proccesing Dayclosures started");
                warehouseRepository.ProccessDayClosures();
                WriteWarehouseLine("Proccesing Dayclosures completed");

                WriteWarehouseLine($"Warehouse sync completed");
            }
            catch (Exception ex)
            {
                WriteWarehouseLine(ex.Message);
                WriteWarehouseLine(ex.StackTrace);
            }
        }

        public static void WriteWarehouseLine(string line)
        {
            WriteLine("WH_SYNC", line);
        }

        public static void WriteHomeDeliveryLine(string line)
        {
            WriteLine("HOME_DELIVERY", line);
        }

        public static void WriteLine(string line)
        {
            WriteLine("SYSTEM", line);
        }

        private static void WriteLine(string prefix, string line)
        {
            string formattedLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{prefix}] {line}";
            Console.WriteLine(formattedLine);

            if (string.IsNullOrEmpty(logPath)) return;

            File.AppendAllText(logPath, $"{formattedLine}\n");
        }
    }
}
