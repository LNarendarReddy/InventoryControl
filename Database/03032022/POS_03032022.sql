/****** Object:  StoredProcedure [dbo].[USP_CU_TBLCATEGORY]    Script Date: 03-Mar-22 11:24:47 AM ******/
DROP PROCEDURE IF EXISTS [dbo].[USP_CU_TBLCATEGORY]
GO

/****** Object:  StoredProcedure [dbo].[USP_CU_TBLCATEGORY]    Script Date: 03-Mar-22 11:24:47 AM ******/
DROP PROCEDURE IF EXISTS [dbo].[POS_USP_R_NONEAN]
GO

/****** Object:  StoredProcedure [dbo].[USP_CU_TBLCATEGORY]    Script Date: 03-Mar-22 11:24:47 AM ******/
DROP PROCEDURE IF EXISTS [dbo].[POS_USP_R_CATEGORY]
GO

/****** Object:  Table [dbo].[TBLCATEGORY]    Script Date: 03-Mar-22 11:24:47 AM ******/
DROP TABLE IF EXISTS [dbo].[TBLCATEGORY]
GO
/****** Object:  UserDefinedTableType [dbo].[TBLCATEGORYTYPE]    Script Date: 03-Mar-22 11:24:47 AM ******/
DROP TYPE IF EXISTS [dbo].[TBLCATEGORYTYPE]
GO
/****** Object:  UserDefinedTableType [dbo].[TBLCATEGORYTYPE]    Script Date: 03-Mar-22 11:24:47 AM ******/
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
/****** Object:  Table [dbo].[TBLCATEGORY]    Script Date: 03-Mar-22 11:24:47 AM ******/
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
/****** Object:  StoredProcedure [dbo].[USP_CU_TBLCATEGORY]    Script Date: 03-Mar-22 11:24:47 AM ******/
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

CREATE PROCEDURE POS_USP_R_NONEAN  
AS  
BEGIN  
  
SELECT   
I.ITEMID,  
IC.ITEMCODEID,  
I.SKUCODE,  
IC.ITEMCODE,  
I.ITEMNAME,  
I.CATEGORYID  
FROM POS_ITEMCODE IC  
INNER JOIN POS_ITEM I ON IC.ITEMID = I.ITEMID  
WHERE ISNULL(IC.ISEAN,0) = 0  
END
GO

CREATE PROCEDURE POS_USP_R_CATEGORY  
AS  
BEGIN  
  
SELECT  
CATEGORYID  
,CATEGORYNAME  
,ISNULL(ALLOWOPENITEMS,0) as ALLOWOPENITEMS  
FROM TBLCATEGORY  
  
END
GO


