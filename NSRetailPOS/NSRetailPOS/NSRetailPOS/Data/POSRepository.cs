using DevExpress.Utils.Text.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailPOS.Data
{
    public class POSRepository
    {
        public DataTable GetBranchList(string Appversion, string DBVersion)
        {
            DataTable dtBranch = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_BRANCH]";
                    cmd.Parameters.Add("@AppVersion", Appversion);
                    cmd.Parameters.Add("@DBVersion", DBVersion);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtBranch);
                    }
                    if(dtBranch != null && dtBranch.Rows.Count > 0)
                    {
                        if (!int.TryParse(Convert.ToString(dtBranch.Rows[0][0]), out int ivalue))
                            throw new Exception(Convert.ToString(dtBranch.Rows[0][0]));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtBranch;
        }
        public DataTable GetCounterList(object BranchID)
        {
            DataTable dtBranch = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_BRANCHCOUNTER]";
                    cmd.Parameters.Add("@BranchID", BranchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtBranch);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Branch Counter List", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtBranch;
        }
        public DataTable GetPOSConfiguration()
        {
            DataTable dtPOSConfiguration = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_POSDETAILS]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtPOSConfiguration);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving POS Configuration", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtPOSConfiguration;
        }
        public void SavePOSConfiguration(object BranchID,object BranchCounterID, object DayClosureID, object BranchRefundID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_C_POSDETAILS]";
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    cmd.Parameters.AddWithValue("@BRANCHCOUNTERID", BranchCounterID);
                    cmd.Parameters.AddWithValue("@DayClosureID", DayClosureID);
                    cmd.Parameters.AddWithValue("@BranchRefundID", BranchRefundID);
                    int IValue = cmd.ExecuteNonQuery();
                    if (IValue == 0)
                        throw new Exception("Error While Saving POS Configuration");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }
        public DataSet GetUserInfo(object BranchID,object BranchCounterID,object Username,object PasswordString, string HDDSNo)
        {
            DataSet dSUserInfo = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_USERCREDENTIALS]";
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    cmd.Parameters.AddWithValue("@BRANCHCOUNTERID", BranchCounterID);
                    cmd.Parameters.AddWithValue("@USERNAME", Username);
                    cmd.Parameters.AddWithValue("@PASSWORDSTRING", PasswordString);
                    cmd.Parameters.AddWithValue("@HDDSNO", HDDSNo);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dSUserInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving User Info", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dSUserInfo;
        }
        public void ChangePassword(object UserID, object PasswordString)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_U_PASSWORD]";
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    cmd.Parameters.AddWithValue("@PASSWORDSTRING", PasswordString);
                    int IValue = cmd.ExecuteNonQuery();
                    if (IValue == 0)
                        throw new Exception("Error While Changing the Password!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { SQLCon.Sqlconn().Close(); }
        }
    }
}
