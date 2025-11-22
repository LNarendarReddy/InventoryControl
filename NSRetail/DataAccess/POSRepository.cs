using Entity;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class POSRepository
    {
        [Obsolete]
        public DataTable GetBRefund(object BranchID,object FromDate,object ToDate)
        {
            DataTable dtBRefund = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_BREFUND]";
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtBRefund);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Branch Refund List", ex);
            }
            finally
            {
                
            }
            return dtBRefund;
        }
        public DataTable GetBRefundDetail(object BRID)
        {
            DataTable dtBRefundDetail = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_BREFUNDDETAIL_1]";
                    cmd.Parameters.AddWithValue("@BRID", BRID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtBRefundDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Branch Refund Details", ex);
            }
            finally
            {
                
            }
            return dtBRefundDetail;
        }

        [Obsolete]
        public DataTable GetDayClosure(object BranchID,object FromDate,object ToDate)
        {
            DataTable dtDayClosure = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_DAYCLOSURE]";
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtDayClosure);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Day Closure List", ex);
            }
            finally
            {
                
            }
            return dtDayClosure;
        }
        public DataTable GetDayClosureDetail(object BRefundID, object CounterID)
        {
            DataTable dtDayClosureDetail = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_DAYCLOSUREDETAIL]";
                    cmd.Parameters.AddWithValue("@DAYCLOSUREID", BRefundID);
                    cmd.Parameters.AddWithValue("@COUNTERID", CounterID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtDayClosureDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Day Closure Details", ex);
            }
            finally
            {
                
            }
            return dtDayClosureDetail;
        }
        public DataTable GetDayClosureBills(object CounterID, object DayClosureID)
        {
            DataTable dtDayClosureBill = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_DAYCLOSUREBILL]";
                    cmd.Parameters.AddWithValue("@COUNTERID", CounterID);
                    cmd.Parameters.AddWithValue("@DAYCLOSUREID", DayClosureID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtDayClosureBill);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Day Closure Bills", ex);
            }
            finally
            {
                
            }
            return dtDayClosureBill;
        }
        public DataSet GetDayClosureItems(object CounterID, object DayClosureID)
        {
            DataSet dsDayClosureItems = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_DAYCLOSUREITEMS]";
                    cmd.Parameters.AddWithValue("@BRANCHCOUNTERID", CounterID);
                    cmd.Parameters.AddWithValue("@DAYCLOSUREID", DayClosureID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsDayClosureItems);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Day Closure Items", ex);
            }
            finally
            {
                
            }
            return dsDayClosureItems;
        }
        public DataSet GetDayClosureRefund(object CounterID, object DayClosureID)
        {
            DataSet dsDayClosureRefund = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_DAYCLOSUREREFUND]";
                    cmd.Parameters.AddWithValue("@BRANCHCOUNTERID", CounterID);
                    cmd.Parameters.AddWithValue("@DAYCLOSUREID", DayClosureID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsDayClosureRefund);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Day Closure Refunds", ex);
            }
            finally
            {
                
            }
            return dsDayClosureRefund;
        }
        public DataSet GetDayClosureVoidItems(object CounterID, object DayClosureID)
        {
            DataSet dsDayClosureVoidItems = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_DAYCLOSUREVOIDITEMS]";
                    cmd.Parameters.AddWithValue("@COUNTERID", CounterID);
                    cmd.Parameters.AddWithValue("@DAYCLOSUREID", DayClosureID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsDayClosureVoidItems);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Day Closure Void Items", ex);
            }
            finally
            {
                
            }
            return dsDayClosureVoidItems;
        }
        public DataSet GetBillDetailByID(object CounterID, object DayClosureID, bool IncludeVoidItems = false)
        {
            DataSet dsbillDetail = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_BILLDETAILBYID]";
                    cmd.Parameters.AddWithValue("@BRANCHCOUNTERID", CounterID);
                    cmd.Parameters.AddWithValue("@BILLID", DayClosureID);
                    cmd.Parameters.AddWithValue("@INCLUDEVOIDITEMS", IncludeVoidItems);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsbillDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving bill Detail", ex);
            }
            return dsbillDetail;
        }
        public DataSet GetRefundItemsByID(object CounterID, object BillID)
        {
            DataSet dtbillDetail = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_CREFUNDITEMS]";
                    cmd.Parameters.AddWithValue("@BRANCHCOUNTERID", CounterID);
                    cmd.Parameters.AddWithValue("@BILLID", BillID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtbillDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving customer refunds", ex);
            }
            return dtbillDetail;
        }
        public void AcceptBRefund(object BRID,object UserID ,DataTable dtBRefundDetail)
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
                    cmd.CommandText = "[USP_U_BREFUND_1]";
                    cmd.Parameters.AddWithValue("@dtbrd", dtBRefundDetail);
                    cmd.Parameters.AddWithValue("@BRID", BRID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    object objReturn = cmd.ExecuteScalar();
                    if(!int.TryParse(Convert.ToString(objReturn), out int id))
                        throw new Exception(Convert.ToString(objReturn));
                    sqlTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                if (ex.Message.Contains("Few items") || ex.Message.Contains("already accepted"))
                    throw ex;
                else
                    throw new Exception("Error While Accepting Branch Refund", ex);
            }
        }

        [Obsolete]
        public DataTable GetRunningSale(object BranchID)
        {
            DataTable dtRunningSale = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_RPT_RUNNINGSALE]";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtRunningSale);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Running Sale", ex);
            }
            finally
            {
                
            }
            return dtRunningSale;
        }
        public DataTable GetTaxWiseSales(object BranchID)
        {
            DataTable dtTaxWiseSale = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_RPT_BRANCHSALEBYGST]";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtTaxWiseSale);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Taxwise Sales", ex);
            }
            finally
            {
                
            }
            return dtTaxWiseSale;
        }
        public DataTable GetZeroStock(object BranchID)
        {
            DataTable dtTaxWiseSale = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_RPT_ZEROSTOCK]";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtTaxWiseSale);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Zero Stock", ex);
            }
            finally
            {
                
            }
            return dtTaxWiseSale;
        }
        public DataTable GetItemwiseSales(object BranchID,object FromDate,object ToDate, object includeBillDate)
        {
            DataTable dtItemwiseSale = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_RPT_ITEMWISESALE]";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                    cmd.Parameters.AddWithValue("@IncludeBillDate", includeBillDate);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItemwiseSale);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Itemwise Sale", ex);
            }
            finally
            {
                
            }
            return dtItemwiseSale;
        }

        public void SaveCreditBillPayment(CreditBillPayment creditBillPayment)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_CREDITBILLPAYMENT]";
                    cmd.Parameters.AddWithValue("@CreditBillPaymentID", creditBillPayment.CreditBillPaymentID);
                    cmd.Parameters.AddWithValue("@Status", creditBillPayment.Status);
                    cmd.Parameters.AddWithValue("@Description", creditBillPayment.Description);
                    cmd.Parameters.AddWithValue("@UserID", creditBillPayment.UserID);
                    cmd.ExecuteNonQuery();
                    creditBillPayment.IsSave = true;                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }

        public object GetBranchExpenseImage(object branchExpenseID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_BRANCHEXPENSE_IMAGE]";
                    cmd.Parameters.AddWithValue("@BranchExpenseID", branchExpenseID);
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While getting Branch expense image - {ex.Message}");
            }
        }
    }
}
