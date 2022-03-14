
INSERT INTO SUBCATEGORY (CATEGORYID, SUBCATEGORYNAME, CREATEDBY, CREATEDDATE)
SELECT CAT.CATEGORYID, SKUM.SUB_CATEGORY, 4, GETDATE() FROM
(select distinct category, Sub_Category  
from skumaster sku 
where category is not null and sub_category is not null) SKUM
inner join TBLCATEGORY CAT ON CAT.CATEGORYNAME = SKUM.category


select * from SUBCATEGORY

insert into TBLCATEGORY(CATEGORYNAME, CREATEDBY, CREATEDDATE, ALLOWOPENITEMS)
select 'FOOTWARE', 4, getdate(), 0 union all
select 'KIRANA', 4, getdate(), 1 union all
select 'FOOD', 4, getdate(), 0 union all
select 'GENERAL', 4, getdate(), 0 union all
select 'PLASTIC', 4, getdate(), 0 union all
select 'JEWELLERY', 4, getdate(), 0 union all
select 'SPICES', 4, getdate(), 1 union all
select 'GIFT', 4, getdate(), 0 union all
select 'CLOTH', 4, getdate(), 0 union all
select 'OFFERS', 4, getdate(), 0 union all
select 'JEWLLERY', 4, getdate(), 0 union all
select 'OILS', 4, getdate(), 0 
		














