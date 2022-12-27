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
                    cmd.Parameters.AddWithValue("@SupplierID", supplierReturns.SupplierID);
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

        public object SaveSupplierReturnsDetail(DataRow drNew,object UserID, object BranchID)
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
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
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
                    object objreturn = cmd.ExecuteScalar();
                    if (objreturn != null)
                    {
                        throw new Exception(Convert.ToString(objreturn));
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Few items are not freezed"))
                    throw ex;
                else
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

        public DataTable GetSupplierReturnsforCN(object SupplierReturnsID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_SUPPLIERRETURNDETAILFORCN]";
                    cmd.Parameters.AddWithValue("@SupplierReturnsID", SupplierReturnsID);
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

        public void UpdateSupplierCostPrice(object BRDID, object SupplierID, object ItemCostPriceID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_SUPPLIERCOSTPRICE]";
                    cmd.Parameters.AddWithValue("@BRDID", BRDID);
                    cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                    cmd.Parameters.AddWithValue("@ItemCostPriceID", ItemCostPriceID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating supplier cost price");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }

        public void UpdateBRReason(object BRDID, object ReasonID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_BRREASON]";
                    cmd.Parameters.AddWithValue("@BRDID", BRDID);
                    cmd.Parameters.AddWithValue("@REASONID", ReasonID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while updating BR Reason");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }

        public DataTable GetReason()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_REASON]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving reason list");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dt;
        }

        public DataTable GetSupplier()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_SUPPLIERFORBR]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving reason list");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dt;
        }

        public void FreezeSupplierReturns(object SupplierReturnsID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_FREEZESUPPLIERRETURNS]";
                    cmd.Parameters.AddWithValue("@SUPPLIERRETURNSID", SupplierReturnsID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    object obj = cmd.ExecuteScalar();
                    if (!int.TryParse(Convert.ToString(obj), out int id))
                        throw new Exception(Convert.ToString(obj));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while freezing supplier returns", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }

        public DataTable ViewSupplierReturnItems(object SupplierReturnsID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_SUPPLIERRETURNSITEMS]";
                    cmd.Parameters.AddWithValue("@SUPPLIERRETURNSID", SupplierReturnsID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving supplier returns items");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dt;
        }
    }
}
