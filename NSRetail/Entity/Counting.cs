using System.Data;

namespace Entity
{
    public class SliceCounting : EntityBase
    {
        public object SliceCountingID { get; set; }

        public object BranchIDs { get; set; }

        public object BranchText { get; set; }

        public object SelectedCriteria { get; set; }

        public object SaleDays { get; set; }

        public DataTable dtCountingItems { get; set; }
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
