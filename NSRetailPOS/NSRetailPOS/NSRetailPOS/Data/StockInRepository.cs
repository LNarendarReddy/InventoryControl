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
    }
}
