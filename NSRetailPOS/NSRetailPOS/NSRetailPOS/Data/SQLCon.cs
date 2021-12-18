using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NSRetailPOS.Data
{
    public static class SQLCon
    {
        private static SqlConnection connection = new SqlConnection();

        public static SqlConnection Sqlconn()
        {
            try
            {
                if (connection.State == ConnectionState.Open)
                {
                    return connection;
                }

                string str = ConfigurationManager.AppSettings["Connection"].ToString();
                connection.ConnectionString = str;
                connection.Open();
            }
            catch (Exception ex) { throw ex; }
            return connection;
        }
    }
}
