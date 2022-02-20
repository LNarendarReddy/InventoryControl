using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class CloudRepository
    {
        public DataTable GetSyncStatus()
        {
            DataTable dtSyncStatus = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_SHOWSYNC]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtSyncStatus);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While sync status from cloud database", ex);
            }
            finally
            {
                SQLCon.SqlCloudConn().Close();
            }
            return dtSyncStatus;
        }
        public void DeleteSyncStatus(object LocationID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_SYNCSTATUS]";
                    cmd.Parameters.AddWithValue("@LocationID", LocationID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While deleteing sync status from cloud database", ex);
            }
            finally
            {
                SQLCon.SqlCloudConn().Close();
            }
        }
    }
}
