using System;
using System.Data;

namespace NSRetailPOS.Entity
{
    public class Bill : ICloneable
    {
        public object BillID { get; set; }

        public object BillNumber { get; set; }

        public object LastBilledAmount { get; set; }

        public object LastBilledQuantity { get; set; }

        public DataTable dtBillDetails { get; set; }

        public object Clone()
        {
            Bill clonedBillObj = new Bill();
            clonedBillObj.BillID = BillID;
            clonedBillObj.BillNumber = BillNumber;
            clonedBillObj.dtBillDetails = dtBillDetails.Copy();

            return clonedBillObj;
        }
    }
}
