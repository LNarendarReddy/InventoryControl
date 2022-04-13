using System;
using System.Data;
using System.Data.SqlClient;

namespace NSRetailPOS.Data
{
    public class StockInRepository
    {
        public DataTable GetStockDispatches(object branchID)
        {
            DataTable dtStockDispatches = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_STOCKDISPATCH]";
                    cmd.Parameters.AddWithValue("@BranchID", branchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtStockDispatches);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While getting stock in list", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtStockDispatches;
        }

        public DataTable GetStockDispatchDetail(object stockDispatchID)
        {
            DataTable dtStockDispatchDetail = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_STOCKDISPATCHDETAIL]";
                    cmd.Parameters.AddWithValue("@StockDispatchID", stockDispatchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtStockDispatchDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While getting stock in detail", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtStockDispatchDetail;
        }

        public void AcceptDispatch(object stockDispatchID, DataTable dtDispatchDetail)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_FINISH_STOCKIN]";
                    cmd.Parameters.AddWithValue("@UserID", Utility.loginInfo.UserID);
                    cmd.Parameters.AddWithValue("@StockDispatchID", stockDispatchID);
                    cmd.Parameters.AddWithValue("@DispatchDetails", dtDispatchDetail);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While closing stock in", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }
    }    
}
