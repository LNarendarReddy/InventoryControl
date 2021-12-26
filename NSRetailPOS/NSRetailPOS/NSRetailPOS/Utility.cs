using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
            billObj.LastBillID = dsBillDetails.Tables["BILL"].Rows[0]["LASTBILLID"];
            billObj.dtBillDetails = dsBillDetails.Tables["BILLDETAILS"];
            if (dsBillDetails.Tables.Count > 2)
                billObj.dtMopValues = dsBillDetails.Tables["MOPDETAILS"];
            return billObj;
        }

        public static void StartSync(BackgroundWorker backgroundWorker)
        {
            LoggerUtility.InitializeLogger();
            int BranchCounterID = 3;
            DateTime syncStartTime = DateTime.Now;
            LoggerUtility.Logger.Info($"POS sync started at {syncStartTime.ToLongTimeString()}");
            backgroundWorker.ReportProgress(0, $"POS sync started at {syncStartTime.ToLongTimeString()}");
            SyncRepository syncRepository = new SyncRepository();
            CloudRepository cloudRepository = new CloudRepository();
            DataTable dtEntity = cloudRepository.GetEntityWiseData("ENTITY", BranchCounterID);
            foreach (DataRow entityRow in dtEntity.Rows)
            {
                string entityName = entityRow["ENTITYNAME"].ToString();
                //LoggerUtility.Logger.Info($"{entityName} sync started");
                ReportText(backgroundWorker, $"{entityName} sync started");
                DataTable dtEntityWiseData = cloudRepository.GetEntityWiseData(entityName, BranchCounterID);
                ReportText(backgroundWorker, $"Found {dtEntityWiseData.Rows.Count} records to sync in entity : {entityName} ");
                syncRepository.SaveData(entityName, dtEntityWiseData);
                cloudRepository.UpdateEntitySyncStatus(entityName, BranchCounterID
                    , syncStartTime);
                ReportText(backgroundWorker, $"{entityName} sync completed");
            }

            //LoggerUtility.Logger.Info($"POS sync completed");
            backgroundWorker.ReportProgress(0, $"POS sync completed at {DateTime.Now.ToLongTimeString()}");
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
            bgwSyncWorker.ReportProgress(0, DateTime.Now.ToString() + " : " + text);
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
