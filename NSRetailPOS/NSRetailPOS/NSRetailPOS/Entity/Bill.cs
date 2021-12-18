using System.Data;

namespace NSRetailPOS.Entity
{
    public class Bill
    {
        public object BillID { get; set; }

        public object BillNumber { get; set; }

        public object LastBilledAmount { get; set; }

        public object LastBilledQuantity { get; set; }

        public object MRP { get; set; }

        public object SalePrice { get; set; }

        public object Savings { get; set; }

        public object SGST { get; set; }

        public object CGST { get; set; }

        public object IGST { get; set; }

        public object CESS { get; set; }

        public object GSTSUM { get; set; }

        public DataTable dtBillDetails { get; set; }
    }
}
