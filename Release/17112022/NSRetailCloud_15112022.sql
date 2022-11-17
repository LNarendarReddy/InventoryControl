ALTER TABLE POS_BILL
ADD CUSTOMERGST VARCHAR(100) NULL
GO
ALTER TABLE POS_BILL
ADD CUSTOMERGST VARCHAR(100) NULL
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
  RETURN                                    
 END                                    
                                    
 IF @EntityName = 'ITEMCODE'                                    
 BEGIN                                    
  SELECT ITEMCODEID, ITEMID, ITEMCODE, CREATEDDATE, UPDATEDATE, DELETEDDATE, FREEITEMCODEID, HSNCODE                                     
  FROM ITEMCODE IC                                    
  WHERE  IC.SYNCDATE > @SyncDate                                  
  RETURN                                    
 END                                    
                                    
 IF @EntityName = 'ITEMPRICE'                                    
 BEGIN                                    
  SELECT ITEMPRICEID, ITEMCODEID, SALEPRICE, MRP, GSTID, CREATEDDATE, UPDATEDATE, DELETEDDATE                                     
  FROM ITEMPRICE IP                                    
  WHERE IP.SYNCDATE > @SyncDate                                 
  RETURN                                    
 END                                    
                                    
 IF @EntityName = 'BRANCH'                                    
 BEGIN                                    
  SELECT BRANCHID, BRANCHNAME, BRANCHCODE, ADDRESS, PHONENO, 
  LANDLINE, EMAILID, SUPERVISORID, ISWAREHOUSE, CREATEDDATE, UPDATEDATE, DELETEDDATE, STATEID, MULTIEDITTHRESHOLD
  FROM BRANCH B                                    
  WHERE                                    
   (B.BRANCHID = @BranchID OR @BranchID = 0)                                    
   AND B.SYNCDATE > @SyncDate                                             
  RETURN                                    
 END                                    
                                    
 IF @EntityName = 'GST'                                    
 BEGIN                                    
  SELECT GSTID, GSTCODE, CGST, SGST, IGST, CESS, CREATEDDATE, UPDATEDATE, DELETEDDATE                                     
  FROM GSTDETAIL GST                                    
  WHERE GST.SYNCDATE > @SyncDate                                  
  RETURN      
 END                                    
        
 IF @EntityName = 'STOCKDISPATCH'                                    
 BEGIN                                    
  SELECT STOCKDISPATCHID, FROMBRANCHID, TOBRANCHID, STATUS, STATUSAPPROVEDBY, STATUSAPPROVEDDATE, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDATE, DELETEDBY, DELETEDDATE, DISPATCHNUMBER                                    
  FROM STOCKDISPATCH SD                                    
  WHERE                   
   (SD.TOBRANCHID = @BranchID OR @BranchID = 0)                                    
   AND SD.SYNCDATE > @SyncDate            
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
  RETURN                                    
 END                                    
                                      
                                    
 IF @EntityName = 'BRANCHCOUNTER'           BEGIN                                    
  SELECT COUNTERID, COUNTERNAME, BRANCHID, CREATEDDATE, UPDATEDDATE, DELETEDDATE                                     
  FROM BRANCHCOUNTER CNTR                                    
  WHERE                                    
   (CNTR.BRANCHID = @BranchID OR @BranchID = 0)                                    
   AND CNTR.SYNCDATE > @SyncDate               
  RETURN                                    
 END                                    
                      
 IF @EntityName = 'MOP'                                    
 BEGIN                                    
  SELECT MOPID, MOPNAME, CREATEDDATE, UPDATEDDATE, DELETEDDATE                                    
  FROM TBLMOP MOP                                    
  WHERE MOP.SYNCDATE > @SyncDate                                      
  RETURN                                    
 END                                    
                                    
 IF @EntityName = 'ROLE'                      
 BEGIN                                    
  SELECT ROLEID, ROLENAME FROM TBLROLE RL                                                             
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
   (USR.BRANCHID = @BranchID OR @BranchID = 0 OR (R.ROLENAME = 'Store Admin' AND USR.BRANCHID = 45))                                    
   AND USR.SYNCDATE > @SyncDate               
  RETURN                                    
 END                                    
                                    
 IF @EntityName = 'UOM'                                    
 BEGIN                                    
  SELECT UOMID, DISPLAYVALUE, BASEUOMID, MULTIPLIER, CREATEDDATE, UPDATEDDATE, DELETEDDATE                          
  FROM UOM UOM                                    
  WHERE UOM.SYNCDATE > @SyncDate                                    
  RETURN                                    
 END                                    
                                    
 IF @EntityName = 'ITEMGROUP'                          
 BEGIN                                    
  SELECT ITEMGROUPID, GROUPNAME, ISACTIVE, CREATEDDATE, UPDATEDDATE, DELETEDDATE                                    
  FROM ITEMGROUP IG                                    
  WHERE IG.SYNCDATE > @SyncDate                                      
  RETURN                           
 END                                    
                                    
 IF @EntityName = 'ITEMGROUPDETAIL'                                    
 BEGIN                                    
  SELECT ITEMGROUPDETAILID, ITEMGROUPID, ITEMCODEID, CREATEDDATE, DELETEDDATE                                     
  FROM ITEMGROUPDETAIL IGD                                    
  WHERE IGD.SYNCDATE > @SyncDate               
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
   , ITEMGROUPID, CREATEDDATE, UPDATEDDATE, DELETEDDATE, ISACTIVE, APPLIESTOID, FREEITEMPRICEID,NUMBEROFITEMS                                     
  FROM                                     
   OFFER OFR                                    
  WHERE                                    
   EXISTS ( SELECT 1 FROM OFFERBRANCH OFRB WHERE OFRB.OFFERID = OFR.OFFERID AND ( OFRB.BRANCHID = @BranchID OR @BranchID = 0 ))                                    
   AND OFR.SYNCDATE > @SyncDate               
  RETURN                       
 END                                    
                                    
 IF @EntityName = 'OFFERBRANCH'                                    
 BEGIN                                    
  SELECT OFFERBRANCHID, OFFERID, BRANCHID, CREATEDDATE, DELETEDDATE                                    
  FROM OFFERBRANCH OFRB                                    
  WHERE                                    
   (OFRB.BRANCHID = @BranchID OR @BranchID = 0)                                    
   AND OFRB.SYNCDATE > @SyncDate               
  RETURN                                    
 END                                    
                                    
 IF @EntityName = 'OFFERITEMMAP'                                    
 BEGIN                                    
  SELECT OFFERITEMMAPID, OFFERID, ITEMCODEID, CREATEDDATE, DELETEDDATE                                     
  FROM OFFERITEMMAP OFRIM                                    
  WHERE                                    
   EXISTS ( SELECT 1 FROM OFFERBRANCH OFRB WHERE OFRB.OFFERID = OFRIM.OFFERID AND (OFRB.BRANCHID = @BranchID OR @BranchID = 0) )                                    
   AND OFRIM.SYNCDATE > @SyncDate               
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
    SCD.QUANTITY,SCD.CREATEDDATE,SCD.UPDATEDDATE,SCD.DELETEDDATE,SCD.WEIGHTINKGS
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
  SPLDISCPER,ISDOORDELIVERY,TENDEREDCASH,TENDEREDCHANGE, CUSTOMERGST
  FROM POS_BILL B                                    
  WHERE B.SYNCDATE > @SyncDate                                      
  RETURN                                    
 END                                    
                                    
 IF @EntityName = 'POS_BILLDETAIL'                                    
 BEGIN                                    
  SELECT BILLDETAILID, BILLID, BRANCHCOUNTERID, ITEMPRICEID, QUANTITY, WEIGHTINKGS, BILLEDAMOUNT,                             
  CREATEDDATE, UPDATEDDATE, DELETEDDATE, CGST                                    
   , SGST, IGST, CESS, GSTVALUE, GSTID, SNO, DISCOUNT,OFFERID,DAYCLOSUREID                
  FROM POS_BILLDETAIL BD                                    
  WHERE BD.SYNCDATE > @SyncDate                                   
  RETURN                                    
 END                                    
                      
  IF @EntityName = 'POS_BILLMOPDETAIL'                                    
 BEGIN                                    
  SELECT BILLMOPDETAILID,BILLID,MOPID,MOPVALUE,CREATEDDATE                              
  UPDATEDBY,UPDATEDDATE,COUNTERID  ,DAYCLOSUREID                            
     FROM POS_BILLMOPDETAIL                                    
   WHERE SYNCDATE > @SyncDate               
  RETURN                    
 END                                    
                      
