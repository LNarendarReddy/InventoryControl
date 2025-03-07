USE [C:\Windows\NSRetailPOS\NSRETAILPOS.MDF]
GO
/****** Object:  StoredProcedure [dbo].[POS_USP_R_LOAD]    Script Date: 24-06-2022 10:38:31 ******/
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
		SELECT @LastBillNum = BC.COUNTERNAME  + '/' + FORMAT(GETDATE(), 'yyyyMMdd') + '/0000'        
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

/****** Object:  StoredProcedure [dbo].[POS_USP_FINISH_BILL]    Script Date: 24-06-2022 10:34:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[POS_USP_FINISH_BILL]      
 @BillID INT      
 , @UserID INT      
 , @DaySequenceID INT      
 , @CustomerNumber VARCHAR(10) = NULL      
 , @CustomerName VARCHAR(100) = NULL      
 , @Rounding DECIMAL(3, 2)
 , @IsDoorDelivery BIT
 , @TenderedCash DECIMAL(11, 2) = 0.00
 , @TenderedChange DECIMAL(11, 2) = 0.00
 , @MopValues  POS_BILLMOPVALUES READONLY      
AS      
BEGIN      
      
	INSERT INTO POS_BILLMOPDETAIL(BILLID, MOPID, MOPVALUE,CREATEDDATE)      
	SELECT @BillID, MOPID, MOPVALUE,GETDATE()  
	FROM @MopValues      
	WHERE MOPVALUE <> 0      
      
	UPDATE POS_BILL      
	SET       
		BILLSTATUS = 1      
		, CUSTOMERNAME = @CustomerName      
		, CUSTOMERNUMBER = @CustomerNumber   
		, ISDOORDELIVERY = @IsDoorDelivery
		, TENDEREDCASH = @TenderedCash
		, TENDEREDCHANGE = @TenderedChange
		, UPDATEDBY = @UserID      
		, UPDATEDDATE = GETDATE()      
		, ROUNDING = @Rounding  
	WHERE BILLID = @BillID       
       
	UPDATE POS_DAYSEQUENCE      
	SET LASTBILLID = @BillID, UPDATEDATE = GETDATE()
	WHERE DAYSEQUENCEID = @DaySequenceID      
      
	EXEC POS_USP_R_GETNEXTBILL @UserID, @DaySequenceID    
 
END
GO

/****** Object:  StoredProcedure [dbo].[POS_USP_R_GETNEXTBILL]    Script Date: 24-06-2022 10:33:45 ******/
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
	WHERE DAYSEQUENCEID = @DaySequenceID       

	IF ISNULL(@BillID, 0) = 0 OR @BillStatus <> 0
	BEGIN
		DECLARE @BillNumber VARCHAR(30)    
		SELECT @BillNumber = DS.LASTUSEDBILLNUM FROM POS_DAYSEQUENCE DS WHERE DS.DAYSEQUENCEID = @DaySequenceID     
    
		SELECT @BillNumber = LEFT(@BillNumber, LEN(@BillNumber) - 4) + RIGHT('000' + CAST((CAST(RIGHT(@BillNumber, 4) AS INT) + 1) as VARCHAR(5)), 4)    
    
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