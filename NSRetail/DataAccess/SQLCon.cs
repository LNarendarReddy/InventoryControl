using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess
{
    public static class SQLCon
    {
        public static SqlConnection Sqlconn()
        {
            SqlConnection ObjCon = new SqlConnection();
            string ServerName = Decrypt(ConfigurationManager.AppSettings["ServerName"].ToString());
            string DBName = Decrypt(ConfigurationManager.AppSettings["DBName"].ToString());
            string UserName = Decrypt(ConfigurationManager.AppSettings["username"].ToString());
            string Password = Decrypt(ConfigurationManager.AppSettings["pwd"].ToString());
            try
            {
                if (ObjCon.State == ConnectionState.Open)
                {
                    return ObjCon;
                }
                else
                {
                    string str = "Data Source = " + ServerName + "; Initial Catalog = " + DBName + "; User Id = " + UserName + "; Password = " + Password + "; Pooling = True; Connect Timeout = 1024; Max Pool Size = 200";
                    ObjCon.ConnectionString = str;
                    ObjCon.Open();
                }
            }
            catch (Exception ex) { throw ex; }
            return ObjCon;
        }

        public static SqlConnection SqlCloudConn()
        {
            SqlConnection ObjCloudCon = new SqlConnection();
            string ServerName = Decrypt(ConfigurationManager.AppSettings["CloudServerName"].ToString());
            string DBName = Decrypt(ConfigurationManager.AppSettings["CloudDBName"].ToString());
            string UserName = Decrypt(ConfigurationManager.AppSettings["Cloudusername"].ToString());
            string Password = Decrypt(ConfigurationManager.AppSettings["Cloudpwd"].ToString());
            try
            {
                if (ObjCloudCon.State == ConnectionState.Open)
                {
                    return ObjCloudCon;
                }
                else
                {
                    string str = "Data Source = " + ServerName + "; Initial Catalog = " + DBName + "; User Id = " + UserName + "; Password = " + Password + "; Pooling = True; Connect Timeout = 1024; Max Pool Size = 200";
                    ObjCloudCon.ConnectionString = str;
                    ObjCloudCon.Open();
                }
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
