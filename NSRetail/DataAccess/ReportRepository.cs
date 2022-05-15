using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ReportRepository
    {
        public DataTable GetReportData(string procedureName, Dictionary<string, object> parameters)
        {
            DataTable dtReportData = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procedureName;
                    foreach (var param in parameters)
                    {
                        string paramName = param.Key;
                        paramName = paramName.StartsWith("@") ? paramName : $"@{paramName}";
                        cmd.Parameters.AddWithValue(paramName, param.Value);
                    }
                    
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtReportData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While Retrieving {procedureName}", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtReportData;
        }

        public void SaveSupplierIndent(DealerIndent dealerIndent)
        {
            DataTable dtReportData = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_SUPPLIERINDENT]";
                    cmd.Parameters.AddWithValue("@SUPPLIERINDENTID", dealerIndent.SupplierIndentID);
                    cmd.Parameters.AddWithValue("@SUPPLIERID", dealerIndent.supplierID);
                    cmd.Parameters.AddWithValue("@CATEGORYID", dealerIndent.CategoryID);
                    cmd.Parameters.AddWithValue("@FROMDATE", dealerIndent.FromDate);
                    cmd.Parameters.AddWithValue("@TODATE", dealerIndent.ToDate);
                    cmd.Parameters.AddWithValue("@USERID", dealerIndent.UserID);
                    cmd.Parameters.AddWithValue("@dtDetail", dealerIndent.dtSupplierIndent);
                    cmd.Parameters.AddWithValue("@ISAPPROVED", dealerIndent.IsApproved);
                    int RowsAfftected = cmd.ExecuteNonQuery();

                    if (RowsAfftected == 0)
                        throw new Exception("Error");

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While Retrieving Supplier Indent", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }

        public void DeleteSupplierIndentDetail(object SupplierIndentDetailID,object @UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_SUPPLIERINDENTDETAIL]";
                    cmd.Parameters.AddWithValue("@SUPPLIERINDENTDETAILID", SupplierIndentDetailID);
                    cmd.Parameters.AddWithValue("@USERID", @UserID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting supplier indent detail");
            }
            finally
            {
                SQLCon.SqlCloudConn().Close();
            }
        }
    }
}
