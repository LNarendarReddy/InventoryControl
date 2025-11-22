using DevExpress.CodeParser;
using DevExpress.Office.Drawing;
using DevExpress.XtraRichEdit.Import.OpenDocument;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace NSRetailPOS.Data
{
    public class RefundRepository
    {
        public DataSet GetBillByNumber(object BillNumber, bool isAdminRole)
        {
            DataSet dsBillDetails = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_BILLBYNUMBER]";
                    cmd.Parameters.AddWithValue("@BILLNUMBER", BillNumber);
                    cmd.Parameters.AddWithValue("@IsAdminRole", isAdminRole);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsBillDetails);
                    }
                    if (dsBillDetails != null && dsBillDetails.Tables[0].Rows.Count > 0
                        && int.TryParse(Convert.ToString(dsBillDetails.Tables[0].Rows[0][0]), out int ivalue))
                    {
                        dsBillDetails.Tables[0].TableName = "BILL";
                        dsBillDetails.Tables[1].TableName = "BILLDETAILS";
                    }
                    else
                        throw new Exception("Bill Does Not Exists!");
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Bill Does Not Exists!"))
                    throw ex;
                else
                    throw new Exception("Error While Retrieving Bill Details", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsBillDetails;
        }

        public void InsertCRefund(DataTable dtRefund,object UserID, object billID, object customerName, object customerNumber)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    DataTable dtCloned = dtRefund.Copy();
                    foreach (DataColumn dc in dtRefund.Columns)
                    {
                        if (dc.ColumnName == "BILLDETAILID" ||
                             dc.ColumnName == "REFUNDQUANTITY" ||
                             dc.ColumnName == "REFUNDWEIGHTINKGS" ||
                             dc.ColumnName == "REFUNDAMOUNT")
                            continue;
                        dtCloned.Columns.Remove(dc.ColumnName);
                    }

                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_CU_CREFUND]";
                    cmd.Parameters.AddWithValue("@dtRefund", dtCloned);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@BillID", billID);
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@CustomerPhone", customerNumber);

                    int ivalue = cmd.ExecuteNonQuery();
                    if(ivalue<=0)
                        throw new Exception("Error while saving Refund Details!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving Refund Details", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }

        public void InsertCRefundWOBill(DataTable dtRefund, object UserID,object CustomerName,object CustomerMobile)
        {
            List<string> columnsToRemove = new List<string> { "ITEMCODE", "ITEMNAME", "MRP", "SALEPRICE", "ISOPENITEM" };
            try
            {
                DataTable dtTemp = dtRefund.Copy();
                columnsToRemove.Where(dtTemp.Columns.Contains).ToList().ForEach(x => dtTemp.Columns.Remove(x));
                
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_C_CR_WO_BILL]";
                    cmd.Parameters.AddWithValue("@CR_WO_Bills", dtTemp);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    cmd.Parameters.AddWithValue("@CustomerName", CustomerName);
                    cmd.Parameters.AddWithValue("@CustomerMobile", CustomerMobile);
                    int ivalue = cmd.ExecuteNonQuery();
                    if (ivalue <= 0)
                        throw new Exception("Error while saving Refund Details!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving Refund Details", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }

        public DataSet GetInitialLoad(object userID)
        {
            DataSet dSInitialLoad = new();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_BREFUND_1]";
                    cmd.Parameters.AddWithValue("@USERID", userID);
                    cmd.Parameters.AddWithValue("@BRANCHID", Utility.branchInfo.BranchID);
                    using SqlDataAdapter da = new(cmd);
                    da.Fill(dSInitialLoad);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving initial load details", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dSInitialLoad;
        }

        public DataTable SaveBRefund(object userId, object categoryID)
        {
            SqlTransaction transaction = null;
            DataTable dt = new();
            try
            {
                using (SqlCommand cmd = new())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    transaction = SQLCon.SqlWHconn().BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_CU_BREFUND_1]";
                    cmd.Parameters.AddWithValue("@USERID", userId);
                    cmd.Parameters.AddWithValue("@BRANCHID", Utility.branchInfo.BranchID);
                    cmd.Parameters.AddWithValue("@CATEGORYID", categoryID);
                    using (SqlDataAdapter da = new(cmd))
                    {
                        da.Fill(dt);
                    }
                    transaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw ex;
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dt;
        }

        public int SaveBRefundDetail(DataRow drDetail)
        {
            SqlTransaction transaction = null;
            int BRefundDetailID = 0;
            try
            {
                using (SqlCommand cmd = new())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    transaction = SQLCon.SqlWHconn().BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_CU_BREFUNDDETAIL_1]";
                    cmd.Parameters.AddWithValue("@BRDID", drDetail["BRDID"]);
                    cmd.Parameters.AddWithValue("@BRID", drDetail["BRID"]);
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", drDetail["ITEMPRICEID"]);
                    cmd.Parameters.AddWithValue("@QUANTITY", drDetail["QUANTITY"]);
                    cmd.Parameters.AddWithValue("@WEIGHTINKGS", drDetail["WEIGHTINKGS"]);
                    cmd.Parameters.AddWithValue("@SNO", drDetail["SNO"]);
                    cmd.Parameters.AddWithValue("@TRAYNUMBER", drDetail["TRAYNUMBER"]);
                    cmd.Parameters.AddWithValue("@REASONID", drDetail["REASONID"]);
                    cmd.Parameters.AddWithValue("@REFUNDDESCRIPTION", drDetail["REFUNDDESCRIPTION"]);
                    object objReturn = cmd.ExecuteScalar();

                    if (!int.TryParse(objReturn.ToString(), out BRefundDetailID))
                        throw new Exception(objReturn.ToString());
                    transaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw ex;
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return BRefundDetailID;
        }

        public void DeleteBRefundDetail(object BRDID, object userID, DataTable dtSnos)
        {
            SqlTransaction transaction = null;
            try
            {
                using (SqlCommand cmd = new())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    transaction = SQLCon.SqlWHconn().BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_D_BREFUNDDETAIL_1]";
                    cmd.Parameters.AddWithValue("@BRDID", BRDID);
                    cmd.Parameters.AddWithValue("@USERID", userID);
                    cmd.Parameters.AddWithValue("@SNos", dtSnos);
                    cmd.ExecuteNonQuery();
                    transaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw new Exception("Error While deleting bill refund detail", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }

        public string FinishBRefund(object BRID)
        {
            SqlTransaction transaction = null;
            string BRefundNumber = string.Empty;
            try
            {
                using (SqlCommand cmd = new())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    transaction = SQLCon.SqlWHconn().BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_FINISHBREFUND_1]";
                    cmd.Parameters.AddWithValue("@BRID", BRID);
                    cmd.Parameters.AddWithValue("@FROMBRANCHID", Utility.branchInfo.BranchID);
                    cmd.Parameters.AddWithValue("@TOBRANCHID", Utility.branchInfo.WarehouseID);
                    object objReturn = cmd.ExecuteScalar();
                    BRefundNumber = Convert.ToString(objReturn);
                    transaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw ex;
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return BRefundNumber;
        }

        public DataTable GETRFR()
        {
            DataTable dtRFR = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_REASONFORREFUND]";
                    cmd.Parameters.AddWithValue("@BRANCHID", Utility.branchInfo.BranchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtRFR);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Reason For Refund", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtRFR;
        }

        public void DiscardBRefund(object BRID, object userId)
        {
            SqlTransaction transaction = null;
            try
            {
                using (SqlCommand cmd = new())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    transaction = SQLCon.SqlWHconn().BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_D_BREFUND_1]";
                    cmd.Parameters.AddWithValue("@BRID", BRID);
                    cmd.Parameters.AddWithValue("@USERID", userId);
                    int rowaffected = cmd.ExecuteNonQuery();
                    if (rowaffected <= 0)
                        throw new Exception("Error while discarding branch return sheet");
                    transaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw ex;
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }

        public DataTable GetCategory()
        {
            DataTable dtCategory = new DataTable();
            SqlConnection conn = SQLCon.SqlWHconn();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_CATEGORY]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtCategory);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Category List", ex);
            }
            finally
            {
                conn.Close();
            }
            return dtCategory;
        }

        public DataTable GetMRPList(object ITEMCODEID)
        {
            DataTable dtItemCodes = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_ITEMMRPLIST]";
                    cmd.Parameters.AddWithValue("@ITEMCODEID", ITEMCODEID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItemCodes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving MRP List", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtItemCodes;
        }

        public DataTable GetItemCodes(object categoryID = null)
        {
            DataTable dtItemCodes = new DataTable();
            SqlConnection conn = SQLCon.SqlWHconn();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_ITEMCODES]";
                    cmd.Parameters.AddWithValue("@CATEGORYID", categoryID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItemCodes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item codes List", ex);
            }
            finally
            {
                conn.Close();
            }
            return dtItemCodes;
        }
    }
}
