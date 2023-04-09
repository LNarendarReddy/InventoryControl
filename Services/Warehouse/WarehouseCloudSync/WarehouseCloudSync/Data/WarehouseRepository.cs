using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace WarehouseCloudSync.Data
{
    public class WarehouseRepository
    {
        Dictionary<string, EntityMapping> entityMapping = new Dictionary<string, EntityMapping>()
        {
            { "POS_BILL",  new EntityMapping("USP_CU_POS_BILL", "@Bills") }
            , { "POS_BILLDETAIL",  new EntityMapping("USP_CU_POS_BILLDETAIL", "@BillDetails") }
            , { "POS_BILLMOPDETAIL",  new EntityMapping("USP_SYNC_CU_BILLMOPDETAIL", "@BillMopDetails") }
            , { "POS_BREFUND",  new EntityMapping("USP_SYNC_CU_BREFUND", "@BRefundDetails") }
            , { "POS_BREFUNDDETAIL",  new EntityMapping("USP_SYNC_CU_BREFUNDDETAL", "@BRefundDetails") }
            , { "POS_CREFUND",  new EntityMapping("USP_SYNC_CU_CREFUND", "@CRefundDetails") }
            , { "POS_DAYCLOSURE",  new EntityMapping("USP_SYNC_CU_DAYCLOSURE", "@DayClosure") }
            , { "POS_DAYCLOSUREDETAIL",  new EntityMapping("USP_SYNC_CU_DAYCLOSUREDETAIL", "@DayClosureDetail") }
            , { "STOCKDISPATCH",  new EntityMapping("USP_SYNC_CU_STOCKDISPATCH", "@StockDispatch") }
            , { "STOCKDISPATCHDETAIL",  new EntityMapping("USP_SYNC_CU_STOCKDISPATCHDETAIL", "@StockDispatchDetail") }
            , { "USER",  new EntityMapping("USP_SYNC_CU_USER", "@User") }
            , { "CLOUD_STOCKCOUNTING",  new EntityMapping("USP_SYNC_CU_STOCKCOUNTING", "@StockCounting") }
            , { "CLOUD_STOCKCOUNTINGDETAIL",  new EntityMapping("USP_SYNC_CU_STOCKCOUNTINGDETAIL", "@StockCountingDetail") }
        };

        public void SaveData(string entityName, DataTable dtEntityWiseData)
        {
            if (!entityMapping.ContainsKey(entityName))
            {
                return;
            }
            SqlTransaction transaction = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SqlCon.SqlWHconn();
                    if (entityName == "POS_DAYCLOSURE")
                    {
                        transaction = cmd.Connection.BeginTransaction();
                        cmd.Transaction = transaction;
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 600;
                    EntityMapping map = entityMapping[entityName];
                    cmd.CommandText = map.ProcedureName;
                    cmd.Parameters.AddWithValue(map.ParameterName, dtEntityWiseData);
                    cmd.ExecuteNonQuery();
                    transaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine(ex.ToString());
                throw new Exception("Error While saving Entity wise data List", ex);
            }
            finally
            {
                SqlCon.SqlWHconn().Close();
            }
        }

        public void ProccessDayClosures()
        {
            SqlTransaction transaction = null;
            object count = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SqlCon.SqlWHconn();
                    transaction = cmd.Connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 600;
                    cmd.CommandText = "USP_P_DAYCLOSURES";
                    count = cmd.ExecuteScalar();
                    transaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                Console.WriteLine(ex.ToString());
                throw new Exception("Error While proccesing day closures", ex);
            }
            finally
            {
                SqlCon.SqlWHconn().Close();
            }

            TimeSpan.TryParse(DateTime.Now.ToLongTimeString(), out TimeSpan curTime);

            if (curTime >= TimeSpan.Parse("20:00:00") || curTime <= TimeSpan.Parse("06:00:00"))
            {
                if (count != null && int.TryParse(count.ToString(), out int countValue) && countValue > 0)
                {
                    Console.WriteLine($"Continuing next day closure process");
                    ProccessDayClosures();
                }
            }
        }

        public DataTable GetEntityWiseData(object EntityName, object SyncDate)
        {
            DataTable dtEntity = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SqlCon.SqlWHconn();
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
                SqlCon.SqlWHconn().Close();
            }
            return dtEntity;
        }                
    }
}
