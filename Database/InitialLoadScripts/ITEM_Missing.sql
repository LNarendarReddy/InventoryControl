SELECT * FROM ITEM WHERE ITEMNAME = 'SUGAR'

SELECT distinct Product_ID, Product_Name FROM skumaster where  NOT EXISTS (SELECT 1 FROM ITEM WHERE SKUCODE = Product_ID) 
and Product_ID is not null and Category is not null

INSERT INTO ITEM(SKUCODE, ITEMNAME, CREATEDBY, CREATEDDATE, CATEGORYID)
SELECT distinct Product_ID, Product_Name, 4, GETDATE(), 4 FROM skumaster where Category is null 
and product_id in(
'1000312'
, '1000846'
, '1001677'
, '1003540'
, '1004673'
, '1005549'
, '1008858'
, '1009021'
, '1009231'
, '1009314'
, '1010101'
, '1011171'
, '1011238'
, '1011268'
, '1011270'
, '1011810'
, '1012001'
, '1012071'
, '1012113'
, '1012127'
, '1012254'
, '1012316'
, '1012394'
, '1012472'
, '1012516'
, '1012752'
, '1012986'
, '1013093'
, '1013152'
, '1013400'
, '1013422'
, '1013570'
, '1013851'
, '1013898'
, '1014671'
, '1015584'
, '1015817'
, '1015853'
, '1015951'
, '1015971'
, '1016216'
, '1016272'
, '1016565'
, '1016769'
, '1016875'
, '1016915'
, '1018076'
, '1025199'
, '1025612'
, '1025944'
, '1026791'
, '1026953'
, '1027972'
, '1028045'
, '1029030'
, '1029698'
, '1031754'
, '1037677'
, '1038659'
, '1038788'
, '1039219'
, '1039290'
, '1040339'
, '1040470'
, '1040578'
, '1040703'
, '1041033'
, '1041711'
, '1041799'
, '1042888'
, '1043078'
, '1043880'
, '1043947'
, '1044971'
, '1045872'
, '1046253'
, '1047070'
, '1047199'
, '1048491'
, '1048903'
, '1049911'
, '1050070'
, '1050071'
, '1050142'
, '1050238'
, '1052175'
, '1052220'
, '1052221'
, '1052222'
, '1052766'
, '1052870'
, '1053012'
, '1053033'
, '1053089'
, '1053368'
, '1053939'
, '1054143'
, '1054170'
, '1055216'
, '1056093'
, '1056098'
, '1056110'
, '1056111'
, '1056113'
, '1056114'
, '1056117'
, '1056118'
, '1056119'
, '1056120'
, '1056121'
, '1056122'
, '1056124'
, '1056125'
, '1056126'
, '1056127'
, '1056128'
, '1056129'
, '1056130'
, '1056132'
, '1056133'
, '1056134'
, '1056135'
, '1056136'
, '1056138'
, '1056139'
, '1056140'
, '1056141'
, '1056142'
, '1056143'
, '1056144'
, '1056145'
, '1056146'
, '1056147'
, '1056148'
, '1056149'
, '1056150'
, '1056151'
, '1056472'
, '1056499'
, '1056531'
, '1056532'
, '1056535'
, '1056536'
, '1056537'
, '1056541'
, '1056542'
, '1056543'
, '1056544'
, '1056546'
, '1056547'
, '1056548'
, '1056549'
, '1056550'
, '1056551'
, '1056555'
, '1056556'
, '1056557'
, '1056558'
, '1056559'
, '1056561'
, '1056563'
, '1056565'
, '1056567'
, '1056568'
, '1056569'
, '1056571'
, '1056572'
, '1056574'
, '1056575'
, '1057091'
, '1057237'
, '1058034')



SELECT top 10 * from ITEMCODE

