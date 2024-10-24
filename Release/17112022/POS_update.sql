USE [C:\Windows\NSRetailPOS\NSRETAILPOS.MDF]
GO
UPDATE TBLCONFIG SET CONFIGVALUE = '1.2.7' WHERE CONFIGID = 1
GO
ALTER PROC [dbo].[USP_R_GETSYNCDATA]                      
(                      
 @EntityName VARCHAR(50)                      
 ,  @SyncDate DATETIME                       
)                      
AS                      
BEGIN                      
                       
 IF @EntityName = 'POS_BILL'                      
 BEGIN                      
  SELECT BILLID, BILLNUMBER, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDDATE, DELETEDBY,                   
  DELETEDDATE, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID , ROUNDING, B.SPLDISCPER, B.ISDOORDELIVERY, B.TENDEREDCASH, B.TENDEREDCHANGE,B.CUSTOMERGST    
  FROM POS_BILL B                      
  WHERE                      
   B.CREATEDDATE > @SyncDate                      
   OR B.UPDATEDDATE > @SyncDate                      
   OR B.DELETEDDATE > @SyncDate                      
  RETURN                      
 END                      
                      
 IF @EntityName = 'POS_BILLDETAIL'                      
 BEGIN                      
  SELECT BILLDETAILID, BILLID, ITEMPRICEID, QUANTITY, WEIGHTINKGS,                   
  BILLEDAMOUNT, CREATEDDATE, UPDATEDDATE, DELETEDDATE, CGST                      
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
  SELECT BILLMOPDETAILID,BILLID,MOPID,MOPVALUE,CREATEDDATE,UPDATEDDATE,DAYCLOSUREID    
  FROM POS_BILLMOPDETAIL BMOP                    
  WHERE                      
   BMOP.CREATEDDATE > @SyncDate                      
   OR BMOP.UPDATEDDATE > @SyncDate                      
  RETURN                      
 END                     
                       
 IF @EntityName = 'STOCKDISPATCH'                      
 BEGIN                      
  SELECT STOCKDISPATCHID, FROMBRANCHID, TOBRANCHID, STATUS, STATUSAPPROVEDBY, STATUSAPPROVEDDATE,                   
  CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDATE, DELETEDBY, DELETEDDATE, DISPATCHNUMBER                      
  FROM POS_STOCKDISPATCH SD                      
  WHERE                      
   SD.CREATEDDATE > @SyncDate                      
   OR SD.UPDATEDATE > @SyncDate                      
   OR SD.DELETEDDATE > @SyncDate                      
  RETURN                      
 END                      
                      
 IF @EntityName = 'STOCKDISPATCHDETAIL'                      
 BEGIN                      
  SELECT STOCKDISPATCHDETAILID, STOCKDISPATCHID, ITEMPRICEID, TRAYNUMBER, DISPATCHQUANTITY,                   
  RECEIVEDQUANTITY, CREATEDDATE, UPDATEDATE, DELETEDDATE, WEIGHTINKGS,ISACCEPTED    
  FROM POS_STOCKDISPATCHDETAIL SDD                      
  WHERE                      
   SDD.CREATEDDATE > @SyncDate                      
   OR SDD.UPDATEDATE > @SyncDate                      
   OR SDD.DELETEDDATE > @SyncDate                      
  RETURN                      
 END                      
                       
 IF @EntityName = 'USER'                      
 BEGIN                      
  SELECT USERID, ROLEID, REPORTINGLEADID, CATEGORYID,                     
  BRANCHID, USERNAME, PASSWORDSTRING, FULLNAME, CNUMBER, EMAIL,                     
  ISOTP, GENDER, DOB, CREATEDDATE, UPDATEDDATE, DELETEDDATE                      
  FROM POS_TBLUSER USR                      
  WHERE                      
   USR.CREATEDDATE > @SyncDate                      
   OR USR.UPDATEDDATE > @SyncDate                      
   OR USR.DELETEDDATE > @SyncDate                      
  RETURN                      
 END                     
                     
 IF @EntityName = 'POS_DAYCLOSURE'                      
 BEGIN                      
  SELECT DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,      
CLOSINGBALANCE,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,                    
CREATEDDATE,UPDATEDDATE, BILLCOUNT, BILLDETAILCOUNT, CREFUNDCOUNT                    
  FROM POS_DAYCLOSURE DC                    
  WHERE                      
   DC.CREATEDDATE > @SyncDate                      
   OR DC.UPDATEDDATE > @SyncDate                      
  RETURN                      
 END                     
                    
  IF @EntityName = 'POS_DAYCLOSUREDETAIL'            
 BEGIN                      
  SELECT DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,                    
