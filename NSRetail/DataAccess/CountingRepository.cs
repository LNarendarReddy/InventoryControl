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
                
            }
            return dtStockCountingDiff;
        }

        public void AcceptStockCounting(object BranchID, string selectedCategories)
        {
            SqlTransaction sqlTransaction = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    sqlTransaction = SQLCon.Sqlconn().BeginTransaction();
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.Transaction =   sqlTransaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_STOCKCOUNTING]";
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    cmd.Parameters.AddWithValue("@SelectedCategories", selectedCategories);
                    object objReturn = cmd.ExecuteScalar();

                    if (!int.TryParse(Convert.ToString(objReturn), out int ivalue))
                        throw new Exception(Convert.ToString(objReturn));
                    sqlTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                if (ex.Message.Contains("Pending sheets") ||
                    ex.Message.Contains("No Counting") ||
                    ex.Message.Contains("Multiple categories"))
                    throw ex;
                else
                    throw new Exception("Error While Accepting Stock Counting", ex);
            }
            finally
            {
                
            }
        }

        public DataTable GetConsolidatedItems(object BranchID)
        {
            DataTable dtStockCounting = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_CONSOLIDATEDSTOCKCOUNTING]";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtStockCounting);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Consolidated Items", ex);
            }
            finally
            {
                
            }
            return dtStockCounting;
        }

        public DataTable ViewCountingDetails(object BranchID, object ItemID)
        {
            DataTable dtStockCounting = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_COUNTINGDETAIL]";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                    cmd.Parameters.AddWithValue("@ItemID", ItemID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtStockCounting);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Counting Details", ex);
            }
            finally
            {
                
            }
            return dtStockCounting;
        }
    }
}
