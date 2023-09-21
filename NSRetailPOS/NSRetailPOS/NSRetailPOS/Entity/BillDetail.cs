using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailPOS.Entity
{
    public class BillDetail
    {
        public object BillDetailID { get; set; }

        public object ItemCode { get; set; }

        public object ItemName { get; set; }

        public object MRP { get; set; }

        public object SalePrice { get; set; }

        public object Quantity { get; set; }

        public object WeightInKGs { get; set; }

        public object BilledAmount { get; set; }

        public object IsOpenItem { get; set; }

        public object ItemPriceID { get; set; }

        public DataTable dtBillDetails { get; set; }

    }
}
