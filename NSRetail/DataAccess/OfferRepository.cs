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
    public class OfferRepository
    {
        public DataTable GetOfferType()
        {
            DataTable dtOfferType = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_OFFERTYPE]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtOfferType);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Offer Type", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtOfferType;
        }
        public DataTable GetItemGroup()
        {
            DataTable dtItemGroup = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMGROUP]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItemGroup);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item group List", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtItemGroup;
        }
        public object SaveItemGroup(object ItemGroupID, object GroupName, object IsActive, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_ITEMGROUP]";
                    cmd.Parameters.AddWithValue("@ItemGroupID", ItemGroupID);
                    cmd.Parameters.AddWithValue("@GroupName", GroupName);
                    cmd.Parameters.AddWithValue("@IsActive", IsActive);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    ItemGroupID = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving item group", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ItemGroupID;
        }
        public DataTable GetItemGroupDetail(object ItemGroupID)
        {
            DataTable dtItemGroupDetail = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_ITEMGROUPDETAIL]";
                    cmd.Parameters.AddWithValue("@ItemGroupID", ItemGroupID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtItemGroupDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Item Group Detail", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtItemGroupDetail;
        }
        public int SaveItemGroupDetail(object ItemGroupID, object ItemCodeID, object UserID)
        {
            int ItemGroupDetailID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_ITEMGROUPDETAIL]";
                    cmd.Parameters.AddWithValue("@ItemGroupID", ItemGroupID);
                    cmd.Parameters.AddWithValue("@ItemCodeID", ItemCodeID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                     object objreturn = cmd.ExecuteScalar();
                    if (!int.TryParse(objreturn.ToString(), out ItemGroupDetailID))
                        throw new Exception("Error while saivng item group detail");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving item group", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return ItemGroupDetailID;
        }
        public void DeleteItemGroupDetail(object itemGroupDetailID,object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_ITEMGROUPDETAIL]";
                    cmd.Parameters.AddWithValue("@ITEMGROUPDETAILID", itemGroupDetailID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected <= 0)
                        throw new Exception("Error while deleting item group detail");
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
        }
        public DataTable GetOffer()
        {
            DataTable dtOffer = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_OFFER]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtOffer);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Offer List", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtOffer;
        }
        public int SaveOffer(Offer offer)
        {
            int OfferID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_OFFER]";
                    cmd.Parameters.AddWithValue("@OfferID", offer.OfferID);
                    cmd.Parameters.AddWithValue("@OfferName", offer.OfferName);
                    cmd.Parameters.AddWithValue("@OfferCode", offer.OfferCode);
                    cmd.Parameters.AddWithValue("@StartDate", offer.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", offer.EndDate);
                    cmd.Parameters.AddWithValue("@OfferValue", offer.OfferValue);
                    cmd.Parameters.AddWithValue("@AppliesToID", offer.AppliesToID);
                    cmd.Parameters.AddWithValue("@OfferTypeID", offer.OfferTypeID);
                    cmd.Parameters.AddWithValue("@CategoryID", offer.CategoryID);
                    cmd.Parameters.AddWithValue("@ItemGroupID", offer.ItemGroupID);
                    cmd.Parameters.AddWithValue("@IsActive", offer.IsActive);
                    cmd.Parameters.AddWithValue("@UserID", offer.UserID);
                    cmd.Parameters.AddWithValue("@FreeItemPriceID", offer.FreeItemPriceID);
                    cmd.Parameters.AddWithValue("@NumberOfItems", offer.NumberOfItems);
                    object objreturn = cmd.ExecuteScalar();
                    if (!int.TryParse(objreturn.ToString(), out OfferID))
                        throw new Exception("Error while saivng offer");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving offer", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return OfferID;
        }
        public void DeleteOffer(object OfferID,object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_OFFER]";
                    cmd.Parameters.AddWithValue("@OfferID", OfferID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected <= 0)
                        throw new Exception("Error while deleting offer");
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
        }
        public DataTable GetOfferBranch(object OfferID)
        {
            DataTable dtOfferBranch = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_OFFERBRANCH]";
                    cmd.Parameters.AddWithValue("@OfferID", OfferID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtOfferBranch);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Offer Branch", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtOfferBranch;
        }
        public int SaveOfferBranch(object OfferID,object BranchID,object UserID)
        {
            int OfferBranchID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_OFFERBRANCH]";
                    cmd.Parameters.AddWithValue("@OfferID", OfferID);
                    cmd.Parameters.AddWithValue("@BranchID", BranchID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    object objreturn = cmd.ExecuteScalar();
                    if (!int.TryParse(objreturn.ToString(), out OfferBranchID))
                        throw new Exception("Error while saivng offer branch");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving offer branch", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return OfferBranchID;
        }
        public void DeleteOfferBranch(object OfferBranchID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_OFFERBRANCH]";
                    cmd.Parameters.AddWithValue("@OfferBranchID", OfferBranchID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected <= 0)
                        throw new Exception("Error while deleting offer branch");
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
        }
        public DataTable GetOfferItem(object OfferID)
        {
            DataTable dtOfferItem = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_OFFERITEM]";
                    cmd.Parameters.AddWithValue("@OfferID", OfferID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtOfferItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Offer Branch", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtOfferItem;
        }
        public int SaveOfferItem(object OfferID, object BranchID, object UserID)
        {
            int OfferBranchID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_OFFERITEM]";
                    cmd.Parameters.AddWithValue("@OfferID", OfferID);
                    cmd.Parameters.AddWithValue("@ItemCodeID", BranchID);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    object objreturn = cmd.ExecuteScalar();
                    if (!int.TryParse(objreturn.ToString(), out OfferBranchID))
                        throw new Exception("Error while saivng offer item");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving offer item", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return OfferBranchID;
        }
        public void DeleteOfferitem(object OfferItemMapID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_OFFERITEM]";
                    cmd.Parameters.AddWithValue("@OFFERITEMMAPID", OfferItemMapID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected <= 0)
                        throw new Exception("Error while deleting offer item");
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
        }
        public DataTable GetApliesTo()
        {
            DataTable dtAppliesTo = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_APPLIESTO]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtAppliesTo);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Applies To", ex);
            }
            finally
            {
                SQLCon.Sqlconn().Close();
            }
            return dtAppliesTo;
        }
    }
}
