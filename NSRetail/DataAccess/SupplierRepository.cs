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
                    cmd.Parameters.AddWithValue("@CategoryID", supplierReturns.CategoryID);
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
                    cmd.Parameters.AddWithValue("@CategoryID", supplierReturns.CategoryID);
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
                    cmd.Parameters.AddWithValue("@ReasonID", drNew["REASONID"]);
                    SupplierReturnsDetailID = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving supplier returns detail");
            }
            finally
            {
                
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
                
            }
        }

        public void UpdateSupplierReturns(object SupplierReturnsID, object UserID, DataTable dt,object ReturnValue)
        {
            try
            {

                if (dt.Rows.Count == 0)
                    return;
                dt.Columns.Remove("SNO");
                dt.Columns.Remove("SUPPLIERRETURNSID");
                dt.Columns.Remove("ITEMCOSTPRICEID");
                dt.Columns.Remove("ITEMCODE");
                dt.Columns.Remove("ITEMNAME");
                dt.Columns.Remove("MRP");
                dt.Columns.Remove("SALEPRICE");
                dt.Columns.Remove("QUANTITY");
                dt.Columns.Remove("COSTPRICE");
                dt.Columns.Remove("WEIGHTINKGS");
                dt.Columns.Remove("TOTALCOSTPRICE");
                dt.Columns.Remove("SELECTED");
                dt.Columns.Remove("BRANCHNAME");
                dt.Columns.Remove("CREATEDBY");
                dt.Columns.Remove("BRAPPOVEDDATE");
                dt.Columns.Remove("UPDATEDBY");
                dt.Columns.Remove("UPDATEDDATE");
                dt.Columns.Remove("REFUNDTYPE");
                dt.Columns.Remove("BRCREATEDDATE");
                dt.Columns.Remove("REASONID");
                dt.Columns.Remove("BREFUNDNUMBER");

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_SUPPLIERRETURNS]";
                    cmd.Parameters.AddWithValue("@SupplierReturnsID", SupplierReturnsID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@dt", dt);
                    cmd.Parameters.AddWithValue("@RETURNVALUE", ReturnValue);
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
                
            }
        }

        public void InitiateCreditNote(object SupplierReturnsID, object UserID, DataTable dt)
        {
            try
            {
                if (dt.Rows.Count == 0)
                    return;

                dt.Columns.Remove("SNO");
                dt.Columns.Remove("SUPPLIERRETURNSID");
                dt.Columns.Remove("ITEMCOSTPRICEID");
                dt.Columns.Remove("ITEMCODE");
                dt.Columns.Remove("ITEMNAME");
                dt.Columns.Remove("MRP");
                dt.Columns.Remove("SALEPRICE");
                dt.Columns.Remove("QUANTITY");
                dt.Columns.Remove("COSTPRICE");
                dt.Columns.Remove("WEIGHTINKGS");
                dt.Columns.Remove("TOTALCOSTPRICE");
                dt.Columns.Remove("SELECTED");
                dt.Columns.Remove("RETURNSTATUS");
                dt.Columns.Remove("BRANCHNAME");
                dt.Columns.Remove("CREATEDBY");
                dt.Columns.Remove("BRAPPOVEDDATE");
                dt.Columns.Remove("UPDATEDBY");
                dt.Columns.Remove("UPDATEDDATE");
                dt.Columns.Remove("REFUNDTYPE");
                dt.Columns.Remove("BRCREATEDDATE");
                dt.Columns.Remove("REASONID");
                dt.Columns.Remove("BREFUNDNUMBER");

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_INITIATECN]";
                    cmd.Parameters.AddWithValue("@SupplierReturnsID", SupplierReturnsID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@dtids", dt);
                    object objreturn = cmd.ExecuteScalar();
                    if (objreturn != null)
                    {
                        throw new Exception(Convert.ToString(objreturn));
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("initiating credit note"))
                    throw ex;
                else
                    throw new Exception("Error while updating supplier returns detail");
            }
            finally
            {
                
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
                
            }
            return dt;
        }

        public void SplitBRQuantity(object BRDID, object BASEQUANTITY, 
            object BASEREASON, object DEVIDEDQUANTITY, object DEVIDEDREASON, object UserID, object SNO)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_SPLIT_BRQUANTITY]";
                    cmd.Parameters.AddWithValue("@BRDID", BRDID);
                    cmd.Parameters.AddWithValue("@BASEQUANTITY", BASEQUANTITY);
                    cmd.Parameters.AddWithValue("@BASEREASON", BASEREASON);
                    cmd.Parameters.AddWithValue("@DEVIDEDQUANTITY", DEVIDEDQUANTITY);
                    cmd.Parameters.AddWithValue("@DEVIDEDREASON", DEVIDEDREASON);
                    cmd.Parameters.AddWithValue("@SNO", SNO);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while splitting quantity");
            }
        }

        public void MoveSupplierReturnsItems(object SupplierID,DataTable detailids, object UserID)
        {
            SqlTransaction sqlTransaction = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    sqlTransaction = SQLCon.Sqlconn().BeginTransaction();
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.Transaction = sqlTransaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_MOVESUPPLIERRETURNS]";
                    cmd.Parameters.AddWithValue("@SUPPLIERID", SupplierID);
                    cmd.Parameters.AddWithValue("@dt", detailids);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    object obj = cmd.ExecuteScalar();
                    if (!int.TryParse(Convert.ToString(obj), out int id))
                        throw new Exception(Convert.ToString(obj));
                    sqlTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                throw new Exception("Error while moving items", ex);
            }
        }
    }
}
