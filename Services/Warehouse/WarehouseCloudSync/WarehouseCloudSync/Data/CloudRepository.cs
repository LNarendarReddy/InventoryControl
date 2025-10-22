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
            , { "USER",  new EntityMapping("USP_CU_USER1", "@User") }
            , { "UOM",  new EntityMapping("USP_CU_UOM", "@UOM") }
            , { "ITEMGROUP",  new EntityMapping("USP_CU_ITEMGROUP", "@ItemGroups") }
            , { "ITEMGROUPDETAIL",  new EntityMapping("USP_CU_ITEMGROUPDETAIL", "@ItemGroupDetails") }
            , { "OFFERTYPE",  new EntityMapping("USP_CU_OFFERTYPE", "@OfferTypes") }
            , { "OFFER",  new EntityMapping("USP_CU_OFFER", "@Offers") }
            , { "OFFERBRANCH",  new EntityMapping("USP_CU_OFFERBRANCH", "@OfferBranches") }
            , { "OFFERITEMMAP",  new EntityMapping("USP_CU_OFFERITEMMAP", "@OfferItemMaps") }
            , { "POS_DENOMINATION",  new EntityMapping("USP_CU_DENOMINATION", "@Denomination") }
            , { "TBLCATEGORY",  new EntityMapping("USP_CU_TBLCATEGORY", "@Category") }
            , { "REASONFORREFUND",  new EntityMapping("USP_CU_REASONFORREFUND", "@RFR") }
            , { "STOCKSUMMARY",  new EntityMapping("USP_CU_STOCKSUMMARY", "@Stock") }
            , { "OFFEREXCLUSION",  new EntityMapping("USP_CU_OFFEREXCLUSION", "@OfferItemExclusion") }
            , { "PAYMENTGATEWAYINFO",  new EntityMapping("USP_CU_PAYMENTGATEWAYINFO", "@PGW") }
        };

        public void SaveData(string entityName, DataTable dtEntityWiseData)
        {
            if (!entityMapping.ContainsKey(entityName))
            {
                return;
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SqlCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 3600;
                    cmd.CommandText = entityMapping[entityName].ProcedureName;
                    cmd.Parameters.AddWithValue(entityMapping[entityName].ParameterName, dtEntityWiseData);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While saving Entity {entityName} wise data List", ex);
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
                    cmd.Parameters.AddWithValue("@NewBuild", true);
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

        public DataTable GetEntityWiseData(object EntityName, object SyncDate)
        {
            DataTable dtEntity = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SqlCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_GETSYNCDATA]";
                    cmd.Parameters.AddWithValue("@EntityName", EntityName);
                    cmd.Parameters.AddWithValue("@SyncDate", SyncDate);
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
