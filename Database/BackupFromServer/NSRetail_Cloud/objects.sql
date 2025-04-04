USE [NSRetail_Cloud]
GO
/****** Object:  StoredProcedure [dbo].[USP_U_ENTITYSYNCTIME]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_U_ENTITYSYNCTIME]
GO
/****** Object:  StoredProcedure [dbo].[USP_U_BRANCHCOUNTER_HDDNO]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_U_BRANCHCOUNTER_HDDNO]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_SHOWSYNC]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_SHOWSYNC]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_POS_IMPORTDATA]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_POS_IMPORTDATA]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_GETSYNCDATA]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_GETSYNCDATA]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_GETSYNC]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_GETSYNC]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BRANCHCOUNTER]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_BRANCHCOUNTER]
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BRANCH]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_R_BRANCH]
GO
/****** Object:  StoredProcedure [dbo].[USP_D_SYNCSTATUS]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_D_SYNCSTATUS]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_USER]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_USER]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_UOM]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_UOM]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_TBLCATEGORY]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_TBLCATEGORY]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_STOCKDISPATCHDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_STOCKDISPATCH]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_STOCKDISPATCH]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ROLE]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_ROLE]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_DAYSEQUENCE]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_POS_DAYSEQUENCE]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_DAYCLOSUREDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_POS_DAYCLOSUREDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_DAYCLOSURE]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_POS_DAYCLOSURE]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_CREFUND]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_POS_CREFUND]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BREFUNDDETAL]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_POS_BREFUNDDETAL]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BREFUND]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_POS_BREFUND]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BILLMOPDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_POS_BILLMOPDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BILLDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_POS_BILLDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BILL]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_POS_BILL]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFERTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_OFFERTYPE]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFERITEMMAP]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_OFFERITEMMAP]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFERBRANCH]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_OFFERBRANCH]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFER]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_OFFER]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_MOP]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_MOP]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMPRICE]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_ITEMPRICE]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMGROUPDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_ITEMGROUPDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMGROUP]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_ITEMGROUP]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMCODE]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_ITEMCODE]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEM]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_ITEM]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_GSTDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_GSTDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_DENOMINATION]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_DENOMINATION]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_BRANCHCOUNTER]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_BRANCHCOUNTER]
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_BRANCH]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_BRANCH]
GO
/****** Object:  StoredProcedure [dbo].[CLOUD_USP_D_STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[CLOUD_USP_D_STOCKDISPATCHDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[CLOUD_USP_D_STOCKCOUNTINGDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[CLOUD_USP_D_STOCKCOUNTINGDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[CLOUD_USP_CU_STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[CLOUD_USP_CU_STOCKDISPATCHDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[CLOUD_USP_CU_STOCKDISPATCH]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[CLOUD_USP_CU_STOCKDISPATCH]
GO
/****** Object:  StoredProcedure [dbo].[CLOUD_USP_CU_STOCKCOUNTINGDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[CLOUD_USP_CU_STOCKCOUNTINGDETAIL]
GO
/****** Object:  StoredProcedure [dbo].[CLOUD_USP_CU_STOCKCOUNTING]    Script Date: 12-03-2022 11:25:00 ******/
DROP PROCEDURE IF EXISTS [dbo].[CLOUD_USP_CU_STOCKCOUNTING]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTITYSYNCSTATUS]') AND type in (N'U'))
ALTER TABLE [dbo].[ENTITYSYNCSTATUS] DROP CONSTRAINT IF EXISTS [CK__ENTITYSYN__SYNCD__42ACE4D4]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTITYSYNCSTATUS]') AND type in (N'U'))
ALTER TABLE [dbo].[ENTITYSYNCSTATUS] DROP CONSTRAINT IF EXISTS [CK__ENTITYSYN__LOCAT__41B8C09B]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTITYSYNCORDER]') AND type in (N'U'))
ALTER TABLE [dbo].[ENTITYSYNCORDER] DROP CONSTRAINT IF EXISTS [CK__ENTITYSYN__SYNCD__4C364F0E]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTITYSYNCORDER]') AND type in (N'U'))
ALTER TABLE [dbo].[ENTITYSYNCORDER] DROP CONSTRAINT IF EXISTS [CK__ENTITYSYN__LOCAT__4B422AD5]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ENTITYSYNCORDER]') AND type in (N'U'))
ALTER TABLE [dbo].[ENTITYSYNCORDER] DROP CONSTRAINT IF EXISTS [FK__ENTITYSYN__ENTIT__4A4E069C]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BRANCHCOUNTER]') AND type in (N'U'))
ALTER TABLE [dbo].[BRANCHCOUNTER] DROP CONSTRAINT IF EXISTS [FK__BRANCHCOU__BRANC__12FDD1B2]
GO
/****** Object:  Table [dbo].[UOM]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[UOM]
GO
/****** Object:  Table [dbo].[TBLUSER]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[TBLUSER]
GO
/****** Object:  Table [dbo].[TBLROLE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[TBLROLE]
GO
/****** Object:  Table [dbo].[TBLMOP]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[TBLMOP]
GO
/****** Object:  Table [dbo].[TBLCATEGORY]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[TBLCATEGORY]
GO
/****** Object:  Table [dbo].[STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[STOCKDISPATCHDETAIL]
GO
/****** Object:  Table [dbo].[STOCKDISPATCH]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[STOCKDISPATCH]
GO
/****** Object:  Table [dbo].[POS_DENOMINATION]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[POS_DENOMINATION]
GO
/****** Object:  Table [dbo].[POS_DAYSEQUENCE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[POS_DAYSEQUENCE]
GO
/****** Object:  Table [dbo].[POS_DAYCLOSUREDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[POS_DAYCLOSUREDETAIL]
GO
/****** Object:  Table [dbo].[POS_DAYCLOSURE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[POS_DAYCLOSURE]
GO
/****** Object:  Table [dbo].[POS_CREFUND]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[POS_CREFUND]
GO
/****** Object:  Table [dbo].[POS_BREFUNDDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[POS_BREFUNDDETAIL]
GO
/****** Object:  Table [dbo].[POS_BREFUND]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[POS_BREFUND]
GO
/****** Object:  Table [dbo].[POS_BILLMOPDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[POS_BILLMOPDETAIL]
GO
/****** Object:  Table [dbo].[POS_BILLDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[POS_BILLDETAIL]
GO
/****** Object:  Table [dbo].[POS_BILL]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[POS_BILL]
GO
/****** Object:  Table [dbo].[OFFERTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[OFFERTYPE]
GO
/****** Object:  Table [dbo].[OFFERITEMMAP]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[OFFERITEMMAP]
GO
/****** Object:  Table [dbo].[OFFERBRANCH]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[OFFERBRANCH]
GO
/****** Object:  Table [dbo].[OFFER]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[OFFER]
GO
/****** Object:  Table [dbo].[ITEMPRICE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[ITEMPRICE]
GO
/****** Object:  Table [dbo].[ITEMGROUPDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[ITEMGROUPDETAIL]
GO
/****** Object:  Table [dbo].[ITEMGROUP]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[ITEMGROUP]
GO
/****** Object:  Table [dbo].[ITEMCODE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[ITEMCODE]
GO
/****** Object:  Table [dbo].[ITEM]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[ITEM]
GO
/****** Object:  Table [dbo].[GSTDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[GSTDETAIL]
GO
/****** Object:  Table [dbo].[ENTITYSYNCSTATUS]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[ENTITYSYNCSTATUS]
GO
/****** Object:  Table [dbo].[ENTITYSYNCORDER]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[ENTITYSYNCORDER]
GO
/****** Object:  Table [dbo].[ENTITY]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[ENTITY]
GO
/****** Object:  Table [dbo].[CLOUD_STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[CLOUD_STOCKDISPATCHDETAIL]
GO
/****** Object:  Table [dbo].[CLOUD_STOCKDISPATCH]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[CLOUD_STOCKDISPATCH]
GO
/****** Object:  Table [dbo].[CLOUD_STOCKCOUNTINGDETAIL]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[CLOUD_STOCKCOUNTINGDETAIL]
GO
/****** Object:  Table [dbo].[CLOUD_STOCKCOUNTING]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[CLOUD_STOCKCOUNTING]
GO
/****** Object:  Table [dbo].[BRANCHCOUNTER]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[BRANCHCOUNTER]
GO
/****** Object:  Table [dbo].[BRANCH]    Script Date: 12-03-2022 11:25:00 ******/
DROP TABLE IF EXISTS [dbo].[BRANCH]
GO
/****** Object:  UserDefinedTableType [dbo].[UOMTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[UOMTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[TBLUSERTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[TBLUSERTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[TBLROLETYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[TBLROLETYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[TBLMOPTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[TBLMOPTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[TBLCATEGORYTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[TBLCATEGORYTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[STOCKDISPATCHTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[STOCKDISPATCHTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[STOCKDISPATCHDETAILTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[STOCKDISPATCHDETAILTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[STOCKCOUNTINGTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[STOCKCOUNTINGTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_DENOMINATIONTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[POS_DENOMINATIONTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_DAYSEQUENCETYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[POS_DAYSEQUENCETYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_DAYCLOSURETYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[POS_DAYCLOSURETYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_DAYCLOSUREDETAILTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[POS_DAYCLOSUREDETAILTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_CREFUNDTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[POS_CREFUNDTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BREFUNDTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[POS_BREFUNDTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BREFUNDDETAILTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[POS_BREFUNDDETAILTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BILLTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[POS_BILLTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BILLMOPDETAILTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[POS_BILLMOPDETAILTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BILLDETAILTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[POS_BILLDETAILTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[OFFERTYPETYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[OFFERTYPETYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[OFFERTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[OFFERTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[OFFERITEMMAPTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[OFFERITEMMAPTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[OFFERBRANCHTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[OFFERBRANCHTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[ITEMTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[ITEMTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[ITEMPRICETYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[ITEMPRICETYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[ITEMGROUPTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[ITEMGROUPTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[ITEMGROUPDETAILTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[ITEMGROUPDETAILTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[ITEMCODETYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[ITEMCODETYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[GSTDETAILTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[GSTDETAILTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[BRANCHTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[BRANCHTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[BRANCHCOUNTERTYPE]    Script Date: 12-03-2022 11:25:00 ******/
DROP TYPE IF EXISTS [dbo].[BRANCHCOUNTERTYPE]
GO
USE [master]
GO
/****** Object:  Database [NSRetail_Cloud]    Script Date: 12-03-2022 11:25:00 ******/
DROP DATABASE IF EXISTS [NSRetail_Cloud]
GO
/****** Object:  Database [NSRetail_Cloud]    Script Date: 12-03-2022 11:25:00 ******/
CREATE DATABASE [NSRetail_Cloud]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NSRetail_Cloud', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NSRetail_Cloud.mdf' , SIZE = 35840KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'NSRetail_Cloud_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NSRetail_Cloud_log.ldf' , SIZE = 52416KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [NSRetail_Cloud] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NSRetail_Cloud].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NSRetail_Cloud] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET ARITHABORT OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NSRetail_Cloud] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NSRetail_Cloud] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NSRetail_Cloud] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NSRetail_Cloud] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NSRetail_Cloud] SET  MULTI_USER 
GO
ALTER DATABASE [NSRetail_Cloud] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NSRetail_Cloud] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NSRetail_Cloud] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NSRetail_Cloud] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [NSRetail_Cloud] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NSRetail_Cloud] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NSRetail_Cloud', N'ON'
GO
ALTER DATABASE [NSRetail_Cloud] SET QUERY_STORE = OFF
GO
USE [NSRetail_Cloud]
GO
/****** Object:  UserDefinedTableType [dbo].[BRANCHCOUNTERTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[BRANCHCOUNTERTYPE] AS TABLE(
	[COUNTERID] [int] NOT NULL,
	[COUNTERNAME] [nvarchar](50) NULL,
	[BRANCHID] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[DAYCLOSUREID] [int] NULL,
	[BRANCHREFUNDID] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[BRANCHTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[BRANCHTYPE] AS TABLE(
	[BRANCHID] [int] NOT NULL,
	[BRANCHNAME] [varchar](100) NOT NULL,
	[BRANCHCODE] [varchar](5) NOT NULL,
	[ADDRESS] [varchar](500) NULL,
	[PHONENO] [varchar](20) NULL,
	[LANDLINE] [varchar](20) NULL,
	[EMAILID] [varchar](50) NULL,
	[SUPERVISORID] [int] NULL,
	[ISWAREHOUSE] [bit] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[STATEID] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[GSTDETAILTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[GSTDETAILTYPE] AS TABLE(
	[GSTID] [int] NOT NULL,
	[GSTCODE] [varchar](20) NOT NULL,
	[CGST] [decimal](5, 2) NULL,
	[SGST] [decimal](5, 2) NULL,
	[IGST] [decimal](5, 2) NULL,
	[CESS] [decimal](5, 2) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ITEMCODETYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[ITEMCODETYPE] AS TABLE(
	[ITEMCODEID] [int] NOT NULL,
	[ITEMID] [int] NOT NULL,
	[ITEMCODE] [varchar](20) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[FREEITEMCODEID] [int] NULL,
	[HSNCODE] [varchar](10) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ITEMGROUPDETAILTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[ITEMGROUPDETAILTYPE] AS TABLE(
	[ITEMGROUPDETAILID] [int] NOT NULL,
	[ITEMGROUPID] [int] NULL,
	[ITEMCODEID] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ITEMGROUPTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[ITEMGROUPTYPE] AS TABLE(
	[ITEMGROUPID] [int] NOT NULL,
	[GROUPNAME] [varchar](100) NULL,
	[ISACTIVE] [bit] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ITEMPRICETYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[ITEMPRICETYPE] AS TABLE(
	[ITEMPRICEID] [int] NOT NULL,
	[ITEMCODEID] [int] NOT NULL,
	[SALEPRICE] [decimal](7, 2) NULL,
	[MRP] [decimal](7, 2) NULL,
	[GSTID] [int] NOT NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[ITEMTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[ITEMTYPE] AS TABLE(
	[ITEMID] [int] NULL,
	[SKUCODE] [varchar](10) NULL,
	[ITEMNAME] [varchar](100) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[ISOPENITEM] [bit] NULL,
	[PARENTITEMID] [int] NULL,
	[UOMID] [int] NULL,
	[CATEGORYID] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[OFFERBRANCHTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[OFFERBRANCHTYPE] AS TABLE(
	[OFFERBRANCHID] [int] NOT NULL,
	[OFFERID] [int] NULL,
	[BRANCHID] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[OFFERITEMMAPTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[OFFERITEMMAPTYPE] AS TABLE(
	[OFFERITEMMAPID] [int] NOT NULL,
	[OFFERID] [int] NULL,
	[ITEMCODEID] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[OFFERTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[OFFERTYPE] AS TABLE(
	[OFFERID] [int] NOT NULL,
	[OFFERNAME] [varchar](100) NULL,
	[OFFERCODE] [varchar](10) NULL,
	[STARTDATE] [datetime] NULL,
	[ENDDATE] [datetime] NULL,
	[OFFERVALUE] [decimal](10, 2) NULL,
	[OFFERTYPEID] [int] NULL,
	[CATEGORYID] [int] NULL,
	[ITEMGROUPID] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[ISACTIVE] [bit] NULL,
	[APPLIESTOID] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[OFFERTYPETYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[OFFERTYPETYPE] AS TABLE(
	[OFFERTYPEID] [int] NOT NULL,
	[OFFERTYPENAME] [varchar](50) NULL,
	[OFFERTYPECODE] [varchar](10) NULL,
	[BUYQUANTITY] [int] NULL,
	[FREEQUANTITY] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BILLDETAILTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[POS_BILLDETAILTYPE] AS TABLE(
	[BILLDETAILID] [int] NOT NULL,
	[BILLID] [int] NOT NULL,
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
/****** Object:  UserDefinedTableType [dbo].[POS_BILLMOPDETAILTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[POS_BILLMOPDETAILTYPE] AS TABLE(
	[BILLMOPDETAILID] [int] NOT NULL,
	[BILLID] [int] NOT NULL,
	[MOPID] [int] NOT NULL,
	[MOPVALUE] [decimal](11, 2) NOT NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DAYCLOSUREID] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BILLTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[POS_BILLTYPE] AS TABLE(
	[BILLID] [int] NOT NULL,
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
	[DAYCLOSUREID] [int] NULL,
	[ROUNDING] [decimal](3, 2) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BREFUNDDETAILTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[POS_BREFUNDDETAILTYPE] AS TABLE(
	[BREFUNDDETAILID] [int] NOT NULL,
	[BREFUNDID] [int] NULL,
	[ITEMPRICEID] [int] NULL,
	[QUANTITY] [int] NULL,
	[WEIGHTINKGS] [decimal](10, 2) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[SNO] [int] NULL,
	[TRAYNUMBER] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BREFUNDTYPE]    Script Date: 12-03-2022 11:25:01 ******/
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
	[BREFUNDNUMBER] [nvarchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_CREFUNDTYPE]    Script Date: 12-03-2022 11:25:01 ******/
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
	[DAYCLOSUREID] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_DAYCLOSUREDETAILTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[POS_DAYCLOSUREDETAILTYPE] AS TABLE(
	[DAYCLOSUREDETAILID] [int] NOT NULL,
	[DAYCLOSUREID] [int] NULL,
	[DENOMINATIONID] [int] NULL,
	[CLOSUREVALUE] [decimal](10, 2) NULL,
	[MOPID] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_DAYCLOSURETYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[POS_DAYCLOSURETYPE] AS TABLE(
	[DAYCLOSUREID] [int] NOT NULL,
	[CLOSUREDATE] [datetime] NULL,
	[BRANCHCOUNTERID] [int] NULL,
	[OPENINGBALANCE] [decimal](10, 2) NULL,
	[CLOSINGBALANCE] [decimal](10, 2) NULL,
	[CLOSINGDIFFERENCE] [decimal](10, 2) NULL,
	[CLOSEDBY] [int] NULL,
	[REFUNDAMOUNT] [decimal](10, 2) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_DAYSEQUENCETYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[POS_DAYSEQUENCETYPE] AS TABLE(
	[OPENDATE] [datetime] NULL,
	[LASTUSEDBILLNUM] [varchar](20) NULL,
	[ISCLOSED] [bit] NULL,
	[BRANCHCOUNTERID] [int] NULL,
	[LASTBILLID] [int] NULL,
	[DAYSEQUENCEID] [int] NOT NULL,
	[OPENBILLID] [int] NULL,
	[CREATEDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[POS_DENOMINATIONTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[POS_DENOMINATIONTYPE] AS TABLE(
	[DENOMINATIONID] [int] NOT NULL,
	[DISPLAYVALUE] [varchar](20) NULL,
	[MULTIPLIER] [decimal](6, 2) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[STOCKCOUNTINGTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[STOCKCOUNTINGTYPE] AS TABLE(
	[STOCKCOUNTINGID] [int] NOT NULL,
	[BRANCHID] [int] NOT NULL,
	[ITEMCODEID] [int] NULL,
	[ITEMID] [int] NULL,
	[ITEMCODE] [varchar](20) NULL,
	[ITEMNAME] [varchar](100) NULL,
	[ITEMPRICEID] [int] NULL,
	[MRP] [decimal](7, 2) NULL,
	[SALEPRICE] [decimal](7, 2) NULL,
	[QUANTITY] [int] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[STOCKDISPATCHDETAILTYPE]    Script Date: 12-03-2022 11:25:01 ******/
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
/****** Object:  UserDefinedTableType [dbo].[STOCKDISPATCHTYPE]    Script Date: 12-03-2022 11:25:01 ******/
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
/****** Object:  UserDefinedTableType [dbo].[TBLCATEGORYTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[TBLCATEGORYTYPE] AS TABLE(
	[CATEGORYID] [int] NOT NULL,
	[CATEGORYNAME] [nvarchar](50) NOT NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[ALLOWOPENITEMS] [bit] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TBLMOPTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[TBLMOPTYPE] AS TABLE(
	[MOPID] [int] NOT NULL,
	[MOPNAME] [nvarchar](50) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TBLROLETYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[TBLROLETYPE] AS TABLE(
	[ROLEID] [int] NOT NULL,
	[ROLENAME] [nvarchar](50) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TBLUSERTYPE]    Script Date: 12-03-2022 11:25:01 ******/
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
/****** Object:  UserDefinedTableType [dbo].[UOMTYPE]    Script Date: 12-03-2022 11:25:01 ******/
CREATE TYPE [dbo].[UOMTYPE] AS TABLE(
	[UOMID] [int] NOT NULL,
	[DISPLAYVALUE] [varchar](20) NOT NULL,
	[BASEUOMID] [int] NULL,
	[MULTIPLIER] [decimal](5, 2) NOT NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
)
GO
/****** Object:  Table [dbo].[BRANCH]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BRANCH](
	[BRANCHID] [int] NOT NULL,
	[BRANCHNAME] [varchar](100) NOT NULL,
	[BRANCHCODE] [varchar](5) NOT NULL,
	[ADDRESS] [varchar](500) NULL,
	[PHONENO] [varchar](20) NULL,
	[LANDLINE] [varchar](20) NULL,
	[EMAILID] [varchar](50) NULL,
	[SUPERVISORID] [int] NULL,
	[ISWAREHOUSE] [bit] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[STATEID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BRANCHID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BRANCHCOUNTER]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BRANCHCOUNTER](
	[COUNTERID] [int] NOT NULL,
	[COUNTERNAME] [nvarchar](50) NULL,
	[BRANCHID] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[DAYCLOSUREID] [int] NULL,
	[BRANCHREFUNDID] [int] NULL,
	[HDDSNO] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[COUNTERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CLOUD_STOCKCOUNTING]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLOUD_STOCKCOUNTING](
	[STOCKCOUNTINGID] [int] IDENTITY(1,1) NOT NULL,
	[BRANCHID] [int] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[STATUS] [bit] NULL,
 CONSTRAINT [PK_POS_STOCKCOUNTING] PRIMARY KEY CLUSTERED 
(
	[STOCKCOUNTINGID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CLOUD_STOCKCOUNTINGDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLOUD_STOCKCOUNTINGDETAIL](
	[STOCKCOUNTINGDETAILID] [int] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[CLOUD_STOCKDISPATCH]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLOUD_STOCKDISPATCH](
	[STOCKDISPATCHID] [int] IDENTITY(1,1) NOT NULL,
	[FROMBRANCHID] [int] NOT NULL,
	[TOBRANCHID] [int] NOT NULL,
	[CATEGORYID] [int] NOT NULL,
	[STATUS] [bit] NOT NULL,
	[STATUSAPPROVEDBY] [varchar](100) NULL,
	[STATUSAPPROVEDDATE] [datetime] NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DISPATCHNUMBER] [varchar](40) NULL,
 CONSTRAINT [PK__CLOUD_ST__42273E466F014419] PRIMARY KEY CLUSTERED 
(
	[STOCKDISPATCHID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CLOUD_STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CLOUD_STOCKDISPATCHDETAIL](
	[STOCKDISPATCHDETAILID] [int] IDENTITY(1,1) NOT NULL,
	[STOCKDISPATCHID] [int] NOT NULL,
	[ITEMPRICEID] [int] NOT NULL,
	[TRAYNUMBER] [int] NULL,
	[DISPATCHQUANTITY] [int] NOT NULL,
	[RECEIVEDQUANTITY] [int] NOT NULL,
	[WEIGHTINKGS] [decimal](10, 2) NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[STOCKDISPATCHDETAILID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ENTITY]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ENTITY](
	[ENTITYID] [int] IDENTITY(1,1) NOT NULL,
	[ENTITYNAME] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ENTITYID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ENTITYSYNCORDER]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ENTITYSYNCORDER](
	[ENTITYSYNCORDERID] [int] IDENTITY(1,1) NOT NULL,
	[ENTITYID] [int] NOT NULL,
	[SYNCORDER] [int] NOT NULL,
	[LOCATIONTYPE] [varchar](15) NULL,
	[SYNCDIRECTION] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[ENTITYSYNCORDERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ENTITYSYNCSTATUS]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ENTITYSYNCSTATUS](
	[ENTITYSYNCSTATUSID] [int] IDENTITY(1,1) NOT NULL,
	[ENTITYID] [int] NOT NULL,
	[LOCATIONID] [int] NOT NULL,
	[LOCATIONTYPE] [varchar](15) NOT NULL,
	[SYNCDIRECTION] [varchar](10) NOT NULL,
	[SYNCDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ENTITYSYNCSTATUSID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GSTDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GSTDETAIL](
	[GSTID] [int] NOT NULL,
	[GSTCODE] [varchar](20) NOT NULL,
	[CGST] [decimal](5, 2) NULL,
	[SGST] [decimal](5, 2) NULL,
	[IGST] [decimal](5, 2) NULL,
	[CESS] [decimal](5, 2) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[GSTID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITEM]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ITEM](
	[ITEMID] [int] NOT NULL,
	[SKUCODE] [varchar](10) NOT NULL,
	[ITEMNAME] [varchar](100) NOT NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[ISOPENITEM] [bit] NULL,
	[PARENTITEMID] [int] NULL,
	[UOMID] [int] NULL,
	[CATEGORYID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ITEMID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITEMCODE]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ITEMCODE](
	[ITEMCODEID] [int] NOT NULL,
	[ITEMID] [int] NOT NULL,
	[ITEMCODE] [varchar](20) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[FREEITEMCODEID] [int] NULL,
	[HSNCODE] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[ITEMCODEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITEMGROUP]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ITEMGROUP](
	[ITEMGROUPID] [int] NOT NULL,
	[GROUPNAME] [varchar](100) NULL,
	[ISACTIVE] [bit] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITEMGROUPDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ITEMGROUPDETAIL](
	[ITEMGROUPDETAILID] [int] NOT NULL,
	[ITEMGROUPID] [int] NULL,
	[ITEMCODEID] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ITEMPRICE]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ITEMPRICE](
	[ITEMPRICEID] [int] NOT NULL,
	[ITEMCODEID] [int] NOT NULL,
	[SALEPRICE] [decimal](7, 2) NULL,
	[MRP] [decimal](7, 2) NULL,
	[GSTID] [int] NOT NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ITEMPRICEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OFFER]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OFFER](
	[OFFERID] [int] NOT NULL,
	[OFFERNAME] [varchar](100) NULL,
	[OFFERCODE] [varchar](10) NULL,
	[STARTDATE] [datetime] NULL,
	[ENDDATE] [datetime] NULL,
	[OFFERTYPEID] [int] NULL,
	[CATEGORYID] [int] NULL,
	[ITEMGROUPID] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[ISACTIVE] [bit] NULL,
	[APPLIESTOID] [int] NULL,
	[OFFERVALUE] [decimal](10, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OFFERBRANCH]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OFFERBRANCH](
	[OFFERBRANCHID] [int] NOT NULL,
	[OFFERID] [int] NULL,
	[BRANCHID] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OFFERITEMMAP]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OFFERITEMMAP](
	[OFFERITEMMAPID] [int] NOT NULL,
	[OFFERID] [int] NULL,
	[ITEMCODEID] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OFFERTYPE]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OFFERTYPE](
	[OFFERTYPEID] [int] NOT NULL,
	[OFFERTYPENAME] [varchar](50) NULL,
	[OFFERTYPECODE] [varchar](10) NULL,
	[BUYQUANTITY] [int] NULL,
	[FREEQUANTITY] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_BILL]    Script Date: 12-03-2022 11:25:01 ******/
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
	[DAYCLOSUREID] [int] NULL,
	[ROUNDING] [decimal](3, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_BILLDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
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
/****** Object:  Table [dbo].[POS_BILLMOPDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
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
/****** Object:  Table [dbo].[POS_BREFUND]    Script Date: 12-03-2022 11:25:01 ******/
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
	[COUNTERID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_BREFUNDDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
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
	[COUNTERID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_CREFUND]    Script Date: 12-03-2022 11:25:01 ******/
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
/****** Object:  Table [dbo].[POS_DAYCLOSURE]    Script Date: 12-03-2022 11:25:01 ******/
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
/****** Object:  Table [dbo].[POS_DAYCLOSUREDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
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
/****** Object:  Table [dbo].[POS_DAYSEQUENCE]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POS_DAYSEQUENCE](
	[OPENDATE] [datetime] NULL,
	[LASTUSEDBILLNUM] [varchar](20) NULL,
	[ISCLOSED] [bit] NULL,
	[BRANCHCOUNTERID] [int] NULL,
	[LASTBILLID] [int] NULL,
	[DAYSEQUENCEID] [int] NOT NULL,
	[OPENBILLID] [int] NULL,
	[CREATEDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POS_DENOMINATION]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POS_DENOMINATION](
	[DENOMINATIONID] [int] NOT NULL,
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
/****** Object:  Table [dbo].[STOCKDISPATCH]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STOCKDISPATCH](
	[STOCKDISPATCHID] [int] NOT NULL,
	[FROMBRANCHID] [int] NOT NULL,
	[TOBRANCHID] [int] NOT NULL,
	[STATUS] [varchar](20) NOT NULL,
	[STATUSAPPROVEDBY] [varchar](100) NULL,
	[STATUSAPPROVEDDATE] [datetime] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
	[DISPATCHNUMBER] [varchar](40) NULL,
	[CREATEDBY] [int] NULL,
	[UPDATEDBY] [int] NULL,
	[DELETEDBY] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[STOCKDISPATCHID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STOCKDISPATCHDETAIL](
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
	[ISACCEPTED] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[STOCKDISPATCHDETAILID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLCATEGORY]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLCATEGORY](
	[CATEGORYID] [int] NOT NULL,
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
/****** Object:  Table [dbo].[TBLMOP]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLMOP](
	[MOPID] [int] NOT NULL,
	[MOPNAME] [nvarchar](50) NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
 CONSTRAINT [PK_TBLMOP] PRIMARY KEY CLUSTERED 
(
	[MOPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLROLE]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLROLE](
	[ROLEID] [int] NOT NULL,
	[ROLENAME] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TBLROLE] PRIMARY KEY CLUSTERED 
(
	[ROLEID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBLUSER]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBLUSER](
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
	[DELETEDDATE] [datetime] NULL,
 CONSTRAINT [PK_TBLUSER] PRIMARY KEY CLUSTERED 
(
	[USERID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UOM]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UOM](
	[UOMID] [int] NOT NULL,
	[DISPLAYVALUE] [varchar](20) NOT NULL,
	[BASEUOMID] [int] NULL,
	[MULTIPLIER] [decimal](5, 2) NOT NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDDATE] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UOMID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BRANCHCOUNTER]  WITH CHECK ADD FOREIGN KEY([BRANCHID])
REFERENCES [dbo].[BRANCH] ([BRANCHID])
GO
ALTER TABLE [dbo].[ENTITYSYNCORDER]  WITH CHECK ADD FOREIGN KEY([ENTITYID])
REFERENCES [dbo].[ENTITY] ([ENTITYID])
GO
ALTER TABLE [dbo].[ENTITYSYNCORDER]  WITH CHECK ADD CHECK  (([LOCATIONTYPE]='BranchCounter' OR [LOCATIONTYPE]='Cloud' OR [LOCATIONTYPE]='Warehouse'))
GO
ALTER TABLE [dbo].[ENTITYSYNCORDER]  WITH CHECK ADD CHECK  (([SYNCDIRECTION]='FromCloud' OR [SYNCDIRECTION]='ToCloud'))
GO
ALTER TABLE [dbo].[ENTITYSYNCSTATUS]  WITH CHECK ADD CHECK  (([LOCATIONTYPE]='BranchCounter' OR [LOCATIONTYPE]='Cloud' OR [LOCATIONTYPE]='Warehouse'))
GO
ALTER TABLE [dbo].[ENTITYSYNCSTATUS]  WITH CHECK ADD CHECK  (([SYNCDIRECTION]='FromCloud' OR [SYNCDIRECTION]='ToCloud'))
GO
/****** Object:  StoredProcedure [dbo].[CLOUD_USP_CU_STOCKCOUNTING]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CLOUD_USP_CU_STOCKCOUNTING]    
@STOCKCOUNTINGID INT = 0,    
@BRANCHID INT,    
@USERID INT,
@CREATEDDATE DATETIME
AS    
BEGIN    

INSERT INTO CLOUD_STOCKCOUNTING (BRANCHID,CREATEDBY,CREATEDDATE)    
SELECT @BRANCHID,@USERID, @CREATEDDATE
SET @STOCKCOUNTINGID = SCOPE_IDENTITY()    
    
SELECT @STOCKCOUNTINGID    

END
GO
/****** Object:  StoredProcedure [dbo].[CLOUD_USP_CU_STOCKCOUNTINGDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CLOUD_USP_CU_STOCKCOUNTINGDETAIL]        
@STOCKCOUNTINGDETAILID INT,        
@STOCKCOUNTINGID INT,        
@ITEMPRICEID INT,        
@QUANTITY INT ,  
@CREATEDDATE DATETIME  
AS        
BEGIN        
    
IF(@STOCKCOUNTINGDETAILID > 0)        
BEGIN        
UPDATE CLOUD_STOCKCOUNTINGDETAIL        
SET QUANTITY = @QUANTITY,        
UPDATEDDATE = @CREATEDDATE  
WHERE STOCKCOUNTINGDETAILID = @STOCKCOUNTINGDETAILID        
END    
    
ELSE        
    
BEGIN        
    
SELECT @STOCKCOUNTINGDETAILID = STOCKCOUNTINGDETAILID FROM CLOUD_STOCKCOUNTINGDETAIL     
WHERE STOCKCOUNTINGID = @STOCKCOUNTINGID AND ITEMPRICEID = @ITEMPRICEID    
    
IF(@STOCKCOUNTINGDETAILID > 0)    
BEGIN    
    
update CLOUD_STOCKCOUNTINGDETAIL set QUANTITY = QUANTITY + @QUANTITY,  
UPDATEDDATE = @CREATEDDATE
where STOCKCOUNTINGDETAILID = @STOCKCOUNTINGDETAILID    
    
end    
else    
begin    
INSERT INTO CLOUD_STOCKCOUNTINGDETAIL(STOCKCOUNTINGID,ITEMPRICEID,QUANTITY,CREATEDDATE)        
SELECT @STOCKCOUNTINGID,@ITEMPRICEID,@QUANTITY,@CREATEDDATE  
        
SET @STOCKCOUNTINGDETAILID = SCOPE_IDENTITY()        
end    
END        
        
SELECT @STOCKCOUNTINGDETAILID        
        
END
GO
/****** Object:  StoredProcedure [dbo].[CLOUD_USP_CU_STOCKDISPATCH]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CLOUD_USP_CU_STOCKDISPATCH]  
@STOCKDISPATCHID INT = 0,            
@FROMBRANCHID INT,            
@TOBRANCHID INT,            
@CATEGORYID INT,          
@USERID INT,
@CREATEDDATE DATETIME
AS            
BEGIN            
      
INSERT INTO CLOUD_STOCKDISPATCH(FROMBRANCHID,TOBRANCHID,CATEGORYID,STATUS,      
CREATEDBY,CREATEDDATE)            
VALUES(@FROMBRANCHID,@TOBRANCHID,@CATEGORYID,0,      
@USERID,@CREATEDDATE)      
      
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
/****** Object:  StoredProcedure [dbo].[CLOUD_USP_CU_STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CLOUD_USP_CU_STOCKDISPATCHDETAIL]              
@STOCKDISPATCHDETAILID INT = 0,              
@STOCKDISPATCHID INT,              
@ITEMPRICEID INT,              
@TRAYNUMBER INT,              
@DISPATCHQUANTITY int,              
@WEIGHTINKGS DECIMAL(10,2),              
@USERID INT,  
@CREATEDDATE DATETIME  
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
UPDATEDATE = @CREATEDDATE  
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
WEIGHTINKGS = WEIGHTINKGS + @WEIGHTINKGS,  
UPDATEDATE = @CREATEDDATE  
WHERE STOCKDISPATCHDETAILID = @STOCKDISPATCHDETAILID       
      
END        
ELSE        
BEGIN        
    
INSERT INTO STOCKDISPATCHDETAIL(STOCKDISPATCHID,ITEMPRICEID,TRAYNUMBER,              
DISPATCHQUANTITY,RECEIVEDQUANTITY,WEIGHTINKGS,CREATEDDATE)              
VALUES(@STOCKDISPATCHID,@ITEMPRICEID,@TRAYNUMBER,              
@DISPATCHQUANTITY,@DISPATCHQUANTITY,@WEIGHTINKGS,@CREATEDDATE)              
SET @STOCKDISPATCHDETAILID = SCOPE_IDENTITY()              
    
END        
      
END              
SELECT @STOCKDISPATCHDETAILID              
END 
GO
/****** Object:  StoredProcedure [dbo].[CLOUD_USP_D_STOCKCOUNTINGDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CLOUD_USP_D_STOCKCOUNTINGDETAIL]    
@STOCKCOUNTINGDETAILID INT ,
@DELETEDDATE DATETIME = NULL
AS    
BEGIN    

UPDATE CLOUD_STOCKCOUNTINGDETAIL SET DELETEDDATE = ISNULL(@DELETEDDATE,GETDATE())
WHERE STOCKCOUNTINGDETAILID = @STOCKCOUNTINGDETAILID    
    
END
GO
/****** Object:  StoredProcedure [dbo].[CLOUD_USP_D_STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CLOUD_USP_D_STOCKDISPATCHDETAIL]  
@STOCKDISPATCHDETAILID INT
AS  
BEGIN  
  
UPDATE CLOUD_STOCKDISPATCHDETAIL SET DELETEDDATE = GETDATE()  
WHERE STOCKDISPATCHDETAILID = @STOCKDISPATCHDETAILID  
  
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_BRANCH]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_CU_BRANCH]
(
	@Branches BRANCHTYPE READONLY
)
AS
BEGIN
	UPDATE B
	SET
		B.BRANCHNAME		 = UB. BRANCHNAME
		, B.BRANCHCODE		 = UB.BRANCHCODE
		, B.ADDRESS			 = UB.ADDRESS
		, B.PHONENO			 = UB.PHONENO
		, B.LANDLINE		 = UB.LANDLINE
		, B.EMAILID			 = UB.EMAILID
		, B.SUPERVISORID	 = UB.SUPERVISORID
		, B.ISWAREHOUSE		 = UB.ISWAREHOUSE
		, B.CREATEDDATE		 = UB.CREATEDDATE
		, B.UPDATEDATE		 = UB.UPDATEDATE
		, B.DELETEDDATE		 = UB.DELETEDDATE
		, B.STATEID			 = UB.STATEID
	FROM
		BRANCH B
		INNER JOIN @Branches UB ON UB.BRANCHID = B.BRANCHID

	INSERT INTO BRANCH(BRANCHID, BRANCHNAME, BRANCHCODE, ADDRESS, PHONENO, LANDLINE, EMAILID, SUPERVISORID, ISWAREHOUSE, CREATEDDATE, UPDATEDATE, DELETEDDATE, STATEID)
	SELECT BRANCHID, BRANCHNAME, BRANCHCODE, ADDRESS, PHONENO, LANDLINE, EMAILID, SUPERVISORID, ISWAREHOUSE, CREATEDDATE, UPDATEDATE, DELETEDDATE, STATEID FROM @Branches UB
	WHERE NOT EXISTS ( SELECT 1 FROM BRANCH BINNER WHERE BINNER.BRANCHID = UB.BRANCHID)

END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_BRANCHCOUNTER]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROC [dbo].[USP_CU_BRANCHCOUNTER]
(
	@BranchCounters BRANCHCOUNTERTYPE READONLY
)
AS
BEGIN
	UPDATE BC
	SET
		BC.COUNTERNAME	 = UBC.COUNTERNAME
		, BC.BRANCHID	 = UBC.BRANCHID
		, BC.CREATEDDATE = UBC.CREATEDDATE
		, BC.UPDATEDDATE = UBC.UPDATEDDATE
		, BC.DELETEDDATE = UBC.DELETEDDATE
		, BC.DAYCLOSUREID = UBC.DAYCLOSUREID
		, BC.BRANCHREFUNDID = UBC.BRANCHREFUNDID
	FROM
		BRANCHCOUNTER BC
		INNER JOIN @BranchCounters UBC ON UBC.COUNTERID = BC.COUNTERID

	INSERT INTO BRANCHCOUNTER(COUNTERID, COUNTERNAME, BRANCHID, CREATEDDATE, UPDATEDDATE, DELETEDDATE
		, DAYCLOSUREID, BRANCHREFUNDID)
	SELECT COUNTERID, COUNTERNAME, BRANCHID, CREATEDDATE, UPDATEDDATE, DELETEDDATE
		, DAYCLOSUREID, BRANCHREFUNDID
	FROM @BranchCounters UBC
	WHERE NOT EXISTS ( SELECT 1 FROM BRANCHCOUNTER BCINNER WHERE BCINNER.COUNTERID = UBC.COUNTERID)

END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_DENOMINATION]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_DENOMINATION]
(  
 @Denomination POS_DENOMINATIONTYPE READONLY  
)  
AS  
BEGIN  
 UPDATE DEN
 SET  
DEN.DISPLAYVALUE = UDEN.DISPLAYVALUE,
DEN.MULTIPLIER = UDEN.MULTIPLIER,
DEN.CREATEDDATE = UDEN.CREATEDDATE,
DEN.UPDATEDDATE = UDEN.UPDATEDDATE
 FROM  
  POS_DENOMINATION DEN
  INNER JOIN @Denomination UDEN 
	ON DEN.DENOMINATIONID = UDEN.DENOMINATIONID  
  
  
 INSERT INTO POS_DENOMINATION(DENOMINATIONID,DISPLAYVALUE,MULTIPLIER,CREATEDDATE,UPDATEDDATE)  
 SELECT DENOMINATIONID,DISPLAYVALUE,MULTIPLIER,CREATEDDATE,UPDATEDDATE FROM @Denomination  DEN
 WHERE NOT EXISTS (SELECT 1 FROM POS_DENOMINATION IDEN WHERE DEN.DENOMINATIONID = IDEN.DENOMINATIONID)  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_GSTDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_CU_GSTDETAIL]
(
	@GSTDetails GSTDETAILTYPE READONLY
)
AS
BEGIN
	UPDATE GST
	SET
		GST.GSTCODE = UGST.GSTCODE
		, GST.CGST = UGST.CGST
		, GST.SGST = UGST.SGST
		, GST.IGST = UGST.IGST
		, GST.CESS = UGST.CESS
		, GST.CREATEDDATE = UGST.CREATEDDATE
		, GST.UPDATEDATE = UGST.UPDATEDATE
		, GST.DELETEDDATE = UGST.DELETEDDATE
	FROM
		GSTDETAIL GST
		INNER JOIN @GSTDetails UGST ON UGST.GSTID = GST.GSTID

	INSERT INTO GSTDETAIL(GSTID, GSTCODE, CGST, SGST, IGST, CESS, CREATEDDATE, UPDATEDATE, DELETEDDATE)
	SELECT GSTID, GSTCODE, CGST, SGST, IGST, CESS, CREATEDDATE, UPDATEDATE, DELETEDDATE
	FROM @GSTDetails UGST
	WHERE NOT EXISTS ( SELECT 1 FROM GSTDETAIL GSTINNER WHERE GSTINNER.GSTID = UGST.GSTID)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEM]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[USP_CU_ITEM]
(
	@Items ITEMTYPE READONLY
)
AS
BEGIN 
	UPDATE I
	SET I.SKUCODE = updatedItems.SKUCODE
		, I.ITEMNAME = updatedItems.ITEMNAME
		, I.CREATEDDATE = updatedItems.CREATEDDATE
		, I.UPDATEDATE = updatedItems.UPDATEDATE
		, I.DELETEDDATE = updatedItems.DELETEDDATE
		, I.ISOPENITEM = updatedItems.ISOPENITEM
		, I.PARENTITEMID = updatedItems.PARENTITEMID
		, I.UOMID = updatedItems.UOMID
		, I.CATEGORYID = updatedItems.CATEGORYID
	FROM
		ITEM I 
		INNER JOIN @Items updatedItems ON updatedItems.ITEMID = I.ITEMID

	INSERT INTO ITEM(ITEMID, SKUCODE, ITEMNAME, CREATEDDATE, UPDATEDATE, DELETEDDATE, ISOPENITEM, PARENTITEMID, UOMID, CATEGORYID)
	SELECT ITEMID, SKUCODE, ITEMNAME, CREATEDDATE, UPDATEDATE, DELETEDDATE, ISOPENITEM, PARENTITEMID, UOMID, CATEGORYID
	FROM @Items updatedItems
	WHERE NOT EXISTS (SELECT 1 FROM ITEM WHERE ITEMID = updatedItems.ITEMID)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMCODE]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_CU_ITEMCODE]
(
	@ItemCodes ITEMCODETYPE READONLY
)
AS
BEGIN
	UPDATE IC
	SET 
		IC.ITEMID = UPDATEDIC.ITEMID
		, IC.ITEMCODE = UPDATEDIC.ITEMCODE
		, IC.CREATEDDATE = UPDATEDIC.CREATEDDATE
		, IC.UPDATEDATE = UPDATEDIC.UPDATEDATE
		, IC.DELETEDDATE = UPDATEDIC.DELETEDDATE
		, IC.FREEITEMCODEID = UPDATEDIC.FREEITEMCODEID
		, IC.HSNCODE = UPDATEDIC.HSNCODE
	FROM 
		ITEMCODE IC 
		INNER JOIN @ItemCodes UPDATEDIC ON UPDATEDIC.ITEMCODEID = IC.ITEMCODEID

	INSERT INTO ITEMCODE(ITEMCODEID, ITEMID, ITEMCODE, CREATEDDATE, UPDATEDATE, DELETEDDATE, FREEITEMCODEID, HSNCODE)
	SELECT ITEMCODEID, ITEMID, ITEMCODE, CREATEDDATE, UPDATEDATE, DELETEDDATE, FREEITEMCODEID, HSNCODE 
	FROM @ItemCodes UPDATEDIC
	WHERE NOT EXISTS
		(
			SELECT 1 FROM ITEMCODE ICINNER WHERE ICINNER.ITEMCODEID = UPDATEDIC.ITEMCODEID
		)

END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMGROUP]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_CU_ITEMGROUP]
(
	@ItemGroups ITEMGROUPTYPE READONLY
)
AS
BEGIN
	UPDATE IG
	SET
		IG.GROUPNAME		  = UIG.GROUPNAME
		, IG.ISACTIVE		  = UIG.ISACTIVE
		, IG.CREATEDDATE	  = UIG.CREATEDDATE
		, IG.UPDATEDDATE	  = UIG.UPDATEDDATE
		, IG.DELETEDDATE	  = UIG.DELETEDDATE
	FROM					  
		ITEMGROUP IG
		INNER JOIN @ItemGroups UIG ON UIG.ITEMGROUPID = IG.ITEMGROUPID

	INSERT INTO ITEMGROUP(ITEMGROUPID, GROUPNAME, ISACTIVE, CREATEDDATE, UPDATEDDATE, DELETEDDATE)
	SELECT ITEMGROUPID, GROUPNAME, ISACTIVE, CREATEDDATE, UPDATEDDATE, DELETEDDATE
	FROM @ItemGroups UIG
	WHERE NOT EXISTS 
		(
			SELECT 1 FROM ITEMGROUP UIGINNER 
			WHERE UIGINNER.ITEMGROUPID = UIG.ITEMGROUPID
		)

END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMGROUPDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_CU_ITEMGROUPDETAIL]
(
	@ItemGroupDetails ITEMGROUPDETAILTYPE READONLY
)
AS
BEGIN

	UPDATE IGD
	SET
		IGD.ITEMGROUPID		   = UIGD.ITEMGROUPID
		, IGD.ITEMCODEID	   = UIGD.ITEMCODEID
		, IGD.CREATEDDATE	   = UIGD.CREATEDDATE
		, IGD.DELETEDDATE	   = UIGD.DELETEDDATE
	FROM					  
		ITEMGROUPDETAIL IGD
		INNER JOIN @ItemGroupDetails UIGD ON UIGD.ITEMGROUPDETAILID = IGD.ITEMGROUPDETAILID

	INSERT INTO ITEMGROUPDETAIL(ITEMGROUPDETAILID, ITEMGROUPID, ITEMCODEID, CREATEDDATE, DELETEDDATE)
	SELECT ITEMGROUPDETAILID, ITEMGROUPID, ITEMCODEID, CREATEDDATE, DELETEDDATE
	FROM @ItemGroupDetails UIG
	WHERE NOT EXISTS 
		(
			SELECT 1 FROM ITEMGROUPDETAIL UIGDINNER 
			WHERE UIGDINNER.ITEMGROUPDETAILID = UIG.ITEMGROUPDETAILID
		)

END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ITEMPRICE]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_CU_ITEMPRICE]
(
	@ItemPrices ITEMPRICETYPE READONLY
)
AS
BEGIN
	UPDATE IP
	SET
		IP.ITEMCODEID = UIP.ITEMCODEID
		, IP.SALEPRICE = UIP.SALEPRICE
		, IP.MRP = UIP.MRP
		, IP.GSTID = UIP.GSTID
		, IP.CREATEDDATE = UIP.CREATEDDATE
		, IP.UPDATEDATE = UIP.UPDATEDATE
		, IP.DELETEDDATE = UIP.DELETEDDATE
	FROM
		ITEMPRICE IP
		INNER JOIN @ItemPrices UIP ON UIP.ITEMPRICEID = IP.ITEMPRICEID

	INSERT INTO ITEMPRICE(ITEMPRICEID, ITEMCODEID, SALEPRICE, MRP, GSTID, CREATEDDATE, UPDATEDATE, DELETEDDATE)
	SELECT ITEMPRICEID, ITEMCODEID, SALEPRICE, MRP, GSTID, CREATEDDATE, UPDATEDATE, DELETEDDATE FROM @ItemPrices UIP
	WHERE NOT EXISTS
		(
			SELECT 1 FROM ITEMPRICE IPINNER WHERE IPINNER.ITEMPRICEID = UIP.ITEMPRICEID
		)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_MOP]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[USP_CU_MOP]
(
	@MOP TBLMOPTYPE READONLY
)
AS
BEGIN
	UPDATE MOP
	SET
		MOP.MOPNAME = UMOP.MOPNAME
		, MOP.CREATEDDATE = UMOP.CREATEDDATE
		, MOP.UPDATEDDATE = UMOP.UPDATEDDATE
		, MOP.DELETEDDATE = UMOP.DELETEDDATE
	FROM
		TBLMOP MOP
		INNER JOIN @MOP UMOP ON UMOP.MOPID = MOP.MOPID

	INSERT INTO TBLMOP(MOPID, MOPNAME, CREATEDDATE, UPDATEDDATE, DELETEDDATE)
	SELECT MOPID, MOPNAME, CREATEDDATE, UPDATEDDATE, DELETEDDATE
	FROM @MOP UMOP
	WHERE NOT EXISTS (SELECT 1 FROM TBLMOP IMOP WHERE IMOP.MOPID = UMOP.MOPID)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFER]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[USP_CU_OFFER]
(
	@Offers OFFERTYPE READONLY
)
AS
BEGIN

	UPDATE OFR
	SET
		OFR.OFFERNAME		   = UOFR.OFFERNAME
		, OFR.OFFERCODE		   = UOFR.OFFERCODE
		, OFR.STARTDATE		   = UOFR.STARTDATE
		, OFR.ENDDATE		   = UOFR.ENDDATE
		, OFR.OFFERVALUE	   = UOFR.OFFERVALUE
		, OFR.OFFERTYPEID	   = UOFR.OFFERTYPEID
		, OFR.CATEGORYID	   = UOFR.CATEGORYID
		, OFR.ITEMGROUPID	   = UOFR.ITEMGROUPID
		, OFR.CREATEDDATE	   = UOFR.CREATEDDATE
		, OFR.UPDATEDDATE	   = UOFR.UPDATEDDATE
		, OFR.DELETEDDATE	   = UOFR.DELETEDDATE
		, OFR.ISACTIVE		   = UOFR.ISACTIVE
		, OFR.APPLIESTOID	   = UOFR.APPLIESTOID
	FROM					  
		OFFER OFR
		INNER JOIN @Offers UOFR ON UOFR.OFFERID = OFR.OFFERID

	INSERT INTO OFFER(OFFERID, OFFERNAME, OFFERCODE, STARTDATE, ENDDATE, OFFERVALUE, OFFERTYPEID, CATEGORYID
		, ITEMGROUPID, CREATEDDATE, UPDATEDDATE, DELETEDDATE, ISACTIVE, APPLIESTOID)
	SELECT OFFERID, OFFERNAME, OFFERCODE, STARTDATE, ENDDATE, OFFERVALUE, OFFERTYPEID, CATEGORYID
		, ITEMGROUPID, CREATEDDATE, UPDATEDDATE, DELETEDDATE, ISACTIVE, APPLIESTOID
	FROM @Offers UOFR
	WHERE NOT EXISTS 
		(
			SELECT 1 FROM OFFER UOFRINNER 
			WHERE UOFRINNER.OFFERID = UOFR.OFFERID
		)

END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFERBRANCH]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_CU_OFFERBRANCH]
(
	@OfferBranches OFFERBRANCHTYPE READONLY
)
AS
BEGIN

	UPDATE OFRB
	SET
		OFRB.OFFERID		 = UOFRB.OFFERID
		, OFRB.BRANCHID		 = UOFRB.BRANCHID
		, OFRB.CREATEDDATE	 = UOFRB.CREATEDDATE
		, OFRB.DELETEDDATE	 = UOFRB.DELETEDDATE
	FROM					  
		OFFERBRANCH OFRB
		INNER JOIN @OfferBranches UOFRB ON UOFRB.OFFERBRANCHID = OFRB.OFFERBRANCHID

	INSERT INTO OFFERBRANCH(OFFERBRANCHID, OFFERID, BRANCHID, CREATEDDATE, DELETEDDATE)
	SELECT OFFERBRANCHID, OFFERID, BRANCHID, CREATEDDATE, DELETEDDATE
	FROM @OfferBranches UOFRB
	WHERE NOT EXISTS 
		(
			SELECT 1 FROM OFFERBRANCH UOFRBINNER 
			WHERE UOFRBINNER.OFFERBRANCHID = UOFRB.OFFERBRANCHID
		)

END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFERITEMMAP]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_CU_OFFERITEMMAP]
(
	@OfferItemMaps OFFERITEMMAPTYPE READONLY
)
AS
BEGIN

	UPDATE OFRIM
	SET
		OFRIM.OFFERID		 = UOFRIM.OFFERID
		, OFRIM.ITEMCODEID	 = UOFRIM.ITEMCODEID
		, OFRIM.CREATEDDATE	 = UOFRIM.CREATEDDATE
		, OFRIM.DELETEDDATE	 = UOFRIM.DELETEDDATE
	FROM					  
		OFFERITEMMAP OFRIM
		INNER JOIN @OfferItemMaps UOFRIM ON UOFRIM.OFFERITEMMAPID = OFRIM.OFFERITEMMAPID

	INSERT INTO OFFERITEMMAP(OFFERITEMMAPID, OFFERID, ITEMCODEID, CREATEDDATE, DELETEDDATE)
	SELECT OFFERITEMMAPID, OFFERID, ITEMCODEID, CREATEDDATE, DELETEDDATE
	FROM @OfferItemMaps UOFRIM
	WHERE NOT EXISTS 
		(
			SELECT 1 FROM OFFERITEMMAP UOFRIMINNER 
			WHERE UOFRIMINNER.OFFERITEMMAPID = UOFRIM.OFFERITEMMAPID
		)

END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_OFFERTYPE]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_CU_OFFERTYPE]
(
	@OfferTypes OFFERTYPETYPE READONLY
)
AS
BEGIN

	UPDATE OFRT
	SET
		OFRT.OFFERTYPENAME		 = UOFRT.OFFERTYPENAME
		, OFRT.OFFERTYPECODE	 = UOFRT.OFFERTYPECODE
		, OFRT.BUYQUANTITY		 = UOFRT.BUYQUANTITY
		, OFRT.FREEQUANTITY		 = UOFRT.FREEQUANTITY
	FROM					  
		OFFERTYPE OFRT
		INNER JOIN @OfferTypes UOFRT ON UOFRT.OFFERTYPEID = OFRT.OFFERTYPEID

	INSERT INTO OFFERTYPE(OFFERTYPEID, OFFERTYPENAME, OFFERTYPECODE, BUYQUANTITY, FREEQUANTITY)
	SELECT OFFERTYPEID, OFFERTYPENAME, OFFERTYPECODE, BUYQUANTITY, FREEQUANTITY
	FROM @OfferTypes UOFRT
	WHERE NOT EXISTS 
		(
			SELECT 1 FROM OFFERTYPE UOFRTINNER 
			WHERE UOFRTINNER.OFFERTYPEID = UOFRT.OFFERTYPEID
		)

END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BILL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_CU_POS_BILL]    
(    
 @Bills POS_BILLTYPE READONLY    
 , @BranchCounterID INT    
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
  , B.DAYCLOSUREID = UB.DAYCLOSUREID
  , B.ROUNDING = UB.ROUNDING
 FROM     
  POS_BILL B    
  INNER JOIN @Bills UB ON UB.BILLID = B.BILLID    
 WHERE    
  B.BRANCHCOUNTERID = @BranchCounterID    
    
 INSERT INTO POS_BILL(BILLID, BRANCHCOUNTERID, BILLNUMBER, CREATEDBY, CREATEDDATE, 
 UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID, ROUNDING)    
 SELECT BILLID, @BranchCounterID, BILLNUMBER, CREATEDBY, CREATEDDATE, 
 UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME ,DAYCLOSUREID, ROUNDING
 FROM @Bills UB    
 WHERE NOT EXISTS    
  (    
   SELECT 1 FROM POS_BILL BINNER WHERE BINNER.BILLID = UB.BILLID AND BINNER.BRANCHCOUNTERID = @BranchCounterID    
  )    
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BILLDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[USP_CU_POS_BILLDETAIL]        
(        
 @BillDetails POS_BILLDETAILTYPE READONLY        
 , @BranchCounterID INT        
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
  BD.BRANCHCOUNTERID = @BranchCounterID        
        
 INSERT INTO POS_BILLDETAIL(BILLDETAILID, BILLID, BRANCHCOUNTERID, ITEMPRICEID,       
 QUANTITY, WEIGHTINKGS, BILLEDAMOUNT, CREATEDDATE, UPDATEDDATE, DELETEDDATE        
  , CGST, SGST, IGST, CESS, GSTVALUE, GSTID, SNO, DISCOUNT,OFFERID,DAYCLOSUREID)        
 SELECT BILLDETAILID, BILLID, @BranchCounterID, ITEMPRICEID,       
 QUANTITY, WEIGHTINKGS, BILLEDAMOUNT, CREATEDDATE, UPDATEDDATE, DELETEDDATE        
  , CGST, SGST, IGST, CESS, GSTVALUE, GSTID, SNO, DISCOUNT ,OFFERID,DAYCLOSUREID       
 FROM @BillDetails UBD        
 WHERE NOT EXISTS        
  (        
   SELECT 1 FROM POS_BILLDETAIL BDINNER WHERE BDINNER.BILLDETAILID = UBD.BILLDETAILID       
   AND BDINNER.BRANCHCOUNTERID = @BranchCounterID        
  )        
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BILLMOPDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_POS_BILLMOPDETAIL]          
(          
 @BillMopDetails POS_BILLMOPDETAILTYPE READONLY          
 , @BranchCounterID INT          
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
BMD.DAYCLOSUREID = UBMD.DAYCLOSUREID
 FROM           
  POS_BILLMOPDETAIL BMD        
  INNER JOIN @BillMopDetails UBMD ON UBMD.BILLMOPDETAILID = BMD.BILLMOPDETAILID          
 WHERE          
  BMD.COUNTERID = @BranchCounterID          
          
 INSERT INTO POS_BILLMOPDETAIL(BILLMOPDETAILID,BILLID,MOPID,MOPVALUE,    
CREATEDDATE,UPDATEDDATE,COUNTERID,DAYCLOSUREID)          
 SELECT BILLMOPDETAILID,BILLID,MOPID,MOPVALUE,    
CREATEDDATE,UPDATEDDATE,@BranchCounterID,DAYCLOSUREID
 FROM @BillMopDetails UBMD          
 WHERE NOT EXISTS          
  (          
   SELECT 1 FROM POS_BILLMOPDETAIL BDINNER WHERE BDINNER.BILLMOPDETAILID = UBMD.BILLMOPDETAILID   
   AND BDINNER.COUNTERID = @BranchCounterID          
  )          
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BREFUND]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_POS_BREFUND]      
(      
 @BRefundDetails POS_BREFUNDTYPE READONLY      
 , @BranchCounterID INT      
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
BR.BREFUNDNUMBER = UBR.BREFUNDNUMBER
 FROM       
  POS_BREFUND BR    
  INNER JOIN @BRefundDetails UBR ON UBR.BREFUNDID = BR.BREFUNDID      
 WHERE      
  BR.COUNTERID = @BranchCounterID      
      
 INSERT INTO POS_BREFUND(BREFUNDID,BRANCHID,CREATEDBY,CREATEDATE,UPDATEDBY,    
UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS,BREFUNDNUMBER,COUNTERID)      
 SELECT BREFUNDID,BRANCHID,CREATEDBY,CREATEDATE,UPDATEDBY,    
UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS,BREFUNDNUMBER,@BranchCounterID    
 FROM @BRefundDetails UBR      
 WHERE NOT EXISTS      
  (      
   SELECT 1 FROM POS_BREFUND BRINNER WHERE BRINNER.BREFUNDID = UBR.BREFUNDID    
   AND BRINNER.COUNTERID = @BranchCounterID      
  )      
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BREFUNDDETAL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_POS_BREFUNDDETAL]    
(    
 @BRefundDetails POS_BREFUNDDETAILTYPE READONLY    
 , @BranchCounterID INT    
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
BRD.COUNTERID = @BranchCounterID  
 FROM     
  POS_BREFUNDDETAIL BRD  
  INNER JOIN @BRefundDetails UBRD ON UBRD.BREFUNDDETAILID = BRD.BREFUNDDETAILID    
 WHERE    
  BRD.COUNTERID = @BranchCounterID    
    
 INSERT INTO POS_BREFUNDDETAIL(BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,  
WEIGHTINKGS,CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,COUNTERID)    
 SELECT BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,  
WEIGHTINKGS,CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,@BranchCounterID  
 FROM @BRefundDetails UBRD  
 WHERE NOT EXISTS    
  (    
   SELECT 1 FROM POS_BREFUNDDETAIL BRDINNER WHERE BRDINNER.BREFUNDDETAILID = UBRD.BREFUNDDETAILID  
   AND BRDINNER.COUNTERID = @BranchCounterID    
  )    
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_CREFUND]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_POS_CREFUND]    
(    
 @CRefundDetails POS_CREFUNDTYPE READONLY    
 , @BranchCounterID INT    
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
CR.COUNTERID = @BranchCounterID  
 FROM     
  POS_CREFUND CR  
  INNER JOIN @CRefundDetails UCR ON UCR.REFUNDID = CR.REFUNDID  
 WHERE    
 CR.COUNTERID = @BranchCounterID    
    
 INSERT INTO POS_CREFUND(REFUNDID,BILLDETAILID,REFUNDQUANTITY,REFUNDWEIGHTINKGS,  
REFUNDAMOUNT,CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,DELETEDBY,  
DELETEDDATE,DAYCLOSUREID,COUNTERID)    
 SELECT REFUNDID,BILLDETAILID,REFUNDQUANTITY,REFUNDWEIGHTINKGS,  
REFUNDAMOUNT,CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,DELETEDBY,  
DELETEDDATE,DAYCLOSUREID,@BranchCounterID  
 FROM @CRefundDetails UCR  
 WHERE NOT EXISTS    
  (    
   SELECT 1 FROM POS_CREFUND CRINNER WHERE CRINNER.REFUNDID = UCR.REFUNDID  
   AND CRINNER.COUNTERID = @BranchCounterID    
  )    
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_DAYCLOSURE]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_POS_DAYCLOSURE]        
(        
 @DayClosure POS_DAYCLOSURETYPE READONLY        
 , @BranchCounterID INT        
)        
AS        
BEGIN        
 UPDATE DC      
 SET        
DC.CLOSUREDATE = UDC.CLOSUREDATE,    
DC.BRANCHCOUNTERID = UDC.BRANCHCOUNTERID,    
DC.OPENINGBALANCE = UDC.OPENINGBALANCE,    
DC.CLOSINGBALANCE = UDC.CLOSINGBALANCE,    
DC.CLOSINGDIFFERENCE = UDC.CLOSINGDIFFERENCE,    
DC.CLOSEDBY = UDC.CLOSEDBY,    
DC.REFUNDAMOUNT = UDC.REFUNDAMOUNT,    
DC.CREATEDDATE = UDC.CREATEDDATE,    
DC.UPDATEDDATE = UDC.UPDATEDDATE    
 FROM         
  POS_DAYCLOSURE DC      
  INNER JOIN @DayClosure UDC ON UDC.DAYCLOSUREID = DC.DAYCLOSUREID     
  AND UDC.BRANCHCOUNTERID = DC.BRANCHCOUNTERID      
        
 INSERT INTO POS_DAYCLOSURE(DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,    
CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDDATE,UPDATEDDATE)        
 SELECT DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,    
CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDDATE,UPDATEDDATE    
 FROM @DayClosure UDC      
 WHERE NOT EXISTS        
  (        
   SELECT 1 FROM POS_DAYCLOSURE DCINNER WHERE DCINNER.DAYCLOSUREID = UDC.DAYCLOSUREID      
   AND DCINNER.BRANCHCOUNTERID = UDC.BRANCHCOUNTERID      
  )        
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_DAYCLOSUREDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_POS_DAYCLOSUREDETAIL]          
(          
 @DayClosureDetail POS_DAYCLOSUREDETAILTYPE READONLY          
 , @BranchCounterID INT          
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
DCD.UPDATEDDATE = UDCD.UPDATEDDATE
 FROM           
  POS_DAYCLOSUREDETAIL DCD      
  INNER JOIN @DayClosureDetail UDCD ON UDCD.DAYCLOSUREDETAILID = DCD.DAYCLOSUREDETAILID       
  AND DCD.COUNTERID = @BranchCounterID       
          
 INSERT INTO POS_DAYCLOSUREDETAIL(DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE,      
  MOPID,CREATEDDATE,UPDATEDDATE,COUNTERID)          
 SELECT DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE,      
  MOPID,CREATEDDATE,UPDATEDDATE,@BranchCounterID      
 FROM @DayClosureDetail UDCD        
 WHERE NOT EXISTS          
  (          
   SELECT 1 FROM POS_DAYCLOSUREDETAIL DCDINNER WHERE DCDINNER.DAYCLOSUREDETAILID = UDCD.DAYCLOSUREDETAILID        
   AND DCDINNER.COUNTERID = @BranchCounterID      
  )          
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_POS_DAYSEQUENCE]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_POS_DAYSEQUENCE]        
(        
 @DaySequence POS_DAYSEQUENCETYPE READONLY        
)        
AS        
BEGIN        
 UPDATE DS      
 SET   
	 DS.OPENDATE			=  UDS.OPENDATE
	, DS.LASTUSEDBILLNUM	=  UDS.LASTUSEDBILLNUM
	, DS.ISCLOSED			=  UDS.ISCLOSED		
	, DS.BRANCHCOUNTERID	=  UDS.BRANCHCOUNTERID
	, DS.LASTBILLID			=  UDS.LASTBILLID		
	, DS.DAYSEQUENCEID		=  UDS.DAYSEQUENCEID	
	, DS.OPENBILLID			=  UDS.OPENBILLID		
	, DS.CREATEDATE			=  UDS.CREATEDATE		
	, DS.UPDATEDATE			=  UDS.UPDATEDATE		
 FROM         
  POS_DAYSEQUENCE DS      
  INNER JOIN @DaySequence UDS ON UDS.DAYSEQUENCEID = DS.DAYSEQUENCEID     
  AND UDS.BRANCHCOUNTERID = DS.BRANCHCOUNTERID      
        

	INSERT INTO POS_DAYSEQUENCE(OPENDATE, LASTUSEDBILLNUM, ISCLOSED, BRANCHCOUNTERID, LASTBILLID, DAYSEQUENCEID
		, OPENBILLID, CREATEDATE, UPDATEDATE)
	SELECT OPENDATE, LASTUSEDBILLNUM, ISCLOSED, BRANCHCOUNTERID, LASTBILLID, DAYSEQUENCEID
		, OPENBILLID, CREATEDATE, UPDATEDATE
	FROM @DaySequence UDS
	WHERE NOT EXISTS 
		(
			SELECT 1 FROM POS_DAYSEQUENCE DSINNER WHERE DSINNER.DAYSEQUENCEID = UDS.DAYSEQUENCEID      
				AND DSINNER.BRANCHCOUNTERID = UDS.BRANCHCOUNTERID     
		)

END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_ROLE]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_ROLE]
(
	@Role TBLROLETYPE READONLY
)
AS
BEGIN
	UPDATE R
	SET R.ROLENAME = UR.ROLENAME		
	FROM
		TBLROLE R
		INNER JOIN @Role UR ON UR.ROLEID = R.ROLEID

	INSERT INTO TBLROLE
	SELECT * FROM @Role UR
	WHERE NOT EXISTS (SELECT 1 FROM TBLROLE IR WHERE IR.ROLEID = UR.ROLEID)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_STOCKDISPATCH]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_CU_STOCKDISPATCH]
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

	INSERT INTO STOCKDISPATCH(STOCKDISPATCHID, FROMBRANCHID, TOBRANCHID, STATUS, STATUSAPPROVEDBY, STATUSAPPROVEDDATE, CREATEDDATE, UPDATEDATE, DELETEDDATE, DISPATCHNUMBER
		, CREATEDBY, UPDATEDBY, DELETEDBY)
	SELECT STOCKDISPATCHID, FROMBRANCHID, TOBRANCHID, STATUS, STATUSAPPROVEDBY, STATUSAPPROVEDDATE, CREATEDDATE, UPDATEDATE, DELETEDDATE, DISPATCHNUMBER
		, CREATEDBY, UPDATEDBY, DELETEDBY
	FROM @StockDispatch USD
	WHERE NOT EXISTS (SELECT 1 FROM STOCKDISPATCH SDI WHERE SDI.STOCKDISPATCHID = USD.STOCKDISPATCHID)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_STOCKDISPATCHDETAIL]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_STOCKDISPATCHDETAIL]    
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
  , SDD.ISACCEPTED = USDD.ISACCEPTED
 FROM    
  STOCKDISPATCHDETAIL SDD    
  INNER JOIN @StockDispatchDetail USDD ON USDD.STOCKDISPATCHDETAILID = SDD.STOCKDISPATCHDETAILID    
    
 INSERT INTO STOCKDISPATCHDETAIL(STOCKDISPATCHDETAILID, STOCKDISPATCHID, ITEMPRICEID, TRAYNUMBER, DISPATCHQUANTITY,   
 RECEIVEDQUANTITY, WEIGHTINKGS, CREATEDDATE , UPDATEDATE, DELETEDDATE,ISACCEPTED)    
 SELECT STOCKDISPATCHDETAILID, STOCKDISPATCHID, ITEMPRICEID, TRAYNUMBER,   
 DISPATCHQUANTITY, RECEIVEDQUANTITY, WEIGHTINKGS, CREATEDDATE    
   , UPDATEDATE, DELETEDDATE  ,ISACCEPTED
 FROM @StockDispatchDetail USDD    
 WHERE NOT EXISTS (SELECT 1 FROM STOCKDISPATCHDETAIL SDDI WHERE SDDI.STOCKDISPATCHDETAILID = USDD.STOCKDISPATCHDETAILID)    

END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_TBLCATEGORY]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_CU_TBLCATEGORY]  
(    
 @Category TBLCATEGORYTYPE READONLY    
)    
AS    
BEGIN    
 UPDATE CAT  
 SET    
CAT.CATEGORYNAME   = UCAT.CATEGORYNAME   ,
CAT.CREATEDBY  = UCAT.CATEGORYNAME   ,
CAT.CREATEDDATE = UCAT.CATEGORYNAME   ,
CAT.UPDATEDBY = UCAT.CATEGORYNAME   ,
CAT.UPDATEDDATE = UCAT.CATEGORYNAME  , 
CAT.DELETEDBY = UCAT.CATEGORYNAME   ,
CAT.DELETEDDATE = UCAT.CATEGORYNAME  , 
CAT.ALLOWOPENITEMS = UCAT.CATEGORYNAME   
 FROM    
  TBLCATEGORY CAT  
  INNER JOIN @Category UCAT   
 ON CAT.CATEGORYID = UCAT.CATEGORYID    
    
    
 INSERT INTO TBLCATEGORY(
 CATEGORYID,CATEGORYNAME,CREATEDBY
,CREATEDDATE,UPDATEDBY,UPDATEDDATE
,DELETEDBY,DELETEDDATE,ALLOWOPENITEMS)    
 SELECT CATEGORYID,CATEGORYNAME,CREATEDBY
,CREATEDDATE,UPDATEDBY,UPDATEDDATE
,DELETEDBY,DELETEDDATE,ALLOWOPENITEMS 
FROM @Category  Cat  
 WHERE NOT EXISTS (SELECT 1 FROM TBLCATEGORY UCAT WHERE Cat.CATEGORYID = UCAT.CATEGORYID)    

END 
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_UOM]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_CU_UOM]
(
	@UOM UOMTYPE READONLY
)
AS
BEGIN
	UPDATE U
	SET
		U.DISPLAYVALUE = UU.DISPLAYVALUE
		, U.BASEUOMID = UU.BASEUOMID
		, U.MULTIPLIER = UU.MULTIPLIER
		, U.CREATEDDATE = UU.CREATEDDATE
		, U.UPDATEDDATE = UU.UPDATEDDATE
		, U.DELETEDDATE = UU.DELETEDDATE
	FROM 
		UOM U
		INNER JOIN @UOM UU ON UU.UOMID = U.UOMID

	INSERT INTO UOM(UOMID, DISPLAYVALUE, BASEUOMID, MULTIPLIER, CREATEDDATE, UPDATEDDATE, DELETEDDATE )
	SELECT UOMID, DISPLAYVALUE, BASEUOMID, MULTIPLIER, CREATEDDATE, UPDATEDDATE, DELETEDDATE  FROM @UOM UU
	WHERE NOT EXISTS (SELECT 1 FROM UOM IU WHERE IU.UOMID = UU.UOMID)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CU_USER]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_CU_USER]
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


	INSERT INTO TBLUSER(USERID, ROLEID, REPORTINGLEADID, CATEGORYID, BRANCHID, USERNAME, PASSWORDSTRING, FULLNAME, CNUMBER, EMAIL, ISOTP, GENDER, DOB, CREATEDDATE, UPDATEDDATE, DELETEDDATE)
	SELECT USERID, ROLEID, REPORTINGLEADID, CATEGORYID, BRANCHID, USERNAME, PASSWORDSTRING, FULLNAME, CNUMBER, EMAIL, ISOTP, GENDER, DOB, CREATEDDATE, UPDATEDDATE, DELETEDDATE FROM @User UR
	WHERE NOT EXISTS (SELECT 1 FROM TBLUSER IU WHERE IU.USERID = UR.USERID)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_D_SYNCSTATUS]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_D_SYNCSTATUS]
@LocationID int
AS
BEGIN

IF(@LocationID = 45)
RETURN

DELETE FROM ENTITYSYNCSTATUS WHERE LOCATIONID = @LocationID
and SYNCDIRECTION = 'FromCloud'

END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BRANCH]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_BRANCH]  
AS  
BEGIN  
  
SELECT  
BRANCHID,  
BRANCHNAME,  
BRANCHCODE,  
ADDRESS,  
PHONENO,  
LANDLINE,  
EMAILID  
FROM BRANCH  
WHERE DELETEDDATE IS NULL
  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_R_BRANCHCOUNTER]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_BRANCHCOUNTER]  
AS  
BEGIN  
  
SELECT   
COUNTERID,  
COUNTERNAME,  
BRANCHID ,
ISNULL(DAYCLOSUREID, 0) + 1 AS DAYCLOSUREID
, ISNULL(BRANCHREFUNDID, 0) + 1 AS BRANCHREFUNDID
FROM BRANCHCOUNTER  
WHERE  DELETEDDATE IS NULL
  
END  
GO
/****** Object:  StoredProcedure [dbo].[USP_R_GETSYNC]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_R_GETSYNC]
(
	@LocationID INT
	, @LocationType varchar(15)
	, @SyncDirection varchar(10)
)
AS
BEGIN

	INSERT INTO ENTITYSYNCSTATUS(ENTITYID, LOCATIONID, LOCATIONTYPE, SYNCDIRECTION, SYNCDATE)
	SELECT ENTITYID, @LocationID, LOCATIONTYPE, SYNCDIRECTION, '1900-01-01'
	FROM ENTITYSYNCORDER ESO
	WHERE 
		ESO.LOCATIONTYPE = @LocationType
		AND ESO.SYNCDIRECTION = @SyncDirection
		AND NOT EXISTS
			(
				SELECT 1 FROM ENTITYSYNCSTATUS ESS 
				WHERE 
					ESS.ENTITYID = ESO.ENTITYID
					AND ESS.LOCATIONID = @LocationID
					AND ESS.LOCATIONTYPE = @LocationType
					AND ESS.SYNCDIRECTION = @SyncDirection
			)
	ORDER BY ESO.SYNCORDER
	
	SELECT ESS.ENTITYSYNCSTATUSID, ESS.ENTITYID, E.ENTITYNAME, ESS.LOCATIONID, ESS.LOCATIONTYPE, ESS.SYNCDIRECTION, ESS.SYNCDATE
	FROM 
		ENTITYSYNCSTATUS ESS
		INNER JOIN ENTITY E ON E.ENTITYID = ESS.ENTITYID
		INNER JOIN ENTITYSYNCORDER ESO ON ESO.ENTITYID = E.ENTITYID 
	WHERE
		ESS.LOCATIONID = @LocationID
		AND ESS.LOCATIONTYPE = @LocationType
		AND ESS.SYNCDIRECTION = @SyncDirection
		AND ESO.LOCATIONTYPE = @LocationType
		AND ESO.SYNCDIRECTION = @SyncDirection
	ORDER BY ESO.SYNCORDER
END
GO
/****** Object:  StoredProcedure [dbo].[USP_R_GETSYNCDATA]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_R_GETSYNCDATA]                        
(                        
 @EntityName VARCHAR(50)                        
 ,  @SyncDate DATETIME                         
 , @BranchID INT = 0                        
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
   (B.BRANCHID = @BranchID OR @BranchID = 0)                        
   AND (B.CREATEDDATE > @SyncDate                        
   OR B.UPDATEDATE > @SyncDate                        
   OR B.DELETEDDATE > @SyncDate)                        
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
   (SD.TOBRANCHID = @BranchID OR @BranchID = 0)                        
   AND (SD.CREATEDDATE > @SyncDate                        
   OR SD.UPDATEDATE > @SyncDate                        
   OR SD.DELETEDDATE > @SyncDate)                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'STOCKDISPATCHDETAIL'                        
 BEGIN                        
  SELECT SDD.STOCKDISPATCHDETAILID, SDD.STOCKDISPATCHID, SDD.ITEMPRICEID, SDD.TRAYNUMBER, SDD.DISPATCHQUANTITY, SDD.RECEIVEDQUANTITY                    
   , SDD.CREATEDDATE, SDD.UPDATEDATE, SDD.DELETEDDATE, SDD.WEIGHTINKGS,SDD.ISACCEPTED  
  FROM                         
   STOCKDISPATCHDETAIL SDD                        
   INNER JOIN STOCKDISPATCH SD ON SD.STOCKDISPATCHID = SDD.STOCKDISPATCHID                        
  WHERE                        
   (SD.TOBRANCHID = @BranchID OR @BranchID = 0)                        
   AND (SDD.CREATEDDATE > @SyncDate                        
   OR SDD.UPDATEDATE > @SyncDate                        
   OR SDD.DELETEDDATE > @SyncDate)                        
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
                          
                        
 IF @EntityName = 'BRANCHCOUNTER'           BEGIN                        
  SELECT COUNTERID, COUNTERNAME, BRANCHID, CREATEDDATE, UPDATEDDATE, DELETEDDATE                         
  FROM BRANCHCOUNTER CNTR                        
  WHERE                        
   (CNTR.BRANCHID = @BranchID OR @BranchID = 0)                        
   AND (CNTR.CREATEDDATE > @SyncDate                        
   OR CNTR.UPDATEDDATE > @SyncDate                        
   OR CNTR.DELETEDDATE > @SyncDate)                        
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
  SELECT USERID, ROLEID, REPORTINGLEADID, CATEGORYID, BRANCHID, USERNAME,                   
  PASSWORDSTRING, FULLNAME, CNUMBER, EMAIL, ISOTP, GENDER, DOB, CREATEDDATE, UPDATEDDATE, DELETEDDATE                        
  FROM TBLUSER USR                        
  WHERE                        
   (USR.BRANCHID = @BranchID OR @BranchID = 0)                        
   AND (USR.CREATEDDATE > @SyncDate                        
   OR USR.UPDATEDDATE > @SyncDate           
   OR USR.DELETEDDATE > @SyncDate)                        
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
  SELECT OFFERID, OFFERNAME, OFFERCODE, STARTDATE, ENDDATE, OFFERVALUE, OFFERTYPEID, CATEGORYID                        
   , ITEMGROUPID, CREATEDDATE, UPDATEDDATE, DELETEDDATE, ISACTIVE, APPLIESTOID                         
  FROM                         
   OFFER OFR                        
  WHERE                        
   EXISTS ( SELECT 1 FROM OFFERBRANCH OFRB WHERE OFRB.BRANCHID = @BranchID OR @BranchID = 0 )                        
   AND (OFR.CREATEDDATE > @SyncDate                        
   OR OFR.UPDATEDDATE > @SyncDate                        
   OR OFR.DELETEDDATE > @SyncDate)                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'OFFERBRANCH'                        
 BEGIN                        
  SELECT OFFERBRANCHID, OFFERID, BRANCHID, CREATEDDATE, DELETEDDATE                        
  FROM OFFERBRANCH OFRB                        
  WHERE                        
   (OFRB.BRANCHID = @BranchID OR @BranchID = 0)                        
   AND (OFRB.CREATEDDATE > @SyncDate                        
   OR OFRB.DELETEDDATE > @SyncDate)                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'OFFERITEMMAP'                        
 BEGIN                        
  SELECT OFFERITEMMAPID, OFFERID, ITEMCODEID, CREATEDDATE, DELETEDDATE                         
  FROM OFFERITEMMAP OFRIM                        
  WHERE                        
   EXISTS ( SELECT 1 FROM OFFERBRANCH OFRB WHERE OFRB.BRANCHID = @BranchID OR @BranchID = 0 )                        
   AND (OFRIM.CREATEDDATE > @SyncDate                        
   OR OFRIM.DELETEDDATE > @SyncDate)                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'CLOUD_STOCKCOUNTING'                        
 BEGIN                        
  SELECT STOCKCOUNTINGID,BRANCHID,CREATEDBY,CREATEDDATE,UPDATEDBY,                       
    UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS                        
  FROM CLOUD_STOCKCOUNTING                         
  WHERE CREATEDDATE > @SyncDate                      
  OR UPDATEDDATE > @SyncDate                      
  RETURN                        
 END                        
                        
 IF @EntityName = 'CLOUD_STOCKCOUNTINGDETAIL'                        
 BEGIN                        
  SELECT SCD.STOCKCOUNTINGDETAILID,SCD.STOCKCOUNTINGID,SCD.ITEMPRICEID,                        
    SCD.QUANTITY,SCD.CREATEDDATE,SCD.UPDATEDDATE,SCD.DELETEDDATE                        
     FROM CLOUD_STOCKCOUNTINGDETAIL SCD                        
   WHERE SCD.CREATEDDATE > @SyncDate                       
   OR SCD.UPDATEDDATE > @SyncDate                       
   OR DELETEDDATE > @SyncDate                      
  RETURN                        
 END                        
          
 IF @EntityName = 'POS_BILL'                        
 BEGIN                        
  SELECT BILLID, BRANCHCOUNTERID, BILLNUMBER, CREATEDBY, CREATEDDATE,           UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS,           
  CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID            
  FROM POS_BILL B                        
  WHERE                        
   B.CREATEDDATE > @SyncDate                        
   OR B.UPDATEDDATE > @SyncDate                        
   OR B.DELETEDDATE > @SyncDate                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'POS_BILLDETAIL'                        
 BEGIN                        
  SELECT BILLDETAILID, BILLID, BRANCHCOUNTERID, ITEMPRICEID, QUANTITY, WEIGHTINKGS, BILLEDAMOUNT,                 
  CREATEDDATE, UPDATEDDATE, DELETEDDATE, CGST                        
   , SGST, IGST, CESS, GSTVALUE, GSTID, SNO, DISCOUNT,OFFERID,DAYCLOSUREID    
  FROM POS_BILLDETAIL BD                        
  WHERE                        
   BD.CREATEDDATE > @SyncDate                        
   OR BD.UPDATEDDATE > @SyncDate                        
   OR BD.DELETEDDATE > @SyncDate                        
  RETURN                        
 END                        
          
  IF @EntityName = 'POS_BILLMOPDETAIL'                        
 BEGIN                        
  SELECT BILLMOPDETAILID,BILLID,MOPID,MOPVALUE,CREATEDDATE                  
  UPDATEDBY,UPDATEDDATE,COUNTERID  ,DAYCLOSUREID                
     FROM POS_BILLMOPDETAIL                        
   WHERE CREATEDDATE > @SyncDate                       
   OR UPDATEDDATE > @SyncDate                       
  RETURN                        
 END                        
          
IF @EntityName = 'POS_CREFUND'                        
 BEGIN                        
  SELECT REFUNDID,BILLDETAILID,REFUNDQUANTITY,REFUNDWEIGHTINKGS,REFUNDAMOUNT,                  
  CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,DELETEDBY,DELETEDDATE,                  
  DAYCLOSUREID,COUNTERID                  
 FROM POS_CREFUND                  
   WHERE CREATEDDATE > @SyncDate                       
   OR UPDATEDDATE > @SyncDate                       
   OR DELETEDDATE > @SyncDate                      
  RETURN                        
 END                  
                  
 IF @EntityName = 'POS_BREFUND'                        
 BEGIN                        
  SELECT BREFUNDID,BRANCHID,CREATEDBY,CREATEDATE,UPDATEDBY,                  
UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS,BREFUNDNUMBER,COUNTERID                  
     FROM POS_BREFUND                   
   WHERE CREATEDATE > @SyncDate                       
   OR UPDATEDDATE > @SyncDate                       
   OR DELETEDDATE > @SyncDate                      
  RETURN                        
 END                  
                   
 IF @EntityName = 'POS_BREFUNDDETAIL'                        
 BEGIN                        
  SELECT BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,WEIGHTINKGS,                  
  CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,COUNTERID                  
     FROM POS_BREFUNDDETAIL                  
   WHERE CREATEDDATE > @SyncDate                       
   OR UPDATEDDATE > @SyncDate                       
  RETURN                        
 END                  
                  
  IF @EntityName = 'POS_DAYCLOSURE'                        
 BEGIN                        
  SELECT DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,                  
  CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDBY,                  
  CREATEDDATE,UPDATEDBY,UPDATEDDATE                  
     FROM POS_DAYCLOSURE                   
   WHERE CREATEDDATE > @SyncDate                       
   OR UPDATEDDATE > @SyncDate                       
  RETURN                        
 END         
                  
 IF @EntityName = 'POS_DAYCLOSUREDETAIL'                        
 BEGIN                        
  SELECT DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE,                  
  MOPID,CREATEDDATE,UPDATEDDATE,COUNTERID                  
     FROM POS_DAYCLOSUREDETAIL                        
   WHERE CREATEDDATE > @SyncDate                       
   OR UPDATEDDATE > @SyncDate                       
  RETURN                        
 END        
         
 IF @EntityName = 'POS_DENOMINATION'                        
 BEGIN                        
  SELECT DENOMINATIONID,DISPLAYVALUE,MULTIPLIER,CREATEDDATE,UPDATEDDATE        
     FROM POS_DENOMINATION                        
   WHERE CREATEDDATE > @SyncDate                       
   OR UPDATEDDATE > @SyncDate                       
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
     FROM TBLCATEGORY                        
   WHERE CREATEDDATE > @SyncDate                       
   OR UPDATEDDATE > @SyncDate                       
   OR DELETEDDATE > @SyncDate                       
  RETURN                        
 END        
                    
END 
GO
/****** Object:  StoredProcedure [dbo].[USP_R_POS_IMPORTDATA]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[USP_R_POS_IMPORTDATA]
(
	@BranchCounterID INT
)
AS
BEGIN

	SELECT 
		BILLID , BILLNUMBER, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE
		, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME, DAYCLOSUREID, ROUNDING 
	FROM POS_BILL 
	WHERE BRANCHCOUNTERID = @BranchCounterID --AND ISNULL(DAYCLOSUREID, 0) = 0

	SELECT 
		BILLDETAILID, BILLID, ITEMPRICEID, QUANTITY, WEIGHTINKGS, BILLEDAMOUNT, CREATEDDATE, UPDATEDDATE
		, DELETEDDATE, CGST, SGST, IGST, CESS, GSTVALUE, GSTID, SNO, DISCOUNT, OFFERID, DAYCLOSUREID
	FROM POS_BILLDETAIL
	WHERE BRANCHCOUNTERID = @BranchCounterID --AND ISNULL(DAYCLOSUREID, 0) = 0

	SELECT 
		BILLMOPDETAILID, BILLID, MOPID, MOPVALUE, CREATEDDATE, UPDATEDDATE, DAYCLOSUREID
	FROM POS_BILLMOPDETAIL
	WHERE COUNTERID = @BranchCounterID --AND ISNULL(DAYCLOSUREID, 0) = 0

	SELECT OPENDATE, LASTUSEDBILLNUM, ISCLOSED, BRANCHCOUNTERID, LASTBILLID, DAYSEQUENCEID, 
		OPENBILLID, CREATEDATE, UPDATEDATE 
	FROM POS_DAYSEQUENCE
	WHERE BRANCHCOUNTERID = @BranchCounterID --AND ISNULL(ISCLOSED, 0) = 0


END

GO
/****** Object:  StoredProcedure [dbo].[USP_R_SHOWSYNC]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_R_SHOWSYNC]  
AS  
BEGIN  
 SELECT *   
 FROM  
  (  
   SELECT ESS.ENTITYSYNCSTATUSID,ESS.LOCATIONID, BWH.BRANCHNAME AS LOCATIONNAME, 
   E.ENTITYNAME, ESS.SYNCDIRECTION, ESS.SYNCDATE  
   FROM  
    ENTITYSYNCSTATUS ESS  
    INNER JOIN ENTITY E ON E.ENTITYID = ESS.ENTITYID  
    INNER JOIN BRANCH BWH ON BWH.BRANCHID = ESS.LOCATIONID  
   WHERE ESS.LOCATIONTYPE = 'Warehouse'  
  
   UNION ALL  
  
   SELECT ESS.ENTITYSYNCSTATUSID,ESS.LOCATIONID,BC.COUNTERNAME AS LOCATIONNAME, 
   E.ENTITYNAME, ESS.SYNCDIRECTION, ESS.SYNCDATE  
   FROM  
    ENTITYSYNCSTATUS ESS  
    INNER JOIN ENTITY E ON E.ENTITYID = ESS.ENTITYID  
    INNER JOIN BRANCHCOUNTER BC ON BC.COUNTERID = ESS.LOCATIONID  
   WHERE ESS.LOCATIONTYPE = 'BranchCounter'  
  ) SYNC  
 ORDER BY SYNC.SYNCDIRECTION, SYNC.LOCATIONNAME  
  
END
GO
/****** Object:  StoredProcedure [dbo].[USP_U_BRANCHCOUNTER_HDDNO]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_U_BRANCHCOUNTER_HDDNO]
(
	@BranchCounterID INT
	, @HDDSNO VARCHAR(30)
)
AS
BEGIN
	
	DECLARE @CurHDDSno VARCHAR(30)
	SELECT @CurHDDSno = HDDSNO FROM BRANCHCOUNTER WHERE COUNTERID = @BranchCounterID 
	
	IF ISNULL(@CurHDDSno, 'New') = 'New'
	BEGIN
		UPDATE BRANCHCOUNTER
		SET HDDSNO = @HDDSNO
		WHERE COUNTERID = @BranchCounterID

		SELECT @CurHDDSno = @HDDSNO
	END

	IF ISNULL(@CurHDDSno, 'New') <> @HDDSNO
	BEGIN
		THROW 51000, 'Machine ID not matching, installation will be canceled', 1
	END

END
GO
/****** Object:  StoredProcedure [dbo].[USP_U_ENTITYSYNCTIME]    Script Date: 12-03-2022 11:25:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_U_ENTITYSYNCTIME]
(
	@EntitySyncStatusID INT
	, @SyncTime  DATETIME
)
AS
BEGIN
	UPDATE ESS
	SET ESS.SYNCDATE = @SyncTime
	FROM ENTITYSYNCSTATUS ESS
	WHERE ESS.ENTITYSYNCSTATUSID = @EntitySyncStatusID
END
GO
USE [master]
GO
ALTER DATABASE [NSRetail_Cloud] SET  READ_WRITE 
GO
