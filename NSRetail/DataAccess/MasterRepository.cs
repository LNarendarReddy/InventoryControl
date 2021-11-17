using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MasterRepository
    {
        public Branch SaveBranch(Branch ObjBranch)
        {
            int BRanchID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_BRANCH]";
                    cmd.Parameters.Add("@BRANCHID", ObjBranch.BRANCHID);
                    cmd.Parameters.Add("@BRANCHNAME", ObjBranch.BRANCHNAME);
                    cmd.Parameters.Add("@BRANCHCODE", ObjBranch.BRANCHCODE);
                    cmd.Parameters.Add("@DESCRIPTION", ObjBranch.DESCRIPTION);
                    cmd.Parameters.Add("@ADDRESS", ObjBranch.ADDRESS);
                    cmd.Parameters.Add("@PHONENO", ObjBranch.PHONENO);
                    cmd.Parameters.Add("@EMAILID", ObjBranch.EMAILID);
                    cmd.Parameters.Add("@USERID", ObjBranch.USERID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out BRanchID))
                        throw new Exception(str);
                    else
                        ObjBranch.BRANCHID = objReturn;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_BName"))
                    throw new Exception("Branch Already Exists!!");
                else
                    throw new Exception("Error While Saving Branch");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjBranch;
        }
        public DataTable GetBranch()
        {
            DataTable dtBranch = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_BRANCH]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtBranch);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Branch List");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtBranch;
        }
        public Category SaveCategory(Category ObjCategory)
        {
            int CategoryID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_CATEGORY]";
                    cmd.Parameters.Add("@CATEGORYID", ObjCategory.CATEGORYID);
                    cmd.Parameters.Add("@CATEGORYNAME", ObjCategory.CATEGORYNAME);
                    cmd.Parameters.Add("@USERID", ObjCategory.USERID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out CategoryID))
                        throw new Exception(str);
                    else
                        ObjCategory.CATEGORYID = objReturn;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_CATEGORYNAME"))
                    throw new Exception("Category Already Exists!!");
                else
                    throw new Exception("Error While Saving Category");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjCategory;
        }
        public DataTable GetCategory()
        {
            DataTable dtCategory = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_CATEGORY]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Category List");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtCategory;
        }
    }
}
