using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace NSRetailPOS.Data
{
    public class ReportRepository
    {
        public DataTable GetReportData(string procedureName, Dictionary<string, object> parameters = null)
        {
            DataTable dtReportData = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandTimeout = 1800;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    ProcessParameters(cmd, parameters);
                    
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtReportData); 
                    }
                }
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("Warehouse and outlets"))
                    throw new Exception(ex.Message);
                throw new Exception($"Error While Retrieving {procedureName}", ex);
            }
            return dtReportData;
        }

        public DataSet GetReportDataset(string procedureName, Dictionary<string, object> parameters = null)
        {
            DataSet dsReportData = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandTimeout = 1800;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    ProcessParameters(cmd, parameters);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsReportData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While Retrieving {procedureName}", ex);
            }
            return dsReportData;
        }

        public DataSet GetSupplierIndentDetail(object SupplierIndentID, bool skipZero = false)
        {
            DataSet dsReportData = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_SUPPLIERINDENTDETAIL]";
                    cmd.Parameters.AddWithValue("@SUPPLIERINDENTID", SupplierIndentID);
                    cmd.Parameters.AddWithValue("@SkipZero", skipZero);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsReportData);
                    }
                }

                dsReportData.Relations.Add(new DataRelation("FT - Branch Details",
                    dsReportData.Tables[0].Columns["SUPPLIERINDENTDETAILID"],
                    dsReportData.Tables[1].Columns["SUPPLIERINDENTDETAILID"]));
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Supplier Indent", ex);
            }

            return dsReportData;
        }
               
        private void ProcessParameters(SqlCommand sqlCommand, Dictionary<string, object> parameters)
        {
            if (parameters == null) return;

            foreach (var param in parameters)
            {
                string paramName = param.Key;
                paramName = paramName.StartsWith("@") ? paramName : $"@{paramName}";
                sqlCommand.Parameters.AddWithValue(paramName, param.Value);
            }
        }
    }
}
