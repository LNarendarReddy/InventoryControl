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
            DateTime syncStartTime = DateTime.Now;
            LoggerUtility.Logger.Info($"Warehouse sync started at {syncStartTime.ToLongTimeString()}");
            WarehouseRepository warehouseRepository = new WarehouseRepository();
            CloudRepository cloudRepository = new CloudRepository();
            DataTable dtEntity = warehouseRepository.GetEntityWiseData("ENTITY", 11);
            foreach (DataRow entityRow in dtEntity.Rows)
            {
                string entityName = entityRow["ENTITYNAME"].ToString();
                LoggerUtility.Logger.Info($"{entityName} sync started");
                DataTable dtEntityWiseData = warehouseRepository.GetEntityWiseData(entityName, 11);
                LoggerUtility.Logger.Info($"Found {dtEntityWiseData.Rows.Count} records to sync in entity : {entityName} ");
                cloudRepository.SaveData(entityName, dtEntityWiseData);
                warehouseRepository.UpdateEntitySyncStatus(entityName, 11, syncStartTime);
                LoggerUtility.Logger.Info($"{entityName} sync completed");
            }

            LoggerUtility.Logger.Info($"Warehouse sync completed");
        }
    }
}
