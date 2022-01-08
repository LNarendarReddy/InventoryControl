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
            LoggerUtility.InitializeLogger();
            int BranchID = 45;
            DateTime syncStartTime = DateTime.Now;
            LoggerUtility.Logger.Info($"Warehouse sync started at {syncStartTime.ToLongTimeString()}");
            WarehouseRepository warehouseRepository = new WarehouseRepository();
            CloudRepository cloudRepository = new CloudRepository();
            DataTable dtEntity = cloudRepository.GetEntityData(BranchID, "ToCloud");
            foreach (DataRow entityRow in dtEntity.Rows)
            {
                string entityName = entityRow["ENTITYNAME"].ToString();
                LoggerUtility.Logger.Info($"{entityName} sync started");
                DataTable dtEntityWiseData = warehouseRepository.GetEntityWiseData(entityName, entityRow["SYNCDATE"]);
                LoggerUtility.Logger.Info($"Found {dtEntityWiseData.Rows.Count} records to sync in entity : {entityName} ");
                cloudRepository.SaveData(entityName, dtEntityWiseData);
                cloudRepository.UpdateEntitySyncStatus(entityRow["ENTITYSYNCSTATUSID"], syncStartTime);
                LoggerUtility.Logger.Info($"{entityName} sync completed");
            }

            // start down sync from cloud
             dtEntity = cloudRepository.GetEntityData(BranchID, "FromCloud");
            foreach (DataRow entityRow in dtEntity.Rows)
            {
                string entityName = entityRow["ENTITYNAME"].ToString();
                LoggerUtility.Logger.Info($"{entityName} sync started");
                DataTable dtEntityWiseData = cloudRepository.GetEntityWiseData(entityName, entityRow["SYNCDATE"]);
                LoggerUtility.Logger.Info($"Found {dtEntityWiseData.Rows.Count} records to sync in entity : {entityName} ");
                warehouseRepository.SaveData(entityName, dtEntityWiseData);
                cloudRepository.UpdateEntitySyncStatus(entityRow["ENTITYSYNCSTATUSID"], syncStartTime);
                LoggerUtility.Logger.Info($"{entityName} sync completed");
            }

            LoggerUtility.Logger.Info($"Warehouse sync completed");

            Thread.Sleep(5 * 60 * 1000);
            StartSync();
        }
    }
}
