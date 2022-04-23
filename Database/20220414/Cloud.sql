IF NOT EXISTS (SELECT 1 FROM SYS.all_columns WHERE [OBJECT_ID] = OBJECT_ID('POS_BILL') AND [NAME] = 'SPLDISCPER')
BEGIN
	EXEC ('ALTER TABLE POS_BILL ADD SPLDISCPER DECIMAL(5, 2)')
END

IF NOT EXISTS (SELECT 1 FROM SYS.all_columns WHERE [OBJECT_ID] = OBJECT_ID('POS_BILL') AND [NAME] = 'ISDOORDELIVERY')
BEGIN
	EXEC ('ALTER TABLE POS_BILL ADD ISDOORDELIVERY BIT')
END

IF NOT EXISTS (SELECT 1 FROM SYS.all_columns WHERE [OBJECT_ID] = OBJECT_ID('POS_BILL') AND [NAME] = 'TENDEREDCASH')
BEGIN
	EXEC ('ALTER TABLE POS_BILL ADD TENDEREDCASH DECIMAL(11, 2)')
END

IF NOT EXISTS (SELECT 1 FROM SYS.all_columns WHERE [OBJECT_ID] = OBJECT_ID('POS_BILL') AND [NAME] = 'TENDEREDCHANGE')
BEGIN
	EXEC ('ALTER TABLE POS_BILL ADD TENDEREDCHANGE DECIMAL(11, 2)')
END

GO


DROP PROCEDURE [dbo].[USP_CU_POS_BILL]
GO


/****** Object:  UserDefinedTableType [dbo].[POS_BILLTYPE]    Script Date: 14-04-2022 07:58:16 ******/
DROP TYPE [dbo].[POS_BILLTYPE]
GO

/****** Object:  UserDefinedTableType [dbo].[POS_BILLTYPE]    Script Date: 14-04-2022 07:58:16 ******/
CREATE TYPE [dbo].[POS_BILLTYPE] AS TABLE(
	[BILLID] [int] NOT NULL,
	[BILLNUMBER] [varchar](30) NULL,
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
	[ROUNDING] [decimal](3, 2) NULL,
	SPLDISCPER DECIMAL(5, 2),
	ISDOORDELIVERY BIT,
	TENDEREDCASH DECIMAL(11, 2),
	TENDEREDCHANGE DECIMAL(11, 2)
)
GO


/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BILL]    Script Date: 14-04-2022 07:53:51 ******/
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
  , B.SPLDISCPER = UB.SPLDISCPER
  , B.ISDOORDELIVERY = UB.ISDOORDELIVERY
  , B.TENDEREDCASH = UB.TENDEREDCASH
  , B.TENDEREDCHANGE = UB.TENDEREDCHANGE
  , B.SYNCDATE = GETUTCDATE() + '05:30'
 FROM     
  POS_BILL B    
  INNER JOIN @Bills UB ON UB.BILLID = B.BILLID    
 WHERE    
  B.BRANCHCOUNTERID = @BranchCounterID 
    
 INSERT INTO POS_BILL(BILLID, BRANCHCOUNTERID, BILLNUMBER, CREATEDBY, CREATEDDATE, 
 UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID, ROUNDING, SYNCDATE
 , SPLDISCPER, ISDOORDELIVERY, TENDEREDCASH, TENDEREDCHANGE)    
 SELECT BILLID, @BranchCounterID, BILLNUMBER, CREATEDBY, CREATEDDATE, 
 UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME ,DAYCLOSUREID, ROUNDING, GETUTCDATE() + '05:30'
 , SPLDISCPER, ISDOORDELIVERY, TENDEREDCASH, TENDEREDCHANGE
 FROM @Bills UB    
 WHERE NOT EXISTS    
  (    
   SELECT 1 FROM POS_BILL BINNER WHERE BINNER.BILLID = UB.BILLID AND BINNER.BRANCHCOUNTERID = @BranchCounterID 
  )    
END
GO



