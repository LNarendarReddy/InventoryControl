USE [NSRetail]
GO
/****** Object:  StoredProcedure [dbo].[USP_U_STOCKENTRY]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_U_STOCKENTRY]
GO
/****** Object:  StoredProcedure [dbo].[USP_U_STOCKDISPATCH]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_U_STOCKDISPATCH]
GO
/****** Object:  StoredProcedure [dbo].[USP_U_PASSWORD]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_U_PASSWORD]
GO
/****** Object:  StoredProcedure [dbo].[USP_U_CHANGEPASSWORD]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_U_CHANGEPASSWORD]
GO
/****** Object:  StoredProcedure [dbo].[USP_U_BREFUND]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_U_BREFUND]
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_USER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_SYNC_CU_USER]
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_SYNC_CU_STOCKDISPATCHDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_STOCKDISPATCH]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_SYNC_CU_STOCKDISPATCH]
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_STOCKCOUNTINGDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_SYNC_CU_STOCKCOUNTINGDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_STOCKCOUNTING]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_SYNC_CU_STOCKCOUNTING]
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_DAYCLOSUREDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_SYNC_CU_DAYCLOSUREDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_DAYCLOSURE]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_SYNC_CU_DAYCLOSURE]
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_CREFUND]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_SYNC_CU_CREFUND]
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_BREFUNDDETAL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_SYNC_CU_BREFUNDDETAL]
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_BREFUND]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_SYNC_CU_BREFUND]
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_BILLMOPDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_SYNC_CU_BILLMOPDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_USERLOGIN]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_USERLOGIN]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_USER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_USER]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_UOM]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_UOM]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_SUBCATEGORY]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_SUBCATEGORY]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKSUMMARY]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_STOCKSUMMARY]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKENTRYDTAFT]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_STOCKENTRYDTAFT]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKENTRY]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_STOCKENTRY]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKDISPATCHDC]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_STOCKDISPATCHDC]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKCOUNTINGDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_STOCKCOUNTINGDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKCOUNTING]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_STOCKCOUNTING]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKCONTINGDIFF]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_STOCKCONTINGDIFF]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STATE]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_STATE]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ROLE]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_ROLE]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_PRINTERTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_PRINTERTYPE]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_PRINTERSETTINGS]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_PRINTERSETTINGS]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_PARENTITEMS]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_PARENTITEMS]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_OFFERTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_OFFERTYPE]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_OFFERITEM]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_OFFERITEM]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_OFFERBRANCH]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_OFFERBRANCH]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_OFFER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_OFFER]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_NONEANLIST]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_NONEANLIST]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_NEXTSKUCODE]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_NEXTSKUCODE]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_MOP]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_MOP]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMVISUALIZER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_ITEMVISUALIZER]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMMRPLIST]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_ITEMMRPLIST]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMGROUPDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_ITEMGROUPDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMGROUP]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_ITEMGROUP]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMCODES]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_ITEMCODES]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMCODELIST]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_ITEMCODELIST]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMCODE]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_ITEMCODE]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEM]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_ITEM]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_INVOICELIST]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_INVOICELIST]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_GST]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_GST]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_GETSYNCDATA]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_GETSYNCDATA]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DISPATCHLIST]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_DISPATCHLIST]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DISPATCHDRAFT]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_DISPATCHDRAFT]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DISPATCHDCLIST]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_DISPATCHDCLIST]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DISPATCH]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_DISPATCH]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DEALER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_DEALER]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DAYCLOSUREVOIDITEMS]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_DAYCLOSUREVOIDITEMS]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DAYCLOSUREREFUND]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_DAYCLOSUREREFUND]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DAYCLOSUREITEMS]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_DAYCLOSUREITEMS]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DAYCLOSUREDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_DAYCLOSUREDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DAYCLOSUREBILL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_DAYCLOSUREBILL]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DAYCLOSURE]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_DAYCLOSURE]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_CURRENTSTOCK]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_CURRENTSTOCK]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_COUNTER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_COUNTER]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_COSTPRICELIST]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_COSTPRICELIST]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_CATEGORY]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_CATEGORY]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BREFUNDDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_BREFUNDDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BREFUND]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_BREFUND]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BRANCHCOUNTER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_BRANCHCOUNTER]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BRANCH]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_BRANCH]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BILLDETAILBYID]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_BILLDETAILBYID]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_APPLIESTO]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_APPLIESTO]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_USER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_USER]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_UOM]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_UOM]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_SUBCATEGORY]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_SUBCATEGORY]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_STOCKENTRYDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_STOCKENTRYDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_STOCKDISPATCHDETAILS]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_STOCKDISPATCHDETAILS]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_OFFERITEM]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_OFFERITEM]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_OFFERBRANCH]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_OFFERBRANCH]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_OFFER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_OFFER]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_MOP]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_MOP]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_ITEMGROUPDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_ITEMGROUPDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_GST]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_GST]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_DISCARDSTOCKENTRY]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_DISCARDSTOCKENTRY]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_DEALER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_DEALER]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_COUNTER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_COUNTER]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_CATEGORY]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_CATEGORY]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_BRANCHCOUNTER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_BRANCHCOUNTER]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_BRANCH]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_BRANCH]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_USER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_USER]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_UOM]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_UOM]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_SUBCATEGORY]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_SUBCATEGORY]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_STOCKENTRYDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_STOCKENTRYDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_STOCKENTRY]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_STOCKENTRY]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_STOCKDISPATCHDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_STOCKDISPATCH]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_STOCKDISPATCH]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_PRINTERSETTINGS]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_PRINTERSETTINGS]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BILLDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_POS_BILLDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BILL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_POS_BILL]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFERITEM]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_OFFERITEM]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFERBRANCH]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_OFFERBRANCH]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_OFFER]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_MOP]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_MOP]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMGROUPDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_ITEMGROUPDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMGROUP]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_ITEMGROUP]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMCODE]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_ITEMCODE]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_GST]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_GST]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_DISPATCHDC]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_DISPATCHDC]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_DEALER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_DEALER]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_COUNTER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_COUNTER]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_CATEGORY]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_CATEGORY]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_BRANCHCOUNTER]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_BRANCHCOUNTER]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_BRANCH]    Script Date: 12-03-2022 11:18:49 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_BRANCH]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UOM]') AND type in (N'U'))
ALTER TABLE [dbo].[UOM] DROP CONSTRAINT IF EXISTS [FK__UOM__BASEUOMID__70A8B9AE]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UOM]') AND type in (N'U'))
ALTER TABLE [dbo].[UOM] DROP CONSTRAINT IF EXISTS [FK__UOM__BASEUOMID__6FB49575]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UOM]') AND type in (N'U'))
ALTER TABLE [dbo].[UOM] DROP CONSTRAINT IF EXISTS [FK__UOM__BASEUOMID__6EC0713C]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBLUSER]') AND type in (N'U'))
ALTER TABLE [dbo].[TBLUSER] DROP CONSTRAINT IF EXISTS [FK__TBLUSER__ROLEID__6DCC4D03]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBLUSER]') AND type in (N'U'))
ALTER TABLE [dbo].[TBLUSER] DROP CONSTRAINT IF EXISTS [FK__TBLUSER__REPORTI__6CD828CA]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKSUMMARY]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKSUMMARY] DROP CONSTRAINT IF EXISTS [FK__STOCKSUMM__ITEMP__1A34DF26]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKSUMMARY]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKSUMMARY] DROP CONSTRAINT IF EXISTS [FK__STOCKSUMM__BRANC__1940BAED]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKENTRYDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKENTRYDETAIL] DROP CONSTRAINT IF EXISTS [FK__STOCKENTR__UPDAT__681373AD]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKENTRYDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKENTRYDETAIL] DROP CONSTRAINT IF EXISTS [FK__STOCKENTR__STOCK__671F4F74]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKENTRYDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKENTRYDETAIL] DROP CONSTRAINT IF EXISTS [FK__STOCKENTR__DELET__662B2B3B]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKENTRYDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKENTRYDETAIL] DROP CONSTRAINT IF EXISTS [FK__STOCKENTR__CREAT__65370702]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKENTRY]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKENTRY] DROP CONSTRAINT IF EXISTS [FK__STOCKENTR__UPDAT__6442E2C9]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKENTRY]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKENTRY] DROP CONSTRAINT IF EXISTS [FK__STOCKENTR__SUPPL__634EBE90]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKENTRY]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKENTRY] DROP CONSTRAINT IF EXISTS [FK__STOCKENTR__DELET__625A9A57]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKENTRY]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKENTRY] DROP CONSTRAINT IF EXISTS [FK__STOCKENTR__CREAT__6166761E]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKDISPATCHDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKDISPATCHDETAIL] DROP CONSTRAINT IF EXISTS [FK__STOCKDISP__STOCK__5F7E2DAC]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKDISPATCHDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKDISPATCHDETAIL] DROP CONSTRAINT IF EXISTS [FK__STOCKDISP__ITEMP__5E8A0973]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKDISPATCH]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKDISPATCH] DROP CONSTRAINT IF EXISTS [FK__STOCKDISP__UPDAT__5BAD9CC8]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKDISPATCH]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKDISPATCH] DROP CONSTRAINT IF EXISTS [FK__STOCKDISP__TOBRA__5AB9788F]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKDISPATCH]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKDISPATCH] DROP CONSTRAINT IF EXISTS [FK__STOCKDISP__TOBRA__59C55456]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKDISPATCH]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKDISPATCH] DROP CONSTRAINT IF EXISTS [FK__STOCKDISP__FROMB__58D1301D]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKDISPATCH]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKDISPATCH] DROP CONSTRAINT IF EXISTS [FK__STOCKDISP__FROMB__57DD0BE4]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKDISPATCH]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKDISPATCH] DROP CONSTRAINT IF EXISTS [FK__STOCKDISP__DELET__56E8E7AB]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[STOCKDISPATCH]') AND type in (N'U'))
ALTER TABLE [dbo].[STOCKDISPATCH] DROP CONSTRAINT IF EXISTS [FK__STOCKDISP__CREAT__55F4C372]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFERITEMMAP]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFERITEMMAP] DROP CONSTRAINT IF EXISTS [FK__OFFERITEM__OFFER__062DE679]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFERITEMMAP]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFERITEMMAP] DROP CONSTRAINT IF EXISTS [FK__OFFERITEM__ITEMC__0539C240]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFERITEMMAP]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFERITEMMAP] DROP CONSTRAINT IF EXISTS [FK__OFFERITEM__DELET__04459E07]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFERITEMMAP]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFERITEMMAP] DROP CONSTRAINT IF EXISTS [FK__OFFERITEM__CREAT__035179CE]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFERBRANCH]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFERBRANCH] DROP CONSTRAINT IF EXISTS [FK__OFFERBRAN__OFFER__00750D23]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFERBRANCH]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFERBRANCH] DROP CONSTRAINT IF EXISTS [FK__OFFERBRAN__DELET__7F80E8EA]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFERBRANCH]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFERBRANCH] DROP CONSTRAINT IF EXISTS [FK__OFFERBRAN__CREAT__7E8CC4B1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFERBRANCH]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFERBRANCH] DROP CONSTRAINT IF EXISTS [FK__OFFERBRAN__BRANC__7D98A078]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFER]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFER] DROP CONSTRAINT IF EXISTS [FK__OFFER__UPDATEDBY__7ABC33CD]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFER]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFER] DROP CONSTRAINT IF EXISTS [FK__OFFER__OFFERTYPE__79C80F94]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFER]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFER] DROP CONSTRAINT IF EXISTS [FK__OFFER__ITEMGROUP__78D3EB5B]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFER]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFER] DROP CONSTRAINT IF EXISTS [FK__OFFER__DELETEDBY__77DFC722]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFER]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFER] DROP CONSTRAINT IF EXISTS [FK__OFFER__CREATEDBY__76EBA2E9]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OFFER]') AND type in (N'U'))
ALTER TABLE [dbo].[OFFER] DROP CONSTRAINT IF EXISTS [FK__OFFER__CATEGORYI__75F77EB0]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMPRICE]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMPRICE] DROP CONSTRAINT IF EXISTS [FK__ITEMPRICE__UPDAT__2645B050]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMPRICE]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMPRICE] DROP CONSTRAINT IF EXISTS [FK__ITEMPRICE__ITEMC__25518C17]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMPRICE]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMPRICE] DROP CONSTRAINT IF EXISTS [FK__ITEMPRICE__GSTID__245D67DE]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMPRICE]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMPRICE] DROP CONSTRAINT IF EXISTS [FK__ITEMPRICE__DELET__236943A5]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMPRICE]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMPRICE] DROP CONSTRAINT IF EXISTS [FK__ITEMPRICE__CREAT__22751F6C]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMGROUPDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMGROUPDETAIL] DROP CONSTRAINT IF EXISTS [FK__ITEMGROUP__ITEMG__4FD1D5C8]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMGROUPDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMGROUPDETAIL] DROP CONSTRAINT IF EXISTS [FK__ITEMGROUP__ITEMC__4EDDB18F]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMGROUPDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMGROUPDETAIL] DROP CONSTRAINT IF EXISTS [FK__ITEMGROUP__DELET__4DE98D56]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMGROUPDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMGROUPDETAIL] DROP CONSTRAINT IF EXISTS [FK__ITEMGROUP__CREAT__4CF5691D]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMGROUP]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMGROUP] DROP CONSTRAINT IF EXISTS [FK__ITEMGROUP__UPDAT__4C0144E4]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMGROUP]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMGROUP] DROP CONSTRAINT IF EXISTS [FK__ITEMGROUP__DELET__4B0D20AB]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMGROUP]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMGROUP] DROP CONSTRAINT IF EXISTS [FK__ITEMGROUP__CREAT__4A18FC72]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMCODE]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMCODE] DROP CONSTRAINT IF EXISTS [FK__ITEMCODE__UPDATE__2180FB33]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMCODE]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMCODE] DROP CONSTRAINT IF EXISTS [FK__ITEMCODE__ITEMID__208CD6FA]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMCODE]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMCODE] DROP CONSTRAINT IF EXISTS [FK__ITEMCODE__FREEIT__1F98B2C1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMCODE]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMCODE] DROP CONSTRAINT IF EXISTS [FK__ITEMCODE__DELETE__1EA48E88]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEMCODE]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEMCODE] DROP CONSTRAINT IF EXISTS [FK__ITEMCODE__CREATE__1DB06A4F]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEM]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEM] DROP CONSTRAINT IF EXISTS [FK__ITEM__UPDATEDBY__1CBC4616]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEM]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEM] DROP CONSTRAINT IF EXISTS [FK__ITEM__UOMID__1BC821DD]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEM]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEM] DROP CONSTRAINT IF EXISTS [FK__ITEM__SUBCATEGOR__1AD3FDA4]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEM]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEM] DROP CONSTRAINT IF EXISTS [FK__ITEM__PARENTITEM__19DFD96B]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEM]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEM] DROP CONSTRAINT IF EXISTS [FK__ITEM__DELETEDBY__18EBB532]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEM]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEM] DROP CONSTRAINT IF EXISTS [FK__ITEM__CREATEDBY__17F790F9]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEM]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEM] DROP CONSTRAINT IF EXISTS [FK__ITEM__CATEGORYID__17036CC0]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GSTDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[GSTDETAIL] DROP CONSTRAINT IF EXISTS [FK__GSTDETAIL__UPDAT__160F4887]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GSTDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[GSTDETAIL] DROP CONSTRAINT IF EXISTS [FK__GSTDETAIL__UPDAT__151B244E]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GSTDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[GSTDETAIL] DROP CONSTRAINT IF EXISTS [FK__GSTDETAIL__DELET__14270015]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GSTDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[GSTDETAIL] DROP CONSTRAINT IF EXISTS [FK__GSTDETAIL__DELET__1332DBDC]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GSTDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[GSTDETAIL] DROP CONSTRAINT IF EXISTS [FK__GSTDETAIL__CREAT__123EB7A3]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GSTDETAIL]') AND type in (N'U'))
ALTER TABLE [dbo].[GSTDETAIL] DROP CONSTRAINT IF EXISTS [FK__GSTDETAIL__CREAT__114A936A]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTITYSYNCSTATUS]') AND type in (N'U'))
ALTER TABLE [dbo].[ENTITYSYNCSTATUS] DROP CONSTRAINT IF EXISTS [FK__ENTITYSYN__ENTIT__10566F31]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTITYSYNCSTATUS]') AND type in (N'U'))
ALTER TABLE [dbo].[ENTITYSYNCSTATUS] DROP CONSTRAINT IF EXISTS [FK__ENTITYSYN__BRANC__0F624AF8]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BRANCH]') AND type in (N'U'))
ALTER TABLE [dbo].[BRANCH] DROP CONSTRAINT IF EXISTS [FK__BRANCH__UPDATEDB__797309D9]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BRANCH]') AND type in (N'U'))
ALTER TABLE [dbo].[BRANCH] DROP CONSTRAINT IF EXISTS [FK__BRANCH__DELETEDB__787EE5A0]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BRANCH]') AND type in (N'U'))
ALTER TABLE [dbo].[BRANCH] DROP CONSTRAINT IF EXISTS [FK__BRANCH__CREATEDB__778AC167]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ITEM]') AND type in (N'U'))
ALTER TABLE [dbo].[ITEM] DROP CONSTRAINT IF EXISTS [DF__ITEM__ISOPENITEM__75A278F5]
GO
/****** Object:  Table [dbo].[UOM]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[UOM]
GO
/****** Object:  Table [dbo].[TBLUSER]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[TBLUSER]
GO
/****** Object:  Table [dbo].[TBLSTATE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[TBLSTATE]
GO
/****** Object:  Table [dbo].[TBLROLE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[TBLROLE]
GO
/****** Object:  Table [dbo].[TBLMOP]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[TBLMOP]
GO
/****** Object:  Table [dbo].[TBLDEALER]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[TBLDEALER]
GO
/****** Object:  Table [dbo].[TBLCATEGORY]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[TBLCATEGORY]
GO
/****** Object:  Table [dbo].[SUBCATEGORY]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[SUBCATEGORY]
GO
/****** Object:  Table [dbo].[STOCKSUMMARY]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[STOCKSUMMARY]
GO
/****** Object:  Table [dbo].[STOCKENTRYDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[STOCKENTRYDETAIL]
GO
/****** Object:  Table [dbo].[STOCKENTRY]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[STOCKENTRY]
GO
/****** Object:  Table [dbo].[STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[STOCKDISPATCHDETAIL]
GO
/****** Object:  Table [dbo].[STOCKDISPATCH]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[STOCKDISPATCH]
GO
/****** Object:  Table [dbo].[STOCKCOUNTINGDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[STOCKCOUNTINGDETAIL]
GO
/****** Object:  Table [dbo].[STOCKCOUNTING]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[STOCKCOUNTING]
GO
/****** Object:  Table [dbo].[skumaster]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[skumaster]
GO
/****** Object:  Table [dbo].[PRINTERTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[PRINTERTYPE]
GO
/****** Object:  Table [dbo].[PRINTERSETTINGS]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[PRINTERSETTINGS]
GO
/****** Object:  Table [dbo].[POS_DENOMINATION]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[POS_DENOMINATION]
GO
/****** Object:  Table [dbo].[POS_DAYCLOSUREDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[POS_DAYCLOSUREDETAIL]
GO
/****** Object:  Table [dbo].[POS_DAYCLOSURE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[POS_DAYCLOSURE]
GO
/****** Object:  Table [dbo].[POS_CREFUND]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[POS_CREFUND]
GO
/****** Object:  Table [dbo].[POS_BREFUNDDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[POS_BREFUNDDETAIL]
GO
/****** Object:  Table [dbo].[POS_BREFUND]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[POS_BREFUND]
GO
/****** Object:  Table [dbo].[POS_BILLMOPDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[POS_BILLMOPDETAIL]
GO
/****** Object:  Table [dbo].[POS_BILLDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[POS_BILLDETAIL]
GO
/****** Object:  Table [dbo].[POS_BILL]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[POS_BILL]
GO
/****** Object:  Table [dbo].[OFFERTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[OFFERTYPE]
GO
/****** Object:  Table [dbo].[OFFERITEMMAP]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[OFFERITEMMAP]
GO
/****** Object:  Table [dbo].[OFFERBRANCH]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[OFFERBRANCH]
GO
/****** Object:  Table [dbo].[OFFER]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[OFFER]
GO
/****** Object:  Table [dbo].[ITEMPRICE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[ITEMPRICE]
GO
/****** Object:  Table [dbo].[ITEMGROUPDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[ITEMGROUPDETAIL]
GO
/****** Object:  Table [dbo].[ITEMGROUP]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[ITEMGROUP]
GO
/****** Object:  Table [dbo].[ITEMCOSTPRICE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[ITEMCOSTPRICE]
GO
/****** Object:  Table [dbo].[ITEMCODE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[ITEMCODE]
GO
/****** Object:  Table [dbo].[ITEM]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[ITEM]
GO
/****** Object:  Table [dbo].[GSTDETAIL]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[GSTDETAIL]
GO
/****** Object:  Table [dbo].[ENTITYSYNCSTATUS]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[ENTITYSYNCSTATUS]
GO
/****** Object:  Table [dbo].[ENTITY]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[ENTITY]
GO
/****** Object:  Table [dbo].[DISPATCHDCMAPPING]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[DISPATCHDCMAPPING]
GO
/****** Object:  Table [dbo].[DISPATCHDC]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[DISPATCHDC]
GO
/****** Object:  Table [dbo].[BRANCHCOUNTER]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[BRANCHCOUNTER]
GO
/****** Object:  Table [dbo].[BRANCH]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[BRANCH]
GO
/****** Object:  Table [dbo].[APPLIESTO]    Script Date: 12-03-2022 11:18:49 ******/
DROP TABLE IF EXISTS [dbo].[APPLIESTO]
GO
/****** Object:  UserDefinedTableType [dbo].[TBLUSERTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[TBLUSERTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[STOCKDISPATCHTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[STOCKDISPATCHTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[STOCKDISPATCHDETAILTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[STOCKDISPATCHDETAILTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[STOCKCOUNTINGTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[STOCKCOUNTINGTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[STOCKCOUNTINGDETAILTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[STOCKCOUNTINGDETAILTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_DAYCLOSURETYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[POS_DAYCLOSURETYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_DAYCLOSUREDETAILTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[POS_DAYCLOSUREDETAILTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_CREFUNDTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[POS_CREFUNDTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BREFUNDTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[POS_BREFUNDTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BREFUNDDETAILTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[POS_BREFUNDDETAILTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BRDTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[POS_BRDTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BILLTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[POS_BILLTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BILLMOPDETAILTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[POS_BILLMOPDETAILTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BILLDETAILTYPE]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[POS_BILLDETAILTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[dtInts]    Script Date: 12-03-2022 11:18:49 ******/
DROP TYPE IF EXISTS [dbo].[dtInts]
GO
USE [master]
GO
/****** Object:  Database [NSRetail]    Script Date: 12-03-2022 11:18:49 ******/
DROP DATABASE IF EXISTS [NSRetail]
GO
/****** Object:  Database [NSRetail]    Script Date: 12-03-2022 11:18:49 ******/
CREATE DATABASE [NSRetail]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NSRetail', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NSRetail.mdf' , SIZE = 193536KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NSRetail_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NSRetail_log.ldf' , SIZE = 1475904KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [NSRetail] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NSRetail].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NSRetail] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NSRetail] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NSRetail] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NSRetail] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NSRetail] SET ARITHABORT OFF 
GO
ALTER DATABASE [NSRetail] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NSRetail] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NSRetail] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NSRetail] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NSRetail] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NSRetail] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NSRetail] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NSRetail] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NSRetail] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NSRetail] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NSRetail] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NSRetail] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NSRetail] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NSRetail] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NSRetail] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NSRetail] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NSRetail] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NSRetail] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NSRetail] SET  MULTI_USER 
GO
ALTER DATABASE [NSRetail] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NSRetail] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NSRetail] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NSRetail] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [NSRetail] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NSRetail] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NSRetail', N'ON'
GO
ALTER DATABASE [NSRetail] SET QUERY_STORE = OFF
GO
USE [NSRetail]
GO
/****** Object:  UserDefinedTableType [dbo].[dtInts]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[dtInts] AS TABLE(
	[ID] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BILLDETAILTYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[POS_BILLDETAILTYPE] AS TABLE(
	[BILLDETAILID] [int] NOT NULL,
	[BILLID] [int] NOT NULL,
	[BRANCHCOUNTERID] [int] NOT NULL,
	[ITEMPRICEID] [int] NOT NULL,
	[QUANTITY] [int] NOT NULL,
	[WEIGHTINKGS] [decimal](5, 2) NULL,
	[BILLEDAMOUNT] [decimal](10, 2) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[CGST] [decimal](10, 2) NULL,
	[SGST] [decimal](10, 2) NULL,
	[IGST] [decimal](10, 2) NULL,
	[CESS] [decimal](10, 2) NULL,
	[GSTVALUE] [decimal](10, 2) NULL,
	[GSTID] [int] NULL,
	[SNO] [int] NULL,
	[DISCOUNT] [decimal](18, 2) NULL,
	[OFFERID] [int] NULL,
	[DAYCLOSUREID] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BILLMOPDETAILTYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[POS_BILLMOPDETAILTYPE] AS TABLE(
	[BILLMOPDETAILID] [int] NOT NULL,
	[BILLID] [int] NOT NULL,
	[MOPID] [int] NOT NULL,
	[MOPVALUE] [decimal](11, 2) NOT NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[COUNTERID] [int] NOT NULL,
	[DAYCLOSUREID] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BILLTYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[POS_BILLTYPE] AS TABLE(
	[BILLID] [int] NOT NULL,
	[BRANCHCOUNTERID] [int] NOT NULL,
	[BILLNUMBER] [varchar](20) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[BILLSTATUS] [int] NULL,
	[CUSTOMERNUMBER] [varchar](10) NULL,
	[CUSTOMERNAME] [varchar](100) NULL,
	[DAYCLOSUREID] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BRDTYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[POS_BRDTYPE] AS TABLE(
	[BREFUNDDETAILID] [int] NOT NULL,
	[BREFUNDID] [int] NULL,
	[ITEMPRICEID] [int] NULL,
	[QUANTITY] [int] NULL,
	[WEIGHTINKGS] [decimal](10, 2) NULL,
	[ACCEPTEDQUANTITY] [int] NULL,
	[ACCEPTEDWEIGHTINKGS] [decimal](10, 2) NULL,
	[COUNTERID] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BREFUNDDETAILTYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[POS_BREFUNDDETAILTYPE] AS TABLE(
	[BREFUNDDETAILID] [int] NOT NULL,
	[BREFUNDID] [int] NULL,
	[ITEMPRICEID] [int] NULL,
	[QUANTITY] [int] NULL,
	[WEIGHTINKGS] [decimal](10, 2) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[SNO] [int] NULL,
	[TRAYNUMBER] [int] NULL,
	[COUNTERID] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BREFUNDTYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[POS_BREFUNDTYPE] AS TABLE(
	[BREFUNDID] [int] NOT NULL,
	[BRANCHID] [int] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [date] NULL,
	[STATUS] [bit] NULL,
	[BREFUNDNUMBER] [nvarchar](50) NULL,
	[COUNTERID] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_CREFUNDTYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[POS_CREFUNDTYPE] AS TABLE(
	[REFUNDID] [int] NOT NULL,
	[BILLDETAILID] [int] NULL,
	[REFUNDQUANTITY] [int] NULL,
	[REFUNDWEIGHTINKGS] [decimal](10, 2) NULL,
	[REFUNDAMOUNT] [decimal](10, 2) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[DAYCLOSUREID] [int] NULL,
	[COUNTERID] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_DAYCLOSUREDETAILTYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[POS_DAYCLOSUREDETAILTYPE] AS TABLE(
	[DAYCLOSUREDETAILID] [int] NOT NULL,
	[DAYCLOSUREID] [int] NULL,
	[DENOMINATIONID] [int] NULL,
	[CLOSUREVALUE] [decimal](10, 2) NULL,
	[MOPID] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[COUNTERID] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_DAYCLOSURETYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[POS_DAYCLOSURETYPE] AS TABLE(
	[DAYCLOSUREID] [int] NOT NULL,
	[CLOSUREDATE] [datetime] NULL,
	[BRANCHCOUNTERID] [int] NULL,
	[OPENINGBALANCE] [decimal](10, 2) NULL,
	[CLOSINGBALANCE] [decimal](10, 2) NULL,
	[CLOSINGDIFFERENCE] [decimal](10, 2) NULL,
	[CLOSEDBY] [int] NULL,
	[REFUNDAMOUNT] [decimal](10, 2) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[STOCKCOUNTINGDETAILTYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[STOCKCOUNTINGDETAILTYPE] AS TABLE(
	[STOCKCOUNTINGDETAILID] [int] NOT NULL,
	[STOCKCOUNTINGID] [int] NULL,
	[ITEMPRICEID] [int] NULL,
	[QUANTITY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[STOCKCOUNTINGTYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[STOCKCOUNTINGTYPE] AS TABLE(
	[STOCKCOUNTINGID] [int] NOT NULL,
	[BRANCHID] [int] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[STATUS] [bit] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[STOCKDISPATCHDETAILTYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[STOCKDISPATCHDETAILTYPE] AS TABLE(
	[STOCKDISPATCHDETAILID] [int] NOT NULL,
	[STOCKDISPATCHID] [int] NOT NULL,
	[ITEMPRICEID] [int] NOT NULL,
	[TRAYNUMBER] [int] NULL,
	[DISPATCHQUANTITY] [int] NOT NULL,
	[RECEIVEDQUANTITY] [int] NOT NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[WEIGHTINKGS] [decimal](10, 2) NULL,
	[ISACCEPTED] [bit] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[STOCKDISPATCHTYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[STOCKDISPATCHTYPE] AS TABLE(
	[STOCKDISPATCHID] [int] NOT NULL,
	[FROMBRANCHID] [int] NOT NULL,
	[TOBRANCHID] [int] NOT NULL,
	[STATUS] [varchar](20) NOT NULL,
	[STATUSAPPROVEDBY] [varchar](100) NULL,
	[STATUSAPPROVEDDATE] [datetime] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[DISPATCHNUMBER] [varchar](40) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TBLUSERTYPE]    Script Date: 12-03-2022 11:18:50 ******/
