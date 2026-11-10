using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class StockTransferRepository
    {
        public DataSet GetLookups()
        {
            using (SqlCommand command = Command("USP_R_SKUSTOCKTRANSFERLOOKUPS"))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataSet result = new DataSet();
                adapter.Fill(result);
                return result;
            }
        }

        public int Save(int branchId, int adjustmentTypeId, int userId, string remarks, DataTable pairs)
        {
            using (SqlCommand command = Command("USP_C_SKUSTOCKTRANSFER"))
            {
                command.Parameters.Add("@BRANCHID", SqlDbType.Int).Value = branchId;
                command.Parameters.Add("@STOCKADJUSTMENTTYPEID", SqlDbType.Int).Value = adjustmentTypeId;
                command.Parameters.Add("@USERID", SqlDbType.Int).Value = userId;
                command.Parameters.Add("@REMARKS", SqlDbType.NVarChar, 500).Value = remarks;
                SqlParameter parameter = command.Parameters.Add("@PAIRS", SqlDbType.Structured);
                parameter.TypeName = "dbo.SKUSTOCKTRANSFERPAIRTYPE";
                parameter.Value = pairs;
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public DataTable GetBatches(int branchId)
        {
            return Read("USP_R_STOCKTRANSFERLIST", "@BRANCHID", branchId);
        }

        public DataTable GetDetails(int transferId)
        {
            return Read("USP_R_STOCKTRANSFERDETAIL", "@STOCKTRANSFERID", transferId);
        }

        private static SqlCommand Command(string name)
        {
            return new SqlCommand("dbo." + name, SQLCon.Sqlconn()) { CommandType = CommandType.StoredProcedure };
        }

        private static DataTable Read(string procedure, string parameter, int value)
        {
            using (SqlCommand command = Command(procedure))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.Add(parameter, SqlDbType.Int).Value = value;
                DataTable result = new DataTable();
                adapter.Fill(result);
                return result;
            }
        }
    }
}
