﻿using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class MasterRepository
    {
        public Branch SaveBranch(Branch ObjBranch)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_BRANCH]";
                    cmd.Parameters.AddWithValue("@BRANCHID", ObjBranch.BRANCHID);
                    cmd.Parameters.AddWithValue("@BRANCHNAME", ObjBranch.BRANCHNAME);
                    cmd.Parameters.AddWithValue("@BRANCHCODE", ObjBranch.BRANCHCODE);
                    cmd.Parameters.AddWithValue("@ADDRESS", ObjBranch.ADDRESS);
                    cmd.Parameters.AddWithValue("@STATEID", ObjBranch.STATEID);
                    cmd.Parameters.AddWithValue("@PHONENO", ObjBranch.PHONENO);
                    cmd.Parameters.AddWithValue("@LANDLINE", ObjBranch.LANDLINE);
                    cmd.Parameters.AddWithValue("@EMAILID", ObjBranch.EMAILID);
                    cmd.Parameters.AddWithValue("@USERID", ObjBranch.UserID);
                    cmd.Parameters.AddWithValue("@ISWAREHOUSE", ObjBranch.ISWAREHOUSE);
                    cmd.Parameters.AddWithValue("@EnableDraftBills", ObjBranch.ENABLEDRAFTBILLS);
                    cmd.Parameters.AddWithValue("@SUPERVISORID", ObjBranch.SUPERVISERID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    int BRanchID;
                    if (!int.TryParse(str, out BRanchID))
                        throw new Exception(str);
                    else
                        ObjBranch.BRANCHID = objReturn;
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
                
            }
            return ObjBranch;
        }
        public DataTable GetBranch(bool UsedInReport = false, bool IncludeDeleted = false)
        {
            DataTable dtBranch = new ReportRepository().GetReportData("USP_R_BRANCH", new Dictionary<string, object>() { { "IncludeDeleted", IncludeDeleted } });

            if (UsedInReport)
            {
                DataRow dr = dtBranch.NewRow();
                dr["BRANCHID"] = 0;
                dr["BRANCHNAME"] = "ALL";
                dtBranch.Rows.InsertAt(dr, 0);
            }

            return dtBranch;
        }
        public Branch DeleteBranch(Branch ObjBranch)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_BRANCH]";
                    cmd.Parameters.AddWithValue("@BRANCHID", ObjBranch.BRANCHID);
                    cmd.Parameters.AddWithValue("@USERID", ObjBranch.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    int BRanchID;
                    if (!int.TryParse(str, out BRanchID))
                        throw new Exception(str);
                    else
                        ObjBranch.BRANCHID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing Branch", ex);
            }
            finally
            {
                
            }
            return ObjBranch;
        }
        public Category SaveCategory(Category ObjCategory)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_CATEGORY]";
                    cmd.Parameters.AddWithValue("@CATEGORYID", ObjCategory.CATEGORYID);
                    cmd.Parameters.AddWithValue("@CATEGORYNAME", ObjCategory.CATEGORYNAME);
                    cmd.Parameters.AddWithValue("@ALLOWOPENITEMS", ObjCategory.AllowOpenItems);
                    cmd.Parameters.AddWithValue("@USERID", ObjCategory.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    int CategoryID;
                    if (!int.TryParse(str, out CategoryID))
                        throw new Exception(str);
                    else
                        ObjCategory.CATEGORYID = objReturn;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_CATEGORYNAME"))
                    throw new Exception("Category Already Exists!!");
                else
                    throw new Exception("Error While Saving Category");
            }
            finally
            {
                
            }
            return ObjCategory;
        }
        public DataTable GetCategory(bool ignoreAllCategory = false)
        {
            DataTable dt = new ReportRepository().GetReportData("USP_R_CATEGORY");
            if(ignoreAllCategory)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "CATEGORYID <> 13";
                return dv.ToTable();
            }
            return dt;
        }
        public DataTable GetAvailableLocations()
        {
            return new ReportRepository().GetReportData("USP_R_LOCATIONLIST");
        }

        public Category DeleteCategory(Category ObjCategory)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_CATEGORY]";
                    cmd.Parameters.AddWithValue("@CATEGORYID", ObjCategory.CATEGORYID);
                    cmd.Parameters.AddWithValue("@USERID", ObjCategory.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    int CategoryID;
                    if (!int.TryParse(str, out CategoryID))
                        throw new Exception(str);
                    else
                        ObjCategory.CATEGORYID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing Category", ex);
            }
            finally
            {
                
            }
            return ObjCategory;
        }

        public SubCategory SaveSubCategory(SubCategory ObjSubCategory)
        {
            int SubCategoryID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_SUBCATEGORY]";
                    cmd.Parameters.AddWithValue("@SUBCATEGORYID", ObjSubCategory.SUBCATEGORYID);
                    cmd.Parameters.AddWithValue("@SUBCATEGORYNAME", ObjSubCategory.SUBCATEGORYNAME);
                    cmd.Parameters.AddWithValue("@CATEGORYID", ObjSubCategory.CATEGORYID);
                    cmd.Parameters.AddWithValue("@USERID", ObjSubCategory.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out SubCategoryID))
                        throw new Exception(str);
                    else
                        ObjSubCategory.SUBCATEGORYID = objReturn;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_COUNTERNAME"))
                    throw new Exception("Sub Category Already Exists!!");
                else
                    throw new Exception("Error While Saving Sub Category");
            }
            finally
            {
                
            }
            return ObjSubCategory;
        }
        public DataTable GetSubCategory(object CategoryID = null)
        {
            return new ReportRepository().GetReportData("USP_R_SUBCATEGORY", new Dictionary<string, object> { { "CATEGORYID", CategoryID } });
        }

        public SubCategory DeleteSubCategory(SubCategory ObjSubCategory)
        {
            int SubCategoryID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_SUBCATEGORY]";
                    cmd.Parameters.AddWithValue("@SUBCATEGORYID", ObjSubCategory.SUBCATEGORYID);
                    cmd.Parameters.AddWithValue("@USERID", ObjSubCategory.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out SubCategoryID))
                        throw new Exception(str);
                    else
                        ObjSubCategory.SUBCATEGORYID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing Sub Category");
            }
            finally
            {
                
            }
            return ObjSubCategory;
        }
        public Dealer SaveDealer(Dealer ObjDealer)
        {
            int DealerID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_DEALER]";
                    cmd.Parameters.AddWithValue("@DEALERID", ObjDealer.DEALERID);
                    cmd.Parameters.AddWithValue("@DEALERNAME", ObjDealer.DEALERNAME);
                    cmd.Parameters.AddWithValue("@ADDRESS", ObjDealer.ADDRESS);
                    cmd.Parameters.AddWithValue("@STATEID", ObjDealer.STATEID);
                    cmd.Parameters.AddWithValue("@PHONENO", ObjDealer.PHONENO);
                    cmd.Parameters.AddWithValue("@GSTIN", ObjDealer.GSTIN);
                    cmd.Parameters.AddWithValue("@PANNUMBER", ObjDealer.PANNUMBER);
                    cmd.Parameters.AddWithValue("@EMAILID", ObjDealer.EMAILID);
                    cmd.Parameters.AddWithValue("@USERID", ObjDealer.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out DealerID))
                        throw new Exception(str);
                    else
                        ObjDealer.DEALERID = objReturn;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_DEALERNAME"))
                    throw new Exception("Dealer Already Exists!!");
                else
                    throw new Exception("Error While Saving Dealer");
            }
            finally
            {
                
            }
            return ObjDealer;
        }
        public DataTable GetDealer(bool UsedInReport = false)
        {
            DataTable dtDealer = new ReportRepository().GetReportData("USP_R_DEALER");
            if (UsedInReport)
            {
                DataRow dr = dtDealer.NewRow();
                dr["DEALERID"] = 0;
                dr["DEALERNAME"] = "ALL";
                dtDealer.Rows.InsertAt(dr, 0);
            }

            return dtDealer;
        }
        public Dealer DeleteDealer (Dealer ObjDealer)
        {
            int DealerID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_DEALER]";
                    cmd.Parameters.AddWithValue("@DEALERID", ObjDealer.DEALERID);
                    cmd.Parameters.AddWithValue("@USERID", ObjDealer.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out DealerID))
                        throw new Exception(str);
                    else
                        ObjDealer.DEALERID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing Dealer");
            }
            finally
            {
                
            }
            return ObjDealer;
        }
        public Counter SaveCounter(Counter ObjCounter)
        {
            int CounterID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_BRANCHCOUNTER]";
                    cmd.Parameters.AddWithValue("@COUNTERID", ObjCounter.COUNTERID);
                    cmd.Parameters.AddWithValue("@COUNTERNAME", ObjCounter.COUNTERNAME);
                    cmd.Parameters.AddWithValue("@BRANCHID", ObjCounter.BRANCHID);
                    cmd.Parameters.AddWithValue("@USERID", ObjCounter.UserID);
                    cmd.Parameters.AddWithValue("@ISMOBILECOUNTER", ObjCounter.ISMOBILECOUNTER);
                    cmd.Parameters.AddWithValue("@STOREID", ObjCounter.StoreID);
                    cmd.Parameters.AddWithValue("@ClientID", ObjCounter.ClientID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out CounterID))
                        throw new Exception(str);
                    else
                        ObjCounter.COUNTERID = objReturn;
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_COUNTERNAME"))
                    throw new Exception("Counter Already Exists!!");
                else
                    throw new Exception("Error While Saving Counter");
            }
            finally
            {
                
            }
            return ObjCounter;
        }
        public DataTable GetCounter()
        {
            return new ReportRepository().GetReportData("USP_R_BRANCHCOUNTER");
        }
        public Counter DeleteCounter(Counter ObjCounter)
        {
            int CounterID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_BRANCHCOUNTER]";
                    cmd.Parameters.AddWithValue("@COUNTERID", ObjCounter.COUNTERID);
                    cmd.Parameters.AddWithValue("@USERID", ObjCounter.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out CounterID))
                        throw new Exception(str);
                    else
                        ObjCounter.COUNTERID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing Counter");
            }
            finally
            {
                
            }
            return ObjCounter;
        }
        public MOP SaveMOP(MOP ObjMOP)
        {
            int MOPID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_MOP]";
                    cmd.Parameters.AddWithValue("@MOPID", ObjMOP.MOPID);
                    cmd.Parameters.AddWithValue("@MOPNAME", ObjMOP.MOPNAME);
                    cmd.Parameters.AddWithValue("@USERID", ObjMOP.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out MOPID))
                        throw new Exception(str);
                    else
                        ObjMOP.MOPID = objReturn;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_MOPNAME"))
                    throw new Exception("MOP Already Exists!!");
                else
                    throw new Exception("Error While Saving MOP");
            }
            finally
            {
                
            }
            return ObjMOP;
        }
        public DataTable GetMOP()
        {
            return new ReportRepository().GetReportData("USP_R_MOP");
        }
        public MOP DeleteMOP(MOP ObjMOP)
        {
            int MOPID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_MOP]";
                    cmd.Parameters.AddWithValue("@MOPID", ObjMOP.MOPID);
                    cmd.Parameters.AddWithValue("@USERID", ObjMOP.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out MOPID))
                        throw new Exception(str);
                    else
                        ObjMOP.MOPID = objReturn;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing MOP");
            }
            finally
            {
                
            }
            return ObjMOP;
        }
        public UOM SaveUOM(UOM ObjUOM)
        {
            int UOMID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_UOM]";
                    cmd.Parameters.AddWithValue("@UOMID", ObjUOM.UOMID);
                    cmd.Parameters.AddWithValue("@DISPLAYVALUE", ObjUOM.DISPLAYVALUE);
                    cmd.Parameters.AddWithValue("@BASEUOMID", ObjUOM.BASEUOMID);
                    cmd.Parameters.AddWithValue("@MULTIPLIER", ObjUOM.MULTIPLIER);
                    cmd.Parameters.AddWithValue("@USERID", ObjUOM.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out UOMID))
                        throw new Exception(str);
                    else
                        ObjUOM.UOMID = objReturn;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_UOMNAME"))
                    throw new Exception("UOM Already Exists!!");
                else
                    throw new Exception("Error While Saving UOM");
            }
            finally
            {
                
            }
            return ObjUOM;
        }
        public DataTable GetUOM()
        {
            return new ReportRepository().GetReportData("USP_R_UOM");
        }
        public UOM DeleteUOM(UOM ObjUOM)
        {
            int UOMID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_UOM]";
                    cmd.Parameters.AddWithValue("@UOMID", ObjUOM.UOMID);
                    cmd.Parameters.AddWithValue("@USERID", ObjUOM.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out UOMID))
                        throw new Exception(str);
                    else
                        ObjUOM.UOMID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing UOM");
            }
            finally
            {
                
            }
            return ObjUOM;
        }

        public GST SaveGST(GST ObjGST)
        {
            int GSTID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_GST]";
                    cmd.Parameters.AddWithValue("@GSTID", ObjGST.GSTID);
                    cmd.Parameters.AddWithValue("@GSTCODE", ObjGST.GSTCODE);
                    cmd.Parameters.AddWithValue("@CGST", ObjGST.CGST);
                    cmd.Parameters.AddWithValue("@SGST", ObjGST.SGST);
                    cmd.Parameters.AddWithValue("@IGST", ObjGST.IGST);
                    cmd.Parameters.AddWithValue("@CESS", ObjGST.CESS);
                    cmd.Parameters.AddWithValue("@USERID", ObjGST.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out GSTID))
                        throw new Exception(str);
                    else
                        ObjGST.GSTID = objReturn;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_GSTCODE"))
                    throw new Exception("GST Already Exists!!");
                else
                    throw new Exception("Error While Saving GST");
            }
            finally
            {
                
            }
            return ObjGST;
        }

        public DataTable GetGST()
        {
            return new ReportRepository().GetReportData("USP_R_GST");
        }

        public GST DeleteGST(GST ObjGST)
        {
            int GSTID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_GST]";
                    cmd.Parameters.AddWithValue("@GSTID", ObjGST.GSTID);
                    cmd.Parameters.AddWithValue("@USERID", ObjGST.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out GSTID))
                        throw new Exception(str);
                    else
                        ObjGST.GSTID = objReturn;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing GST");
            }
            finally
            {
                
            }
            return ObjGST;
        }

        public DataTable GetPrinterType()
        {
            return new ReportRepository().GetReportData("USP_R_PRINTERTYPE");
        }

        public PrinterSettings SavePrinterSettings(PrinterSettings ObjPrinterSettings)
        {
            int PRINTERSETTINGSID = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_PRINTERSETTINGS]";
                    cmd.Parameters.AddWithValue("@PRINTERSETTINGSID", ObjPrinterSettings.PRINTERSETTINGSID);
                    cmd.Parameters.AddWithValue("@PRINTERTYPEID", ObjPrinterSettings.PRINTERTYPEID);
                    cmd.Parameters.AddWithValue("@PRINTERNAME", ObjPrinterSettings.PRINTERNAME);
                    cmd.Parameters.AddWithValue("@USERID", ObjPrinterSettings.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out PRINTERSETTINGSID))
                        throw new Exception(str);
                    else
                        ObjPrinterSettings.PRINTERSETTINGSID = objReturn;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_PRINTERSETTING"))
                    throw new Exception("Printer Already Exists!!");
                else
                    throw new Exception("Error While Saving Printer");
            }
            finally
            {
                
            }
            return ObjPrinterSettings;
        }

        public DataTable GetPrinterSettings(object UserID)
        {
            return new ReportRepository().GetReportData("USP_R_PRINTERSETTINGS", new Dictionary<string, object> { { "USERID", UserID } });
        }

        public DataTable GetStates()
        {
            return new ReportRepository().GetReportData("USP_R_STATE");
        }

        public void ClearProcedureCache()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DBCC FREEPROCCACHE";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                 throw new Exception("Error While Clearing cache", ex);
            }
            finally
            {
                
            }
        }

        public void SaveItemClassification(ItemClassification itemClassification)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_ITEMCLASSIFICATION]";
                    cmd.Parameters.AddWithValue("@ItemClassificationID", itemClassification.ItemClassificationID);
                    cmd.Parameters.AddWithValue("@ClassificationName", itemClassification.ItemClassificationName);
                    cmd.Parameters.AddWithValue("@CategoryID", itemClassification.CategoryID);
                    cmd.Parameters.AddWithValue("@UserID", itemClassification.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out int ItemClassificationID))
                        throw new Exception(str);
                    itemClassification.ItemClassificationID = ItemClassificationID;
                    itemClassification.IsSave = true;
                }
            }
            finally
            {
                
            }
        }

        public void SaveItemSubClassification(ItemSubClassification itemSubClassification)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_ITEMSUBCLASSIFICATION]";
                    cmd.Parameters.AddWithValue("@ItemSubClassificationID", itemSubClassification.ItemSubClassificationID);
                    cmd.Parameters.AddWithValue("@SubClassificationName", itemSubClassification.ItemSubClassificationName);
                    cmd.Parameters.AddWithValue("@ItemClassificationID", itemSubClassification.ItemClassificationID);
                    cmd.Parameters.AddWithValue("@UserID", itemSubClassification.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out int ItemSubClassificationID))
                        throw new Exception(str);
                    itemSubClassification.ItemSubClassificationID = ItemSubClassificationID;
                    itemSubClassification.IsSave = true;
                }
            }
            finally
            {
                
            }
        }

        public void ClearCounterHDDSNo(Counter ObjCounter)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.SqlCloudConn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CLR_HDDSNO]";
                    cmd.Parameters.AddWithValue("@COUNTERID", ObjCounter.COUNTERID);
                    cmd.Parameters.AddWithValue("@USERID", ObjCounter.UserID);
                    cmd.Parameters.AddWithValue("@Reason", ObjCounter.Description);
                    cmd.ExecuteNonQuery();                    
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

        public ItemClassification DeleteItemClassification(ItemClassification itemClassification)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_ITEMCLASSIFICATION]";
                    cmd.Parameters.AddWithValue("@ItemClassificationID", itemClassification.ItemClassificationID);
                    cmd.Parameters.AddWithValue("@UserID", itemClassification.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out int itemClassificationID))
                        throw new Exception(str);
                    else
                        itemClassification.IsSave = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing Item Classification " + ex.Message);
            }
            return itemClassification;
        }

        public ItemSubClassification DeleteItemSubClassification(ItemSubClassification itemSubClassification)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_ITEMSUBCLASSIFICATION]";
                    cmd.Parameters.AddWithValue("@ItemSubClassificationID", itemSubClassification.ItemSubClassificationID);
                    cmd.Parameters.AddWithValue("@UserID", itemSubClassification.UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out int itemSubClassificationID))
                        throw new Exception(str);
                    else
                        itemSubClassification.IsSave = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error While Deleteing Item Sub Classification " + ex.Message);
            }
            finally
            {
                
            }
            return itemSubClassification;
        }

        public void InitiateStockCounting(object BranchID, object UserId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_INITIATESTOCKCOUNTING]";
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    cmd.Parameters.AddWithValue("@USERID", UserId);
                    object obj = cmd.ExecuteScalar();
                    if (!int.TryParse(Convert.ToString(obj), out int ivalue))
                        throw new Exception(Convert.ToString(obj));
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("There are few sheets"))
                    throw ex;
                else
                    throw new Exception("Error While performing action");
            }
        }

        public void UpdateStockDispatchStatus(object BranchID, object UserId, bool StockDispatchStatus)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_U_STOCKDISPATCHSTATUS]";
                    cmd.Parameters.AddWithValue("@BRANCHID", BranchID);
                    cmd.Parameters.AddWithValue("@StockDispatchStatus", StockDispatchStatus);
                    cmd.Parameters.AddWithValue("@USERID", UserId);
                    object obj = cmd.ExecuteScalar();
                    if (!int.TryParse(Convert.ToString(obj), out int ivalue))
                        throw new Exception(Convert.ToString(obj));
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("There are few dispatches"))
                    throw ex;
                else
                    throw new Exception("Error While performing action");
            }
        }


        public DataTable GetBrand()
        {
            return new ReportRepository().GetReportData("USP_R_BRAND");
        }

        public object SaveBrand(object BrandID, string BrandName, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_BRAND]";
                    cmd.Parameters.AddWithValue("@BRANDID", BrandID);
                    cmd.Parameters.AddWithValue("@BRANDNAME", BrandName);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (int.TryParse(str, out int CategoryID))
                        return objReturn;
                    else
                        throw new Exception(str);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_TBLBRAND_BRANDNAME"))
                    throw new Exception("Brand Already Exists!!");
                else
                    throw new Exception("Error While Saving Brand");
            }
        }

        public void DeleteBrand(object BrandID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_BRAND]";
                    cmd.Parameters.AddWithValue("@BRANDID", BrandID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out int id))
                        throw new Exception(str);
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Few items are mapped with selected brand"))
                    throw ex;
                else
                    throw new Exception("Error while deleteing brand", ex);
            }
        }

        public DataTable GetManufacturer()
        {
            return new ReportRepository().GetReportData("USP_R_MANUFACTURER");
        }

        public object SaveManufacturer(object MANUFACTURERID, string MANUFACTURERNAME, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_CU_MANUFACTURER]";
                    cmd.Parameters.AddWithValue("@MANUFACTURERID", MANUFACTURERID);
                    cmd.Parameters.AddWithValue("@MANUFACTURERNAME", MANUFACTURERNAME);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (int.TryParse(str, out int CategoryID))
                        return objReturn;
                    else
                        throw new Exception(str);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UC_TBLMANUFACTURER_MANUFACTURERNAME"))
                    throw new Exception("Manufacturer Already Exists!!");
                else
                    throw new Exception("Error While Saving manufacturer");
            }
        }

        public void DeleteManufacturer(object MANUFACTURERID, object UserID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = SQLCon.Sqlconn();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "[USP_D_MANUFACTURER]";
                    cmd.Parameters.AddWithValue("@MANUFACTURERID", MANUFACTURERID);
                    cmd.Parameters.AddWithValue("@USERID", UserID);
                    object objReturn = cmd.ExecuteScalar();
                    string str = Convert.ToString(objReturn);
                    if (!int.TryParse(str, out int id))
                        throw new Exception(str);
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Few items are mapped with selected"))
                    throw ex;
                else
                    throw new Exception("Error While Deleteing manufacturer", ex);
            }
        }

        public DataTable GetUQCData()
        {
            return new ReportRepository().GetReportData("USP_R_UQCDATA");
        }
    }
}