INSERT INTO ITEMCODE(ITEMID, ITEMCODE, ISEAN, HSNCODE, CREATEDBY, CREATEDDATE)
SELECT I.ITEMID, EAN_Code, CASE WHEN EAN_Code = I.SKUCODE OR EAN_Code = '00' + I.SKUCODE THEN 0 ELSE 1 END
, HSN_Code, 4, GETDATE() FROM (
SELECT distinct Product_ID, Product_Name, EAN_Code, HSN_Code FROM skumaster where Category is null 
and product_id in(
'1000312'
, '1000846'
, '1001677'
, '1003540'
, '1004673'
, '1005549'
, '1008858'
, '1009021'
, '1009231'
, '1009314'
, '1010101'
, '1011171'
, '1011238'
, '1011268'
, '1011270'
, '1011810'
, '1012001'
, '1012071'
, '1012113'
, '1012127'
, '1012254'
, '1012316'
, '1012394'
, '1012472'
, '1012516'
, '1012752'
, '1012986'
, '1013093'
, '1013152'
, '1013400'
, '1013422'
, '1013570'
, '1013851'
, '1013898'
, '1014671'
, '1015584'
, '1015817'
, '1015853'
, '1015951'
, '1015971'
, '1016216'
, '1016272'
, '1016565'
, '1016769'
, '1016875'
, '1016915'
, '1018076'
, '1025199'
, '1025612'
, '1025944'
, '1026791'
, '1026953'
, '1027972'
, '1028045'
, '1029030'
, '1029698'
, '1031754'
, '1037677'
, '1038659'
, '1038788'
, '1039219'
, '1039290'
, '1040339'
, '1040470'
, '1040578'
, '1040703'
, '1041033'
, '1041711'
, '1041799'
, '1042888'
, '1043078'
, '1043880'
, '1043947'
, '1044971'
, '1045872'
, '1046253'
, '1047070'
, '1047199'
, '1048491'
, '1048903'
, '1049911'
, '1050070'
, '1050071'
, '1050142'
, '1050238'
, '1052175'
, '1052220'
, '1052221'
, '1052222'
, '1052766'
, '1052870'
, '1053012'
, '1053033'
, '1053089'
, '1053368'
, '1053939'
, '1054143'
, '1054170'
, '1055216'
, '1056093'
, '1056098'
, '1056110'
, '1056111'
, '1056113'
, '1056114'
, '1056117'
, '1056118'
, '1056119'
, '1056120'
, '1056121'
, '1056122'
, '1056124'
, '1056125'
, '1056126'
, '1056127'
, '1056128'
, '1056129'
, '1056130'
, '1056132'
, '1056133'
, '1056134'
, '1056135'
, '1056136'
, '1056138'
, '1056139'
, '1056140'
, '1056141'
, '1056142'
, '1056143'
, '1056144'
, '1056145'
, '1056146'
, '1056147'
, '1056148'
, '1056149'
, '1056150'
, '1056151'
, '1056472'
, '1056499'
, '1056531'
, '1056532'
, '1056535'
, '1056536'
, '1056537'
, '1056541'
, '1056542'
, '1056543'
, '1056544'
, '1056546'
, '1056547'
, '1056548'
, '1056549'
, '1056550'
, '1056551'
, '1056555'
, '1056556'
, '1056557'
, '1056558'
, '1056559'
, '1056561'
, '1056563'
, '1056565'
, '1056567'
, '1056568'
, '1056569'
, '1056571'
, '1056572'
, '1056574'
, '1056575'
, '1057091'
, '1057237'
, '1058034')) SKUIC
INNER JOIN ITEM I ON I.SKUCODE = SKUIC.Product_ID

























