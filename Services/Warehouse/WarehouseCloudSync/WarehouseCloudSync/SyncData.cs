using System;
using System.Data;
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
            DataTable dtEntity = warehouseRepository.GetEntityWiseData("ENTITY", BranchID);
            foreach (DataRow entityRow in dtEntity.Rows)
            {
                string entityName = entityRow["ENTITYNAME"].ToString();
                LoggerUtility.Logger.Info($"{entityName} sync started");
                DataTable dtEntityWiseData = warehouseRepository.GetEntityWiseData(entityName, BranchID);
                LoggerUtility.Logger.Info($"Found {dtEntityWiseData.Rows.Count} records to sync in entity : {entityName} ");
                cloudRepository.SaveData(entityName, dtEntityWiseData);
                warehouseRepository.UpdateEntitySyncStatus(entityName, BranchID
                    , syncStartTime);
                LoggerUtility.Logger.Info($"{entityName} sync completed");
            }

            LoggerUtility.Logger.Info($"Warehouse sync completed");
        }
    }
}