IF @EntityName = 'POS_CREFUND'                                    
 BEGIN                                    
  SELECT REFUNDID,BILLDETAILID,REFUNDQUANTITY,REFUNDWEIGHTINKGS,REFUNDAMOUNT,                              
  CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,DELETEDBY,DELETEDDATE,                              
  DAYCLOSUREID,COUNTERID                              
 FROM POS_CREFUND                              
   WHERE SYNCDATE > @SyncDate              
  RETURN                                    
 END                              
                              
 IF @EntityName = 'POS_BREFUND'                                    
 BEGIN                                    
  SELECT BREFUNDID,BRANCHID,CREATEDBY,CREATEDATE,UPDATEDBY,                              
UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS,BREFUNDNUMBER,COUNTERID                              
     FROM POS_BREFUND                        
   WHERE SYNCDATE > @SyncDate               
  RETURN                                    
 END                              
                               
 IF @EntityName = 'POS_BREFUNDDETAIL'                                    
 BEGIN                                    
  SELECT BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,WEIGHTINKGS,                              
  CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,COUNTERID,REASONID,DELETEDDATE        
     FROM POS_BREFUNDDETAIL                              
   WHERE SYNCDATE > @SyncDate               
  RETURN                                    
 END                              
                              
  IF @EntityName = 'POS_DAYCLOSURE'                                    
 BEGIN                                    
  SELECT DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,                
  CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDBY,                              
  CREATEDDATE,UPDATEDBY,UPDATEDDATE, BILLCOUNT, BILLDETAILCOUNT, CREFUNDCOUNT                              
     FROM POS_DAYCLOSURE                               
   WHERE SYNCDATE > @SyncDate               
  RETURN                                    
 END                     
                              
 IF @EntityName = 'POS_DAYCLOSUREDETAIL'                                    
 BEGIN                                    
  SELECT DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE,        
  MOPID,CREATEDDATE,UPDATEDDATE,COUNTERID                              
     FROM POS_DAYCLOSUREDETAIL                                    
   WHERE SYNCDATE > @SyncDate               
  RETURN                                    
 END                    
                     
 IF @EntityName = 'POS_DENOMINATION'                                    
 BEGIN                                    
  SELECT DENOMINATIONID,DISPLAYVALUE,MULTIPLIER,CREATEDDATE,UPDATEDDATE                    
     FROM POS_DENOMINATION                                    
   WHERE SYNCDATE > @SyncDate               
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
DROP PROC [dbo].[USP_CU_POS_BILL]    
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BILLTYPE]    Script Date: 15-11-2022 15:42:50 ******/
DROP TYPE [dbo].[POS_BILLTYPE]
GO

