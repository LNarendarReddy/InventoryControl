using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WarehouseCloudSync.Data
{
    public class CloudRepository
    {
        Dictionary<string, EntityMapping> entityMapping = new Dictionary<string, EntityMapping>()
        {
            { "ITEM",  new EntityMapping("USP_CU_ITEM", "@Items") }
            , { "ITEMCODE",  new EntityMapping("USP_CU_ITEMCODE", "@ItemCodes") }
            , { "ITEMPRICE",  new EntityMapping("USP_CU_ITEMPRICE", "@ItemPrices") }
            , { "BRANCH",  new EntityMapping("USP_CU_BRANCH", "@Branches") }
            , { "GST",  new EntityMapping("USP_CU_GSTDETAIL", "@GSTDetails") }
            , { "STOCKDISPATCH",  new EntityMapping("USP_CU_STOCKDISPATCH", "@StockDispatch") }
            , { "STOCKDISPATCHDETAIL",  new EntityMapping("USP_CU_STOCKDISPATCHDETAIL", "@StockDispatchDetail") }
            , { "BRANCHCOUNTER",  new EntityMapping("USP_CU_BRANCHCOUNTER", "@BranchCounters") }
            , { "MOP",  new EntityMapping("USP_CU_MOP", "@MOP") }
            , { "ROLE",  new EntityMapping("USP_CU_ROLE", "@Role") }
            , { "USER",  new EntityMapping("USP_CU_USER", "@User") }
            , { "UOM",  new EntityMapping("USP_CU_UOM", "@UOM") }
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
                    cmd.Connection = SqlCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = entityMapping[entityName].ProcedureName;
                    cmd.Parameters.AddWithValue(entityMapping[entityName].ParameterName, dtEntityWiseData);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving Entity wise data List", ex);
            }
            finally
            {
                SqlCon.SqlCloudconn().Close();
            }
        }

        public DataTable GetEntityData(int locationID, string syncDirection)
        {
            DataTable dtEntity = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SqlCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_GETSYNC]";
                    cmd.Parameters.AddWithValue("@LocationID", locationID);
                    cmd.Parameters.AddWithValue("@LocationType", "Warehouse");
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
                SqlCon.SqlCloudconn().Close();
            }
            return dtEntity;
        }

        public void UpdateEntitySyncStatus(object entitySyncStatusID, DateTime syncTime)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SqlCon.SqlCloudconn();
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
                SqlCon.SqlCloudconn().Close();
            }
        }
    }

    class EntityMapping
    {
        public EntityMapping(string procedureName, string parameterName)
        {
            ProcedureName = procedureName;
            ParameterName = parameterName;
        }

        public string ProcedureName { get; }

        public string ParameterName { get; }
    }
}