alter PROCEDURE [dbo].[POS_USP_CU_BILLDETAIL]              
 @BillID int              
 , @ItemPriceID INT              
 , @Quantity INT              
 , @WeightInKgs DECIMAL(7, 2)              
 , @UserID INT            
 , @BillDetailID INT        
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
  AND @BillDetailID <= 0       
        
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
        
 DECLARE @OfferID INT, @DealID INT, @OfferAppliesToID INT, @DealAppliesToID INT        
 SELECT @OfferID = OFFERID, @OfferAppliesToID = OFFERAPPLIESTOID, @DealID = DEALID, @DealAppliesToID = DEALAPPLIESTOID        
 FROM UDF_GETOFFERS(@ItemPriceID)        
         
 CREATE TABLE #BilledGroupItems(BILLDETAILID INT, TOTALQUANTITY INT, MRPQUANTITY INT, SALEPRICEQUANTITY INT, FREEQUANTITY INT)        
        
 -- if there is no offer and no deal, update billed amount directly        
 IF ISNULL(@DealID, 0) = 0 AND ISNULL(@OfferID, 0) = 0        
 BEGIN        
  UPDATE BD        
  SET        
   BD.BILLEDAMOUNT  = IP.SALEPRICE * @QuantityOrWeightMultiplier      
   , BD.DISCOUNT  = ((IP.MRP - IP.SALEPRICE) * BD.QUANTITY)        
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
    INNER JOIN POS_GSTDETAIL GST ON GST.GSTID = IP.GSTID        
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
 SET         
  BD.CGST = ROUND((BD.BILLEDAMOUNT * GST.CGST) / 100, 2)        
  , BD.SGST = ROUND((BD.BILLEDAMOUNT * GST.SGST) / 100, 2)        
  , BD.IGST = ROUND((BD.BILLEDAMOUNT * GST.IGST) / 100, 2)        
  , BD.CESS = ROUND((BD.BILLEDAMOUNT * GST.CESS) / 100, 2)        
  , BD.GSTVALUE = ROUND((BD.BILLEDAMOUNT * (GST.CGST + GST.SGST + GST.IGST + GST.CESS)) / 100, 2)        
 FROM        
  POS_BILLDETAIL BD        
  INNER JOIN #BilledGroupItems BGI ON BGI.BILLDETAILID = BD.BILLDETAILID        
  INNER JOIN POS_GSTDETAIL GST ON GST.GSTID = BD.GSTID        
         
 SELECT                            
  BD.BILLDETAILID, B.BILLID, IP.ITEMPRICEID,                    
  BD.SNO, I.ITEMNAME, IC.ITEMCODE, ISNULL(IC.HSNCODE,'') AS HSNCODE,IP.MRP    
  , CAST(ROUND(BD.BILLEDAMOUNT / (CASE WHEN I.ISOPENITEM = 0 THEN BD.QUANTITY ELSE BD.WEIGHTINKGS END), 2) as decimal(10,2)) END AS SALEPRICE 
  --,CASE WHEN I.ISOPENITEM = 0 THEN CAST(ROUND(BD.BILLEDAMOUNT / BD.QUANTITY,2) as decimal(10,2)) ELSE    
  -- cast(ROUND(BD.BILLEDAMOUNT / BD.WEIGHTINKGS,2) as decimal(10,2)) END AS SALEPRICE    
  , GST.GSTCODE, BD.QUANTITY,                    
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
alter PROCEDURE [dbo].[POS_USP_R_BILL]                               
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
  B.BILLID, B.BILLNUMBER, B.BILLSTATUS,                           
  @LastBilledAmount AS LASTBILLEDAMOUNT, @LastBilledQuantity AS LASTBILLEDQUANTITY  ,                          
  @LastBillID AS LASTBILLID, B.CREATEDDATE, B.ROUNDING                
  FROM                               
  POS_BILL B                              
  LEFT JOIN POS_BILL LASTBILL ON LASTBILL.BILLID = @LastBillID                               
 WHERE B.BILLID = @BillID                              
                              
 SELECT                              
  BD.BILLDETAILID, B.BILLID, IP.ITEMPRICEID,                      
  BD.SNO, I.ITEMNAME, IC.ITEMCODE, ISNULL(IC.HSNCODE,'') AS HSNCODE,IP.MRP        
  , CAST(ROUND(BD.BILLEDAMOUNT / (CASE WHEN ISNULL(I.ISOPENITEM,0) = 0 THEN BD.QUANTITY ELSE BD.WEIGHTINKGS END), 2) AS decimal(10,2)) END AS SALEPRICE
  --,CASE WHEN ISNULL(I.ISOPENITEM,0) = 0 THEN   
  --CAST(ROUND(BD.BILLEDAMOUNT/BD.QUANTITY,2) AS DECIMAL(10,2)) ELSE      
  --CAST(ROUND(BD.BILLEDAMOUNT/BD.WEIGHTINKGS,2) AS decimal(10,2)) END AS SALEPRICE      
  , GST.GSTCODE, BD.QUANTITY,                      
  BD.WEIGHTINKGS, BD.BILLEDAMOUNT,                      
  BD.CGST, BD.SGST, BD.IGST, BD.CESS,                      
  BD.GSTVALUE, BD.GSTID,GST.CGST AS CGSTDESC,                      
  GST.SGST AS SGSTDESC,GST.CESS AS CESSDESC,                      
  ISNULL(I.ISOPENITEM,0) AS ISOPENITEM, DISCOUNT, BD.OFFERID, OFRTYP.OFFERTYPECODE          
 FROM                              
  POS_BILLDETAIL BD                              
  INNER JOIN POS_BILL B ON B.BILLID = BD.BILLID                              
  INNER JOIN POS_ITEMPRICE IP ON IP.ITEMPRICEID = BD.ITEMPRICEID                              
  INNER JOIN POS_ITEMCODE IC ON IC.ITEMCODEID = IP.ITEMCODEID                              
  INNER JOIN POS_ITEM I ON I.ITEMID = IC.ITEMID                              
  INNER JOIN POS_GSTDETAIL GST ON GST.GSTID = IP.GSTID               
  LEFT JOIN POS_OFFER OFR ON OFR.OFFERID = BD.OFFERID          
  LEFT JOIN POS_OFFERTYPE OFRTYP ON OFRTYP.OFFERTYPEID = OFR.OFFERTYPEID          
 WHERE B.BILLID = @BillID AND BD.DELETEDDATE IS NULL            
 ORDER BY BD.SNO                              
END
GO
alter PROCEDURE [dbo].[POS_USP_R_BILLBYNUMBER]              
@BILLNUMBER VARCHAR(50)              
AS              
BEGIN              
              