/****** Object:  UserDefinedTableType [dbo].[POS_BILLTYPE]    Script Date: 15-11-2022 15:42:50 ******/
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
	[SPLDISCPER] [decimal](5, 2) NULL,
	[ISDOORDELIVERY] [bit] NULL,
	[TENDEREDCASH] [decimal](11, 2) NULL,
	[TENDEREDCHANGE] [decimal](11, 2) NULL,
	CUSTOMERGST NVARCHAR(100) NULL
)
GO
CREATE PROC [dbo].[USP_CU_POS_BILL]    
(    
 @Bills POS_BILLTYPE READONLY    
 , @BranchCounterID INT    
)    
AS    
BEGIN 

IF EXISTS(SELECT 1 FROM @Bills B GROUP BY B.BILLID HAVING COUNT(1) > 1)
BEGIN
	INSERT INTO DUMP_POS_BILLTYPE
	SELECT *, GETDATE() FROM @Bills
END

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
  , B.CUSTOMERGST = UB.CUSTOMERGST
  , B.SYNCDATE = GETUTCDATE() + '05:30'
 FROM     
  POS_BILL B    
  INNER JOIN @Bills UB ON UB.BILLID = B.BILLID    
 WHERE    
  B.BRANCHCOUNTERID = @BranchCounterID 
    
 INSERT INTO POS_BILL(BILLID, BRANCHCOUNTERID, BILLNUMBER, CREATEDBY, CREATEDDATE, 
 UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID, ROUNDING, SYNCDATE
 , SPLDISCPER, ISDOORDELIVERY, TENDEREDCASH, TENDEREDCHANGE, CUSTOMERGST)    
 SELECT BILLID, @BranchCounterID, BILLNUMBER, CREATEDBY, CREATEDDATE, 
 UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME ,DAYCLOSUREID, ROUNDING, GETUTCDATE() + '05:30'
 , SPLDISCPER, ISDOORDELIVERY, TENDEREDCASH, TENDEREDCHANGE, CUSTOMERGST
 FROM @Bills UB    
 WHERE NOT EXISTS    
  (    
   SELECT 1 FROM POS_BILL BINNER WHERE BINNER.BILLID = UB.BILLID AND BINNER.BRANCHCOUNTERID = @BranchCounterID 
  )    
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
		, SPLDISCPER, ISDOORDELIVERY, TENDEREDCASH, TENDEREDCHANGE ,CUSTOMERGST
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

	SELECT REFUNDID, BILLDETAILID, REFUNDQUANTITY, REFUNDWEIGHTINKGS, REFUNDAMOUNT, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, DAYCLOSUREID
	FROM POS_CREFUND
	WHERE COUNTERID = @BranchCounterID

	SELECT BREFUNDID, BRANCHID, CREATEDBY, CREATEDATE, UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, STATUS, BREFUNDNUMBER
	FROM POS_BREFUND
	WHERE COUNTERID = @BranchCounterID

	SELECT BREFUNDDETAILID, BREFUNDID, ITEMPRICEID, QUANTITY, WEIGHTINKGS, CREATEDDATE, UPDATEDDATE, SNO, TRAYNUMBER, REASONID, DELETEDDATE
	FROM POS_BREFUNDDETAIL
	WHERE COUNTERID = @BranchCounterID
         
  SELECT DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,                  
  CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDBY,                                
  CREATEDDATE,UPDATEDBY,UPDATEDDATE                                
     FROM POS_DAYCLOSURE                                 
   WHERE BRANCHCOUNTERID = @BranchCounterID
 
                                
  SELECT DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE,          
  MOPID,CREATEDDATE,UPDATEDDATE,COUNTERID                                
     FROM POS_DAYCLOSUREDETAIL                                      
   WHERE COUNTERID = @BranchCounterID

END
GO