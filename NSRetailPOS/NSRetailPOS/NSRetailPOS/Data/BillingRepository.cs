using NSRetailPOS.Entity;
using System;
using System.Data;
using System.Data.SqlClient;

namespace NSRetailPOS.Data
{
    public class BillingRepository
    {
        public DataTable SaveBillDetail(object billID, object ItemPriceID, object quantity, object weightInKGS, object userID, object billDetailID)
        {
            DataTable dtBillDetails = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_CU_BILLDETAIL]";
                    cmd.Parameters.AddWithValue("@BillID", billID);
                    cmd.Parameters.AddWithValue("@ItemPriceID", ItemPriceID);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@WeightInKgs", weightInKGS);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@BillDetailID", billDetailID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtBillDetails);
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
            return dtBillDetails;
        }

        public DataSet GetInitialLoad(object userID, object branchCounterID)
        {
            DataSet dsInitialLoad = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_LOAD]";
                    cmd.Parameters.AddWithValue("@USerID", userID);
                    cmd.Parameters.AddWithValue("@BranchCounterID", branchCounterID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsInitialLoad);
                    }

                    dsInitialLoad.Tables[0].TableName = "DAYSEQUENCE";
                    if (dsInitialLoad.Tables.Count > 1)
                    {
                        dsInitialLoad.Tables[1].TableName = "BILL";
                        dsInitialLoad.Tables[2].TableName = "BILLDETAILS";
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
            return dsInitialLoad;
        }

        public DataSet FinishBill(object userID, int daySequenceID, Bill billObj)
        {
            DataSet dsNextBill = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_FINISH_BILL]";
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@BillID", billObj.BillID);
                    cmd.Parameters.AddWithValue("@DaySequenceID", daySequenceID);
                    cmd.Parameters.AddWithValue("@CustomerName", billObj.CustomerName);
                    cmd.Parameters.AddWithValue("@CustomerNumber", billObj.CustomerNumber);
                    DataTable dtTemp = billObj.dtMopValues.Copy();
                    if (dtTemp.Columns.Contains("MOPNAME"))
                    {
                        dtTemp.Columns.Remove("MOPNAME");
                    }
                    cmd.Parameters.AddWithValue("@MopValues", dtTemp);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsNextBill);
                    }

                    dsNextBill.Tables[0].TableName = "BILL";
                    dsNextBill.Tables[1].TableName = "BILLDETAILS";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While finishing and getting next bill", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsNextBill;
        }

        public DataSet DraftBill(object userID, int daySequenceID, object billID)
        {
            DataSet dsNextBill = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_DRAFT_BILL]";
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@BillID", billID);
                    cmd.Parameters.AddWithValue("@DaySequenceID", daySequenceID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsNextBill);
                    }

                    dsNextBill.Tables[0].TableName = "BILL";
                    dsNextBill.Tables[1].TableName = "BILLDETAILS";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While drafting and getting next bill", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsNextBill;
        }

        public DataTable DeleteBillDetail(object billDetailID, object userID, DataTable dtSNos)
        {
            DataTable dtBillDetails = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_D_BILLDETAIL]";
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@BillDetailID", billDetailID);
                    cmd.Parameters.AddWithValue("@SNos", dtSNos);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtBillDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While deleting bill detail", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }

            return dtBillDetails;
        }

        public DataTable GetMOPs()
        {
            DataTable dtMOPs = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_MOP]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtMOPs);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While getting mode of payments", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtMOPs;
        }

        public DataTable GetDraftBills(int daySequenceID)
        {
            DataTable draftBills = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_DRAFTBILLS]";
                    cmd.Parameters.AddWithValue("@DaySequenceID", daySequenceID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(draftBills);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While getting draft bills", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return draftBills;
        }

        public DataSet GetBill(int daySequenceID, object billID)
        {
            DataSet dsBillDetails = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_BILL]";
                    cmd.Parameters.AddWithValue("@BillID", billID);
                    cmd.Parameters.AddWithValue("@DaySequenceID", daySequenceID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsBillDetails);
                    }

                    dsBillDetails.Tables[0].TableName = "BILL";
                    dsBillDetails.Tables[1].TableName = "BILLDETAILS";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While getting bill", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsBillDetails;
        }

        public DataSet GetLastBill(int daySequenceID, object billID)
        {
            DataSet dsBillDetails = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_LASTBILL]";
                    cmd.Parameters.AddWithValue("@BillID", billID);
                    cmd.Parameters.AddWithValue("@DaySequenceID", daySequenceID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsBillDetails);
                    }

                    dsBillDetails.Tables[0].TableName = "BILL";
                    dsBillDetails.Tables[1].TableName = "BILLDETAILS";
                    dsBillDetails.Tables[2].TableName = "MOPDETAILS";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While getting bill", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsBillDetails;
        }

        public DataSet GetDayClosure()
        {
            DataSet dsDayClosure = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_R_DAYCLOSURE]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsDayClosure);
                    }
                    if (dsDayClosure != null && dsDayClosure.Tables.Count > 0)
                    {
                        if (dsDayClosure.Tables[0].Rows.Count == 1)
                            throw new Exception(Convert.ToString(dsDayClosure.Tables[0].Rows[0][0]));
                        dsDayClosure.Tables[0].TableName = "DENOMINATION";
                        dsDayClosure.Tables[1].TableName = "MOP";
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("No Bills Available!"))
                    throw ex;
                else
                    throw new Exception("Error While getting Day Closure", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsDayClosure;
        }

        public void SaveDayClosure(object BranchCounterID, DataTable dtDenomination, DataTable dtMOP,object UserID,object RefundAmount)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    dtDenomination.Columns.Remove("DISPLAYVALUE");
                    dtDenomination.Columns.Remove("MULTIPLIER");
                    dtDenomination.Columns.Remove("QUANTITY");

                    dtMOP.Columns.Remove("MOPNAME");

                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_CU_DAYCLOSURE]";
                    cmd.Parameters.Add("@BRANCHCOUNTERID", BranchCounterID);
                    cmd.Parameters.Add("@dtDenomination", dtDenomination);
                    cmd.Parameters.Add("@dtMOP", dtMOP);
                    cmd.Parameters.Add("@USERID", UserID);
                    cmd.Parameters.Add("@RefundAmount", RefundAmount);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected <= 0)
                        throw new Exception("Error while saving day cllosure");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving Day Closure", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }
    }
}
