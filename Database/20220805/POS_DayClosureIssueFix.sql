USE [C:\Windows\NSRetailPOS\NSRetailPOS.mdf]
GO

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

GO


/****** Object:  UserDefinedFunction [dbo].[UDF_GETOFFERS]    Script Date: 12-08-2022 12:25:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[UDF_GETOFFERS] (@ItemPriceID INT) 
RETURNS @Offer TABLE (ITEMPRICEID INT, OFFERID INT, OFFERAPPLIESTOID INT, DEALID INT, DEALAPPLIESTOID INT)
AS
BEGIN

	DECLARE @ItemCodeID INT, @CategoryID INT, @MaxDate DATE, @OfferID INT, @DealID INT
	SELECT @MaxDate = '2100-01-01'	 

	SELECT @ItemCodeID = IC.ITEMCODEID, @CategoryID = I.CATEGORYID
	FROM 
		POS_ITEMPRICE IP
		INNER JOIN POS_ITEMCODE IC ON IC.ITEMCODEID = IP.ITEMCODEID
		INNER JOIN POS_ITEM I ON I.ITEMID = IC.ITEMID
	WHERE IP.ITEMPRICEID = @ItemPriceID

	DECLARE @Offers TABLE (OFFERID INT, APPLIESTOID INT, OFFERTYPEID INT)

	INSERT INTO @Offers
	SELECT OFR.OFFERID, OFR.APPLIESTOID, OFR.OFFERTYPEID
	FROM
		POS_OFFER OFR
		INNER JOIN POS_OFFERBRANCH OFRB ON OFRB.OFFERID = OFR.OFFERID
	WHERE 
		GETDATE() BETWEEN OFR.STARTDATE AND ISNULL(OFR.ENDDATE, @MaxDate)
		AND OFR.ISACTIVE = 1
		AND OFR.DELETEDDATE IS NULL
		AND OFRB.DELETEDDATE IS NULL
		AND 
			(
				EXISTS (SELECT 1 FROM POS_OFFERITEMMAP OFRIM WHERE OFRIM.OFFERID = OFR.OFFERID AND OFRIM.ITEMCODEID = @ItemCodeID AND OFRIM.DELETEDDATE IS NULL)
				OR OFR.CATEGORYID = @CategoryID
				OR EXISTS 
					(
						SELECT 1 
						FROM 
							POS_ITEMGROUP IG 
							INNER JOIN POS_ITEMGROUPDETAIL IGD ON IGD.ITEMGROUPID = IG.ITEMGROUPID
						WHERE 
							IGD.ITEMCODEID = @ItemCodeID
							AND OFR.ITEMGROUPID = IG.ITEMGROUPID
							AND IG.DELETEDDATE IS NULL
							AND IGD.DELETEDDATE IS NULL
					)
			)
	ORDER BY APPLIESTOID ASC, OFR.OFFERID DESC

	
	SELECT TOP 1 @OfferID = OFFERID
	FROM @Offers
	WHERE OFFERTYPEID IN (1, 2, 3)
	ORDER BY APPLIESTOID ASC, OFFERID DESC

	SELECT @DealID = OFFERID
	FROM @Offers
	WHERE OFFERTYPEID IN (4, 5)
	ORDER BY APPLIESTOID ASC, OFFERID DESC

	INSERT INTO @Offer(ITEMPRICEID, OFFERID, DEALID)
	SELECT @ItemPriceID, @OfferID, @DealID
	
	UPDATE OFRTMP
	SET 
		OFRTMP.OFFERAPPLIESTOID = OFRID.APPLIESTOID
		, OFRTMP.DEALAPPLIESTOID = DLID.APPLIESTOID
	FROM 
		@Offer OFRTMP
		LEFT JOIN POS_OFFER OFRID ON OFRID.OFFERID = OFRTMP.OFFERID 
		LEFT JOIN POS_OFFER DLID ON DLID.OFFERID = OFRTMP.DEALID
	

	RETURN
END

GO

/****** Object:  StoredProcedure [dbo].[POS_USP_R_GETBILLOFFERS]    Script Date: 12-08-2022 17:43:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER   PROC [dbo].[POS_USP_R_GETBILLOFFERS]
(
	@BillID INT
)
AS
BEGIN

	DECLARE @MaxDate DATE, @OfferID INT, @TotalBill DECIMAL(14, 2)
	SELECT @MaxDate = '2100-01-01'
	
	SELECT OFR.OFFERID, OFR.OFFERVALUE
	INTO #AvailableOffers
	FROM
		POS_OFFER OFR
		INNER JOIN POS_OFFERBRANCH OFRB ON OFRB.OFFERID = OFR.OFFERID	
		INNER JOIN POS_OFFERTYPE OFRTYP ON OFRTYP.OFFERTYPEID = OFR.OFFERTYPEID
	WHERE 
		GETDATE() BETWEEN OFR.STARTDATE AND ISNULL(OFR.ENDDATE, @MaxDate)
		AND OFR.ISACTIVE = 1
		AND OFR.DELETEDDATE IS NULL
		AND OFRB.DELETEDDATE IS NULL
		AND OFRTYP.OFFERTYPEID = 1004

	IF EXISTS ( SELECT 1 FROM #AvailableOffers )
	BEGIN

		SELECT @TotalBill = SUM(PBD.BILLEDAMOUNT)
		FROM POS_BILLDETAIL PBD
		WHERE 
			PBD.DELETEDDATE IS NULL
			AND PBD.BILLID = @BillID

		SELECT TOP 1 @OfferID = OFR.OFFERID
		FROM #AvailableOffers OFR
		WHERE OFR.OFFERVALUE <= @TotalBill
		ORDER BY OFR.OFFERVALUE DESC

		CREATE TABLE #ITEMCODES(ITEMCODEID INT)

		INSERT INTO #ITEMCODES(ITEMCODEID)
		SELECT OFRIC.ITEMCODEID			
		FROM POS_OFFERITEMMAP OFRIC
		WHERE OFRIC.OFFERID = @OfferID

		INSERT INTO #ITEMCODES(ITEMCODEID)
		SELECT IGD.ITEMCODEID			
		FROM 
			POS_OFFER OFR
			INNER JOIN POS_ITEMGROUP IG ON IG.ITEMGROUPID = OFR.ITEMGROUPID
			INNER JOIN POS_ITEMGROUPDETAIL IGD ON IGD.ITEMGROUPID = IG.ITEMGROUPID
		WHERE OFR.OFFERID = @OfferID

		SELECT IP.ITEMPRICEID, I.SKUCODE, I.ITEMNAME, IC.ITEMCODE, IP.MRP, IP.SALEPRICE
		FROM
			#ITEMCODES ICL
			INNER JOIN POS_ITEMCODE IC ON IC.ITEMCODEID = ICL.ITEMCODEID
			INNER JOIN POS_ITEMPRICE IP ON IP.ITEMCODEID = IC.ITEMCODEID
			INNER JOIN POS_ITEM I ON I.ITEMID = IC.ITEMID
	END
END

GO


/****** Object:  StoredProcedure [dbo].[POS_USP_CU_BILLDETAIL]    Script Date: 12-08-2022 15:37:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[POS_USP_CU_BILLDETAIL]                
 @BillID int                
 , @ItemPriceID INT                
 , @Quantity INT                
 , @WeightInKgs DECIMAL(7, 2)                
 , @UserID INT              
 , @BillDetailID INT     
 , @IsBillOfferItem BIT = 0
AS                
BEGIN           
         
 DECLARE @QuantityOrWeightMultiplier DECIMAL(7, 2)        
        
 SELECT @BillDetailID = BILLDETAILID        
 , @Quantity = @Quantity + ISNULL(BD.QUANTITY, 0)        
 , @WeightInKgs = @WeightInKgs + ISNULL(BD.WEIGHTINKGS, 0)          
 FROM POS_BILLDETAIL BD          
 WHERE           
  BD.BILLID = @BillID          
  AND BD.ITEMPRICEID = @ItemPriceID          
  AND BD.DELETEDDATE IS NULL          
  AND @BillDetailID <= 0  AND @IsBillOfferItem = 0     
          
  SELECT        
 @QuantityOrWeightMultiplier =         
  CASE         
   WHEN ISNULL(I.ISOPENITEM, 0) = 0 THEN @Quantity         
  ELSE @WeightInKgs END        
  FROM        
 POS_ITEMPRICE IP         
 INNER JOIN POS_ITEMCODE IC ON IC.ITEMCODEID = IP.ITEMCODEID        
 INNER JOIN POS_ITEM I ON I.ITEMID = IC.ITEMID        
  WHERE        
 IP.ITEMPRICEID = @ItemPriceID        
            
 IF ISNULL(@BillDetailID, 0) <= 0                
 BEGIN                
            
  DECLARE @SNo INT          
          
  SELECT @SNo = MAX(SNO) FROM POS_BILLDETAIL WHERE BILLID = @BillID AND DELETEDDATE IS NULL          
  SELECT @SNo = ISNULL(@SNo, 0) + 1          
          
  INSERT INTO POS_BILLDETAIL(BILLID, ITEMPRICEID, QUANTITY, WEIGHTINKGS, CREATEDDATE, GSTID, SNO)                
  SELECT @BillID, @ItemPriceID, @Quantity, @WeightInKgs, GETDATE(), GSTID, @SNo          
  FROM POS_ITEMPRICE WHERE ITEMPRICEID = @ItemPriceID          
                
  SET @BillDetailID = SCOPE_IDENTITY()                
 END                
 ELSE                
 BEGIN                
  UPDATE POS_BILLDETAIL                
  SET                
   QUANTITY = @Quantity                
   , WEIGHTINKGS = @WeightInKgs                
   , UPDATEDDATE = GETDATE()                
  WHERE BILLDETAILID = @BillDetailID                
 END          
          
 DECLARE @OfferID INT, @DealID INT, @OfferAppliesToID INT, @DealAppliesToID INT, @SpecialDiscountPer DECIMAL(4, 2)          
 SELECT @OfferID = OFFERID, @OfferAppliesToID = OFFERAPPLIESTOID, @DealID = DEALID, @DealAppliesToID = DEALAPPLIESTOID          
 FROM UDF_GETOFFERS(@ItemPriceID) WHERE @IsBillOfferItem = 0     

 SELECT @OfferID = NULL,@OfferAppliesToID = null
 FROM POS_ITEMPRICE IP 
 WHERE IP.ITEMPRICEID = @ItemPriceID AND IP.MRP - DBO.UDF_GET_DISCOUNTVALUE(@OfferID, @ItemPriceID) > IP.SALEPRICE
           
 CREATE TABLE #BilledGroupItems(BILLDETAILID INT, TOTALQUANTITY INT, MRPQUANTITY INT, SALEPRICEQUANTITY INT, FREEQUANTITY INT)    
 
 SELECT @SpecialDiscountPer = SPLDISCPER FROM POS_BILL WHERE BILLID = @BillID
          
	IF(ISNULL(@SpecialDiscountPer, 0.0) > 0.0)
	BEGIN
		
		--If a special discount is applied, ignore all offers and deals
		SELECT @OfferID = NULL, @DealID = NULL

		DECLARE @SalePrice DECIMAL(7, 2), @DiscountValue DECIMAL(7 ,2)

		SELECT @SalePrice = IP.SALEPRICE, @DiscountValue = (IP.SALEPRICE * @SpecialDiscountPer) / 100
		FROM POS_ITEMPRICE IP WHERE IP.ITEMPRICEID = @ItemPriceID

		-- update billed amount and discount based on overall special discount
		UPDATE BD          
		SET          
			BD.BILLEDAMOUNT  = (IP.SALEPRICE - @DiscountValue) * @QuantityOrWeightMultiplier        
			, BD.DISCOUNT  = @DiscountValue * @QuantityOrWeightMultiplier          
		FROM           
			POS_BILLDETAIL BD          
			INNER JOIN POS_ITEMPRICE IP ON IP.ITEMPRICEID = BD.ITEMPRICEID          
			INNER JOIN POS_ITEMCODE IC ON IC.ITEMCODEID = IP.ITEMCODEID        
			INNER JOIN POS_ITEM I ON I.ITEMID = IC.ITEMID        
		 WHERE BD.BILLDETAILID = @BillDetailID    
		 
		 -- insert into # table so that GST is calculated and values are sent back to UI
		 INSERT INTO #BilledGroupItems(BILLDETAILID) SELECT @BillDetailID

	END

 -- if there is no offer and no deal, update billed amount directly          
 ELSE IF ISNULL(@DealID, 0) = 0 AND ISNULL(@OfferID, 0) = 0          
 BEGIN          
  UPDATE BD          
  SET          
   BD.BILLEDAMOUNT  = CASE WHEN @IsBillOfferItem = 1 THEN 0 ELSE IP.SALEPRICE * @QuantityOrWeightMultiplier END
   , BD.DISCOUNT  = ((IP.MRP - CASE WHEN @IsBillOfferItem = 1 THEN 0 ELSE IP.SALEPRICE END) * BD.QUANTITY)          
  FROM           
   POS_BILLDETAIL BD          
   INNER JOIN POS_ITEMPRICE IP ON IP.ITEMPRICEID = BD.ITEMPRICEID          
   INNER JOIN POS_ITEMCODE IC ON IC.ITEMCODEID = IP.ITEMCODEID        
   INNER JOIN POS_ITEM I ON I.ITEMID = IC.ITEMID        
  WHERE BD.BILLDETAILID = @BillDetailID          
 END          
 ELSE          
 BEGIN            
  DECLARE @MRP DECIMAL(10, 2), @OfferDiscount DECIMAL(10, 2)          
  SELECT @MRP = MRP FROM POS_ITEMPRICE WHERE ITEMPRICEID = @ItemPriceID          
             
  -- if only offer exists, apply offer directly          
  IF ISNULL(@DealID, 0) = 0 AND ISNULL(@OfferID, 0) > 0          
  BEGIN          
             
   SELECT @OfferDiscount = dbo.UDF_GET_DISCOUNTVALUE(@OfferID, @ItemPriceID) * @QuantityOrWeightMultiplier          
          
   UPDATE BD          
   SET          
    BD.BILLEDAMOUNT  = (@MRP * @QuantityOrWeightMultiplier) - @OfferDiscount          
    , BD.DISCOUNT  = @OfferDiscount           
    , BD.OFFERID = @OfferID          
   FROM           
    POS_BILLDETAIL BD          
    INNER JOIN POS_ITEMPRICE IP ON IP.ITEMPRICEID = BD.ITEMPRICEID                   
   WHERE BD.BILLDETAILID = @BillDetailID          
  END          
  -- If both offer and deal exists, apply one or both based on quantity          
  ELSE IF ISNULL(@DealID, 0) > 0          
  BEGIN          
          
   DECLARE @MRPQuantity INT, @FreeQuantity INT, @SalePriceQuantity INT          
             
   DECLARE @DealQuantity INT          
    SELECT @DealQuantity = OFRTYP.BUYQUANTITY + OFRTYP.FREEQUANTITY           
    FROM           
     POS_OFFER OFR          
     INNER JOIN POS_OFFERTYPE OFRTYP ON OFRTYP.OFFERTYPEID = OFR.OFFERTYPEID          
    WHERE          
     OFR.OFFERID = @DealID          
   -- if deal applies to same item then calculate based on quantity          
   --IF EXISTS ( SELECT 1 FROM #OfferMap OFRMP WHERE OFFERAPPLIESTOID = 1)          
   IF @DealAppliesToID = 1          
   BEGIN          
          
    SELECT @MRPQuantity = (@Quantity / (OFRTYPE.BUYQUANTITY + OFRTYPE.FREEQUANTITY)) * OFRTYPE.BUYQUANTITY          
     , @FreeQuantity = (@Quantity / (OFRTYPE.BUYQUANTITY + OFRTYPE.FREEQUANTITY)) * OFRTYPE.FREEQUANTITY          
     , @SalePriceQuantity = (@Quantity % (OFRTYPE.BUYQUANTITY + OFRTYPE.FREEQUANTITY))          
    FROM           
     POS_OFFER OFR          
     INNER JOIN POS_OFFERTYPE OFRTYPE ON OFRTYPE.OFFERTYPEID = OFR.OFFERTYPEID          
    WHERE OFR.OFFERID = @DealID          
          
    DECLARE @SalePriceDiscount DECIMAL(10, 2) = dbo.UDF_GET_DISCOUNTVALUE(@OfferID, @ItemPriceID)          
          
    UPDATE BD          
    SET           
     BD.BILLEDAMOUNT = (@MRPQuantity * @MRP) + (@SalePriceQuantity * (@MRP - @SalePriceDiscount))          
     , BD.DISCOUNT = (@FreeQuantity * @MRP) + (@SalePriceQuantity * @SalePriceDiscount)          
     ,OFFERID = CASE WHEN (@MRPQuantity + @FreeQuantity +@SalePriceQuantity) / @DealQuantity > 0           
     THEN @DealID ELSE @OfferID END          
    FROM          
     POS_BILLDETAIL BD          
    WHERE BD.BILLDETAILID = @BillDetailID          
   END          
   ELSE          
   BEGIN          
   -- this will be the complex case of deal to a group          
              
    INSERT INTO #BilledGroupItems          
    SELECT BD.BILLDETAILID, BD.QUANTITY AS TOTALQUANTITY, 0 AS MRPQUANTITY, 0 AS SALEPRICEQUANTITY, 0 AS FREEQUANTITY           
    FROM          
     POS_BILLDETAIL BD          
     INNER JOIN POS_ITEMPRICE IP ON IP.ITEMPRICEID = BD.ITEMPRICEID          
     INNER JOIN POS_ITEMGROUPDETAIL IGD ON IGD.ITEMCODEID = IP.ITEMCODEID          
     INNER JOIN POS_ITEMGROUP IG ON IG.ITEMGROUPID = IGD.ITEMGROUPID          
     INNER JOIN POS_OFFER OFR ON OFR.ITEMGROUPID = IG.ITEMGROUPID          
    WHERE          
     BD.BILLID = @BillID          
     AND BD.DELETEDDATE IS NULL          
     AND OFR.OFFERID = @DealID          
          
    DEClARE @BuyQuantity INT          
    SELECT @BuyQuantity = SUM(TOTALQUANTITY) FROM #BilledGroupItems          
             
    SELECT @MRPQuantity = (@BuyQuantity / (OFRTYPE.BUYQUANTITY + OFRTYPE.FREEQUANTITY)) * OFRTYPE.BUYQUANTITY          
     , @FreeQuantity = (@BuyQuantity / (OFRTYPE.BUYQUANTITY + OFRTYPE.FREEQUANTITY)) * OFRTYPE.FREEQUANTITY          
     , @SalePriceQuantity = (@BuyQuantity % (OFRTYPE.BUYQUANTITY + OFRTYPE.FREEQUANTITY))          
    FROM           
     POS_OFFER OFR          
     INNER JOIN POS_OFFERTYPE OFRTYPE ON OFRTYPE.OFFERTYPEID = OFR.OFFERTYPEID          
    WHERE OFR.OFFERID = @DealID          
                  
    -- Find lowest MRP items to give as free          
    WHILE @FreeQuantity > 0          
    BEGIN          
     DECLARE @CurBillDetailID INT, @CurQuantity INT          
          
     SELECT TOP 1 @CurBillDetailID = BGI.BILLDETAILID, @CurQuantity = BGI.TOTALQUANTITY          
     FROM           
      #BilledGroupItems BGI           
      INNER JOIN POS_BILLDETAIL BD ON BD.BILLDETAILID = BGI.BILLDETAILID          
      INNER JOIN POS_ITEMPRICE IP ON IP.ITEMPRICEID = BD.ITEMPRICEID          
     WHERE BGI.TOTALQUANTITY > 0          
     ORDER BY IP.MRP     
               
     DECLARE @FreeQuantityToupdate INT          
     IF @CurQuantity >= @FreeQuantity          
     BEGIN          
      SELECT @CurQuantity = @CurQuantity - @FreeQuantity, @FreeQuantityToupdate = @FreeQuantity, @FreeQuantity = 0          
     END          
     ELSE          
     BEGIN          
      SELECT @FreeQuantityToupdate = @FreeQuantity - @CurQuantity, @FreeQuantity = @FreeQuantity - @CurQuantity, @CurQuantity = 0          
     END          
          
     UPDATE #BilledGroupItems          
     SET TOTALQUANTITY = @CurQuantity, FREEQUANTITY = @FreeQuantityToupdate          
     WHERE BILLDETAILID = @CurBillDetailID          
    END          
          
    -- Find next lowest MRP items to give as sale price          
    WHILE @SalePriceQuantity > 0          
    BEGIN          
          
     SELECT TOP 1 @CurBillDetailID = BGI.BILLDETAILID, @CurQuantity = BGI.TOTALQUANTITY          
     FROM           
      #BilledGroupItems BGI           
      INNER JOIN POS_BILLDETAIL BD ON BD.BILLDETAILID = BGI.BILLDETAILID          
      INNER JOIN POS_ITEMPRICE IP ON IP.ITEMPRICEID = BD.ITEMPRICEID          
     WHERE BGI.TOTALQUANTITY > 0          
     ORDER BY IP.MRP          
          
     DECLARE @SalePriceToUpdate INT          
     IF @CurQuantity >= @SalePriceQuantity          
     BEGIN          
      SELECT @CurQuantity = @CurQuantity - @SalePriceQuantity, @SalePriceToUpdate = @SalePriceQuantity, @SalePriceQuantity = 0          
     END          
     ELSE          
     BEGIN          
      SELECT @SalePriceToUpdate = @SalePriceQuantity - @CurQuantity, @SalePriceQuantity = @SalePriceQuantity - @CurQuantity, @CurQuantity = 0          
     END          
          
     UPDATE #BilledGroupItems          
     SET TOTALQUANTITY = @CurQuantity, SALEPRICEQUANTITY = @SalePriceToUpdate          
     WHERE BILLDETAILID = @CurBillDetailID          
    END          
                  
    UPDATE #BilledGroupItems          
    SET MRPQUANTITY = TOTALQUANTITY           
          
    -- update quantities and MRP based prices instead of sale prices          
    UPDATE BD          
    SET             
     BD.BILLEDAMOUNT = (BGI.MRPQUANTITY * IP.MRP) + (BGI.SALEPRICEQUANTITY * (IP.MRP - dbo.UDF_GET_DISCOUNTVALUE(dbo.UDF_GETOFFER(IP.ITEMPRICEID), IP.ITEMPRICEID)))          
     , BD.DISCOUNT = (BGI.FREEQUANTITY * IP.MRP) + (BGI.SALEPRICEQUANTITY * dbo.UDF_GET_DISCOUNTVALUE(dbo.UDF_GETOFFER(IP.ITEMPRICEID), IP.ITEMPRICEID))          
     , BD.OFFERID = CASE WHEN @BuyQuantity / @DealQuantity > 0 THEN @DealID ELSE @OfferID END          
    FROM          
     POS_BILLDETAIL BD          
     INNER JOIN #BilledGroupItems BGI ON BGI.BILLDETAILID = BD.BILLDETAILID          
     INNER JOIN POS_ITEMPRICE IP ON IP.ITEMPRICEID = BD.ITEMPRICEID          
          
   END          
  END          
 END          
            
 INSERT INTO #BilledGroupItems          
 SELECT @BillDetailID, @Quantity, 0, 0, 0          
 WHERE NOT EXISTS ( SELECT 1 FROM #BilledGroupItems WHERE BILLDETAILID = @BillDetailID)          
            
 -- recalculate GST in case of offer or deals          
 UPDATE BD          
 SET BD.GSTVALUE = ROUND(BD.BILLEDAMOUNT - ((BD.BILLEDAMOUNT / (100 + GST.CGST + GST.SGST + GST.CESS)) * 100), 2)          
 FROM          
  POS_BILLDETAIL BD          
  INNER JOIN #BilledGroupItems BGI ON BGI.BILLDETAILID = BD.BILLDETAILID          
  INNER JOIN POS_GSTDETAIL GST ON GST.GSTID = BD.GSTID       
  
  UPDATE BD          
 SET           
  BD.CGST = ROUND((((BD.BILLEDAMOUNT - BD.GSTVALUE) * GST.CGST) / 100), 2)          
  , BD.SGST = ROUND((((BD.BILLEDAMOUNT - BD.GSTVALUE) * GST.SGST) / 100), 2)          
  --, BD.IGST = ROUND((BD.BILLEDAMOUNT * GST.IGST) / 100, 2)          
  , BD.CESS = ROUND((((BD.BILLEDAMOUNT - BD.GSTVALUE) * GST.CESS) / 100), 2)          
 FROM          
  POS_BILLDETAIL BD          
  INNER JOIN #BilledGroupItems BGI ON BGI.BILLDETAILID = BD.BILLDETAILID          
  INNER JOIN POS_GSTDETAIL GST ON GST.GSTID = BD.GSTID       
           
 SELECT                              
  BD.BILLDETAILID, B.BILLID, IP.ITEMPRICEID,                      
  BD.SNO, I.ITEMNAME, IC.ITEMCODE, ISNULL(IC.HSNCODE,'') AS HSNCODE,IP.MRP                            
  ,IP.SALEPRICE, GST.GSTCODE, BD.QUANTITY,                      
  BD.WEIGHTINKGS, BD.BILLEDAMOUNT,                      
  BD.CGST, BD.SGST, BD.IGST, BD.CESS,                      
  BD.GSTVALUE, BD.GSTID,GST.CGST AS CGSTDESC,                      
  GST.SGST AS SGSTDESC,GST.CESS AS CESSDESC,                      
  ISNULL(I.ISOPENITEM,0) AS ISOPENITEM, DISCOUNT, BD.OFFERID, OFRTYP.OFFERTYPECODE          
 FROM                              
  POS_BILLDETAIL BD               
 INNER JOIN #BilledGroupItems BGI ON BGI.BILLDETAILID = BD.BILLDETAILID          
  INNER JOIN POS_BILL B ON B.BILLID = BD.BILLID                              
  INNER JOIN POS_ITEMPRICE IP ON IP.ITEMPRICEID = BD.ITEMPRICEID                              
  INNER JOIN POS_ITEMCODE IC ON IC.ITEMCODEID = IP.ITEMCODEID                              
  INNER JOIN POS_ITEM I ON I.ITEMID = IC.ITEMID                              
  INNER JOIN POS_GSTDETAIL GST ON GST.GSTID = IP.GSTID               
  LEFT JOIN POS_OFFER OFR ON OFR.OFFERID = BD.OFFERID          
  LEFT JOIN POS_OFFERTYPE OFRTYP ON OFRTYP.OFFERTYPEID = OFR.OFFERTYPEID          
END
GO
