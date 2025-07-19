USE [C:\Windows\NSRetailPOS\NSRETAILPOS.MDF]
GO
:ON ERROR EXIT
GO

/****** Object:  StoredProcedure [dbo].[POS_USP_CU_BILLDETAIL]    Script Date: 18-07-2025 20:11:37 ******/
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
 , @BillOfferPrice DECIMAL(7, 2) = 0
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
          
 DECLARE @OfferID INT, @DealID INT, @OfferAppliesToID INT, @DealAppliesToID INT, @SpecialDiscountPer DECIMAL(4, 2), @DealTypeID INT = 0
 SELECT @OfferID = OFFERID, @OfferAppliesToID = OFFERAPPLIESTOID, @DealID = DEALID, @DealAppliesToID = DEALAPPLIESTOID          
 FROM UDF_GETOFFERS(@ItemPriceID,@Quantity) WHERE @IsBillOfferItem = 0     

 SELECT @DealTypeID = OFFERTYPEID FROM POS_OFFER WHERE OFFERID = @DealID AND ISNULL(@DealID,0) > 0

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
   BD.BILLEDAMOUNT  = CASE WHEN @IsBillOfferItem = 1 THEN @BillOfferPrice ELSE IP.SALEPRICE END * @QuantityOrWeightMultiplier
   , BD.DISCOUNT  = ((IP.MRP - CASE WHEN @IsBillOfferItem = 1 THEN @BillOfferPrice ELSE IP.SALEPRICE END) * @QuantityOrWeightMultiplier)          
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
  ELSE IF ISNULL(@DealID, 0) > 0 AND (@DealTypeID = 4 OR @DealTypeID = 5 OR @DealTypeID = 1007 OR @DealTypeID = 1008 OR @DealTypeID = 1009)
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
	 INNER JOIN POS_OFFERITEMMAP OFRITM ON OFRITM.ITEMCODEID = IP.ITEMCODEID	       
    WHERE          
     BD.BILLID = @BillID          
     AND BD.DELETEDDATE IS NULL          
     AND OFRITM.OFFERID = @DealID       
	 AND OFRITM.DELETEDDATE IS NULL
          
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
 BD.BILLEDAMOUNT = (BGI.MRPQUANTITY * IP.MRP) + (BGI.SALEPRICEQUANTITY * (IP.MRP - dbo.UDF_GET_DISCOUNTVALUE(dbo.UDF_GETOFFER(IP.ITEMPRICEID, BGI.SALEPRICEQUANTITY), IP.ITEMPRICEID)))          
     , BD.DISCOUNT = (BGI.FREEQUANTITY * IP.MRP) + (BGI.SALEPRICEQUANTITY * dbo.UDF_GET_DISCOUNTVALUE(dbo.UDF_GETOFFER(IP.ITEMPRICEID, BGI.SALEPRICEQUANTITY), IP.ITEMPRICEID))          
     , BD.OFFERID = CASE WHEN @BuyQuantity / @DealQuantity > 0 THEN @DealID ELSE @OfferID END          
    FROM          
     POS_BILLDETAIL BD          
     INNER JOIN #BilledGroupItems BGI ON BGI.BILLDETAILID = BD.BILLDETAILID          
     INNER JOIN POS_ITEMPRICE IP ON IP.ITEMPRICEID = BD.ITEMPRICEID          
          
   END          
  END          
 END          

	SELECT OFR.OFFERID, OFR.NUMBEROFITEMS, OFR.FREEITEMPRICEID, ISNULL(OFR.OFFERTHRESHOLDPRICE, 0.00) AS FREEITEMPRICE
	INTO #FreeItemOffers
	FROM
		POS_OFFER OFR
		INNER JOIN POS_OFFERBRANCH OFRB ON OFRB.OFFERID = OFR.OFFERID		
	WHERE 
		GETDATE() BETWEEN OFR.STARTDATE AND ISNULL(OFR.ENDDATE, '2100-01-01')
		AND OFR.ISACTIVE = 1
		AND OFR.DELETEDDATE IS NULL
		AND OFRB.DELETEDDATE IS NULL
		AND OFR.OFFERTYPEID = 1006
		AND 
			(
				OFR.FREEITEMPRICEID = @ItemPriceID
				OR EXISTS
				(
					SELECT 1 
					FROM 
						POS_OFFERITEMMAP OFRMAP 
						INNER JOIN POS_ITEMPRICE IP ON IP.ITEMCODEID = OFRMAP.ITEMCODEID
					WHERE 
						OFRMAP.DELETEDDATE IS NULL
						AND IP.ITEMPRICEID = @ItemPriceID
						AND OFR.OFFERID = OFRMAP.OFFERID
				)				
			)
	
	DECLARE @FreeItemOfferID INT, @FreeItemPriceID INT, @FreeItemBillDetailID INT, @FreeItemNoOfItemsToBuy INT, @FreeItemPrice DECIMAL(18, 2)

	SELECT 
		@FreeItemOfferID = FIO.OFFERID, @FreeItemPriceID = FIO.FREEITEMPRICEID
		, @FreeItemNoOfItemsToBuy = FIO.NUMBEROFITEMS, @FreeItemPrice = FIO.FREEITEMPRICE
	FROM 
		#FreeItemOffers FIO
		INNER JOIN POS_OFFERITEMMAP OFRMAP ON OFRMAP.OFFERID = FIO.OFFERID
		INNER JOIN 
			(
				SELECT IP.ITEMCODEID, SUM(PBDINNER.QUANTITY) QUANTITY
				FROM 
					POS_ITEMPRICE IP 
					INNER JOIN POS_BILLDETAIL PBDINNER ON PBDINNER.ITEMPRICEID = IP.ITEMPRICEID
				WHERE
					PBDINNER.DELETEDDATE IS NULL
					AND PBDINNER.BILLID = @BillID
				GROUP BY IP.ITEMCODEID
			) PBD ON PBD.ITEMCODEID = OFRMAP.ITEMCODEID
	WHERE OFRMAP.DELETEDDATE IS NULL		
	GROUP BY FIO.OFFERID, FIO.FREEITEMPRICEID, FIO.NUMBEROFITEMS, FIO.FREEITEMPRICE
	HAVING SUM(PBD.QUANTITY) >= FIO.NUMBEROFITEMS
	ORDER BY FIO.NUMBEROFITEMS DESC

	SELECT @FreeItemBillDetailID = PBD.BILLDETAILID
	FROM POS_BILLDETAIL PBD 
	WHERE PBD.ITEMPRICEID = @FreeItemPriceID AND PBD.BILLID = @BillID AND PBD.DELETEDDATE IS NULL
	
	IF ISNULL(@FreeItemBillDetailID, 0) <> 0
	BEGIN
		
		SELECT
			FIO.OFFERID, FIO.NUMBEROFITEMS, PBD.BILLDETAILID, PBD.QUANTITY AS BUYQTY, 0 AS MRPQTY
			, 0 AS SALEPRICEQTY, 0 AS FREEQTY, 1 AS ISBUYITEMS
		INTO #BUYOFFERS
		FROM
			#FreeItemOffers FIO
			INNER JOIN POS_OFFERITEMMAP OFRMAP ON OFRMAP.OFFERID = FIO.OFFERID			
			INNER JOIN POS_ITEMPRICE IP ON IP.ITEMCODEID = OFRMAP.ITEMCODEID
			INNER JOIN POS_BILLDETAIL PBD ON PBD.ITEMPRICEID = IP.ITEMPRICEID
		WHERE 
			PBD.BILLID = @BillID
			AND PBD.DELETEDDATE IS NULL
			AND OFRMAP.DELETEDDATE IS NULL

		INSERT INTO #BUYOFFERS
		SELECT FIO.OFFERID, 0, PBD.BILLDETAILID, PBD.QUANTITY AS BUYQTY, 0 AS MRPQTY, 0 AS SALEPRICEQTY, 0 AS FREEQTY, 0 AS ISBUYITEMS
		FROM #FreeItemOffers FIO CROSS APPLY POS_BILLDETAIL PBD
		WHERE PBD.ITEMPRICEID = @FreeItemPriceID AND PBD.BILLID = @BillID AND PBD.DELETEDDATE IS NULL

		--CHECK IF MINIMUM ITEMS BOUGHT
		IF (SELECT COUNT(1) FROM #BUYOFFERS BO WHERE BO.ISBUYITEMS = 1) < @FreeItemNoOfItemsToBuy
		BEGIN
			-- DELETE ITEMS SO THAT NO OFFER IS PROCESSED
			DELETE FROM #BUYOFFERS
		END

		DECLARE @FreeItemQuantity INT

		SELECT @FreeItemQuantity = BO.BUYQTY
		FROM #BUYOFFERS BO
		WHERE BO.ISBUYITEMS = 0

		SELECT @FreeItemQuantity = CASE WHEN @FreeItemQuantity > MIN(BO.BUYQTY) THEN MIN(BO.BUYQTY) ELSE @FreeItemQuantity END
		FROM #BUYOFFERS BO
		WHERE BO.ISBUYITEMS = 1

		UPDATE BO
		SET BO.MRPQTY = @FreeItemQuantity, BO.SALEPRICEQTY = BO.BUYQTY - @FreeItemQuantity
		FROM #BUYOFFERS BO
		WHERE BO.ISBUYITEMS = 1

		UPDATE BO
		SET BO.FREEQTY = @FreeItemQuantity, BO.SALEPRICEQTY = BO.BUYQTY - @FreeItemQuantity
		FROM #BUYOFFERS BO
		WHERE BO.ISBUYITEMS = 0

		UPDATE PBD
		SET 
			PBD.BILLEDAMOUNT = (BO.MRPQTY * IP.MRP) 
				+ (BO.SALEPRICEQTY * (IP.MRP - dbo.UDF_GET_DISCOUNTVALUE(dbo.UDF_GETOFFER(IP.ITEMPRICEID,BO.SALEPRICEQTY), IP.ITEMPRICEID)))
				+ (BO.FREEQTY * @FreeItemPrice)
			, PBD.DISCOUNT = (BO.FREEQTY * (IP.MRP - @FreeItemPrice)) + (BO.SALEPRICEQTY * dbo.UDF_GET_DISCOUNTVALUE(dbo.UDF_GETOFFER(IP.ITEMPRICEID, BO.SALEPRICEQTY), IP.ITEMPRICEID))
			, PBD.OFFERID = BO.OFFERID
		FROM
			#BUYOFFERS BO
			INNER JOIN POS_BILLDETAIL PBD ON PBD.BILLDETAILID = BO.BILLDETAILID
			INNER JOIN POS_ITEMPRICE IP ON IP.ITEMPRICEID = PBD.ITEMPRICEID

		INSERT INTO #BilledGroupItems
		SELECT BO.BILLDETAILID, BO.BUYQTY, BO.MRPQTY, BO.SALEPRICEQTY, BO.FREEQTY
		FROM #BUYOFFERS BO
		WHERE NOT EXISTS ( SELECT 1 FROM #BilledGroupItems WHERE BILLDETAILID = BO.BILLDETAILID)   
				
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

/****** Object:  StoredProcedure [dbo].[POS_USP_R_LOAD]    Script Date: 18-07-2025 20:24:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[POS_USP_R_LOAD]        
@UserID INT, @BranchCounterID INT        
AS        
BEGIN        
	DECLARE @DaySequenceID INT        
        
	SELECT TOP 1 @DaySequenceID = DAYSEQUENCEID FROM POS_DAYSEQUENCE ORDER BY OPENDATE DESC         
        
	IF ISNULL(@DaySequenceID, 0) = 0 OR (SELECT OPENDATE FROM POS_DAYSEQUENCE WHERE DAYSEQUENCEID = @DaySequenceID) <> CAST(GETDATE() AS DATE)        
	BEGIN        
		INSERT INTO POS_DAYSEQUENCE(OPENDATE, BRANCHCOUNTERID, CREATEDATE)        
		SELECT CAST(GETDATE() AS DATE), @BranchCounterID, GETDATE()        
        
		SET @DaySequenceID = SCOPE_IDENTITY()        
	END        
	ELSE IF (SELECT ISCLOSED FROM POS_DAYSEQUENCE WHERE DAYSEQUENCEID = @DaySequenceID) = 1        
	BEGIN         
		SELECT 'Billing closed'        
		RETURN        
	END        
        
	DECLARE @LastBillNum VARCHAR(30)        
	SELECT @LastBillNum = LASTUSEDBILLNUM FROM POS_DAYSEQUENCE WHERE DAYSEQUENCEID = @DaySequenceID        
        
	IF ISNULL(@LastBillNum, '') = ''        
	BEGIN        
		
		SELECT @LastBillNum = BC.COUNTERNAME  + '/' + FORMAT(GETDATE(), 'yyMMdd') + '/000'        
		FROM POS_BRANCHCOUNTER BC WHERE BC.COUNTERID = @BranchCounterID   
		        
		UPDATE POS_DAYSEQUENCE        
		SET LASTUSEDBILLNUM = @LastBillNum,
		UPDATEDATE = GETDATE()
		WHERE DAYSEQUENCEID = @DaySequenceID        
	END        
         
	SELECT @DaySequenceID AS DAYSEQUENCEID 
	EXEC POS_USP_R_GETNEXTBILL @UserID, @DaySequenceID        
      
END
GO

/****** Object:  StoredProcedure [dbo].[POS_USP_R_GETNEXTBILL]    Script Date: 18-07-2025 20:25:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[POS_USP_R_GETNEXTBILL]    
 @UserID INT    
 , @DaySequenceID INT    
AS    
BEGIN    
	DECLARE @BillID INT, @BillStatus INT   
	
	SELECT @BillID = OPENBILLID, @BillStatus = PB.BILLSTATUS        
	FROM 
		POS_DAYSEQUENCE DS
		INNER JOIN POS_BILL PB ON PB.BILLID = DS.OPENBILLID
	WHERE DAYSEQUENCEID = @DaySequenceID AND PB.DELETEDDATE IS NULL  

	IF ISNULL(@BillID, 0) = 0 OR @BillStatus <> 0 
	BEGIN
		DECLARE @BillNumber VARCHAR(30)    
		SELECT @BillNumber = DS.LASTUSEDBILLNUM FROM POS_DAYSEQUENCE DS WHERE DS.DAYSEQUENCEID = @DaySequenceID     
    
		SELECT @BillNumber = LEFT(@BillNumber, LEN(@BillNumber) - 3) + RIGHT('00' + CAST((CAST(RIGHT(@BillNumber, 3) AS INT) + 1) as VARCHAR(5)), 3)  
    
		INSERT INTO POS_BILL(BILLNUMBER, BILLSTATUS, CREATEDBY, CREATEDDATE)    
		SELECT @BillNumber, 0, @UserID, GETDATE()    
      
		SET @BillID = SCOPE_IDENTITY()  
		
		UPDATE POS_DAYSEQUENCE    
		SET LASTUSEDBILLNUM = @BillNumber
			, OPENBILLID = @BillID    
			,UPDATEDATE = GETDATE()
		WHERE DAYSEQUENCEID = @DaySequenceID  
	END

	EXEC POS_USP_R_BILL @BillID, @DaySequenceID    
END    
GO

/****** Object:  StoredProcedure [dbo].[POS_USP_R_BILLBYNUMBER]    Script Date: 18-07-2025 20:27:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[POS_USP_R_BILLBYNUMBER]                  
@BILLNUMBER VARCHAR(50)    
, @IsAdminRole BIT = 0
AS                  
BEGIN                  
                  
DECLARE @BILLID INT = 0                  
SELECT @BILLID = BILLID FROM POS_BILL WHERE BILLNUMBER = @BILLNUMBER AND BILLSTATUS = 1 AND (CREATEDDATE > GETDATE() - 7 OR @IsAdminRole = 1)
IF @BILLID  =0                  
BEGIN                  
SELECT 'BILL DOES NOT EXISTS!'                  
END                  
ELSE                  
BEGIN                  
  
 SELECT BILLID, CREATEDDATE, CUSTOMERNAME, CUSTOMERNUMBER  
 FROM POS_BILL B  
 WHERE BILLID = @BILLID 
                  
 SELECT                                    
  BD.BILLDETAILID, B.BILLID, IP.ITEMPRICEID,                            
  BD.SNO, I.ITEMNAME, IC.ITEMCODE, ISNULL(IC.HSNCODE,'') AS HSNCODE,IP.MRP                                  
  ,IP.SALEPRICE, GST.GSTCODE, BD.QUANTITY - ISNULL(CR.REFUNDQUANTITY,0) AS QUANTITY,                  
  BD.WEIGHTINKGS - ISNULL(CR.REFUNDWEIGHTINKGS,0) AS  WEIGHTINKGS,                
  BD.BILLEDAMOUNT - ISNULL(CR.REFUNDAMOUNT,0) AS BILLEDAMOUNT,                
  BD.CGST, BD.SGST, BD.IGST, BD.CESS,                            
  BD.GSTVALUE, BD.GSTID,GST.CGST AS CGSTDESC,                            
  GST.SGST AS SGSTDESC,GST.CESS AS CESSDESC,                            
  ISNULL(I.ISOPENITEM,0) AS ISOPENITEM, DISCOUNT ,                  
  0 AS REFUNDQUANTITY,0.00 AS REFUNDWEIGHTINKGS,0.00 AS REFUNDAMOUNT,              
  BD.CREATEDDATE, I.SKUCODE              
 FROM                                    
  POS_BILLDETAIL BD                                    
  INNER JOIN POS_BILL B ON B.BILLID = BD.BILLID                                    
  INNER JOIN POS_ITEMPRICE IP ON IP.ITEMPRICEID = BD.ITEMPRICEID                                    
  INNER JOIN POS_ITEMCODE IC ON IC.ITEMCODEID = IP.ITEMCODEID                                    
  INNER JOIN POS_ITEM I ON I.ITEMID = IC.ITEMID                                    
  INNER JOIN POS_GSTDETAIL GST ON GST.GSTID = IP.GSTID                                    
  LEFT JOIN (SELECT BILLDETAILID,SUM(REFUNDQUANTITY) AS REFUNDQUANTITY,                  
  SUM(REFUNDWEIGHTINKGS) AS REFUNDWEIGHTINKGS, SUM(REFUNDAMOUNT) AS REFUNDAMOUNT              
  FROM POS_CREFUND GROUP BY BILLDETAILID) AS CR ON BD.BILLDETAILID = CR.BILLDETAILID                  
 WHERE B.BILLID = @BillID AND BD.DELETEDDATE IS NULL          
 ORDER BY BD.SNO                                    
                  
END                  
                  
END
GO

/****** Object:  UserDefinedFunction [dbo].[UDF_GETOFFERS]    Script Date: 19-07-2025 12:10:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[UDF_GETOFFERS] (@ItemPriceID INT, @Quantity INT) 
RETURNS @Offer TABLE (ITEMPRICEID INT, OFFERID INT, OFFERAPPLIESTOID INT, DEALID INT, DEALAPPLIESTOID INT)
AS
BEGIN

	DECLARE @ItemCodeID INT, @CategoryID INT, @MaxDate DATE, @OfferID INT, @DealID INT,@MRP DECIMAL(18,2)
	SELECT @MaxDate = '2100-01-01'	 

	SELECT @ItemCodeID = IC.ITEMCODEID, @CategoryID = I.CATEGORYID,@MRP = IP.MRP
	FROM 
		POS_ITEMPRICE IP
		INNER JOIN POS_ITEMCODE IC ON IC.ITEMCODEID = IP.ITEMCODEID
		INNER JOIN POS_ITEM I ON I.ITEMID = IC.ITEMID
	WHERE IP.ITEMPRICEID = @ItemPriceID

	DECLARE @Offers TABLE (OFFERID INT, APPLIESTOID INT, OFFERTYPEID INT)

	--INSERT INTO @Offers
	--SELECT OFR.OFFERID, OFR.APPLIESTOID, OFR.OFFERTYPEID
	--FROM
	--	POS_OFFER OFR
	--	INNER JOIN POS_OFFERBRANCH OFRB ON OFRB.OFFERID = OFR.OFFERID
	--WHERE 
	--	GETDATE() BETWEEN OFR.STARTDATE AND ISNULL(OFR.ENDDATE, @MaxDate)
	--	AND OFR.ISACTIVE = 1
	--	AND OFR.DELETEDDATE IS NULL
	--	AND OFRB.DELETEDDATE IS NULL
	--	AND 
	--		(
	--			EXISTS (SELECT 1 FROM POS_OFFERITEMMAP OFRIM WHERE OFRIM.OFFERID = OFR.OFFERID AND OFRIM.ITEMCODEID = @ItemCodeID AND OFRIM.DELETEDDATE IS NULL)
	--			OR OFR.CATEGORYID = @CategoryID
	--			OR EXISTS 
	--				(
	--					SELECT 1 
	--					FROM 
	--						POS_ITEMGROUP IG 
	--						INNER JOIN POS_ITEMGROUPDETAIL IGD ON IGD.ITEMGROUPID = IG.ITEMGROUPID
	--					WHERE 
	--						IGD.ITEMCODEID = @ItemCodeID
	--						AND OFR.ITEMGROUPID = IG.ITEMGROUPID
	--						AND IG.DELETEDDATE IS NULL
	--						AND IGD.DELETEDDATE IS NULL
	--				)
	--		)
	--ORDER BY APPLIESTOID ASC, OFR.OFFERID DESC


	INSERT INTO @Offers
	SELECT * FROM 
		(
			SELECT OFR.OFFERID, OFR.APPLIESTOID, OFR.OFFERTYPEID
			FROM
				POS_OFFER OFR
				INNER JOIN POS_OFFERBRANCH OFRB ON OFRB.OFFERID = OFR.OFFERID
			WHERE 
				GETDATE() BETWEEN OFR.STARTDATE AND ISNULL(OFR.ENDDATE, @MaxDate)
				AND OFR.ISACTIVE = 1
				AND OFR.DELETEDDATE IS NULL
				AND OFRB.DELETEDDATE IS NULL
				AND OFR.APPLIESTOID = 1
				AND EXISTS (SELECT 1 FROM POS_OFFERITEMMAP OFRIM WHERE OFRIM.OFFERID = OFR.OFFERID AND OFRIM.ITEMCODEID = @ItemCodeID AND OFRIM.DELETEDDATE IS NULL)
			UNION ALL
			SELECT OFR.OFFERID, OFR.APPLIESTOID, OFR.OFFERTYPEID
			FROM
				POS_OFFER OFR
				INNER JOIN POS_OFFERBRANCH OFRB ON OFRB.OFFERID = OFR.OFFERID
			WHERE 
				GETDATE() BETWEEN OFR.STARTDATE AND ISNULL(OFR.ENDDATE, @MaxDate)
				AND OFR.ISACTIVE = 1
				AND OFR.DELETEDDATE IS NULL
				AND OFRB.DELETEDDATE IS NULL
				AND OFR.APPLIESTOID = 3
				AND OFR.CATEGORYID = @CategoryID
				AND (OFR.OFFERTHRESHOLDPRICE IS NULL OR OFR.NUMBEROFITEMS IS NULL OR OFR.OFFERTHRESHOLDPRICE < @MRP OR OFR.NUMBEROFITEMS <= @Quantity)
		) OFRMAIN
	ORDER BY APPLIESTOID ASC, OFFERID DESC

	
	SELECT TOP 1 @OfferID = OFFERID
	FROM @Offers
	WHERE OFFERTYPEID IN (1, 2, 3)
	ORDER BY APPLIESTOID ASC, OFFERID DESC

	SELECT @DealID = OFFERID
	FROM @Offers
	WHERE OFFERTYPEID IN (4, 5, 1007, 1008, 1009)
	ORDER BY APPLIESTOID ASC, OFFERID DESC

	INSERT INTO @Offer(ITEMPRICEID, OFFERID, DEALID)
	SELECT @ItemPriceID, @OfferID, @DealID
	
	DECLARE @DealAppliedToCount INT

	SELECT @DealAppliedToCount = COUNT(1)
	FROM POS_OFFERITEMMAP WHERE OFFERID = @DealID

	UPDATE OFRTMP
	SET 
		OFRTMP.OFFERAPPLIESTOID = OFRID.APPLIESTOID
		, OFRTMP.DEALAPPLIESTOID = @DealAppliedToCount --DLID.APPLIESTOID
	FROM 
		@Offer OFRTMP
		LEFT JOIN POS_OFFER OFRID ON OFRID.OFFERID = OFRTMP.OFFERID 
		--LEFT JOIN POS_OFFER DLID ON DLID.OFFERID = OFRTMP.DEALID
	
	RETURN
END
GO


UPDATE TBLCONFIG SET CONFIGVALUE = '1.7.3' WHERE CONFIGID = 1
GO
