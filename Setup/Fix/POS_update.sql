USE [C:\Windows\NSRetailPOS\NSRETAILPOS.MDF]
GO
:ON ERROR EXIT
GO

IF NOT EXISTS ( SELECT 1 FROM SYS.ALL_COLUMNS WHERE [NAME] = 'ISIGSTBILL' AND [OBJECT_ID] = OBJECT_ID('POS_BILL') )
BEGIN
	EXEC ('ALTER TABLE POS_BILL ADD ISIGSTBILL BIT DEFAULT 0')
	EXEC ('UPDATE POS_BILL SET ISIGSTBILL = 0')
END
GO

/****** Object:  StoredProcedure [dbo].[POS_USP_FINISH_BILL]    Script Date: 14-03-2026 09:46:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[POS_USP_FINISH_BILL]      
 @BillID INT      
 , @UserID INT      
 , @DaySequenceID INT      
 , @CustomerNumber VARCHAR(10) = NULL      
 , @CustomerName VARCHAR(100) = NULL      
 , @CustomerGST VARCHAR(100) = NULL      
 , @Rounding DECIMAL(3, 2)
 , @IsDoorDelivery BIT
 , @TenderedCash DECIMAL(11, 2) = 0.00
 , @TenderedChange DECIMAL(11, 2) = 0.00
 , @MopValues  POS_BILLMOPVALUES READONLY      
AS      
BEGIN      
    
	IF NOT EXISTS
	(
		SELECT 1
		FROM @MopValues      
		WHERE MOPVALUE <> 0 
	)
	BEGIN
		SELECT 'Payment details missing, please retry or contact administrator if the problem persists'
		RETURN
	END

	INSERT INTO POS_BILLMOPDETAIL(BILLID, MOPID, MOPVALUE,CREATEDDATE)      
	SELECT @BillID, MOPID, MOPVALUE,GETDATE()  
	FROM @MopValues      
	WHERE MOPVALUE <> 0    
	
	DECLARE @IsIGSTBill BIT = 0
	
	IF EXISTS ( SELECT 1 WHERE LEN(@CustomerGST) >= 15 AND @CustomerGST NOT LIKE '37%' )
	BEGIN
		
		UPDATE POS_BILLDETAIL
		SET IGST = SGST + CGST, SGST = 0, CGST = 0, UPDATEDDATE = GETDATE()
		WHERE BILLID = @BillID

		SELECT @IsIGSTBill = 1

	END
      
	UPDATE POS_BILL      
	SET       
		BILLSTATUS = 1      
		, CUSTOMERNAME = @CustomerName      
		, CUSTOMERNUMBER = @CustomerNumber 
		, CUSTOMERGST = @CustomerGST
		, ISDOORDELIVERY = @IsDoorDelivery
		, TENDEREDCASH = @TenderedCash
		, TENDEREDCHANGE = @TenderedChange
		, UPDATEDBY = @UserID      
		, UPDATEDDATE = GETDATE()
		, BILLCLOSEDBY = @UserID
		, BILLCLOSEDDATE = GETDATE()
		, ROUNDING = @Rounding  
		, ISIGSTBILL = @IsIGSTBill
	WHERE BILLID = @BillID       
       
	UPDATE POS_DAYSEQUENCE      
	SET LASTBILLID = @BillID, UPDATEDATE = GETDATE()
	WHERE DAYSEQUENCEID = @DaySequenceID      
      
	EXEC POS_USP_R_GETNEXTBILL @UserID, @DaySequenceID    
 
END
GO

/****** Object:  StoredProcedure [dbo].[POS_USP_R_BILL]    Script Date: 16-03-2026 08:17:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[POS_USP_R_BILL]                                 
 @BillID INT,                     
 @DaySequenceID INT                                
AS                                
BEGIN                                
                                
	 DECLARE @LastBillID INT                                
	 SELECT @LastBillID = LASTBILLID FROM POS_DAYSEQUENCE WHERE DAYSEQUENCEID = @DaySequenceID                                
                                
	 DECLARE @LastBilledAmount DECIMAL(11, 2)                                
	 DECLARE @LastBilledQuantity INT                                
                                
	 SELECT @LastBilledAmount = SUM(BD.BILLEDAMOUNT), @LastBilledQuantity = SUM(BD.QUANTITY)           
	 FROM POS_BILLDETAIL BD WHERE BD.BILLID = @LastBillID AND BD.DELETEDDATE IS NULL                                
                                
	 SELECT                                 
		  B.BILLID, B.BILLNUMBER, B.BILLSTATUS, B.ISIGSTBILL,                             
		  @LastBilledAmount AS LASTBILLEDAMOUNT, @LastBilledQuantity AS LASTBILLEDQUANTITY  ,                            
		  @LastBillID AS LASTBILLID, B.CREATEDDATE, B.ROUNDING,B.ISDOORDELIVERY,
		  B.CUSTOMERNAME, B.CUSTOMERNUMBER,B.CUSTOMERGST, B.TENDEREDCASH, B.TENDEREDCHANGE
	  FROM                                 
		  POS_BILL B                                
		  LEFT JOIN POS_BILL LASTBILL ON LASTBILL.BILLID = @LastBillID                                 
	 WHERE B.BILLID = @BillID                                
                                
	 SELECT                                
		  BD.BILLDETAILID, B.BILLID, IP.ITEMPRICEID
		  , CASE WHEN BD.DELETEDDATE IS NULL THEN BD.SNO ELSE NULL END SNO
		  , I.ITEMNAME, IC.ITEMCODE, ISNULL(IC.HSNCODE,'') AS HSNCODE,IP.MRP                              
		  ,IP.SALEPRICE, GST.GSTCODE, BD.QUANTITY,                        
		  BD.WEIGHTINKGS, BD.BILLEDAMOUNT,                        
		  BD.CGST, BD.SGST, BD.IGST, BD.CESS,                        
		  BD.GSTVALUE, BD.GSTID,GST.CGST AS CGSTDESC,                        
		  GST.SGST AS SGSTDESC, GST.IGST AS IGSTDESC ,GST.CESS AS CESSDESC,                        
		  ISNULL(I.ISOPENITEM,0) AS ISOPENITEM, DISCOUNT, BD.OFFERID, OFRTYP.OFFERTYPECODE     
		  , BD.DELETEDDATE
	 FROM                                
		  POS_BILLDETAIL BD                                
		  INNER JOIN POS_BILL B ON B.BILLID = BD.BILLID                                
		  INNER JOIN POS_ITEMPRICE IP ON IP.ITEMPRICEID = BD.ITEMPRICEID                                
		  INNER JOIN POS_ITEMCODE IC ON IC.ITEMCODEID = IP.ITEMCODEID                                
		  INNER JOIN POS_ITEM I ON I.ITEMID = IC.ITEMID                                
		  INNER JOIN POS_GSTDETAIL GST ON GST.GSTID = IP.GSTID                 
		  LEFT JOIN POS_OFFER OFR ON OFR.OFFERID = BD.OFFERID            
		  LEFT JOIN POS_OFFERTYPE OFRTYP ON OFRTYP.OFFERTYPEID = OFR.OFFERTYPEID            
	 WHERE B.BILLID = @BillID --AND BD.DELETEDDATE IS NULL              
	 ORDER BY BD.SNO          
	 
	 SELECT PGWTD.BILLID, PGWTD.MOPID, PGWTD.AMOUNT, PGWTD.PAYMENTREQUEST, PGWTD.PAYMENTRESPONSE, PGWTD.ADDITIONALCONFIG
		, PGWTD.CREATEDDATE
	 FROM POS_PGW_TRANSACTIONDATA PGWTD
	 WHERE PGWTD.BILLID = @BillID
END
GO

/****** Object:  StoredProcedure [dbo].[USP_R_GETSYNCDATA]    Script Date: 16-03-2026 09:10:28 ******/
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
			DELETEDDATE, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID , 
			ROUNDING, B.SPLDISCPER, B.ISDOORDELIVERY, B.TENDEREDCASH, B.TENDEREDCHANGE,B.CUSTOMERGST,
			BILLCLOSEDBY, BILLCLOSEDDATE, B.ISIGSTBILL
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
UPDATEDBY,UPDATEDDATE,DELETEDBY,DELETEDDATE,STATUS,BREFUNDNUMBER, CATEGORYID                    
  FROM POS_BREFUND BR                    
  WHERE                      
   BR.CREATEDATE > @SyncDate                      
   OR BR.UPDATEDDATE > @SyncDate                      
  RETURN                      
 END                     
                    
 IF @EntityName = 'POS_BREFUNDDETAIL'                      
 BEGIN                      
  SELECT BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,                    
WEIGHTINKGS,CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,REASONID,DELETEDDATE, REFUNDDESCRIPTION
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

 IF @EntityName = 'POS_PGW_TRANSACTIONDATA'          
	BEGIN          
		SELECT 
			TRANSACTIONDATAID
			,BILLID          
			,MOPID           
			,AMOUNT          
			,PAYMENTREQUEST  
			,PAYMENTRESPONSE 
			,PAYMENTGATEWAYID
			,ADDITIONALCONFIG
			,CREATEDDATE     
		FROM POS_PGW_TRANSACTIONDATA TRS 
		WHERE CREATEDDATE > @SyncDate               
	RETURN          
	END     
                       
END

GO

UPDATE TBLCONFIG SET CONFIGVALUE = '1.7.6' WHERE CONFIGID = 1
GO
