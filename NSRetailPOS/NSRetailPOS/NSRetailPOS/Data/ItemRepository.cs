using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailPOS.Data
{
    public class ItemRepository
    {
        public SqlConnection Sqlconn()
        {
            SqlConnection ObjCon = new SqlConnection();

            try
            {
                string str = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\NSRetailPOS.mdf;Integrated security=true";
                ObjCon.ConnectionString = str;
                ObjCon.Open();
            }
            catch (Exception ex) { throw ex; }
            return ObjCon;
        }

        public DataTable GetItemCodes()
        {
            DataTable dtItemCodes = new DataTable();
            SqlConnection conn = Sqlconn();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMCODES]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItemCodes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item codes List", ex);
            }
            finally
            {
                conn.Close();
            }
            return dtItemCodes;
        }

        public DataTable GetMOPs()
        {
            DataTable dtItemCodes = new DataTable();
            SqlConnection conn = Sqlconn();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_MOP]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItemCodes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Mode of payments List", ex);
            }
            finally
            {
                conn.Close();
            }
            return dtItemCodes;
        }
    }
}
