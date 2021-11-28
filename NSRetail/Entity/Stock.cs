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

        private object _STOCKDISPATCHID = 0;
        public object STOCKDISPATCHID { get { return _STOCKDISPATCHID; } set { _STOCKDISPATCHID = value; } }
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