CLOSUREVALUE,MOPID,CREATEDDATE,UPDATEDDATE                    
  FROM POS_DAYCLOSUREDETAIL DCD                    
  WHERE                      
   DCD.CREATEDDATE > @SyncDate                      
   OR DCD.UPDATEDDATE > @SyncDate                      
  RETURN                      
 END                     
                    
  IF @EntityName = 'POS_CREFUND'                      
 BEGIN                      
  SELECT REFUNDID,BILLDETAILID,REFUNDQUANTITY,REFUNDWEIGHTINKGS,                    
REFUNDAMOUNT,CREATEDBY,CREATEDDATE,UPDATEDBY,UPDATEDDATE,                    
DELETEDBY,DELETEDDATE,DAYCLOSUREID                    
  FROM POS_CREFUND CR                    
  WHERE                      
   CR.CREATEDDATE > @SyncDate                      
   OR CR.UPDATEDDATE > @SyncDate                      
  RETURN                      
 END                     
                    
 IF @EntityName = 'POS_BREFUND'                      
 BEGIN                      
  SELECT BREFUNDID,BRANCHID,CREATEDBY,CREATEDATE,                    
UPDATEDBY,UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS,BREFUNDNUMBER                    
  FROM POS_BREFUND BR                    
  WHERE                      
   BR.CREATEDATE > @SyncDate                      
   OR BR.UPDATEDDATE > @SyncDate                      
  RETURN                      
 END                     
                    
 IF @EntityName = 'POS_BREFUNDDETAIL'                      
 BEGIN                      
  SELECT BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,                    
WEIGHTINKGS,CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,REASONID,DELETEDDATE  
  FROM POS_BREFUNDDETAIL BRD                    
  WHERE                      
   BRD.CREATEDDATE > @SyncDate                      
   OR BRD.UPDATEDDATE > @SyncDate                      
   OR BRD.DELETEDDATE > @SyncDate                      
  RETURN                      
 END          
     
 IF @EntityName = 'POS_DAYSEQUENCE'                      
 BEGIN                      
  SELECT OPENDATE, LASTUSEDBILLNUM, ISCLOSED, BRANCHCOUNTERID, LASTBILLID, DAYSEQUENCEID, OPENBILLID    
 , CREATEDATE, UPDATEDATE               
  FROM POS_DAYSEQUENCE DS    
  WHERE                      
   DS.CREATEDATE > @SyncDate                      
   OR DS.UPDATEDATE > @SyncDate                      
  RETURN                      
 END      
                       
END
GO
DROP PROC [dbo].[USP_CU_POS_IMPORTDATA]        
GO
/****** Object:  UserDefinedTableType [dbo].[POS_BILLTYPE]    Script Date: 15-11-2022 15:29:39 ******/
DROP TYPE [dbo].[POS_BILLTYPE]
GO