select * from ITEMPRICE
INSERT INTO ITEMPRICE(ITEMCODEID
, SALEPRICE
, MRP
, GSTID
, CREATEDBY
, CREATEDDATE)
SELECT IC.ITEMCODEID, SKUIP.SAle_price, SKUIP.MRP, SKUIP.GSTID, 4, GETDATE() FROM (
SELECT distinct Product_ID, Product_Name, EAN_Code, HSN_Code, Sale_Price, MRP,
CASE Tax_Code
	 WHEN 'GST_0'	 THEN	 6
	 WHEN 'GST_12'	 THEN	5
	 WHEN 'GST_18'	 THEN	8
	 WHEN 'GST_28'	 THEN	9
	 WHEN 'GST_28_12' THEN	11
	 WHEN 'GST_28_36' THEN	 12
	 WHEN 'GST_28_5'	 THEN	10
	 WHEN 'GST_3'	 THEN	7
	 WHEN 'GST_5'	 THEN	4
	END AS GSTID
FROM skumaster where Category is null 
and product_id in(
'1000312'
, '1000846'
, '1001677'
, '1003540'
, '1004673'
, '1005549'
, '1008858'
, '1009021'
, '1009231'
, '1009314'
, '1010101'
, '1011171'
, '1011238'
, '1011268'
, '1011270'
, '1011810'
, '1012001'
, '1012071'
, '1012113'
, '1012127'
, '1012254'
, '1012316'
, '1012394'
, '1012472'
, '1012516'
, '1012752'
, '1012986'
, '1013093'
, '1013152'
, '1013400'
, '1013422'
, '1013570'
, '1013851'
, '1013898'
, '1014671'
, '1015584'
, '1015817'
, '1015853'
, '1015951'
, '1015971'
, '1016216'
, '1016272'
, '1016565'
, '1016769'
, '1016875'
, '1016915'
, '1018076'
, '1025199'
, '1025612'
, '1025944'
, '1026791'
, '1026953'
, '1027972'
, '1028045'
, '1029030'
, '1029698'
, '1031754'
, '1037677'
, '1038659'
, '1038788'
, '1039219'
, '1039290'
, '1040339'
, '1040470'
, '1040578'
, '1040703'
, '1041033'
, '1041711'
, '1041799'
, '1042888'
, '1043078'
, '1043880'
, '1043947'
, '1044971'
, '1045872'
, '1046253'
, '1047070'
, '1047199'
, '1048491'
, '1048903'
, '1049911'
, '1050070'
, '1050071'
, '1050142'
, '1050238'
, '1052175'
, '1052220'
, '1052221'
, '1052222'
, '1052766'
, '1052870'
, '1053012'
, '1053033'
, '1053089'
, '1053368'
, '1053939'
, '1054143'
, '1054170'
, '1055216'
, '1056093'
, '1056098'
, '1056110'
, '1056111'
, '1056113'
, '1056114'
, '1056117'
, '1056118'
, '1056119'
, '1056120'
, '1056121'
, '1056122'
, '1056124'
, '1056125'
, '1056126'
, '1056127'
, '1056128'
, '1056129'
, '1056130'
, '1056132'
, '1056133'
, '1056134'
, '1056135'
, '1056136'
, '1056138'
, '1056139'
, '1056140'
, '1056141'
, '1056142'
, '1056143'
, '1056144'
, '1056145'
, '1056146'
, '1056147'
, '1056148'
, '1056149'
, '1056150'
, '1056151'
, '1056472'
, '1056499'
, '1056531'
, '1056532'
, '1056535'
, '1056536'
, '1056537'
, '1056541'
, '1056542'
, '1056543'
, '1056544'
, '1056546'
, '1056547'
, '1056548'
, '1056549'
, '1056550'
, '1056551'
, '1056555'
, '1056556'
, '1056557'
, '1056558'
, '1056559'
, '1056561'
, '1056563'
, '1056565'
, '1056567'
, '1056568'
, '1056569'
, '1056571'
, '1056572'
, '1056574'
, '1056575'
, '1057091'
, '1057237'
, '1058034')) SKUIP
INNER JOIN ITEM I ON I.SKUCODE = SKUIP.Product_ID
INNER JOIN ITEMCODE IC ON IC.ITEMCODE = SKUIP.EAN_Code AND IC.ITEMID = I.ITEMID
WHERE
	SKUIP.GSTID IS NOT NULL





