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
                    cmd.Parameters.Add("@DESCRIPTION", ObjBranch.Description);
                    cmd.Parameters.Add("@ADDRESS", ObjBranch.ADDRESS);
                    cmd.Parameters.Add("@PHONENO", ObjBranch.PHONENO);
                    cmd.Parameters.Add("@EMAILID", ObjBranch.EMAILID);
                    cmd.Parameters.Add("@USERID", ObjBranch.UserID);
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
        public Branch DeleteBranch(Branch ObjBranch)
        {
            int BRanchID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_BRANCH]";
                    cmd.Parameters.Add("@BRANCHID", ObjBranch.BRANCHID);
                    cmd.Parameters.Add("@USERID", ObjBranch.UserID);
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
                throw new Exception("Error While Deleteing Branch");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjBranch;
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
                    cmd.Parameters.Add("@USERID", ObjCategory.UserID);
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
        public Category DeleteCategory(Category ObjCategory)
        {
            int CategoryID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_CATEGORY]";
                    cmd.Parameters.Add("@CATEGORYID", ObjCategory.CATEGORYID);
                    cmd.Parameters.Add("@USERID", ObjCategory.UserID);
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
                throw new Exception("Error While Deleteing Category");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjCategory;
        }
        public User SaveUser(User ObjUser)
        {
            int UserID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_USER]";
                    cmd.Parameters.Add("@USERID", ObjUser.USERID);
                    cmd.Parameters.Add("@ROLEID", ObjUser.ROLEID);
                    cmd.Parameters.Add("@REPORTINGLEADID", ObjUser.REPORTINGLEADID);
                    cmd.Parameters.Add("@CATEGORYID", ObjUser.CATEGORYID);
                    cmd.Parameters.Add("@BRANCHID", ObjUser.BRANCHID);
                    cmd.Parameters.Add("@USERNAME", ObjUser.USERNAME);
                    cmd.Parameters.Add("@PASSWORDSTRING", ObjUser.PASSWORDSTRING);
                    cmd.Parameters.Add("@FULLNAME", ObjUser.FULLNAME);
                    cmd.Parameters.Add("@EMAIL", ObjUser.EMAIL);
                    cmd.Parameters.Add("@CNUMBER", ObjUser.CNUMBER);
                    cmd.Parameters.Add("@GENDER", ObjUser.GENDER);
                    cmd.Parameters.Add("@CUSERID", ObjUser.CUSERID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out UserID))
                        throw new Exception(str);
                    else
                        ObjUser.USERID = objReturn;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_USERNAME"))
                    throw new Exception("User Already Exists!!");
                else
                    throw new Exception("Error While Saving User");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjUser;
        }
        public DataTable GetUser()
        {
            DataTable dtUser = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_USER]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtUser);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving User List");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtUser;
        }
        public User DeleteUser(User ObjUser)
        {
            int UserID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_USER]";
                    cmd.Parameters.Add("@USERID", ObjUser.USERID);
                    cmd.Parameters.Add("@DUSERID", ObjUser.CUSERID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out UserID))
                        throw new Exception(str);
                    else
                        ObjUser.USERID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing User");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjUser;
        }
        public DataTable GetRole()
        {
            DataTable dtRole = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ROLE]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtRole);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Role List");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtRole;
        }
        public DataTable GetUserCredentials(string UserName, string Password)
        {
            DataTable dtUser = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_USERLOGIN]";
                    cmd.Parameters.Add("@USERNAME", UserName);
                    cmd.Parameters.Add("@PASSWORD", Password);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtUser);
                    }
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        int Ivalue = 0;
                        string str = Convert.ToString(dtUser.Rows[0][0]);
                        if (!int.TryParse(str, out Ivalue))
                            throw new Exception(str);
                    }
                    else
                        throw new Exception("Error in login");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { SQLCon.Sqlconn().Close(); }
            return dtUser;
        }
        public DataTable ChangePassword(User ObjUser)
        {
            DataTable dtUser = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_PASSWORD]";
                    cmd.Parameters.Add("@USERID", ObjUser.USERID);
                    cmd.Parameters.Add("@PASSWORDSTRING", ObjUser.PASSWORDSTRING);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtUser);
                    }
                    if (dtUser != null && dtUser.Rows.Count > 0)
                    {
                        int Ivalue = 0;
                        string str = Convert.ToString(dtUser.Rows[0][0]);
                        if (!int.TryParse(str, out Ivalue))
                            throw new Exception(str);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { SQLCon.Sqlconn().Close(); }
            return dtUser;
        }
        public Dealer SaveDealer(Dealer ObjDealer)
        {
            int DealerID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_DEALER]";
                    cmd.Parameters.Add("@DEALERID", ObjDealer.DEALERID);
                    cmd.Parameters.Add("@DEALERNAME", ObjDealer.DEALERNAME);
                    cmd.Parameters.Add("@ADDRESS", ObjDealer.ADDRESS);
                    cmd.Parameters.Add("@PHONENO", ObjDealer.PHONENO);
                    cmd.Parameters.Add("@GSTIN", ObjDealer.GSTIN);
                    cmd.Parameters.Add("@EMAILID", ObjDealer.EMAILID);
                    cmd.Parameters.Add("@USERID", ObjDealer.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out DealerID))
                        throw new Exception(str);
                    else
                        ObjDealer.DEALERID = objReturn;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_DEALERNAME"))
                    throw new Exception("Dealer Already Exists!!");
                else
                    throw new Exception("Error While Saving Dealer");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjDealer;
        }
        public DataTable GetDealer()
        {
            DataTable dtDealer = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_DEALER]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtDealer);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Dealer List");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtDealer;
        }
        public Dealer DeleteDealer (Dealer ObjDealer)
        {
            int DealerID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_DEALER]";
                    cmd.Parameters.Add("@DEALERID", ObjDealer.DEALERID);
                    cmd.Parameters.Add("@USERID", ObjDealer.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out DealerID))
                        throw new Exception(str);
                    else
                        ObjDealer.DEALERID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing Dealer");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjDealer;
        }
    }
}
