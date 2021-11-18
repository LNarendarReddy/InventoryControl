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
                    cmd.Parameters.AddWithValue("@HSCNO", itemObj.HSCNO);
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
                if (ex.Message.Contains("UC_BName"))
                    throw new Exception("Branch Already Exists!!");
                else
                    throw new Exception("Error While Saving Branch");
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
                    cmd.CommandText = "[USP_R_ITEM]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItems);
                    }
                }
            }
            catch (Exception ex)
            {                
                throw new Exception("Error While Retrieving Branch List");
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtItems;
        }
    }
}
