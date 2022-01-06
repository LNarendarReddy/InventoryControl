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
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[POS_USP_CU_CREFUND]";
                    DataView dv = dtRefund.DefaultView;
                    dv.RowFilter = "REFUNDQUANTITY > 0";
                    cmd.Parameters.AddWithValue("@dtRefund", dv.ToTable());
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
    }
}
