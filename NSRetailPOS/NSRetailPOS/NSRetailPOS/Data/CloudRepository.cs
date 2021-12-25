using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailPOS.Data
{
    public class CloudRepository
    {
        public DataTable GetEntityWiseData(object EntityName, int branchCounterID)
        {
            DataTable dtEntity = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_GETSYNC]";
                    cmd.Parameters.AddWithValue("@EntityName", EntityName);
                    cmd.Parameters.AddWithValue("@BranchCounterID", branchCounterID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retreiving Entity wise data List", ex);
            }
            finally
            {
                SQLCon.SqlCloudconn().Close();
            }
            return dtEntity;
        }

        public void UpdateEntitySyncStatus(string entityName, int branchCounterID, DateTime syncTime)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_ENTITYSYNCTIME]";
                    cmd.Parameters.AddWithValue("@EntityName", entityName);
                    cmd.Parameters.AddWithValue("@BranchCounterID", branchCounterID);
                    cmd.Parameters.AddWithValue("@SyncTime", syncTime);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retreiving Entity wise data List", ex);
            }
            finally
            {
                SQLCon.SqlCloudconn().Close();
            }
        }
    }
}