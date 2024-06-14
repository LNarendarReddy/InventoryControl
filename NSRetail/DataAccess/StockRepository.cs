using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class StockRepository
    {
        public StockDispatch SaveDispatch(StockDispatch ObjStockDispatch)
        {
            int StockDispatchID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_STOCKDISPATCH]";
                    cmd.Parameters.AddWithValue("@STOCKDISPATCHID", ObjStockDispatch.STOCKDISPATCHID);
                    cmd.Parameters.AddWithValue("@FROMBRANCHID", ObjStockDispatch.FROMBRANCHID);
                    cmd.Parameters.AddWithValue("@TOBRANCHID", ObjStockDispatch.TOBRANCHID);
                    cmd.Parameters.AddWithValue("@CATEGORYID", ObjStockDispatch.CATEGORYID);
                    cmd.Parameters.AddWithValue("@USERID", ObjStockDispatch.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out StockDispatchID))
                        throw new Exception(str);
                    else
                        ObjStockDispatch.STOCKDISPATCHID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Saving Dispatch");
            }
            finally
            {
                
            }
            return ObjStockDispatch;
        }
        public StockDispatchDetail SaveDispatchDetail(StockDispatchDetail ObjStockDispatchDetail)
        {
            int StockDispatchDetailID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_STOCKDISPATCHDETAIL]";
                    cmd.Parameters.AddWithValue("@STOCKDISPATCHDETAILID", ObjStockDispatchDetail.STOCKDISPATCHDETAILID);
                    cmd.Parameters.AddWithValue("@STOCKDISPATCHID", ObjStockDispatchDetail.STOCKDISPATCHID);
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", ObjStockDispatchDetail.ITEMPRICEID);
                    cmd.Parameters.AddWithValue("@TRAYNUMBER", ObjStockDispatchDetail.TRAYNUMBER);
                    cmd.Parameters.AddWithValue("@DISPATCHQUANTITY", ObjStockDispatchDetail.DISPATCHQUANTITY);
                    cmd.Parameters.AddWithValue("@WEIGHTINKGS", ObjStockDispatchDetail.WEIGHTINKGS);
                    cmd.Parameters.AddWithValue("@USERID", ObjStockDispatchDetail.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out StockDispatchDetailID))
                        throw new Exception(str);
                    else
                        ObjStockDispatchDetail.STOCKDISPATCHDETAILID = StockDispatchDetailID;
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error While Saving Dispatch Detail - {ex.Message}");
            }
            finally
            {
                
            }
            return ObjStockDispatchDetail;
        }
        public StockDispatch GetDispatchDraft(StockDispatch objStockDispatch)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_DISPATCHDRAFT]";
                    cmd.Parameters.AddWithValue("@CATEGORYID", objStockDispatch.CATEGORYID);
                    cmd.Parameters.AddWithValue("@USERID", objStockDispatch.UserID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string str = Convert.ToString(ds.Tables[0].Rows[0][0]);
                        int iValue = 0;
                        if (!int.TryParse(str, out iValue))
                            objStockDispatch.STOCKDISPATCHID = 0;
                        else
                        {
                            objStockDispatch.STOCKDISPATCHID = iValue;
                            objStockDispatch.FROMBRANCHID = ds.Tables[0].Rows[0]["FROMBRANCHID"];
                            objStockDispatch.TOBRANCHID = ds.Tables[0].Rows[0]["TOBRANCHID"];
                            objStockDispatch.CATEGORYID = ds.Tables[0].Rows[0]["CATEGORYID"];
                            objStockDispatch.dtDispatch = ds.Tables[1].Copy();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Reading Dispatch");
            }
            finally
            {
                
            }
            return objStockDispatch;
        }
        public void DeleteDispatchDetail(object StockDispatchDetailID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_STOCKDISPATCHDETAILS]";
                    cmd.Parameters.AddWithValue("@STOCKDISPATCHDETAILID", StockDispatchDetailID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleting Dispatch Detail");
            }
            finally
            {
                
            }
        }
        public void UpdateDispatch(StockDispatch ObjStockDispatch)
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
                    cmd.CommandText = "[USP_U_STOCKDISPATCH]";
                    cmd.Parameters.AddWithValue("@STOCKDISPATCHID", ObjStockDispatch.STOCKDISPATCHID);
                    object obj = cmd.ExecuteScalar();
                    if(!int.TryParse(Convert.ToString(obj), out int id))
                        throw new Exception(Convert.ToString(obj));
                    sqlTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                throw new Exception("Error While Updating Dispatch", ex);
            }
            finally
            {
                
            }
        }
        public DataTable GetDispatchList(object BranchID, object FromDate,object ToDate)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_DISPATCHLIST]";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
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
                throw new Exception("Error While Retreiving Dispatch List");
            }
            finally
            {
                
            }
            return dt;
        }
        public DataSet GetDispatch(object StockDispatchID)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_DISPATCH]";
                    cmd.Parameters.AddWithValue("@STOCKDISPATCHID", StockDispatchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retreiving Dispatch");
            }
            finally
            {
                
            }
            return ds;
        }
        public StockEntry SaveInvoice(StockEntry ObjStockEntry)
        {
            int StockEntryID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_STOCKENTRY]";
                    cmd.Parameters.AddWithValue("@STOCKENTRYID", ObjStockEntry.STOCKENTRYID);
                    cmd.Parameters.AddWithValue("@SUPPLIERID", ObjStockEntry.SUPPLIERID);
                    cmd.Parameters.AddWithValue("@SUPPLIERINVOICENO", ObjStockEntry.SUPPLIERINVOICENO);
                    cmd.Parameters.AddWithValue("@TAXINCLUSIVE", ObjStockEntry.TAXINCLUSIVE);
                    cmd.Parameters.AddWithValue("@INVOICEDATE", ObjStockEntry.InvoiceDate);
                    cmd.Parameters.AddWithValue("@TCS", ObjStockEntry.TCS);
                    cmd.Parameters.AddWithValue("@DISCOUNTPER", ObjStockEntry.DISCOUNTPER);
                    cmd.Parameters.AddWithValue("@DISCOUNT", ObjStockEntry.DISCOUNTFLAT);
                    cmd.Parameters.AddWithValue("@EXPENSES", ObjStockEntry.EXPENSES);
                    cmd.Parameters.AddWithValue("@TRANSPORT", ObjStockEntry.TRANSPORT);
                    cmd.Parameters.AddWithValue("@CATEGORYID", ObjStockEntry.CATEGORYID);
                    cmd.Parameters.AddWithValue("@USERID", ObjStockEntry.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out StockEntryID))
                        throw new Exception(str);
                    else
                        ObjStockEntry.STOCKENTRYID = objReturn;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Invoice Number Already Exists"))
                    throw ex;
                else
                    throw new Exception("Error While Saving Stock Invoice");
            }
            finally
            {
                
            }
            return ObjStockEntry;
        }
        public StockEntryDetail SaveInvoiceDetail(StockEntryDetail ObjStockEntryDetail)
        {
            DataTable dt = new DataTable();
            int StockEntryDetailID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_STOCKENTRYDETAIL1]";
                    cmd.Parameters.AddWithValue("@STOCKENTRYDETAILID", ObjStockEntryDetail.STOCKENTRYDETAILID);
                    cmd.Parameters.AddWithValue("@STOCKENTRYID", ObjStockEntryDetail.STOCKENTRYID);
                    cmd.Parameters.AddWithValue("@ITEMCODEID", ObjStockEntryDetail.ITEMCODEID);
                    cmd.Parameters.AddWithValue("@COSTPRICEWT", ObjStockEntryDetail.COSTPRICEWT);
                    cmd.Parameters.AddWithValue("@COSTPRICEWOT", ObjStockEntryDetail.COSTPRICEWOT);
                    cmd.Parameters.AddWithValue("@MRP", ObjStockEntryDetail.MRP);
                    cmd.Parameters.AddWithValue("@SALEPRICE", ObjStockEntryDetail.SALEPRICE);
                    cmd.Parameters.AddWithValue("@QUANTITY", ObjStockEntryDetail.QUANTITY);
                    cmd.Parameters.AddWithValue("@WEIGHTINKGS", ObjStockEntryDetail.WEIGHTINKGS);
                    cmd.Parameters.AddWithValue("@USERID", ObjStockEntryDetail.UserID);
                    cmd.Parameters.AddWithValue("@GSTID", ObjStockEntryDetail.GSTID);
                    cmd.Parameters.AddWithValue("@FREEQUANTITY", ObjStockEntryDetail.FreeQuantity);
                    cmd.Parameters.AddWithValue("@DISCOUNTFLAT", ObjStockEntryDetail.DiscountFlat);
                    cmd.Parameters.AddWithValue("@DISCOUNTPERCENTAGE", ObjStockEntryDetail.DiscountPercentage);
                    cmd.Parameters.AddWithValue("@SCHEMEPERCENTAGE", ObjStockEntryDetail.SchemePercentage);
                    cmd.Parameters.AddWithValue("@SCHEMEFLAT", ObjStockEntryDetail.SchemeFlat);
                    cmd.Parameters.AddWithValue("@TOTALPRICEWT", ObjStockEntryDetail.TotalPriceWT);
                    cmd.Parameters.AddWithValue("@TOTALPRICEWOT", ObjStockEntryDetail.TotalPriceWOT);
                    cmd.Parameters.AddWithValue("@APPLIEDDISCOUNT", ObjStockEntryDetail.AppliedDiscount);
                    cmd.Parameters.AddWithValue("@APPLIEDSCHEME", ObjStockEntryDetail.AppliedScheme);
                    cmd.Parameters.AddWithValue("@APPLIEDDGST", ObjStockEntryDetail.AppliedGST);
                    cmd.Parameters.AddWithValue("@FINALPRICE", ObjStockEntryDetail.FinalPrice);
                    cmd.Parameters.AddWithValue("@CGST", ObjStockEntryDetail.CGST);
                    cmd.Parameters.AddWithValue("@SGST", ObjStockEntryDetail.SGST);
                    cmd.Parameters.AddWithValue("@IGST", ObjStockEntryDetail.IGST);
                    cmd.Parameters.AddWithValue("@CESS", ObjStockEntryDetail.CESS);
                    cmd.Parameters.AddWithValue("@HSNCODE", ObjStockEntryDetail.HSNCODE);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string str = Convert.ToString(dt.Rows[0][0]);
                        if (!int.TryParse(str, out StockEntryDetailID))
                            throw new Exception(str);
                        else
                        {
                            RefreshObject(dt, ObjStockEntryDetail);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("MRP already exists"))
                    throw ex;
                throw new Exception("Error While Saving Invoice Detail");
            }
            return ObjStockEntryDetail;
        }

        private void RefreshObject(DataTable dataTable, StockEntryDetail stockEntryDetail)
        {
            stockEntryDetail.STOCKENTRYDETAILID = dataTable.Rows[0]["STOCKENTRYDETAILID"];
            stockEntryDetail.ITEMID = dataTable.Rows[0]["ITEMID"];
            stockEntryDetail.ITEMCODEID = dataTable.Rows[0]["ITEMCODEID"];
            stockEntryDetail.ITEMPRICEID = dataTable.Rows[0]["ITEMPRICEID"];
            stockEntryDetail.SKUCODE = dataTable.Rows[0]["SKUCODE"];
            stockEntryDetail.ITEMCODE = dataTable.Rows[0]["ITEMCODE"];
            stockEntryDetail.ITEMNAME = dataTable.Rows[0]["ITEMNAME"];
            stockEntryDetail.COSTPRICEWT = dataTable.Rows[0]["COSTPRICEWT"];
            stockEntryDetail.COSTPRICEWOT = dataTable.Rows[0]["COSTPRICEWOT"];
            stockEntryDetail.GSTID = dataTable.Rows[0]["GSTID"];
            stockEntryDetail.MRP = dataTable.Rows[0]["MRP"];
            stockEntryDetail.SALEPRICE = dataTable.Rows[0]["SALEPRICE"];
            stockEntryDetail.QUANTITY = dataTable.Rows[0]["QUANTITY"];
            stockEntryDetail.WEIGHTINKGS = dataTable.Rows[0]["WEIGHTINKGS"];
            stockEntryDetail.FreeQuantity = dataTable.Rows[0]["FREEQUANTITY"];
            stockEntryDetail.DiscountFlat = dataTable.Rows[0]["DISCOUNTFLAT"];
            stockEntryDetail.DiscountPercentage = dataTable.Rows[0]["DISCOUNTPERCENTAGE"];
            stockEntryDetail.SchemePercentage = dataTable.Rows[0]["SCHEMEPERCENTAGE"];
            stockEntryDetail.SchemeFlat = dataTable.Rows[0]["SCHEMEFLAT"];
            stockEntryDetail.TotalPriceWT = dataTable.Rows[0]["TOTALPRICEWT"];
            stockEntryDetail.TotalPriceWOT = dataTable.Rows[0]["TOTALPRICEWOT"];
            stockEntryDetail.AppliedDiscount = dataTable.Rows[0]["APPLIEDDISCOUNT"];
            stockEntryDetail.AppliedScheme = dataTable.Rows[0]["APPLIEDSCHEME"];
            stockEntryDetail.AppliedGST = dataTable.Rows[0]["APPLIEDDGST"];
            stockEntryDetail.FinalPrice = dataTable.Rows[0]["FINALPRICE"];
            stockEntryDetail.CGST = dataTable.Rows[0]["CGST"];
            stockEntryDetail.SGST = dataTable.Rows[0]["SGST"];
            stockEntryDetail.IGST = dataTable.Rows[0]["IGST"];
            stockEntryDetail.CESS = dataTable.Rows[0]["CESS"];
            stockEntryDetail.HSNCODE = dataTable.Rows[0]["HSNCODE"];
        }

        public void SaveStockAdjustment(StockAdjustment stockAdjustment)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_STOCKADJUSTMENT]";
                    cmd.Parameters.AddWithValue("@ItemID", stockAdjustment.ItemID);
                    cmd.Parameters.AddWithValue("@BRANCHID", stockAdjustment.BranchID);
                    cmd.Parameters.AddWithValue("@Description", stockAdjustment.Description);
                    cmd.Parameters.AddWithValue("@StockSummary", stockAdjustment.dtStockSummary);
                    cmd.Parameters.AddWithValue("@USERID", stockAdjustment.UserID);

                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out int stockAdjustmentID))
                        throw new Exception(str);                    
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error while saving stock adjustment - {ex.Message}", ex);
            }
            finally
            {
                
            }
        }
        public StockEntry GetInvoiceDraft(StockEntry objStockEntry)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_STOCKENTRYDTAFT]";
                    cmd.Parameters.AddWithValue("@CATEGORYID", objStockEntry.CATEGORYID);
                    cmd.Parameters.AddWithValue("@USERID", objStockEntry.UserID);
                    cmd.Parameters.AddWithValue("@STOCKENTRYID", objStockEntry.STOCKENTRYID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string str = Convert.ToString(ds.Tables[0].Rows[0][0]);
                        int iValue = 0;
                        if (!int.TryParse(str, out iValue))
                            objStockEntry.STOCKENTRYID = 0;
                        else
                        {
                            objStockEntry.STOCKENTRYID = iValue;
                            objStockEntry.SUPPLIERID = ds.Tables[0].Rows[0]["SUPPLIERID"];
                            objStockEntry.SUPPLIERINVOICENO = ds.Tables[0].Rows[0]["SUPPLIERINVOICENO"];
                            objStockEntry.TAXINCLUSIVE = ds.Tables[0].Rows[0]["TAXINCLUSIVEVALUE"];
                            objStockEntry.InvoiceDate = ds.Tables[0].Rows[0]["INVOICEDATE"];
                            objStockEntry.TCS = ds.Tables[0].Rows[0]["TCS"];
                            objStockEntry.DISCOUNTPER = ds.Tables[0].Rows[0]["DISCOUNTPER"];
                            objStockEntry.DISCOUNTFLAT = ds.Tables[0].Rows[0]["DISCOUNT"];
                            objStockEntry.EXPENSES = ds.Tables[0].Rows[0]["EXPENSES"];
                            objStockEntry.TRANSPORT = ds.Tables[0].Rows[0]["TRANSPORT"];                            
                            objStockEntry.dtStockEntry = ds.Tables[1].Copy();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Reading Stock Entry");
            }
            finally
            {
                
            }
            return objStockEntry;
        }
        public void DeleteInvoiceDetail(object StockEntryDetailID,object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_STOCKENTRYDETAIL]";
                    cmd.Parameters.AddWithValue("@STOCKENTRYDETAILID", StockEntryDetailID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleting Invoice Detail");
            }
            finally
            {
                
            }
        }
        public void UpdateInvoice(StockEntry ObjStockEntry)
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
                    cmd.CommandText = "[USP_U_STOCKENTRY]";
                    cmd.Parameters.AddWithValue("@STOCKENTRYID", ObjStockEntry.STOCKENTRYID);
                    cmd.Parameters.AddWithValue("@TCS", ObjStockEntry.TCS);
                    cmd.Parameters.AddWithValue("@DISCOUNTPER", ObjStockEntry.DISCOUNTPER);
                    cmd.Parameters.AddWithValue("@DISCOUNTFLAT", ObjStockEntry.DISCOUNTFLAT);
                    cmd.Parameters.AddWithValue("@EXPENSES", ObjStockEntry.EXPENSES);
                    cmd.Parameters.AddWithValue("@TRANSPORT", ObjStockEntry.TRANSPORT);
                    object obj = cmd.ExecuteScalar();
                    if (!int.TryParse(Convert.ToString(obj), out int id))
                        throw new Exception(Convert.ToString(obj));
                    sqlTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                throw new Exception("Error While Updating Invoice", ex);
            }
        }
        public DataTable GetInvoiceList(object DealerID, object  FromDate,object ToDate)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_INVOICELIST]";
                    cmd.Parameters.AddWithValue("@DealerID", DealerID);
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
                throw new Exception("Error While Retreiving Invoice List");
            }
            finally
            {
                
            }
            return dt;
        }
        public DataSet GetInvoice(object StockEntryID)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_STOCKENTRY]";
                    cmd.Parameters.AddWithValue("@STOCKENTRYID", StockEntryID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retreiving Invoice");
            }
            finally
            {
                
            }
            return ds;
        }
               
        public DataSet SaveDispatchDC(object BranchID, object CategoryID, object LocationName, object UserID)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_DISPATCHDC]";
                    cmd.Parameters.AddWithValue("@CATEGORYID", CategoryID);
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    cmd.Parameters.AddWithValue("@LocationName", LocationName);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Saving Dispatch DC", ex);
            }
            finally
            {
                
            }
            return ds;
        }
        public DataSet GetDispatchDC(object DispatchDCID)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_STOCKDISPATCHDC]";
                    cmd.Parameters.AddWithValue("@DISPATCHDCID", DispatchDCID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retreiving Dispatch DC", ex);
            }
            finally
            {
                
            }
            return ds;
        }
        public DataTable GetDCList(object BranchID, object FromDate,object ToDate)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_DISPATCHDCLIST]";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
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
                throw new Exception("Error While Retreiving Dispatch DC List", ex);
            }
            finally
            {
                
            }
            return dt;
        }
        public DataTable GetCurrentStock(object FROMBRANCHID,object TOBRANCHID,
            object ITEMCODEID,object PARENTITEMID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_CURRENTSTOCK]";
                    cmd.Parameters.AddWithValue("@FROMBRANCHID", FROMBRANCHID);
                    cmd.Parameters.AddWithValue("@TOBRANCHID", TOBRANCHID);
                    cmd.Parameters.AddWithValue("@ITEMCODEID", ITEMCODEID);
                    cmd.Parameters.AddWithValue("@PARENTITEMID", PARENTITEMID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retreiving Current Stock", ex);
            }
            finally
            {
                
            }
            return dt;
        }
        public void DiscardStockEntry(object StockEntryID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_DISCARDSTOCKENTRY]";
                    cmd.Parameters.AddWithValue("@STOCKENTRYID", StockEntryID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected <= 0)
                        throw new Exception("Nothing is deleted");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleting Invoice", ex);
            }
            finally
            {
                
            }
        }
        public void DiscardStockDispatch(object StockDispatchID,object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_DISPATCH]";
                    cmd.Parameters.AddWithValue("@StockDispatchID", StockDispatchID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected <= 0)
                        throw new Exception("Nothing is deleted");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleting Dispatch");
            }
            finally
            {
                
            }
        }

        public void ProcessWarehouseDispatch(object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_WHDISPATCH]";
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    object objreturn = cmd.ExecuteScalar();
                    if (!string.IsNullOrEmpty(Convert.ToString(objreturn)))
                        throw new Exception(Convert.ToString(objreturn));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while processing warehouse dispatch");
            }
            finally
            {
                
            }
        }

        public void AddProcessingSlippage(object itemPriceID, object weightInKgs, object description, object userID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_STOCKSLIPPAGE]";
                    cmd.Parameters.AddWithValue("@ItemPriceID", itemPriceID);
                    cmd.Parameters.AddWithValue("@WeightInKGs", weightInKgs);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while adding processing slippage", ex);
            }
            finally
            {
                
            }
        }

        public void AddBulkProcessingDetail(object addBulkProcessingID, object addBulkProcessingDetailID, object ItemPriceID, bool isOpenItem, object qtyOrWght)
        {
            SqlTransaction transaction = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    transaction = cmd.Connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_ADDBULKPROCESSINGDETAIL]";
                    cmd.Parameters.AddWithValue("@ADDBULKPROCESSINGID", addBulkProcessingID);
                    cmd.Parameters.AddWithValue("@ADDBULKPROCESSINGDETAILID", addBulkProcessingDetailID);
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", ItemPriceID);
                    cmd.Parameters.AddWithValue("@Quantity", isOpenItem ? 0 : qtyOrWght);
                    cmd.Parameters.AddWithValue("@WeightInKGs", isOpenItem ? qtyOrWght : 0);
                    object returnValue = cmd.ExecuteScalar();
                    if (!int.TryParse(returnValue?.ToString(), out int retVal))
                        throw new Exception(returnValue.ToString());
                    transaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw new Exception($"Error while adding bulk processing : {ex.Message}", ex);
            }
        }

        public void DeleteBulkProcessingDetail(object addBulkProcessingDetailID, object addBulkProcessingID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_ADDBULKPROCESSINGDETAIL]";
                    cmd.Parameters.AddWithValue("@ADDBULKPROCESSINGDETAILID", addBulkProcessingDetailID);
                    cmd.Parameters.AddWithValue("@ADDBULKPROCESSINGID", addBulkProcessingID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While Deleting Dispatch Detail - {ex.Message}", ex);
            }
        }

        public void SubmitBulkProcessing(object addBulkProcessingID, object userID)
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
                    cmd.CommandText = "[USP_U_ADDBULKPROCESSSING]";
                    cmd.Parameters.AddWithValue("@ADDBULKPROCESSINGID", addBulkProcessingID);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    object obj = cmd.ExecuteScalar();
                    if (!int.TryParse(Convert.ToString(obj), out int id))
                        throw new Exception(Convert.ToString(obj));
                    sqlTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                throw new Exception($"Error While Updating Dispatch - {ex.Message}", ex);
            }
        }

        public DataTable FindItemPriceInStockEntry(object ItemPriceID, object StockEntryID)
        {
            DataTable entries = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[P_R_FINDITEMPRICEININOIVCE]";
                    cmd.Parameters.AddWithValue("@STOCKENTRYID", StockEntryID);
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", ItemPriceID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(entries);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Itemprices", ex);
            }
            return entries;
        }
    }
}
