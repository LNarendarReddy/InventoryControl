﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailPOS.Data
{
    public class POSRepository
    {
        public DataTable GetBranchList()
        {
            DataTable dtBranch = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_BRANCH]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtBranch);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Branch List", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtBranch;
        }
        public DataTable GetCounterList()
        {
            DataTable dtBranch = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_BRANCHCOUNTER]";
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
        public void SavePOSConfiguration(object BranchID,object BranchCounterID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_C_POSDETAILS]";
                    cmd.Parameters.Add("@BRANCHID", BranchID);
                    cmd.Parameters.Add("@BRANCHCOUNTERID", BranchCounterID);
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
        public DataSet GetUserInfo(object BranchID,object BranchCounterID,object Username,object PasswordString)
        {
            DataSet dSUserInfo = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_USERCREDENTIALS]";
                    cmd.Parameters.Add("@BRANCHID", BranchID);
                    cmd.Parameters.Add("@BRANCHCOUNTERID", BranchCounterID);
                    cmd.Parameters.Add("@USERNAME", Username);
                    cmd.Parameters.Add("@PASSWORDSTRING", PasswordString);
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
                    cmd.Parameters.Add("@USERID", UserID);
                    cmd.Parameters.Add("@PASSWORDSTRING", PasswordString);
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