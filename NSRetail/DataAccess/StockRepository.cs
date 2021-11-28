using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class StockRepository
    {
        public StockDispatch SaveDispatch(StockDispatch ObjStockDispatch)
        {
            int StockDispatchID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_STOCKDISPATCH]";
                    cmd.Parameters.Add("@STOCKDISPATCHID", ObjStockDispatch.STOCKDISPATCHID);
                    cmd.Parameters.Add("@FROMBRANCHID", ObjStockDispatch.FROMBRANCHID);
                    cmd.Parameters.Add("@TOBRANCHID", ObjStockDispatch.TOBRANCHID);
                    cmd.Parameters.Add("@CATEGORYID", ObjStockDispatch.CATEGORYID);
                    cmd.Parameters.Add("@USERID", ObjStockDispatch.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out StockDispatchID))
                        throw new Exception(str);
                    else
                        ObjStockDispatch.STOCKDISPATCHID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Saving Dispatch");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjStockDispatch;
        }
        public StockDispatchDetail SaveDispatchDetail(StockDispatchDetail ObjStockDispatchDetail)
        {
            int StockDispatchDetailID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_STOCKDISPATCHDETAIL]";
                    cmd.Parameters.Add("@STOCKDISPATCHDETAILID", ObjStockDispatchDetail.STOCKDISPATCHDETAILID);
                    cmd.Parameters.Add("@STOCKDISPATCHID", ObjStockDispatchDetail.STOCKDISPATCHID);
                    cmd.Parameters.Add("@ITEMPRICEID", ObjStockDispatchDetail.ITEMPRICEID);
                    cmd.Parameters.Add("@TRAYNUMBER", ObjStockDispatchDetail.TRAYNUMBER);
                    cmd.Parameters.Add("@DISPATCHQUANTITY", ObjStockDispatchDetail.DISPATCHQUANTITY);
                    cmd.Parameters.Add("@WEIGHTINKGS", ObjStockDispatchDetail.WEIGHTINKGS);
                    cmd.Parameters.Add("@USERID", ObjStockDispatchDetail.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out StockDispatchDetailID))
                        throw new Exception(str);
                    else
                        ObjStockDispatchDetail.STOCKDISPATCHDETAILID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Saving Dispatch Detail");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ObjStockDispatchDetail;
        }
        public StockDispatch GetDispatchDraft(StockDispatch objStockDispatch)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_DISPATCHDRAFT]";
                    cmd.Parameters.Add("@USERID", objStockDispatch.UserID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string str = Convert.ToString(ds.Tables[0].Rows[0][0]);
                        int iValue = 0;
                        if (!int.TryParse(str, out iValue))
                            objStockDispatch.STOCKDISPATCHID = 0;
                        else
                        {
                            objStockDispatch.STOCKDISPATCHID = iValue;
                            objStockDispatch.FROMBRANCHID = ds.Tables[0].Rows[0]["FROMBRANCHID"];
                            objStockDispatch.TOBRANCHID = ds.Tables[0].Rows[0]["TOBRANCHID"];
                            objStockDispatch.dtDispatch = ds.Tables[1].Copy();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Reading Dispatch");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return objStockDispatch;
        }
        public void DeleteDispatchDetail(object StockDispatchDetailID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_STOCKDISPATCHDETAILS]";
                    cmd.Parameters.Add("@STOCKDISPATCHDETAILID", StockDispatchDetailID);
                    object objReturn = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleting Dispatch Detail");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
        }
    }
}
