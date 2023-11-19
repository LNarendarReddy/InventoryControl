using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailPOS.Entity
{
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
}
