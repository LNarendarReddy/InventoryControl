using Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net;
using System.Runtime.InteropServices;

namespace DataAccess
{
    public class CloudRepository
    {
        public DataTable GetSyncStatus()
        {
            DataTable dtSyncStatus = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_SHOWSYNC]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtSyncStatus);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While sync status from cloud database", ex);
            }
            finally
            {
                SQLCon.SqlCloudConn().Close();
            }
            return dtSyncStatus;
        }

        public void DeleteSyncStatus(object LocationID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_SYNCSTATUS]";
                    cmd.Parameters.AddWithValue("@LocationID", LocationID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While deleteing sync status from cloud database", ex);
            }
            finally
            {
                SQLCon.SqlCloudConn().Close();
            }
        }

        public StockCounting GetCounting(StockCounting stockCounting)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[CLOUD_USP_R_STOCKCOUNTING]";
                    cmd.Parameters.AddWithValue("@USERID", stockCounting.UserID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                    if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (int.TryParse(Convert.ToString(ds.Tables[0].Rows[0][0]), out int ivalue))
                        {
                            stockCounting.STOCKCOUNTINGID = ds.Tables[0].Rows[0][0];
                            stockCounting.BRANCHID = ds.Tables[0].Rows[0][1];
                        }
                        else
                        {
                            stockCounting.STOCKCOUNTINGID = 0;
                        }
                    }
                    if (ds.Tables.Count > 1)
                        stockCounting.dtStockCountning = ds.Tables[1];
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving stock counting");
            }
            finally
            {
                SQLCon.SqlCloudConn().Close();
            }
            return stockCounting;
        }

        public StockCounting SaveStockCounting(StockCounting stockCounting)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[CLOUD_USP_CU_STOCKCOUNTING]";
                    cmd.Parameters.AddWithValue("@STOCKCOUNTINGID", stockCounting.STOCKCOUNTINGID);
                    cmd.Parameters.AddWithValue("@BranchID", stockCounting.BRANCHID);
                    cmd.Parameters.AddWithValue("@USERID", stockCounting.UserID);
                    cmd.Parameters.AddWithValue("@CREATEDDATE", DateTime.Now);
                    object obj = cmd.ExecuteScalar();
                    if (!int.TryParse(Convert.ToString(obj), out int ivalue))
                        throw new Exception(Convert.ToString(obj));
                    else
                        stockCounting.STOCKCOUNTINGID = ivalue;

                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Stock counting not enabled"))
                    throw ex;
                else
                    throw new Exception("Error while saving stock counting");
            }
            finally
            {
                SQLCon.SqlCloudConn().Close();
            }
            return stockCounting;
        }

        public object SaveStockCountingDetail(DataRow drNew)
        {
            object stockcountingdetailid = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[CLOUD_USP_CU_STOCKCOUNTINGDETAIL]";
                    cmd.Parameters.AddWithValue("@STOCKCOUNTINGDETAILID", drNew["STOCKCOUNTINGDETAILID"]);
                    cmd.Parameters.AddWithValue("@STOCKCOUNTINGID", drNew["STOCKCOUNTINGID"]);
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", drNew["ITEMPRICEID"]);
                    cmd.Parameters.AddWithValue("@QUANTITY", drNew["QUANTITY"]);
                    cmd.Parameters.AddWithValue("@WEIGHTINKGS", drNew["WEIGHTINKGS"]);
                    cmd.Parameters.AddWithValue("@CREATEDDATE", DateTime.Now);
                    object obj = cmd.ExecuteScalar();
                    if (!int.TryParse(Convert.ToString(obj), out int ivalue))
                        throw new Exception(Convert.ToString(obj));
                    else
                        stockcountingdetailid = ivalue;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Counting sheet already submitted") || ex.Message.Contains("Stock counting not enabled"))
                    throw ex;
                else
                    throw new Exception("Error while saving stock counting detail");
            }
            finally
            {
                SQLCon.SqlCloudConn().Close();
            }
            return stockcountingdetailid;
        }

        public void DeleteStockCounting(object StockCountingDetailID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[CLOUD_USP_D_STOCKCOUNTINGDETAIL1]";
                    cmd.Parameters.AddWithValue("@STOCKCOUNTINGDETAILID", StockCountingDetailID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting stock counting detail");
            }
            finally
            {
                SQLCon.SqlCloudConn().Close();
            }
        }

        public void DeleteStockCounting(object BranchID, object ItemCodeID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[CLOUD_USP_D_STOCKCOUNTINGDETAIL]";
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                    cmd.Parameters.AddWithValue("@ItemCodeID", ItemCodeID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting stock counting detail");
            }
            finally
            {
                SQLCon.SqlCloudConn().Close();
            }
        }

        public void UpdateStockCounting(StockCounting stockCounting)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[CLOUD_USP_U_STOCKCOUNTING]";
                    cmd.Parameters.AddWithValue("@STOCKCOUNTINGID", stockCounting.STOCKCOUNTINGID);
                    cmd.Parameters.AddWithValue("@USERID", stockCounting.UserID);
                    cmd.Parameters.AddWithValue("@UPDATEDDATE", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while submitting stock counting");
            }
            finally
            {
                SQLCon.SqlCloudConn().Close();
            }
        }

        public void DiscardCounting(StockCounting stockCounting)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[CLOUD_USP_D_STOCKCOUNTING]";
                    cmd.Parameters.AddWithValue("@STOCKCOUNTINGID", stockCounting.STOCKCOUNTINGID);
                    cmd.Parameters.AddWithValue("@USERID", stockCounting.UserID);
                    int rowsaffetced = cmd.ExecuteNonQuery();
                    if(rowsaffetced == 0)
                        throw new Exception("Error while discarding counting");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while discarding counting");
            }
            finally
            {
                SQLCon.SqlCloudConn().Close();
            }
        }

        public void InitiateCounting(object branchID, bool InitiateCounting)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_INITIATESTOCKCOUNTING]";
                    cmd.Parameters.AddWithValue("@BRANCHID", branchID);
                    cmd.Parameters.AddWithValue("@InitiateCounting", InitiateCounting);
                    object obj = cmd.ExecuteNonQuery();
                    if (!int.TryParse(Convert.ToString(obj), out int ivalue))
                        throw new Exception("Error while Initiating counting");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while discarding counting");
            }
            finally
            {
                SQLCon.SqlCloudConn().Close();
            }
        }
    }
}
