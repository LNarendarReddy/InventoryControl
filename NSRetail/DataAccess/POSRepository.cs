using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class POSRepository
    {
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
                SQLCon.Sqlconn().Close();
            }
            return dtBRefund;
        }
        public DataTable GetBRefundDetail(object BRefundID,object CounterID)
        {
            DataTable dtBRefundDetail = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_BREFUNDDETAIL]";
                    cmd.Parameters.AddWithValue("@BREFUNDID", BRefundID);
                    cmd.Parameters.AddWithValue("@COUNTERID", CounterID);
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
                SQLCon.Sqlconn().Close();
            }
            return dtBRefundDetail;
        }
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
                SQLCon.Sqlconn().Close();
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
                SQLCon.Sqlconn().Close();
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
                SQLCon.Sqlconn().Close();
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
                SQLCon.Sqlconn().Close();
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
                SQLCon.Sqlconn().Close();
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
                SQLCon.Sqlconn().Close();
            }
            return dsDayClosureVoidItems;
        }
        public DataSet GetBillDetailByID(object CounterID, object DayClosureID)
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
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsbillDetail;
        }
        public void AcceptBRefund(object CounterID, object BRefundID,object UserID ,DataTable dtBRefundDetail)
        {
            try
             {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_BREFUND]";
                    cmd.Parameters.AddWithValue("@dtbrd", dtBRefundDetail);
                    cmd.Parameters.AddWithValue("@COUNTERID", CounterID);
                    cmd.Parameters.AddWithValue("@BREFUNDID", BRefundID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Accepting Branch Refund", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }
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
                SQLCon.Sqlconn().Close();
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
                SQLCon.Sqlconn().Close();
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
                SQLCon.Sqlconn().Close();
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
                SQLCon.Sqlconn().Close();
            }
            return dtItemwiseSale;
        }
    }
}
