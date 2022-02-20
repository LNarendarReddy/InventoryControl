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
        public DataTable GetBRefund(object BranchID)
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
        public DataTable GetDayClosure(object BranchID)
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
        public DataTable GetDayClosureItems(object CounterID, object DayClosureID)
        {
            DataTable dtDayClosureItems = new DataTable();
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
                        da.Fill(dtDayClosureItems);
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
            return dtDayClosureItems;
        }
        public DataTable GetDayClosureRefund(object CounterID, object DayClosureID)
        {
            DataTable dtDayClosureRefund = new DataTable();
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
                        da.Fill(dtDayClosureRefund);
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
            return dtDayClosureRefund;
        }
        public DataTable GetDayClosureVoidItems(object CounterID, object DayClosureID)
        {
            DataTable dtDayClosureVoidItems = new DataTable();
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
                        da.Fill(dtDayClosureVoidItems);
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
            return dtDayClosureVoidItems;
        }
        public DataTable GetBillDetailByID(object CounterID, object DayClosureID)
        {
            DataTable dtbillDetail = new DataTable();
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
                        da.Fill(dtbillDetail);
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
            return dtbillDetail;
        }
    }
}
