using System;
using System.Data;
using System.Data.SqlClient;

namespace NSRetailPOS.Data
{
    public class BillingRepository
    {
        public int SaveBillDetail(DataRow drBillDetail, int userID)
        {
            int billDetailID;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_BILLDETAIL]";
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

        public DataSet GetInitialLoad(int userID, int branchCounterID)
        {
            DataSet dsInitialLoad = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_LOAD]";
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

        public DataSet FinishBill(object billID, int userID, int daySequenceID)
        {
            DataSet dsNextBill = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_FINISH_BILL]";
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
                throw new Exception("Error While finishing and getting next bill", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsNextBill;
        }

        public void DeleteBillDetail(object billDetailID, int userID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_BILLDETAIL]";
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@BillDetailID", billDetailID);
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
    }
}
