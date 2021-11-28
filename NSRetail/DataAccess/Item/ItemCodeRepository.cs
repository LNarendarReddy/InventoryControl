using Entity;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ItemCodeRepository
    {
        public DataSet GetItemCodes(object CategoryID)
        {
            DataSet dsItemCodes = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMCODES]";
                    cmd.Parameters.Add("@CATEGORYID", CategoryID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsItemCodes);
                    }

                    dsItemCodes.Tables[0].TableName = "ITEMS";
                    dsItemCodes.Tables[1].TableName = "ITEMCODES";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item codes List", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsItemCodes;
        }

        public DataSet GetItemCode(object itemCodeID)
        {
            DataSet dsItemCode = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMCODE]";
                    cmd.Parameters.AddWithValue("@ItemCodeID", itemCodeID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsItemCode);
                    }

                    dsItemCode.Tables[0].TableName = "ITEMCODEDETAIL";
                    dsItemCode.Tables[1].TableName = "ITEMCODEPRICES";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item code details", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsItemCode;
        }

        public Item SaveItemCode(Item itemObj)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_ITEMCODE]";
                    cmd.Parameters.AddWithValue("@ItemCodeID", itemObj.ItemCodeID);
                    cmd.Parameters.AddWithValue("@ItemID", itemObj.ItemID);
                    cmd.Parameters.AddWithValue("@ItemName", itemObj.ItemName);
                    cmd.Parameters.AddWithValue("@ItemCode", itemObj.ItemCode);
                    cmd.Parameters.AddWithValue("@Description", itemObj.Description);
                    cmd.Parameters.AddWithValue("@CategoryID", itemObj.CategoryID);
                    cmd.Parameters.AddWithValue("@HSNCode", itemObj.HSNCode);
                    cmd.Parameters.AddWithValue("@IsEAN", itemObj.IsEAN);
                    cmd.Parameters.AddWithValue("@SKUCode", itemObj.SKUCode);
                    cmd.Parameters.AddWithValue("@CostPrice", itemObj.CostPrice);
                    cmd.Parameters.AddWithValue("@SalePrice", itemObj.SalePrice);
                    cmd.Parameters.AddWithValue("@MRP", itemObj.MRP);
                    cmd.Parameters.AddWithValue("@GSTID", itemObj.GSTID);
                    cmd.Parameters.AddWithValue("@UserID", itemObj.UserID);
                    object objReturn = cmd.ExecuteScalar();

                    string str = Convert.ToString(objReturn.ToString().Split(',')[0]);
                    if (!int.TryParse(str, out int itemCodeID))
                        throw new Exception(str);

                    itemObj.ItemCodeID = itemCodeID;
                    itemObj.ItemID = objReturn.ToString().Split(',')[1];
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
            return itemObj;
        }

        public DataSet GetItemVisualizer(int itemID)
        {
            DataSet dsItemVisualizer = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMVISUALIZER]";
                    cmd.Parameters.AddWithValue("@ItemID", itemID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsItemVisualizer);
                    }
                }

                dsItemVisualizer.Tables[0].TableName = "ITEM";
                dsItemVisualizer.Tables[1].TableName = "ITEMCODES";
                dsItemVisualizer.Tables[2].TableName = "ITEMPRICES";
                dsItemVisualizer.Tables[3].TableName = "ITEMSTOCKSUMMARY";

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item code details", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsItemVisualizer;
        }

        public DataTable GetEANList()
        {
            DataTable dtItemCodes = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMCODELIST]";
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
                SQLCon.Sqlconn().Close();
            }
            return dtItemCodes;
        }

        public DataTable GetMRPList(object ITEMCODEID)
        {
            DataTable dtItemCodes = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMMRPLIST]";
                    cmd.Parameters.Add("@ITEMCODEID", ITEMCODEID);
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

        public DataTable GetItemList()
        {
            DataTable dtItem = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEM]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item List", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtItem;
        }

        public string GetNextSKUCode()
        {
            string nextSKUCode = string.Empty;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_NEXTSKUCODE]";
                    nextSKUCode = Convert.ToString(cmd.ExecuteScalar());
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
            return nextSKUCode;
        }
    }
}
