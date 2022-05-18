using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SupplierRepository
    {
        public SupplierReturns GetInitialLoad(SupplierReturns supplierReturns)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_INITIALSR]";
                    cmd.Parameters.AddWithValue("@UserID", supplierReturns.UserID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        supplierReturns.SupplierReturnsID = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0][0] : 0;
                        supplierReturns.SupplierID = ds.Tables[0].Rows.Count > 0 ? ds.Tables[0].Rows[0][1] : 0;
                    }
                    if (ds.Tables.Count > 1)
                        supplierReturns.dtSupplierReturns = ds.Tables[1];
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving Supplier Returns");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return supplierReturns;
        }

        public SupplierReturns SaveSupplierReturns(SupplierReturns supplierReturns)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_SUPPLIERRETURNS]";
                    cmd.Parameters.AddWithValue("@SupplierReturnsID", supplierReturns.SupplierReturnsID);
                    cmd.Parameters.AddWithValue("@SupplierID", supplierReturns.SupplierID);
                    cmd.Parameters.AddWithValue("@UserID", supplierReturns.UserID);
                    supplierReturns.SupplierReturnsID = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving supplier returns");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return supplierReturns;
        }

        public object SaveSupplierReturnsDetail(DataRow drNew,object UserID)
        {
            object SupplierReturnsDetailID = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_SUPPLIERRETURNSDETAIL]";
                    cmd.Parameters.AddWithValue("@SupplierReturnsDetailID", drNew["SUPPLIERRETURNSDETAILID"]);
                    cmd.Parameters.AddWithValue("@SupplierReturnsID", drNew["SUPPLIERRETURNSID"]);
                    cmd.Parameters.AddWithValue("@ItemCostPriceID", drNew["ITEMCOSTPRICEID"]);
                    cmd.Parameters.AddWithValue("@Quantity", drNew["QUANTITY"]);
                    cmd.Parameters.AddWithValue("@WeightInKGS", drNew["WEIGHTINKGS"]);
                    cmd.Parameters.AddWithValue("@TotalCostPrice", drNew["TOTALCOSTPRICE"]);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    SupplierReturnsDetailID = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving supplier returns detail");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return SupplierReturnsDetailID;
        }

        public void DeleteSupplierReturnsDetail(object SupplierReturnsDetailID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_SUPPLIERRETURNSDETAIL]";
                    cmd.Parameters.AddWithValue("@SupplierReturnsDetailID", SupplierReturnsDetailID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting supplier returns detail");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }

        public void UpdateSupplierReturns(object SupplierReturnsID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_SUPPLIERRETURNS]";
                    cmd.Parameters.AddWithValue("@SupplierReturnsID", SupplierReturnsID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating supplier returns detail");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }

        public DataTable GetSupplierReturns(object SupplierID,object FromDate,object ToDate)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_SUPPLIERRETURNS]";
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving supplier returns");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dt;
        }

        public DataTable GetSupllierReturnsDetail(object SupplierReturnsID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_SUPPLIERRETURNSDETAIL]";
                    cmd.Parameters.AddWithValue("@SUPPLIERRETURNSID", SupplierReturnsID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving Supplier Returns");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dt;
        }
    }
}
