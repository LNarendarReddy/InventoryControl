using NSRetailPOS.Entity;
using System;
using System.Data;
using System.Data.SqlClient;

namespace NSRetailPOS.Data
{
    public class BillingRepository
    {
        public int SaveBillDetail(DataRow drBillDetail, object userID)
        {
            int billDetailID;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_CU_BILLDETAIL]";
                    cmd.Parameters.AddWithValue("@BillDetailID", drBillDetail["BILLDETAILID"]);
                    cmd.Parameters.AddWithValue("@BillID", drBillDetail["BILLID"]);
                    cmd.Parameters.AddWithValue("@ItemPriceID", drBillDetail["ITEMPRICEID"]);
                    cmd.Parameters.AddWithValue("@SNo", drBillDetail["SNO"]);
                    cmd.Parameters.AddWithValue("@Quantity", drBillDetail["QUANTITY"]);
                    cmd.Parameters.AddWithValue("@WeightInKgs", drBillDetail["WEIGHTINKGS"]);
                    cmd.Parameters.AddWithValue("@BilledAmount", drBillDetail["BILLEDAMOUNT"]);
                    cmd.Parameters.AddWithValue("@GSTID", drBillDetail["GSTID"]);
                    cmd.Parameters.AddWithValue("@CGST", drBillDetail["CGST"]);
                    cmd.Parameters.AddWithValue("@SGST", drBillDetail["SGST"]);
                    cmd.Parameters.AddWithValue("@IGST", drBillDetail["IGST"]);
                    cmd.Parameters.AddWithValue("@Cess", drBillDetail["CESS"]);
                    cmd.Parameters.AddWithValue("@GSTVALUE", drBillDetail["GSTVALUE"]);
                    cmd.Parameters.AddWithValue("@DISCOUNT", drBillDetail["DISCOUNT"]);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    object objReturn = cmd.ExecuteScalar();

                    if (!int.TryParse(objReturn.ToString(), out billDetailID))
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
            return billDetailID;
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

        public void DeleteBillDetail(object billDetailID, object userID, DataTable dtSNos)
        {
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
                    cmd.ExecuteNonQuery();
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

        public void SaveDayClosure(object BranchCounterID, DataTable dtDenomination, DataTable dtMOP,object UserID)
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
