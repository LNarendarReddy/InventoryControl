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
                    cmd.Parameters.Add("@BRANCHID", BranchID);
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
                    cmd.Parameters.Add("@BREFUNDID", BRefundID);
                    cmd.Parameters.Add("@COUNTERID", CounterID);
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
                    cmd.Parameters.Add("@BRANCHID", BranchID);
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
                    cmd.Parameters.Add("@DAYCLOSUREID", BRefundID);
                    cmd.Parameters.Add("@COUNTERID", CounterID);
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
    }
}
