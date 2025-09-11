using NSRetailPOS.Entity;
using System;
using System.Data;
using System.Data.SqlClient;

namespace NSRetailPOS.Data
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
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMCODES_STOCKENTRY_BRANCH]";
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
            return dsItemCodes;
        }

        public DataSet GetItemCode(object itemCodeID, object CategoryID)
        {
            DataSet dsItemCode = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMCODE]";
                    cmd.Parameters.AddWithValue("@ItemCodeID", itemCodeID);
                    cmd.Parameters.AddWithValue("@CATEGORYID", CategoryID);
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
            return dsItemCode;
        }

        public DataTable GetItemCodeByCategory(object CategoryID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMCODEBYCATEGORY]";
                    cmd.Parameters.AddWithValue("@CATEGORYID", CategoryID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item code list", ex);
            }
            return dt;
        }

        public Item SaveItemCode(Item itemObj)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_ITEMCODE]";
                    cmd.Parameters.AddWithValue("@ItemCodeID", itemObj.ItemCodeID);
                    cmd.Parameters.AddWithValue("@ItemID", itemObj.ItemID);
                    cmd.Parameters.AddWithValue("@ItemName", itemObj.ItemName);
                    cmd.Parameters.AddWithValue("@ItemCode", itemObj.ItemCode);
                    cmd.Parameters.AddWithValue("@Description", itemObj.Description);
                    cmd.Parameters.AddWithValue("@CategoryID", itemObj.CategoryID);
                    cmd.Parameters.AddWithValue("@SubCategoryID", itemObj.SubCategoryID);
                    cmd.Parameters.AddWithValue("@HSNCode", itemObj.HSNCode);
                    cmd.Parameters.AddWithValue("@IsEAN", itemObj.IsEAN);
                    cmd.Parameters.AddWithValue("@SKUCode", itemObj.SKUCode);
                    cmd.Parameters.AddWithValue("@CostPriceWT", itemObj.CostPriceWT);
                    cmd.Parameters.AddWithValue("@CostPriceWOT", itemObj.CostPriceWOT);
                    cmd.Parameters.AddWithValue("@SalePrice", itemObj.SalePrice);
                    cmd.Parameters.AddWithValue("@MRP", itemObj.MRP);
                    cmd.Parameters.AddWithValue("@GSTID", itemObj.GSTID);
                    cmd.Parameters.AddWithValue("@UserID", itemObj.UserID);
                    cmd.Parameters.AddWithValue("@IsOpenItem", itemObj.IsOpenItem);
                    cmd.Parameters.AddWithValue("@ParentItemID", itemObj.ParentItemID);
                    cmd.Parameters.AddWithValue("@UOMID", itemObj.UOMID);
                    cmd.Parameters.AddWithValue("@FreeItemCodeID", itemObj.FreeItemCodeID);
                    cmd.Parameters.AddWithValue("@ClassificationID", itemObj.ClassificationID);
                    cmd.Parameters.AddWithValue("@SubClassificationID", itemObj.SubClassificationID);
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
            return itemObj;
        }

        public DataSet GetItemVisualizer(int itemID)
        {
            DataSet dsItemVisualizer = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMVISUALIZER]";
                    cmd.Parameters.AddWithValue("@ItemID", itemID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsItemVisualizer);
                    }
                }

                dsItemVisualizer.Tables[0].TableName = "ITEM";
                dsItemVisualizer.Tables[1].TableName = "ITEMPRICES";
                dsItemVisualizer.Tables[2].TableName = "ITEMSTOCKSUMMARY";
                dsItemVisualizer.Tables[3].TableName = "BRANCHPRICES";

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item code details", ex);
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
                    cmd.Connection = SQLCon.SqlWHconn();
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
            return dtItemCodes;
        }

        public DataTable GetMRPList(object ITEMCODEID)
        {
            DataTable dtItemCodes = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMMRPLIST]";
                    cmd.Parameters.AddWithValue("@ITEMCODEID", ITEMCODEID);
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
            return dtItemCodes;
        }

        public DataTable GetItemList()
        {
            DataTable dtItem = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
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

            }
            return nextSKUCode;
        }

        public DataTable GetParentItems(object CategoryID)
        {
            DataTable dtParentItems = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_PARENTITEMS]";
                    cmd.Parameters.AddWithValue("@CATEGORYID", CategoryID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtParentItems);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Parent Item List", ex);
            }
            return dtParentItems;
        }

        public DataTable GetCostPriceList(object ITEMCODEID)
        {
            DataTable dtItemCodes = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_COSTPRICELIST]";
                    cmd.Parameters.AddWithValue("@ITEMCODEID", ITEMCODEID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItemCodes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Cost Price List", ex);
            }
            return dtItemCodes;
        }

        public void DeleteItemPrice(object ItemPriceID, object UserID)
        {
            SqlTransaction transaction = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    transaction = cmd.Connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_ITEMPRICE]";
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", ItemPriceID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    object returnValue = cmd.ExecuteScalar();
                    if (!int.TryParse(returnValue?.ToString(), out int rowsafftected))
                        throw new Exception(returnValue.ToString());
                    transaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw new Exception($"Error while deleting itemprice : {ex.Message}", ex);
            }
        }

        public int SaveItemPrice(object itemCodeID,object ItemPriceID, object MRP, object SalePrice, object GSTID, object UserID)
        {
            SqlTransaction sqlTransaction = null;
            int retVal =  0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    sqlTransaction = cmd.Connection.BeginTransaction();
                    cmd.Transaction = sqlTransaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_ITEMPRICE]";
                    cmd.Parameters.AddWithValue("@ITEMCODEID", itemCodeID);
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", ItemPriceID);
                    cmd.Parameters.AddWithValue("@MRP", MRP);
                    cmd.Parameters.AddWithValue("@SALEPRICE", SalePrice);
                    cmd.Parameters.AddWithValue("@GSTID", GSTID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    object objReturn = cmd.ExecuteScalar();
                    if (!int.TryParse(Convert.ToString(objReturn), out retVal))
                        throw new Exception(Convert.ToString(objReturn));
                    sqlTransaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                throw new Exception($"Error while updating itemprice: {ex.Message}", ex);
            }
            return retVal;
        }

        public DataTable ExportItemList(object ExportType)
        {
            DataTable dtItems = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_EXPORTITEMS]";
                    cmd.Parameters.AddWithValue("@EXPORTTYPE", ExportType);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItems);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Items", ex);
            }
            return dtItems;
        }

        public DataTable GetItemPriceList()
        {
            DataTable dtItems = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMPRICELIST]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItems);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item Price List", ex);
            }
            return dtItems;
        }

        public BranchItemPrice SaveBranchItemPrice(BranchItemPrice branchItemPrice)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_BRANCHITEMPRICE]";
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", branchItemPrice.ITEMPRICEID);
                    cmd.Parameters.AddWithValue("@PARENTITEMPRICEID", branchItemPrice.PARENTITEMPRICEID);
                    cmd.Parameters.AddWithValue("@SALEPRICE", branchItemPrice.SALEPRICE);
                    cmd.Parameters.AddWithValue("@BRANCHID", branchItemPrice.BRANCHID);
                    cmd.Parameters.AddWithValue("@USERID", branchItemPrice.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    if (int.TryParse(Convert.ToString(objReturn), out int ItemPriceID))
                        branchItemPrice.ITEMPRICEID = ItemPriceID;
                    else
                        throw new Exception(Convert.ToString(objReturn));

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return branchItemPrice;
        }

        public void DeleteBranchItemPrice(object ItemPriceID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_BRANCHITEMPRICE]";
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", ItemPriceID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    int rowsafftected = cmd.ExecuteNonQuery();
                    if (rowsafftected <= 0)
                        throw new Exception("Error while deleting branch itemprice");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting itemprice", ex);
            }
        }

        public void DeleteItemCode(object ItemCodeID, object UserID, bool isDeleteOrUndelete = true)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_ITEMCODE]";
                    cmd.Parameters.AddWithValue("@ITEMCODEID", ItemCodeID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    cmd.Parameters.AddWithValue("@isDelete", isDeleteOrUndelete);
                    int rowsafftected = cmd.ExecuteNonQuery();
                    if (rowsafftected <= 0)
                        throw new Exception("Error while deleting item code");

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting item code", ex);
            }
        }

        public DataTable GetOffers(object ItemPriceID)
        {
            DataTable dtOffers = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_GETOFFERS]";
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", ItemPriceID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtOffers);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Offers", ex);
            }
            finally
            {

            }
            return dtOffers;
        }

        public DataTable GetMRPList_Branch(object ITEMCODEID, object BranchID)
        {
            DataTable dtItemCodes = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlWHconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMPRICE_BRANCH]";
                    cmd.Parameters.AddWithValue("@ITEMCODEID", ITEMCODEID);
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
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
            return dtItemCodes;
        }
    }
}
