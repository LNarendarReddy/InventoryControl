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
        };

        public void SaveData(string entityName, DataTable dtEntityWiseData)
        {
            if(dtEntityWiseData?.Rows.Count == 0)
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
                throw new Exception("Error While saving Entity wise data List", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
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
