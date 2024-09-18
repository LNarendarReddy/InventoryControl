using Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class UserRepository
    {
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
                    throw new Exception($"Error While Saving User : {ex.Message}");
            }
            finally
            {

            }
            return ObjUser;
        }

        public DataTable GetUser()
        {
            return new ReportRepository().GetReportData("USP_R_USER");
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
                throw new Exception("Error While Deleteing User");
            }
            finally
            {

            }
            return ObjUser;
        }

        public DataTable GetRoles()
        {
            return new ReportRepository().GetReportData("USP_R_ROLE");
        }

        public DataSet GetRoleAccessList(object roleID, object userID)
        {
            return new ReportRepository().GetReportDataset("USP_R_HASACCESS"
                , new Dictionary<string, object> { { "RoleID", roleID }, { "UserID", userID } });
        }

        public DataSet GetUserCredentials(string UserName, string Password, string AppVersion)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "USERNAME", UserName }
                , { "PASSWORD", Password }
                , { "APPVERSION", AppVersion }
            };
            DataSet dSUser = new ReportRepository().GetReportDataset("USP_R_USERLOGIN", parameters);

            if (dSUser != null && dSUser.Tables[0].Rows.Count > 0)
            {
                int Ivalue = 0;
                string str = Convert.ToString(dSUser.Tables[0].Rows[0][0]);
                if (!int.TryParse(str, out Ivalue))
                    throw new Exception(str);
            }
            else
                throw new Exception("Error in login");

            return dSUser;
        }

        public DataTable ChangePassword(User ObjUser)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "USERID", ObjUser.USERID }
                , { "PASSWORDSTRING", ObjUser.PASSWORDSTRING }
            };

            DataTable dtUser = new ReportRepository().GetReportData("USP_U_PASSWORD", parameters);
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                int Ivalue = 0;
                string str = Convert.ToString(dtUser.Rows[0][0]);
                if (!int.TryParse(str, out Ivalue))
                    throw new Exception(str);
            }

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
                    if (cmd.ExecuteNonQuery() <= 0)
                        throw new Exception("Error while resetting the password");

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public void SaveAccess(object roleID, object userID
            , object addedAccessList, object removedAccessList
            , object applyToUsers, object changedByUserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_C_HASACCESS]";
                    cmd.Parameters.AddWithValue("@USERID", userID);
                    cmd.Parameters.AddWithValue("@ROLEID", roleID);
                    cmd.Parameters.AddWithValue("@AddedAccessList", addedAccessList);
                    cmd.Parameters.AddWithValue("@RemovedAccessList", removedAccessList);
                    cmd.Parameters.AddWithValue("@ApplyToUsers", applyToUsers);
                    cmd.Parameters.AddWithValue("@ChangedByUserID", changedByUserID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error While Saving access date : {ex.Message}");
            }
        }
    }
}
