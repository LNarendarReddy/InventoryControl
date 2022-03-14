
INSERT INTO ITEM(SKUCODE, ITEMNAME, CATEGORYID, SUBCATEGORYID, CREATEDBY, CREATEDDATE)
SELECT 
	product_id, product_name, CAT.CATEGORYID, SUBCAT.SUBCATEGORYID, 4, GETDATE()
FROM
	(select distinct product_id, product_name, category, sub_category from skumaster 
	where product_ID is not null and category is not null) SKUITEM
	INNER JOIN TBLCATEGORY CAT ON CAT.CATEGORYNAME = SKUITEM.category
	LEFT JOIN SUBCATEGORY SUBCAT ON SUBCAT.CATEGORYID = CAT.CATEGORYID AND SUBCAT.SUBCATEGORYNAME = sub_category

select * from ITEM

select * from skumaster where product_id is null

select distinct product_id, product_name, category, sub_category from skumaster 
where product_ID is not null and category is  null