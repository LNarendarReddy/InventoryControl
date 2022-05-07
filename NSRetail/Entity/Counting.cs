using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Counting
    {
        
    }
    public class StockCounting : EntityBase
    {
        public object STOCKCOUNTINGID { get; set; }
        public object BRANCHID { get; set; }
        public DataTable dtStockCountning { get; set; }
    }
    public class StockCountingDetail : EntityBase
    {
        public object STOCKCOUNTINGDETAILID { get; set; }
        public object STOCKCOUNTINGID { get; set; }
        public object ITEMPRICEID { get; set; }
        public object QUANTITY { get; set; }
        public object WEIGHTINKGS { get; set; }
    }
}
