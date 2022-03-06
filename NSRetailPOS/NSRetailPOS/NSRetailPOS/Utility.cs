using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using NSRetailPOS.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace NSRetailPOS
{
    public static class Utility
    {
        public static LoginInfo logininfo = new LoginInfo();
        public static BranchInfo branchinfo = new BranchInfo();
        public static Bill GetBill(DataSet dsBillDetails)
        {
            Bill billObj = new Bill();
            billObj.BillID = dsBillDetails.Tables["BILL"].Rows[0]["BILLID"];
            billObj.BillNumber = dsBillDetails.Tables["BILL"].Rows[0]["BILLNUMBER"];
            billObj.LastBilledAmount = dsBillDetails.Tables["BILL"].Rows[0]["LASTBILLEDAMOUNT"];
            billObj.LastBilledQuantity = dsBillDetails.Tables["BILL"].Rows[0]["LASTBILLEDQUANTITY"];
            billObj.Rounding = dsBillDetails.Tables["BILL"].Rows[0]["ROUNDING"];
            billObj.LastBillID = dsBillDetails.Tables["BILL"].Rows[0]["LASTBILLID"];
            billObj.dtBillDetails = dsBillDetails.Tables["BILLDETAILS"];
            if (dsBillDetails.Tables.Count > 2)
                billObj.dtMopValues = dsBillDetails.Tables["MOPDETAILS"];
            return billObj;
        }

        public static void StartSync(BackgroundWorker backgroundWorker, bool forceFullSync = false)
        {
            try
            {
                LoggerUtility.InitializeLogger();
                DateTime syncStartTime = DateTime.Now.AddMinutes(-15);
                //LoggerUtility.Logger.Info($"POS sync started at {syncStartTime.ToLongTimeString()}");
                backgroundWorker?.ReportProgress(0, $"POS sync started at {syncStartTime.ToLongTimeString()}");
                SyncRepository syncRepository = new SyncRepository();
                CloudRepository cloudRepository = new CloudRepository();
                DataTable dtEntity = cloudRepository.GetEntityData(branchinfo.BranchCounterID, "FromCloud");
                foreach (DataRow entityRow in dtEntity.Rows)
                {
                    string entityName = entityRow["ENTITYNAME"].ToString();
                    //LoggerUtility.Logger.Info($"{entityName} down sync started");
                    ReportText(backgroundWorker, $"{entityName} down sync started");
                    DataTable dtEntityWiseData = cloudRepository.GetEntityWiseData(
                        entityName, 
                        forceFullSync ? "01-01-1900" : entityRow["SYNCDATE"]
                        , branchinfo.BranchID);
                    ReportText(backgroundWorker, $"Found {dtEntityWiseData.Rows.Count} records to down sync in entity : {entityName} ");
                    syncRepository.SaveData(entityName, dtEntityWiseData);
                    cloudRepository.UpdateEntitySyncStatus(entityRow["ENTITYSYNCSTATUSID"], syncStartTime);
                    ReportText(backgroundWorker, $"{entityName} down sync completed");
                }

                // start up sync
                dtEntity = cloudRepository.GetEntityData(branchinfo.BranchCounterID, "ToCloud");
                foreach (DataRow entityRow in dtEntity.Rows)
                {
                    string entityName = entityRow["ENTITYNAME"].ToString();
                    ReportText(backgroundWorker, $"{entityName} up sync started");
                    DataTable dtEntityWiseData = syncRepository.GetEntityWiseData(entityName, entityRow["SYNCDATE"]);
                    ReportText(backgroundWorker, $"Found {dtEntityWiseData.Rows.Count} records to up sync in entity : {entityName} ");
                    cloudRepository.SaveData(entityName, dtEntityWiseData);
                    cloudRepository.UpdateEntitySyncStatus(entityRow["ENTITYSYNCSTATUSID"], syncStartTime);
                    ReportText(backgroundWorker, $"{entityName} up sync completed");
                }

                // clear old data
                ReportText(backgroundWorker, $"clearing one month old data");
                syncRepository.ClearOldData();

                //LoggerUtility.Logger.Info($"POS sync completed");
                backgroundWorker?.ReportProgress(0, $"POS sync completed at {DateTime.Now.ToLongTimeString()}");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error while running sync : {ex.Message} {Environment.NewLine} {ex.StackTrace}");
            }
        }
        private static byte[] Encrypt(byte[] input)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes("NSoftSol", new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 });
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
        private static byte[] Decrypt(byte[] input)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes("NSoftSol", new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 });
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
        public static string Encrypt(string input)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(input)));
        }
        public static string Decrypt(string input)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(input)));
        }

        private static void ReportText(BackgroundWorker bgwSyncWorker, string text)
        {
            string displayText = DateTime.Now.ToString() + " : " + text;
            bgwSyncWorker?.ReportProgress(0, displayText);
            if(bgwSyncWorker == null)
            {
                SplashScreenManager.Default.SetWaitFormDescription(displayText);
            }
        }

        public static string GetHDDSerialNumber()
        {
            ManagementObjectSearcher moSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject wmi_HD in moSearcher.Get())
            {
                object serialNo = wmi_HD["SerialNumber"];
                if (!string.IsNullOrEmpty(serialNo as string)) { return serialNo.ToString(); }
            }

            return string.Empty;
        }

        public static void PrintBarCode(object ItemCode, object ItemName,
            string SalePrice, object oQuantity, object MRP, object BatchNumber,
            object PackedDate, object CategoryID,object IsOpenCategory)
        {
            try
            {
                int Quantity = 0;
                if (int.TryParse(Convert.ToString(oQuantity), out Quantity))
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ItemCode", typeof(string));
                    dt.Columns.Add("ItemName", typeof(string));
                    dt.Columns.Add("SalePrice", typeof(string));
                    dt.Columns.Add("MRP", typeof(string));
                    DataRow dr = null;
                    for (int i = 0; i < Quantity; i++)
                    {
                        dr = dt.NewRow();
                        dr["ItemCode"] = ItemCode;
                        dr["ItemName"] = ItemName;
                        dr["SalePrice"] = SalePrice;
                        dr["MRP"] = MRP;
                        dt.Rows.Add(dr);
                    }
                    rptBarcode rpt = new rptBarcode();
                    rpt.DataSource = dt;
                    rpt.ShowPrintMarginsWarning = false;
                    rpt.Parameters["BatchNumber"].Value = BatchNumber;
                    rpt.Parameters["PackedDate"].Value = DateTime.Now.ToString("MM/yyyy");
                    rpt.Parameters["CategoryID"].Value = CategoryID;
                    rpt.Parameters["IsOpenCategory"].Value = IsOpenCategory;
                    rpt.CreateDocument();
                    ReportPrintTool printTool = new ReportPrintTool(rpt);
                    printTool.Print();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<int> AllLocateByValue(this GridView view, string fieldName, object value)
        {
            List<int> list = new List<int>();
            int rowHandle = view.LocateByValue(fieldName, value);
            while (rowHandle != GridControl.InvalidRowHandle)
            {
                list.Add(rowHandle);
                rowHandle = view.LocateByValue(rowHandle + 1, view.Columns[fieldName], value);
            }
            return list;
        }
    }
    
    public class LoginInfo
    {
        public object UserID { get; set; }
        public object UserName { get; set; }
        public object Password { get; set; }
        public object UserFullName { get; set; }
    }
    public class BranchInfo
    {
        public object BranchID { get; set; }
        public object BranchName { get; set; }
        public object BranchCode { get; set; }
        public object BranchAddress { get; set; }
        public object PhoneNumber { get; set; }
        public object LandLine { get; set; }
        public object GSTIN { get; set; }
        public object BranchCounterID { get; set; }
        public object BranchCounterName { get; set; }
    }
}
