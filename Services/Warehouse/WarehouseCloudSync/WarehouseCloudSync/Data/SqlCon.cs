﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WarehouseCloudSync.Data
{
    public static class SqlCon
    {
        private static SqlConnection ObjWHCon = null;
        private static SqlConnection ObjCloudCon = null;
        static string BuildType = Convert.ToString(ConfigurationManager.AppSettings["BuildType"]);

        public static SqlConnection SqlWHconn()
        {
            if (ObjWHCon?.State == ConnectionState.Open)
            {
                return ObjWHCon;
            } 

            ObjWHCon = new SqlConnection();
            string ServerName = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}WHServerName"].ToString());
            string DBName = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}WHDBName"].ToString());
            string UserName = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}WHusername"].ToString());
            string Password = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}WHpwd"].ToString());
            try
            {
                string str = "Data Source = " + ServerName + "; Initial Catalog = " + DBName + "; User Id = " + UserName + "; Password = " + Password + "; Pooling = True; Connect Timeout = 1024; Max Pool Size = 200";
                ObjWHCon.ConnectionString = str;
                ObjWHCon.Open();
            }
            catch (Exception ex) { throw ex; }
            return ObjWHCon;
        }

        public static SqlConnection SqlCloudconn()
        {
            if (ObjCloudCon?.State == ConnectionState.Open)
            {
                return ObjCloudCon;
            }

            ObjCloudCon = new SqlConnection();
            string ServerName = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}CloudServerName"].ToString());
            string DBName = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}CloudDBName"].ToString());
            string UserName = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}Cloudusername"].ToString());
            string Password = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}Cloudpwd"].ToString());
            try
            {
                string str = "Data Source = " + ServerName + "; Initial Catalog = " + DBName + "; User Id = " + UserName + "; Password = " + Password + "; Pooling = True; Connect Timeout = 1024; Max Pool Size = 200";
                ObjCloudCon.ConnectionString = str;
                ObjCloudCon.Open();
            }
            catch (Exception ex) { throw ex; }
            return ObjCloudCon;
        }

        public static string Decrypt(string input)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(input)));
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
    }
}
