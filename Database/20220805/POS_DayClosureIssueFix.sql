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


/****** Object:  StoredProcedure [dbo].[POS_USP_CU_DAYCLOSURE]    Script Date: 05-08-2022 08:01:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[POS_USP_CU_DAYCLOSURE]              
@BRANCHCOUNTERID INT,              
@dtDenomination POS_BILLMOPVALUES readonly,              
@dtMOP POS_BILLMOPVALUES readonly,              
@RefundAmount decimal(10,2),          
@USERID INT          
, @DaySequenceID INT      
AS              
BEGIN     

DECLARE  @DAYCLOSUREID INT,@OpeningBalance decimal(18,2),              
@ClosingBalance decimal(18,2), @BillCount INT, @BillDetailCount INT, @CRefundCount INT              
              
select @OpeningBalance =   ISNULL(SUM(MOPVALUE),0) from POS_BILLMOPDETAIL BM              
WHERE BM.DAYCLOSUREID IS NULL  
              
SELECT @ClosingBalance = ISNULL(SUM(MOPVALUE),0) FROM @dtDenomination  
SELECT @ClosingBalance = @ClosingBalance +  ISNULL(SUM(MOPVALUE),0) FROM @dtMOP              
              
INSERT INTO POS_DAYCLOSURE(BRANCHCOUNTERID,OPENINGBALANCE,              
CLOSINGBALANCE,CLOSINGDIFFERENCE,REFUNDAMOUNT,CLOSUREDATE,CLOSEDBY,      
CREATEDDATE)      
SELECT   
@BRANCHCOUNTERID  
,@OpeningBalance  
,@ClosingBalance  
,@OpeningBalance - ISNULL(@RefundAmount,0) - @ClosingBalance  
,ISNULL(@RefundAmount,0)
,GETDATE()  
,@USERID  
,GETDATE()      
              
SET @DAYCLOSUREID = SCOPE_IDENTITY()              
              
INSERT INTO POS_DAYCLOSUREDETAIL(DAYCLOSUREID,DENOMINATIONID,CLOSUREVALUE,CREATEDDATE)      
SELECT @DAYCLOSUREID,MOPID,ISNULL(MOPVALUE,0),GETDATE() FROM @dtDenomination              
              
INSERT INTO POS_DAYCLOSUREDETAIL(DAYCLOSUREID,MOPID,CLOSUREVALUE,CREATEDDATE  )              
SELECT @DAYCLOSUREID,MOPID,ISNULL(MOPVALUE,0),GETDATE() FROM @dtMOP              
              
UPDATE POS_BILL SET DAYCLOSUREID = @DAYCLOSUREID,      
UPDATEDDATE = GETDATE(), UPDATEDBY = @USERID      
WHERE DAYCLOSUREID IS NULL    

SET @BillCount = @@ROWCOUNT

UPDATE POS_BILLDETAIL SET DAYCLOSUREID = @DAYCLOSUREID,      
UPDATEDDATE = GETDATE()           
WHERE DAYCLOSUREID IS NULL    
    
SET @BillDetailCount = @@ROWCOUNT

UPDATE POS_BILLMOPDETAIL SET DAYCLOSUREID = @DAYCLOSUREID,      
UPDATEDDATE = GETDATE()      
WHERE DAYCLOSUREID IS NULL    
              
UPDATE POS_CREFUND SET DAYCLOSUREID = @DAYCLOSUREID,      
UPDATEDDATE   = GETDATE(),      
UPDATEDBY = @USERID      
WHERE DAYCLOSUREID IS NULL      

SET @CRefundCount = @@ROWCOUNT
         
UPDATE POS_DAYSEQUENCE SET ISCLOSED = 1, UPDATEDATE = GETDATE() 
WHERE DAYSEQUENCEID = @DaySequenceID      

UPDATE POS_DAYCLOSURE
SET BILLCOUNT = @BillCount, BILLDETAILCOUNT = @BillDetailCount, CREFUNDCOUNT = @CRefundCount
WHERE DAYCLOSUREID = @DAYCLOSUREID

SELECT @DAYCLOSUREID    
      
END
GO


/****** Object:  StoredProcedure [dbo].[USP_R_GETSYNCDATA]    Script Date: 05-08-2022 10:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
  DELETEDDATE, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID , ROUNDING, B.SPLDISCPER, B.ISDOORDELIVERY, B.TENDEREDCASH, B.TENDEREDCHANGE    
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

