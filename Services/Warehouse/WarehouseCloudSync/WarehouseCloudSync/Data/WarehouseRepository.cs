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
            , { "STOCKDISPATCH",  new EntityMapping("USP_SYNC_CU_STOCKDISPATCH", "@StockDispatch") }
            , { "STOCKDISPATCHDETAIL",  new EntityMapping("USP_SYNC_CU_STOCKDISPATCHDETAIL", "@StockDispatchDetail") }
            , { "USER",  new EntityMapping("POS_USP_CU_USER", "@User") }
            , { "CLOUD_STOCKCOUNTING",  new EntityMapping("USP_SYNC_CU_STOCKCOUNTING", "@StockCounting") }
            , { "CLOUD_STOCKCOUNTINGDETAIL",  new EntityMapping("USP_SYNC_CU_STOCKCOUNTINGDETAIL", "@StockCountingDetail") }
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
                    cmd.Connection = SqlCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    EntityMapping map = entityMapping[entityName];
                    cmd.CommandText = map.ProcedureName;
                    cmd.Parameters.AddWithValue(map.ParameterName, dtEntityWiseData);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving Entity wise data List", ex);
            }
            finally
            {
                SqlCon.SqlWHconn().Close();
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
