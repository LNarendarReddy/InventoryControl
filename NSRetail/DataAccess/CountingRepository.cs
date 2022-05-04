using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CountingRepository
    {
        public DataTable GetStockCounting(object BranchID)
        {
            DataTable dtStockCounting = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_STOCKCOUNTING]";
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtStockCounting);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Stock Counting List", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtStockCounting;
        }
        
        public DataTable GetStockCountingDetail(object StockCountingID)
        {
            DataTable dtStockCountingDetail = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_STOCKCOUNTINGDETAIL]";
                    cmd.Parameters.AddWithValue("@STOCKCOUNTINGID", StockCountingID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtStockCountingDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Stock Counting Detail", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtStockCountingDetail;
        }

        public DataTable GetStockCountingDiff(object BranchID)
        {
            DataTable dtStockCountingDiff = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_STOCKCONTINGDIFF]";
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtStockCountingDiff);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Stock Counting Difference", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtStockCountingDiff;
        }

        public DataTable GetStockCountingNoteEntered(object BranchID)
        {
            DataTable dtStockCountingDiff = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_STOCKCONTINGNOTENTERED]";
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtStockCountingDiff);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Stock Counting Not Entered", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtStockCountingDiff;
        }

        public void AcceptStockCounting(object BranchID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_STOCKCOUNTING]";
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    object objReturn = cmd.ExecuteScalar();

                    if (!int.TryParse(Convert.ToString(objReturn), out int ivalue))
                        throw new Exception(Convert.ToString(objReturn));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Accepting Stock Counting", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }
    }
}