CREATE TYPE [dbo].[TBLUSERTYPE] AS TABLE(
	[USERID] [int] NOT NULL,
	[ROLEID] [int] NOT NULL,
	[REPORTINGLEADID] [int] NULL,
	[CATEGORYID] [int] NULL,
	[BRANCHID] [int] NULL,
	[USERNAME] [nvarchar](50) NOT NULL,
	[PASSWORDSTRING] [nvarchar](100) NOT NULL,
	[FULLNAME] [nvarchar](50) NOT NULL,
	[CNUMBER] [nvarchar](50) NULL,
	[EMAIL] [nvarchar](50) NULL,
	[ISOTP] [bit] NULL,
	[GENDER] [int] NULL,
	[DOB] [date] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
)
GO
/****** Object:  Table [dbo].[APPLIESTO]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APPLIESTO](
	[AppliesToID] [int] IDENTITY(1,1) NOT NULL,
	[AppliesToName] [varchar](50) NULL,
 CONSTRAINT [PK_APPLIESTO] PRIMARY KEY CLUSTERED 
(
	[AppliesToID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BRANCH]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BRANCH](
	[BRANCHID] [int] IDENTITY(1,1) NOT NULL,
	[BRANCHNAME] [varchar](100) NOT NULL,
	[BRANCHCODE] [varchar](5) NOT NULL,
	[ADDRESS] [varchar](500) NULL,
	[PHONENO] [varchar](20) NULL,
	[LANDLINE] [varchar](20) NULL,
	[EMAILID] [varchar](50) NULL,
	[SUPERVISORID] [int] NULL,
	[ISWAREHOUSE] [bit] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[STATEID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BRANCHID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BRANCHCOUNTER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BRANCHCOUNTER](
	[COUNTERID] [int] IDENTITY(1,1) NOT NULL,
	[COUNTERNAME] [nvarchar](50) NULL,
	[BRANCHID] [int] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[DAYCLOSUREID] [int] NULL,
	[BRANCHREFUNDID] [int] NULL,
 CONSTRAINT [PK_BRANCHCOUNTER] PRIMARY KEY CLUSTERED 
(
	[COUNTERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DISPATCHDC]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DISPATCHDC](
	[DISPATCHDCID] [int] IDENTITY(1,1) NOT NULL,
	[DISPATCHDCNUMBER] [varchar](40) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[BRANCHID] [int] NULL,
	[CATEGORYID] [int] NULL,
	[FROMBRANCHID] [int] NULL,
 CONSTRAINT [PK_DISPATCHDC] PRIMARY KEY CLUSTERED 
(
	[DISPATCHDCID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DISPATCHDCMAPPING]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DISPATCHDCMAPPING](
	[DISPATCHDCMAPPINGID] [int] IDENTITY(1,1) NOT NULL,
	[STOCKDISPATCHID] [int] NULL,
	[DISPATCHDCID] [int] NULL,
 CONSTRAINT [PK_DISPATCHDCMAPPING] PRIMARY KEY CLUSTERED 
(
	[DISPATCHDCMAPPINGID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ENTITY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ENTITY](
	[ENTITYID] [int] IDENTITY(1,1) NOT NULL,
	[ENTITYNAME] [varchar](50) NOT NULL,
	[SYNCORDER] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ENTITYID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ENTITYSYNCSTATUS]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ENTITYSYNCSTATUS](
	[ENTITYSYNCSTATUSID] [int] IDENTITY(1,1) NOT NULL,
	[ENTITYID] [int] NULL,
	[BRANCHID] [int] NULL,
	[SYNCSTATUS] [int] NULL,
	[SYNCDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ENTITYSYNCSTATUSID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GSTDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GSTDETAIL](
	[GSTID] [int] IDENTITY(1,1) NOT NULL,
	[GSTCODE] [varchar](20) NOT NULL,
	[CGST] [decimal](5, 2) NULL,
	[SGST] [decimal](5, 2) NULL,
	[IGST] [decimal](5, 2) NULL,
	[CESS] [decimal](5, 2) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[GSTID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITEM]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ITEM](
	[ITEMID] [int] IDENTITY(1,1) NOT NULL,
	[SKUCODE] [varchar](10) NOT NULL,
	[ITEMNAME] [varchar](100) NOT NULL,
	[DESCRIPTION] [varchar](500) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[CATEGORYID] [int] NULL,
	[SUBCATEGORYID] [int] NULL,
	[ISOPENITEM] [bit] NULL,
	[PARENTITEMID] [int] NULL,
	[UOMID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ITEMID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITEMCODE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ITEMCODE](
	[ITEMCODEID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMID] [int] NOT NULL,
	[ITEMCODE] [varchar](20) NULL,
	[ISEAN] [bit] NULL,
	[HSNCODE] [varchar](10) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[FREEITEMCODEID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ITEMCODEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITEMCOSTPRICE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ITEMCOSTPRICE](
	[ITEMCOSTPRICEID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMPRICEID] [int] NULL,
	[COSTPRICEWT] [decimal](11, 4) NULL,
	[COSTPRICEWOT] [decimal](11, 4) NULL,
	[QUANTITY] [int] NULL,
	[WEIGHTINKGS] [decimal](10, 2) NULL,
	[GSTID] [int] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [date] NULL,
 CONSTRAINT [PK_ITEMCOSTPRICE] PRIMARY KEY CLUSTERED 
(
	[ITEMCOSTPRICEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITEMGROUP]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ITEMGROUP](
	[ITEMGROUPID] [int] IDENTITY(1,1) NOT NULL,
	[GROUPNAME] [varchar](100) NULL,
	[ISACTIVE] [bit] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ITEMGROUPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITEMGROUPDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ITEMGROUPDETAIL](
	[ITEMGROUPDETAILID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMGROUPID] [int] NULL,
	[ITEMCODEID] [int] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ITEMGROUPDETAILID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITEMPRICE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ITEMPRICE](
	[ITEMPRICEID] [int] IDENTITY(1,1) NOT NULL,
	[ITEMCODEID] [int] NOT NULL,
	[SALEPRICE] [decimal](7, 2) NULL,
	[MRP] [decimal](7, 2) NULL,
	[GSTID] [int] NOT NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ITEMPRICEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OFFER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OFFER](
	[OFFERID] [int] IDENTITY(1,1) NOT NULL,
	[OFFERNAME] [varchar](100) NULL,
	[OFFERCODE] [varchar](10) NULL,
	[STARTDATE] [datetime] NULL,
	[ENDDATE] [datetime] NULL,
	[OFFERVALUE] [decimal](10, 2) NULL,
	[OFFERTYPEID] [int] NULL,
	[CATEGORYID] [int] NULL,
	[ITEMGROUPID] [int] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[ISACTIVE] [bit] NULL,
	[APPLIESTOID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[OFFERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_OFFERCODE] UNIQUE NONCLUSTERED 
(
	[OFFERCODE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OFFERBRANCH]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OFFERBRANCH](
	[OFFERBRANCHID] [int] IDENTITY(1,1) NOT NULL,
	[OFFERID] [int] NULL,
	[BRANCHID] [int] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[OFFERBRANCHID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OFFERITEMMAP]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OFFERITEMMAP](
	[OFFERITEMMAPID] [int] IDENTITY(1,1) NOT NULL,
	[OFFERID] [int] NULL,
	[ITEMCODEID] [int] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[OFFERITEMMAPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OFFERTYPE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OFFERTYPE](
	[OFFERTYPEID] [int] IDENTITY(1,1) NOT NULL,
	[OFFERTYPENAME] [varchar](50) NULL,
	[OFFERTYPECODE] [varchar](10) NULL,
	[BUYQUANTITY] [int] NULL,
	[FREEQUANTITY] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[OFFERTYPEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_BILL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POS_BILL](
	[BILLID] [int] NOT NULL,
	[BRANCHCOUNTERID] [int] NOT NULL,
	[BILLNUMBER] [varchar](20) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[BILLSTATUS] [int] NULL,
	[CUSTOMERNUMBER] [varchar](10) NULL,
	[CUSTOMERNAME] [varchar](100) NULL,
	[DAYCLOSUREID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_BILLDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POS_BILLDETAIL](
	[BILLDETAILID] [int] NOT NULL,
	[BILLID] [int] NOT NULL,
	[BRANCHCOUNTERID] [int] NOT NULL,
	[ITEMPRICEID] [int] NOT NULL,
	[QUANTITY] [int] NOT NULL,
	[WEIGHTINKGS] [decimal](5, 2) NULL,
	[BILLEDAMOUNT] [decimal](10, 2) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[CGST] [decimal](10, 2) NULL,
	[SGST] [decimal](10, 2) NULL,
	[IGST] [decimal](10, 2) NULL,
	[CESS] [decimal](10, 2) NULL,
	[GSTVALUE] [decimal](10, 2) NULL,
	[GSTID] [int] NULL,
	[SNO] [int] NULL,
	[DISCOUNT] [decimal](18, 2) NULL,
	[OFFERID] [int] NULL,
	[DAYCLOSUREID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_BILLMOPDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POS_BILLMOPDETAIL](
	[BILLMOPDETAILID] [int] NOT NULL,
	[BILLID] [int] NOT NULL,
	[MOPID] [int] NOT NULL,
	[MOPVALUE] [decimal](11, 2) NOT NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[COUNTERID] [int] NOT NULL,
	[DAYCLOSUREID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_BREFUND]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POS_BREFUND](
	[BREFUNDID] [int] NOT NULL,
	[BRANCHID] [int] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [date] NULL,
	[STATUS] [bit] NULL,
	[BREFUNDNUMBER] [nvarchar](50) NULL,
	[COUNTERID] [int] NOT NULL,
	[IsAccepted] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_BREFUNDDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POS_BREFUNDDETAIL](
	[BREFUNDDETAILID] [int] NOT NULL,
	[BREFUNDID] [int] NULL,
	[ITEMPRICEID] [int] NULL,
	[QUANTITY] [int] NULL,
	[WEIGHTINKGS] [decimal](10, 2) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[SNO] [int] NULL,
	[TRAYNUMBER] [int] NULL,
	[COUNTERID] [int] NOT NULL,
	[ACCEPTEDQUANTITY] [int] NULL,
	[ACCEPTEDWEIGHTKGS] [decimal](10, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_CREFUND]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POS_CREFUND](
	[REFUNDID] [int] NOT NULL,
	[BILLDETAILID] [int] NULL,
	[REFUNDQUANTITY] [int] NULL,
	[REFUNDWEIGHTINKGS] [decimal](10, 2) NULL,
	[REFUNDAMOUNT] [decimal](10, 2) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[DAYCLOSUREID] [int] NULL,
	[COUNTERID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_DAYCLOSURE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POS_DAYCLOSURE](
	[DAYCLOSUREID] [int] NOT NULL,
	[CLOSUREDATE] [datetime] NULL,
	[BRANCHCOUNTERID] [int] NULL,
	[OPENINGBALANCE] [decimal](10, 2) NULL,
	[CLOSINGBALANCE] [decimal](10, 2) NULL,
	[CLOSINGDIFFERENCE] [decimal](10, 2) NULL,
	[CLOSEDBY] [int] NULL,
	[REFUNDAMOUNT] [decimal](10, 2) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_DAYCLOSUREDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POS_DAYCLOSUREDETAIL](
	[DAYCLOSUREDETAILID] [int] NOT NULL,
	[DAYCLOSUREID] [int] NULL,
	[DENOMINATIONID] [int] NULL,
	[CLOSUREVALUE] [decimal](10, 2) NULL,
	[MOPID] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[COUNTERID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_DENOMINATION]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POS_DENOMINATION](
	[DENOMINATIONID] [int] IDENTITY(1,1) NOT NULL,
	[DISPLAYVALUE] [varchar](20) NULL,
	[MULTIPLIER] [decimal](6, 2) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[DENOMINATIONID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRINTERSETTINGS]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRINTERSETTINGS](
	[PRINTERSETTINGSID] [int] IDENTITY(1,1) NOT NULL,
	[PRINTERTYPEID] [int] NULL,
	[PRINTERNAME] [varchar](500) NULL,
	[USERID] [int] NULL,
 CONSTRAINT [PK_PRINTERSETTINGS] PRIMARY KEY CLUSTERED 
(
	[PRINTERSETTINGSID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PRINTERTYPE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PRINTERTYPE](
	[PRINTERTYPEID] [int] IDENTITY(1,1) NOT NULL,
	[PRINTERTYPENAME] [varchar](500) NULL,
 CONSTRAINT [PK_IC_PrinterType] PRIMARY KEY CLUSTERED 
(
	[PRINTERTYPEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[skumaster]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[skumaster](
	[Category] [nvarchar](max) NULL,
	[Sub_Category] [nvarchar](max) NULL,
	[Section] [nvarchar](max) NULL,
	[Department] [nvarchar](max) NULL,
	[Product_ID] [nvarchar](max) NULL,
	[Product_Name] [nvarchar](max) NULL,
	[Alias_Name] [nvarchar](max) NULL,
	[EAN_Code] [nvarchar](max) NULL,
	[Brand] [nvarchar](max) NULL,
	[Style] [nvarchar](max) NULL,
	[Size] [nvarchar](max) NULL,
	[Colour] [nvarchar](max) NULL,
	[Fabric] [nvarchar](max) NULL,
	[Product_Range] [nvarchar](max) NULL,
	[Measurement_Range] [nvarchar](max) NULL,
	[TECHNICAL_SPECIFICATION] [nvarchar](max) NULL,
	[UOM] [nvarchar](max) NULL,
	[UOM1] [nvarchar](max) NULL,
	[UOM2] [nvarchar](max) NULL,
	[UOM_LABEL] [nvarchar](max) NULL,
	[STOCK_QTY] [nvarchar](max) NULL,
	[Re_Order_Point] [nvarchar](max) NULL,
	[Lead_Time] [nvarchar](max) NULL,
	[Cost_Price] [nvarchar](max) NULL,
	[Cash_Disc] [nvarchar](max) NULL,
	[NRV] [nvarchar](max) NULL,
	[MRP] [nvarchar](max) NULL,
	[Sale_Price] [nvarchar](max) NULL,
	[wholesale_price] [nvarchar](max) NULL,
	[Tax_Code] [nvarchar](max) NULL,
	[Mark_up] [nvarchar](max) NULL,
	[Min_Order_Qty] [nvarchar](max) NULL,
	[Batch_No] [nvarchar](max) NULL,
	[Expiry_Date] [nvarchar](max) NULL,
	[Warranty_Period] [nvarchar](max) NULL,
	[Manufacturer_Name] [nvarchar](max) NULL,
	[SUPPLIER_NAME] [nvarchar](max) NULL,
	[Is_Editable] [nvarchar](max) NULL,
	[Is_Taxable] [nvarchar](max) NULL,
	[JIT_Order] [nvarchar](max) NULL,
	[Image_1] [nvarchar](max) NULL,
	[Image_2] [nvarchar](max) NULL,
	[Image_3] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[min_sale_qty] [nvarchar](max) NULL,
	[Zero_Stock_Order] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[Item_Type] [nvarchar](max) NULL,
	[Utility] [nvarchar](max) NULL,
	[Manufactured_Item] [nvarchar](max) NULL,
	[Packed_Item] [nvarchar](max) NULL,
	[HSN_Code] [nvarchar](max) NULL,
	[Packaging_Date] [nvarchar](max) NULL,
	[Business_Category] [nvarchar](max) NULL,
	[Business_Subcategory] [nvarchar](max) NULL,
	[Tracking_Required] [nvarchar](max) NULL,
	[Product_usage] [nvarchar](max) NULL,
	[Product_Side_Effects] [nvarchar](max) NULL,
	[Product_Precautions] [nvarchar](max) NULL,
	[Product_Handing] [nvarchar](max) NULL,
	[Product_Consumption_Interaction] [nvarchar](max) NULL,
	[is_combo] [nvarchar](max) NULL,
	[Location_ID] [nvarchar](max) NULL,
	[Is_Tax_Exclusive] [nvarchar](max) NULL,
	[Is_Cost_Price_Editable] [nvarchar](max) NULL,
	[Life_Time_Span] [nvarchar](max) NULL,
	[Stock_Maintain_Days] [nvarchar](max) NULL,
	[Stock_Factor] [nvarchar](max) NULL,
	[MBQ] [nvarchar](max) NULL,
	[Max_Sale_Qty] [nvarchar](max) NULL,
	[Discount_Type] [nvarchar](max) NULL,
	[Discount] [nvarchar](max) NULL,
	[RFID_Tag] [nvarchar](max) NULL,
	[Product_Serial_Number] [nvarchar](max) NULL,
	[Batch_Required] [nvarchar](max) NULL,
	[Theme] [nvarchar](max) NULL,
	[Sub_Theme] [nvarchar](max) NULL,
	[Material_Type] [nvarchar](max) NULL,
	[Season] [nvarchar](max) NULL,
	[Item_Design] [nvarchar](max) NULL,
	[Pattern_Code] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STOCKCOUNTING]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STOCKCOUNTING](
	[STOCKCOUNTINGID] [int] NOT NULL,
	[BRANCHID] [int] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[STATUS] [bit] NULL,
 CONSTRAINT [PK_STOCKCOUNTING] PRIMARY KEY CLUSTERED 
(
	[STOCKCOUNTINGID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STOCKCOUNTINGDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STOCKCOUNTINGDETAIL](
	[STOCKCOUNTINGDETAILID] [int] NOT NULL,
	[STOCKCOUNTINGID] [int] NULL,
	[ITEMPRICEID] [int] NULL,
	[QUANTITY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
 CONSTRAINT [PK_STOCKCOUNTINGDETAIL] PRIMARY KEY CLUSTERED 
(
	[STOCKCOUNTINGDETAILID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STOCKDISPATCH]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STOCKDISPATCH](
	[STOCKDISPATCHID] [int] IDENTITY(1,1) NOT NULL,
	[FROMBRANCHID] [int] NOT NULL,
	[TOBRANCHID] [int] NOT NULL,
	[STATUS] [varchar](20) NOT NULL,
	[STATUSAPPROVEDBY] [varchar](100) NULL,
	[STATUSAPPROVEDDATE] [datetime] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[CATEGORYID] [int] NULL,
	[DISPATCHNUMBER] [varchar](40) NULL,
PRIMARY KEY CLUSTERED 
(
	[STOCKDISPATCHID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STOCKDISPATCHDETAIL](
	[STOCKDISPATCHDETAILID] [int] IDENTITY(1,1) NOT NULL,
	[STOCKDISPATCHID] [int] NOT NULL,
	[ITEMPRICEID] [int] NOT NULL,
	[TRAYNUMBER] [int] NULL,
	[DISPATCHQUANTITY] [int] NOT NULL,
	[RECEIVEDQUANTITY] [int] NOT NULL,
	[WEIGHTINKGS] [decimal](10, 2) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[ISACCEPTED] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[STOCKDISPATCHDETAILID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STOCKENTRY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STOCKENTRY](
	[STOCKENTRYID] [int] IDENTITY(1,1) NOT NULL,
	[STOCKENTRYNUMBER] [varchar](12) NULL,
	[DESCRIPTION] [varchar](500) NULL,
	[SUPPLIERID] [int] NOT NULL,
	[SUPPLIERINVOICENO] [varchar](12) NULL,
	[TAXINCLUSIVE] [bit] NULL,
	[CATEGORYID] [int] NULL,
	[STATUS] [bit] NULL,
	[INVOICEDATE] [date] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[TCS] [decimal](10, 2) NULL,
	[DISCOUNTPER] [decimal](10, 2) NULL,
	[DISCOUNT] [decimal](10, 2) NULL,
	[EXPENSES] [decimal](10, 2) NULL,
	[TRANSPORT] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[STOCKENTRYID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STOCKENTRYDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STOCKENTRYDETAIL](
	[STOCKENTRYDETAILID] [int] IDENTITY(1,1) NOT NULL,
	[DESCRIPTION] [varchar](500) NULL,
	[STOCKENTRYID] [int] NOT NULL,
	[QUANTITY] [int] NOT NULL,
	[WEIGHTINKGS] [decimal](10, 2) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[FREEITEMCOSTPRICEID] [int] NULL,
	[FREEQUANTITY] [int] NULL,
	[DISCOUNTFLAT] [decimal](9, 2) NULL,
	[DISCOUNTPERCENTAGE] [decimal](4, 2) NULL,
	[SCHEMEPERCENTAGE] [decimal](4, 2) NULL,
	[SCHEMEFLAT] [decimal](9, 2) NULL,
	[TOTALPRICEWT] [decimal](11, 4) NULL,
	[TOTALPRICEWOT] [decimal](11, 4) NULL,
	[APPLIEDDISCOUNT] [decimal](9, 2) NULL,
	[APPLIEDSCHEME] [decimal](9, 2) NULL,
	[APPLIEDDGST] [decimal](9, 2) NULL,
	[FINALPRICE] [decimal](9, 2) NULL,
	[ITEMCOSTPRICEID] [int] NULL,
	[CGST] [decimal](10, 2) NULL,
	[SGST] [decimal](10, 2) NULL,
	[IGST] [decimal](10, 2) NULL,
	[CESS] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[STOCKENTRYDETAILID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STOCKSUMMARY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STOCKSUMMARY](
	[STOCKSUMMARYID] [int] IDENTITY(1,1) NOT NULL,
	[BRANCHID] [int] NOT NULL,
	[ITEMPRICEID] [int] NOT NULL,
	[QUANTITY] [int] NOT NULL,
	[INTRANSITQUANTITY] [int] NULL,
	[WEIGHTINKGS] [decimal](9, 2) NULL,
	[INTRANSITWEIGHTINKGS] [decimal](9, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[STOCKSUMMARYID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SUBCATEGORY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SUBCATEGORY](
	[SUBCATEGORYID] [int] IDENTITY(1,1) NOT NULL,
	[CATEGORYID] [int] NULL,
	[SUBCATEGORYNAME] [varchar](50) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
 CONSTRAINT [PK_SUBCATEGORY] PRIMARY KEY CLUSTERED 
(
	[SUBCATEGORYID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLCATEGORY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLCATEGORY](
	[CATEGORYID] [int] IDENTITY(1,1) NOT NULL,
	[CATEGORYNAME] [nvarchar](50) NOT NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[ALLOWOPENITEMS] [bit] NULL,
 CONSTRAINT [PK_TBLCATEGORY] PRIMARY KEY CLUSTERED 
(
	[CATEGORYID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLDEALER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLDEALER](
	[DEALERID] [int] IDENTITY(1,1) NOT NULL,
	[DEALERNAME] [varchar](100) NULL,
	[PHONENO] [varchar](20) NULL,
	[ADDRESS] [varchar](500) NULL,
	[GSTIN] [varchar](20) NULL,
	[PANNUMBER] [varchar](20) NULL,
	[EMAILID] [varchar](50) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [date] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [date] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [date] NULL,
	[STATEID] [int] NULL,
 CONSTRAINT [PK_TBLDEALER] PRIMARY KEY CLUSTERED 
(
	[DEALERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UC_D_DEALERNAME] UNIQUE NONCLUSTERED 
(
	[DEALERNAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLMOP]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLMOP](
	[MOPID] [int] IDENTITY(1,1) NOT NULL,
	[MOPNAME] [nvarchar](50) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
 CONSTRAINT [PK_TBLMOP] PRIMARY KEY CLUSTERED 
(
	[MOPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLROLE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLROLE](
	[ROLEID] [int] IDENTITY(1,1) NOT NULL,
	[ROLENAME] [nvarchar](50) NOT NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [int] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [int] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [int] NULL,
 CONSTRAINT [PK_TBLROLE] PRIMARY KEY CLUSTERED 
(
	[ROLEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLSTATE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLSTATE](
	[STATEID] [int] IDENTITY(1,1) NOT NULL,
	[STATENAME] [varchar](50) NULL,
 CONSTRAINT [PK_TBLSTATE] PRIMARY KEY CLUSTERED 
(
	[STATEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLUSER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLUSER](
	[USERID] [int] IDENTITY(1,1) NOT NULL,
	[ROLEID] [int] NOT NULL,
	[REPORTINGLEADID] [int] NULL,
	[CATEGORYID] [int] NULL,
	[BRANCHID] [int] NULL,
	[USERNAME] [nvarchar](50) NOT NULL,
	[PASSWORDSTRING] [nvarchar](100) NOT NULL,
	[FULLNAME] [nvarchar](50) NOT NULL,
	[CNUMBER] [nvarchar](50) NULL,
	[EMAIL] [nvarchar](50) NULL,
	[ISOTP] [bit] NULL,
	[GENDER] [int] NULL,
	[DOB] [date] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
 CONSTRAINT [PK_TBLUSER] PRIMARY KEY CLUSTERED 
(
	[USERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UOM]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UOM](
	[UOMID] [int] IDENTITY(1,1) NOT NULL,
	[DISPLAYVALUE] [varchar](20) NOT NULL,
	[BASEUOMID] [int] NULL,
	[MULTIPLIER] [decimal](5, 2) NOT NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UOMID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ITEM] ADD  DEFAULT ((0)) FOR [ISOPENITEM]
GO
ALTER TABLE [dbo].[BRANCH]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[BRANCH]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[BRANCH]  WITH CHECK ADD FOREIGN KEY([UPDATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ENTITYSYNCSTATUS]  WITH CHECK ADD FOREIGN KEY([BRANCHID])
REFERENCES [dbo].[BRANCH] ([BRANCHID])
GO
ALTER TABLE [dbo].[ENTITYSYNCSTATUS]  WITH CHECK ADD FOREIGN KEY([ENTITYID])
REFERENCES [dbo].[ENTITY] ([ENTITYID])
GO
ALTER TABLE [dbo].[GSTDETAIL]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[GSTDETAIL]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[GSTDETAIL]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[GSTDETAIL]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[GSTDETAIL]  WITH CHECK ADD FOREIGN KEY([UPDATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[GSTDETAIL]  WITH CHECK ADD FOREIGN KEY([UPDATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEM]  WITH CHECK ADD FOREIGN KEY([CATEGORYID])
REFERENCES [dbo].[TBLCATEGORY] ([CATEGORYID])
GO
ALTER TABLE [dbo].[ITEM]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEM]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEM]  WITH CHECK ADD FOREIGN KEY([PARENTITEMID])
REFERENCES [dbo].[ITEM] ([ITEMID])
GO
ALTER TABLE [dbo].[ITEM]  WITH CHECK ADD FOREIGN KEY([SUBCATEGORYID])
REFERENCES [dbo].[SUBCATEGORY] ([SUBCATEGORYID])
GO
ALTER TABLE [dbo].[ITEM]  WITH CHECK ADD FOREIGN KEY([UOMID])
REFERENCES [dbo].[UOM] ([UOMID])
GO
ALTER TABLE [dbo].[ITEM]  WITH CHECK ADD FOREIGN KEY([UPDATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEMCODE]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEMCODE]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEMCODE]  WITH CHECK ADD FOREIGN KEY([FREEITEMCODEID])
REFERENCES [dbo].[ITEMCODE] ([ITEMCODEID])
GO
ALTER TABLE [dbo].[ITEMCODE]  WITH CHECK ADD FOREIGN KEY([ITEMID])
REFERENCES [dbo].[ITEM] ([ITEMID])
GO
ALTER TABLE [dbo].[ITEMCODE]  WITH CHECK ADD FOREIGN KEY([UPDATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEMGROUP]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEMGROUP]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEMGROUP]  WITH CHECK ADD FOREIGN KEY([UPDATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEMGROUPDETAIL]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEMGROUPDETAIL]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEMGROUPDETAIL]  WITH CHECK ADD FOREIGN KEY([ITEMCODEID])
REFERENCES [dbo].[ITEMCODE] ([ITEMCODEID])
GO
ALTER TABLE [dbo].[ITEMGROUPDETAIL]  WITH CHECK ADD FOREIGN KEY([ITEMGROUPID])
REFERENCES [dbo].[ITEMGROUP] ([ITEMGROUPID])
GO
ALTER TABLE [dbo].[ITEMPRICE]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEMPRICE]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[ITEMPRICE]  WITH CHECK ADD FOREIGN KEY([GSTID])
REFERENCES [dbo].[GSTDETAIL] ([GSTID])
GO
ALTER TABLE [dbo].[ITEMPRICE]  WITH CHECK ADD FOREIGN KEY([ITEMCODEID])
REFERENCES [dbo].[ITEMCODE] ([ITEMCODEID])
GO
ALTER TABLE [dbo].[ITEMPRICE]  WITH CHECK ADD FOREIGN KEY([UPDATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[OFFER]  WITH CHECK ADD FOREIGN KEY([CATEGORYID])
REFERENCES [dbo].[TBLCATEGORY] ([CATEGORYID])
GO
ALTER TABLE [dbo].[OFFER]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[OFFER]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[OFFER]  WITH CHECK ADD FOREIGN KEY([ITEMGROUPID])
REFERENCES [dbo].[ITEMGROUP] ([ITEMGROUPID])
GO
ALTER TABLE [dbo].[OFFER]  WITH CHECK ADD FOREIGN KEY([OFFERTYPEID])
REFERENCES [dbo].[OFFERTYPE] ([OFFERTYPEID])
GO
ALTER TABLE [dbo].[OFFER]  WITH CHECK ADD FOREIGN KEY([UPDATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[OFFERBRANCH]  WITH CHECK ADD FOREIGN KEY([BRANCHID])
REFERENCES [dbo].[BRANCH] ([BRANCHID])
GO
ALTER TABLE [dbo].[OFFERBRANCH]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[OFFERBRANCH]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[OFFERBRANCH]  WITH CHECK ADD FOREIGN KEY([OFFERID])
REFERENCES [dbo].[OFFER] ([OFFERID])
GO
ALTER TABLE [dbo].[OFFERITEMMAP]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[OFFERITEMMAP]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[OFFERITEMMAP]  WITH CHECK ADD FOREIGN KEY([ITEMCODEID])
REFERENCES [dbo].[ITEMCODE] ([ITEMCODEID])
GO
ALTER TABLE [dbo].[OFFERITEMMAP]  WITH CHECK ADD FOREIGN KEY([OFFERID])
REFERENCES [dbo].[OFFER] ([OFFERID])
GO
ALTER TABLE [dbo].[STOCKDISPATCH]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[STOCKDISPATCH]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[STOCKDISPATCH]  WITH CHECK ADD FOREIGN KEY([FROMBRANCHID])
REFERENCES [dbo].[BRANCH] ([BRANCHID])
GO
ALTER TABLE [dbo].[STOCKDISPATCH]  WITH CHECK ADD FOREIGN KEY([FROMBRANCHID])
REFERENCES [dbo].[BRANCH] ([BRANCHID])
GO
ALTER TABLE [dbo].[STOCKDISPATCH]  WITH CHECK ADD FOREIGN KEY([TOBRANCHID])
REFERENCES [dbo].[BRANCH] ([BRANCHID])
GO
ALTER TABLE [dbo].[STOCKDISPATCH]  WITH CHECK ADD FOREIGN KEY([TOBRANCHID])
REFERENCES [dbo].[BRANCH] ([BRANCHID])
GO
ALTER TABLE [dbo].[STOCKDISPATCH]  WITH CHECK ADD FOREIGN KEY([UPDATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[STOCKDISPATCHDETAIL]  WITH CHECK ADD FOREIGN KEY([ITEMPRICEID])
REFERENCES [dbo].[ITEMPRICE] ([ITEMPRICEID])
GO
ALTER TABLE [dbo].[STOCKDISPATCHDETAIL]  WITH CHECK ADD FOREIGN KEY([STOCKDISPATCHID])
REFERENCES [dbo].[STOCKDISPATCH] ([STOCKDISPATCHID])
GO
ALTER TABLE [dbo].[STOCKENTRY]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[STOCKENTRY]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[STOCKENTRY]  WITH CHECK ADD FOREIGN KEY([SUPPLIERID])
REFERENCES [dbo].[TBLDEALER] ([DEALERID])
GO
ALTER TABLE [dbo].[STOCKENTRY]  WITH CHECK ADD FOREIGN KEY([UPDATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[STOCKENTRYDETAIL]  WITH CHECK ADD FOREIGN KEY([CREATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[STOCKENTRYDETAIL]  WITH CHECK ADD FOREIGN KEY([DELETEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[STOCKENTRYDETAIL]  WITH CHECK ADD FOREIGN KEY([STOCKENTRYID])
REFERENCES [dbo].[STOCKENTRY] ([STOCKENTRYID])
GO
ALTER TABLE [dbo].[STOCKENTRYDETAIL]  WITH CHECK ADD FOREIGN KEY([UPDATEDBY])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[STOCKSUMMARY]  WITH CHECK ADD FOREIGN KEY([BRANCHID])
REFERENCES [dbo].[BRANCH] ([BRANCHID])
GO
ALTER TABLE [dbo].[STOCKSUMMARY]  WITH CHECK ADD FOREIGN KEY([ITEMPRICEID])
REFERENCES [dbo].[ITEMPRICE] ([ITEMPRICEID])
GO
ALTER TABLE [dbo].[TBLUSER]  WITH CHECK ADD FOREIGN KEY([REPORTINGLEADID])
REFERENCES [dbo].[TBLUSER] ([USERID])
GO
ALTER TABLE [dbo].[TBLUSER]  WITH CHECK ADD FOREIGN KEY([ROLEID])
REFERENCES [dbo].[TBLROLE] ([ROLEID])
GO
ALTER TABLE [dbo].[UOM]  WITH CHECK ADD FOREIGN KEY([BASEUOMID])
REFERENCES [dbo].[UOM] ([UOMID])
GO
ALTER TABLE [dbo].[UOM]  WITH CHECK ADD FOREIGN KEY([BASEUOMID])
REFERENCES [dbo].[UOM] ([UOMID])
GO
ALTER TABLE [dbo].[UOM]  WITH CHECK ADD FOREIGN KEY([BASEUOMID])
REFERENCES [dbo].[UOM] ([UOMID])
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_BRANCH]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_BRANCH]        
@BRANCHID int = 0,        
@BRANCHNAME varchar(100),        
@BRANCHCODE varchar(5),        
@ADDRESS varchar(500),        
@STATEID INT,
@PHONENO varchar(20),        
@LANDLINE VARCHAR(20),  
@EMAILID varchar(50),        
@SUPERVISORID INT,  
@ISWAREHOUSE BIT,  
@USERID int = 0  
AS        
BEGIN        
        
 IF @BRANCHID > 0         
 BEGIN        
  UPDATE BRANCH         
  SET      
   BRANCHNAME = @BRANCHNAME,  
   BRANCHCODE = @BRANCHCODE,  
   ADDRESS = @ADDRESS,  
   STATEID = @STATEID,
   PHONENO = @PHONENO,  
   LANDLINE = @LANDLINE,  
   EMAILID = @EMAILID,  
   ISWAREHOUSE = @ISWAREHOUSE,  
   SUPERVISORID = @SUPERVISORID,  
   UPDATEDBY = @USERID,  
   UPDATEDATE = GETDATE()  
  WHERE BRANCHID = @BRANCHID        
 END        
 ELSE        
 BEGIN        
  INSERT INTO BRANCH (BRANCHNAME, BRANCHCODE, ADDRESS, STATEID,
  PHONENO,LANDLINE, EMAILID, ISWAREHOUSE,SUPERVISORID,   
  CREATEDBY, CREATEDDATE)        
  VALUES (@BRANCHNAME, @BRANCHCODE, @ADDRESS,@STATEID,
  @PHONENO,@LANDLINE, @EMAILID,@ISWAREHOUSE,@SUPERVISORID,   
  @USERID, GETDATE())        
        
  SET @BRANCHID = SCOPE_IDENTITY()        
 END        
        
 SELECT @BRANCHID        
        
END     
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_BRANCHCOUNTER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_CU_BRANCHCOUNTER]  
@COUNTERID INT = 0,  
@BRANCHID int,  
@COUNTERNAME varchar(100),  
@USERID int = 0      
AS  
BEGIN  
      
 IF @COUNTERID > 0       
 BEGIN      
  UPDATE BRANCHCOUNTER  
  SET    
   BRANCHID = @BRANCHID,      
   COUNTERNAME = @COUNTERNAME,      
   UPDATEDBY = @USERID,      
   UPDATEDDATE = GETDATE()      
  WHERE COUNTERID = @COUNTERID  
 END      
 ELSE      
 BEGIN      
  INSERT INTO BRANCHCOUNTER(BRANCHID, COUNTERNAME,CREATEDBY,CREATEDDATE)      
  VALUES (@BRANCHID,@COUNTERNAME,@USERID, GETDATE())      
      
  SET @COUNTERID = SCOPE_IDENTITY()      
 END      
      
 SELECT @COUNTERID  
      
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_CATEGORY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_CATEGORY]  
@CATEGORYID int = 0,  
@CATEGORYNAME varchar(100),  
@ALLOWOPENITEMS BIT,
@USERID varchar(100) = 0  
AS  
BEGIN  
  
 IF @CATEGORYID > 0   
 BEGIN  
  UPDATE TBLCATEGORY   
  SET
   CATEGORYNAME = @CATEGORYNAME,
   ALLOWOPENITEMS = @ALLOWOPENITEMS,
   UPDATEDBY = @USERID,
   UPDATEDDATE = GETDATE()
   WHERE CATEGORYID = @CATEGORYID
 END  
 ELSE  
 BEGIN  
  INSERT INTO TBLCATEGORY (CATEGORYNAME, ALLOWOPENITEMS,CREATEDBY, CREATEDDATE)  
  VALUES (@CATEGORYNAME, @ALLOWOPENITEMS,@USERID,GETDATE())  
  
  SET @CATEGORYID = SCOPE_IDENTITY()  
 END  
  
 SELECT @CATEGORYID  
  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_COUNTER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_COUNTER]  
@COUNTERID INT = 0,  
@BRANCHID int,  
@COUNTERNAME varchar(100),  
@USERID int = 0      
AS  
BEGIN  
      
 IF @COUNTERID > 0       
 BEGIN      
  UPDATE TBLCOUNTER  
  SET    
   BRANCHID = @BRANCHID,      
   COUNTERNAME = @COUNTERNAME,      
   UPDATEDBY = @USERID,      
   UPDATEDDATE = GETDATE()      
  WHERE COUNTERID = @COUNTERID  
 END      
 ELSE      
 BEGIN      
  INSERT INTO TBLCOUNTER(BRANCHID, COUNTERNAME,CREATEDBY,CREATEDDATE)      
  VALUES (@BRANCHID,@COUNTERNAME,@USERID, GETDATE())      
      
  SET @COUNTERID = SCOPE_IDENTITY()      
 END      
      
 SELECT @COUNTERID  
      
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_DEALER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_DEALER]          
@DEALERID int = 0,          
@DEALERNAME varchar(100),          
@ADDRESS varchar(500),          
@STATEID INT,
@PHONENO varchar(20),  
@GSTIN varchar(20),          
@PANNUMBER varchar(20),          
@EMAILID varchar(50),          
@USERID int = 0          
AS          
BEGIN          
          
 IF @DEALERID > 0           
 BEGIN          
  UPDATE TBLDealer      
  SET        
    DEALERNAME = @DEALERNAME,          
   ADDRESS = @ADDRESS,          
   STATEID = @STATEID,
   PHONENO = @PHONENO,          
   EMAILID = @EMAILID,          
   GSTIN = @GSTIN,      
   PANNUMBER = @PANNUMBER,      
   UPDATEDBY = @USERID,          
   UPDATEDDATE = GETDATE()          
  WHERE DEALERID = @DEALERID      
 END          
 ELSE          
 BEGIN          
  INSERT INTO TBLDealer(DEALERNAME,ADDRESS,STATEID,PHONENO,GSTIN,    
  PANNUMBER,EMAILID,CREATEDBY, CREATEDDATE)          
  VALUES (@DEALERNAME,@ADDRESS,@STATEID,@PHONENO,@GSTIN,    
  @PANNUMBER,@EMAILID,@USERID, GETDATE())          
          
  SET @DEALERID = SCOPE_IDENTITY()          
 END          
          
 SELECT @DEALERID          
          
END   
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_DISPATCHDC]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_DISPATCHDC]          
@DISPATCHDCID INT = 0,          
@CATEGORYID INT,          
@BRANCHID INT,          
@USERID INT          
AS          
BEGIN          
      
IF NOT EXISTS(SELECT 1 FROM STOCKDISPATCH SD           
WHERE NOT EXISTS (SELECT 1 FROM DISPATCHDCMAPPING DDM           
WHERE SD.STOCKDISPATCHID = DDM.STOCKDISPATCHID)  
AND SD.TOBRANCHID = @BRANCHID           
AND SD.CATEGORYID = @CATEGORYID)  
RETURN;      
    
DECLARE @FROMBRANCHID INT    
SELECT TOP(1) @FROMBRANCHID = BRANCHID FROM BRANCH WHERE ISWAREHOUSE = 1    
    
INSERT INTO DISPATCHDC(FROMBRANCHID,BRANCHID,CATEGORYID,CREATEDBY,CREATEDDATE)    
SELECT @FROMBRANCHID,@BRANCHID,@CATEGORYID,@USERID,GETDATE()    
SET @DISPATCHDCID = SCOPE_IDENTITY()    
    
DECLARE @DISPATCHDCNUMBER VARCHAR(40)          
SELECT @DISPATCHDCNUMBER = 'DC' + BRANCHCODE + REPLACE(CONVERT(VARCHAR(10), GETDATE(),111),'/','')             
+ REPLACE(convert(varchar, getdate(), 108),':','') + CAST(@DISPATCHDCID AS VARCHAR(10))            
FROM BRANCH WHERE BRANCHID = @BRANCHID          
          
UPDATE DISPATCHDC SET DISPATCHDCNUMBER = @DISPATCHDCNUMBER          
WHERE DISPATCHDCID = @DISPATCHDCID          
          
INSERT INTO DISPATCHDCMAPPING(STOCKDISPATCHID,DISPATCHDCID)          
SELECT SD.STOCKDISPATCHID,@DISPATCHDCID FROM STOCKDISPATCH SD           
WHERE NOT EXISTS (SELECT 1 FROM DISPATCHDCMAPPING DDM           
WHERE SD.STOCKDISPATCHID = DDM.STOCKDISPATCHID AND SD.TOBRANCHID = @BRANCHID           
AND SD.CATEGORYID = @CATEGORYID)          
        
EXEC USP_R_STOCKDISPATCHDC @DISPATCHDCID        
          
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_GST]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_GST]        
@GSTID int = 0,        
@GSTCODE varchar(100),        
@CGST DECIMAL(5,2),  
@SGST DECIMAL(5,2),  
@IGST DECIMAL(5,2),  
@CESS DECIMAL(5,2),  
@USERID int = 0        
AS        
BEGIN        
        
 IF @GSTID > 0    
 BEGIN        
  UPDATE GSTDETAIL    
  SET      
   GSTCODE = @GSTCODE,    
   CGST = @CGST,    
   SGST = @SGST,    
   IGST = @IGST,    
   CESS = @CESS,    
   UPDATEDBY = @USERID,        
   UPDATEDATE = GETDATE()        
  WHERE GSTID = @GSTID    
 END        
 ELSE        
 BEGIN        
  INSERT INTO GSTDETAIL (GSTCODE, CGST, SGST, IGST,CESS, CREATEDBY, CREATEDDATE)        
  VALUES (@GSTCODE, @CGST, @SGST, @IGST, @CESS,@USERID, GETDATE())        
        
  SET @GSTID = SCOPE_IDENTITY()        
 END        
        
 SELECT @GSTID    
        
END   
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMCODE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_ITEMCODE]  
@ItemCodeID INT = 0  
, @ItemID INT = 0  
, @ItemName VARCHAR(100)  
, @ItemCode VARCHAR(20)  
, @Description VARCHAR(500) = NULL  
, @CategoryID INT  
, @SubCategoryID INT  = NULL
, @HSNCode VARCHAR(10) = NULL  
, @IsEAN BIT  
, @SKUCode VARCHAR(10)   
, @CostPriceWT DECIMAL(11, 4)  
, @CostPriceWOT DECIMAL(11, 4)  
, @SalePrice DECIMAL(10, 2)  
, @MRP DECIMAL(10, 2)  
, @GSTID INT  
, @UserID INT  
, @IsOpenItem BIT = 0  
, @ParentItemID INT = NULL  
, @UOMID INT = NULL  
, @FreeItemCodeID INT = NULL  
AS  
BEGIN  
  
 --Pre-write validations  
 IF EXISTS (SELECT 1 FROM ITEM I WHERE I.SKUCODE = @SKUCode AND I.DELETEDDATE IS NULL AND I.ITEMID != @ItemID)  
 BEGIN  
  SELECT 'Item SKU code already exists'  
  RETURN  
 END  
  
 IF EXISTS (SELECT 1 FROM ITEMCODE IC WHERE IC.ITEMCODE = @ItemCode AND IC.DELETEDDATE IS NULL AND IC.ITEMCODEID != @ItemCodeID)  
 BEGIN  
  SELECT 'Item code already exists'  
  RETURN  
 END  
  
 -- End Validations  
  
 -- pre clean variable not to violate FK relations  
  
 IF @ParentItemID <= 0  
 BEGIN  
  SET @ParentItemID = NULL  
 END  
   
 IF @UOMID <= 0  
 BEGIN  
  SET @UOMID = NULL  
 END  
   
 IF @FreeItemCodeID <= 0  
 BEGIN  
  SET @FreeItemCodeID = NULL  
 END  
  
 -- end pre clean  
  
 IF @ItemID > 0  
 BEGIN  
  UPDATE ITEM  
  SET  
   SKUCODE = @SKUCode  
   , ITEMNAME = @ItemName  
   , DESCRIPTION = @Description  
   , CATEGORYID = @CategoryID  
   , UPDATEDBY = @UserID  
   , UPDATEDATE = GETDATE()  
   , ISOPENITEM = @IsOpenItem  
   , PARENTITEMID = @ParentItemID  
   , SUBCATEGORYID = @SubCategoryID  
   , UOMID = @UOMID  
  WHERE ITEMID = @ItemID  
 END  
 ELSE  
 BEGIN  
  INSERT INTO ITEM(SKUCODE, ITEMNAME, DESCRIPTION, CATEGORYID, 
  CREATEDBY, CREATEDDATE, ISOPENITEM, SUBCATEGORYID, PARENTITEMID, UOMID)  
  SELECT @SKUCode, @ItemName, @Description, @CategoryID, 
  @UserID, GETDATE(), @IsOpenItem, @SubCategoryID, @ParentItemID, @UOMID  
  
  SET @ItemID = SCOPE_IDENTITY()  
  
  --IF @ParentItemID > 0  
  --BEGIN  
  -- UPDATE ITEM SET PARENTITEMID = @ParentItemID WHERE ITEMID = @ItemID  
  --END  
  --IF @UOMID > 0  
  --BEGIN  
  -- UPDATE ITEM SET UOMID = @UOMID WHERE ITEMID = @ItemID  
  --END  
 END  
  
 -- Store Item Code  
 IF @ItemCodeID > 0  
 BEGIN  
  UPDATE ITEMCODE  
  SET  
   ITEMCODE = @ItemCode  
   , ISEAN = @IsEAN  
   , HSNCODE = @HSNCode  
   , UPDATEDBY = @UserID  
   , UPDATEDATE = GETDATE()  
   , FREEITEMCODEID = @FreeItemCodeID  
  WHERE ITEMCODEID = @ItemCodeID  
 END  
 ELSE  
 BEGIN  
  INSERT INTO ITEMCODE(ITEMCODE, ITEMID, HSNCODE, ISEAN, 
  CREATEDBY, CREATEDDATE, FREEITEMCODEID)  
  SELECT @ItemCode, @ItemID, @HSNCode, @IsEAN, 
  @UserID, GETDATE(), @FreeItemCodeID  
  
  SET @ItemCodeID = SCOPE_IDENTITY()  
 END  
   
 -- Store Item Price  
 DECLARE @ITEMPRICEID INT = 0
 
 SELECT @ITEMPRICEID = ITEMPRICEID FROM ITEMPRICE IP WHERE   
  IP.ITEMCODEID = @ItemCodeID AND IP.SALEPRICE = @SalePrice AND IP.MRP = @MRP 
  AND IP.GSTID = @GSTID AND IP.DELETEDDATE IS NULL

 IF @ITEMPRICEID = 0  
 BEGIN  
  INSERT INTO ITEMPRICE(ITEMCODEID, SALEPRICE, MRP, GSTID, CREATEDBY, CREATEDDATE)  
  SELECT @ItemCodeID, @SalePrice, @MRP, @GSTID, @UserID, GETDATE()  
  SET @ITEMPRICEID = SCOPE_IDENTITY()
 END  

 --DECLARE @ZeroGSTID INT  
 --SELECT @ZeroGSTID = GSTID FROM GSTDETAIL WHERE CGST = 0 AND SGST = 0 AND IGST = 0 AND CESS = 0  
   
 --IF @FreeItemCodeID > 0 AND   
 -- NOT EXISTS  
 --  (SELECT 1 FROM ITEMPRICE IP WHERE   
 --   IP.ITEMCODEID = @FreeItemCodeID AND IP.COSTPRICEWT = 0 AND IP.COSTPRICEWOT = 0 AND   
 --   IP.SALEPRICE = 0 AND IP.MRP = 0 AND IP.GSTID = @ZeroGSTID AND IP.DELETEDDATE IS NULL)  
 --BEGIN   
 -- INSERT INTO ITEMPRICE(ITEMCODEID, COSTPRICEWT, COSTPRICEWOT, SALEPRICE, MRP, GSTID, CREATEDBY, CREATEDDATE)  
 -- SELECT @FreeItemCodeID, 0, 0, 0, 0, @ZeroGSTID, @UserID, GETDATE()  
 --END  
  
 SELECT CONCAT(@ItemCodeID, ',', @ItemID) AS ITEMANDCODEID  
  
END  
  
  
  
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMGROUP]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_ITEMGROUP]
@ItemGroupID INT,
@GroupName NVARCHAR(100),
@IsActive BIT,
@UserID INT
AS
BEGIN

IF(@ItemGroupID > 0)
BEGIN

UPDATE ITEMGROUP SET GROUPNAME = @GroupName,
ISACTIVE = @IsActive,UPDATEDBY = @UserID,UPDATEDDATE = GETDATE()
WHERE ITEMGROUPID = @ItemGroupID

END
ELSE
BEGIN

insert into ITEMGROUP(GROUPNAME,ISACTIVE,CREATEDBY,CREATEDDATE)
select @GroupName,@IsActive,@UserID,GETDATE()

set @ItemGroupID = SCOPE_IDENTITY()

END

select @ItemGroupID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMGROUPDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[USP_CU_ITEMGROUPDETAIL]
@ItemGroupID int,
@ItemCodeID int,
@UserID int
AS
BEGIN

insert into ITEMGROUPDETAIL(ITEMGROUPID,ITEMCODEID,CREATEDBY,CREATEDDATE)
select @ItemGroupID,@ItemCodeID,@UserID,GETDATE()

select SCOPE_IDENTITY() as ITEMGROUPDETAILID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_MOP]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_MOP]
@MOPID int = 0,    
@MOPNAME varchar(100),    
@USERID varchar(100) = 0    
AS    
BEGIN    
    
 IF @MOPID > 0     
 BEGIN    
  UPDATE TBLMOP    
  SET  
   MOPNAME = @MOPNAME,  
   UPDATEDBY = @USERID,  
   UPDATEDDATE = GETDATE()  
   WHERE MOPID = @MOPID
 END    
 ELSE    
 BEGIN    
  INSERT INTO TBLMOP (MOPNAME,CREATEDBY, CREATEDDATE)    
  VALUES (@MOPNAME,@USERID,GETDATE())    
    
  SET @MOPID = SCOPE_IDENTITY()    
 END    
    
 SELECT @MOPID    
    
END    
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_OFFER]            
@OfferID int = 0,            
@OfferName nvarchar(100),            
@OfferCode nvarchar(10),            
@StartDate datetime,            
@EndDate datetime,            
@OfferValue decimal(10,2) = null,
@OfferTypeID int,            
@CategoryID int = null,            
@ItemGroupID int = null,           
@IsActive bit,          
@AppliesToID int,      
@UserID int            
AS            
BEGIN            
            
IF(@OfferID <= 0)            
begin            
            
insert into OFFER(OFFERNAME,OFFERCODE,STARTDATE,      
ENDDATE,OFFERVALUE,OFFERTYPEID,CATEGORYID,ITEMGROUPID,      
CREATEDBY,CREATEDDATE,ISACTIVE,APPLIESTOID)            
select @OfferName,@OfferCode,@StartDate,      
@EndDate,@OfferValue,@OfferTypeID,@CategoryID,@ItemGroupID,      
@UserID,GETDATE(),@IsActive,@AppliesToID
            
set @OfferID = SCOPE_IDENTITY()            
            
end            
else             
begin            
            
update OFFER set OFFERNAME = @OfferName,OFFERCODE = @OfferCode,            
STARTDATE = @StartDate,ENDDATE = @EndDate,OFFERVALUE = @OfferValue,
OFFERTYPEID = @OfferTypeID,CATEGORYID = @CategoryID,ITEMGROUPID = @ItemGroupID,            
UPDATEDBY = @UserID,UPDATEDDATE = GETDATE(),ISACTIVE = @IsActive,APPLIESTOID = @AppliesToID 
where OFFERID = @OfferID            
            
end            
            
select @OfferID            
            
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFERBRANCH]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_OFFERBRANCH]
@OfferID int,
@BranchID int,
@UserID int
AS
BEGIN

INSERT INTO OFFERBRANCH(OFFERID,BRANCHID,CREATEDBY,CREATEDDATE)
SELECT @OfferID,@BranchID,@UserID,GETDATE()

select SCOPE_IDENTITY() AS OFFERBRANCHID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFERITEM]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_OFFERITEM]
@OfferID int =0,
@ItemCodeID int,
@UserID int

AS
BEGIN

INSERT INTO OFFERITEMMAP(OFFERID,ITEMCODEID,CREATEDBY,CREATEDDATE)
SELECT @OfferID,@ItemCodeID,@UserID,GETDATE()

SELECT SCOPE_IDENTITY() AS OFFERITEMMAPID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BILL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_POS_BILL]  
(  
 @Bills POS_BILLTYPE READONLY  
)  
AS  
BEGIN  
 UPDATE B  
 SET  
  B.BILLNUMBER = UB.BILLNUMBER  
  , B.CREATEDBY = UB.CREATEDBY  
  , B.CREATEDDATE = UB.CREATEDDATE  
  , B.UPDATEDBY = UB.UPDATEDBY  
  , B.UPDATEDDATE = UB.UPDATEDDATE  
  , B.DELETEDBY = UB.DELETEDBY  
  , B.DELETEDDATE = UB.DELETEDDATE  
  , B.BILLSTATUS = UB.BILLSTATUS  
  , B.CUSTOMERNUMBER = UB.CUSTOMERNUMBER  
  , B.CUSTOMERNAME = UB.CUSTOMERNAME  
  ,B.DAYCLOSUREID = UB.DAYCLOSUREID
 FROM   
  POS_BILL B  
  INNER JOIN @Bills UB ON UB.BILLID = B.BILLID  
 WHERE  
  B.BRANCHCOUNTERID = UB.BRANCHCOUNTERID  
  
 INSERT INTO POS_BILL(BILLID, BRANCHCOUNTERID, BILLNUMBER, CREATEDBY, CREATEDDATE, 
 UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID)  
 SELECT BILLID, BRANCHCOUNTERID, BILLNUMBER, CREATEDBY, CREATEDDATE, 
 UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID
 FROM @Bills UB  
 WHERE NOT EXISTS  
  (  
   SELECT 1 FROM POS_BILL BINNER WHERE BINNER.BILLID = UB.BILLID AND BINNER.BRANCHCOUNTERID = UB.BRANCHCOUNTERID  
  )  
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BILLDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_POS_BILLDETAIL]      
(      
 @BillDetails POS_BILLDETAILTYPE READONLY      
)      
AS      
BEGIN      
 UPDATE BD      
 SET      
  BD.ITEMPRICEID   = UBD.ITEMPRICEID      
  , BD.QUANTITY   = UBD.QUANTITY      
  , BD.WEIGHTINKGS  = UBD.WEIGHTINKGS      
  , BD.BILLEDAMOUNT  = UBD.BILLEDAMOUNT      
  , BD.CREATEDDATE  = UBD.CREATEDDATE      
  , BD.UPDATEDDATE  = UBD.UPDATEDDATE      
  , BD.DELETEDDATE  = UBD.DELETEDDATE      
  , BD.CGST    = UBD.CGST      
  , BD.SGST    = UBD.SGST      
  , BD.IGST    = UBD.IGST      
  , BD.CESS    = UBD.CESS      
  , BD.GSTVALUE   = UBD.GSTVALUE      
  , BD.GSTID    = UBD.GSTID      
  , BD.SNO    = UBD.SNO      
  , BD.DISCOUNT   = UBD.DISCOUNT      
  ,BD.OFFERID = UBD.OFFERID 
  ,BD.DAYCLOSUREID = UBD.DAYCLOSUREID
 FROM       
  POS_BILLDETAIL BD      
  INNER JOIN @BillDetails UBD ON UBD.BILLDETAILID = BD.BILLDETAILID      
 WHERE      
  BD.BRANCHCOUNTERID = UBD.BRANCHCOUNTERID      
      
 INSERT INTO POS_BILLDETAIL(BILLDETAILID, BILLID, BRANCHCOUNTERID, ITEMPRICEID, QUANTITY, WEIGHTINKGS,     
 BILLEDAMOUNT, CREATEDDATE, UPDATEDDATE, DELETEDDATE      
  , CGST, SGST, IGST, CESS, GSTVALUE, GSTID, SNO, DISCOUNT,OFFERID,DAYCLOSUREID)      
 SELECT BILLDETAILID, BILLID, BRANCHCOUNTERID, ITEMPRICEID, QUANTITY, WEIGHTINKGS, BILLEDAMOUNT,     
 CREATEDDATE, UPDATEDDATE, DELETEDDATE      
  , CGST, SGST, IGST, CESS, GSTVALUE, GSTID, SNO, DISCOUNT,OFFERID  ,DAYCLOSUREID
 FROM @BillDetails UBD      
 WHERE NOT EXISTS      
  (      
   SELECT 1 FROM POS_BILLDETAIL BDINNER WHERE BDINNER.BILLDETAILID = UBD.BILLDETAILID AND BDINNER.BRANCHCOUNTERID = UBD.BRANCHCOUNTERID      
  )      
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_PRINTERSETTINGS]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_PRINTERSETTINGS]    
@PRINTERSETTINGSID INT  =0,    
@PRINTERTYPEID INT,    
@PRINTERNAME VARCHAR(500),    
@USERID INT    
AS    
BEGIN    
    
IF @PRINTERSETTINGSID > 0    
UPDATE PRINTERSETTINGS SET PRINTERTYPEID = @PRINTERTYPEID,    
PRINTERNAME = @PRINTERNAME    
WHERE PRINTERSETTINGSID = @PRINTERSETTINGSID    
ELSE  
BEGIN
IF NOT EXISTS(SELECT 1 FROM PRINTERSETTINGS WHERE PRINTERTYPEID = @PRINTERTYPEID AND USERID = @USERID)
BEGIN
INSERT INTO PRINTERSETTINGS(PRINTERTYPEID,PRINTERNAME,USERID)    
VALUES(@PRINTERTYPEID,@PRINTERNAME,@USERID)    
SET @PRINTERSETTINGSID = SCOPE_IDENTITY()    
END
ELSE
BEGIN

UPDATE PRINTERSETTINGS SET PRINTERTYPEID = @PRINTERTYPEID,    
PRINTERNAME = @PRINTERNAME    
WHERE PRINTERTYPEID = @PRINTERTYPEID AND USERID = @USERID

END
END
    
SELECT @PRINTERSETTINGSID    
    
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_STOCKDISPATCH]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_STOCKDISPATCH]        
@STOCKDISPATCHID INT = 0,        
@FROMBRANCHID INT,        
@TOBRANCHID INT,        
@CATEGORYID INT,      
@USERID INT        
AS        
BEGIN        
  
INSERT INTO STOCKDISPATCH(FROMBRANCHID,TOBRANCHID,CATEGORYID,STATUS,  
CREATEDBY,CREATEDDATE)        
VALUES(@FROMBRANCHID,@TOBRANCHID,@CATEGORYID,0,  
@USERID,GETDATE())  
  
SELECT @STOCKDISPATCHID = SCOPE_IDENTITY()  
  
DECLARE @BRANCHCODE VARCHAR(20)  
SELECT @BRANCHCODE = BRANCHCODE FROM BRANCH WHERE BRANCHID = @TOBRANCHID  
  
UPDATE STOCKDISPATCH SET DISPATCHNUMBER = 
@BRANCHCODE + REPLACE(CONVERT(VARCHAR(10), CREATEDDATE,111),'/','')           
+ REPLACE(convert(varchar, CREATEDDATE, 108),':','')
+ CAST(STOCKDISPATCHID AS VARCHAR(10))
WHERE STOCKDISPATCHID = @STOCKDISPATCHID  

SELECT @STOCKDISPATCHID
  
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_STOCKDISPATCHDETAIL]          
@STOCKDISPATCHDETAILID INT = 0,          
@STOCKDISPATCHID INT,          
@ITEMPRICEID INT,          
@TRAYNUMBER INT,          
@DISPATCHQUANTITY int,          
@WEIGHTINKGS DECIMAL(10,2),          
@USERID INT          
AS          
BEGIN          
          
IF @STOCKDISPATCHDETAILID > 0          
BEGIN          
          
UPDATE STOCKDISPATCHDETAIL           
SET ITEMPRICEID = @ITEMPRICEID,          
TRAYNUMBER = @TRAYNUMBER,          
DISPATCHQUANTITY = @DISPATCHQUANTITY,          
RECEIVEDQUANTITY = @DISPATCHQUANTITY,        
WEIGHTINKGS = @WEIGHTINKGS,          
UPDATEDATE = GETDATE()          
WHERE STOCKDISPATCHDETAILID = @STOCKDISPATCHDETAILID          
          
END          
ELSE          
BEGIN          
    
SELECT @STOCKDISPATCHDETAILID = ISNULL(STOCKDISPATCHDETAILID,0) FROM STOCKDISPATCHDETAIL     
WHERE STOCKDISPATCHID = @STOCKDISPATCHID AND ITEMPRICEID = @ITEMPRICEID AND TRAYNUMBER = @TRAYNUMBER    
    
IF(@STOCKDISPATCHDETAILID  > 0)    
BEGIN    
  
UPDATE STOCKDISPATCHDETAIL SET   
DISPATCHQUANTITY = DISPATCHQUANTITY + @DISPATCHQUANTITY,  
RECEIVEDQUANTITY = DISPATCHQUANTITY + @DISPATCHQUANTITY,  
WEIGHTINKGS = WEIGHTINKGS + @WEIGHTINKGS  
WHERE STOCKDISPATCHDETAILID = @STOCKDISPATCHDETAILID   
  
END    
ELSE    
BEGIN    
INSERT INTO STOCKDISPATCHDETAIL(STOCKDISPATCHID,ITEMPRICEID,TRAYNUMBER,          
DISPATCHQUANTITY,RECEIVEDQUANTITY,WEIGHTINKGS,CREATEDDATE)          
VALUES(@STOCKDISPATCHID,@ITEMPRICEID,@TRAYNUMBER,          
@DISPATCHQUANTITY,@DISPATCHQUANTITY,@WEIGHTINKGS,GETDATE())          
SET @STOCKDISPATCHDETAILID = SCOPE_IDENTITY()          
END    
  
END          
SELECT @STOCKDISPATCHDETAILID          
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_STOCKENTRY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_STOCKENTRY]          
@STOCKENTRYID INT = 0,              
@SUPPLIERID INT,              
@SUPPLIERINVOICENO VARCHAR(12),              
@TAXINCLUSIVE BIT,          
@INVOICEDATE DATE,      
@TCS DECIMAL(10,2) = NULL,    
@DISCOUNTPER DECIMAL(10,2) = NULL,    
@DISCOUNT DECIMAL(10,2) = NULL,    
@EXPENSES DECIMAL(10,2) = NULL,    
@TRANSPORT DECIMAL(10,2) = NULL,    
@CATEGORYID INT,            
@USERID INT          
AS          
BEGIN          
      
INSERT INTO STOCKENTRY(SUPPLIERID,SUPPLIERINVOICENO,      
TAXINCLUSIVE,CATEGORYID,CREATEDBY,CREATEDDATE,STATUS,INVOICEDATE,  
TCS,DISCOUNTPER,DISCOUNT,EXPENSES,TRANSPORT)      
VALUES(@SUPPLIERID,@SUPPLIERINVOICENO,      
@TAXINCLUSIVE,@CATEGORYID,@USERID,GETDATE(),0,@INVOICEDATE,  
@TCS,@DISCOUNTPER,@DISCOUNT,@EXPENSES,@TRANSPORT)   
      
SELECT SCOPE_IDENTITY() AS STOCKENTRYID          
       
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_STOCKENTRYDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_STOCKENTRYDETAIL]                  
@STOCKENTRYDETAILID INT = 0,                            
@STOCKENTRYID INT,                
@ITEMCODEID INT,                
@COSTPRICEWT DECIMAL(11, 4),                  
@COSTPRICEWOT DECIMAL(11, 4),          
@MRP DECIMAL(10,2),                  
@SALEPRICE DECIMAL(10,2),                  
@QUANTITY INT,                  
@WEIGHTINKGS DECIMAL(10,2),                            
@USERID INT,          
@GSTID INT,          
@FREEQUANTITY INT = 0,          
@DISCOUNTFLAT DECIMAL(9, 2) = 0,          
@DISCOUNTPERCENTAGE DECIMAL(4, 2) = 0,          
@SCHEMEPERCENTAGE DECIMAL(4, 2) = 0,          
@SCHEMEFLAT DECIMAL(9, 2) = 0,          
@TOTALPRICEWT DECIMAL(11, 4) = 0,          
@TOTALPRICEWOT DECIMAL(11, 4) = 0,          
@APPLIEDDISCOUNT DECIMAL(9, 2) = 0,          
@APPLIEDSCHEME DECIMAL(9, 2) = 0,          
@APPLIEDDGST DECIMAL(9, 2) = 0,          
@FINALPRICE DECIMAL(9, 2),  
@CGST DECIMAL(10,2) = 0,  
@SGST DECIMAL(10,2) = 0,  
@IGST DECIMAL(10,2) = 0,  
@CESS DECIMAL(10,2) = 0  
AS                            
BEGIN                            

DECLARE @ITEMPRICEID INT,@ITEMCOSTPRICEID INT

SELECT
 @ITEMPRICEID = ITEMPRICEID
 FROM ITEMPRICE
 WHERE
  ITEMCODEID = @ITEMCODEID
  AND MRP = @MRP
  AND SALEPRICE = @SALEPRICE
  AND GSTID = @GSTID

 IF ISNULL(@ITEMPRICEID, 0) < 1          
 BEGIN          
  INSERT INTO ITEMPRICE(ITEMCODEID, SALEPRICE, MRP, GSTID, CREATEDBY, CREATEDDATE)          
  SELECT @ITEMCODEID, @SALEPRICE, @MRP, @GSTID, @USERID, GETDATE()         
          
  SELECT @ITEMPRICEID = SCOPE_IDENTITY()          
 END          
                            
SELECT @ITEMCOSTPRICEID = ITEMCOSTPRICEID FROM ITEMCOSTPRICE        
WHERE ITEMPRICEID = @ITEMPRICEID         
  AND COSTPRICEWT = @COSTPRICEWT           
  AND COSTPRICEWOT = @COSTPRICEWOT        
  AND GSTID = @GSTID          
        
  IF ISNULL(@ITEMCOSTPRICEID , 0) < 1        
  BEGIN        
        
   INSERT INTO ITEMCOSTPRICE(ITEMPRICEID,COSTPRICEWT,COSTPRICEWOT,QUANTITY,        
   WEIGHTINKGS,GSTID,CREATEDBY,CREATEDDATE)        
   SELECT @ITEMPRICEID,@COSTPRICEWT,@COSTPRICEWOT,@QUANTITY,        
   @WEIGHTINKGS,@GSTID,@USERID,GETDATE()        
 SET @ITEMCOSTPRICEID = SCOPE_IDENTITY()        
  END        
  ELSE        
  BEGIN        
        
   UPDATE ITEMCOSTPRICE SET QUANTITY = QUANTITY + @QUANTITY,        
   WEIGHTINKGS = WEIGHTINKGS + @WEIGHTINKGS        
   WHERE ITEMCOSTPRICEID = @ITEMCOSTPRICEID        
        
  END        


IF(@STOCKENTRYDETAILID > 0)
BEGIN

UPDATE STOCKENTRYDETAIL SET 
ITEMCOSTPRICEID = @ITEMCOSTPRICEID,
QUANTITY = @QUANTITY,
WEIGHTINKGS = @WEIGHTINKGS,
FREEQUANTITY = @FREEQUANTITY, 
DISCOUNTFLAT = @DISCOUNTFLAT, 
DISCOUNTPERCENTAGE = @DISCOUNTPERCENTAGE,    
SCHEMEPERCENTAGE = @SCHEMEPERCENTAGE, 
SCHEMEFLAT = @SCHEMEFLAT, 
TOTALPRICEWT = @TOTALPRICEWT, 
TOTALPRICEWOT = @TOTALPRICEWOT, 
APPLIEDDISCOUNT = @APPLIEDDISCOUNT,    
APPLIEDSCHEME = @APPLIEDSCHEME, 
APPLIEDDGST = @APPLIEDDGST, 
FINALPRICE = @FINALPRICE, 
FREEITEMCOSTPRICEID = @ITEMCOSTPRICEID,  
CGST = @CGST,
SGST = @SGST,
IGST = @IGST,
CESS  = CESS
WHERE STOCKENTRYDETAILID = @STOCKENTRYDETAILID

END
ELSE
BEGIN
 INSERT INTO STOCKENTRYDETAIL(STOCKENTRYID,ITEMCOSTPRICEID,QUANTITY,    
 WEIGHTINKGS,CREATEDBY,CREATEDDATE, FREEQUANTITY, DISCOUNTFLAT, DISCOUNTPERCENTAGE,    
 SCHEMEPERCENTAGE, SCHEMEFLAT, TOTALPRICEWT, TOTALPRICEWOT, APPLIEDDISCOUNT,    
 APPLIEDSCHEME, APPLIEDDGST, FINALPRICE, FREEITEMCOSTPRICEID,  
 CGST,SGST,IGST,CESS )    
 VALUES(@STOCKENTRYID,@ITEMCOSTPRICEID,@QUANTITY,    
 @WEIGHTINKGS,@USERID,GETDATE(), @FREEQUANTITY, @DISCOUNTFLAT, @DISCOUNTPERCENTAGE    
 , @SCHEMEPERCENTAGE, @SCHEMEFLAT, @TOTALPRICEWT, @TOTALPRICEWOT,    
 @APPLIEDDISCOUNT, @APPLIEDSCHEME, @APPLIEDDGST, @FINALPRICE, @ITEMCOSTPRICEID,  
 @CGST,@SGST,@IGST,@CESS)    
                
 SET @STOCKENTRYDETAILID = SCOPE_IDENTITY()                
 END
                
 SELECT @STOCKENTRYDETAILID, @ITEMPRICEID              
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_SUBCATEGORY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_SUBCATEGORY]
@SUBCATEGORYID INT = 0,    
@CATEGORYID int,    
@SUBCATEGORYNAME varchar(100),    
@USERID int = 0        
AS    
BEGIN    
        
 IF @SUBCATEGORYID > 0         
 BEGIN        
  UPDATE SUBCATEGORY
  SET      
   CATEGORYID = @CATEGORYID,        
   SUBCATEGORYNAME = @SUBCATEGORYNAME,        
   UPDATEDBY = @USERID,        
   UPDATEDDATE = GETDATE()        
  WHERE SUBCATEGORYID = @SUBCATEGORYID    
 END        
 ELSE        
 BEGIN        
  INSERT INTO SUBCATEGORY(CATEGORYID, SUBCATEGORYNAME,CREATEDBY,CREATEDDATE)        
  VALUES (@CATEGORYID,@SUBCATEGORYNAME,@USERID, GETDATE())        
        
  SET @SUBCATEGORYID = SCOPE_IDENTITY()        
 END        
        
 SELECT @SUBCATEGORYID    
        
END   
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_UOM]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_UOM]
@UOMID int = 0,    
@DISPLAYVALUE varchar(100),    
@BASEUOMID int,
@MULTIPLIER decimal(5,2),
@USERID int
AS    
BEGIN    
    
 IF @UOMID > 0     
 BEGIN    
  UPDATE UOM
  SET  
   DISPLAYVALUE = @DISPLAYVALUE,  
   BASEUOMID = @BASEUOMID,
   MULTIPLIER = @MULTIPLIER,
   UPDATEDBY = @USERID,  
   UPDATEDDATE = GETDATE()  
   WHERE UOMID = @UOMID 
 END    
 ELSE    
 BEGIN    
  INSERT INTO UOM(DISPLAYVALUE,BASEUOMID,MULTIPLIER,CREATEDBY, CREATEDDATE)    
  VALUES (@DISPLAYVALUE,@BASEUOMID,@MULTIPLIER,@USERID,GETDATE())    
    
  SET @UOMID = SCOPE_IDENTITY()    
 END    
    
 SELECT @UOMID    
    
END    
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_USER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CU_USER]              
@USERID int = 0,              
@ROLEID int = 0,              
@REPORTINGLEADID int = 0,              
@CATEGORYID int = 0,              
@BRANCHID int = 0,              
@USERNAME varchar(50),              
@PASSWORDSTRING varchar(100),              
@FULLNAME varchar(50),              
@EMAIL varchar(50),              
@CNUMBER VARCHAR(50),          
@GENDER INT,              
@DOB DATE = NULL,              
@CUSERID int = 0              
AS              
BEGIN              
              
 IF @USERID > 0               
 BEGIN              
  UPDATE TBLUSER               
  SET            
  ROLEID = @ROLEID,          
  REPORTINGLEADID = @REPORTINGLEADID,          
  CATEGORYID = @CATEGORYID,          
  BRANCHID = @BRANCHID,          
   FULLNAME = @FULLNAME,              
   CNUMBER = @CNUMBER,              
   EMAIL = @EMAIL,              
   GENDER = @GENDER,              
   DOB = @DOB,          
   UPDATEDBY = @CUSERID,              
   UPDATEDDATE = GETDATE()  
  WHERE USERID = @USERID              
 END              
 ELSE              
 BEGIN              
          
  INSERT INTO TBLUSER(ROLEID, REPORTINGLEADID,           
  CATEGORYID,BRANCHID, USERNAME, PASSWORDSTRING,           
  FULLNAME, CNUMBER, EMAIL,ISOTP,          
  GENDER,DOB,CREATEDBY,CREATEDDATE)              
  VALUES (@ROLEID, @REPORTINGLEADID,           
  @CATEGORYID,@BRANCHID, @USERNAME, @PASSWORDSTRING,           
  @FULLNAME, @CNUMBER, @EMAIL,1,          
  @GENDER,@DOB,@CUSERID,GETDATE())              
              
  SET @USERID = SCOPE_IDENTITY()              
 END              
              
 SELECT @USERID          
              
END   
GO
/****** Object:  StoredProcedure [dbo].[USP_D_BRANCH]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_D_BRANCH]  
@BRANCHID INT = 0,  
@USERID INT  
AS  
BEGIN  
  
UPDATE BRANCH SET DELETEDBY = @USERID,  
DELETEDDATE = GETDATE()  
WHERE BRANCHID = @BRANCHID  

SELECT @BRANCHID    

END
GO
/****** Object:  StoredProcedure [dbo].[USP_D_BRANCHCOUNTER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_D_BRANCHCOUNTER]
@COUNTERID INT = 0,    
@USERID INT    
AS    
BEGIN    
    
UPDATE BRANCHCOUNTER SET DELETEDBY = @USERID,    
DELETEDDATE = GETDATE()    
WHERE COUNTERID = @COUNTERID
  
SELECT @COUNTERID  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_D_CATEGORY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_D_CATEGORY]  
@CATEGORYID INT = 0,  
@USERID INT  
AS  
BEGIN  
  
UPDATE TBLCATEGORY SET DELETEDBY = @USERID,  
DELETEDDATE = GETDATE()  
WHERE CATEGORYID = @CATEGORYID  

SELECT @CATEGORYID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_D_COUNTER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_D_COUNTER]
@COUNTERID INT = 0,    
@USERID INT    
AS    
BEGIN    
    
UPDATE TBLCOUNTER SET DELETEDBY = @USERID,    
DELETEDDATE = GETDATE()    
WHERE COUNTERID = @COUNTERID
  
SELECT @COUNTERID  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_D_DEALER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_D_DEALER]  
@DEALERID INT = 0,  
@USERID INT  
AS  
BEGIN  
  
UPDATE TBLDEALER SET  
DELETEDBY = @DEALERID,  
 DELETEDDATE = GETDATE()  
WHERE DEALERID = @DEALERID

SELECT @DEALERID
  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_D_DISCARDSTOCKENTRY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_D_DISCARDSTOCKENTRY]
@STOCKENTRYID INT = 0
AS
BEGIN

DELETE FROM STOCKENTRYDETAIL WHERE STOCKENTRYID = @STOCKENTRYID
DELETE FROM STOCKENTRY WHERE STOCKENTRYID = @STOCKENTRYID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_D_GST]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_D_GST]
@GSTID INT = 0,    
@USERID INT    
AS    
BEGIN    
    
UPDATE GSTDETAIL SET DELETEDBY = @USERID,
DELETEDDATE = GETDATE()    
WHERE GSTID = @GSTID

SELECT @GSTID
  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_D_ITEMGROUPDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_D_ITEMGROUPDETAIL]
@ITEMGROUPDETAILID INT = 0,
@USERID INT
AS
BEGIN

UPDATE ITEMGROUPDETAIL SET DELETEDBY = @USERID,
DELETEDDATE = GETDATE() WHERE ITEMGROUPDETAILID = @ITEMGROUPDETAILID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_D_MOP]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_D_MOP]
@MOPID INT = 0,    
@USERID INT    
AS    
BEGIN    
    
UPDATE TBLMOP SET DELETEDBY = @USERID,    
DELETEDDATE = GETDATE()    
WHERE MOPID = @MOPID
  
SELECT @MOPID
  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_D_OFFER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_D_OFFER]
@OfferID int = 0,
@UserID int
AS
BEGIN

UPDATE OFFER SET DELETEDBY = @UserID,
DELETEDDATE = GETDATE()
where OFFERID = @OfferID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_D_OFFERBRANCH]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_D_OFFERBRANCH]
@OfferBranchID int,
@UserID int
AS
BEGIN

update OFFERBRANCH set DELETEDBY = @UserID,
DELETEDDATE = GETDATE()
where OFFERBRANCHID = @OfferBranchID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_D_OFFERITEM]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_D_OFFERITEM]
@OFFERITEMMAPID INT = 0,
@USERID INT
AS
BEGIN

UPDATE OFFERITEMMAP SET DELETEDBY = @USERID,
DELETEDDATE = GETDATE()
WHERE OFFERITEMMAPID = @OFFERITEMMAPID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_D_STOCKDISPATCHDETAILS]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_D_STOCKDISPATCHDETAILS]
@STOCKDISPATCHDETAILID INT = 0
AS
BEGIN

DELETE FROM STOCKDISPATCHDETAIL WHERE STOCKDISPATCHDETAILID = @STOCKDISPATCHDETAILID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_D_STOCKENTRYDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_D_STOCKENTRYDETAIL]
@STOCKENTRYDETAILID INT
AS
BEGIN

DELETE FROM STOCKENTRYDETAIL 
WHERE STOCKENTRYDETAILID = @STOCKENTRYDETAILID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_D_SUBCATEGORY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_D_SUBCATEGORY]  
@SUBCATEGORYID INT = 0,      
@USERID INT      
AS      
BEGIN      
      
UPDATE SUBCATEGORY SET DELETEDBY = @USERID,      
DELETEDDATE = GETDATE()      
WHERE SUBCATEGORYID = @SUBCATEGORYID
    
SELECT @SUBCATEGORYID
END    
GO
/****** Object:  StoredProcedure [dbo].[USP_D_UOM]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_D_UOM]
@UOMID INT = 0,    
@USERID INT    
AS    
BEGIN    
    
UPDATE UOM SET DELETEDBY = @USERID,    
DELETEDDATE = GETDATE()    
WHERE UOMID = @UOMID    
  
SELECT @UOMID  
  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_D_USER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_D_USER]
@USERID INT = 0,    
@CUSERID INT    
AS    
BEGIN    
    
UPDATE TBLUSER SET DELETEDBY = @CUSERID,
DELETEDDATE = GETDATE()    
WHERE USERID = @USERID
  
SELECT @USERID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_APPLIESTO]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_APPLIESTO]
AS
BEGIN

SELECT AppliesToID,AppliesToName FROM APPLIESTO

END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BILLDETAILBYID]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_R_BILLDETAILBYID]
@BRANCHCOUNTERID INT,
@BILLID INT
AS
BEGIN

SELECT 
IC.ITEMCODE
,I.ITEMNAME
,IP.MRP
,IP.SALEPRICE
,BD.QUANTITY
,BD.WEIGHTINKGS
,GST.GSTCODE
,BD.GSTVALUE
,BD.BILLEDAMOUNT
,BD.DISCOUNT
FROM 
POS_BILLDETAIL BD 
INNER JOIN ITEMPRICE IP ON BD.ITEMPRICEID = IP.ITEMPRICEID
INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID
LEFT JOIN OFFER OFR ON BD.OFFERID = OFR.OFFERID
LEFT JOIN GSTDETAIL GST ON BD.GSTID = GST.GSTID
WHERE BD.BRANCHCOUNTERID = @BRANCHCOUNTERID 
AND BD.BILLID = @BILLID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BRANCH]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_BRANCH]
AS                
BEGIN                
          
SELECT                 
B.BRANCHID,B.BRANCHNAME,B.BRANCHCODE,B.ADDRESS,
B.STATEID,B.PHONENO,B.LANDLINE,  
B.EMAILID,ISNULL(B.ISWAREHOUSE,0) AS ISWAREHOUSE,B.SUPERVISORID,  
BS.FULLNAME AS SUPERVISOR, BS.CNUMBER AS SUPERVISORMOBILE,  
C.FULLNAME AS CREATEDBY,B.CREATEDDATE,      
U.FULLNAME AS UPDATEDBY,B.UPDATEDATE  
FROM BRANCH B         
LEFT JOIN TBLUSER C ON B.CREATEDBY = C.USERID          
LEFT JOIN TBLUSER U ON B.UPDATEDBY = U.USERID          
LEFT JOIN TBLUSER BS ON B.SUPERVISORID = BS.USERID  
WHERE B.DELETEDDATE IS NULL                
             
END    
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BRANCHCOUNTER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_R_BRANCHCOUNTER]  
AS            
BEGIN            
      
SELECT             
BC.COUNTERID,BC.COUNTERNAME,BC.BRANCHID,B.BRANCHNAME,C.FULLNAME AS CREATEDBY,  
BC.CREATEDDATE,U.FULLNAME AS UPDATEDBY,BC.UPDATEDDATE  
FROM BRANCHCOUNTER BC  
INNER JOIN BRANCH B ON BC.BRANCHID = B.BRANCHID  
LEFT JOIN TBLUSER C ON BC.CREATEDBY = C.USERID      
LEFT JOIN TBLUSER U ON BC.UPDATEDBY = U.USERID      
WHERE BC.DELETEDDATE IS NULL            
         
END    
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BREFUND]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_BREFUND]      
@BRANCHID INT      
AS      
BEGIN      
      
SELECT       
BR.BREFUNDID,BR.BREFUNDNUMBER,BC.COUNTERNAME,      
CU.FULLNAME AS CREATEDBY,BR.CREATEDATE,    
BR.COUNTERID,BR.BRANCHID,ISNULL(IsAccepted,0) AS IsAccepted,  
case when ISNULL(IsAccepted,0) = 0 then 'Not Accepted'  
else 'Accepted' end as STATUS  
FROM POS_BREFUND BR      
INNER JOIN BRANCH B ON BR.BRANCHID = B.BRANCHID      
INNER JOIN BRANCHCOUNTER BC ON BR.COUNTERID = BC.COUNTERID      
INNER JOIN TBLUSER CU ON BR.CREATEDBY = CU.USERID      
WHERE B.BRANCHID = @BRANCHID AND ISNULL(BR.STATUS,0) <> 0      
      
      
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BREFUNDDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_BREFUNDDETAIL]      
@BREFUNDID INT,      
@COUNTERID INT      
AS      
BEGIN      
      
SELECT BRD.BREFUNDDETAILID,BRD.BREFUNDID,IP.ITEMPRICEID ,
BRD.SNO,IC.ITEMCODE,I.ITEMNAME,      
IP.MRP,IP.SALEPRICE, BRD.QUANTITY,BRD.WEIGHTINKGS,    
ISNULL(ACCEPTEDQUANTITY,BRD.QUANTITY) AS ACCEPTEDQUANTITY,    
ISNULL(ACCEPTEDWEIGHTKGS,BRD.WEIGHTINKGS) AS ACCEPTEDWEIGHTKGS ,
BRD.COUNTERID
FROM POS_BREFUNDDETAIL BRD      
INNER JOIN ITEMPRICE IP ON BRD.ITEMPRICEID = IP.ITEMPRICEID      
INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID      
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID      
WHERE  BRD.BREFUNDID = @BREFUNDID AND BRD.COUNTERID = @COUNTERID      
      
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_CATEGORY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_CATEGORY]      
AS      
BEGIN      
      
SELECT       
 CT.CATEGORYID,CT.CATEGORYNAME, CT.ALLOWOPENITEMS,
 C.FULLNAME AS CREATEDBY,CT.CREATEDDATE,    
 U.FULLNAME AS UPDATEDBY,CT.UPDATEDDATE  
FROM TBLCATEGORY CT  
LEFT JOIN TBLUSER C ON CT.CREATEDBY = C.USERID  
LEFT JOIN TBLUSER U ON CT.UPDATEDBY = U.USERID  
WHERE CT.DELETEDDATE IS NULL      
 
      
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_R_COSTPRICELIST]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_R_COSTPRICELIST]    
@ITEMCODEID INT    
AS    
BEGIN    
    
SELECT 
ICP.COSTPRICEWOT
,ICP.COSTPRICEWT
,IP.MRP
,IP.SALEPRICE
,D.DEALERNAME
,SE.INVOICEDATE
,G.GSTID
,G.CGST  
,G.SGST 
,G.IGST  
,G.CESS  
,IP.ITEMPRICEID
FROM ITEMPRICE IP  
INNER JOIN GSTDETAIL G ON IP.GSTID = G.GSTID  
LEFT JOIN ITEMCOSTPRICE ICP ON IP.ITEMPRICEID = ICP.ITEMPRICEID  
LEFT JOIN STOCKENTRYDETAIL SED ON ICP.ITEMCOSTPRICEID = SED.ITEMCOSTPRICEID  
LEFT JOIN STOCKENTRY SE ON SED.STOCKENTRYID = SE.STOCKENTRYID    
LEFT JOIN TBLDEALER D ON SE.SUPPLIERID = D.DEALERID    
WHERE IP.ITEMCODEID = @ITEMCODEID    
ORDER BY SE.INVOICEDATE DESC  
    
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_COUNTER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_COUNTER]  
AS            
BEGIN            
      
SELECT             
BC.COUNTERID,BC.COUNTERNAME,BC.BRANCHID,B.BRANCHNAME,C.FULLNAME AS CREATEDBY,  
BC.CREATEDDATE,U.FULLNAME AS UPDATEDBY,BC.UPDATEDDATE  
FROM TBLCOUNTER BC  
INNER JOIN BRANCH B ON BC.BRANCHID = B.BRANCHID  
LEFT JOIN TBLUSER C ON BC.CREATEDBY = C.USERID      
LEFT JOIN TBLUSER U ON BC.UPDATEDBY = U.USERID      
WHERE BC.DELETEDDATE IS NULL            
         
END    
GO
/****** Object:  StoredProcedure [dbo].[USP_R_CURRENTSTOCK]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_CURRENTSTOCK]  
@FROMBRANCHID INT,  
@TOBRANCHID INT,  
@ITEMCODEID INT,  
@PARENTITEMID INT = 0  
  
AS  
BEGIN  
  
 DECLARE @FROMBRANCHSTCK DECIMAL(10,2) = 0,@TOBRANCHSTOCK DECIMAL(10,2) = 0  
 IF(@PARENTITEMID > 0)  
 BEGIN  
  
  SELECT @FROMBRANCHSTCK = WEIGHTINKGS  
  FROM STOCKSUMMARY SS  
  INNER JOIN ITEMPRICE IP ON SS.ITEMPRICEID = IP.ITEMPRICEID
  INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID  
  INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID  
  WHERE BRANCHID = @FROMBRANCHID AND I.PARENTITEMID = @PARENTITEMID  
  
 END  
 ELSE  
 BEGIN  
  SELECT @FROMBRANCHSTCK = QUANTITY  
  FROM STOCKSUMMARY SS 
  INNER JOIN ITEMPRICE IP ON SS.ITEMPRICEID = IP.ITEMPRICEID
  INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID
  WHERE BRANCHID = @FROMBRANCHID AND IC.ITEMCODEID = @ITEMCODEID  
 END  
  
 IF(@PARENTITEMID > 0)  
 BEGIN  
  
  SELECT @TOBRANCHSTOCK = WEIGHTINKGS  
  FROM STOCKSUMMARY SS  
  INNER JOIN ITEMPRICE IP ON SS.ITEMPRICEID = IP.ITEMPRICEID
  INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID  
  INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID  
  WHERE BRANCHID = @TOBRANCHID AND I.PARENTITEMID = @PARENTITEMID  
  
 END  
 ELSE  
 BEGIN  
  SELECT @TOBRANCHSTOCK = QUANTITY  
  FROM STOCKSUMMARY SS
  INNER JOIN ITEMPRICE IP ON SS.ITEMPRICEID = IP.ITEMPRICEID
  INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID  
  WHERE BRANCHID = @FROMBRANCHID AND IC.ITEMCODEID = @ITEMCODEID  
 END  
  
 SELECT @FROMBRANCHSTCK, @TOBRANCHSTOCK  
  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DAYCLOSURE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_DAYCLOSURE]    
@BRANCHID INT    
    
AS    
BEGIN    
    
SELECT   
DC.DAYCLOSUREID,  
DC.CLOSUREDATE,  
DC.BRANCHCOUNTERID,  
DC.OPENINGBALANCE,  
DC.CLOSINGBALANCE,  
DC.CLOSINGDIFFERENCE,  
CU.FULLNAME AS CLOSEDBY,  
DC.REFUNDAMOUNT,  
B.BRANCHNAME,  
BC.COUNTERNAME  
FROM POS_DAYCLOSURE DC    
INNER JOIN BRANCHCOUNTER BC ON DC.BRANCHCOUNTERID = BC.COUNTERID    
INNER JOIN BRANCH B ON BC.BRANCHID = B.BRANCHID
INNER JOIN TBLUSER CU ON DC.CLOSEDBY = CU.USERID  
WHERE B.BRANCHID = @BRANCHID 

 
    
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DAYCLOSUREBILL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_DAYCLOSUREBILL]  
@COUNTERID INT,  
@DAYCLOSUREID INT  
AS  
BEGIN  
  
SELECT   
B.BILLID  
,B.BRANCHCOUNTERID  
,B.DAYCLOSUREID  
,B.BILLNUMBER  
,B.CUSTOMERNAME  
,B.CUSTOMERNUMBER  
,CU.FULLNAME AS CREATEDBY  
,CU.CREATEDDATE  
FROM  POS_BILL B  
INNER JOIN TBLUSER CU ON B.CREATEDBY = CU.USERID  
WHERE B.BRANCHCOUNTERID = @COUNTERID 
AND DAYCLOSUREID = @DAYCLOSUREID  
AND  BILLSTATUS = 1

  
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DAYCLOSUREDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_DAYCLOSUREDETAIL]  
@DAYCLOSUREID INT,  
@COUNTERID INT  
  
AS  
BEGIN  
  
SELECT DCD.MOPID,MOP.MOPNAME,DCD.CLOSUREVALUE FROM (  
SELECT   
MOPID,
SUM(CLOSUREVALUE) AS CLOSUREVALUE  
FROM POS_DAYCLOSUREDETAIL  
WHERE DAYCLOSUREID = @DAYCLOSUREID AND COUNTERID = @COUNTERID  
GROUP BY MOPID) DCD   
LEFT JOIN TBLMOP MOP ON ISNULL(DCD.MOPID,1) = MOP.MOPID  
  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DAYCLOSUREITEMS]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_DAYCLOSUREITEMS]        
@BRANCHCOUNTERID INT,        
@DAYCLOSUREID INT      
AS        
BEGIN        
        
SELECT         
        
IC.ITEMCODE,I.ITEMNAME,IP.MRP,IP.SALEPRICE,BD.QUANTITY,        
BD.WEIGHTINKGS,BD.BILLEDAMOUNT,BD.DISCOUNT        
        
FROM (        
SELECT ITEMPRICEID        
, SUM(QUANTITY) AS QUANTITY        
,SUM(ISNULL(WEIGHTINKGS,0)) AS WEIGHTINKGS        
,SUM(DISCOUNT) AS DISCOUNT        
,SUM(BILLEDAMOUNT) AS BILLEDAMOUNT        
        
FROM POS_BILLDETAIL BD   
INNER JOIN POS_BILL B ON BD.BRANCHCOUNTERID = B.BRANCHCOUNTERID  
AND BD.DAYCLOSUREID = B.DAYCLOSUREID
WHERE B.BRANCHCOUNTERID = @BRANCHCOUNTERID        
AND B.DAYCLOSUREID = @DAYCLOSUREID        
AND B.BILLSTATUS = 1 GROUP BY ITEMPRICEID) BD        
INNER JOIN ITEMPRICE IP ON BD.ITEMPRICEID = IP.ITEMPRICEID        
INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID        
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID        
        
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DAYCLOSUREREFUND]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_DAYCLOSUREREFUND]    
@DAYCLOSUREID INT,    
@BRANCHCOUNTERID INT    
AS    
BEGIN    
    
SELECT IC.ITEMCODE,I.ITEMNAME,IP.MRP,IP.SALEPRICE,    
CR.REFUNDQUANTITY AS QUANTITY  
,CR.REFUNDWEIGHTINKGS AS WEIGHTINKGS  
,CR.REFUNDAMOUNT  AS BILLEDAMOUNT    
FROM (    
SELECT BD.ITEMPRICEID,    
SUM(CR.REFUNDQUANTITY) AS REFUNDQUANTITY,    
SUM(ISNULL(CR.REFUNDWEIGHTINKGS,0)) AS REFUNDWEIGHTINKGS,    
SUM(REFUNDAMOUNT) AS REFUNDAMOUNT FROM POS_CREFUND CR    
INNER JOIN POS_BILLDETAIL BD ON CR.BILLDETAILID = BD.BILLDETAILID     
AND CR.COUNTERID = BD.BRANCHCOUNTERID    
WHERE CR.COUNTERID = @BRANCHCOUNTERID     
AND CR.DAYCLOSUREID = @DAYCLOSUREID    
GROUP BY ITEMPRICEID) CR    
INNER JOIN ITEMPRICE IP ON CR.ITEMPRICEID = IP.ITEMPRICEID    
INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID    
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID    
    
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DAYCLOSUREVOIDITEMS]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_DAYCLOSUREVOIDITEMS]  
@COUNTERID INT,  
@DAYCLOSUREID INT  
AS  
BEGIN  
  
SELECT   
IC.ITEMCODE,I.ITEMNAME,IP.MRP,IP.SALEPRICE,      
BD.QUANTITY,BD.WEIGHTINKGS,BD.BILLEDAMOUNT,  
B.BILLNUMBER,CU.FULLNAME AS CREATEDBY  
FROM POS_BILLDETAIL BD  
INNER JOIN POS_BILL B ON B.BILLID = BD.BILLID 
AND B.BRANCHCOUNTERID = BD.BRANCHCOUNTERID
AND B.DAYCLOSUREID = BD.DAYCLOSUREID
INNER JOIN TBLUSER CU ON B.CREATEDBY = CU.USERID  
INNER JOIN ITEMPRICE IP ON BD.ITEMPRICEID = IP.ITEMPRICEID      
INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID      
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID      
WHERE B.BRANCHCOUNTERID = @COUNTERID  
AND B.DAYCLOSUREID = @DAYCLOSUREID  
AND BD.DELETEDDATE IS NOT NULL  
  
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DEALER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_DEALER]      
AS      
BEGIN      
SELECT       
D.DEALERID,      
D.DEALERNAME,      
D.ADDRESS,      
D.STATEID,
D.PHONENO,      
D.GSTIN,      
D.PANNUMBER,      
D.EMAILID,      
C.FULLNAME AS CREATEDBY,D.CREATEDDATE,      
U.FULLNAME AS UPDATEDBY,D.UPDATEDDATE      
FROM TBLDealer  D    
LEFT JOIN TBLUSER C ON D.CREATEDBY = C.USERID        
LEFT JOIN TBLUSER U ON D.UPDATEDBY = U.USERID        
LEFT JOIN TBLSTATE S ON D.STATEID = S.STATEID
WHERE D.DELETEDBY IS NULL      
END      
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DISPATCH]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_DISPATCH]    
@STOCKDISPATCHID INT    
AS    
BEGIN    
    
SELECT SD.STOCKDISPATCHID,SD.FROMBRANCHID,SD.TOBRANCHID,        
SD.CATEGORYID,C.CATEGORYNAME,FB.BRANCHNAME AS FROMBRANCHNAME,        
FB.BRANCHCODE,TB.BRANCHNAME AS TOBRANCHNAME,      
CU.FULLNAME AS CREATEDBY,SD.CREATEDDATE,        
UU.FULLNAME AS UPDATEDBY,SD.UPDATEDATE,  
SD.DISPATCHNUMBER  
FROM STOCKDISPATCH SD        
LEFT JOIN BRANCH FB ON SD.FROMBRANCHID = FB.BRANCHID        
LEFT JOIN BRANCH TB ON SD.TOBRANCHID = TB.BRANCHID        
LEFT JOIN TBLUSER CU ON SD.CREATEDBY = CU.USERID        
LEFT JOIN TBLUSER UU ON SD.UPDATEDBY = UU.USERID        
LEFT JOIN TBLCATEGORY C ON SD.CATEGORYID = C.CATEGORYID        
WHERE STOCKDISPATCHID = @STOCKDISPATCHID        
    
    
SELECT         
DS.STOCKDISPATCHDETAILID,I.ITEMID,IC.ITEMCODEID,IP.ITEMPRICEID,        
I.SKUCODE,IC.ITEMCODE,I.ITEMNAME,IP.MRP,IP.SALEPRICE,DS.DISPATCHQUANTITY,        
DS.WEIGHTINKGS,DS.TRAYNUMBER        
FROM  STOCKDISPATCHDETAIL DS           
INNER JOIN ITEMPRICE IP ON DS.ITEMPRICEID = IP.ITEMPRICEID          
INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID          
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID        
WHERE STOCKDISPATCHID = @STOCKDISPATCHID          
    
END    
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DISPATCHDCLIST]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_R_DISPATCHDCLIST]  
AS  
BEGIN  

SELECT   
DD.DISPATCHDCID,DD.DISPATCHDCNUMBER,  
B.BRANCHCODE,
B.BRANCHNAME,C.CATEGORYNAME,  
DD.BRANCHID,DD.CATEGORYID,  
U.FULLNAME AS CREATEDBY,DD.CREATEDDATE  
FROM DISPATCHDC DD  
INNER JOIN BRANCH B ON DD.BRANCHID = B.BRANCHID  
INNER JOIN TBLCATEGORY C ON DD.CATEGORYID = C.CATEGORYID  
INNER JOIN TBLUSER U ON DD.CREATEDBY = U.USERID  

END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DISPATCHDRAFT]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_DISPATCHDRAFT]        
@USERID INT,    
@CATEGORYID INT    
AS        
BEGIN        
        
DECLARE @STOCKDISPATCHID INT  
SELECT @STOCKDISPATCHID = ISNULL(STOCKDISPATCHID,0)  
FROM STOCKDISPATCH         
WHERE CREATEDBY = @USERID     
AND CATEGORYID = @CATEGORYID AND ISNULL(STATUS,0)  = 0        
        
IF @STOCKDISPATCHID > 0        
BEGIN        
        
exec USP_R_DISPATCH @STOCKDISPATCHID  
  
END        
ELSE      
SELECT 'DISPATCH DOES NOT EXISTS'      
      
END      
GO
/****** Object:  StoredProcedure [dbo].[USP_R_DISPATCHLIST]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_DISPATCHLIST]    
AS    
BEGIN    
    
SELECT     
SD.STOCKDISPATCHID, 
SD.DISPATCHNUMBER,    
B.BRANCHCODE,B.BRANCHNAME,C.CATEGORYNAME,
CU.FULLNAME AS CREATEDBY,    
SD.CREATEDDATE    
FROM STOCKDISPATCH SD     
LEFT JOIN BRANCH B ON SD.TOBRANCHID = B.BRANCHID    
LEFT JOIN TBLCATEGORY C ON SD.CATEGORYID = C.CATEGORYID    
LEFT JOIN TBLUSER CU ON SD.CREATEDBY = CU.USERID    
WHERE ISNULL(STATUS,0) = 1    
    
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_R_GETSYNCDATA]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_R_GETSYNCDATA]        
(        
 @EntityName VARCHAR(50)        
 ,  @SyncDate DATETIME        
)        
AS        
BEGIN        
        
 IF @EntityName = 'ITEM'        
 BEGIN        
  SELECT ITEMID, SKUCODE, ITEMNAME, CREATEDDATE, UPDATEDATE, DELETEDDATE, ISOPENITEM, PARENTITEMID, UOMID, CATEGORYID        
  FROM ITEM I        
  WHERE        
   I.CREATEDDATE > @SyncDate        
   OR I.UPDATEDATE > @SyncDate        
   OR I.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'ITEMCODE'        
 BEGIN        
  SELECT ITEMCODEID, ITEMID, ITEMCODE, CREATEDDATE, UPDATEDATE, DELETEDDATE, FREEITEMCODEID, HSNCODE         
  FROM ITEMCODE IC        
  WHERE        
   IC.CREATEDDATE > @SyncDate        
   OR IC.UPDATEDATE > @SyncDate        
   OR IC.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'ITEMPRICE'        
 BEGIN        
  SELECT ITEMPRICEID, ITEMCODEID, SALEPRICE, MRP, GSTID, CREATEDDATE, UPDATEDATE, DELETEDDATE         
  FROM ITEMPRICE IP        
  WHERE        
   IP.CREATEDDATE > @SyncDate        
   OR IP.UPDATEDATE > @SyncDate        
   OR IP.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'BRANCH'        
 BEGIN        
  SELECT BRANCHID, BRANCHNAME, BRANCHCODE, ADDRESS, PHONENO, LANDLINE, EMAILID, SUPERVISORID, ISWAREHOUSE, CREATEDDATE, UPDATEDATE, DELETEDDATE, STATEID         
  FROM BRANCH B        
  WHERE        
   B.CREATEDDATE > @SyncDate        
   OR B.UPDATEDATE > @SyncDate        
   OR B.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'GST'        
 BEGIN        
  SELECT GSTID, GSTCODE, CGST, SGST, IGST, CESS, CREATEDDATE, UPDATEDATE, DELETEDDATE         
  FROM GSTDETAIL GST        
  WHERE        
   GST.CREATEDDATE > @SyncDate        
   OR GST.UPDATEDATE > @SyncDate        
   OR GST.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'STOCKDISPATCH'        
 BEGIN        
  SELECT STOCKDISPATCHID, FROMBRANCHID, TOBRANCHID, STATUS, STATUSAPPROVEDBY, STATUSAPPROVEDDATE, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDATE, DELETEDBY, DELETEDDATE, DISPATCHNUMBER        
  FROM STOCKDISPATCH SD        
  WHERE        
   SD.CREATEDDATE > @SyncDate        
   OR SD.UPDATEDATE > @SyncDate        
   OR SD.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'STOCKDISPATCHDETAIL'        
 BEGIN        
  SELECT STOCKDISPATCHDETAILID, STOCKDISPATCHID, ITEMPRICEID, TRAYNUMBER,       
  DISPATCHQUANTITY, RECEIVEDQUANTITY, CREATEDDATE        
   , UPDATEDATE, DELETEDDATE, WEIGHTINKGS,ISACCEPTED  
  FROM STOCKDISPATCHDETAIL SDD        
  WHERE        
   SDD.CREATEDDATE > @SyncDate        
   OR SDD.UPDATEDATE > @SyncDate        
   OR SDD.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 --IF @EntityName = 'SUBCATEGORY'        
 --BEGIN        
 -- SELECT * FROM SUBCATEGORY SC        
 -- WHERE        
 --  SC.CREATEDDATE > @SyncDate        
 --  OR SC.UPDATEDDATE > @SyncDate        
 --  OR SC.DELETEDDATE > @SyncDate        
 -- RETURN        
 --END        
        
 IF @EntityName = 'BRANCHCOUNTER'        
 BEGIN        
  SELECT COUNTERID, COUNTERNAME, BRANCHID, CREATEDDATE, UPDATEDDATE, DELETEDDATE     
 , DAYCLOSUREID, BRANCHREFUNDID    
  FROM BRANCHCOUNTER CNTR        
  WHERE        
   CNTR.CREATEDDATE > @SyncDate        
   OR CNTR.UPDATEDDATE > @SyncDate        
   OR CNTR.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 --IF @EntityName = 'DEALER'        
 --BEGIN        
 -- SELECT * FROM TBLDEALER DLR        
 -- WHERE        
 --  DLR.CREATEDDATE > @SyncDate        
 --  OR DLR.UPDATEDDATE > @SyncDate        
 --  OR DLR.DELETEDDATE > @SyncDate        
 -- RETURN        
 --END        
        
 IF @EntityName = 'MOP'        
 BEGIN        
  SELECT MOPID, MOPNAME, CREATEDDATE, UPDATEDDATE, DELETEDDATE        
  FROM TBLMOP MOP        
  WHERE        
   MOP.CREATEDDATE > @SyncDate        
   OR MOP.UPDATEDDATE > @SyncDate        
   OR MOP.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'ROLE'        
 BEGIN        
  SELECT ROLEID, ROLENAME FROM TBLROLE RL        
  --WHERE        
  -- RL.CREATEDATE > @SyncDate        
  -- OR RL.UPDATEDATE > @SyncDate        
  -- OR RL.DELETEDDATE > @SyncDate        
  RETURN        
 END        
     IF @EntityName = 'USER'        
 BEGIN        
  SELECT USERID, ROLEID, REPORTINGLEADID, CATEGORYID, BRANCHID, USERNAME, PASSWORDSTRING, FULLNAME, CNUMBER, EMAIL, ISOTP, GENDER, DOB, CREATEDDATE, UPDATEDDATE, DELETEDDATE        
  FROM TBLUSER USR        
  WHERE        
   USR.CREATEDDATE > @SyncDate        
   OR USR.UPDATEDDATE > @SyncDate        
   OR USR.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'UOM'        
 BEGIN        
  SELECT UOMID, DISPLAYVALUE, BASEUOMID, MULTIPLIER, CREATEDDATE, UPDATEDDATE, DELETEDDATE         
  FROM UOM UOM        
  WHERE        
   UOM.CREATEDDATE > @SyncDate        
   OR UOM.UPDATEDDATE > @SyncDate        
   OR UOM.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'ITEMGROUP'        
 BEGIN        
  SELECT ITEMGROUPID, GROUPNAME, ISACTIVE, CREATEDDATE, UPDATEDDATE, DELETEDDATE        
  FROM ITEMGROUP IG        
  WHERE        
   IG.CREATEDDATE > @SyncDate        
   OR IG.UPDATEDDATE > @SyncDate        
   OR IG.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'ITEMGROUPDETAIL'        
 BEGIN        
  SELECT ITEMGROUPDETAILID, ITEMGROUPID, ITEMCODEID, CREATEDDATE, DELETEDDATE         
  FROM ITEMGROUPDETAIL IGD        
  WHERE        
   IGD.CREATEDDATE > @SyncDate        
   OR IGD.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'OFFERTYPE'        
 BEGIN        
  SELECT OFFERTYPEID, OFFERTYPENAME, OFFERTYPECODE, BUYQUANTITY, FREEQUANTITY         
  FROM OFFERTYPE OT        
  RETURN        
 END        
        
 IF @EntityName = 'OFFER'        
 BEGIN        
  SELECT OFFERID, OFFERNAME, OFFERCODE, STARTDATE, ENDDATE, OFFERVALUE, OFFERTYPEID, CATEGORYID, ITEMGROUPID        
   , CREATEDDATE, UPDATEDDATE, DELETEDDATE, ISACTIVE, APPLIESTOID         
  FROM OFFER OFR        
  WHERE        
   OFR.CREATEDDATE > @SyncDate        
   OR OFR.UPDATEDDATE > @SyncDate        
   OR OFR.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'OFFERBRANCH'        
 BEGIN        
  SELECT OFFERBRANCHID, OFFERID, BRANCHID, CREATEDDATE, DELETEDDATE        
  FROM OFFERBRANCH OFRB        
  WHERE        
   OFRB.CREATEDDATE > @SyncDate        
   OR OFRB.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'OFFERITEMMAP'        
 BEGIN        
  SELECT OFFERITEMMAPID, OFFERID, ITEMCODEID, CREATEDDATE, DELETEDDATE         
  FROM OFFERITEMMAP OFRIM        
  WHERE        
   OFRIM.CREATEDDATE > @SyncDate        
   OR OFRIM.DELETEDDATE > @SyncDate        
  RETURN        
 END        
    
 IF @EntityName = 'POS_DENOMINATION'        
 BEGIN        
  SELECT DENOMINATIONID,DISPLAYVALUE,MULTIPLIER,CREATEDDATE,UPDATEDDATE    
  FROM POS_DENOMINATION DEN    
  WHERE        
   DEN.CREATEDDATE > @SyncDate        
   OR DEN.UPDATEDDATE > @SyncDate        
  RETURN        
 END    

 IF @EntityName = 'TBLCATEGORY'        
 BEGIN        
  SELECT 
CATEGORYID
,CATEGORYNAME
,CREATEDBY
,CREATEDDATE
,UPDATEDBY
,UPDATEDDATE
,DELETEDBY
,DELETEDDATE
,ALLOWOPENITEMS
  FROM TBLCATEGORY CAT
  WHERE
   CAT.CREATEDDATE > @SyncDate
   OR CAT.UPDATEDDATE > @SyncDate
   OR CAT.DELETEDDATE > @SyncDate
  RETURN        
 END    
     
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_R_GST]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_GST]
AS          
BEGIN          
    
SELECT           
G.GSTID,G.GSTCODE,G.CGST,G.SGST,G.IGST,G.CESS
,C.FULLNAME AS CREATEDBY,G.CREATEDDATE,U.FULLNAME AS UPDATEDBY,G.UPDATEDATE    
FROM GSTDETAIL G
LEFT JOIN TBLUSER C ON G.CREATEDBY = C.USERID    
LEFT JOIN TBLUSER U ON G.UPDATEDBY = U.USERID    
WHERE G.DELETEDDATE IS NULL          
ORDER BY LEN(G.GSTCODE), G.GSTCODE
       
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_R_INVOICELIST]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_INVOICELIST]      
AS      
BEGIN      
      
SELECT       
SE.STOCKENTRYID,SE.SUPPLIERINVOICENO,      
CASE WHEN SE.TAXINCLUSIVE = 0 THEN 'NO' ELSE 'YES' END AS TAXINCLUSIVE,
D.DEALERNAME,      
C.CATEGORYNAME,CU.FULLNAME AS CREATEDBY, SE.CREATEDDATE  ,  
SE.INVOICEDATE,SE.TCS,SE.DISCOUNTPER,SE.DISCOUNT,  
SE.EXPENSES,SE.TRANSPORT  
FROM STOCKENTRY SE      
LEFT JOIN TBLDEALER D ON SE.SUPPLIERID = D.DEALERID      
LEFT JOIN TBLCATEGORY C ON SE.CATEGORYID = C.CATEGORYID      
LEFT JOIN TBLUSER CU ON SE.CREATEDBY = CU.USERID      
WHERE ISNULL(STATUS,0) = 1      
      
END    
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEM]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_ITEM]
AS
BEGIN

SELECT I.ITEMID,IC.ITEMCODEID,IC.ITEMCODE,I.ITEMNAME,I.SKUCODE FROM ITEMCODE IC 
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMCODE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_R_ITEMCODE]    
@ItemCodeID INT  
, @CategoryID INT    
AS    
BEGIN    
    
 DECLARE @AllCategoryID INT    
 SELECT @AllCategoryID = CATEGORYID FROM TBLCATEGORY WHERE CATEGORYNAME = 'ALL'    
    
 SELECT    
  IC.ITEMCODEID    
  , IC.ITEMID    
  , IC.ITEMCODE    
  , I.ITEMNAME    
  , I.SKUCODE    
  , I.DESCRIPTION    
  , I.CATEGORYID    
  , ISNULL(I.ISOPENITEM, 0) AS ISOPENITEM    
  , I.PARENTITEMID    
  , I.SUBCATEGORYID    
  , I.UOMID    
  , IC.HSNCODE    
  , IC.ISEAN      
  , CU.FULLNAME AS CREATEDBY    
  , I.CREATEDDATE    
  , UU.FULLNAME AS UPDATEDBY    
  , I.UPDATEDATE     
  , IC.FREEITEMCODEID    
 FROM     
  ITEMCODE IC     
  INNER JOIN ITEM I ON I.ITEMID = IC.ITEMID    
  INNER JOIN TBLCATEGORY CAT ON CAT.CATEGORYID = I.CATEGORYID    
  LEFT JOIN TBLUSER CU ON CU.USERID = IC.CREATEDBY    
  LEFT JOIN TBLUSER UU ON UU.USERID = IC.UPDATEDBY      
 WHERE     
  IC.ITEMCODEID = @ItemCodeID    
  AND IC.DELETEDDATE IS NULL    
  -- to be uncommented after the category fix
  --AND (I.CATEGORYID = @CategoryID OR @CategoryID = @AllCategoryID)    
    
 SELECT     
  ICP.COSTPRICEWT    
  , ICP.COSTPRICEWOT    
  , IP.SALEPRICE    
  , IP.MRP    
  , IP.GSTID    
 FROM  
  ITEMPRICE IP    
  LEFT JOIN ITEMCODE IC ON IC.ITEMCODEID = IP.ITEMCODEID    
  LEFT JOIN ITEM I ON I.ITEMID = IC.ITEMID    
  LEFT JOIN TBLCATEGORY CAT ON CAT.CATEGORYID = I.CATEGORYID    
  LEFT JOIN ITEMCOSTPRICE ICP ON ICP.ITEMPRICEID = IP.ITEMPRICEID  
 WHERE    
  IC.ITEMCODEID = @ItemCodeID    
  AND IP.DELETEDDATE IS NULL    
  -- to be uncommented after the category fix
  --AND (I.CATEGORYID = @CategoryID OR @CategoryID = @AllCategoryID)    
  
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMCODELIST]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_ITEMCODELIST]  
AS  
BEGIN  
  
SELECT IC.ITEMCODEID,I.SKUCODE,IC.ITEMCODE,I.ITEMNAME FROM ITEMCODE IC  
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID  
WHERE PARENTITEMID IS NOT NULL AND IC.ISEAN = 0  
  
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMCODES]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_R_ITEMCODES]  
@CATEGORYID INT = 0                
AS                    
BEGIN                    
                
SELECT I.ITEMID, I.SKUCODE, I.ITEMNAME,C.CATEGORYNAME                
FROM ITEM I LEFT JOIN TBLCATEGORY C ON I.CATEGORYID = C.CATEGORYID                
WHERE I.DELETEDDATE IS NULL                    
                    
 SELECT                    
  IBC.ITEMCODEID,I.ITEMID,IBC.ITEMCODE,I.ITEMNAME,I.SKUCODE,            
  C.CATEGORYNAME,CU.FULLNAME AS CREATEDBY,I.CREATEDDATE,            
  UU.FULLNAME AS UPDATEDBY,I.UPDATEDATE,ISNULL(I.ISOPENITEM,0) AS ISOPENITEM,            
   ISNULL(I.PARENTITEMID,0) AS PARENTITEMID,UM.MULTIPLIER ,      
   I.CATEGORYID,C.CATEGORYNAME,I.SUBCATEGORYID,SC.SUBCATEGORYNAME,
   IBC.HSNCODE
 FROM                    
  ITEMCODE IBC                     
  INNER JOIN ITEM I ON I.ITEMID = IBC.ITEMID                    
  LEFT JOIN TBLUSER CU ON CU.USERID = I.CREATEDBY                    
  LEFT JOIN TBLUSER UU ON UU.USERID = I.UPDATEDBY                    
 LEFT JOIN TBLCATEGORY C ON I.CATEGORYID = C.CATEGORYID                
 LEFT JOIN SUBCATEGORY sc ON I.SUBCATEGORYID = SC.SUBCATEGORYID
 LEFT JOIN UOM UM ON I.UOMID = UM.UOMID            
 WHERE   IBC.DELETEDDATE IS NULL                    
          
 EXEC [USP_R_NONEANLIST]          
    
 IF(@CATEGORYID <> 10)    
 BEGIN    
    
 SELECT                    
  IBC.ITEMCODEID,I.ITEMID,IBC.ITEMCODE,I.ITEMNAME,I.SKUCODE,            
  C.CATEGORYNAME,CU.FULLNAME AS CREATEDBY,I.CREATEDDATE,            
  UU.FULLNAME AS UPDATEDBY,I.UPDATEDATE,ISNULL(I.ISOPENITEM,0) AS ISOPENITEM,            
   ISNULL(I.PARENTITEMID,0) AS PARENTITEMID,UM.MULTIPLIER ,      
   I.CATEGORYID      
 FROM                    
  ITEMCODE IBC                     
  INNER JOIN ITEM I ON I.ITEMID = IBC.ITEMID                    
  LEFT JOIN TBLUSER CU ON CU.USERID = I.CREATEDBY                    
  LEFT JOIN TBLUSER UU ON UU.USERID = I.UPDATEDBY                    
 LEFT JOIN TBLCATEGORY C ON I.CATEGORYID = C.CATEGORYID                
 LEFT JOIN UOM UM ON I.UOMID = UM.UOMID            
 WHERE  /*I.CATEGORYID = @CATEGORYID AND*/ IBC.DELETEDDATE IS NULL               
    
 END    
          
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMGROUP]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_ITEMGROUP]
AS
BEGIN