select * from ITEMCOSTPRICE
INSERT INTO ITEMCOSTPRICE(ITEMPRICEID
, COSTPRICEWT
, GSTID
, CREATEDBY
, CREATEDDATE)
SELECT IP.ITEMPRICEID, SKUIP.Cost_Price, SKUIP.GSTID, 4, GETDATE() FROM (
SELECT distinct Product_ID, Product_Name, EAN_Code, HSN_Code, Sale_Price, MRP, Cost_Price,
CASE Tax_Code
	 WHEN 'GST_0'	 THEN	 6
	 WHEN 'GST_12'	 THEN	5
	 WHEN 'GST_18'	 THEN	8
	 WHEN 'GST_28'	 THEN	9
	 WHEN 'GST_28_12' THEN	11
	 WHEN 'GST_28_36' THEN	 12
	 WHEN 'GST_28_5'	 THEN	10
	 WHEN 'GST_3'	 THEN	7
	 WHEN 'GST_5'	 THEN	4
	END AS GSTID
FROM skumaster where Category is null 
and product_id in(
'1000312'
, '1000846'
, '1001677'
, '1003540'
, '1004673'
, '1005549'
, '1008858'
, '1009021'
, '1009231'
, '1009314'
, '1010101'
, '1011171'
, '1011238'
, '1011268'
, '1011270'
, '1011810'
, '1012001'
, '1012071'
, '1012113'
, '1012127'
, '1012254'
, '1012316'
, '1012394'
, '1012472'
, '1012516'
, '1012752'
, '1012986'
, '1013093'
, '1013152'
, '1013400'
, '1013422'
, '1013570'
, '1013851'
, '1013898'
, '1014671'
, '1015584'
, '1015817'
, '1015853'
, '1015951'
, '1015971'
, '1016216'
, '1016272'
, '1016565'
, '1016769'
, '1016875'
, '1016915'
, '1018076'
, '1025199'
, '1025612'
, '1025944'
, '1026791'
, '1026953'
, '1027972'
, '1028045'
, '1029030'
, '1029698'
, '1031754'
, '1037677'
, '1038659'
, '1038788'
, '1039219'
, '1039290'
, '1040339'
, '1040470'
, '1040578'
, '1040703'
, '1041033'
, '1041711'
, '1041799'
, '1042888'
, '1043078'
, '1043880'
, '1043947'
, '1044971'
, '1045872'
, '1046253'
, '1047070'
, '1047199'
, '1048491'
, '1048903'
, '1049911'
, '1050070'
, '1050071'
, '1050142'
, '1050238'
, '1052175'
, '1052220'
, '1052221'
, '1052222'
, '1052766'
, '1052870'
, '1053012'
, '1053033'
, '1053089'
, '1053368'
, '1053939'
, '1054143'
, '1054170'
, '1055216'
, '1056093'
, '1056098'
, '1056110'
, '1056111'
, '1056113'
, '1056114'
, '1056117'
, '1056118'
, '1056119'
, '1056120'
, '1056121'
, '1056122'
, '1056124'
, '1056125'
, '1056126'
, '1056127'
, '1056128'
, '1056129'
, '1056130'
, '1056132'
, '1056133'
, '1056134'
, '1056135'
, '1056136'
, '1056138'
, '1056139'
, '1056140'
, '1056141'
, '1056142'
, '1056143'
, '1056144'
, '1056145'
, '1056146'
, '1056147'
, '1056148'
, '1056149'
, '1056150'
, '1056151'
, '1056472'
, '1056499'
, '1056531'
, '1056532'
, '1056535'
, '1056536'
, '1056537'
, '1056541'
, '1056542'
, '1056543'
, '1056544'
, '1056546'
, '1056547'
, '1056548'
, '1056549'
, '1056550'
, '1056551'
, '1056555'
, '1056556'
, '1056557'
, '1056558'
, '1056559'
, '1056561'
, '1056563'
, '1056565'
, '1056567'
, '1056568'
, '1056569'
, '1056571'
, '1056572'
, '1056574'
, '1056575'
, '1057091'
, '1057237'
, '1058034')) SKUIP
INNER JOIN ITEM I ON I.SKUCODE = SKUIP.Product_ID
INNER JOIN ITEMCODE IC ON IC.ITEMCODE = SKUIP.EAN_Code AND IC.ITEMID = I.ITEMID
INNER JOIN ITEMPRICE IP ON IP.ITEMCODEID = IC.ITEMCODEID AND SKUIP.Sale_Price = IP.SALEPRICE AND SKUIP.MRP = IP.MRP
WHERE
	SKUIP.GSTID IS NOT NULL