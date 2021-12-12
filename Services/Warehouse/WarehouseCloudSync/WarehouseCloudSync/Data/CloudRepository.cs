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
            , { "DEALER",  new EntityMapping("USP_CU_DEALER", "@Dealer") }
            , { "STOCKDISPATCH",  new EntityMapping("USP_CU_STOCKDISPATCH", "@StockDispatch") }
            , { "STOCKDISPATCHDETAIL",  new EntityMapping("USP_CU_STOCKDISPATCHDETAIL", "@StockDispatchDetail") }
            , { "SUBCATEGORY",  new EntityMapping("USP_CU_SUBCATEGORY", "@SubCategory") }
            , { "CATEGORY",  new EntityMapping("USP_CU_CATEGORY", "@Category") }
            , { "COUNTER",  new EntityMapping("USP_CU_COUNTER", "@Counter") }
            , { "MOP",  new EntityMapping("USP_CU_MOP", "@MOP") }
            , { "ROLE",  new EntityMapping("USP_CU_ROLE", "@Role") }
            , { "USER",  new EntityMapping("USP_CU_USER", "@User") }
            , { "UOM",  new EntityMapping("USP_CU_UOM", "@UOM") }
        };

        public void SaveData(string entityName, DataTable dtEntityWiseData)
        {
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
