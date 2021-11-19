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
    public class ItemRepository
    {
        public Item SaveItem(Item itemObj)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_ITEM]";
                    cmd.Parameters.AddWithValue("@ItemID", itemObj.ItemID);
                    cmd.Parameters.AddWithValue("@ItemName", itemObj.ItemName);
                    cmd.Parameters.AddWithValue("@ItemCode", itemObj.ItemCode);
                    cmd.Parameters.AddWithValue("@Description", itemObj.Description);
                    cmd.Parameters.AddWithValue("@HSNCode", itemObj.HSNCode);
                    cmd.Parameters.AddWithValue("@IsEANCode", itemObj.IsEANCode);
                    cmd.Parameters.AddWithValue("@EANCode", itemObj.EANCode);
                    cmd.Parameters.AddWithValue("@CostPrice", itemObj.CostPrice);
                    cmd.Parameters.AddWithValue("@SalePrice", itemObj.SalePrice);
                    cmd.Parameters.AddWithValue("@MRP", itemObj.MRP);
                    cmd.Parameters.AddWithValue("@GSTID", 4);//itemObj.GSTID);
                    cmd.Parameters.AddWithValue("@UserID", itemObj.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out int itemID))
                        throw new Exception(str);
                    else
                        itemObj.ItemID = objReturn;
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

        public DataTable GetItems()
        {
            DataTable dtItems = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMS]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItems);
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
            return dtItems;
        }

        public DataSet GetItem(object itemID)
        {
            DataSet dsItems = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEM]";
                    cmd.Parameters.AddWithValue("@ItemID", itemID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsItems);
                    }

                    dsItems.Tables[0].TableName = "ITEM";
                    dsItems.Tables[1].TableName = "ITEMBARCODE";
                    dsItems.Tables[2].TableName = "ITEMBARCODEPRICE";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item details", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dsItems;
        }
    }
}
