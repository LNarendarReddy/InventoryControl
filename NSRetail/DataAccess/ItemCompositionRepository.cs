using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ItemCompositionRepository
    {
        public DataSet GetComposition(int destinationItemId)
        {
            using (SqlCommand command = new SqlCommand("dbo.USP_R_ITEMCOMPOSITION", SQLCon.Sqlconn()))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@DESTINATIONITEMID", SqlDbType.Int).Value = destinationItemId;
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet result = new DataSet();
                    adapter.Fill(result);
                    return result;
                }
            }
        }

        public void SaveComposition(int destinationItemId, int revision, DataTable ingredients, int userId)
        {
            using (SqlCommand command = new SqlCommand("dbo.USP_CU_ITEMCOMPOSITION", SQLCon.Sqlconn()))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@DESTINATIONITEMID", SqlDbType.Int).Value = destinationItemId;
                command.Parameters.Add("@EXPECTEDREVISION", SqlDbType.Int).Value = revision;
                command.Parameters.Add("@USERID", SqlDbType.Int).Value = userId;
                SqlParameter detail = command.Parameters.Add("@INGREDIENTS", SqlDbType.Structured);
                detail.TypeName = "dbo.ITEMCOMPOSITIONDETAILTYPE";
                detail.Value = ingredients;
                command.ExecuteNonQuery();
            }
        }
    }
}
