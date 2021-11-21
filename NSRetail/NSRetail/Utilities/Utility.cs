using DataAccess;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NSRetail
{
    public static class Utility
    {
        private static DataTable dtGST;

        public static int UserID = 4;
        public static string UserName = string.Empty;
        public static string Password = string.Empty;
        public static string FullName = string.Empty;
        public static string Role = string.Empty;
        public static string Category = string.Empty;
        public static string Branch = string.Empty;
        public static string ReportingLead = string.Empty;
        public static int RoleID = 0;
        public static int ReportingLeadID = 0;
        public static int CategoryID = 0;
        public static int BranchID = 0;
        public static string Email = string.Empty;


        public static void Setfocus(GridView view, string ColumnName, object Value)
        {
            try
            {
                int rowHandle = view.LocateByValue(ColumnName, Value);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                    view.FocusedRowHandle = rowHandle;
            }
            catch (Exception ex){}
        }
        private static  byte[] Encrypt(byte[] input)
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

        public static DataTable GetGSTBaseline()
        {
            return dtGST = dtGST ?? new MasterRepository().GetGST();
        }
    }
}
