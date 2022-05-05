using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace NSRetailPOS.Data
{
    public class RefundRepository
    {
        public DataSet GetBillByNumber(object BillNumber)
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
                    cmd.Parameters.AddWithValue("@REASONID", drDetail["REASONID"]);
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

        public DataTable GETRFR()
        {
            DataTable dtRFR = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_REASONFORREFUND]";
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
    }
}
