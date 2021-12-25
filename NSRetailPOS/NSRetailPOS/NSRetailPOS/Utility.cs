using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailPOS
{
    public static class Utility
    {
        public static Bill GetBill(DataSet dsBillDetails)
        {
            Bill billObj = new Bill();
            billObj.BillID = dsBillDetails.Tables["BILL"].Rows[0]["BILLID"];
            billObj.BillNumber = dsBillDetails.Tables["BILL"].Rows[0]["BILLNUMBER"];
            billObj.LastBilledAmount = dsBillDetails.Tables["BILL"].Rows[0]["LASTBILLEDAMOUNT"];
            billObj.LastBilledQuantity = dsBillDetails.Tables["BILL"].Rows[0]["LASTBILLEDQUANTITY"];

            billObj.dtBillDetails = dsBillDetails.Tables["BILLDETAILS"];
            return billObj;
        }

        public static void StartSync()
        {
            LoggerUtility.InitializeLogger();
            int BranchCounterID = 3;
            DateTime syncStartTime = DateTime.Now;
            LoggerUtility.Logger.Info($"POS sync started at {syncStartTime.ToLongTimeString()}");
            SyncRepository syncRepository = new SyncRepository();
            CloudRepository cloudRepository = new CloudRepository();
            DataTable dtEntity = cloudRepository.GetEntityWiseData("ENTITY", BranchCounterID);
            foreach (DataRow entityRow in dtEntity.Rows)
            {
                string entityName = entityRow["ENTITYNAME"].ToString();
                LoggerUtility.Logger.Info($"{entityName} sync started");
                DataTable dtEntityWiseData = cloudRepository.GetEntityWiseData(entityName, BranchCounterID);
                LoggerUtility.Logger.Info($"Found {dtEntityWiseData.Rows.Count} records to sync in entity : {entityName} ");
                syncRepository.SaveData(entityName, dtEntityWiseData);
                cloudRepository.UpdateEntitySyncStatus(entityName, BranchCounterID
                    , syncStartTime);
                LoggerUtility.Logger.Info($"{entityName} sync completed");
            }

            LoggerUtility.Logger.Info($"POS sync completed");
        }
    }
}
