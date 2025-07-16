using System.Data;

namespace Entity
{
    public class Reports
    {
        
    }

    public class DealerIndent : EntityBase
    {
        public object SupplierIndentID { get; set; }
        public object supplierID { get; set; }
        public object CategoryID { get; set; }
        public object IndentDays { get; set; }
        public object SafetyDays { get; set; }
        public object Status { get; set; }
        public int IsApproved { get; set; }
        public object IndentNo { get; set; }
        public object MobileNo { get; set; }
        public DataTable dtSupplierIndent { get; set; }
    }

    public class CreditBillPayment : EntityBase
    {
        public object CreditBillPaymentID { get; set; }

        public object BillNumber { get; set; }

        public object CustomerName { get; set; }

        public object CustomerNumber { get; set; }

        public object CustomerGST { get; set; }

        public object MOPValue { get; set; }

        public object Status { get; set; }
        
    }

    public class BranchExpense : EntityBase
    {
        public object BranchExpenseID { get; set; }

        public object BranchExpenseTypeID { get; set; }

        public object Amount { get; set; }

        public object BillImage { get; set; }

        public object BranchID { get; set; }
    }
}
