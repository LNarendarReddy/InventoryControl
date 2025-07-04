﻿using Entity;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

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
                    cmd.Parameters.AddWithValue("@CATEGORYID", CategoryID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dsItemCodes);
                    }
                    dsItemCodes.Tables[0].TableName = "ITEMS";
                    dsItemCodes.Tables[1].TableName = "ITEMCODES";
                    //dsItemCodes.Tables[2].TableName = "NONEAN";
                    //if (dsItemCodes.Tables.Count > 3)
                    //    dsItemCodes.Tables[3].TableName = "ITEMCODESFILTERED";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item codes List", ex);
            }
            finally
            {
                
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
                    cmd.Connection = SQLCon.Sqlconn();
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
            finally
            {
                
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
                    cmd.Connection = SQLCon.Sqlconn();
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
            finally
            {
                
            }
            return dt;
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
                    cmd.Parameters.AddWithValue("@BrandID", itemObj.BrandID);
                    cmd.Parameters.AddWithValue("@ManufacturerID", itemObj.ManufacturerID);
                    cmd.Parameters.AddWithValue("@UQCID", itemObj.UQCID);
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
                dsItemVisualizer.Tables[1].TableName = "ITEMPRICES";
                dsItemVisualizer.Tables[2].TableName = "ITEMSTOCKSUMMARY";
                dsItemVisualizer.Tables[3].TableName = "BRANCHPRICES";

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item code details", ex);
            }
            finally
            {
                
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
            finally
            {
                
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
                    cmd.Connection = SQLCon.Sqlconn();
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
            finally
            {
                
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
                    cmd.Connection = SQLCon.Sqlconn();
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
            finally
            {
                
            }
            return dtItemCodes;
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

        public void DeleteItemPrice(object ItemPriceID, object UserID)
        {
            SqlTransaction transaction = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
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

        public int SaveItemPrice(ItemMRP itemMRP)            
        {
            SqlTransaction sqlTransaction = null;
            int retVal =  0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    sqlTransaction = cmd.Connection.BeginTransaction();
                    cmd.Transaction = sqlTransaction;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_ITEMPRICE]";
                    cmd.Parameters.AddWithValue("@ITEMCODEID", itemMRP.ITEMCODEID);
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", itemMRP.ITEMPRICEID);
                    cmd.Parameters.AddWithValue("@MRP", itemMRP.MRP);
                    cmd.Parameters.AddWithValue("@SALEPRICE", itemMRP.SalePrice);
                    cmd.Parameters.AddWithValue("@GSTID", itemMRP.GSTID);
                    cmd.Parameters.AddWithValue("@USERID", itemMRP.UserID);
                    cmd.Parameters.AddWithValue("@Immediate", itemMRP.Immediate);
                    cmd.Parameters.AddWithValue("@GoLiveDate", itemMRP.GoLiveDateTime);
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
                    cmd.Connection = SQLCon.Sqlconn();
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
            finally
            {
                
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
                    cmd.Connection = SQLCon.Sqlconn();
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
            finally
            {
                
            }
            return dtItems;
        }

        public void SaveBranchItemPrice(BranchItemPrice branchItemPrice)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
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
            finally
            {
                
            }
        }

        public void DeleteBranchItemPrice(object ItemPriceID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
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
            finally
            {
                
            }
        }

        public void DeleteItemCode(object ItemCodeID, object UserID, bool isDeleteOrUndelete = true)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
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
            finally
            {
                
            }
        }

        public object GetGSTID(object ItemCodeID)
        {
            object nextSKUCode = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMGST]";
                    cmd.Parameters.AddWithValue("@ITEMCODEID", ItemCodeID);
                    nextSKUCode = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return nextSKUCode;
        }

        public void UnDeleteItemPrice(object ItemPriceID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_UD_ITEMPRICE]";
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", ItemPriceID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    object objReturn = cmd.ExecuteScalar();
                    if (!int.TryParse(objReturn?.ToString(), out int value))
                        throw new Exception(objReturn.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while un-deleting item code : {ex.Message}", ex);
            }
        }

        public DataTable GetStockByBranch(object ItemPriceID)
        {
            DataTable dtItems = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_STOCKBYBRANCH]";
                    cmd.Parameters.AddWithValue("@ITEMPRICEID", ItemPriceID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItems);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving stock by branch", ex);
            }
            
            return dtItems;
        }
    }
}
