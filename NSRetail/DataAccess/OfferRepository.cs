using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OfferRepository
    {
        public DataTable GetOfferType(object BaseType = null)
        {
            DataTable dtOfferType = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_OFFERTYPE]";
                    cmd.Parameters.AddWithValue("@BASETYPEID", BaseType);
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
                
            }
        }
        public DataTable GetOffer(bool IsDeal = false)
        {
            DataTable dtOffer = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_OFFER]";
                    cmd.Parameters.AddWithValue("@BASETYPEID", IsDeal ? 2 : 1);
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
                    cmd.Parameters.AddWithValue("@UserID", offer.UserID);
                    cmd.Parameters.AddWithValue("@FreeItemPriceID", offer.FreeItemPriceID);
                    cmd.Parameters.AddWithValue("@NumberOfItems", offer.NumberOfItems);
                    cmd.Parameters.AddWithValue("@OFFERTHRESHOLDPRICE", offer.OfferThresholdPrice);
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
                
            }
        }
        public DataTable GetOfferBranch(object OfferID, bool IsBaseOffer = false)
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
                    cmd.Parameters.AddWithValue("@IsBaseOffer", IsBaseOffer);
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
                
            }
            return dtOfferBranch;
        }
        public int SaveOfferBranch(object OfferID,object BranchID,object UserID, bool IsBaseOffer = false)
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
                    cmd.Parameters.AddWithValue("@IsBaseoffer", IsBaseOffer);
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
                
            }
            return OfferBranchID;
        }
        public void DeleteOfferBranch(object OfferBranchID, object UserID, bool isBaseOffer = false)
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
                    cmd.Parameters.AddWithValue("@IsBaseOffer", isBaseOffer);
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
                
            }
            return dtOfferItem;
        }
        public int SaveOfferItem(object OfferID, object BranchID, object UserID, object NUMBEROFPIECES)
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
                    cmd.Parameters.AddWithValue("@NUMBEROFPIECES", NUMBEROFPIECES);
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
                
            }
            return OfferBranchID;
        }
        public void ImportOfferItems(object OfferID, DataTable dtItems, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_IMP_OFFERITEMS]";
                    cmd.Parameters.AddWithValue("@OFFERID", OfferID);
                    cmd.Parameters.AddWithValue("@dtitems", dtItems);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    int ivalue = cmd.ExecuteNonQuery();
                    if (ivalue <= 0)
                        throw new Exception("Error while importing Items");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving offer", ex);
            }
            finally
            {
                
            }
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
                
            }
            return dtAppliesTo;
        }
        public BaseOffer SaveBaseOffer(BaseOffer baseOffer)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_BASEOFFER]";
                    cmd.Parameters.AddWithValue("@BASEOFFERID", baseOffer.BASEOFFERID);
                    cmd.Parameters.AddWithValue("@OFFERCODE", baseOffer.OFFERCODE);
                    cmd.Parameters.AddWithValue("@OFFERNAME", baseOffer.OFFERNAME);
                    cmd.Parameters.AddWithValue("@CATEGORYID", baseOffer.CATEGORYID);
                    cmd.Parameters.AddWithValue("@STARTDATE", baseOffer.STARTDATE);
                    cmd.Parameters.AddWithValue("@ENDDATE", baseOffer.ENDDATE);
                    cmd.Parameters.AddWithValue("@UserID", baseOffer.UserID);
                    object obj = cmd.ExecuteScalar();
                    if (int.TryParse(Convert.ToString(obj), out int ivalue))
                        baseOffer.BASEOFFERID = ivalue;
                    else
                        throw new Exception(Convert.ToString(obj));
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_BO_OFFERCODE"))
                    throw new Exception("Offer code already exists", ex);
                else
                    throw new Exception("Error While saving base offer", ex);
            }
            finally
            {
                
            }
            return baseOffer;
        }
        public DataTable GetBaseOffer()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_BASEOFFER]";
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving base offer", ex);
            }
            finally
            {
                
            }
            return dt;
        }
        public void DeleteBaseOffer(object BaseOfferID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_BASEOFFER]";
                    cmd.Parameters.AddWithValue("@BASEOFFERID", BaseOfferID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected <= 0)
                        throw new Exception("Error while deleting base offer");
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
        public Offer SaveOfferFromBaseOffer(Offer offer)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_OFFERFROMBASE]";
                    cmd.Parameters.AddWithValue("@OFFERID", offer.OfferID);
                    cmd.Parameters.AddWithValue("@OFFERVALUE", offer.OfferValue);
                    cmd.Parameters.AddWithValue("@OFFERTYPEID", offer.OfferTypeID);
                    cmd.Parameters.AddWithValue("@ITEMCODEID", offer.ItemCodeID);
                    cmd.Parameters.AddWithValue("@BASEOFFERID", offer.BaseOfferID);
                    cmd.Parameters.AddWithValue("@UserID", offer.UserID);
                    cmd.Parameters.AddWithValue("@NUMBEROFPIECES", offer.OfferThreshold);

                    offer.OfferID = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving offer", ex);
            }
            finally
            {
                
            }
            return offer;
        }
        public DataTable GetOfferByBaseOffer(object BaseOfferID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_OFFERBYBASEOFFER]";
                    cmd.Parameters.AddWithValue("@BASEOFFERID", BaseOfferID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving offer list", ex);
            }
            finally
            {
                
            }
            return dt;
        }
        public void DeleteOfferFromBase(object OfferID, object ItemCodeID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_OFFERFROMBASE]";
                    cmd.Parameters.AddWithValue("@OFFERID", OfferID);
                    cmd.Parameters.AddWithValue("@ITEMCODEID", ItemCodeID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected <= 0)
                        throw new Exception("Error while deleting base offer");
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
        public void ImportOffer(object BaseOfferID,object CategoryID,  DataTable dtOffers, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand() { CommandTimeout = 1800 })
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_IMP_OFFER]";
                    cmd.Parameters.AddWithValue("@BASEOFFERID", BaseOfferID);
                    cmd.Parameters.AddWithValue("@CATEGORYID", CategoryID);
                    cmd.Parameters.AddWithValue("@dtOffer", dtOffers);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    object objreturn = cmd.ExecuteScalar();
                    if (!int.TryParse(Convert.ToString(objreturn), out int ivalue))
                    {
                        throw new Exception(Convert.ToString(objreturn));
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("recognized") || ex.Message.Contains("category"))
                    throw ex;
                else
                    throw new Exception("Error While importing offer list", ex);
            }
            finally
            {
                
            }
        }
        public DataTable GetOfferExclusion(object OfferID)
        {
            DataTable dtOfferItem = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_R_OFFEREXCLUSION]";
                    cmd.Parameters.AddWithValue("@OFFERID", OfferID);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtOfferItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Retrieving Offer Exclusion List", ex);
            }
            return dtOfferItem;
        }
        public int SaveOfferExclusion(object OfferID, object EXCLUSIONID, object UserID, bool IsCategory = false)
        {
            int OFFEREXCLUSIONID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_OFFEREXCLUSION]";
                    cmd.Parameters.AddWithValue("@OFFERID", OfferID);
                    cmd.Parameters.AddWithValue("@EXCLUSIONID", EXCLUSIONID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    cmd.Parameters.AddWithValue("@ISCATEGORY", IsCategory);
                    object objreturn = cmd.ExecuteScalar();
                    if (!int.TryParse(objreturn.ToString(), out OFFEREXCLUSIONID))
                        throw new Exception("Error while saivng offer exclusion");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return OFFEREXCLUSIONID;
        }
        public void ImportOfferExclusion(object OfferID, DataTable dtItems, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_IMP_OFFERITEMS]";
                    cmd.Parameters.AddWithValue("@OFFERID", OfferID);
                    cmd.Parameters.AddWithValue("@dtitems", dtItems);
                    cmd.Parameters.AddWithValue("@UserID", UserID);
                    int ivalue = cmd.ExecuteNonQuery();
                    if (ivalue <= 0)
                        throw new Exception("Error while importing Items");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While saving offer", ex);
            }
        }
        public void DeleteOfferExclusion(object OFFEREXCLUSIONID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_OFFEREXCLUSION]";
                    cmd.Parameters.AddWithValue("@OFFEREXCLUSIONID", OFFEREXCLUSIONID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected <= 0)
                        throw new Exception("Error while deleting offer exclusion");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
