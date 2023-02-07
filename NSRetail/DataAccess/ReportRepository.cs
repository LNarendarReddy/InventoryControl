using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ReportRepository
    {
        public DataTable GetReportData(string procedureName, Dictionary<string, object> parameters = null, bool useCloudConn = false)
        {
            DataTable dtReportData = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = useCloudConn ? SQLCon.SqlCloudConn() : SQLCon.Sqlconn();
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
                throw new Exception($"Error While Retrieving {procedureName}", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
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
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsReportData;
        }

        public void SaveSupplierIndent(DealerIndent dealerIndent)
        {
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
                    dealerIndent.dtSupplierIndent.Columns.Remove("SNO");
                    dealerIndent.dtSupplierIndent.Columns.Remove("SKUCODE");
                    dealerIndent.dtSupplierIndent.Columns.Remove("ITEMNAME");
                    dealerIndent.dtSupplierIndent.Columns.Remove("SUBCATEGORYNAME");
                    int RowsAfftected = cmd.ExecuteNonQuery();

                    if (RowsAfftected <= 0)
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

        public DataTable GetSupplierIndent(object CategoryID, object FromDate, object ToDate)
        {
            DataTable dtReportData = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_SUPPLIERINDENT]";
                    cmd.Parameters.AddWithValue("@CATEGORYID", CategoryID);
                    cmd.Parameters.AddWithValue("@FROMDATE", FromDate);
                    cmd.Parameters.AddWithValue("@TODATE", ToDate);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtReportData);
                    }
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
            return dtReportData;
        }

        public DataTable GetSupplierIndentDetail(object SupplierIndentID)
        {
            DataTable dtReportData = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_SUPPLIERINDENTDETAIL]";
                    cmd.Parameters.AddWithValue("@SUPPLIERINDENTID", SupplierIndentID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtReportData);
                    }
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
            return dtReportData;
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

        public DataTable GetStockSummaryByID(object ItemID , object BranchID)
        {
            DataTable dtReportData = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_STOCKSUMMARYITEMID]";
                    cmd.Parameters.AddWithValue("@ITEMID", ItemID);
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtReportData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While Retrieving Stock Summary By ItemID", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtReportData;
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
