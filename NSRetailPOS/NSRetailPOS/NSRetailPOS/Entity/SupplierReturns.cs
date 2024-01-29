using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailPOS.Entity
{
    public class SupplierReturns : EntityBase
    {
        public object SupplierReturnsID { get; set; }
        public object SupplierID { get; set; }
        public object CategoryID { get; set; }
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
}
