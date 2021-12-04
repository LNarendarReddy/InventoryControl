using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Stock :  EntityBase
    {
    }
    public class StockDispatch : EntityBase
    {

        public object STOCKDISPATCHID { get; set; }
        public object FROMBRANCHID { get; set; }
        public object TOBRANCHID { get; set; }
        public object CATEGORYID { get; set; }
        public DataTable dtDispatch { get; set; }
    }
    public class StockDispatchDetail : EntityBase
    {
        public object STOCKDISPATCHDETAILID { get; set; }
        public object STOCKDISPATCHID { get; set; }
        public object ITEMPRICEID { get; set; }
        public object TRAYNUMBER { get; set; }
        public object DISPATCHQUANTITY { get; set; }
        public object WEIGHTINKGS { get; set; }
    }
    public class StockEntry : EntityBase
    {
        public object STOCKENTRYID { get; set; }
        public object SUPPLIERID { get; set; }
        public object SUPPLIERINVOICENO { get; set; }
        public object TAXINCLUSIVE { get; set; }
        public object InvoiceDate { get; set; }
        public object CATEGORYID { get; set; }
        public DataTable dtStockEntry { get; set; }
    }
    public class StockEntryDetail : EntityBase
    {
        public object STOCKENTRYDETAILID { get; set; }
        public object STOCKENTRYID { get; set; }
        public object ITEMID { get; set; }
        public object ITEMCODEID { get; set; }
        public object ITEMPRICEID { get; set; }
        public object ITEMCODE { get; set; }
        public object ITEMNAME { get; set; }
        public object SKUCODE { get; set; }
        public object QUANTITY { get; set; }
        public object WEIGHTINKGS { get; set; }
        public object COSTPRICEWT { get; set; }
        public object COSTPRICEWOT { get; set; }
        public object MRP { get; set; }
        public object SALEPRICE { get; set; }

    }
}
