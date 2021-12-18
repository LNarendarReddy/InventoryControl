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
        public DataTable GetItemCodes()
        {
            DataTable dtItemCodes = new DataTable();
            SqlConnection conn = SQLCon.Sqlconn();
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
            SqlConnection conn = SQLCon.Sqlconn();
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

        public DataTable GetMRPList(object ITEMCODEID)
        {
            DataTable dtItemCodes = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMMRPLIST]";
                    cmd.Parameters.AddWithValue("@ITEMCODEID", ITEMCODEID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItemCodes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving MRP List", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtItemCodes;
        }
    }
}
