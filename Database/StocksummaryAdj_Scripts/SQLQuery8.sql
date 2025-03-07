select CAST(SSL.CREATEDDATE AS DATE) AS CREATEDDATE, SUM(SSL.OLDQTY - SSL.QUANTITY) AS DIFF, SSL.TTYPE from STOCKSUMMARYLOG SSL 
INNER JOIN STOCKSUMMARY SS ON SS.STOCKSUMMARYID = SSL.STOCKSUMMARYID
INNER JOIN ITEMPRICE IP ON SS.ITEMPRICEID = IP.ITEMPRICEID AND IP.ITEMCODEID = 11327
WHERE SS.BRANCHID = 48 and SSL.CREATEDDATE > '2023-03-14 15:45:13.340'-- and ssl.ttype = 'BS'
GROUP BY CAST(SSL.CREATEDDATE AS DATE), SSL.TTYPE
ORDER BY CAST(SSL.CREATEDDATE AS DATE)

select SS.ITEMPRICEID, SSL.*, SSL.OLDQTY - SSL.QUANTITY AS DIFF from STOCKSUMMARYLOG SSL 
INNER JOIN STOCKSUMMARY SS ON SS.STOCKSUMMARYID = SSL.STOCKSUMMARYID
INNER JOIN ITEMPRICE IP ON SS.ITEMPRICEID = IP.ITEMPRICEID AND IP.ITEMCODEID = 11327
WHERE SS.BRANCHID = 48 and SSL.CREATEDDATE > '2023-03-14 15:45:13.340' --and ssl.ttype = 'BS'
ORDER BY CAST(SSL.CREATEDDATE AS DATE)

--select SUM(SSL.OLDQTY - SSL.QUANTITY) AS DIFF from STOCKSUMMARYLOG SSL 
--INNER JOIN STOCKSUMMARY SS ON SS.STOCKSUMMARYID = SSL.STOCKSUMMARYID
--INNER JOIN ITEMPRICE IP ON SS.ITEMPRICEID = IP.ITEMPRICEID AND IP.ITEMCODEID = 11327
--WHERE SS.BRANCHID = 46 and SSL.CREATEDDATE > '2023-03-14 15:45:13.340' and ssl.ttype = 'BS'

--select SS.ITEMPRICEID, SSL.*, SSL.OLDQTY - SSL.QUANTITY AS DIFF from STOCKSUMMARYLOG SSL 
--INNER JOIN STOCKSUMMARY SS ON SS.STOCKSUMMARYID = SSL.STOCKSUMMARYID
--INNER JOIN ITEMPRICE IP ON SS.ITEMPRICEID = IP.ITEMPRICEID AND IP.ITEMCODEID = 60557
--WHERE SS.BRANCHID = 48 and SSL.CREATEDDATE > '2023-03-14 15:00:00.803' and ssl.ttype = 'BS'
--ORDER BY CAST(SSL.CREATEDDATE AS DATE)

--select IC.ITEMCODE,I.ITEMNAME, SUM(SSL.OLDQTY - SSL.QUANTITY) AS DIFF from STOCKSUMMARYLOG SSL 
--INNER JOIN STOCKSUMMARY SS ON SS.STOCKSUMMARYID = SSL.STOCKSUMMARYID
--INNER JOIN ITEMPRICE IP ON SS.ITEMPRICEID = IP.ITEMPRICEID
--INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID
--INNER JOIN ITEM I ON I.ITEMID = IC.ITEMID
--WHERE SS.BRANCHID = 48 and (SSL.CREATEDDATE = '2023-03-26 15:53:29.803' OR SSL.CREATEDDATE = '2023-03-31 21:22:41.340') and ssl.ttype = 'BS'
--GROUP BY IC.ITEMCODE,I.ITEMNAME

--select I.ITEMID,I.SKUCODE,I.ITEMNAME, SUM(SSL.OLDQTY - SSL.QUANTITY) AS DIFF from STOCKSUMMARYLOG SSL 
--INNER JOIN STOCKSUMMARY SS ON SS.STOCKSUMMARYID = SSL.STOCKSUMMARYID
--INNER JOIN ITEMPRICE IP ON SS.ITEMPRICEID = IP.ITEMPRICEID
--INNER JOIN ITEMCODE IC ON IP.ITEMCODEID = IC.ITEMCODEID
--INNER JOIN ITEM I ON I.ITEMID = IC.ITEMID
--WHERE SS.BRANCHID = 48 and (SSL.CREATEDDATE = '2023-03-26 15:53:29.803' OR SSL.CREATEDDATE = '2023-03-31 21:22:41.340') and ssl.ttype = 'BS'
--GROUP BY I.ITEMID,I.SKUCODE,I.ITEMNAME