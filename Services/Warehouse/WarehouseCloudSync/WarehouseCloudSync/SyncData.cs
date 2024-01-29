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
            try
            {
                int BranchID = 45;
                DateTime syncStartTime = DateTime.Now.AddMinutes(-5);
                WriteLine($"Warehouse sync started at {syncStartTime.ToLongTimeString()}");
                WarehouseRepository warehouseRepository = new WarehouseRepository();
                CloudRepository cloudRepository = new CloudRepository();
                DataTable dtEntity = cloudRepository.GetEntityData(BranchID, "ToCloud");
                foreach (DataRow entityRow in dtEntity.Rows)
                {
                    string entityName = entityRow["ENTITYNAME"].ToString();
                    DataTable dtEntityWiseData = warehouseRepository.GetEntityWiseData(entityName, entityRow["SYNCDATE"]);
                    WriteLine($"Found {dtEntityWiseData.Rows.Count} records to up sync in entity : {entityName} ");
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
                    WriteLine($"Found {dtEntityWiseData.Rows.Count} records to down sync in entity : {entityName} ");
                    if (dtEntityWiseData?.Rows.Count > 0)
                    {
                        warehouseRepository.SaveData(entityName, dtEntityWiseData);
                        cloudRepository.UpdateEntitySyncStatus(entityRow["ENTITYSYNCSTATUSID"], syncStartTime);
                    }
                }
                
                WriteLine("Proccesing Dayclosures started");
                warehouseRepository.ProccessDayClosures();
                WriteLine("Proccesing Dayclosures completed");

                WriteLine($"Warehouse sync completed");
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
                WriteLine(ex.StackTrace);
            }
            Thread.Sleep(5 * 60 * 1000);
            StartSync();
        }

        public static void WriteLine(string line)
        {
            Console.WriteLine(line);

            if (string.IsNullOrEmpty(logPath)) return;

            File.AppendAllText(logPath, $"{line}\n");
        }
    }
}
