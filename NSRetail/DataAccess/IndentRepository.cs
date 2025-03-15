using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace DataAccess
{
    public class IndentRepository
    {
        public void SaveBranchIndent(int BranchIndentID, int FromBranchID, object ToBranchID, 
            object CategotyID, object NoOfDays, int UserID, DataTable dt)
        {
            List<string> allowedcolumns = new List<string>() 
            {   "ITEMID", 
                "BRANCHSTOCK", 
                "AVGSALES", 
                "NOOFDAYSSALES", 
                "INDENTQUANTITY", 
                "LASTDISPATCHDATE", 
                "SUBCATEGORYID" 
            };
            var columnsToRemove = dt.Columns.Cast<DataColumn>().Select(x => x.ColumnName).Where(x => !allowedcolumns.Contains(x)).ToList();
            columnsToRemove.ForEach(x => dt.Columns.Remove(x));

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_BRANCHINDENT_v2]";
                    cmd.Parameters.AddWithValue("@BRANCHINDENTID", BranchIndentID);
                    cmd.Parameters.AddWithValue("@FROMBRANCHID", FromBranchID);
                    cmd.Parameters.AddWithValue("@TOBRANCHID", ToBranchID);
                    cmd.Parameters.AddWithValue("@CATEGORYID", CategotyID);
                    cmd.Parameters.AddWithValue("@NOOFDAYS", NoOfDays);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    cmd.Parameters.AddWithValue("@dtDetail", dt);
                    object obj = cmd.ExecuteScalar();
                    string str = Convert.ToString(obj);
                    if (!int.TryParse(str, out int iValue))
                    {
                        if (str.Contains("Indent cannot be created") || str.Contains("Few indents"))
                            throw new Exception(str);
                        else if (iValue <= 0)
                            throw new Exception("Something went wrong");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIndentItems(object BranchIndentID, object StockDispatchID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_INDENTITEMS]";
                    cmd.Parameters.AddWithValue("@BRANCHINDENTID", BranchIndentID);
                    cmd.Parameters.AddWithValue("@STOCKDISPATCHID", StockDispatchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public int DiscardIndent(object BranchIndentID, object UserID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_BRANCHINDENT]";
                    cmd.Parameters.AddWithValue("@BRANCHINDENTID", BranchIndentID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected <= 0)
                        throw new Exception("Something went wrong");
                    return rowsAffected;
               }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
