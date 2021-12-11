using System;
using System.Data;
using WarehouseCloudSync.Data;

namespace WarehouseCloudSync
{
    public class SyncData
    {
        public void StartSync()
        {
            DateTime syncStartTime = DateTime.Now;
            WarehouseRepository warehouseRepository = new WarehouseRepository();
            CloudRepository cloudRepository = new CloudRepository();
            DataTable dtEntity = warehouseRepository.GetEntityWiseData("ENTITY", 11);
            foreach (DataRow entityRow in dtEntity.Rows)
            {
                string entityName = entityRow["ENTITYNAME"].ToString();
                DataTable dtEntityWiseData = warehouseRepository.GetEntityWiseData(entityName, 11);
                cloudRepository.SaveData(entityName, dtEntityWiseData);
                warehouseRepository.UpdateEntitySyncStatus(entityName, 11, syncStartTime);
            }
        }
    }
}
