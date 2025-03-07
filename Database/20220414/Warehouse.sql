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


/****** Object:  StoredProcedure [dbo].[USP_CU_POS_BILL]    Script Date: 14-04-2022 08:05:45 ******/
DROP PROCEDURE [dbo].[USP_CU_POS_BILL]
GO

/****** Object:  UserDefinedTableType [dbo].[POS_BILLTYPE]    Script Date: 14-04-2022 08:06:05 ******/
DROP TYPE [dbo].[POS_BILLTYPE]
GO

/****** Object:  UserDefinedTableType [dbo].[POS_BILLTYPE]    Script Date: 14-04-2022 08:06:05 ******/
CREATE TYPE [dbo].[POS_BILLTYPE] AS TABLE(
	[BILLID] [int] NOT NULL,
	[BRANCHCOUNTERID] [int] NOT NULL,
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
	SPLDISCPER DECIMAL(5, 2),
	ISDOORDELIVERY BIT,
	TENDEREDCASH DECIMAL(11, 2),
	TENDEREDCHANGE DECIMAL(11, 2)
)
GO


CREATE PROC [dbo].[USP_CU_POS_BILL]  
(  
 @Bills POS_BILLTYPE READONLY  
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
  , B.SPLDISCPER = UB.SPLDISCPER
  , B.ISDOORDELIVERY = UB.ISDOORDELIVERY
  , B.TENDEREDCASH = UB.TENDEREDCASH
  , B.TENDEREDCHANGE = UB.TENDEREDCHANGE
 FROM   
  POS_BILL B  
  INNER JOIN @Bills UB ON UB.BILLID = B.BILLID  
 WHERE  
  B.BRANCHCOUNTERID = UB.BRANCHCOUNTERID  
  
 INSERT INTO POS_BILL(BILLID, BRANCHCOUNTERID, BILLNUMBER, CREATEDBY, CREATEDDATE, 
 UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID
 , SPLDISCPER, ISDOORDELIVERY, TENDEREDCASH, TENDEREDCHANGE)  
 SELECT BILLID, BRANCHCOUNTERID, BILLNUMBER, CREATEDBY, CREATEDDATE, 
 UPDATEDBY, UPDATEDDATE, DELETEDBY, DELETEDDATE, BILLSTATUS, CUSTOMERNUMBER, CUSTOMERNAME,DAYCLOSUREID
 , SPLDISCPER, ISDOORDELIVERY, TENDEREDCASH, TENDEREDCHANGE
 FROM @Bills UB  
 WHERE NOT EXISTS  
  (  
   SELECT 1 FROM POS_BILL BINNER WHERE BINNER.BILLID = UB.BILLID AND BINNER.BRANCHCOUNTERID = UB.BRANCHCOUNTERID  
	
  )  
END
GO
ALTER TABLE POS_BREFUNDDETAIL
ADD REASONID INT
GO
ALTER TABLE POS_BREFUNDDETAIL
ADD DELETEDDATE DATETIME
GO
DROP PROC [dbo].[USP_SYNC_CU_BREFUNDDETAL]          
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
	[COUNTERID] [int] NOT NULL,
	REASONID INT NULL,
	DELETEDDATE DATETIME NULL
)
GO
CREATE PROC [dbo].[USP_SYNC_CU_BREFUNDDETAL]          
(          
 @BRefundDetails POS_BREFUNDDETAILTYPE READONLY          
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
BRD.COUNTERID = UBRD.COUNTERID,
BRD.REASONID = UBRD.REASONID,
BRD.DELETEDDATE = UBRD.DELETEDDATE
 FROM           
  POS_BREFUNDDETAIL BRD        
  INNER JOIN @BRefundDetails UBRD ON UBRD.BREFUNDDETAILID = BRD.BREFUNDDETAILID          
 WHERE          
  BRD.COUNTERID = UBRD.COUNTERID AND BRD.BREFUNDID = UBRD.BREFUNDID   
          
 INSERT INTO POS_BREFUNDDETAIL(BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,        
WEIGHTINKGS,CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,COUNTERID,
REASONID,DELETEDDATE)          
 SELECT BREFUNDDETAILID,BREFUNDID,ITEMPRICEID,QUANTITY,        
WEIGHTINKGS,CREATEDDATE,UPDATEDDATE,SNO,TRAYNUMBER,COUNTERID ,
REASONID,DELETEDDATE
 FROM @BRefundDetails UBRD        
 WHERE NOT EXISTS          
  (          
   SELECT 1 FROM POS_BREFUNDDETAIL BRDINNER WHERE BRDINNER.BREFUNDDETAILID = UBRD.BREFUNDDETAILID        
   AND BRDINNER.COUNTERID = UBRD.COUNTERID  AND BRDINNER.BREFUNDID = UBRD.BREFUNDID  
  )          
END 
GO
ALTER PROCEDURE USP_R_BREFUNDDETAIL        
@BREFUNDID INT,        
@COUNTERID INT        
AS        
BEGIN        
        
SELECT BRD.BREFUNDDETAILID,BRD.BREFUNDID,IP.ITEMPRICEID ,  
BRD.SNO,IC.ITEMCODE,I.ITEMNAME,        
IP.MRP,IP.SALEPRICE, BRD.QUANTITY,BRD.WEIGHTINKGS,      
ISNULL(ACCEPTEDQUANTITY,BRD.QUANTITY) AS ACCEPTEDQUANTITY,      
ISNULL(ACCEPTEDWEIGHTKGS,BRD.WEIGHTINKGS) AS ACCEPTEDWEIGHTKGS ,  
BRD.COUNTERID,BRD.REASONID,BRD.DELETEDDATE,RFR.REASONNAME
FROM POS_BREFUNDDETAIL BRD        
INNER JOIN ITEMPRICE IP ON BRD.ITEMPRICEID = IP.ITEMPRICEID        
INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID        
INNER JOIN ITEM I ON IC.ITEMID = I.ITEMID        
INNER JOIN REASONFORREFUND RFR ON BRD.REASONID = RFR.REASONID
WHERE  BRD.BREFUNDID = @BREFUNDID 
	AND BRD.COUNTERID = @COUNTERID        
	AND BRD.DELETEDDATE IS NULL
        
END
GO
INSERT INTO REASONFORREFUND(REASONNAME,CREATEDBY,CREATEDDATE)
VALUES('EXPIRED',4,GETDATE())
GO
INSERT INTO REASONFORREFUND(REASONNAME,CREATEDBY,CREATEDDATE)
VALUES('DAMAGED',4,GETDATE())
GO
INSERT INTO REASONFORREFUND(REASONNAME,CREATEDBY,CREATEDDATE)
VALUES('WRONG DISPATCH',4,GETDATE())
GO
INSERT INTO REASONFORREFUND(REASONNAME,CREATEDBY,CREATEDDATE)
VALUES('NOT FOR SALE',4,GETDATE())
GO