SELECT 
ITEMGROUPID,GROUPNAME,
CASE WHEN ISACTIVE = 0 THEN 'NO' ELSE 'YES' END AS ISACTIVE,
U.FULLNAME AS CREATEDBY,
IG.CREATEDDATE
FROM ITEMGROUP IG
INNER JOIN TBLUSER U ON IG.CREATEDBY = U.USERID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMGROUPDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_ITEMGROUPDETAIL]    
@ItemGroupID int    
AS    
BEGIN    
    
select     
IGD.ITEMGROUPDETAILID,  
IC.ITEMCODEID,IC.ITEMCODE,    
I.ITEMNAME,IC.HSNCODE,I.CATEGORYID,    
I.SUBCATEGORYID,    
C.CATEGORYNAME,SC.SUBCATEGORYNAME    
from ITEMGROUPDETAIL IGD    
INNER JOIN ITEMCODE IC ON IGD.ITEMCODEID = IC.ITEMCODEID    
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID    
INNER JOIN TBLCATEGORY C ON I.CATEGORYID = C.CATEGORYID    
LEFT JOIN SUBCATEGORY SC ON I.SUBCATEGORYID = SC.SUBCATEGORYID    
where ITEMGROUPID = @ItemGroupID AND IGD.DELETEDDATE IS NULL    
    
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMMRPLIST]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_ITEMMRPLIST]        
@ITEMCODEID NVARCHAR(20)        
AS        
BEGIN        
        
