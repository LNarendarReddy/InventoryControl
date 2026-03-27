using DevExpress.CodeParser;
using DevExpress.Utils.Zip.Internal;
using NSRetailPOS.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailPOS.Data
{
    public class StockRepository
    {
        public StockEntry SaveInvoice(StockEntry ObjStockEntry)
        {
            int StockEntryID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_STOCKENTRY_v2]";
                    cmd.Parameters.AddWithValue("@STOCKENTRYID", ObjStockEntry.STOCKENTRYID);
                    cmd.Parameters.AddWithValue("@SUPPLIERID", ObjStockEntry.SUPPLIERID);
                    cmd.Parameters.AddWithValue("@SUPPLIERINVOICENO", ObjStockEntry.SUPPLIERINVOICENO);
                    cmd.Parameters.AddWithValue("@INVOICEDATE", ObjStockEntry.InvoiceDate);
                    cmd.Parameters.AddWithValue("@TCS", ObjStockEntry.TCS);
                    cmd.Parameters.AddWithValue("@DISCOUNTPER", ObjStockEntry.DISCOUNTPER);
                    cmd.Parameters.AddWithValue("@DISCOUNT", ObjStockEntry.DISCOUNTFLAT);
                    cmd.Parameters.AddWithValue("@EXPENSES", ObjStockEntry.EXPENSES);
                    cmd.Parameters.AddWithValue("@TRANSPORT", ObjStockEntry.TRANSPORT);
                    cmd.Parameters.AddWithValue("@CATEGORYID", ObjStockEntry.CATEGORYID);
                    cmd.Parameters.AddWithValue("@USERID", ObjStockEntry.UserID);
                    cmd.Parameters.AddWithValue("@InvoiceType", ObjStockEntry.InvoiceType);
                    cmd.Parameters.AddWithValue("@PriceEntryMethod", ObjStockEntry.PriceEntryMethod);
                    cmd.Parameters.AddWithValue("@LorryFrightMode", ObjStockEntry.LorryFrightMode);
                    cmd.Parameters.AddWithValue("@SourceBranchID", ObjStockEntry.SourceBranchID);
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
                throw new Exception("Error While Saving Stock Invoice", ex);
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
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_STOCKENTRYDETAIL_v2]";
                    cmd.Parameters.AddWithValue("@STOCKENTRYDETAILID", ObjStockEntryDetail.STOCKENTRYDETAILID);
                    cmd.Parameters.AddWithValue("@STOCKENTRYID", ObjStockEntryDetail.STOCKENTRYID);
                    cmd.Parameters.AddWithValue("@ITEMCODEID", ObjStockEntryDetail.ITEMCODEID);
                    cmd.Parameters.AddWithValue("@QUANTITY", ObjStockEntryDetail.QUANTITY);
                    cmd.Parameters.AddWithValue("@WEIGHTINKGS", ObjStockEntryDetail.WEIGHTINKGS);
                    cmd.Parameters.AddWithValue("@MRP", ObjStockEntryDetail.MRP);
                    cmd.Parameters.AddWithValue("@SALEPRICE", ObjStockEntryDetail.SALEPRICE);
                    cmd.Parameters.AddWithValue("@COSTPRICEWT", ObjStockEntryDetail.COSTPRICEWT);
                    cmd.Parameters.AddWithValue("@COSTPRICEWOT", ObjStockEntryDetail.COSTPRICEWOT);
                    cmd.Parameters.AddWithValue("@INVOICECPWITHTAX", ObjStockEntryDetail.GROSSCOSTPRICEWT);
                    cmd.Parameters.AddWithValue("@INVOICECPWITHOUTTAX", ObjStockEntryDetail.GROSSCOSTPRICEWOT);
                    cmd.Parameters.AddWithValue("@GSTID", ObjStockEntryDetail.GSTID);
                    cmd.Parameters.AddWithValue("@DISCOUNTFLAT", ObjStockEntryDetail.DiscountFlat);
                    cmd.Parameters.AddWithValue("@DISCOUNTPERCENTAGE", ObjStockEntryDetail.DiscountPercentage);
                    cmd.Parameters.AddWithValue("@SCHEMEPERCENTAGE", ObjStockEntryDetail.SchemePercentage);
                    cmd.Parameters.AddWithValue("@SCHEMEFLAT", ObjStockEntryDetail.SchemeFlat);
                    cmd.Parameters.AddWithValue("@TOTALPRICEWT", ObjStockEntryDetail.TotalPriceWT);
                    cmd.Parameters.AddWithValue("@TOTALPRICEWOT", ObjStockEntryDetail.TotalPriceWOT);
                    cmd.Parameters.AddWithValue("@APPLIEDDISCOUNT", ObjStockEntryDetail.AppliedDiscount);
                    cmd.Parameters.AddWithValue("@APPLIEDSCHEME", ObjStockEntryDetail.AppliedScheme);
                    cmd.Parameters.AddWithValue("@APPLIEDDGST", ObjStockEntryDetail.AppliedGST);
                    cmd.Parameters.AddWithValue("@FINALPRICEWOTAX", ObjStockEntryDetail.FinalPriceWOTax);
                    cmd.Parameters.AddWithValue("@FINALPRICE", ObjStockEntryDetail.FinalPrice);
                    cmd.Parameters.AddWithValue("@CGST", ObjStockEntryDetail.CGST);
                    cmd.Parameters.AddWithValue("@SGST", ObjStockEntryDetail.SGST);
                    cmd.Parameters.AddWithValue("@IGST", ObjStockEntryDetail.IGST);
                    cmd.Parameters.AddWithValue("@CESS", ObjStockEntryDetail.CESS);
                    cmd.Parameters.AddWithValue("@HSNCODE", ObjStockEntryDetail.HSNCODE);
                    cmd.Parameters.AddWithValue("@USERID", ObjStockEntryDetail.UserID);
                    cmd.Parameters.AddWithValue("@ISFREEITEM", ObjStockEntryDetail.IsFreeItem);
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
                throw new Exception("Error While Saving Invoice Detail", ex);
            }
            return ObjStockEntryDetail;
        }
        public StockEntry GetInvoiceDraft(StockEntry objStockEntry)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_STOCKENTRYDTAFT_v2]";
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
                            objStockEntry.SUPPLIERNAME = ds.Tables[0].Rows[0]["SUPPLIERNAME"];
                            objStockEntry.InvoiceDate = ds.Tables[0].Rows[0]["INVOICEDATE"];
                            objStockEntry.TCS = ds.Tables[0].Rows[0]["TCS"];
                            objStockEntry.DISCOUNTFLAT = ds.Tables[0].Rows[0]["DISCOUNT"];
                            objStockEntry.EXPENSES = ds.Tables[0].Rows[0]["EXPENSES"];
                            objStockEntry.TRANSPORT = ds.Tables[0].Rows[0]["TRANSPORT"];
                            objStockEntry.SourceBranchID = ds.Tables[0].Rows[0]["SOURCEBRANCHID"];
                            objStockEntry.InvoiceType = ds.Tables[0].Rows[0]["InvoiceType"];
                            objStockEntry.PriceEntryMethod = ds.Tables[0].Rows[0]["PriceEntryMethod"];
                            objStockEntry.LorryFrightMode = ds.Tables[0].Rows[0]["LorryFrightMode"];
                            objStockEntry.dtStockEntry = ds.Tables[1].Copy();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Reading Stock Entry", ex);
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
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_STOCKENTRYDETAIL]";
                    cmd.Parameters.AddWithValue("@STOCKENTRYDETAILID", StockEntryDetailID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While Deleting Invoice Detail - {ex.Message}");
            }
            finally
            {
                
            }
        }
        public void UpdateInvoice(StockEntry ObjStockEntry)
        {
            ObjStockEntry.dtCreditNote.Columns.Remove("CNNumber");
            ObjStockEntry.dtCreditNote.Columns.Remove("AdjustmentType");

            SqlTransaction sqlTransaction = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    sqlTransaction = SQLCon.SqlWHconn().BeginTransaction();
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.Transaction = sqlTransaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_STOCKENTRY_v2]";
                    cmd.Parameters.AddWithValue("@STOCKENTRYID", ObjStockEntry.STOCKENTRYID);
                    cmd.Parameters.AddWithValue("@SUPPLIERID", ObjStockEntry.SUPPLIERID);
                    cmd.Parameters.AddWithValue("@SUPPLIERINVOICENO", ObjStockEntry.SUPPLIERINVOICENO);
                    cmd.Parameters.AddWithValue("@INVOICEDATE", ObjStockEntry.InvoiceDate);
                    cmd.Parameters.AddWithValue("@TCS", ObjStockEntry.TCS);
                    cmd.Parameters.AddWithValue("@DISCOUNTFLAT", ObjStockEntry.DISCOUNTFLAT);
                    cmd.Parameters.AddWithValue("@EXPENSES", ObjStockEntry.EXPENSES);
                    cmd.Parameters.AddWithValue("@TRANSPORT", ObjStockEntry.TRANSPORT);
                    cmd.Parameters.AddWithValue("@PackingCharges", ObjStockEntry.PackingCharges);
                    cmd.Parameters.AddWithValue("@DispatchBranchId", ObjStockEntry.DispatchBranchID);
                    cmd.Parameters.AddWithValue("@CATEGORYID", ObjStockEntry.CATEGORYID);
                    cmd.Parameters.AddWithValue("@USERID", ObjStockEntry.UserID);
                    cmd.Parameters.AddWithValue("@SupplierIndentID", ObjStockEntry.SupplierIndentId);
                    cmd.Parameters.AddWithValue("@Notes", ObjStockEntry.Notes);
                    cmd.Parameters.AddWithValue("@dtcreditnotes", ObjStockEntry.dtCreditNote);
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
        public DataSet GetInvoice(object StockEntryID)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_STOCKENTRY_v2]";
                    cmd.Parameters.AddWithValue("@STOCKENTRYID", StockEntryID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retreiving Invoice", ex);
            }
            return ds;
        }
        public void DiscardStockEntry(object StockEntryID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
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
        }
        public DataSet GetDispatch(object StockDispatchID)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
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
            return ds;
        }
        private void RefreshObject(DataTable dataTable, StockEntryDetail stockEntryDetail)
        {
            stockEntryDetail.STOCKENTRYDETAILID = dataTable.Rows[0]["STOCKENTRYDETAILID"];
            stockEntryDetail.ITEMID = dataTable.Rows[0]["ITEMID"];
            stockEntryDetail.ITEMCODEID = dataTable.Rows[0]["ITEMCODEID"];
            stockEntryDetail.SKUCODE = dataTable.Rows[0]["SKUCODE"];
            stockEntryDetail.ITEMCODE = dataTable.Rows[0]["ITEMCODE"];
            stockEntryDetail.ITEMNAME = dataTable.Rows[0]["ITEMNAME"];
            stockEntryDetail.COSTPRICEWT = dataTable.Rows[0]["CPWITHTAX"];
            stockEntryDetail.COSTPRICEWOT = dataTable.Rows[0]["CPWITHOUTTAX"];
            stockEntryDetail.GROSSCOSTPRICEWT = dataTable.Rows[0]["INVOICECPWITHTAX"];
            stockEntryDetail.GROSSCOSTPRICEWOT = dataTable.Rows[0]["INVOICECPWITHOUTTAX"];
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
            stockEntryDetail.FinalPriceWOTax = dataTable.Rows[0]["FINALPRICEWOTAX"];
            stockEntryDetail.FinalPrice = dataTable.Rows[0]["FINALPRICE"];
            stockEntryDetail.CGST = dataTable.Rows[0]["CGST"];
            stockEntryDetail.SGST = dataTable.Rows[0]["SGST"];
            stockEntryDetail.IGST = dataTable.Rows[0]["IGST"];
            stockEntryDetail.CESS = dataTable.Rows[0]["CESS"];
            stockEntryDetail.HSNCODE = dataTable.Rows[0]["HSNCODE"];
            stockEntryDetail.IsFreeItem = dataTable.Rows[0]["ISFREEITEM"];
        }
    }
}
