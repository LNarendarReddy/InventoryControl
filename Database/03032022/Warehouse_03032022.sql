ALTER PROC [dbo].[USP_R_GETSYNCDATA]        
(        
 @EntityName VARCHAR(50)        
 ,  @SyncDate DATETIME        
)        
AS        
BEGIN        
        
 IF @EntityName = 'ITEM'        
 BEGIN        
  SELECT ITEMID, SKUCODE, ITEMNAME, CREATEDDATE, UPDATEDATE, DELETEDDATE, ISOPENITEM, PARENTITEMID, UOMID, CATEGORYID        
  FROM ITEM I        
  WHERE        
   I.CREATEDDATE > @SyncDate        
   OR I.UPDATEDATE > @SyncDate        
   OR I.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'ITEMCODE'        
 BEGIN        
  SELECT ITEMCODEID, ITEMID, ITEMCODE, CREATEDDATE, UPDATEDATE, DELETEDDATE, FREEITEMCODEID, HSNCODE         
  FROM ITEMCODE IC        
  WHERE        
   IC.CREATEDDATE > @SyncDate        
   OR IC.UPDATEDATE > @SyncDate        
   OR IC.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'ITEMPRICE'        
 BEGIN        
  SELECT ITEMPRICEID, ITEMCODEID, SALEPRICE, MRP, GSTID, CREATEDDATE, UPDATEDATE, DELETEDDATE         
  FROM ITEMPRICE IP        
  WHERE        
   IP.CREATEDDATE > @SyncDate        
   OR IP.UPDATEDATE > @SyncDate        
   OR IP.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'BRANCH'        
 BEGIN        
  SELECT BRANCHID, BRANCHNAME, BRANCHCODE, ADDRESS, PHONENO, LANDLINE, EMAILID, SUPERVISORID, ISWAREHOUSE, CREATEDDATE, UPDATEDATE, DELETEDDATE, STATEID         
  FROM BRANCH B        
  WHERE        
   B.CREATEDDATE > @SyncDate        
   OR B.UPDATEDATE > @SyncDate        
   OR B.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'GST'        
 BEGIN        
  SELECT GSTID, GSTCODE, CGST, SGST, IGST, CESS, CREATEDDATE, UPDATEDATE, DELETEDDATE         
  FROM GSTDETAIL GST        
  WHERE        
   GST.CREATEDDATE > @SyncDate        
   OR GST.UPDATEDATE > @SyncDate        
   OR GST.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'STOCKDISPATCH'        
 BEGIN        
  SELECT STOCKDISPATCHID, FROMBRANCHID, TOBRANCHID, STATUS, STATUSAPPROVEDBY, STATUSAPPROVEDDATE, CREATEDBY, CREATEDDATE, UPDATEDBY, UPDATEDATE, DELETEDBY, DELETEDDATE, DISPATCHNUMBER        
  FROM STOCKDISPATCH SD        
  WHERE        
   SD.CREATEDDATE > @SyncDate        
   OR SD.UPDATEDATE > @SyncDate        
   OR SD.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'STOCKDISPATCHDETAIL'        
 BEGIN        
  SELECT STOCKDISPATCHDETAILID, STOCKDISPATCHID, ITEMPRICEID, TRAYNUMBER,       
  DISPATCHQUANTITY, RECEIVEDQUANTITY, CREATEDDATE        
   , UPDATEDATE, DELETEDDATE, WEIGHTINKGS,ISACCEPTED  
  FROM STOCKDISPATCHDETAIL SDD        
  WHERE        
   SDD.CREATEDDATE > @SyncDate        
   OR SDD.UPDATEDATE > @SyncDate        
   OR SDD.DELETEDDATE > @SyncDate        
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
        
 --IF @EntityName = 'CATEGORY'        
 --BEGIN        
 -- SELECT * FROM TBLCATEGORY CAT        
 -- WHERE        
 --  CAT.CREATEDDATE > @SyncDate        
 --  OR CAT.UPDATEDDATE > @SyncDate        
 --  OR CAT.DELETEDDATE > @SyncDate        
 -- RETURN        
 --END        
        
 IF @EntityName = 'BRANCHCOUNTER'        
 BEGIN        
  SELECT COUNTERID, COUNTERNAME, BRANCHID, CREATEDDATE, UPDATEDDATE, DELETEDDATE     
 , DAYCLOSUREID, BRANCHREFUNDID    
  FROM BRANCHCOUNTER CNTR        
  WHERE        
   CNTR.CREATEDDATE > @SyncDate        
   OR CNTR.UPDATEDDATE > @SyncDate        
   OR CNTR.DELETEDDATE > @SyncDate        
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
  WHERE        
   MOP.CREATEDDATE > @SyncDate        
   OR MOP.UPDATEDDATE > @SyncDate        
   OR MOP.DELETEDDATE > @SyncDate        
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
  SELECT USERID, ROLEID, REPORTINGLEADID, CATEGORYID, BRANCHID, USERNAME, PASSWORDSTRING, FULLNAME, CNUMBER, EMAIL, ISOTP, GENDER, DOB, CREATEDDATE, UPDATEDDATE, DELETEDDATE        
  FROM TBLUSER USR        
  WHERE        
   USR.CREATEDDATE > @SyncDate        
   OR USR.UPDATEDDATE > @SyncDate        
   OR USR.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'UOM'        
 BEGIN        
  SELECT UOMID, DISPLAYVALUE, BASEUOMID, MULTIPLIER, CREATEDDATE, UPDATEDDATE, DELETEDDATE         
  FROM UOM UOM        
  WHERE        
   UOM.CREATEDDATE > @SyncDate        
   OR UOM.UPDATEDDATE > @SyncDate        
   OR UOM.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'ITEMGROUP'        
 BEGIN        
  SELECT ITEMGROUPID, GROUPNAME, ISACTIVE, CREATEDDATE, UPDATEDDATE, DELETEDDATE        
  FROM ITEMGROUP IG        
  WHERE        
   IG.CREATEDDATE > @SyncDate        
   OR IG.UPDATEDDATE > @SyncDate        
   OR IG.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'ITEMGROUPDETAIL'        
 BEGIN        
  SELECT ITEMGROUPDETAILID, ITEMGROUPID, ITEMCODEID, CREATEDDATE, DELETEDDATE         
  FROM ITEMGROUPDETAIL IGD        
  WHERE        
   IGD.CREATEDDATE > @SyncDate        
   OR IGD.DELETEDDATE > @SyncDate        
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
  SELECT OFFERID, OFFERNAME, OFFERCODE, STARTDATE, ENDDATE, OFFERVALUE, OFFERTYPEID, CATEGORYID, ITEMGROUPID        
   , CREATEDDATE, UPDATEDDATE, DELETEDDATE, ISACTIVE, APPLIESTOID         
  FROM OFFER OFR        
  WHERE        
   OFR.CREATEDDATE > @SyncDate        
   OR OFR.UPDATEDDATE > @SyncDate        
   OR OFR.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'OFFERBRANCH'        
 BEGIN        
  SELECT OFFERBRANCHID, OFFERID, BRANCHID, CREATEDDATE, DELETEDDATE        
  FROM OFFERBRANCH OFRB        
  WHERE        
   OFRB.CREATEDDATE > @SyncDate        
   OR OFRB.DELETEDDATE > @SyncDate        
  RETURN        
 END        
        
 IF @EntityName = 'OFFERITEMMAP'        
 BEGIN        
  SELECT OFFERITEMMAPID, OFFERID, ITEMCODEID, CREATEDDATE, DELETEDDATE         
  FROM OFFERITEMMAP OFRIM        
  WHERE        
   OFRIM.CREATEDDATE > @SyncDate        
   OR OFRIM.DELETEDDATE > @SyncDate        
  RETURN        
 END        
    
 IF @EntityName = 'POS_DENOMINATION'        
 BEGIN        
  SELECT DENOMINATIONID,DISPLAYVALUE,MULTIPLIER,CREATEDDATE,UPDATEDDATE    
  FROM POS_DENOMINATION DEN    
  WHERE        
   DEN.CREATEDDATE > @SyncDate        
   OR DEN.UPDATEDDATE > @SyncDate        
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
  FROM TBLCATEGORY CAT
  WHERE
   CAT.CREATEDDATE > @SyncDate
   OR CAT.UPDATEDDATE > @SyncDate
   OR CAT.DELETEDDATE > @SyncDate
  RETURN        
 END    
     
END 
GO