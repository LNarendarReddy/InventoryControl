using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NSRetailPOS.Data
{
    public class SyncRepository
    {
        Dictionary<string, EntityMapping> entityMapping = new Dictionary<string, EntityMapping>()
        {
            { "ITEM",  new EntityMapping("POS_USP_CU_ITEM", "@Items") }
            , { "ITEMCODE",  new EntityMapping("POS_USP_CU_ITEMCODE", "@ItemCodes") }
            , { "ITEMPRICE",  new EntityMapping("POS_USP_CU_ITEMPRICE", "@ItemPrices") }
            , { "BRANCH",  new EntityMapping("POS_USP_CU_BRANCH", "@Branches") }
            , { "GST",  new EntityMapping("POS_USP_CU_GSTDETAIL", "@GSTDetails") }
            , { "STOCKDISPATCH",  new EntityMapping("POS_USP_CU_STOCKDISPATCH", "@StockDispatch") }
            , { "STOCKDISPATCHDETAIL",  new EntityMapping("POS_USP_CU_STOCKDISPATCHDETAIL", "@StockDispatchDetail") }
            , { "BRANCHCOUNTER",  new EntityMapping("POS_USP_CU_BRANCHCOUNTER", "@BranchCounters") }
            , { "MOP",  new EntityMapping("POS_USP_CU_MOP", "@MOP") }
            , { "ROLE",  new EntityMapping("POS_USP_CU_ROLE", "@Role") }
            , { "USER",  new EntityMapping("POS_USP_CU_USER", "@User") }
            , { "UOM",  new EntityMapping("POS_USP_CU_UOM", "@UOM") }
            , { "ITEMGROUP",  new EntityMapping("POS_USP_CU_ITEMGROUP", "@ItemGroups") }
            , { "ITEMGROUPDETAIL",  new EntityMapping("POS_USP_CU_ITEMGROUPDETAIL", "@ItemGroupDetails") }
            , { "OFFERTYPE",  new EntityMapping("POS_USP_CU_OFFERTYPE", "@OfferTypes") }
            , { "OFFER",  new EntityMapping("POS_USP_CU_OFFER", "@Offers") }
            , { "OFFERBRANCH",  new EntityMapping("POS_USP_CU_OFFERBRANCH", "@OfferBranches") }
            , { "OFFERITEMMAP",  new EntityMapping("POS_USP_CU_OFFERITEMMAP", "@OfferItemMaps") }
            , { "POS_DENOMINATION",  new EntityMapping("USP_CU_DENOMINATION", "@Denomination") }
            , { "TBLCATEGORY",  new EntityMapping("USP_CU_TBLCATEGORY", "@Category") }
            , { "REASONFORREFUND",  new EntityMapping("USP_CU_REASONFORREFUND", "@RFR") }
        };

        public void SaveData(string entityName, DataTable dtEntityWiseData)
        {
            if(!entityMapping.ContainsKey(entityName))
            {
                return;
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlSyncConn();
                    cmd.CommandType = CommandType.StoredProcedure;
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
                SQLCon.SqlSyncConn().Close();
            }
        }

        public void ClearOldData()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlSyncConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "POS_USP_D_OLD_DATA";                    
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While clearing old data", ex);
            }
            finally
            {
                SQLCon.SqlSyncConn().Close();
            }
        }

        public DataTable GetEntityWiseData(object EntityName, object SyncDate)
        {
            DataTable dtEntity = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlSyncConn();
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
                SQLCon.SqlSyncConn().Close();
            }
            return dtEntity;
        }

        public void ImportDaySequence(DataSet dsRestoreData)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_CU_POS_IMPORTDATA";
                    cmd.Parameters.AddWithValue("@Bill", dsRestoreData.Tables[0]);
                    cmd.Parameters.AddWithValue("@BillDetail", dsRestoreData.Tables[1]);
                    cmd.Parameters.AddWithValue("@BillMOPDetail", dsRestoreData.Tables[2]);
                    cmd.Parameters.AddWithValue("@DaySequence", dsRestoreData.Tables[3]);
                    cmd.Parameters.AddWithValue("@CRefund", dsRestoreData.Tables[4]);
                    cmd.Parameters.AddWithValue("@BRefund", dsRestoreData.Tables[5]);
                    cmd.Parameters.AddWithValue("@BRefundDetail", dsRestoreData.Tables[6]);
                    cmd.Parameters.AddWithValue("@DayClosure", dsRestoreData.Tables[7]);
                    cmd.Parameters.AddWithValue("@DayClosureDetail", dsRestoreData.Tables[8]);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While importing day sequence", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }

        public void SaveHDDSNo(string HDDSNo)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_U_POS_HDDSNO";
                    cmd.Parameters.AddWithValue("@BranchCounterID", Utility.branchInfo.BranchCounterID);
                    cmd.Parameters.AddWithValue("@HDDSNO", HDDSNo);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While saving machine identifier", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }
    }

    class EntityMapping
    {
        public EntityMapping(string procedureName, string parameterName, bool includeBranchCounterID = false)
        {
            ProcedureName = procedureName;
            ParameterName = parameterName;
            IncludeBranchCounterID = includeBranchCounterID;
        }

        public string ProcedureName { get; }

        public string ParameterName { get; }

        public bool IncludeBranchCounterID { get; }
    }
}