SELECT IP.ITEMPRICEID,       
IP.MRP,      
IP.SALEPRICE,    
IP.GSTID,    
GD.GSTCODE,    
GD.CGST,    
GD.SGST,    
GD.IGST,    
GD.CESS    
FROM ITEMPRICE IP    
INNER JOIN GSTDETAIL GD ON IP.GSTID = GD.GSTID    
WHERE ITEMCODEID = @ITEMCODEID        
        
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ITEMVISUALIZER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_R_ITEMVISUALIZER]      
@ItemID INT      
AS      
BEGIN      
      
 SELECT I.ITEMNAME, I.SKUCODE, I.DESCRIPTION      
 FROM ITEM I      
 WHERE I.ITEMID = @ItemID      
      
 SELECT       
  IC.ITEMCODEID      
  , IC.ITEMCODE      
  , IC.ISEAN      
  , IC.HSNCODE      
  , CU.FULLNAME AS CREATEDBY      
  , IC.CREATEDDATE      
  , UU.FULLNAME AS UPDATEDBY      
  , IC.UPDATEDATE      
 FROM       
  ITEMCODE IC      
  LEFT JOIN TBLUSER CU ON CU.USERID = IC.CREATEDBY      
  LEFT JOIN TBLUSER UU ON UU.USERID = IC.UPDATEDBY      
 WHERE IC.ITEMID = @ItemID      
      
 SELECT       
  IP.ITEMPRICEID      
  , IP.ITEMCODEID      
  , IC.ITEMCODE      
  , ICP.COSTPRICEWT      
  , ICP.COSTPRICEWOT      
  , IP.SALEPRICE      
  , IP.MRP      
  , IP.GSTID      
  , GST.GSTCODE      
  , CU.FULLNAME AS CREATEDBY      
  , IP.CREATEDDATE      
  , UU.FULLNAME AS UPDATEDBY      
  , IP.UPDATEDATE      
 FROM      
  ITEMPRICE IP      
  INNER JOIN ITEMCODE IC ON IC.ITEMCODEID = IP.ITEMCODEID      
  INNER JOIN GSTDETAIL GST ON GST.GSTID = IP.GSTID      
  INNER JOIN ITEMCOSTPRICE ICP ON ICP.ITEMPRICEID = IP.ITEMPRICEID    
  LEFT JOIN TBLUSER CU ON CU.USERID = IP.CREATEDBY      
  LEFT JOIN TBLUSER UU ON UU.USERID = IP.UPDATEDBY      
 WHERE IC.ITEMID = @ItemID      
      
 SELECT       
  SS.BRANCHID      
  , B.BRANCHNAME      
  , SS.QUANTITY      
  , SS.INTRANSITQUANTITY      
  , SS.WEIGHTINKGS      
  , SS.INTRANSITWEIGHTINKGS      
 FROM       
  STOCKSUMMARY SS      
  INNER JOIN BRANCH B ON B.BRANCHID = SS.BRANCHID      
 WHERE SS.ITEMPRICEID = @ItemID      
      