DECLARE @BILLID INT = 0              
SELECT @BILLID = BILLID FROM POS_BILL WHERE BILLNUMBER = @BILLNUMBER              
IF @BILLID  =0              
BEGIN              
SELECT 'BILL DOES NOT EXISTS!'              
END              
ELSE              
BEGIN              
              
 SELECT                                
  BD.BILLDETAILID, B.BILLID, IP.ITEMPRICEID,                        
  BD.SNO, I.ITEMNAME, IC.ITEMCODE, ISNULL(IC.HSNCODE,'') AS HSNCODE,IP.MRP                              
  ,CASE WHEN ISNULL(I.ISOPENITEM,0) = 0     
  THEN CAST(ROUND((BD.BILLEDAMOUNT - ISNULL(CR.REFUNDAMOUNT,0))/(BD.QUANTITY - ISNULL(CR.REFUNDQUANTITY,0)),2) as decimal(10,2))
  ELSE cast(ROUND((BD.BILLEDAMOUNT - ISNULL(CR.REFUNDAMOUNT,0)) / BD.WEIGHTINKGS,2) as decimal(10,2)) END AS SALEPRICE    
  , GST.GSTCODE, BD.QUANTITY - ISNULL(CR.REFUNDQUANTITY,0) AS QUANTITY,              
  BD.WEIGHTINKGS - ISNULL(CR.REFUNDWEIGHTINKGS,0) AS  WEIGHTINKGS,            
  BD.BILLEDAMOUNT - ISNULL(CR.REFUNDAMOUNT,0) AS BILLEDAMOUNT,            
  BD.CGST, BD.SGST, BD.IGST, BD.CESS,                        
  BD.GSTVALUE, BD.GSTID,GST.CGST AS CGSTDESC,                        
  GST.SGST AS SGSTDESC,GST.CESS AS CESSDESC,                        
  ISNULL(I.ISOPENITEM,0) AS ISOPENITEM, DISCOUNT ,              
  0 AS REFUNDQUANTITY,0.00 AS REFUNDWEIGHTINKGS,0.00 AS REFUNDAMOUNT,          
  BD.CREATEDDATE          
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


----- sainath



/****** Object:  UserDefinedFunction [dbo].[POS_UDF_GET_LAST_MODIFIED_DATE]    Script Date: 05-03-2022 20:32:08 ******/
DROP FUNCTION IF EXISTS [dbo].[UDF_GET_LAST_MODIFIED_DATE]
GO

/****** Object:  UserDefinedFunction [dbo].[POS_UDF_GET_LAST_MODIFIED_DATE]    Script Date: 05-03-2022 20:32:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[UDF_GET_LAST_MODIFIED_DATE] 
(
	@CreatedDate DATETIME, @UpdatedDate DATETIME, @DeletedDate DATETIME
)
RETURNS DATETIME
AS
BEGIN
	DECLARE @LastModifiedDate DATETIME

	SELECT @LastModifiedDate = MAX(datevalue) from (VALUES (@CreatedDate), (@UpdatedDate), (@DeletedDate)) AS datevaluetable (datevalue)

	RETURN @LastModifiedDate

END
GO

DROP PROCEDURE IF EXISTS [dbo].[POS_USP_D_OLD_DATA]
GO

CREATE PROC POS_USP_D_OLD_DATA
AS
BEGIN

	DECLARE @CutOffDate DATETIME = GETDATE() - 30
	
	DELETE FROM POS_DAYSEQUENCE WHERE dbo.UDF_GET_LAST_MODIFIED_DATE(CREATEDATE, UPDATEDATE, NULL) < @CutOffDate
	DELETE FROM POS_DAYCLOSUREDETAIL WHERE dbo.UDF_GET_LAST_MODIFIED_DATE(CREATEDDATE, UPDATEDDATE, NULL) < @CutOffDate
	DELETE FROM POS_DAYCLOSURE WHERE dbo.UDF_GET_LAST_MODIFIED_DATE(CREATEDDATE, UPDATEDDATE, NULL) < @CutOffDate
	DELETE FROM POS_STOCKDISPATCHDETAIL WHERE dbo.UDF_GET_LAST_MODIFIED_DATE(CREATEDDATE, UPDATEDATE, DELETEDDATE) < @CutOffDate
	DELETE FROM POS_STOCKDISPATCH WHERE dbo.UDF_GET_LAST_MODIFIED_DATE(CREATEDDATE, UPDATEDATE, DELETEDDATE) < @CutOffDate
	DELETE FROM POS_BREFUNDDETAIL WHERE dbo.UDF_GET_LAST_MODIFIED_DATE(CREATEDDATE, UPDATEDDATE, NULL) < @CutOffDate
	DELETE FROM POS_BREFUND WHERE dbo.UDF_GET_LAST_MODIFIED_DATE(CREATEDATE, UPDATEDDATE, DELETEDDATE) < @CutOffDate
	DELETE FROM POS_CREFUND WHERE dbo.UDF_GET_LAST_MODIFIED_DATE(CREATEDDATE, UPDATEDDATE, DELETEDDATE) < @CutOffDate
	DELETE FROM POS_BILLMOPDETAIL WHERE dbo.UDF_GET_LAST_MODIFIED_DATE(CREATEDDATE, UPDATEDDATE, NULL) < @CutOffDate
	DELETE FROM POS_BILLDETAIL WHERE dbo.UDF_GET_LAST_MODIFIED_DATE(CREATEDDATE, UPDATEDDATE, DELETEDDATE) < @CutOffDate
	DELETE FROM POS_BILL WHERE dbo.UDF_GET_LAST_MODIFIED_DATE(CREATEDDATE, UPDATEDDATE, DELETEDDATE) < @CutOffDate
END