/****** Object:  UserDefinedTableType [dbo].[POS_BILLTYPE]    Script Date: 15-11-2022 15:29:39 ******/
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
	SPLDISCPER [decimal](3, 2) NULL,
	ISDOORDELIVERY bit ,     
	TENDEREDCASH [decimal](18, 2) NULL,
	TENDEREDCHANGE [decimal](18, 2) NULL,
	CUSTOMERGST nvarchar (100)
)
GO
CREATE PROC [dbo].[USP_CU_POS_IMPORTDATA]        
(        
 @Bill POS_BILLTYPE READONLY        
 , @BillDetail POS_BILLDETAILTYPE READONLY        
 , @BillMOPDetail POS_BILLMOPDETAILTYPE READONLY        
 , @DaySequence POS_DAYSEQUENCETYPE READONLY     
 , @CRefund POS_IMP_CREFUNDTYPE READONLY
 , @BRefund POS_BREFUNDTYPE READONLY
 , @BRefundDetail POS_BREFUNDDETAILTYPE READONLY
 , @DayClosure POS_DAYCLOSURETYPE READONLY
 , @DayClosureDetail POS_DAYCLOSUREDETAILTYPE READONLY
)        
AS        
BEGIN  

	SET IDENTITY_INSERT POS_BILL ON

	INSERT INTO POS_BILL(BILLID, BILLNUMBER, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE
		, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME, DAYCLOSUREID, ROUNDING, 
		SPLDISCPER, ISDOORDELIVERY, TENDEREDCASH, TENDEREDCHANGE, CUSTOMERGST)
	SELECT BILLID, BILLNUMBER, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE
		, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME, DAYCLOSUREID, ROUNDING
		, SPLDISCPER, ISDOORDELIVERY, TENDEREDCASH, TENDEREDCHANGE, CUSTOMERGST
	FROM @Bill UB

	SET IDENTITY_INSERT POS_BILL OFF

	SET IDENTITY_INSERT POS_BILLDETAIL ON

	INSERT INTO POS_BILLDETAIL(BILLDETAILID, BILLID, ITEMPRICEID, QUANTITY, WEIGHTINKGS, BILLEDAMOUNT, CREATEDDATE, UPDATEDDATE
		, DELETEDDATE, CGST, SGST, IGST, CESS, GSTVALUE, GSTID, SNO, DISCOUNT, OFFERID, DAYCLOSUREID)
	SELECT BILLDETAILID, BILLID, ITEMPRICEID, QUANTITY, WEIGHTINKGS, BILLEDAMOUNT, CREATEDDATE, UPDATEDDATE
		, DELETEDDATE, CGST, SGST, IGST, CESS, GSTVALUE, GSTID, SNO, DISCOUNT, OFFERID, DAYCLOSUREID
	FROM @BillDetail UBD

	SET IDENTITY_INSERT POS_BILLDETAIL OFF

	SET IDENTITY_INSERT POS_BILLMOPDETAIL ON

	INSERT INTO POS_BILLMOPDETAIL(BILLMOPDETAILID, BILLID, MOPID, MOPVALUE, CREATEDDATE, UPDATEDDATE, DAYCLOSUREID)
	SELECT BILLMOPDETAILID, BILLID, MOPID, MOPVALUE, CREATEDDATE, UPDATEDDATE, DAYCLOSUREID
	FROM @BillMOPDetail UBMOP

	SET IDENTITY_INSERT POS_BILLMOPDETAIL OFF
        
	SET IDENTITY_INSERT POS_DAYSEQUENCE ON

	INSERT INTO POS_DAYSEQUENCE(OPENDATE, LASTUSEDBILLNUM, ISCLOSED, BRANCHCOUNTERID, LASTBILLID, DAYSEQUENCEID
		, OPENBILLID, CREATEDATE, UPDATEDATE)
	SELECT OPENDATE, LASTUSEDBILLNUM, ISCLOSED, BRANCHCOUNTERID, LASTBILLID, DAYSEQUENCEID
		, OPENBILLID, CREATEDATE, UPDATEDATE
	FROM @DaySequence UDS

	SET IDENTITY_INSERT POS_DAYSEQUENCE OFF

	SET IDENTITY_INSERT POS_CREFUND ON

	INSERT INTO POS_CREFUND(REFUNDID, BILLDETAILID, REFUNDQUANTITY, REFUNDWEIGHTINKGS, REFUNDAMOUNT, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDDATE
		, DELETEDBY, DELETEDDATE, DAYCLOSUREID)
	SELECT REFUNDID, BILLDETAILID, REFUNDQUANTITY, REFUNDWEIGHTINKGS, REFUNDAMOUNT, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDDATE
		, DELETEDBY, DELETEDDATE, DAYCLOSUREID
	FROM @CRefund UCR

	SET IDENTITY_INSERT POS_CREFUND OFF
        
	SET IDENTITY_INSERT POS_BREFUND ON

	INSERT INTO POS_BREFUND(BREFUNDID, BRANCHID, CREATEDBY, CREATEDATE, UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, STATUS, BREFUNDNUMBER)
	SELECT BREFUNDID, BRANCHID, CREATEDBY, CREATEDATE, UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, STATUS, BREFUNDNUMBER
	FROM @BRefund UBR

	SET IDENTITY_INSERT POS_BREFUND OFF

	SET IDENTITY_INSERT POS_BREFUNDDETAIL ON

	INSERT INTO POS_BREFUNDDETAIL(BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,WEIGHTINKGS,CREATEDDATE
					,UPDATEDDATE,SNO,TRAYNUMBER,REASONID,DELETEDDATE)
	SELECT BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,WEIGHTINKGS,CREATEDDATE
					,UPDATEDDATE,SNO,TRAYNUMBER,REASONID,DELETEDDATE FROM @BRefundDetail UBR

	SET IDENTITY_INSERT POS_BREFUNDDETAIL OFF


	SET IDENTITY_INSERT POS_DAYCLOSURE ON

	INSERT INTO POS_DAYCLOSURE(DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,CLOSINGBALANCE
				,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDDATE,UPDATEDDATE)
	SELECT DAYCLOSUREID,CLOSUREDATE,BRANCHCOUNTERID,OPENINGBALANCE,CLOSINGBALANCE
				,CLOSINGDIFFERENCE,CLOSEDBY,REFUNDAMOUNT,CREATEDDATE,UPDATEDDATE FROM @DayClosure

	SET IDENTITY_INSERT POS_DAYCLOSURE OFF

	SET IDENTITY_INSERT POS_DAYCLOSUREDETAIL ON

	INSERT INTO POS_DAYCLOSUREDETAIL(DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE
					,MOPID,CREATEDDATE,UPDATEDDATE)
	SELECT DAYCLOSUREDETAILID,DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE
					,MOPID,CREATEDDATE,UPDATEDDATE FROM @DayClosureDetail

	SET IDENTITY_INSERT POS_DAYCLOSUREDETAIL OFF
END
GO