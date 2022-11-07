using DevExpress.CodeParser;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NSRetailPOS.Data
{
    public class CloudRepository
    {

        Dictionary<string, EntityMapping> entityMapping = new Dictionary<string, EntityMapping>()
        {
              { "POS_BILL",  new EntityMapping("USP_CU_POS_BILL", "@Bills", true) }
            , { "POS_BILLDETAIL",  new EntityMapping("USP_CU_POS_BILLDETAIL", "@BillDetails", true) }
            , { "POS_BILLMOPDETAIL",  new EntityMapping("USP_CU_POS_BILLMOPDETAIL", "@BillMopDetails", true) }
            , { "POS_CREFUND",  new EntityMapping("USP_CU_POS_CREFUND", "@CRefundDetails", true) }
            , { "POS_BREFUND",  new EntityMapping("USP_CU_POS_BREFUND", "@BRefundDetails", true) }
            , { "POS_BREFUNDDETAIL",  new EntityMapping("USP_CU_POS_BREFUNDDETAL", "@BRefundDetails", true) }
            , { "POS_DAYCLOSURE",  new EntityMapping("USP_CU_POS_DAYCLOSURE", "@DayClosure", true) }
            , { "POS_DAYCLOSUREDETAIL",  new EntityMapping("USP_CU_POS_DAYCLOSUREDETAIL", "@DayClosureDetail", true) }
            , { "STOCKDISPATCH",  new EntityMapping("USP_CU_STOCKDISPATCH", "@StockDispatch") }
            , { "STOCKDISPATCHDETAIL",  new EntityMapping("USP_CU_STOCKDISPATCHDETAIL", "@StockDispatchDetail") }
            , { "USER",  new EntityMapping("USP_CU_USER", "@User") }
            , { "POS_DAYSEQUENCE",  new EntityMapping("USP_CU_POS_DAYSEQUENCE", "@DaySequence") }
        };
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
                    cmd.Connection = SQLCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    EntityMapping map = entityMapping[entityName];
                    cmd.CommandText = map.ProcedureName;
                    cmd.Parameters.AddWithValue(map.ParameterName, dtEntityWiseData);
                    if (map.IncludeBranchCounterID)
                    { 
                        cmd.Parameters.AddWithValue("@BranchCounterID", Utility.branchInfo.BranchCounterID); 
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

        public DataSet GetDaySequence(object branchCounterID)
        {
            DataSet dsRestoreData = new DataSet();

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_POS_IMPORTDATA]";
                    cmd.Parameters.AddWithValue("@BranchCounterID", branchCounterID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsRestoreData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While getting restore data", ex);
            }
            finally
            {
                SQLCon.SqlCloudconn().Close();
            }

            return dsRestoreData;
        }

        public void CheckOrAddHDDSerialNumber(object branchCounterID, string HDDSNo)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_BRANCHCOUNTER_HDDNO]";
                    cmd.Parameters.AddWithValue("@BranchCounterID", branchCounterID);
                    cmd.Parameters.AddWithValue("@HDDSNO", HDDSNo);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SQLCon.SqlCloudconn().Close();
            }
        }

        public Tuple<string,string> GetPOSVersion ()
        {
            Tuple<string, string> posversion = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_POSVersion]";
                    object POSVersion = cmd.ExecuteScalar();
                    posversion = Tuple.Create(Convert.ToString(POSVersion).Split(',')[0], Convert.ToString(POSVersion).Split(',')[1]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retreiving POS Version", ex);
            }
            finally
            {
                SQLCon.SqlCloudconn().Close();
            }
            return posversion;
        }

        public object GetTimeZone()
        {
            object timezone = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_GetTimeZone]";
                    timezone = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retreiving timezone", ex);
            }
            finally
            {
                SQLCon.SqlCloudconn().Close();
            }
            return timezone;
        }
    }
}