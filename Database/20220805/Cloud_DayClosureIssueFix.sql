IF NOT EXISTS ( SELECT 1 FROM SYS.columns WHERE [object_id] = OBJECT_ID('POS_DAYCLOSURE') AND [name] = 'BILLCOUNT' )
BEGIN
	EXEC ('ALTER TABLE POS_DAYCLOSURE ADD BILLCOUNT INT')
END
GO
IF NOT EXISTS ( SELECT 1 FROM SYS.columns WHERE [object_id] = OBJECT_ID('POS_DAYCLOSURE') AND [name] = 'BILLDETAILCOUNT' )
BEGIN
	EXEC ('ALTER TABLE POS_DAYCLOSURE ADD BILLDETAILCOUNT INT')
END
GO
IF NOT EXISTS ( SELECT 1 FROM SYS.columns WHERE [object_id] = OBJECT_ID('POS_DAYCLOSURE') AND [name] = 'CREFUNDCOUNT' )
BEGIN
	EXEC ('ALTER TABLE POS_DAYCLOSURE ADD CREFUNDCOUNT INT')
END
GO

DROP PROC USP_CU_POS_DAYCLOSURE
GO

/****** Object:  UserDefinedTableType [dbo].[POS_DAYCLOSURETYPE]    Script Date: 05-08-2022 20:53:20 ******/
DROP TYPE [dbo].[POS_DAYCLOSURETYPE]
GO

/****** Object:  UserDefinedTableType [dbo].[POS_DAYCLOSURETYPE]    Script Date: 05-08-2022 20:53:20 ******/
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
	[UPDATEDDATE] [datetime] NULL,
	[BILLCOUNT] INT NULL,
	[BILLDETAILCOUNT] INT NULL,
	[CREFUNDCOUNT] INT NULL
)
GO



/****** Object:  StoredProcedure [dbo].[USP_CU_POS_DAYCLOSURE]    Script Date: 05-08-2022 20:52:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER PROC [dbo].[USP_CU_POS_DAYCLOSURE]        
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
		, DC.BILLCOUNT = UDC.BILLCOUNT
		, DC.BILLDETAILCOUNT = UDC.BILLDETAILCOUNT
		, DC.CREFUNDCOUNT = UDC.CREFUNDCOUNT
		, DC.SYNCDATE = GETUTCDATE() + '05:30'
	FROM         
		POS_DAYCLOSURE DC      
		INNER JOIN @DayClosure UDC ON UDC.DAYCLOSUREID = DC.DAYCLOSUREID     
		AND UDC.BRANCHCOUNTERID = DC.BRANCHCOUNTERID      
        
	INSERT INTO POS_DAYCLOSURE(DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,    
		CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDDATE,UPDATEDDATE, SYNCDATE
		, BILLCOUNT, BILLDETAILCOUNT, CREFUNDCOUNT)        
	SELECT DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,    
		CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDDATE,UPDATEDDATE, GETUTCDATE() + '05:30'
		, BILLCOUNT, BILLDETAILCOUNT, CREFUNDCOUNT
	FROM @DayClosure UDC      
	WHERE NOT EXISTS        
		(        
			SELECT 1 FROM POS_DAYCLOSURE DCINNER 
			WHERE DCINNER.DAYCLOSUREID = UDC.DAYCLOSUREID      
				AND DCINNER.BRANCHCOUNTERID = UDC.BRANCHCOUNTERID      
		)        
END
GO

/****** Object:  StoredProcedure [dbo].[USP_R_GETSYNCDATA]    Script Date: 05-08-2022 21:09:59 ******/
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
   (USR.BRANCHID = @BranchID OR @BranchID = 0 OR (R.ROLENAME = 'Store Admin' AND USR.BRANCHID = 45))                                    
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
  CREATEDDATE,UPDATEDBY,UPDATEDDATE, BILLCOUNT, BILLDETAILCOUNT, CREFUNDCOUNT                              
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

