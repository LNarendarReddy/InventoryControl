using NSRetailPOS.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailPOS.Data
{
    internal class OperationsRepository
    {
        public void SaveBranchExpense(BranchExpense branchExpense)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_BRANCHEXPENSE]";
                    cmd.Parameters.AddWithValue("@BranchExpenseID", branchExpense.BranchExpenseID);
                    cmd.Parameters.AddWithValue("@BranchExpenseTypeID", branchExpense.BranchExpenseTypeID);
                    cmd.Parameters.AddWithValue("@BranchID", Utility.branchInfo.BranchID);
                    cmd.Parameters.AddWithValue("@Description", branchExpense.Description);
                    cmd.Parameters.AddWithValue("@Amount", branchExpense.Amount);
                    cmd.Parameters.AddWithValue("@UserID", Utility.loginInfo.UserID);
                    object procValue = cmd.ExecuteScalar();

                    string str = Convert.ToString(procValue);
                    if (!int.TryParse(str, out int BranchExpenseID))
                        throw new Exception(str);
                    else
                    {
                        branchExpense.BranchExpenseID = BranchExpenseID;
                        branchExpense.IsSave = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error While Saving Branch expense - {ex.Message}");
            }
        }
       
        public void DeleteBranchExpense(BranchExpense branchExpense)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_BRANCHEXPENSE]";
                    cmd.Parameters.AddWithValue("@BranchExpenseID", branchExpense.BranchExpenseID);
                    cmd.Parameters.AddWithValue("@UserID", Utility.loginInfo.UserID);
                    cmd.ExecuteNonQuery();
                    branchExpense.IsSave = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While Deleting Branch expense - {ex.Message}");
            }
        }
    }
}
