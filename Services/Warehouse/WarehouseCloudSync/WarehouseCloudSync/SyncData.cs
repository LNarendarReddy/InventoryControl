using System;
using System.Data;
using System.Threading;
using WarehouseCloudSync.Data;

namespace WarehouseCloudSync
{
    public class SyncData
    {
        public void StartSync()
        {
            try
            {
                LoggerUtility.InitializeLogger();
                int BranchID = 45;
                DateTime syncStartTime = DateTime.Now.AddMinutes(-5);
                //LoggerUtility.Logger.Info($"Warehouse sync started at {syncStartTime.ToLongTimeString()}");
                //Console.Clear();
                Console.WriteLine($"Warehouse sync started at {syncStartTime.ToLongTimeString()}");
                WarehouseRepository warehouseRepository = new WarehouseRepository();
                CloudRepository cloudRepository = new CloudRepository();
                DataTable dtEntity = cloudRepository.GetEntityData(BranchID, "ToCloud");
                foreach (DataRow entityRow in dtEntity.Rows)
                {
                    string entityName = entityRow["ENTITYNAME"].ToString();
                    //LoggerUtility.Logger.Info($"{entityName} up sync started");
                    //Console.WriteLine($"{entityName} up sync started");
                    DataTable dtEntityWiseData = warehouseRepository.GetEntityWiseData(entityName, entityRow["SYNCDATE"]);
                    //LoggerUtility.Logger.Info($"Found {dtEntityWiseData.Rows.Count} records to up sync in entity : {entityName} ");
                    Console.WriteLine($"Found {dtEntityWiseData.Rows.Count} records to up sync in entity : {entityName} ");
                    if (dtEntityWiseData?.Rows.Count > 0)
                    {
                        cloudRepository.SaveData(entityName, dtEntityWiseData);
                        cloudRepository.UpdateEntitySyncStatus(entityRow["ENTITYSYNCSTATUSID"], syncStartTime);
                    }
                    //LoggerUtility.Logger.Info($"{entityName} up sync completed");
                    //Console.WriteLine($"{entityName} up sync completed");
                }

                // start down sync from cloud
                dtEntity = cloudRepository.GetEntityData(BranchID, "FromCloud");
                foreach (DataRow entityRow in dtEntity.Rows)
                {
                    string entityName = entityRow["ENTITYNAME"].ToString();
                    //LoggerUtility.Logger.Info($"{entityName} down sync started");
                    //Console.WriteLine($"{entityName} down sync started");
                    DataTable dtEntityWiseData = cloudRepository.GetEntityWiseData(entityName, entityRow["SYNCDATE"]);
                    //LoggerUtility.Logger.Info($"Found {dtEntityWiseData.Rows.Count} records to down sync in entity : {entityName} ");
                    Console.WriteLine($"Found {dtEntityWiseData.Rows.Count} records to down sync in entity : {entityName} ");
                    if (dtEntityWiseData?.Rows.Count > 0)
                    {
                        warehouseRepository.SaveData(entityName, dtEntityWiseData);
                        cloudRepository.UpdateEntitySyncStatus(entityRow["ENTITYSYNCSTATUSID"], syncStartTime);
                    }
                    //LoggerUtility.Logger.Info($"{entityName} down sync completed");
                    //Console.WriteLine($"{entityName} down sync completed");
                }

                //LoggerUtility.Logger.Info($"Warehouse sync completed");
                Console.WriteLine($"Warehouse sync completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.ReadLine();
            }
            Thread.Sleep(5 * 60 * 1000);
            StartSync();
        }
    }
}
