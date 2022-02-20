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
                    cmd.Parameters.AddWithValue("@BRANCHID", ObjBranch.BRANCHID);
                    cmd.Parameters.AddWithValue("@BRANCHNAME", ObjBranch.BRANCHNAME);
                    cmd.Parameters.AddWithValue("@BRANCHCODE", ObjBranch.BRANCHCODE);
                    cmd.Parameters.AddWithValue("@ADDRESS", ObjBranch.ADDRESS);
                    cmd.Parameters.AddWithValue("@STATEID", ObjBranch.STATEID);
                    cmd.Parameters.AddWithValue("@PHONENO", ObjBranch.PHONENO);
                    cmd.Parameters.AddWithValue("@LANDLINE", ObjBranch.LANDLINE);
                    cmd.Parameters.AddWithValue("@EMAILID", ObjBranch.EMAILID);
                    cmd.Parameters.AddWithValue("@USERID", ObjBranch.UserID);
                    cmd.Parameters.AddWithValue("@ISWAREHOUSE", ObjBranch.ISWAREHOUSE);
                    cmd.Parameters.AddWithValue("@SUPERVISORID", ObjBranch. SUPERVISERID);
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
                    cmd.Parameters.AddWithValue("@BRANCHID", ObjBranch.BRANCHID);
                    cmd.Parameters.AddWithValue("@USERID", ObjBranch.UserID);
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
                    cmd.Parameters.AddWithValue("@CATEGORYID", ObjCategory.CATEGORYID);
                    cmd.Parameters.AddWithValue("@CATEGORYNAME", ObjCategory.CATEGORYNAME);
                    cmd.Parameters.AddWithValue("@ALLOWOPENITEMS", ObjCategory.AllowOpenItems);
                    cmd.Parameters.AddWithValue("@USERID", ObjCategory.UserID);
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
                    cmd.Parameters.AddWithValue("@CATEGORYID", ObjCategory.CATEGORYID);
                    cmd.Parameters.AddWithValue("@USERID", ObjCategory.UserID);
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
        public SubCategory SaveSubCategory(SubCategory ObjSubCategory)
        {
            int SubCategoryID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_SUBCATEGORY]";
                    cmd.Parameters.AddWithValue("@SUBCATEGORYID", ObjSubCategory.SUBCATEGORYID);
                    cmd.Parameters.AddWithValue("@SUBCATEGORYNAME", ObjSubCategory.SUBCATEGORYNAME);
                    cmd.Parameters.AddWithValue("@CATEGORYID", ObjSubCategory.CATEGORYID);
                    cmd.Parameters.AddWithValue("@USERID", ObjSubCategory.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out SubCategoryID))
                        throw new Exception(str);
                    else
                        ObjSubCategory.SUBCATEGORYID = objReturn;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_COUNTERNAME"))
                    throw new Exception("Sub Category Already Exists!!");
                else
                    throw new Exception("Error While Saving Sub Category");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjSubCategory;
        }
        public DataTable GetSubCategory()
        {
            DataTable dtSubCategory = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_SUBCATEGORY]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtSubCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Sub Category List");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtSubCategory;
        }
        public SubCategory DeleteSubCategory(SubCategory ObjSubCategory)
        {
            int SubCategoryID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_SUBCATEGORY]";
                    cmd.Parameters.AddWithValue("@SUBCATEGORYID", ObjSubCategory.SUBCATEGORYID);
                    cmd.Parameters.AddWithValue("@USERID", ObjSubCategory.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out SubCategoryID))
                        throw new Exception(str);
                    else
                        ObjSubCategory.SUBCATEGORYID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing Sub Category");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjSubCategory;
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
                    cmd.Parameters.AddWithValue("@USERID", ObjUser.USERID);
                    cmd.Parameters.AddWithValue("@ROLEID", ObjUser.ROLEID);
                    cmd.Parameters.AddWithValue("@REPORTINGLEADID", ObjUser.REPORTINGLEADID);
                    cmd.Parameters.AddWithValue("@CATEGORYID", ObjUser.CATEGORYID);
                    cmd.Parameters.AddWithValue("@BRANCHID", ObjUser.BRANCHID);
                    cmd.Parameters.AddWithValue("@USERNAME", ObjUser.USERNAME);
                    cmd.Parameters.AddWithValue("@PASSWORDSTRING", ObjUser.PASSWORDSTRING);
                    cmd.Parameters.AddWithValue("@FULLNAME", ObjUser.FULLNAME);
                    cmd.Parameters.AddWithValue("@EMAIL", ObjUser.EMAIL);
                    cmd.Parameters.AddWithValue("@CNUMBER", ObjUser.CNUMBER);
                    cmd.Parameters.AddWithValue("@GENDER", ObjUser.GENDER);
                    cmd.Parameters.AddWithValue("@CUSERID", ObjUser.CUSERID);
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
                    cmd.Parameters.AddWithValue("@USERID", ObjUser.USERID);
                    cmd.Parameters.AddWithValue("@DUSERID", ObjUser.CUSERID);
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
        public DataSet GetUserCredentials(string UserName, string Password)
        {
            DataSet dSUser = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_USERLOGIN]";
                    cmd.Parameters.AddWithValue("@USERNAME", UserName);
                    cmd.Parameters.AddWithValue("@PASSWORD", Password);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dSUser);
                    }
                    if (dSUser != null && dSUser.Tables[0].Rows.Count > 0)
                    {
                        int Ivalue = 0;
                        string str = Convert.ToString(dSUser.Tables[0].Rows[0][0]);
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
            return dSUser;
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
                    cmd.Parameters.AddWithValue("@USERID", ObjUser.USERID);
                    cmd.Parameters.AddWithValue("@PASSWORDSTRING", ObjUser.PASSWORDSTRING);
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
        public void ResetPassword(object UserID, string PasswordString)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_CHANGEPASSWORD]";
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    cmd.Parameters.AddWithValue("@PASSWORDSTRING", PasswordString);
                    if (cmd.ExecuteNonQuery() == 0)
                        throw new Exception("Error while resetting the password");
                        
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { SQLCon.Sqlconn().Close(); }
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
                    cmd.Parameters.AddWithValue("@DEALERID", ObjDealer.DEALERID);
                    cmd.Parameters.AddWithValue("@DEALERNAME", ObjDealer.DEALERNAME);
                    cmd.Parameters.AddWithValue("@ADDRESS", ObjDealer.ADDRESS);
                    cmd.Parameters.AddWithValue("@STATEID", ObjDealer.STATEID);
                    cmd.Parameters.AddWithValue("@PHONENO", ObjDealer.PHONENO);
                    cmd.Parameters.AddWithValue("@GSTIN", ObjDealer.GSTIN);
                    cmd.Parameters.AddWithValue("@PANNUMBER", ObjDealer.PANNUMBER);
                    cmd.Parameters.AddWithValue("@EMAILID", ObjDealer.EMAILID);
                    cmd.Parameters.AddWithValue("@USERID", ObjDealer.UserID);
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
                    cmd.Parameters.AddWithValue("@DEALERID", ObjDealer.DEALERID);
                    cmd.Parameters.AddWithValue("@USERID", ObjDealer.UserID);
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
        public Counter SaveCounter(Counter ObjCounter)
        {
            int CounterID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_BRANCHCOUNTER]";
                    cmd.Parameters.AddWithValue("@COUNTERID", ObjCounter.COUNTERID);
                    cmd.Parameters.AddWithValue("@COUNTERNAME", ObjCounter.COUNTERNAME);
                    cmd.Parameters.AddWithValue("@BRANCHID", ObjCounter.BRANCHID);
                    cmd.Parameters.AddWithValue("@USERID", ObjCounter.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out CounterID))
                        throw new Exception(str);
                    else
                        ObjCounter.COUNTERID = objReturn;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_COUNTERNAME"))
                    throw new Exception("Counter Already Exists!!");
                else
                    throw new Exception("Error While Saving Counter");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjCounter;
        }
        public DataTable GetCounter()
        {
            DataTable dtCounter = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_BRANCHCOUNTER]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtCounter);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Counter List");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtCounter;
        }
        public Counter DeleteCounter(Counter ObjCounter)
        {
            int CounterID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_BRANCHCOUNTER]";
                    cmd.Parameters.AddWithValue("@COUNTERID", ObjCounter.COUNTERID);
                    cmd.Parameters.AddWithValue("@USERID", ObjCounter.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out CounterID))
                        throw new Exception(str);
                    else
                        ObjCounter.COUNTERID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing Counter");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjCounter;
        }
        public MOP SaveMOP(MOP ObjMOP)
        {
            int MOPID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_MOP]";
                    cmd.Parameters.AddWithValue("@MOPID", ObjMOP.MOPID);
                    cmd.Parameters.AddWithValue("@MOPNAME", ObjMOP.MOPNAME);
                    cmd.Parameters.AddWithValue("@USERID", ObjMOP.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out MOPID))
                        throw new Exception(str);
                    else
                        ObjMOP.MOPID = objReturn;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_MOPNAME"))
                    throw new Exception("MOP Already Exists!!");
                else
                    throw new Exception("Error While Saving MOP");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjMOP;
        }
        public DataTable GetMOP()
        {
            DataTable dtMOP = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_MOP]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtMOP);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving MOP List");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtMOP;
        }
        public MOP DeleteMOP(MOP ObjMOP)
        {
            int MOPID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_MOP]";
                    cmd.Parameters.AddWithValue("@CATEGORYID", ObjMOP.MOPID);
                    cmd.Parameters.AddWithValue("@USERID", ObjMOP.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out MOPID))
                        throw new Exception(str);
                    else
                        ObjMOP.MOPID = objReturn;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing MOP");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjMOP;
        }
        public UOM SaveUOM(UOM ObjUOM)
        {
            int UOMID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_UOM]";
                    cmd.Parameters.AddWithValue("@UOMID", ObjUOM.UOMID);
                    cmd.Parameters.AddWithValue("@DISPLAYVALUE", ObjUOM.DISPLAYVALUE);
                    cmd.Parameters.AddWithValue("@BASEUOMID", ObjUOM.BASEUOMID);
                    cmd.Parameters.AddWithValue("@MULTIPLIER", ObjUOM.MULTIPLIER);
                    cmd.Parameters.AddWithValue("@USERID", ObjUOM.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out UOMID))
                        throw new Exception(str);
                    else
                        ObjUOM.UOMID = objReturn;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_UOMNAME"))
                    throw new Exception("UOM Already Exists!!");
                else
                    throw new Exception("Error While Saving UOM");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjUOM;
        }
        public DataTable GetUOM()
        {
            DataTable dtUOM = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_UOM]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtUOM);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving UOM List");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtUOM;
        }
        public UOM DeleteUOM(UOM ObjUOM)
        {
            int UOMID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_UOM]";
                    cmd.Parameters.AddWithValue("@UOMID", ObjUOM.UOMID);
                    cmd.Parameters.AddWithValue("@USERID", ObjUOM.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out UOMID))
                        throw new Exception(str);
                    else
                        ObjUOM.UOMID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing UOM");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjUOM;
        }
        public GST SaveGST(GST ObjGST)
        {
            int GSTID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_GST]";
                    cmd.Parameters.AddWithValue("@GSTID", ObjGST.GSTID);
                    cmd.Parameters.AddWithValue("@GSTCODE", ObjGST.GSTCODE);
                    cmd.Parameters.AddWithValue("@CGST", ObjGST.CGST);
                    cmd.Parameters.AddWithValue("@SGST", ObjGST.SGST);
                    cmd.Parameters.AddWithValue("@IGST", ObjGST.IGST);
                    cmd.Parameters.AddWithValue("@CESS", ObjGST.CESS);
                    cmd.Parameters.AddWithValue("@USERID", ObjGST.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out GSTID))
                        throw new Exception(str);
                    else
                        ObjGST.GSTID = objReturn;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_GSTCODE"))
                    throw new Exception("GST Already Exists!!");
                else
                    throw new Exception("Error While Saving GST");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjGST;
        }
        public DataTable GetGST()
        {
            DataTable dtGST = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_GST]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtGST);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving GST List");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtGST;
        }
        public GST DeleteGST(GST ObjGST)
        {
            int GSTID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_GST]";
                    cmd.Parameters.AddWithValue("@GSTID", ObjGST.GSTID);
                    cmd.Parameters.AddWithValue("@USERID", ObjGST.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out GSTID))
                        throw new Exception(str);
                    else
                        ObjGST.GSTID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing GST");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjGST;
        }
        public DataTable GetPrinterType()
        {
            DataTable dtPrinterType = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_PRINTERTYPE]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtPrinterType);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Printer Type");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtPrinterType;
        }
        public PrinterSettings SavePrinterSettings(PrinterSettings ObjPrinterSettings)
        {
            int PRINTERSETTINGSID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_PRINTERSETTINGS]";
                    cmd.Parameters.AddWithValue("@PRINTERSETTINGSID", ObjPrinterSettings.PRINTERSETTINGSID);
                    cmd.Parameters.AddWithValue("@PRINTERTYPEID", ObjPrinterSettings.PRINTERTYPEID);
                    cmd.Parameters.AddWithValue("@PRINTERNAME", ObjPrinterSettings.PRINTERNAME);
                    cmd.Parameters.AddWithValue("@USERID", ObjPrinterSettings.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out PRINTERSETTINGSID))
                        throw new Exception(str);
                    else
                        ObjPrinterSettings.PRINTERSETTINGSID = objReturn;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_PRINTERSETTING"))
                    throw new Exception("Printer Already Exists!!");
                else
                    throw new Exception("Error While Saving Printer");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjPrinterSettings;
        }
        public DataTable GetPrinterSettings(object UserID)
        {
            DataTable dtPrinterSettings = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_PRINTERSETTINGS]";
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtPrinterSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Printer Settings");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtPrinterSettings;
        }
        public DataTable GetStates()
        {
            DataTable dtStates = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_STATE]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtStates);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving State List");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtStates;
        }
    }
}