/****** Object:  StoredProcedure [dbo].[USP_R_GETSYNCDATA]    Script Date: 14-04-2022 08:02:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[USP_R_GETSYNCDATA]                        
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
  WHERE I.SYNCDATE > @SyncDate                       
   --I.CREATEDDATE > @SyncDate                        
   --OR I.UPDATEDATE > @SyncDate                        
   --OR I.DELETEDDATE > @SyncDate                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'ITEMCODE'                        
 BEGIN                        
  SELECT ITEMCODEID, ITEMID, ITEMCODE, CREATEDDATE, UPDATEDATE, DELETEDDATE, FREEITEMCODEID, HSNCODE                         
  FROM ITEMCODE IC                        
  WHERE  IC.SYNCDATE > @SyncDate                      
   --IC.CREATEDDATE > @SyncDate                        
   --OR IC.UPDATEDATE > @SyncDate                        
   --OR IC.DELETEDDATE > @SyncDate                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'ITEMPRICE'                        
 BEGIN                        
  SELECT ITEMPRICEID, ITEMCODEID, SALEPRICE, MRP, GSTID, CREATEDDATE, UPDATEDATE, DELETEDDATE                         
  FROM ITEMPRICE IP                        
  WHERE IP.SYNCDATE > @SyncDate                  
   --IP.CREATEDDATE > @SyncDate                        
   --OR IP.UPDATEDATE > @SyncDate                        
   --OR IP.DELETEDDATE > @SyncDate                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'BRANCH'                        
 BEGIN                        
  SELECT BRANCHID, BRANCHNAME, BRANCHCODE, ADDRESS, PHONENO, LANDLINE, EMAILID, SUPERVISORID, ISWAREHOUSE, CREATEDDATE, UPDATEDATE, DELETEDDATE, STATEID                         
  FROM BRANCH B                        
  WHERE                        
   (B.BRANCHID = @BranchID OR @BranchID = 0)                        
   AND B.SYNCDATE > @SyncDate
   --(B.CREATEDDATE > @SyncDate                        
   --OR B.UPDATEDATE > @SyncDate                        
   --OR B.DELETEDDATE > @SyncDate)                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'GST'                        
 BEGIN                        
  SELECT GSTID, GSTCODE, CGST, SGST, IGST, CESS, CREATEDDATE, UPDATEDATE, DELETEDDATE                         
  FROM GSTDETAIL GST                        
  WHERE GST.SYNCDATE > @SyncDate                      
   --GST.CREATEDDATE > @SyncDate                        
   --OR GST.UPDATEDATE > @SyncDate                        
   --OR GST.DELETEDDATE > @SyncDate                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'STOCKDISPATCH'                        
 BEGIN                        
  SELECT STOCKDISPATCHID, FROMBRANCHID, TOBRANCHID, STATUS, STATUSAPPROVEDBY, STATUSAPPROVEDDATE, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDATE, DELETEDBY, DELETEDDATE, DISPATCHNUMBER                        
  FROM STOCKDISPATCH SD                        
  WHERE                        
   (SD.TOBRANCHID = @BranchID OR @BranchID = 0)                        
   AND SD.SYNCDATE > @SyncDate
   --(SD.CREATEDDATE > @SyncDate                        
   --OR SD.UPDATEDATE > @SyncDate                        
   --OR SD.DELETEDDATE > @SyncDate)                        
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
   AND SD.SYNCDATE > @SyncDate
   --(SDD.CREATEDDATE > @SyncDate                        
   --OR SDD.UPDATEDATE > @SyncDate                        
   --OR SDD.DELETEDDATE > @SyncDate)                        
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
   AND CNTR.SYNCDATE > @SyncDate
   --(CNTR.CREATEDDATE > @SyncDate                        
   --OR CNTR.UPDATEDDATE > @SyncDate                        
   --OR CNTR.DELETEDDATE > @SyncDate)                        
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
  WHERE MOP.SYNCDATE > @SyncDate                       
   --MOP.CREATEDDATE > @SyncDate                        
   --OR MOP.UPDATEDDATE > @SyncDate                        
   --OR MOP.DELETEDDATE > @SyncDate                        
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
  SELECT USERID, USR.ROLEID, REPORTINGLEADID, CATEGORYID, BRANCHID, USERNAME,                   
  PASSWORDSTRING, FULLNAME, CNUMBER, EMAIL, ISOTP, GENDER, DOB, CREATEDDATE, UPDATEDDATE, DELETEDDATE                        
  FROM 
	TBLUSER USR    
	INNER JOIN TBLROLE R ON R.ROLEID = USR.ROLEID
  WHERE                        
   (USR.BRANCHID = @BranchID OR @BranchID = 0 OR R.ROLENAME = 'Discount Admin')                        
   AND USR.SYNCDATE > @SyncDate
   --(USR.CREATEDDATE > @SyncDate                        
   --OR USR.UPDATEDDATE > @SyncDate           
   --OR USR.DELETEDDATE > @SyncDate)                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'UOM'                        
 BEGIN                        
  SELECT UOMID, DISPLAYVALUE, BASEUOMID, MULTIPLIER, CREATEDDATE, UPDATEDDATE, DELETEDDATE              
  FROM UOM UOM                        
  WHERE UOM.SYNCDATE > @SyncDate                     
   --UOM.CREATEDDATE > @SyncDate                        
   --OR UOM.UPDATEDDATE > @SyncDate                        
   --OR UOM.DELETEDDATE > @SyncDate                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'ITEMGROUP'              
 BEGIN                        
  SELECT ITEMGROUPID, GROUPNAME, ISACTIVE, CREATEDDATE, UPDATEDDATE, DELETEDDATE                        
  FROM ITEMGROUP IG                        
  WHERE IG.SYNCDATE > @SyncDate                       
   --IG.CREATEDDATE > @SyncDate                        
   --OR IG.UPDATEDDATE > @SyncDate                        
   --OR IG.DELETEDDATE > @SyncDate                        
  RETURN               
 END                        
                        
 IF @EntityName = 'ITEMGROUPDETAIL'                        
 BEGIN                        
  SELECT ITEMGROUPDETAILID, ITEMGROUPID, ITEMCODEID, CREATEDDATE, DELETEDDATE                         
  FROM ITEMGROUPDETAIL IGD                        
  WHERE IGD.SYNCDATE > @SyncDate                        
   --IGD.CREATEDDATE > @SyncDate                        
   --OR IGD.DELETEDDATE > @SyncDate                        
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
   EXISTS ( SELECT 1 FROM OFFERBRANCH OFRB WHERE OFRB.OFFERID = OFR.OFFERID AND ( OFRB.BRANCHID = @BranchID OR @BranchID = 0 ))                        
   AND OFR.SYNCDATE > @SyncDate
   --(OFR.CREATEDDATE > @SyncDate                        
   --OR OFR.UPDATEDDATE > @SyncDate                        
   --OR OFR.DELETEDDATE > @SyncDate)                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'OFFERBRANCH'                        
 BEGIN                        
  SELECT OFFERBRANCHID, OFFERID, BRANCHID, CREATEDDATE, DELETEDDATE                        
  FROM OFFERBRANCH OFRB                        
  WHERE                        
   (OFRB.BRANCHID = @BranchID OR @BranchID = 0)                        
   AND OFRB.SYNCDATE > @SyncDate
   --(OFRB.CREATEDDATE > @SyncDate                        
   --OR OFRB.DELETEDDATE > @SyncDate)                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'OFFERITEMMAP'                        
 BEGIN                        
  SELECT OFFERITEMMAPID, OFFERID, ITEMCODEID, CREATEDDATE, DELETEDDATE                         
  FROM OFFERITEMMAP OFRIM                        
  WHERE                        
   EXISTS ( SELECT 1 FROM OFFERBRANCH OFRB WHERE OFRB.OFFERID = OFRIM.OFFERID AND (OFRB.BRANCHID = @BranchID OR @BranchID = 0) )                        
   AND OFRIM.SYNCDATE > @SyncDate
   --(OFRIM.CREATEDDATE > @SyncDate                        
   --OR OFRIM.DELETEDDATE > @SyncDate)                        
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
  SELECT BILLID, BRANCHCOUNTERID, BILLNUMBER, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS,           
  CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID, SPLDISCPER, ISDOORDELIVERY, TENDEREDCASH, TENDEREDCHANGE
  FROM POS_BILL B                        
  WHERE B.SYNCDATE > @SyncDate                       
   --B.CREATEDDATE > @SyncDate                        
   --OR B.UPDATEDDATE > @SyncDate                        
   --OR B.DELETEDDATE > @SyncDate                        
  RETURN                        
 END                        
                        
 IF @EntityName = 'POS_BILLDETAIL'                        
 BEGIN                        
  SELECT BILLDETAILID, BILLID, BRANCHCOUNTERID, ITEMPRICEID, QUANTITY, WEIGHTINKGS, BILLEDAMOUNT,                 
  CREATEDDATE, UPDATEDDATE, DELETEDDATE, CGST                        
   , SGST, IGST, CESS, GSTVALUE, GSTID, SNO, DISCOUNT,OFFERID,DAYCLOSUREID    
  FROM POS_BILLDETAIL BD                        
  WHERE BD.SYNCDATE > @SyncDate                       
   --BD.CREATEDDATE > @SyncDate                        
   --OR BD.UPDATEDDATE > @SyncDate                        
   --OR BD.DELETEDDATE > @SyncDate                        
  RETURN                        
 END                        
          
  IF @EntityName = 'POS_BILLMOPDETAIL'                        
 BEGIN                        
  SELECT BILLMOPDETAILID,BILLID,MOPID,MOPVALUE,CREATEDDATE                  
  UPDATEDBY,UPDATEDDATE,COUNTERID  ,DAYCLOSUREID                
     FROM POS_BILLMOPDETAIL                        
   WHERE SYNCDATE > @SyncDate
   --CREATEDDATE > @SyncDate                       
   --OR UPDATEDDATE > @SyncDate                       
  RETURN                        
 END                        
          
IF @EntityName = 'POS_CREFUND'                        
 BEGIN                        
  SELECT REFUNDID,BILLDETAILID,REFUNDQUANTITY,REFUNDWEIGHTINKGS,REFUNDAMOUNT,                  
  CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,DELETEDBY,DELETEDDATE,                  
  DAYCLOSUREID,COUNTERID                  
 FROM POS_CREFUND                  
   WHERE SYNCDATE > @SyncDate
   --CREATEDDATE > @SyncDate                       
   --OR UPDATEDDATE > @SyncDate                       
   --OR DELETEDDATE > @SyncDate                      
  RETURN                        
 END                  
                  
 IF @EntityName = 'POS_BREFUND'                        
 BEGIN                        
  SELECT BREFUNDID,BRANCHID,CREATEDBY,CREATEDATE,UPDATEDBY,                  
UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS,BREFUNDNUMBER,COUNTERID                  
     FROM POS_BREFUND                   
   WHERE SYNCDATE > @SyncDate
   --CREATEDATE > @SyncDate                       
   --OR UPDATEDDATE > @SyncDate                       
   --OR DELETEDDATE > @SyncDate                      
  RETURN                        
 END                  
                   
 IF @EntityName = 'POS_BREFUNDDETAIL'                        
 BEGIN                        
  SELECT BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,WEIGHTINKGS,                  
  CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,COUNTERID                  
     FROM POS_BREFUNDDETAIL                  
   WHERE SYNCDATE > @SyncDate
   --CREATEDDATE > @SyncDate                       
   --OR UPDATEDDATE > @SyncDate                       
  RETURN                        
 END                  
                  
  IF @EntityName = 'POS_DAYCLOSURE'                        
 BEGIN                        
  SELECT DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,                  
  CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDBY,                  
  CREATEDDATE,UPDATEDBY,UPDATEDDATE                  
     FROM POS_DAYCLOSURE                   
   WHERE SYNCDATE > @SyncDate
   --CREATEDDATE > @SyncDate                       
   --OR UPDATEDDATE > @SyncDate                       
  RETURN                        
 END         
                  
 IF @EntityName = 'POS_DAYCLOSUREDETAIL'                        
 BEGIN                        
  SELECT DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE,                  
  MOPID,CREATEDDATE,UPDATEDDATE,COUNTERID                  
     FROM POS_DAYCLOSUREDETAIL                        
   WHERE SYNCDATE > @SyncDate
   --CREATEDDATE > @SyncDate                       
   --OR UPDATEDDATE > @SyncDate                       
  RETURN                        
 END        
         
 IF @EntityName = 'POS_DENOMINATION'                        
 BEGIN                        
  SELECT DENOMINATIONID,DISPLAYVALUE,MULTIPLIER,CREATEDDATE,UPDATEDDATE        
     FROM POS_DENOMINATION                        
   WHERE SYNCDATE > @SyncDate
   --CREATEDDATE > @SyncDate                       
   --OR UPDATEDDATE > @SyncDate                       
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
   WHERE SYNCDATE > @SyncDate
   --CREATEDDATE > @SyncDate                       
   --OR UPDATEDDATE > @SyncDate                       
   --OR DELETEDDATE > @SyncDate                       
  RETURN                        
 END        
                    
END
GO
ALTER TABLE POS_BREFUNDDETAIL
ADD REASONID INT
GO
ALTER TABLE POS_BREFUNDDETAIL
ADD DELETEDDATE DATETIME
GO
DROP PROCEDURE [dbo].[USP_CU_POS_BREFUNDDETAL]      
GO
DROP TYPE [dbo].[POS_BREFUNDDETAILTYPE]
GO
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
	REASONID int null,
	DELETEDDATE datetime
)
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
, BRD.SYNCDATE = GETUTCDATE() + '05:30' 
,BRD.REASONID = UBRD.REASONID
,BRD.DELETEDDATE = UBRD.DELETEDDATE
 FROM       
  POS_BREFUNDDETAIL BRD    
  INNER JOIN @BRefundDetails UBRD ON UBRD.BREFUNDDETAILID = BRD.BREFUNDDETAILID      
 WHERE      
  BRD.COUNTERID = @BranchCounterID AND BRD.BREFUNDID = UBRD.BREFUNDID  
      
 INSERT INTO POS_BREFUNDDETAIL(BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,    
WEIGHTINKGS,CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,COUNTERID, SYNCDATE,REASONID,DELETEDDATE)      
 SELECT BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,    
WEIGHTINKGS,CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,@BranchCounterID, GETUTCDATE() + '05:30',
REASONID,DELETEDDATE
 FROM @BRefundDetails UBRD    
 WHERE NOT EXISTS      
  (      
   SELECT 1 FROM POS_BREFUNDDETAIL BRDINNER WHERE BRDINNER.BREFUNDDETAILID = UBRD.BREFUNDDETAILID    
   AND BRDINNER.COUNTERID = @BranchCounterID AND BRDINNER.BREFUNDID = UBRD.BREFUNDID  
  )      
END
GO
ALTER PROC [dbo].[USP_R_GETSYNCDATA]                            
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
  WHERE I.SYNCDATE > @SyncDate                           
   --I.CREATEDDATE > @SyncDate                            
   --OR I.UPDATEDATE > @SyncDate                            
   --OR I.DELETEDDATE > @SyncDate                            
  RETURN                            
 END                            
                            
 IF @EntityName = 'ITEMCODE'                            
 BEGIN                            
  SELECT ITEMCODEID, ITEMID, ITEMCODE, CREATEDDATE, UPDATEDATE, DELETEDDATE, FREEITEMCODEID, HSNCODE                             
  FROM ITEMCODE IC                            
  WHERE  IC.SYNCDATE > @SyncDate                          
   --IC.CREATEDDATE > @SyncDate                            
   --OR IC.UPDATEDATE > @SyncDate                            
   --OR IC.DELETEDDATE > @SyncDate                            
  RETURN                            
 END                            
                            
 IF @EntityName = 'ITEMPRICE'                            
 BEGIN                            
  SELECT ITEMPRICEID, ITEMCODEID, SALEPRICE, MRP, GSTID, CREATEDDATE, UPDATEDATE, DELETEDDATE                             
  FROM ITEMPRICE IP                            
  WHERE IP.SYNCDATE > @SyncDate                      
   --IP.CREATEDDATE > @SyncDate                            
   --OR IP.UPDATEDATE > @SyncDate                            
   --OR IP.DELETEDDATE > @SyncDate                            
  RETURN                            
 END                            
                            
 IF @EntityName = 'BRANCH'                            
 BEGIN                            
  SELECT BRANCHID, BRANCHNAME, BRANCHCODE, ADDRESS, PHONENO, LANDLINE, EMAILID, SUPERVISORID, ISWAREHOUSE, CREATEDDATE, UPDATEDATE, DELETEDDATE, STATEID                             
  FROM BRANCH B                            
  WHERE                            
   (B.BRANCHID = @BranchID OR @BranchID = 0)                            
   AND B.SYNCDATE > @SyncDate    
   --(B.CREATEDDATE > @SyncDate                            
   --OR B.UPDATEDATE > @SyncDate                            
   --OR B.DELETEDDATE > @SyncDate)                            
  RETURN                            
 END                            
                            
 IF @EntityName = 'GST'                            
 BEGIN                            
  SELECT GSTID, GSTCODE, CGST, SGST, IGST, CESS, CREATEDDATE, UPDATEDATE, DELETEDDATE                             
  FROM GSTDETAIL GST                            
  WHERE GST.SYNCDATE > @SyncDate                          
   --GST.CREATEDDATE > @SyncDate                            
   --OR GST.UPDATEDATE > @SyncDate                            
   --OR GST.DELETEDDATE > @SyncDate                            
  RETURN                            
 END                            
                            
 IF @EntityName = 'STOCKDISPATCH'                            
 BEGIN                            
  SELECT STOCKDISPATCHID, FROMBRANCHID, TOBRANCHID, STATUS, STATUSAPPROVEDBY, STATUSAPPROVEDDATE, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDATE, DELETEDBY, DELETEDDATE, DISPATCHNUMBER                            
  FROM STOCKDISPATCH SD                            
  WHERE                            
   (SD.TOBRANCHID = @BranchID OR @BranchID = 0)                            
   AND SD.SYNCDATE > @SyncDate    
   --(SD.CREATEDDATE > @SyncDate                            
   --OR SD.UPDATEDATE > @SyncDate                            
   --OR SD.DELETEDDATE > @SyncDate)              
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
   AND SD.SYNCDATE > @SyncDate    
   --(SDD.CREATEDDATE > @SyncDate                            
   --OR SDD.UPDATEDATE > @SyncDate                            
   --OR SDD.DELETEDDATE > @SyncDate)                            
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
   AND CNTR.SYNCDATE > @SyncDate    
   --(CNTR.CREATEDDATE > @SyncDate                            
   --OR CNTR.UPDATEDDATE > @SyncDate                            
   --OR CNTR.DELETEDDATE > @SyncDate)                            
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
  WHERE MOP.SYNCDATE > @SyncDate                           
   --MOP.CREATEDDATE > @SyncDate                            
   --OR MOP.UPDATEDDATE > @SyncDate                            
   --OR MOP.DELETEDDATE > @SyncDate                            
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
  SELECT USERID, USR.ROLEID, REPORTINGLEADID, CATEGORYID, BRANCHID, USERNAME,                       
  PASSWORDSTRING, FULLNAME, CNUMBER, EMAIL, ISOTP, GENDER, DOB, CREATEDDATE, UPDATEDDATE, DELETEDDATE                            
  FROM     
 TBLUSER USR        
 INNER JOIN TBLROLE R ON R.ROLEID = USR.ROLEID    
  WHERE                         
   (USR.BRANCHID = @BranchID OR @BranchID = 0 OR R.ROLENAME = 'Discount Admin')                            
   AND USR.SYNCDATE > @SyncDate    
   --(USR.CREATEDDATE > @SyncDate                            
   --OR USR.UPDATEDDATE > @SyncDate               
   --OR USR.DELETEDDATE > @SyncDate)                            
  RETURN                            
 END                            
                            
 IF @EntityName = 'UOM'                            
 BEGIN                            
  SELECT UOMID, DISPLAYVALUE, BASEUOMID, MULTIPLIER, CREATEDDATE, UPDATEDDATE, DELETEDDATE                  
  FROM UOM UOM                            
  WHERE UOM.SYNCDATE > @SyncDate                         
   --UOM.CREATEDDATE > @SyncDate                            
   --OR UOM.UPDATEDDATE > @SyncDate                            
   --OR UOM.DELETEDDATE > @SyncDate                            
  RETURN                            
 END                            
                            
 IF @EntityName = 'ITEMGROUP'                  
 BEGIN                            
  SELECT ITEMGROUPID, GROUPNAME, ISACTIVE, CREATEDDATE, UPDATEDDATE, DELETEDDATE                            
  FROM ITEMGROUP IG                            
  WHERE IG.SYNCDATE > @SyncDate                           
   --IG.CREATEDDATE > @SyncDate                            
   --OR IG.UPDATEDDATE > @SyncDate                            
   --OR IG.DELETEDDATE > @SyncDate                            
  RETURN                   
 END                            
                            
 IF @EntityName = 'ITEMGROUPDETAIL'                            
 BEGIN                            
  SELECT ITEMGROUPDETAILID, ITEMGROUPID, ITEMCODEID, CREATEDDATE, DELETEDDATE                             
  FROM ITEMGROUPDETAIL IGD                            
  WHERE IGD.SYNCDATE > @SyncDate                            
   --IGD.CREATEDDATE > @SyncDate                            
   --OR IGD.DELETEDDATE > @SyncDate                            
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
   EXISTS ( SELECT 1 FROM OFFERBRANCH OFRB WHERE OFRB.OFFERID = OFR.OFFERID AND ( OFRB.BRANCHID = @BranchID OR @BranchID = 0 ))                            
   AND OFR.SYNCDATE > @SyncDate    
   --(OFR.CREATEDDATE > @SyncDate                            
   --OR OFR.UPDATEDDATE > @SyncDate                            
   --OR OFR.DELETEDDATE > @SyncDate)                            
  RETURN                            
 END                            
                            
 IF @EntityName = 'OFFERBRANCH'                            
 BEGIN                            
  SELECT OFFERBRANCHID, OFFERID, BRANCHID, CREATEDDATE, DELETEDDATE                            
  FROM OFFERBRANCH OFRB                            
  WHERE                            
   (OFRB.BRANCHID = @BranchID OR @BranchID = 0)                            
   AND OFRB.SYNCDATE > @SyncDate    
   --(OFRB.CREATEDDATE > @SyncDate                            
   --OR OFRB.DELETEDDATE > @SyncDate)                            
  RETURN                            
 END                            
                            
 IF @EntityName = 'OFFERITEMMAP'                            
 BEGIN                            
  SELECT OFFERITEMMAPID, OFFERID, ITEMCODEID, CREATEDDATE, DELETEDDATE                             
  FROM OFFERITEMMAP OFRIM                            
  WHERE                            
   EXISTS ( SELECT 1 FROM OFFERBRANCH OFRB WHERE OFRB.OFFERID = OFRIM.OFFERID AND (OFRB.BRANCHID = @BranchID OR @BranchID = 0) )                            
   AND OFRIM.SYNCDATE > @SyncDate    
   --(OFRIM.CREATEDDATE > @SyncDate                            
   --OR OFRIM.DELETEDDATE > @SyncDate)                            
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
  WHERE B.SYNCDATE > @SyncDate                           
   --B.CREATEDDATE > @SyncDate                            
   --OR B.UPDATEDDATE > @SyncDate                            
   --OR B.DELETEDDATE > @SyncDate                            
  RETURN                            
 END                            
                            
 IF @EntityName = 'POS_BILLDETAIL'                            
 BEGIN                            
  SELECT BILLDETAILID, BILLID, BRANCHCOUNTERID, ITEMPRICEID, QUANTITY, WEIGHTINKGS, BILLEDAMOUNT,                     
  CREATEDDATE, UPDATEDDATE, DELETEDDATE, CGST                            
   , SGST, IGST, CESS, GSTVALUE, GSTID, SNO, DISCOUNT,OFFERID,DAYCLOSUREID        
  FROM POS_BILLDETAIL BD                            
  WHERE BD.SYNCDATE > @SyncDate                           
   --BD.CREATEDDATE > @SyncDate                            
   --OR BD.UPDATEDDATE > @SyncDate                            
   --OR BD.DELETEDDATE > @SyncDate                            
  RETURN                            
 END                            
              
  IF @EntityName = 'POS_BILLMOPDETAIL'                            
 BEGIN                            
  SELECT BILLMOPDETAILID,BILLID,MOPID,MOPVALUE,CREATEDDATE                      
  UPDATEDBY,UPDATEDDATE,COUNTERID  ,DAYCLOSUREID                    
     FROM POS_BILLMOPDETAIL                            
   WHERE SYNCDATE > @SyncDate    
   --CREATEDDATE > @SyncDate                           
   --OR UPDATEDDATE > @SyncDate                           
  RETURN            
 END                            
              
IF @EntityName = 'POS_CREFUND'                            
 BEGIN                            
  SELECT REFUNDID,BILLDETAILID,REFUNDQUANTITY,REFUNDWEIGHTINKGS,REFUNDAMOUNT,                      
  CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,DELETEDBY,DELETEDDATE,                      
  DAYCLOSUREID,COUNTERID                      
 FROM POS_CREFUND                      
   WHERE SYNCDATE > @SyncDate    
   --CREATEDDATE > @SyncDate                           
   --OR UPDATEDDATE > @SyncDate                           
   --OR DELETEDDATE > @SyncDate                          
  RETURN                            
 END                      
                      
 IF @EntityName = 'POS_BREFUND'                            
 BEGIN                            
  SELECT BREFUNDID,BRANCHID,CREATEDBY,CREATEDATE,UPDATEDBY,                      
UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS,BREFUNDNUMBER,COUNTERID                      
     FROM POS_BREFUND                       
   WHERE SYNCDATE > @SyncDate    
   --CREATEDATE > @SyncDate                           
   --OR UPDATEDDATE > @SyncDate                           
   --OR DELETEDDATE > @SyncDate                          
  RETURN                            
 END                      
                       
 IF @EntityName = 'POS_BREFUNDDETAIL'                            
 BEGIN                            
  SELECT BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,WEIGHTINKGS,                      
  CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,COUNTERID,REASONID,DELETEDDATE
     FROM POS_BREFUNDDETAIL                      
   WHERE SYNCDATE > @SyncDate    
   --CREATEDDATE > @SyncDate                           
   --OR UPDATEDDATE > @SyncDate                           
  RETURN                            
 END                      
                      
  IF @EntityName = 'POS_DAYCLOSURE'                            
 BEGIN                            
  SELECT DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,                      
  CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDBY,                      
  CREATEDDATE,UPDATEDBY,UPDATEDDATE                      
     FROM POS_DAYCLOSURE                       
   WHERE SYNCDATE > @SyncDate    
   --CREATEDDATE > @SyncDate                           
   --OR UPDATEDDATE > @SyncDate                           
  RETURN                            
 END             
                      
 IF @EntityName = 'POS_DAYCLOSUREDETAIL'                            
 BEGIN                            
  SELECT DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE,                      
  MOPID,CREATEDDATE,UPDATEDDATE,COUNTERID                      
     FROM POS_DAYCLOSUREDETAIL                            
   WHERE SYNCDATE > @SyncDate    
   --CREATEDDATE > @SyncDate                           
   --OR UPDATEDDATE > @SyncDate                           
  RETURN                            
 END            
             
 IF @EntityName = 'POS_DENOMINATION'                            
 BEGIN                            
  SELECT DENOMINATIONID,DISPLAYVALUE,MULTIPLIER,CREATEDDATE,UPDATEDDATE            
     FROM POS_DENOMINATION                            
   WHERE SYNCDATE > @SyncDate    
   --CREATEDDATE > @SyncDate                           
   --OR UPDATEDDATE > @SyncDate                           
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
   WHERE SYNCDATE > @SyncDate    
   --CREATEDDATE > @SyncDate                           
   --OR UPDATEDDATE > @SyncDate                           
   --OR DELETEDDATE > @SyncDate                           
  RETURN                            
 END            
  
 IF @EntityName = 'REASONFORREFUND'                 
 BEGIN                            
  SELECT     
REASONID  
,REASONNAME  
,CREATEDBY  
,CREATEDDATE  
,UPDATEDBY  
,UPDATEDDATE  
,DELETEDBY  
,DELETEDDATE  
     FROM REASONFORREFUND                            
   WHERE SYNCDATE > @SyncDate       
  RETURN                            
 END            
                        
END  
GO
INSERT INTO ENTITY(ENTITYNAME)
VALUES('REASONFORREFUND')
GO
IF NOT EXISTS(SELECT 1 FROM ENTITYSYNCORDER 
WHERE ENTITYID = (SELECT ENTITYID FROM ENTITY WHERE ENTITYNAME = 'REASONFORREFUND') 
	AND LOCATIONTYPE = 'Warehouse' AND SYNCDIRECTION = 'ToCloud')
	BEGIN

	INSERT INTO ENTITYSYNCORDER(ENTITYID,SYNCORDER,LOCATIONTYPE,SYNCDIRECTION)
	SELECT ENTITYID,22,'Warehouse','ToCloud' FROM ENTITY WHERE ENTITYNAME = 'REASONFORREFUND'

	END
GO
IF NOT EXISTS(SELECT 1 FROM ENTITYSYNCORDER 
WHERE ENTITYID = (SELECT ENTITYID FROM ENTITY WHERE ENTITYNAME = 'REASONFORREFUND') 
	AND LOCATIONTYPE = 'BranchCounter' AND SYNCDIRECTION = 'FromCloud')
	BEGIN

	INSERT INTO ENTITYSYNCORDER(ENTITYID,SYNCORDER,LOCATIONTYPE,SYNCDIRECTION)
	SELECT ENTITYID,21,'BranchCounter','FromCloud' FROM ENTITY WHERE ENTITYNAME = 'REASONFORREFUND'

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
  WHERE I.SYNCDATE > @SyncDate                             
   --I.CREATEDDATE > @SyncDate                              
   --OR I.UPDATEDATE > @SyncDate                              
   --OR I.DELETEDDATE > @SyncDate                              
  RETURN                              
 END                              
                              
 IF @EntityName = 'ITEMCODE'                              
 BEGIN                              
  SELECT ITEMCODEID, ITEMID, ITEMCODE, CREATEDDATE, UPDATEDATE, DELETEDDATE, FREEITEMCODEID, HSNCODE                               
  FROM ITEMCODE IC                              
  WHERE  IC.SYNCDATE > @SyncDate                            
   --IC.CREATEDDATE > @SyncDate                              
   --OR IC.UPDATEDATE > @SyncDate                              
   --OR IC.DELETEDDATE > @SyncDate                              
  RETURN                              
 END                              
                              
 IF @EntityName = 'ITEMPRICE'                              
 BEGIN                              
  SELECT ITEMPRICEID, ITEMCODEID, SALEPRICE, MRP, GSTID, CREATEDDATE, UPDATEDATE, DELETEDDATE                               
  FROM ITEMPRICE IP                              
  WHERE IP.SYNCDATE > @SyncDate                        
   --IP.CREATEDDATE > @SyncDate                              
   --OR IP.UPDATEDATE > @SyncDate                              
   --OR IP.DELETEDDATE > @SyncDate                              
  RETURN                              
 END                              
                              
 IF @EntityName = 'BRANCH'                              
 BEGIN                              
  SELECT BRANCHID, BRANCHNAME, BRANCHCODE, ADDRESS, PHONENO, LANDLINE, EMAILID, SUPERVISORID, ISWAREHOUSE, CREATEDDATE, UPDATEDATE, DELETEDDATE, STATEID                               
  FROM BRANCH B                              
  WHERE                              
   (B.BRANCHID = @BranchID OR @BranchID = 0)                              
   AND B.SYNCDATE > @SyncDate      
   --(B.CREATEDDATE > @SyncDate                              
   --OR B.UPDATEDATE > @SyncDate                              
   --OR B.DELETEDDATE > @SyncDate)                              
  RETURN                              
 END                              
                              
 IF @EntityName = 'GST'                              
 BEGIN                              
  SELECT GSTID, GSTCODE, CGST, SGST, IGST, CESS, CREATEDDATE, UPDATEDATE, DELETEDDATE                               
  FROM GSTDETAIL GST                              
  WHERE GST.SYNCDATE > @SyncDate                            
   --GST.CREATEDDATE > @SyncDate                              
   --OR GST.UPDATEDATE > @SyncDate                              
   --OR GST.DELETEDDATE > @SyncDate                              
  RETURN                              
 END                              
                              
 IF @EntityName = 'STOCKDISPATCH'                              
 BEGIN                              
  SELECT STOCKDISPATCHID, FROMBRANCHID, TOBRANCHID, STATUS, STATUSAPPROVEDBY, STATUSAPPROVEDDATE, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDATE, DELETEDBY, DELETEDDATE, DISPATCHNUMBER                              
  FROM STOCKDISPATCH SD                              
  WHERE             
   (SD.TOBRANCHID = @BranchID OR @BranchID = 0)                              
   AND SD.SYNCDATE > @SyncDate      
   --(SD.CREATEDDATE > @SyncDate                              
   --OR SD.UPDATEDATE > @SyncDate                              
   --OR SD.DELETEDDATE > @SyncDate)                
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
   AND SD.SYNCDATE > @SyncDate      
   --(SDD.CREATEDDATE > @SyncDate                              
   --OR SDD.UPDATEDATE > @SyncDate                              
   --OR SDD.DELETEDDATE > @SyncDate)                              
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
   AND CNTR.SYNCDATE > @SyncDate      
   --(CNTR.CREATEDDATE > @SyncDate                              
   --OR CNTR.UPDATEDDATE > @SyncDate                              
   --OR CNTR.DELETEDDATE > @SyncDate)                              
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
  WHERE MOP.SYNCDATE > @SyncDate                             
   --MOP.CREATEDDATE > @SyncDate                              
   --OR MOP.UPDATEDDATE > @SyncDate                              
   --OR MOP.DELETEDDATE > @SyncDate                              
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
  SELECT USERID, USR.ROLEID, REPORTINGLEADID, CATEGORYID, BRANCHID, USERNAME,                         
  PASSWORDSTRING, FULLNAME, CNUMBER, EMAIL, ISOTP, GENDER, DOB, CREATEDDATE, UPDATEDDATE, DELETEDDATE                              
  FROM       
 TBLUSER USR          
 INNER JOIN TBLROLE R ON R.ROLEID = USR.ROLEID      
  WHERE                           
   (USR.BRANCHID = @BranchID OR @BranchID = 0 OR R.ROLENAME = 'Discount Admin')                              
   AND USR.SYNCDATE > @SyncDate      
   --(USR.CREATEDDATE > @SyncDate                              
   --OR USR.UPDATEDDATE > @SyncDate                 
   --OR USR.DELETEDDATE > @SyncDate)                              
  RETURN                              
 END                              
                              
 IF @EntityName = 'UOM'                              
 BEGIN                              
  SELECT UOMID, DISPLAYVALUE, BASEUOMID, MULTIPLIER, CREATEDDATE, UPDATEDDATE, DELETEDDATE                    
  FROM UOM UOM                              
  WHERE UOM.SYNCDATE > @SyncDate                           
   --UOM.CREATEDDATE > @SyncDate                              
   --OR UOM.UPDATEDDATE > @SyncDate                              
   --OR UOM.DELETEDDATE > @SyncDate                              
  RETURN                              
 END                              
                              
 IF @EntityName = 'ITEMGROUP'                    
 BEGIN                              
  SELECT ITEMGROUPID, GROUPNAME, ISACTIVE, CREATEDDATE, UPDATEDDATE, DELETEDDATE                              
  FROM ITEMGROUP IG                              
  WHERE IG.SYNCDATE > @SyncDate                             
   --IG.CREATEDDATE > @SyncDate                              
   --OR IG.UPDATEDDATE > @SyncDate                              
   --OR IG.DELETEDDATE > @SyncDate                              
  RETURN                     
 END                              
                              
 IF @EntityName = 'ITEMGROUPDETAIL'                              
 BEGIN                              
  SELECT ITEMGROUPDETAILID, ITEMGROUPID, ITEMCODEID, CREATEDDATE, DELETEDDATE                               
  FROM ITEMGROUPDETAIL IGD                              
  WHERE IGD.SYNCDATE > @SyncDate                              
   --IGD.CREATEDDATE > @SyncDate                              
   --OR IGD.DELETEDDATE > @SyncDate                              
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
   EXISTS ( SELECT 1 FROM OFFERBRANCH OFRB WHERE OFRB.OFFERID = OFR.OFFERID AND ( OFRB.BRANCHID = @BranchID OR @BranchID = 0 ))                              
   AND OFR.SYNCDATE > @SyncDate      
   --(OFR.CREATEDDATE > @SyncDate                              
   --OR OFR.UPDATEDDATE > @SyncDate                              
   --OR OFR.DELETEDDATE > @SyncDate)                              
  RETURN                 
 END                              
                              
 IF @EntityName = 'OFFERBRANCH'                              
 BEGIN                              
  SELECT OFFERBRANCHID, OFFERID, BRANCHID, CREATEDDATE, DELETEDDATE                              
  FROM OFFERBRANCH OFRB                              
  WHERE                              
   (OFRB.BRANCHID = @BranchID OR @BranchID = 0)                              
   AND OFRB.SYNCDATE > @SyncDate      
   --(OFRB.CREATEDDATE > @SyncDate                              
   --OR OFRB.DELETEDDATE > @SyncDate)                              
  RETURN                              
 END                              
                              
 IF @EntityName = 'OFFERITEMMAP'                              
 BEGIN                              
  SELECT OFFERITEMMAPID, OFFERID, ITEMCODEID, CREATEDDATE, DELETEDDATE                               
  FROM OFFERITEMMAP OFRIM                              
  WHERE                              
   EXISTS ( SELECT 1 FROM OFFERBRANCH OFRB WHERE OFRB.OFFERID = OFRIM.OFFERID AND (OFRB.BRANCHID = @BranchID OR @BranchID = 0) )                              
   AND OFRIM.SYNCDATE > @SyncDate      
   --(OFRIM.CREATEDDATE > @SyncDate                              
   --OR OFRIM.DELETEDDATE > @SyncDate)                              
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
  SELECT BILLID, BRANCHCOUNTERID, BILLNUMBER, CREATEDBY, CREATEDDATE,
  UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS,                 
  CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID,
  SPLDISCPER,ISDOORDELIVERY,TENDEREDCASH,TENDEREDCHANGE
  FROM POS_BILL B                              
  WHERE B.SYNCDATE > @SyncDate                             
   --B.CREATEDDATE > @SyncDate                              
   --OR B.UPDATEDDATE > @SyncDate                              
   --OR B.DELETEDDATE > @SyncDate                              
  RETURN                              
 END                              
                              
 IF @EntityName = 'POS_BILLDETAIL'                              
 BEGIN                              
  SELECT BILLDETAILID, BILLID, BRANCHCOUNTERID, ITEMPRICEID, QUANTITY, WEIGHTINKGS, BILLEDAMOUNT,                       
  CREATEDDATE, UPDATEDDATE, DELETEDDATE, CGST                              
   , SGST, IGST, CESS, GSTVALUE, GSTID, SNO, DISCOUNT,OFFERID,DAYCLOSUREID          
  FROM POS_BILLDETAIL BD                              
  WHERE BD.SYNCDATE > @SyncDate                             
   --BD.CREATEDDATE > @SyncDate                              
   --OR BD.UPDATEDDATE > @SyncDate                              
   --OR BD.DELETEDDATE > @SyncDate                 
  RETURN                              
 END                              
                
  IF @EntityName = 'POS_BILLMOPDETAIL'                              
 BEGIN                              
  SELECT BILLMOPDETAILID,BILLID,MOPID,MOPVALUE,CREATEDDATE                        
  UPDATEDBY,UPDATEDDATE,COUNTERID  ,DAYCLOSUREID                      
     FROM POS_BILLMOPDETAIL                              
   WHERE SYNCDATE > @SyncDate      
   --CREATEDDATE > @SyncDate                             
   --OR UPDATEDDATE > @SyncDate                             
  RETURN              
 END                              
                
IF @EntityName = 'POS_CREFUND'                              
 BEGIN                              
  SELECT REFUNDID,BILLDETAILID,REFUNDQUANTITY,REFUNDWEIGHTINKGS,REFUNDAMOUNT,                        
  CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,DELETEDBY,DELETEDDATE,                        
  DAYCLOSUREID,COUNTERID                        
 FROM POS_CREFUND                        
   WHERE SYNCDATE > @SyncDate      
   --CREATEDDATE > @SyncDate                             
   --OR UPDATEDDATE > @SyncDate                             
   --OR DELETEDDATE > @SyncDate                            
  RETURN                              
 END                        
                        
 IF @EntityName = 'POS_BREFUND'                              
 BEGIN                              
  SELECT BREFUNDID,BRANCHID,CREATEDBY,CREATEDATE,UPDATEDBY,                        
UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS,BREFUNDNUMBER,COUNTERID                        
     FROM POS_BREFUND                         
   WHERE SYNCDATE > @SyncDate      
   --CREATEDATE > @SyncDate                             
   --OR UPDATEDDATE > @SyncDate                             
   --OR DELETEDDATE > @SyncDate                            
  RETURN                              
 END                        
                         
 IF @EntityName = 'POS_BREFUNDDETAIL'                              
 BEGIN                              
  SELECT BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,WEIGHTINKGS,                        
  CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,COUNTERID,REASONID,DELETEDDATE  
     FROM POS_BREFUNDDETAIL                        
   WHERE SYNCDATE > @SyncDate      
   --CREATEDDATE > @SyncDate                             
   --OR UPDATEDDATE > @SyncDate                             
  RETURN                              
 END                        
                        
  IF @EntityName = 'POS_DAYCLOSURE'                              
 BEGIN                              
  SELECT DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,                        
  CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDBY,                        
  CREATEDDATE,UPDATEDBY,UPDATEDDATE                        
     FROM POS_DAYCLOSURE                         
   WHERE SYNCDATE > @SyncDate      
   --CREATEDDATE > @SyncDate                             
   --OR UPDATEDDATE > @SyncDate                             
  RETURN                              
 END               
                        
 IF @EntityName = 'POS_DAYCLOSUREDETAIL'                              
 BEGIN                              
  SELECT DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE,                        
  MOPID,CREATEDDATE,UPDATEDDATE,COUNTERID                        
     FROM POS_DAYCLOSUREDETAIL                              
   WHERE SYNCDATE > @SyncDate      
   --CREATEDDATE > @SyncDate                             
   --OR UPDATEDDATE > @SyncDate                             
  RETURN                              
 END              
               
 IF @EntityName = 'POS_DENOMINATION'                              
 BEGIN                              
  SELECT DENOMINATIONID,DISPLAYVALUE,MULTIPLIER,CREATEDDATE,UPDATEDDATE              
     FROM POS_DENOMINATION                              
   WHERE SYNCDATE > @SyncDate      
   --CREATEDDATE > @SyncDate                             
   --OR UPDATEDDATE > @SyncDate                             
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
   WHERE SYNCDATE > @SyncDate      
   --CREATEDDATE > @SyncDate                             
   --OR UPDATEDDATE > @SyncDate                             
   --OR DELETEDDATE > @SyncDate                             
  RETURN                              
 END              
    
 IF @EntityName = 'REASONFORREFUND'                   
 BEGIN                              
  SELECT       
REASONID    
,REASONNAME    
,CREATEDBY    
,CREATEDDATE    
,UPDATEDBY    
,UPDATEDDATE    
,DELETEDBY    
,DELETEDDATE    
     FROM REASONFORREFUND                              
   WHERE SYNCDATE > @SyncDate         
  RETURN                              
 END              
                          
END 
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
  WHERE I.SYNCDATE > @SyncDate                                 
   --I.CREATEDDATE > @SyncDate                                  
   --OR I.UPDATEDATE > @SyncDate                                  
   --OR I.DELETEDDATE > @SyncDate                                  
  RETURN                                  
 END                                  
                                  
 IF @EntityName = 'ITEMCODE'                                  
 BEGIN                                  
  SELECT ITEMCODEID, ITEMID, ITEMCODE, CREATEDDATE, UPDATEDATE, DELETEDDATE, FREEITEMCODEID, HSNCODE                                   
  FROM ITEMCODE IC                                  
  WHERE  IC.SYNCDATE > @SyncDate                                
   --IC.CREATEDDATE > @SyncDate                                  
   --OR IC.UPDATEDATE > @SyncDate                                  
   --OR IC.DELETEDDATE > @SyncDate                                  
  RETURN                                  
 END                                  
                                  
 IF @EntityName = 'ITEMPRICE'                                  
 BEGIN                                  
  SELECT ITEMPRICEID, ITEMCODEID, SALEPRICE, MRP, GSTID, CREATEDDATE, UPDATEDATE, DELETEDDATE                                   
  FROM ITEMPRICE IP                                  
  WHERE IP.SYNCDATE > @SyncDate                            
   --IP.CREATEDDATE > @SyncDate                                  
   --OR IP.UPDATEDATE > @SyncDate                                  
   --OR IP.DELETEDDATE > @SyncDate                                  
  RETURN                                  
 END                                  
                                  
 IF @EntityName = 'BRANCH'                                  
 BEGIN                                  
  SELECT BRANCHID, BRANCHNAME, BRANCHCODE, ADDRESS, PHONENO, LANDLINE, EMAILID, SUPERVISORID, ISWAREHOUSE, CREATEDDATE, UPDATEDATE, DELETEDDATE, STATEID                                   
  FROM BRANCH B                                  
  WHERE                                  
   (B.BRANCHID = @BranchID OR @BranchID = 0)                                  
   AND B.SYNCDATE > @SyncDate          
   --(B.CREATEDDATE > @SyncDate                                  
   --OR B.UPDATEDATE > @SyncDate                                  
   --OR B.DELETEDDATE > @SyncDate)                                  
  RETURN                                  
 END                                  
                                  
 IF @EntityName = 'GST'                                  
 BEGIN                                  
  SELECT GSTID, GSTCODE, CGST, SGST, IGST, CESS, CREATEDDATE, UPDATEDATE, DELETEDDATE                                   
  FROM GSTDETAIL GST                                  
  WHERE GST.SYNCDATE > @SyncDate                                
   --GST.CREATEDDATE > @SyncDate                                  
   --OR GST.UPDATEDATE > @SyncDate                                  
   --OR GST.DELETEDDATE > @SyncDate                                  
  RETURN                                  
 END                                  
                                  
 IF @EntityName = 'STOCKDISPATCH'                                  
 BEGIN                                  
  SELECT STOCKDISPATCHID, FROMBRANCHID, TOBRANCHID, STATUS, STATUSAPPROVEDBY, STATUSAPPROVEDDATE, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDATE, DELETEDBY, DELETEDDATE, DISPATCHNUMBER                                  
  FROM STOCKDISPATCH SD                                  
  WHERE                 
   (SD.TOBRANCHID = @BranchID OR @BranchID = 0)                                  
   AND SD.SYNCDATE > @SyncDate          
   --(SD.CREATEDDATE > @SyncDate                                  
   --OR SD.UPDATEDATE > @SyncDate                                  
   --OR SD.DELETEDDATE > @SyncDate)                    
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
   AND SD.SYNCDATE > @SyncDate          
   --(SDD.CREATEDDATE > @SyncDate                                  
   --OR SDD.UPDATEDATE > @SyncDate                                  
   --OR SDD.DELETEDDATE > @SyncDate)                                  
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
   AND CNTR.SYNCDATE > @SyncDate          
   --(CNTR.CREATEDDATE > @SyncDate                                  
   --OR CNTR.UPDATEDDATE > @SyncDate                                  
   --OR CNTR.DELETEDDATE > @SyncDate)                                  
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
  WHERE MOP.SYNCDATE > @SyncDate                                 
   --MOP.CREATEDDATE > @SyncDate                                  
   --OR MOP.UPDATEDDATE > @SyncDate                                  
   --OR MOP.DELETEDDATE > @SyncDate                                  
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
  SELECT USERID, USR.ROLEID, REPORTINGLEADID, CATEGORYID, BRANCHID, USERNAME,                             
  PASSWORDSTRING, FULLNAME, CNUMBER, EMAIL, ISOTP, GENDER, DOB, CREATEDDATE, UPDATEDDATE, DELETEDDATE                                  
  FROM           
 TBLUSER USR              
 INNER JOIN TBLROLE R ON R.ROLEID = USR.ROLEID          
  WHERE                               
   (USR.BRANCHID = @BranchID OR @BranchID = 0 OR R.ROLENAME = 'Store Admin')                                  
   AND USR.SYNCDATE > @SyncDate          
   --(USR.CREATEDDATE > @SyncDate                                  
   --OR USR.UPDATEDDATE > @SyncDate                     
   --OR USR.DELETEDDATE > @SyncDate)                                  
  RETURN                                  
 END                                  
                                  
 IF @EntityName = 'UOM'                                  
 BEGIN                                  
  SELECT UOMID, DISPLAYVALUE, BASEUOMID, MULTIPLIER, CREATEDDATE, UPDATEDDATE, DELETEDDATE                        
  FROM UOM UOM                                  
  WHERE UOM.SYNCDATE > @SyncDate                               
   --UOM.CREATEDDATE > @SyncDate                                  
   --OR UOM.UPDATEDDATE > @SyncDate                                  
   --OR UOM.DELETEDDATE > @SyncDate                                  
  RETURN                                  
 END                                  
                                  
 IF @EntityName = 'ITEMGROUP'                        
 BEGIN                                  
  SELECT ITEMGROUPID, GROUPNAME, ISACTIVE, CREATEDDATE, UPDATEDDATE, DELETEDDATE                                  
  FROM ITEMGROUP IG                                  
  WHERE IG.SYNCDATE > @SyncDate                                 
   --IG.CREATEDDATE > @SyncDate                                  
   --OR IG.UPDATEDDATE > @SyncDate                                  
   --OR IG.DELETEDDATE > @SyncDate                                  
  RETURN                         
 END                                  
                                  
 IF @EntityName = 'ITEMGROUPDETAIL'                                  
 BEGIN                                  
  SELECT ITEMGROUPDETAILID, ITEMGROUPID, ITEMCODEID, CREATEDDATE, DELETEDDATE                                   
  FROM ITEMGROUPDETAIL IGD                                  
  WHERE IGD.SYNCDATE > @SyncDate                                  
   --IGD.CREATEDDATE > @SyncDate                                  
   --OR IGD.DELETEDDATE > @SyncDate                                  
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
   EXISTS ( SELECT 1 FROM OFFERBRANCH OFRB WHERE OFRB.OFFERID = OFR.OFFERID AND ( OFRB.BRANCHID = @BranchID OR @BranchID = 0 ))                                  
   AND OFR.SYNCDATE > @SyncDate          
   --(OFR.CREATEDDATE > @SyncDate                                  
   --OR OFR.UPDATEDDATE > @SyncDate                                  
   --OR OFR.DELETEDDATE > @SyncDate)                                  
  RETURN                     
 END                                  
                                  
 IF @EntityName = 'OFFERBRANCH'                                  
 BEGIN                                  
  SELECT OFFERBRANCHID, OFFERID, BRANCHID, CREATEDDATE, DELETEDDATE                                  
  FROM OFFERBRANCH OFRB                                  
  WHERE                                  
   (OFRB.BRANCHID = @BranchID OR @BranchID = 0)                                  
   AND OFRB.SYNCDATE > @SyncDate          
   --(OFRB.CREATEDDATE > @SyncDate                                  
   --OR OFRB.DELETEDDATE > @SyncDate)                                  
  RETURN                                  
 END                                  
                                  
 IF @EntityName = 'OFFERITEMMAP'                                  
 BEGIN                                  
  SELECT OFFERITEMMAPID, OFFERID, ITEMCODEID, CREATEDDATE, DELETEDDATE                                   
  FROM OFFERITEMMAP OFRIM                                  
  WHERE                                  
   EXISTS ( SELECT 1 FROM OFFERBRANCH OFRB WHERE OFRB.OFFERID = OFRIM.OFFERID AND (OFRB.BRANCHID = @BranchID OR @BranchID = 0) )                                  
   AND OFRIM.SYNCDATE > @SyncDate          
   --(OFRIM.CREATEDDATE > @SyncDate                                  
   --OR OFRIM.DELETEDDATE > @SyncDate)                                  
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
  SELECT BILLID, BRANCHCOUNTERID, BILLNUMBER, CREATEDBY, CREATEDDATE,    
  UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS,                     
  CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID,    
  SPLDISCPER,ISDOORDELIVERY,TENDEREDCASH,TENDEREDCHANGE    
  FROM POS_BILL B                                  
  WHERE B.SYNCDATE > @SyncDate                                 
   --B.CREATEDDATE > @SyncDate                                  
   --OR B.UPDATEDDATE > @SyncDate                                  
   --OR B.DELETEDDATE > @SyncDate                                  
  RETURN                                  
 END                                  
                                  
 IF @EntityName = 'POS_BILLDETAIL'                                  
 BEGIN                                  
  SELECT BILLDETAILID, BILLID, BRANCHCOUNTERID, ITEMPRICEID, QUANTITY, WEIGHTINKGS, BILLEDAMOUNT,                           
  CREATEDDATE, UPDATEDDATE, DELETEDDATE, CGST                                  
   , SGST, IGST, CESS, GSTVALUE, GSTID, SNO, DISCOUNT,OFFERID,DAYCLOSUREID              
  FROM POS_BILLDETAIL BD                                  
  WHERE BD.SYNCDATE > @SyncDate                                 
   --BD.CREATEDDATE > @SyncDate                                  
   --OR BD.UPDATEDDATE > @SyncDate                                  
   --OR BD.DELETEDDATE > @SyncDate                     
  RETURN                                  
 END                                  
                    
  IF @EntityName = 'POS_BILLMOPDETAIL'                                  
 BEGIN                                  
  SELECT BILLMOPDETAILID,BILLID,MOPID,MOPVALUE,CREATEDDATE                            
  UPDATEDBY,UPDATEDDATE,COUNTERID  ,DAYCLOSUREID                          
     FROM POS_BILLMOPDETAIL                                  
   WHERE SYNCDATE > @SyncDate          
   --CREATEDDATE > @SyncDate                                 
   --OR UPDATEDDATE > @SyncDate                                 
  RETURN                  
 END                                  
                    
IF @EntityName = 'POS_CREFUND'                                  
 BEGIN                                  
  SELECT REFUNDID,BILLDETAILID,REFUNDQUANTITY,REFUNDWEIGHTINKGS,REFUNDAMOUNT,                            
  CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,DELETEDBY,DELETEDDATE,                            
  DAYCLOSUREID,COUNTERID                            
 FROM POS_CREFUND                            
   WHERE SYNCDATE > @SyncDate          
   --CREATEDDATE > @SyncDate                                 
   --OR UPDATEDDATE > @SyncDate                                 
   --OR DELETEDDATE > @SyncDate                                
  RETURN                                  
 END                            
                            
 IF @EntityName = 'POS_BREFUND'                                  
 BEGIN                                  
  SELECT BREFUNDID,BRANCHID,CREATEDBY,CREATEDATE,UPDATEDBY,                            
UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS,BREFUNDNUMBER,COUNTERID                            
     FROM POS_BREFUND                             
   WHERE SYNCDATE > @SyncDate          
   --CREATEDATE > @SyncDate                                 
   --OR UPDATEDDATE > @SyncDate                                 
   --OR DELETEDDATE > @SyncDate                                
  RETURN                                  
 END                            
                             
 IF @EntityName = 'POS_BREFUNDDETAIL'                                  
 BEGIN                                  
  SELECT BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,WEIGHTINKGS,                            
  CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,COUNTERID,REASONID,DELETEDDATE      
     FROM POS_BREFUNDDETAIL                            
   WHERE SYNCDATE > @SyncDate          
   --CREATEDDATE > @SyncDate                                 
   --OR UPDATEDDATE > @SyncDate                                 
  RETURN                                  
 END                            
                            
  IF @EntityName = 'POS_DAYCLOSURE'                                  
 BEGIN                                  
  SELECT DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,              
  CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDBY,                            
  CREATEDDATE,UPDATEDBY,UPDATEDDATE                            
     FROM POS_DAYCLOSURE                             
   WHERE SYNCDATE > @SyncDate          
   --CREATEDDATE > @SyncDate                                 
   --OR UPDATEDDATE > @SyncDate                                 
  RETURN                                  
 END                   
                            
 IF @EntityName = 'POS_DAYCLOSUREDETAIL'                                  
 BEGIN                                  
  SELECT DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE,      
  MOPID,CREATEDDATE,UPDATEDDATE,COUNTERID                            
     FROM POS_DAYCLOSUREDETAIL                                  
   WHERE SYNCDATE > @SyncDate          
   --CREATEDDATE > @SyncDate                                 
   --OR UPDATEDDATE > @SyncDate                                 
  RETURN                                  
 END                  
                   
 IF @EntityName = 'POS_DENOMINATION'                                  
 BEGIN                                  
  SELECT DENOMINATIONID,DISPLAYVALUE,MULTIPLIER,CREATEDDATE,UPDATEDDATE                  
     FROM POS_DENOMINATION                                  
   WHERE SYNCDATE > @SyncDate          
   --CREATEDDATE > @SyncDate                                 
   --OR UPDATEDDATE > @SyncDate                                 
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
   WHERE SYNCDATE > @SyncDate          
   --CREATEDDATE > @SyncDate                                 
   --OR UPDATEDDATE > @SyncDate                                 
   --OR DELETEDDATE > @SyncDate                                 
  RETURN                                  
 END                  
        
 IF @EntityName = 'REASONFORREFUND'                       
 BEGIN                                  
  SELECT           
REASONID        
,REASONNAME        
,CREATEDBY        
,CREATEDDATE        
,UPDATEDBY        
,UPDATEDDATE        
,DELETEDBY        
,DELETEDDATE        
     FROM REASONFORREFUND                                  
   WHERE SYNCDATE > @SyncDate             
  RETURN                                  
 END                  
                              
END 
GO