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
        public object TCS { get; set; }
        public object DISCOUNTPER { get; set; }
        public object DISCOUNTFLAT { get; set; }
        public object EXPENSES { get; set; }
        public object TRANSPORT { get; set; }
        public object SumTotalPriceWT { get; set; }
        public object SumTotalPriceWOT { get; set; }
        public object SumGSTValue { get; set; }
        public object SumFinalPrice { get; set; }

        public bool CalculateIGST = false;
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
        public object GSTID { get; set; }

        public object DiscountPercentage { get; set; }

        public object DiscountFlat { get; set; }

        public object SchemePercentage { get; set; }

        public object SchemeFlat { get; set; }

        public object FreeQuantity { get; set; }

        public object FreeItemMRPID { get; set; }
        
        public object TotalPriceWT { get; set; }

        public object TotalPriceWOT { get; set; }

        public object AppliedDiscount { get; set; }

        public object AppliedScheme { get; set; }
        
        public object AppliedGST { get; set; }

        public object FinalPrice { get; set; }
        public object CGST { get; set; }
        public object SGST { get; set; }
        public object IGST { get; set; }
        public object CESS { get; set; }
        public object HSNCODE { get; set; }

    }
    public class SupplierReturns : EntityBase
    {
        public object SupplierReturnsID { get; set; }
        public object SupplierID { get; set; }
        public DataTable dtSupplierReturns { get; set; }
    }
    public class SupplierReturnsDetail : EntityBase
    {
        public object SupplierReturnsDetailID { get; set; }
        public object SupplierReturnsID { get; set; }
        public object ItemPriceID { get; set; }
        public object Quantity { get; set; }
        public object WeightInKgs { get; set; }
    }
    public class StockAdjustment : EntityBase
    {
        public object BranchID { get; set; }
        public object StockAdjustmentID { get; set; }
        public object ItemPriceID { get; set; }
        public object Quantity { get; set; }
        public object WeightInKgs { get; set; }

    }
}
