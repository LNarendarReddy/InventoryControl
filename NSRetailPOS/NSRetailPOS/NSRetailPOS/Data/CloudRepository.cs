using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NSRetailPOS.Data
{
    public class CloudRepository
    {
        public DataTable GetEntityWiseData(object EntityName, object SyncDate, object BranchID)
        {
            DataTable dtEntity = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_GETSYNCDATA]";
                    cmd.Parameters.AddWithValue("@EntityName", EntityName);
                    cmd.Parameters.AddWithValue("@SyncDate", SyncDate);
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While Retreiving Entity wise data List - {EntityName}", ex);
            }
            finally
            {
                SQLCon.SqlCloudconn().Close();
            }
            return dtEntity;
        }

        public DataTable GetEntityData(object locationID, string syncDirection)
        {
            DataTable dtEntity = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_GETSYNC]";
                    cmd.Parameters.AddWithValue("@LocationID", locationID);
                    cmd.Parameters.AddWithValue("@LocationType", "BranchCounter");
                    cmd.Parameters.AddWithValue("@SyncDirection", syncDirection);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtEntity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retreiving Entity data List", ex);
            }
            finally
            {
                SQLCon.SqlCloudconn().Close();
            }
            return dtEntity;
        }

        public void UpdateEntitySyncStatus(object entitySyncStatusID, DateTime syncTime)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_ENTITYSYNCTIME]";
                    cmd.Parameters.AddWithValue("@EntitySyncStatusID", entitySyncStatusID);
                    cmd.Parameters.AddWithValue("@SyncTime", syncTime);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving Entity sync status", ex);
            }
            finally
            {
                SQLCon.SqlCloudconn().Close();
            }
        }

        Dictionary<string, EntityMapping> entityMapping = new Dictionary<string, EntityMapping>()
        {
            { "POS_BILL",  new EntityMapping("USP_CU_POS_BILL", "@Bills", true) }
            , { "POS_BILLDETAIL",  new EntityMapping("USP_CU_POS_BILLDETAIL", "@BillDetails", true) }
            , { "STOCKDISPATCH",  new EntityMapping("USP_CU_STOCKDISPATCH", "@StockDispatch") }
            , { "STOCKDISPATCHDETAIL",  new EntityMapping("USP_CU_STOCKDISPATCHDETAIL", "@StockDispatchDetail") }
            , { "USER",  new EntityMapping("USP_CU_USER", "@User") }
        };

        public void SaveData(string entityName, DataTable dtEntityWiseData)
        {
            if (dtEntityWiseData?.Rows.Count == 0)
            {
                return;
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    EntityMapping map = entityMapping[entityName];
                    cmd.CommandText = map.ProcedureName;
                    cmd.Parameters.AddWithValue(map.ParameterName, dtEntityWiseData);
                    if (map.IncludeBranchCounterID)
                    { 
                        cmd.Parameters.AddWithValue("@BranchCounterID", Utility.branchinfo.BranchCounterID); 
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving Entity wise data List", ex);
            }
            finally
            {
                SQLCon.SqlCloudconn().Close();
            }
        }
    }
}