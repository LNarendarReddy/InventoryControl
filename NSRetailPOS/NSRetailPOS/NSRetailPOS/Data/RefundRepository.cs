using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailPOS.Data
{
    public class RefundRepository
    {
        public DataTable GetBillByNumber(object BillNumber)
        {
            DataTable dtBillDetails = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_BILLBYNUMBER]";
                    cmd.Parameters.AddWithValue("@BILLNUMBER", BillNumber);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtBillDetails);
                    }
                    if (dtBillDetails != null && dtBillDetails.Rows.Count > 0
                        && int.TryParse(Convert.ToString(dtBillDetails.Rows[0][0]), out int ivalue))
                    {
                        dtBillDetails.TableName = "BILLDETAILS";
                    }
                    else
                        throw new Exception("Bill Does Not Exists!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Bill Details", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtBillDetails;
        }

        public void InsertCRefund(DataTable dtRefund,object UserID)
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

        public void InsertCRefundWOBill(DataTable dtRefund, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_CU_CREFUND]";
                    cmd.Parameters.AddWithValue("@dtRefund", dtRefund);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
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

        public DataSet GetInitialLoad(object userID,object BranchID)
        {
            DataSet dSInitialLoad = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_BREFUND]";
                    cmd.Parameters.AddWithValue("@USERID", userID);
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dSInitialLoad);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving initial load details", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dSInitialLoad;
        }

        public int SaveBRefundDetail(DataRow drDetail)
        {
            int BRefundDetailID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_CU_BREFUNDDETAIL]";
                    cmd.Parameters.AddWithValue("@BREFUNDDETAILID", drDetail["BREFUNDDETAILID"]);
                    cmd.Parameters.AddWithValue("@BREFUNDID", drDetail["BREFUNDID"]);
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", drDetail["ITEMPRICEID"]);
                    cmd.Parameters.AddWithValue("@QUANTITY", drDetail["QUANTITY"]);
                    cmd.Parameters.AddWithValue("@WEIGHTINKGS", drDetail["WEIGHTINKGS"]);
                    cmd.Parameters.AddWithValue("@SNO", drDetail["SNO"]);
                    cmd.Parameters.AddWithValue("@TRAYNUMBER", drDetail["TRAYNUMBER"]);
                    object objReturn = cmd.ExecuteScalar();

                    if (!int.TryParse(objReturn.ToString(), out BRefundDetailID))
                        throw new Exception(objReturn.ToString());
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
            return BRefundDetailID;
        }

        public void DeleteBRefundDetail(object BRefundDetailID, object userID, DataTable dtSnos)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_D_BREFUNDDETAIL]";
                    cmd.Parameters.AddWithValue("@BREFUNDDETAILID", BRefundDetailID);
                    cmd.Parameters.AddWithValue("@USERID", userID);
                    cmd.Parameters.AddWithValue("@SNos", dtSnos);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While deleting bill refund detail", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }

        public void FinishBRefund(object BRefundID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_FINISHBREFUND]";
                    cmd.Parameters.AddWithValue("@BREFUNDID", BRefundID);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected == 0)
                        throw new Exception("Error while saving Branch Refund");
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
    }
}
