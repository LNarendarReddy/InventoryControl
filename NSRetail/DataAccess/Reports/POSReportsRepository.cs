using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.Reports
{
    public class POSReportsRepository
    {
        public DataTable GetBranchRefundItemWise(object BranchID, object FromDate, object ToDate)
        {
            DataTable dtItemwiseBR = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_RPT_ITEMWISEBRANCHREFUND]";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                    cmd.Parameters.AddWithValue("@FromDate", FromDate);
                    cmd.Parameters.AddWithValue("@ToDate", ToDate);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItemwiseBR);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Itemwise branch refunds", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtItemwiseBR;
        }
    }
}
