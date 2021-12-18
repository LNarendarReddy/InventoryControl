using System;
using System.Data;
using System.Data.SqlClient;

namespace NSRetailPOS.Data
{
    public class BillingRepository
    {
        //public Item SaveItemCode(Item itemObj)
        //{
        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.Connection = SQLCon.Sqlconn();
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.CommandText = "[USP_CU_ITEMCODE]";
        //            cmd.Parameters.AddWithValue("@ItemCodeID", itemObj.ItemCodeID);
        //            cmd.Parameters.AddWithValue("@ItemID", itemObj.ItemID);
        //            cmd.Parameters.AddWithValue("@ItemName", itemObj.ItemName);
        //            cmd.Parameters.AddWithValue("@ItemCode", itemObj.ItemCode);
        //            cmd.Parameters.AddWithValue("@Description", itemObj.Description);
        //            cmd.Parameters.AddWithValue("@CategoryID", itemObj.CategoryID);
        //            cmd.Parameters.AddWithValue("@SubCategoryID", itemObj.SubCategoryID);
        //            cmd.Parameters.AddWithValue("@HSNCode", itemObj.HSNCode);
        //            cmd.Parameters.AddWithValue("@IsEAN", itemObj.IsEAN);
        //            cmd.Parameters.AddWithValue("@SKUCode", itemObj.SKUCode);
        //            cmd.Parameters.AddWithValue("@CostPriceWT", itemObj.CostPriceWT);
        //            cmd.Parameters.AddWithValue("@CostPriceWOT", itemObj.CostPriceWOT);
        //            cmd.Parameters.AddWithValue("@SalePrice", itemObj.SalePrice);
        //            cmd.Parameters.AddWithValue("@MRP", itemObj.MRP);
        //            cmd.Parameters.AddWithValue("@GSTID", itemObj.GSTID);
        //            cmd.Parameters.AddWithValue("@UserID", itemObj.UserID);
        //            cmd.Parameters.AddWithValue("@IsOpenItem", itemObj.IsOpenItem);
        //            cmd.Parameters.AddWithValue("@ParentItemID", itemObj.ParentItemID);
        //            cmd.Parameters.AddWithValue("@UOMID", itemObj.UOMID);
        //            cmd.Parameters.AddWithValue("@FreeItemCodeID", itemObj.FreeItemCodeID);
        //            object objReturn = cmd.ExecuteScalar();

        //            string str = Convert.ToString(objReturn.ToString().Split(',')[0]);
        //            if (!int.TryParse(str, out int itemCodeID))
        //                throw new Exception(str);

        //            itemObj.ItemCodeID = itemCodeID;
        //            itemObj.ItemID = objReturn.ToString().Split(',')[1];
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        SQLCon.Sqlconn().Close();
        //    }
        //    return itemObj;
        //}

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
    }
}