END   
GO
/****** Object:  StoredProcedure [dbo].[USP_R_MOP]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_MOP]        
AS        
BEGIN        
        
SELECT         
 M.MOPID,M.MOPNAME,      
 C.FULLNAME AS CREATEDBY,M.CREATEDDATE,      
 U.FULLNAME AS UPDATEDBY,M.UPDATEDDATE    
FROM TBLMOP M
LEFT JOIN TBLUSER C ON M.CREATEDBY = C.USERID    
LEFT JOIN TBLUSER U ON M.UPDATEDBY = U.USERID    
WHERE M.DELETEDDATE IS NULL        
   
        
END   
GO
/****** Object:  StoredProcedure [dbo].[USP_R_NEXTSKUCODE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_R_NEXTSKUCODE]
AS
BEGIN
	SELECT MAX(CAST(SKUCODE AS BIGINT)) + 1 FROM ITEM
END

GO
/****** Object:  StoredProcedure [dbo].[USP_R_NONEANLIST]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_NONEANLIST]
AS          
BEGIN          
          
SELECT IC.ITEMCODEID,I.SKUCODE,    
IC.ITEMCODE,I.ITEMNAME,I.CATEGORYID      
FROM ITEMCODE IC          
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID          
WHERE IC.ISEAN = 0          
          
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_OFFER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_OFFER]               
AS              
BEGIN              
              
SELECT              
O.OFFERID,O.OFFERCODE,O.OFFERNAME,O.STARTDATE,O.ENDDATE,              
O.OFFERVALUE,O.OFFERTYPEID,OT.OFFERTYPECODE,  
OT.OFFERTYPENAME,O.CATEGORYID,C.CATEGORYNAME,O.ITEMGROUPID,IG.GROUPNAME,              
CU.FULLNAME AS CREATEDBY,O.CREATEDDATE,              
UU.FULLNAME AS UPDATEDBY,O.UPDATEDDATE,              
CASE WHEN O.ISACTIVE = 1 THEN 'YES' ELSE 'NO' END AS ISACTIVE,            
O.APPLIESTOID as  AppliesToID,          
ATO.AppliesToName      
          
FROM OFFER O              
LEFT JOIN OFFERTYPE OT ON O.OFFERTYPEID = OT.OFFERTYPEID              
LEFT JOIN TBLCATEGORY C ON O.CATEGORYID = C.CATEGORYID              
LEFT JOIN ITEMGROUP IG ON O.ITEMGROUPID = IG.ITEMGROUPID              
LEFT JOIN APPLIESTO ATO ON O.APPLIESTOID = ATO.AppliesToID      
LEFT JOIN TBLUSER CU ON O.CREATEDBY = CU.USERID              
LEFT JOIN TBLUSER UU ON O.UPDATEDBY = UU.USERID              

where O.DELETEDBY IS NULL
              
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_OFFERBRANCH]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_OFFERBRANCH]      
@OfferID int      
AS      
BEGIN      
      
select OB.OFFERBRANCHID,B.BRANCHID,      
B.BRANCHNAME,B.BRANCHCODE,B.PHONENO from OFFERBRANCH OB      
INNER JOIN BRANCH B ON OB.BRANCHID = B.BRANCHID      
where OB.OFFERID = @OfferID  and OB.DELETEDBY is null  
      
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_OFFERITEM]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_OFFERITEM]  
@OfferID int  
AS  
BEGIN  
  
select       
OI.OFFERITEMMAPID,IC.ITEMCODEID,IC.ITEMCODE,      
I.ITEMNAME,IC.HSNCODE,I.CATEGORYID,      
I.SUBCATEGORYID,C.CATEGORYNAME,SC.SUBCATEGORYNAME      
from OFFERITEMMAP OI  
LEFT JOIN ITEMCODE IC ON OI.ITEMCODEID = IC.ITEMCODEID      
LEFT JOIN ITEM I ON IC.ITEMID = I.ITEMID      
LEFT JOIN TBLCATEGORY C ON I.CATEGORYID = C.CATEGORYID      
LEFT JOIN SUBCATEGORY SC ON I.SUBCATEGORYID = SC.SUBCATEGORYID      
where OI.OFFERID = @OfferID AND OI.DELETEDDATE IS NULL      
  
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_OFFERTYPE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_OFFERTYPE]
AS
BEGIN

SELECT OFFERTYPEID,OFFERTYPECODE,OFFERTYPENAME
FROM OFFERTYPE

END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_PARENTITEMS]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_R_PARENTITEMS]            
@CATEGORYID INT = 0        
AS            
BEGIN            
        
 DECLARE @AllCategoryID INT    
 SELECT @AllCategoryID = CAT.CATEGORYID FROM TBLCATEGORY CAT WHERE CAT.CATEGORYNAME = 'ALL'    
    
    
 SELECT I.ITEMID, I.SKUCODE, I.ITEMNAME,
 I.SKUCODE AS ITEMCODE ,IC.ITEMCODEID,I.ISOPENITEM,
 i.PARENTITEMID, I.UOMID
  FROM ITEM I     
  INNER JOIN TBLCATEGORY CAT ON CAT.CATEGORYID = I.CATEGORYID    
  INNER JOIN ITEMCODE IC ON I.ITEMID = IC.ITEMID
 WHERE    
  I.ISOPENITEM = 1    
  --- to be uncommented when category users are created and issue is fixed
  --AND (CAT.CATEGORYID = @CATEGORYID OR @CATEGORYID = @AllCategoryID)    
  AND I.DELETEDDATE IS NULL    
    
    
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_PRINTERSETTINGS]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_PRINTERSETTINGS]
@USERID INT
AS
BEGIN

SELECT 
PS.PRINTERSETTINGSID,
PS.PRINTERTYPEID,
PT.PRINTERTYPENAME,
PS.PRINTERNAME,
PS.USERID 
FROM PRINTERSETTINGS PS INNER JOIN PRINTERTYPE PT
ON PS.PRINTERTYPEID = PT.PRINTERTYPEID
WHERE PS.USERID = @USERID


END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_PRINTERTYPE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_PRINTERTYPE]
AS
BEGIN
SELECT PRINTERTYPEID,PRINTERTYPENAME FROM PRINTERTYPE
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_ROLE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_ROLE]
AS      
BEGIN      
      
SELECT       
 ROLEID,ROLENAME
FROM TBLROLE
      
      
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STATE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_R_STATE]
AS
BEGIN
SELECT STATEID,STATENAME FROM TBLSTATE
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKCONTINGDIFF]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_STOCKCONTINGDIFF]
@BRANCHID INT
AS
BEGIN

SELECT 
IC.ITEMCODE,I.ITEMNAME,IP.MRP,IP.SALEPRICE,
C.CATEGORYNAME,SCAT.SUBCATEGORYNAME,
SC.QUANTITY AS PHYSICALSTOCK,
SS.QUANTITY AS SYSTEMSTOCK
FROM (SELECT ITEMPRICEID,SUM(QUANTITY) AS QUANTITY FROM STOCKCOUNTINGDETAIL SCD
INNER JOIN STOCKCOUNTING SC ON SCD.STOCKCOUNTINGID = SC.STOCKCOUNTINGID
WHERE SC.BRANCHID = @BRANCHID AND ISNULL(STATUS,0) = 0 GROUP BY ITEMPRICEID) SC
LEFT JOIN STOCKSUMMARY SS ON SC.ITEMPRICEID = SS.ITEMPRICEID
INNER JOIN ITEMPRICE IP ON SC.ITEMPRICEID = IP.ITEMPRICEID
INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID
INNER JOIN TBLCATEGORY C ON I.CATEGORYID = C.CATEGORYID
INNER JOIN SUBCATEGORY SCAT ON I.SUBCATEGORYID = SCAT.SUBCATEGORYID
WHERE SS.BRANCHID = @BRANCHID

END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKCOUNTING]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_STOCKCOUNTING]     
@BRANCHID INT = 0        
AS        
BEGIN        
        
SELECT SC.STOCKCOUNTINGID,         
CU.FULLNAME AS CREATEDBY,        
SC.CREATEDDATE,        
UU.FULLNAME AS UPDATEDBY,        
SC.UPDATEDDATE,        
CASE WHEN ISNULL(SC.STATUS,0) = 0 THEN 'Not Verified'        
ELSE 'Verified' END AS STATUS        
FROM STOCKCOUNTING SC        
left JOIN TBLUSER CU ON SC.CREATEDBY = CU.USERID        
left JOIN TBLUSER UU ON SC.UPDATEDBY = UU.USERID        
WHERE SC.BRANCHID = @BRANCHID AND ISNULL(STATUS,0) = 1
        
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKCOUNTINGDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_STOCKCOUNTINGDETAIL]    
@STOCKCOUNTINGID INT    
AS    
BEGIN    
    
SELECT STOCKCOUNTINGDETAILID,SCD.ITEMPRICEID ,  
IC.ITEMCODE,I.ITEMNAME,IP.MRP,IP.SALEPRICE,SCD.QUANTITY  
FROM STOCKCOUNTINGDETAIL SCD    
INNER JOIN ITEMPRICE IP ON SCD.ITEMPRICEID = IP.ITEMPRICEID    
INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID    
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID    
INNER JOIN TBLCATEGORY C ON I.CATEGORYID = C.CATEGORYID  
INNER JOIN SUBCATEGORY SC ON I.SUBCATEGORYID = SC.SUBCATEGORYID  
where SCD.STOCKCOUNTINGID = @STOCKCOUNTINGID
 
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKDISPATCHDC]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_STOCKDISPATCHDC]          
@DISPATCHDCID INT          
AS          
BEGIN          
    
SELECT         
DD.DISPATCHDCID,        
DD.DISPATCHDCNUMBER,        
B.BRANCHNAME AS TOBRANCHNAME,    
B.ADDRESS AS TOADDRESS,    
B.PHONENO AS TOPHONENO,    
TS.STATENAME AS TOSTATENAME,    
FB.BRANCHNAME AS FROMBRANCHNAME,    
FB.ADDRESS AS FROMADDRESS,    
FB.PHONENO AS FROMPHONENO,    
FS.STATENAME AS FROMSTATENAME,    
C.CATEGORYNAME,        
U.FULLNAME AS CREATEDBY,        
DD.CREATEDDATE        
FROM DISPATCHDC DD        
LEFT JOIN BRANCH B ON DD.BRANCHID = B.BRANCHID    
LEFT JOIN BRANCH FB ON DD.FROMBRANCHID = FB.BRANCHID    
LEFT JOIN TBLCATEGORY C ON DD.CATEGORYID = C.CATEGORYID        
LEFT JOIN TBLUSER U ON DD.CREATEDBY = U.USERID        
LEFT JOIN TBLSTATE TS ON B.STATEID = TS.STATEID    
LEFT JOIN TBLSTATE FS ON FB.STATEID = TS.STATEID    
WHERE DISPATCHDCID = @DISPATCHDCID        
        
SELECT  
ROW_NUMBER() OVER(ORDER BY SDD.STOCKDISPATCHID,SDD.STOCKDISPATCHDETAILID DESC) AS Sno  
,IC.ITEMCODE,I.ITEMNAME,IC.HSNCODE,SDD.DISPATCHQUANTITY,SDD.WEIGHTINKGS,      
G.GSTCODE,G.CGST,G.SGST,G.IGST,G.CESS,        
ICP.COSTPRICE,IP.MRP,IP.SALEPRICE        
FROM STOCKDISPATCHDETAIL SDD      
INNER JOIN ITEMPRICE IP ON SDD.ITEMPRICEID = IP.ITEMPRICEID        
INNER JOIN GSTDETAIL G ON IP.GSTID = G.GSTID        
LEFT JOIN (SELECT ITEMPRICEID,SUM(COSTPRICEWT * QUANTITY)/SUM(QUANTITY) AS COSTPRICE         
FROM ITEMCOSTPRICE GROUP BY ITEMPRICEID) AS ICP ON IP.ITEMPRICEID = ICP.ITEMPRICEID        
INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID        
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID        
WHERE EXISTS(SELECT STOCKDISPATCHID FROM DISPATCHDCMAPPING DDM         
WHERE SDD.STOCKDISPATCHID = DDM.STOCKDISPATCHID        
AND DISPATCHDCID = @DISPATCHDCID)        
        
END    
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKENTRY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_STOCKENTRY]                    
@STOCKENTRYID INT                    
AS                    
BEGIN                    
          
          
DECLARE @FinalPriceSUM DECIMAL(9, 2)              
SELECT @FinalPriceSUM = SUM(FINALPRICE) FROM STOCKENTRYDETAIL WHERE STOCKENTRYID = @STOCKENTRYID              
          
SELECT SE.STOCKENTRYID,SE.SUPPLIERID,SE.SUPPLIERINVOICENO,                    
CASE WHEN SE.TAXINCLUSIVE = 0 THEN 'NO' ELSE 'YES' END AS TAXINCLUSIVE,    
SE.TAXINCLUSIVE AS TAXINCLUSIVEVALUE,  
SE.CATEGORYID,S.DEALERNAME,S.GSTIN,                    
C.CATEGORYNAME,CU.FULLNAME AS CREATEDBY,                    
SE.CREATEDDATE,SE.INVOICEDATE,                
SE.TCS,SE.DISCOUNTPER,SE.DISCOUNT,      
SE.EXPENSES,SE.TRANSPORT, @FinalPriceSUM AS FINALPRICE      
FROM STOCKENTRY SE                    
LEFT JOIN TBLDEALER S ON SE.SUPPLIERID = S.DEALERID                  
LEFT JOIN TBLCATEGORY C ON SE.CATEGORYID = C.CATEGORYID                    
LEFT JOIN TBLUSER CU ON SE.CREATEDBY = CU.USERID                    
WHERE STOCKENTRYID = @STOCKENTRYID                    
        
SELECT                       
SE.STOCKENTRYDETAILID,                      
I.ITEMID,                      
IC.ITEMCODEID,                      
IP.ITEMPRICEID,                      
I.SKUCODE,                      
IC.ITEMCODE,                      
I.ITEMNAME,                      
ICP.COSTPRICEWT,                      
ICP.COSTPRICEWOT,
ICP.GSTID,
IP.MRP,                      
IP.SALEPRICE,                      
SE.QUANTITY,                      
SE.WEIGHTINKGS            
, SE.FREEQUANTITY                
, SE.DISCOUNTFLAT                
, SE.DISCOUNTPERCENTAGE                
, SE.SCHEMEPERCENTAGE                
, SE.SCHEMEFLAT                
, SE.TOTALPRICEWT                
, SE.TOTALPRICEWOT                
, SE.APPLIEDDISCOUNT                
, SE.APPLIEDSCHEME                
,G.GSTCODE              
, SE.APPLIEDDGST                
, SE.FINALPRICE      
,SE.CGST      
,SE.SGST      
,SE.IGST      
,SE.CESS      
FROM STOCKENTRYDETAIL SE                       
LEFT JOIN ITEMCOSTPRICE ICP ON SE.ITEMCOSTPRICEID = ICP.ITEMCOSTPRICEID        
LEFT JOIN ITEMPRICE IP ON ICP.ITEMPRICEID = IP.ITEMPRICEID        
LEFT JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID        
LEFT JOIN ITEM I ON IC.ITEMID = I.ITEMID        
LEFT JOIN GSTDETAIL G ON IP.GSTID = G.GSTID        
WHERE STOCKENTRYID = @STOCKENTRYID        
                    
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKENTRYDTAFT]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_STOCKENTRYDTAFT]            
@USERID INT,            
@CATEGORYID INT            
AS            
BEGIN            
            
DECLARE @STOCKENTRYID INT            
SELECT @STOCKENTRYID = STOCKENTRYID FROM STOCKENTRY            
WHERE CREATEDBY = @USERID AND CATEGORYID = @CATEGORYID             
AND ISNULL(STATUS,0) = 0            
  
EXEC USP_R_STOCKENTRY  @STOCKENTRYID
            
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_R_STOCKSUMMARY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[USP_R_STOCKSUMMARY]  
@BranchID INT = 0  
, @ItemID INT = 0  
AS  
BEGIN  
   
	SELECT   
		SS.STOCKSUMMARYID, B.BRANCHNAME  
		, I.SKUCODE, I.ITEMNAME, ISNULL(I.ISOPENITEM, 0) AS ISOPENITEM  
		, PARENTITEM.ITEMNAME AS PARENTITEMNAME, PARENTITEM.SKUCODE AS PARENTSKUCODE  
		, SS.QUANTITY, SS.INTRANSITQUANTITY, SS.WEIGHTINKGS, SS.INTRANSITWEIGHTINKGS   
	FROM   
		STOCKSUMMARY SS 
		INNER JOIN ITEMPRICE IP ON IP.ITEMPRICEID = SS.ITEMPRICEID
		INNER JOIN ITEMCODE IC ON IC.ITEMCODEID = IP.ITEMCODEID
		INNER JOIN ITEM I ON I.ITEMID = IC.ITEMCODEID
		INNER JOIN BRANCH B ON B.BRANCHID = SS.BRANCHID  
		LEFT JOIN ITEM PARENTITEM ON PARENTITEM.ITEMID = I.PARENTITEMID  
	WHERE   
		(SS.BRANCHID = @BranchID OR @BranchID = 0)  
		AND (I.ITEMID = @ItemID OR @ItemID = 0)  
  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_R_SUBCATEGORY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_SUBCATEGORY]    
AS              
BEGIN              
        
SELECT               

SC.SUBCATEGORYID,SC.SUBCATEGORYNAME,SC.CATEGORYID,C.CATEGORYNAME,
CU.FULLNAME AS CREATEDBY,    
SC.CREATEDDATE,UU.FULLNAME AS UPDATEDBY,SC.UPDATEDDATE    
FROM SUBCATEGORY SC
INNER JOIN TBLCATEGORY C ON SC.CATEGORYID = C.CATEGORYID
LEFT JOIN TBLUSER CU ON SC.CREATEDBY = CU.USERID        
LEFT JOIN TBLUSER UU ON SC.UPDATEDBY = UU.USERID        
WHERE SC.DELETEDDATE IS NULL              
           
END      
GO
/****** Object:  StoredProcedure [dbo].[USP_R_UOM]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_UOM]
AS        
BEGIN        
        
SELECT         
 UO.UOMID,UO.DISPLAYVALUE,
 UO.MULTIPLIER,UO.BASEUOMID,
 BU.DISPLAYVALUE AS BASEUOM,
 C.FULLNAME AS CREATEDBY,UO.CREATEDDATE,      
 U.FULLNAME AS UPDATEDBY,UO.UPDATEDDATE    
FROM UOM UO
LEFT JOIN TBLUSER C ON UO.CREATEDBY = C.USERID    
LEFT JOIN TBLUSER U ON UO.UPDATEDBY = U.USERID    
LEFT JOIN UOM BU ON UO.BASEUOMID = BU.UOMID
WHERE UO.DELETEDDATE IS NULL        
ORDER BY LEN(UO.DISPLAYVALUE), UO.DISPLAYVALUE
        
END   
GO
/****** Object:  StoredProcedure [dbo].[USP_R_USER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_USER]          
AS          
BEGIN          
          
SELECT           
 TU.USERID,TU.ROLEID,TU.REPORTINGLEADID,  
 TU.CATEGORYID,TU.BRANCHID,TU.USERNAME,          
 TU.PASSWORDSTRING,TU.FULLNAME,TU.CNUMBER,  
 TU.EMAIL,TU.ISOTP,TU.GENDER,TU.DOB,  
 R.ROLENAME,CT.CATEGORYNAME,B.BRANCHNAME,RL.FULLNAME AS REPORTINGLEAD,  
 C.FULLNAME AS CREATEDBY,TU.CREATEDDATE,U.FULLNAME AS UPDATEDBY,TU.UPDATEDDATE  
FROM TBLUSER TU  
LEFT JOIN TBLUSER C ON TU.CREATEDBY = C.USERID  
LEFT JOIN TBLUSER U ON TU.UPDATEDBY = U.USERID  
INNER JOIN TBLROLE R ON TU.ROLEID = R.ROLEID  
INNER JOIN TBLCATEGORY CT ON TU.CATEGORYID = CT.CATEGORYID  
INNER JOIN BRANCH B ON TU.BRANCHID = B.BRANCHID  
INNER JOIN TBLUSER RL ON TU.REPORTINGLEADID = RL.USERID  
WHERE TU.DELETEDDATE IS NULL          
               
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_USERLOGIN]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_USERLOGIN]      
@USERNAME NVARCHAR(100),                                              
@PASSWORD NVARCHAR(100)                            
AS                                              
BEGIN                                              
                                              
DECLARE @USERID INT = 0                            
DECLARE @WAREHOUSEID INT = 0
SELECT TOP(1) @WAREHOUSEID = BRANCHID FROM BRANCH WHERE ISWAREHOUSE = 1
                                      
SELECT @USERID = USERID      
FROM TBLUSER                                           
WHERE  
BRANCHID = @WAREHOUSEID
AND USERNAME = @USERNAME      
AND PASSWORDSTRING = @PASSWORD and DELETEDBY IS NULL      

                                              
IF (@USERID > 0)                                              
BEGIN                                              
                                    
SELECT                                              
U.USERID,                                              
U.USERNAME,                                              
U.FULLNAME,                                              
U.PASSWORDSTRING,                            
U.CNUMBER,                            
U.EMAIL,      
U.GENDER,      
isnull(U.ISOTP,0) as ISOTP,       
U.REPORTINGLEADID,      
RL.FullName AS REPORTINGLEAD,      
U.ROLEID,      
R.ROLENAME,      
U.CATEGORYID,      
C.CATEGORYNAME,      
U.BRANCHID,      
B.BRANCHNAME,  
ISNULL(C.ALLOWOPENITEMS,0) AS ALLOWOPENITEMS  
FROM TBLUSER U                         
left JOIN TBLROLE R on U.ROLEID = R.ROLEID      
left JOIN TBLUSER RL on U.REPORTINGLEADID = RL.USERID      
left JOIN TBLCATEGORY C on U.CATEGORYID = C.CATEGORYID      
left JOIN BRANCH B on U.BRANCHID = B.BRANCHID      
WHERE U.USERID = @USERID      
    
EXEC USP_R_PRINTERSETTINGS @USERID    
                                             
END                                              
ELSE                                              
SELECT 'Invalid Username or Password'                                              
                                              
END   
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_BILLMOPDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SYNC_CU_BILLMOPDETAIL]          
(          
 @BillMopDetails POS_BILLMOPDETAILTYPE READONLY          
)          
AS          
BEGIN          
 UPDATE BMD          
 SET          
BMD.BILLID = UBMD.BILLID,        
BMD.MOPID = UBMD.MOPID,        
BMD.MOPVALUE = UBMD.MOPVALUE,        
BMD.CREATEDDATE = UBMD.CREATEDDATE,        
BMD.UPDATEDDATE = UBMD.UPDATEDDATE,        
BMD.COUNTERID = UBMD.COUNTERID,
BMD.DAYCLOSUREID = UBMD.DAYCLOSUREID    

 FROM           
  POS_BILLMOPDETAIL BMD        
  INNER JOIN @BillMopDetails UBMD ON UBMD.BILLMOPDETAILID = BMD.BILLMOPDETAILID          
 WHERE          
  BMD.COUNTERID = UBMD.COUNTERID    
          
 INSERT INTO POS_BILLMOPDETAIL(BILLMOPDETAILID,BILLID,MOPID,MOPVALUE,  
CREATEDDATE,UPDATEDDATE,COUNTERID,DAYCLOSUREID)          
 SELECT BILLMOPDETAILID,BILLID,MOPID,MOPVALUE,  
CREATEDDATE,UPDATEDDATE,COUNTERID,DAYCLOSUREID
 FROM @BillMopDetails UBMD          
 WHERE NOT EXISTS          
  (          
   SELECT 1 FROM POS_BILLMOPDETAIL BDINNER WHERE BDINNER.BILLMOPDETAILID = UBMD.BILLMOPDETAILID        
   AND BDINNER.COUNTERID = UBMD.COUNTERID    
  )          
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_BREFUND]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SYNC_CU_BREFUND]      
(      
 @BRefundDetails POS_BREFUNDTYPE READONLY
)      
AS      
BEGIN      
 UPDATE BR    
 SET      
BR.BRANCHID = UBR.BRANCHID,    
BR.CREATEDBY = UBR.CREATEDBY,    
BR.CREATEDATE = UBR.CREATEDATE,    
BR.UPDATEDBY = UBR.UPDATEDBY,    
BR.UPDATEDDATE = UBR.UPDATEDDATE,    
BR.DELETEDBY = UBR.DELETEDBY,    
BR.DELETEDDATE = UBR.DELETEDDATE,    
BR.STATUS = UBR.STATUS,    
BR.BREFUNDNUMBER = UBR.BREFUNDNUMBER,    
BR.COUNTERID = UBR.COUNTERID
 FROM       
  POS_BREFUND BR    
  INNER JOIN @BRefundDetails UBR ON UBR.BREFUNDID = BR.BREFUNDID      
 WHERE      
  BR.COUNTERID = UBR.COUNTERID
      
 INSERT INTO POS_BREFUND(BREFUNDID,BRANCHID,CREATEDBY,CREATEDATE,UPDATEDBY,    
UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS,BREFUNDNUMBER,COUNTERID)      
 SELECT BREFUNDID,BRANCHID,CREATEDBY,CREATEDATE,UPDATEDBY,    
UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS,BREFUNDNUMBER,COUNTERID
 FROM @BRefundDetails UBR      
 WHERE NOT EXISTS      
  (      
   SELECT 1 FROM POS_BREFUND BRINNER WHERE BRINNER.BREFUNDID = UBR.BREFUNDID    
   AND BRINNER.COUNTERID = UBR.COUNTERID
  )      

  
  UPDATE BC
  SET 
	BC.BRANCHREFUNDID = MAXBR.MAXREFUNDID
	, UPDATEDDATE = GETDATE()
  FROM 
	BRANCHCOUNTER BC
	INNER JOIN 
		(
			SELECT BR.COUNTERID, MAX(BR.BREFUNDID) AS MAXREFUNDID 
			FROM @BRefundDetails BR 
			GROUP BY BR.COUNTERID
		) MAXBR ON MAXBR.COUNTERID = BC.COUNTERID
	WHERE ISNULL(BC.BRANCHREFUNDID, 0) < MAXBR.MAXREFUNDID
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_BREFUNDDETAL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SYNC_CU_BREFUNDDETAL]        
(        
 @BRefundDetails POS_BREFUNDDETAILTYPE READONLY        
)        
AS        
BEGIN        
 UPDATE BRD      
 SET        
BRD.BREFUNDID  = UBRD.BREFUNDID,      
BRD.ITEMPRICEID = UBRD.ITEMPRICEID,      
BRD.QUANTITY = UBRD.QUANTITY,      
BRD.WEIGHTINKGS = UBRD.WEIGHTINKGS,      
BRD.CREATEDDATE = UBRD.CREATEDDATE,      
BRD.UPDATEDDATE = UBRD.UPDATEDDATE,      
BRD.SNO = UBRD.SNO,      
BRD.TRAYNUMBER = UBRD.TRAYNUMBER,      
BRD.COUNTERID = UBRD.COUNTERID  
 FROM         
  POS_BREFUNDDETAIL BRD      
  INNER JOIN @BRefundDetails UBRD ON UBRD.BREFUNDDETAILID = BRD.BREFUNDDETAILID        
 WHERE        
  BRD.COUNTERID = UBRD.COUNTERID  
        
 INSERT INTO POS_BREFUNDDETAIL(BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,      
WEIGHTINKGS,CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,COUNTERID)        
 SELECT BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,      
WEIGHTINKGS,CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,COUNTERID  
 FROM @BRefundDetails UBRD      
 WHERE NOT EXISTS        
  (        
   SELECT 1 FROM POS_BREFUNDDETAIL BRDINNER WHERE BRDINNER.BREFUNDDETAILID = UBRD.BREFUNDDETAILID      
   AND BRDINNER.COUNTERID = UBRD.COUNTERID  
  )        
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_CREFUND]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SYNC_CU_CREFUND]      
(      
 @CRefundDetails POS_CREFUNDTYPE READONLY
)      
AS      
BEGIN      
 UPDATE CR    
 SET      
CR.BILLDETAILID  = UCR.BILLDETAILID,    
CR.REFUNDQUANTITY  = UCR.REFUNDQUANTITY,    
CR.REFUNDWEIGHTINKGS = UCR.REFUNDWEIGHTINKGS,    
CR.REFUNDAMOUNT = UCR.REFUNDAMOUNT,    
CR.CREATEDBY = UCR.CREATEDBY,    
CR.CREATEDDATE = UCR.CREATEDDATE,    
CR.UPDATEDBY = UCR.UPDATEDBY,    
CR.UPDATEDDATE = UCR.UPDATEDDATE,    
CR.DELETEDBY = UCR.DELETEDBY,    
CR.DELETEDDATE = UCR.DELETEDDATE,    
CR.DAYCLOSUREID = UCR.DAYCLOSUREID,    
CR.COUNTERID = UCR.COUNTERID
 FROM       
  POS_CREFUND CR    
  INNER JOIN @CRefundDetails UCR ON UCR.REFUNDID = CR.REFUNDID    
 WHERE      
 CR.COUNTERID = UCR.COUNTERID
      
 INSERT INTO POS_CREFUND(REFUNDID,BILLDETAILID,REFUNDQUANTITY,REFUNDWEIGHTINKGS,    
REFUNDAMOUNT,CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,DELETEDBY,    
DELETEDDATE,DAYCLOSUREID,COUNTERID)      
 SELECT REFUNDID,BILLDETAILID,REFUNDQUANTITY,REFUNDWEIGHTINKGS,    
REFUNDAMOUNT,CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,DELETEDBY,    
DELETEDDATE,DAYCLOSUREID,COUNTERID
 FROM @CRefundDetails UCR    
 WHERE NOT EXISTS      
  (      
   SELECT 1 FROM POS_CREFUND CRINNER WHERE CRINNER.REFUNDID = UCR.REFUNDID    
   AND CRINNER.COUNTERID = UCR.COUNTERID
  )      
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_DAYCLOSURE]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SYNC_CU_DAYCLOSURE]              
(              
 @DayClosure POS_DAYCLOSURETYPE READONLY      
)              
AS              
BEGIN              
              
 MERGE STOCKSUMMARY AS Target    
 USING   
 (  
  SELECT   
   BC.BRANCHID,BD.ITEMPRICEID,  
   SUM(BD.QUANTITY) AS QUANTITY,  
   SUM(BD.WEIGHTINKGS) AS WEIGHTINKGS  
  FROM   
   POS_BILLDETAIL BD  
   INNER JOIN POS_BILL B ON BD.BILLID =B.BILLID AND BD.BRANCHCOUNTERID = B.BRANCHCOUNTERID  
   INNER JOIN @DayClosure UDC ON B.DAYCLOSUREID = UDC.DAYCLOSUREID AND B.BRANCHCOUNTERID = UDC.BRANCHCOUNTERID  
   INNER JOIN BRANCHCOUNTER BC ON B.BRANCHCOUNTERID = BC.COUNTERID  
  WHERE  
   NOT EXISTS   
    (  
     SELECT 1 FROM POS_DAYCLOSURE DCINNER   
     WHERE DCINNER.DAYCLOSUREID = UDC.DAYCLOSUREID   
      AND DCINNER.BRANCHCOUNTERID = UDC.BRANCHCOUNTERID            
     )  
   GROUP BY BC.BRANCHID,BD.ITEMPRICEID  
 ) AS Source          
 ON Source.BRANCHID = Target.BRANCHID AND Source.ITEMPRICEID = Target.ITEMPRICEID    
              
 WHEN MATCHED THEN UPDATE SET          
 Target.QUANTITY = ISNULL(Target.QUANTITY,0) - ISNULL(Source.QUANTITY,0),          
 Target.WEIGHTINKGS = ISNULL(Target.WEIGHTINKGS,0) - ISNULL(Source.WEIGHTINKGS,0);  
  
 MERGE STOCKSUMMARY AS Target    
 USING   
 (  
  SELECT   
   BC.BRANCHID,BD.ITEMPRICEID,  
   SUM(CR.REFUNDQUANTITY) AS QUANTITY,  
   SUM(CR.REFUNDWEIGHTINKGS) AS WEIGHTINKGS  
  FROM   
   POS_CREFUND CR  
   INNER JOIN POS_BILLDETAIL BD ON CR.BILLDETAILID = BD.BILLDETAILID AND CR.COUNTERID = BD.BRANCHCOUNTERID  
   INNER JOIN @DayClosure UDC ON CR.DAYCLOSUREID = UDC.DAYCLOSUREID  AND CR.COUNTERID = UDC.BRANCHCOUNTERID  
   INNER JOIN BRANCHCOUNTER BC ON CR.COUNTERID = BC.COUNTERID  
  WHERE NOT EXISTS   
   (  
    SELECT 1 FROM POS_DAYCLOSURE DCINNER   
    WHERE DCINNER.DAYCLOSUREID = UDC.DAYCLOSUREID            
       AND DCINNER.BRANCHCOUNTERID = UDC.BRANCHCOUNTERID            
   )  
  GROUP BY BC.BRANCHID,BD.ITEMPRICEID  
 ) AS Source          
 ON Source.BRANCHID = Target.BRANCHID AND Source.ITEMPRICEID = Target.ITEMPRICEID    
              
 WHEN MATCHED THEN UPDATE SET          
 Target.QUANTITY = ISNULL(Target.QUANTITY,0) + ISNULL(Source.QUANTITY,0),          
 Target.WEIGHTINKGS = ISNULL(Target.WEIGHTINKGS,0) + ISNULL(Source.WEIGHTINKGS,0);  

 INSERT INTO POS_DAYCLOSURE(DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,          
  CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDBY,CREATEDDATE,          
  UPDATEDBY,UPDATEDDATE)              
 SELECT DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,          
  CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDBY,CREATEDDATE,          
  UPDATEDBY,UPDATEDDATE          
 FROM @DayClosure UDC            
 WHERE NOT EXISTS              
  (              
   SELECT 1 FROM POS_DAYCLOSURE DCINNER WHERE DCINNER.DAYCLOSUREID = UDC.DAYCLOSUREID            
   AND DCINNER.BRANCHCOUNTERID = UDC.BRANCHCOUNTERID            
  )  
  
  UPDATE BC
  SET 
	BC.DAYCLOSUREID = MAXDC.MAXDAYCLOSUREID
	, UPDATEDDATE = GETDATE()
  FROM 
	BRANCHCOUNTER BC
	INNER JOIN 
		(
			SELECT DC.BRANCHCOUNTERID, MAX(DC.DAYCLOSUREID) AS MAXDAYCLOSUREID 
			FROM @DayClosure DC 
			GROUP BY DC.BRANCHCOUNTERID
		) MAXDC ON MAXDC.BRANCHCOUNTERID = BC.COUNTERID
	WHERE ISNULL(BC.DAYCLOSUREID, 0) < MAXDC.MAXDAYCLOSUREID
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_DAYCLOSUREDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SYNC_CU_DAYCLOSUREDETAIL]          
(          
 @DayClosureDetail POS_DAYCLOSUREDETAILTYPE READONLY    
)          
AS          
BEGIN          
 UPDATE DCD      
 SET          
DCD.DAYCLOSUREID = UDCD.DAYCLOSUREID,      
DCD.DENOMINATIONID = UDCD.DENOMINATIONID,      
DCD.CLOSUREVALUE = UDCD.CLOSUREVALUE,      
DCD.MOPID = UDCD.MOPID,      
DCD.CREATEDDATE = UDCD.CREATEDDATE,      
DCD.UPDATEDDATE = UDCD.UPDATEDDATE,      
DCD.COUNTERID =  UDCD.COUNTERID     
 FROM           
  POS_DAYCLOSUREDETAIL DCD      
  INNER JOIN @DayClosureDetail UDCD ON UDCD.DAYCLOSUREDETAILID = DCD.DAYCLOSUREDETAILID       
  AND DCD.COUNTERID = UDCD.COUNTERID          
          
 INSERT INTO POS_DAYCLOSUREDETAIL(DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE,      
  MOPID,CREATEDDATE,UPDATEDDATE,COUNTERID)          
 SELECT DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE,      
  MOPID,CREATEDDATE,UPDATEDDATE,COUNTERID         
 FROM @DayClosureDetail UDCD        
 WHERE NOT EXISTS          
  (          
   SELECT 1 FROM POS_DAYCLOSUREDETAIL DCDINNER WHERE DCDINNER.DAYCLOSUREDETAILID = UDCD.DAYCLOSUREDETAILID        
   AND DCDINNER.COUNTERID = UDCD.COUNTERID         
  )          
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_STOCKCOUNTING]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SYNC_CU_STOCKCOUNTING]
(  
 @StockCounting STOCKCOUNTINGTYPE READONLY  
)  
AS  
BEGIN  
 UPDATE SC
 SET  
SC.BRANCHID =SCN.BRANCHID
,SC.CREATEDBY = SCN.CREATEDBY
,SC.CREATEDDATE = SCN.CREATEDDATE
,SC.UPDATEDBY = SCN.UPDATEDBY
,SC.UPDATEDDATE = SCN.UPDATEDDATE
,SC.DELETEDBY = SCN.DELETEDBY
,SC.DELETEDDATE = SCN.DELETEDDATE
,SC.STATUS = SCN.STATUS
 FROM   
  STOCKCOUNTING SC
  INNER JOIN @StockCounting SCN ON SCN.STOCKCOUNTINGID = SC.STOCKCOUNTINGID
  
 INSERT INTO STOCKCOUNTING(STOCKCOUNTINGID,BRANCHID,CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS)  
 SELECT STOCKCOUNTINGID,BRANCHID,CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS
 FROM @StockCounting SC
 WHERE NOT EXISTS  
  (  
   SELECT 1 FROM STOCKCOUNTING SCINNER WHERE SCINNER. STOCKCOUNTINGID = SC.STOCKCOUNTINGID 
  )  
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_STOCKCOUNTINGDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SYNC_CU_STOCKCOUNTINGDETAIL]  
(  
 @StockCountingDetail STOCKCOUNTINGDETAILTYPE READONLY  
)  
AS  
BEGIN  
 UPDATE SCD
 SET  
SCD.STOCKCOUNTINGID = USCD.STOCKCOUNTINGID
,SCD.ITEMPRICEID = USCD.ITEMPRICEID
,SCD.QUANTITY = USCD.QUANTITY
,SCD.CREATEDDATE = USCD.CREATEDDATE
,SCD.UPDATEDDATE = USCD.UPDATEDDATE
,SCD.DELETEDDATE = USCD.DELETEDDATE
 FROM   
  STOCKCOUNTINGDETAIL SCD
  INNER JOIN @StockCountingDetail USCD ON USCD.STOCKCOUNTINGDETAILID = SCD.STOCKCOUNTINGDETAILID
  
 INSERT INTO STOCKCOUNTINGDETAIL( STOCKCOUNTINGDETAILID,STOCKCOUNTINGID,ITEMPRICEID,QUANTITY,CREATEDDATE,UPDATEDDATE,DELETEDDATE)  
 SELECT STOCKCOUNTINGDETAILID,STOCKCOUNTINGID,ITEMPRICEID,QUANTITY,CREATEDDATE,UPDATEDDATE,DELETEDDATE
 FROM @StockCountingDetail USCD  
 WHERE NOT EXISTS  
  (  
   SELECT 1 FROM STOCKCOUNTINGDETAIL SCDINNER WHERE SCDINNER.STOCKCOUNTINGDETAILID = USCD.STOCKCOUNTINGDETAILID
  )  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_STOCKDISPATCH]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_SYNC_CU_STOCKDISPATCH]
(
	@StockDispatch STOCKDISPATCHTYPE READONLY
)
AS
BEGIN
	UPDATE SD
	SET								 
		SD.FROMBRANCHID				= USD.FROMBRANCHID
		, SD.TOBRANCHID				= USD.TOBRANCHID
		, SD.STATUS					= USD.STATUS
		, SD.STATUSAPPROVEDBY		= USD.STATUSAPPROVEDBY
		, SD.STATUSAPPROVEDDATE		= USD.STATUSAPPROVEDDATE
		, SD.CREATEDDATE			= USD.CREATEDDATE
		, SD.UPDATEDATE				= USD.UPDATEDATE
		, SD.DELETEDDATE			= USD.DELETEDDATE
		, SD.DISPATCHNUMBER			= USD.DISPATCHNUMBER
		, SD.CREATEDBY              = USD.CREATEDBY
		, SD.UPDATEDBY              = USD.UPDATEDBY
		, SD.DELETEDBY              = USD.DELETEDBY
	FROM
		STOCKDISPATCH SD
		INNER JOIN @StockDispatch USD ON USD.STOCKDISPATCHID = SD.STOCKDISPATCHID

	--INSERT INTO STOCKDISPATCH(STOCKDISPATCHID, FROMBRANCHID, TOBRANCHID, STATUS, STATUSAPPROVEDBY, STATUSAPPROVEDDATE, CREATEDDATE, UPDATEDATE, DELETEDDATE, DISPATCHNUMBER
	--	, CREATEDBY, UPDATEDBY, DELETEDBY)
	--SELECT STOCKDISPATCHID, FROMBRANCHID, TOBRANCHID, STATUS, STATUSAPPROVEDBY, STATUSAPPROVEDDATE, CREATEDDATE, UPDATEDATE, DELETEDDATE, DISPATCHNUMBER
	--	, CREATEDBY, UPDATEDBY, DELETEDBY
	--FROM @StockDispatch USD
	--WHERE NOT EXISTS (SELECT 1 FROM STOCKDISPATCH SDI WHERE SDI.STOCKDISPATCHID = USD.STOCKDISPATCHID)
END


GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SYNC_CU_STOCKDISPATCHDETAIL]          
(          
 @StockDispatchDetail STOCKDISPATCHDETAILTYPE READONLY          
)          
AS          
BEGIN          
 UPDATE SDD          
 SET          
  SDD.STOCKDISPATCHID = USDD.STOCKDISPATCHID          
  , SDD.ITEMPRICEID = USDD.ITEMPRICEID          
  , SDD.TRAYNUMBER = USDD.TRAYNUMBER          
  , SDD.DISPATCHQUANTITY = USDD.DISPATCHQUANTITY          
  , SDD.RECEIVEDQUANTITY = USDD.RECEIVEDQUANTITY          
  , SDD.CREATEDDATE = USDD.CREATEDDATE          
  , SDD.UPDATEDATE = USDD.UPDATEDATE          
  , SDD.DELETEDDATE = USDD.DELETEDDATE          
  , SDD.WEIGHTINKGS = USDD.WEIGHTINKGS    
  ,SDD.ISACCEPTED = USDD.ISACCEPTED
 FROM          
  STOCKDISPATCHDETAIL SDD          
  INNER JOIN @StockDispatchDetail USDD ON USDD.STOCKDISPATCHDETAILID = SDD.STOCKDISPATCHDETAILID          
  WHERE ISNULL(USDD.ISACCEPTED,0) = 1
    
MERGE STOCKSUMMARY AS Target    
USING (SELECT SD.TOBRANCHID AS BRANCHID,    
USDD.ITEMPRICEID,USDD.RECEIVEDQUANTITY,USDD.WEIGHTINKGS    
FROM @StockDispatchDetail USDD    
INNER JOIN STOCKDISPATCHDETAIL SDD ON USDD.STOCKDISPATCHDETAILID = SDD.STOCKDISPATCHDETAILID    
INNER JOIN STOCKDISPATCH SD ON USDD.STOCKDISPATCHID = SD.STOCKDISPATCHID    
WHERE ISNULL(USDD.ISACCEPTED,0) = 1
) AS Source          
ON Source.BRANCHID = Target.BRANCHID AND Source.ITEMPRICEID = Target.ITEMPRICEID    
              
WHEN MATCHED THEN UPDATE SET          
Target.INTRANSITQUANTITY = ISNULL(Target.INTRANSITQUANTITY,0) - ISNULL(Source.RECEIVEDQUANTITY,0),          
Target.INTRANSITWEIGHTINKGS = ISNULL(Target.INTRANSITWEIGHTINKGS,0) - ISNULL(Source.WEIGHTINKGS,0),    
Target.QUANTITY = ISNULL(Target.QUANTITY,0) + ISNULL(Source.RECEIVEDQUANTITY,0),          
Target.WEIGHTINKGS = ISNULL(Target.WEIGHTINKGS,0) + ISNULL(Source.WEIGHTINKGS,0);    
    
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SYNC_CU_USER]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SYNC_CU_USER]      
(      
 @User TBLUSERTYPE READONLY      
)      
AS      
BEGIN      

 UPDATE U      
 SET      
  U.ROLEID = UU.ROLEID      
  , U.REPORTINGLEADID = UU.REPORTINGLEADID      
  , U.CATEGORYID = UU.CATEGORYID      
  , U.BRANCHID = UU.BRANCHID      
  , U.USERNAME = UU.USERNAME      
  , U.PASSWORDSTRING = UU.PASSWORDSTRING      
  , U.FULLNAME = UU.FULLNAME      
  , U.CNUMBER = UU.CNUMBER      
  , U.EMAIL = UU.EMAIL      
  , U.ISOTP = UU.ISOTP      
  , U.GENDER = UU.GENDER      
  , U.DOB = UU.DOB      
  , U.CREATEDDATE = UU.CREATEDDATE      
  , U.UPDATEDDATE = UU.UPDATEDDATE      
  , U.DELETEDDATE = UU.DELETEDDATE      
 FROM      
  TBLUSER U      
  INNER JOIN @User UU ON UU.USERID = U.USERID      

END 
GO
/****** Object:  StoredProcedure [dbo].[USP_U_BREFUND]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_U_BREFUND]      
@dtbrd POS_BRDTYPE readonly,  
@BREFUNDID INT,  
@COUNTERID INT,  
@USERID INT  
    
AS      
BEGIN      

--Change BRefund status to accepted
UPDATE POS_BREFUND SET   
IsAccepted = 1,  
UPDATEDBY = @USERID,  
UPDATEDDATE = GETDATE()  
WHERE COUNTERID = @COUNTERID  
AND BREFUNDID = @BREFUNDID  

--updating accepted quantity and weight in kgs

UPDATE BRD    
SET     
BRD.ACCEPTEDQUANTITY = UBRD.ACCEPTEDQUANTITY,    
BRD.ACCEPTEDWEIGHTKGS = UBRD.ACCEPTEDWEIGHTINKGS    
FROM POS_BREFUNDDETAIL BRD    
INNER JOIN @dtbrd UBRD ON BRD.BREFUNDID = UBRD.BREFUNDID    
AND BRD.BREFUNDDETAILID = UBRD.BREFUNDDETAILID    
AND BRD.COUNTERID = UBRD.COUNTERID    
    
--Dedutcing quantity from branch quantity

MERGE STOCKSUMMARY AS Target        
 USING       
 (      
  SELECT       
   BC.BRANCHID,UBRD.ITEMPRICEID,      
   UBRD.ACCEPTEDQUANTITY AS QUANTITY,      
   UBRD.ACCEPTEDWEIGHTINKGS AS WEIGHTINKGS      
  FROM       
   @dtbrd UBRD      
   INNER JOIN BRANCHCOUNTER BC ON UBRD.COUNTERID = BC.COUNTERID) AS Source              
 ON Source.BRANCHID = Target.BRANCHID AND Source.ITEMPRICEID = Target.ITEMPRICEID    
                  
 WHEN MATCHED THEN UPDATE SET              
 Target.QUANTITY = ISNULL(Target.QUANTITY,0) - ISNULL(Source.QUANTITY,0),              
 Target.WEIGHTINKGS = ISNULL(Target.WEIGHTINKGS,0) - ISNULL(Source.WEIGHTINKGS,0);      

--adding quantity to warehouse
 MERGE STOCKSUMMARY AS Target        
 USING       
 (      
  SELECT       
   45 AS BRANCHID,UBRD.ITEMPRICEID,      
   UBRD.ACCEPTEDQUANTITY AS QUANTITY,      
   UBRD.ACCEPTEDWEIGHTINKGS AS WEIGHTINKGS
  FROM       
   @dtbrd UBRD 
   INNER JOIN ITEMPRICE IP ON UBRD.ITEMPRICEID = IP.ITEMPRICEID
   INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID
   INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID
   WHERE I.PARENTITEMID IS NULL
   ) AS Source              
 ON Source.BRANCHID = Target.BRANCHID AND Source.ITEMPRICEID = Target.ITEMPRICEID
                  
 WHEN MATCHED THEN UPDATE SET              
 Target.QUANTITY = ISNULL(Target.QUANTITY,0) + ISNULL(Source.QUANTITY,0),
 Target.WEIGHTINKGS = ISNULL(Target.WEIGHTINKGS,0) + ISNULL(Source.WEIGHTINKGS,0);  

 --adding weight in kgs to warehouse

 MERGE STOCKSUMMARY AS Target        
 USING
 (
  SELECT
   45 AS BRANCHID,UBRD.ITEMPRICEID,
   UBRD.ACCEPTEDQUANTITY AS QUANTITY,
   UBRD.ACCEPTEDWEIGHTINKGS AS WEIGHTINKGS,
   I.PARENTITEMID,
   I.ISOPENITEM,
   U.MULTIPLIER,
   PIP.ITEMPRICEID AS PARENTITEMPRICEID
  FROM
   @dtbrd UBRD
   INNER JOIN ITEMPRICE IP ON UBRD.ITEMPRICEID = IP.ITEMPRICEID
   INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID
   INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID
   INNER JOIN ITEM PITEM ON I.PARENTITEMID = PITEM.ITEMID
   INNER JOIN ITEMCODE PIC ON PITEM.ITEMID = PIC.ITEMID
   INNER JOIN ITEMPRICE PIP ON PIC.ITEMCODEID = PIP.ITEMCODEID
   LEFT JOIN  UOM  U ON I.UOMID = U.UOMID
   WHERE I.PARENTITEMID IS NOT NULL
   ) AS Source
 ON Source.BRANCHID = Target.BRANCHID AND Source.PARENTITEMPRICEID = Target.ITEMPRICEID

 WHEN MATCHED THEN UPDATE SET
 Target.QUANTITY = 0,
 Target.WEIGHTINKGS = CASE WHEN Source.ISOPENITEM = 1 THEN
 ISNULL(Target.WEIGHTINKGS,0) + ISNULL(Source.WEIGHTINKGS,0) ELSE
 ISNULL(Target.WEIGHTINKGS,0) + ISNULL(Source.QUANTITY * Source.MULTIPLIER,0) END;
      
END
GO
/****** Object:  StoredProcedure [dbo].[USP_U_CHANGEPASSWORD]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_U_CHANGEPASSWORD]  
@UserID int,  
@PasswordString varchar(100)  
AS  
BEGIN  
  
UPDATE TBLUSER SET PASSWORDSTRING = @PasswordString,  
ISOTP = 1, UPDATEDDATE = GETDATE()
WHERE USERID = @UserID  
  
END
GO
/****** Object:  StoredProcedure [dbo].[USP_U_PASSWORD]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_U_PASSWORD]
@USERID INT,    
@PASSWORDSTRING nvarchar(500)    
as    
begin    
    
update TBLUSER set PASSWORDSTRING = @PASSWORDSTRING,  
ISOTP = 0  
where USERID = @USERID
    
SELECT                                
U.USERID,                                
U.USERNAME,                                
U.FULLNAME,                                
U.PASSWORDSTRING
FROM TBLUSER U           
WHERE U.USERID= @USERID
    
end  
GO
/****** Object:  StoredProcedure [dbo].[USP_U_STOCKDISPATCH]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_U_STOCKDISPATCH]              
@STOCKDISPATCHID INT              
AS              
BEGIN              

--Update stock stockdispatch status              
UPDATE STOCKDISPATCH SET STATUS = 1  , UPDATEDATE = GETDATE()            
WHERE STOCKDISPATCHID = @STOCKDISPATCHID              

--update branch stock
DECLARE @FROMBRANCHID INT ,@TOBRANCHID INT      
SELECT @FROMBRANCHID = FROMBRANCHID, @TOBRANCHID = TOBRANCHID     
FROM STOCKDISPATCH WHERE STOCKDISPATCHID = @STOCKDISPATCHID    
      
MERGE STOCKSUMMARY AS Target      
USING (SELECT @TOBRANCHID AS BRANCHID,ITEMPRICEID,      
SUM(DISPATCHQUANTITY) AS DISPATCHQUANTITY,      
SUM(WEIGHTINKGS) AS WEIGHTINKGS FROM STOCKDISPATCHDETAIL      
WHERE STOCKDISPATCHID = @STOCKDISPATCHID      
GROUP BY ITEMPRICEID) AS Source      
ON Source.BRANCHID = Target.BRANCHID AND Source.ITEMPRICEID = Target.ITEMPRICEID      
          
WHEN NOT MATCHED BY Target THEN      
INSERT (BRANCHID,ITEMPRICEID,INTRANSITQUANTITY,    
INTRANSITWEIGHTINKGS,QUANTITY,WEIGHTINKGS)      
VALUES(Source.BRANCHID,Source.ITEMPRICEID, ISNULL(Source.DISPATCHQUANTITY,0),    
ISNULL(Source.WEIGHTINKGS,0),0,0)      
          
WHEN MATCHED THEN UPDATE SET      
    Target.INTRANSITQUANTITY = ISNULL(Target.INTRANSITQUANTITY,0) + ISNULL(Source.DISPATCHQUANTITY,0),      
    Target.INTRANSITWEIGHTINKGS = ISNULL(Target.INTRANSITWEIGHTINKGS,0) + ISNULL(Source.WEIGHTINKGS,0);      

--update warehouse stock

MERGE STOCKSUMMARY AS Target      
USING (
SELECT 
@FROMBRANCHID AS BRANCHID,
SDD.ITEMPRICEID,SUM(DISPATCHQUANTITY) AS DISPATCHQUANTITY,      
SUM(WEIGHTINKGS) AS WEIGHTINKGS 
FROM STOCKDISPATCHDETAIL SDD 
INNER JOIN ITEMPRICE IP ON SDD.ITEMPRICEID = IP.ITEMPRICEID
INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID
WHERE STOCKDISPATCHID = @STOCKDISPATCHID 
AND I.PARENTITEMID IS NULL GROUP BY SDD.ITEMPRICEID) AS Source      
ON Source.BRANCHID = Target.BRANCHID AND Source.ITEMPRICEID = Target.ITEMPRICEID      
          
WHEN NOT MATCHED BY Target THEN      
      
INSERT (BRANCHID,ITEMPRICEID,INTRANSITQUANTITY,      
INTRANSITWEIGHTINKGS,QUANTITY,WEIGHTINKGS)      
VALUES(Source.BRANCHID,Source.ITEMPRICEID,     
0,0,ISNULL(-Source.DISPATCHQUANTITY,0),ISNULL(-Source.WEIGHTINKGS,0))      
      
WHEN MATCHED THEN UPDATE SET      
Target.QUANTITY = isnull(Target.QUANTITY,0) - ISNULL(Source.DISPATCHQUANTITY,0),      
Target.WEIGHTINKGS = isnull(Target.WEIGHTINKGS,0) - ISNULL(Source.WEIGHTINKGS,0);      

--update warehouse weight in kgs stock

MERGE STOCKSUMMARY AS Target      
USING (
SELECT BRANCHID
,SDD.ITEMPRICEID
,DISPATCHQUANTITY
,WEIGHTINKGS
,I.PARENTITEMID
,I.ISOPENITEM
,U.MULTIPLIER
,PIP.ITEMPRICEID AS PARENTITEMPRICEID FROM 
(SELECT 
@FROMBRANCHID AS BRANCHID,
SDD.ITEMPRICEID,SUM(DISPATCHQUANTITY) AS DISPATCHQUANTITY,      
SUM(WEIGHTINKGS) AS WEIGHTINKGS
FROM STOCKDISPATCHDETAIL SDD
WHERE STOCKDISPATCHID = @STOCKDISPATCHID GROUP BY SDD.ITEMPRICEID) SDD 
INNER JOIN ITEMPRICE IP ON SDD.ITEMPRICEID = IP.ITEMPRICEID
INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID
INNER JOIN UOM U ON I.UOMID = U.UOMID
INNER JOIN ITEM PITEM ON I.PARENTITEMID = PITEM.ITEMID
INNER JOIN ITEMCODE PIC ON PITEM.ITEMID = PIC.ITEMID
INNER JOIN ITEMPRICE PIP ON PIC.ITEMCODEID = PIP.ITEMCODEID
WHERE I.PARENTITEMID IS NULL) AS Source      
ON Source.BRANCHID = Target.BRANCHID AND Source.PARENTITEMPRICEID = Target.ITEMPRICEID      
          
WHEN NOT MATCHED BY Target THEN      
      
INSERT (BRANCHID,ITEMPRICEID,INTRANSITQUANTITY,      
INTRANSITWEIGHTINKGS,QUANTITY,WEIGHTINKGS)      
VALUES(Source.BRANCHID,Source.ITEMPRICEID,     
0,0,0,CASE WHEN Source.ISOPENITEM = 1 THEN ISNULL(Source.WEIGHTINKGS,0) ELSE  
 ISNULL(Source.DISPATCHQUANTITY * Source.MULTIPLIER,0) END)      
      
WHEN MATCHED THEN UPDATE SET      
Target.QUANTITY = 0,  
 Target.WEIGHTINKGS = CASE WHEN Source.ISOPENITEM = 1 THEN  
 ISNULL(Target.WEIGHTINKGS,0) + ISNULL(Source.WEIGHTINKGS,0) ELSE  
 ISNULL(Target.WEIGHTINKGS,0) + ISNULL(Source.DISPATCHQUANTITY * Source.MULTIPLIER,0) END;   
  
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_U_STOCKENTRY]    Script Date: 12-03-2022 11:18:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_U_STOCKENTRY]                
@STOCKENTRYID INT ,          
@TCS DECIMAL(10,2) = 0,          
@DISCOUNTPER DECIMAL(10,2) = 0,          
@DISCOUNTFLAT DECIMAL(10,2) = 0,          
@EXPENSES DECIMAL(10,2) = 0,          
@TRANSPORT DECIMAL(10,2)= 0          
          
AS                
BEGIN                
                
UPDATE STOCKENTRY SET STATUS = 1,          
TCS = @TCS,          
DISCOUNTPER = @DISCOUNTPER,          
DISCOUNT = @DISCOUNTFLAT,          
EXPENSES = @EXPENSES,          
TRANSPORT = @TRANSPORT          
WHERE STOCKENTRYID = @STOCKENTRYID               
          
DECLARE @WareHouseID INT              
SELECT @WareHouseID = BRANCHID FROM BRANCH WHERE ISWAREHOUSE = 1              

MERGE STOCKSUMMARY AS Target
USING (SELECT @WareHouseID as BRANCHID,ITEMPRICEID ,SUM(QUANTITY) AS QUANTITY,
SUM(WEIGHTINKGS) AS WEIGHTINKGS FROM (
SELECT IP.ITEMPRICEID,SED.QUANTITY,SED.WEIGHTINKGS FROM STOCKENTRYDETAIL SED
INNER JOIN ITEMCOSTPRICE ICP ON SED.ITEMCOSTPRICEID = ICP.ITEMCOSTPRICEID      
INNER JOIN ITEMPRICE IP ON ICP.ITEMPRICEID = IP.ITEMPRICEID 
WHERE STOCKENTRYID = @STOCKENTRYID) AS STOCK GROUP BY ITEMPRICEID) AS Source
ON Source.BRANCHID = Target.BRANCHID AND Source.ITEMPRICEID = Target.ITEMPRICEID
    
WHEN NOT MATCHED BY Target THEN
INSERT (BRANCHID,ITEMPRICEID,QUANTITY,WEIGHTINKGS)
VALUES(Source.BRANCHID,Source.ITEMPRICEID, Source.QUANTITY,Source.WEIGHTINKGS)
    
WHEN MATCHED THEN UPDATE SET
    Target.QUANTITY	= Target.QUANTITY + Source.QUANTITY,
    Target.WEIGHTINKGS = Target.WEIGHTINKGS + Source.WEIGHTINKGS;
         
END
GO
USE [master]
GO
ALTER DATABASE [NSRetail] SET  READ_WRITE 
GO
