IF NOT EXISTS (SELECT 1 FROM ENTITY WHERE ENTITYNAME = 'TBLCATEGORY')
BEGIN
INSERT INTO ENTITY(ENTITYNAME)
VALUES ('TBLCATEGORY')
END
GO

DECLARE @EntityID INT
SELECT @EntityID = ENTITYID FROM ENTITY WHERE ENTITYNAME = 'TBLCATEGORY'

IF NOT EXISTS (SELECT 1 FROM ENTITYSYNCORDER WHERE ENTITYID = @EntityID AND LOCATIONTYPE = 'Warehouse' AND SYNCDIRECTION = 'ToCloud')
BEGIN
INSERT INTO ENTITYSYNCORDER(ENTITYID,SYNCORDER,LOCATIONTYPE,SYNCDIRECTION)
VALUES(@EntityID,21,'Warehouse','ToCloud')
END
GO

IF NOT EXISTS (SELECT 1 FROM ENTITYSYNCORDER WHERE ENTITYID = @EntityID AND LOCATIONTYPE = 'BranchCounter' AND SYNCDIRECTION = 'FromCloud')
BEGIN
INSERT INTO ENTITYSYNCORDER(ENTITYID,SYNCORDER,LOCATIONTYPE,SYNCDIRECTION)
VALUES(@EntityID,20,'BranchCounter','FromCloud')
END

GO
/****** Object:  Table [dbo].[TBLCATEGORY]    Script Date: 03-Mar-22 9:46:35 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TBLCATEGORY]') AND type in (N'U'))
DROP TABLE [dbo].[TBLCATEGORY]
GO

/****** Object:  Table [dbo].[TBLCATEGORY]    Script Date: 03-Mar-22 9:46:35 AM ******/
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
CREATE TYPE [dbo].[TBLCATEGORYTYPE] AS TABLE (
	[CATEGORYID] [int] NOT NULL,
	[CATEGORYNAME] [nvarchar](50) NOT NULL,
	[CREATEDBY] [int] NULL,
	[CREATEDDATE] [datetime] NULL,
	[UPDATEDBY] [int] NULL,
	[UPDATEDDATE] [datetime] NULL,
	[DELETEDBY] [int] NULL,
	[DELETEDDATE] [datetime] NULL,
	[ALLOWOPENITEMS] [bit] NULL)
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
alter PROC [dbo].[USP_R_GETSYNCDATA]                        
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



ALTER PROC [dbo].[USP_R_POS_IMPORTDATA]
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

