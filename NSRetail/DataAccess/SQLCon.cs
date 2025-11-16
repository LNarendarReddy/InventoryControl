using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class SQLCon
    {
        static SqlConnection ObjCon = new SqlConnection();
        public static string BuildType = Convert.ToString(ConfigurationManager.AppSettings["BuildType"]);
        private static string SelectedDBName = null;
        public static string DBName { get; private set; }

        public static SqlConnection Sqlconn(int timeout = 1024)
        {             
            try
            {
                if (ObjCon?.State == ConnectionState.Open)
                {
                    return ObjCon;
                }
                else
                {
                    string ServerName = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}ServerName"].ToString());
                    string DBName = SelectedDBName ?? Decrypt(ConfigurationManager.AppSettings[$"{BuildType}DBName"].ToString());
                    string UserName = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}username"].ToString());
                    string Password = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}pwd"].ToString());

                    ObjCon = new SqlConnection();
                    string str = $"Data Source = {ServerName}; Initial Catalog = {DBName}; User Id = {UserName}; Password = {Password}; Pooling = True; Connect Timeout = {timeout}; Max Pool Size = 30000";
                    ObjCon.ConnectionString = str;
                    ObjCon.Open();
                    SQLCon.DBName = DBName;
                }
            }
            catch (Exception ex) { throw ex; }
            return ObjCon;
        }

        public static bool TryConn(Action<string> action, string connType = "Auto")
        {
            ClearConn(action);

            if (connType == "Auto")
            {
                if (CheckConn("ProdLAN", action)) return true;
                if (CheckConn("ProdWAN", action)) return true;
            }
            else
            {
                if(CheckConn(connType, action)) return true;
            }

            return false;
        }

        private static bool CheckConn(string connType, Action<string> action)
        {
            action.Invoke($"Checking {connType} sql connection");
            int timeout = 3000;
            bool isSuccess = false;
            BuildType = connType;
            Stopwatch sw = new Stopwatch();
            try
            {
                bool connectSuccess = false;
                // Try to open the connection, if anything goes wrong, make sure we set connectSuccess = false
                Thread t = new Thread(delegate ()
                {
                    try
                    {
                        sw.Start();
                        Sqlconn(3);
                        connectSuccess = true;
                    }
                    catch { }
                });

                // Make sure it's marked as a background thread so it'll get cleaned up automatically
                t.IsBackground = true;
                t.Start();

                // Keep trying to join the thread until we either succeed or the timeout value has been exceeded
                while (timeout > sw.ElapsedMilliseconds)
                    if (t.Join(1))
                        break;

                // If we didn't connect successfully, throw an exception
                if (!connectSuccess)
                    throw new Exception("Timed out while trying to connect.");

                isSuccess = true;
                action.Invoke($"{connType} sql connection successful");
            }
            catch 
            {
                action.Invoke($"{connType} sql connection failed");
            }
            finally
            {
                // clear connection as we created with timeout of 3 secs
                ClearConn(action);
            }

            return isSuccess;
        }

        private static void ClearConn(Action<string> action)
        {
            action.Invoke("Clearing existing sql connection");

            if (ObjCon?.State == ConnectionState.Open)
            {
                ObjCon.Close();
                ObjCon.Dispose();
                ObjCon = null;
            }

            action.Invoke("Sql connection cleared");
        }

        public static SqlConnection SqlCloudConn()
        {
            SqlConnection ObjCloudCon = new SqlConnection();
            string ServerName = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}CloudServerName"].ToString());
            string DBName = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}CloudDBName"].ToString());
            string UserName = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}Cloudusername"].ToString());
            string Password = Decrypt(ConfigurationManager.AppSettings[$"{BuildType}Cloudpwd"].ToString());
            try
            {
                if (ObjCloudCon.State == ConnectionState.Open)
                {
                    return ObjCloudCon;
                }
                else
                {
                    string str = $"Data Source = {ServerName}; Initial Catalog = {DBName}; User Id = {UserName}; Password = {Password}; Pooling = True; Connect Timeout = 1024; Max Pool Size = 200";
                    ObjCloudCon.ConnectionString = str;
                    ObjCloudCon.Open();
                }
            }
            catch (Exception ex) { throw ex; }
            return ObjCloudCon;
        }

        public static void ChangeConnection(string dbName)
        {
            SelectedDBName = dbName;
            ObjCon?.Close();
            ObjCon?.Dispose();
            ObjCon = null;
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
