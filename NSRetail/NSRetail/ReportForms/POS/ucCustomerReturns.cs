using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.POS
{
    public partial class ucCustomerReturns : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;

        public override Control FirstControl => cmbBranch;
        public override Control LastControl => chkIncludeBranch;
        public ucCustomerReturns()
        {
            InitializeComponent();
            columnHeaders = new Dictionary<string, string>
            {
                { "REFUNDDATE", "Refund Date" }
                , { "BILLNUMBER", "Bill Number" }
                , { "REFUNDQUANTITY", "Quantity" }
                , { "REFUNDWEIGHTINKGS", "Weight In Kgs" }
                , { "REFUNDAMOUNT", "Refund Amount" }
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;
        }
        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "IncludeBillNo", chkIncludeBillNumber.EditValue }
                , { "IncludeBranch", chkIncludeBranch.EditValue }
                , { "IncludeRefundDate", chkIncludeDate.EditValue }
            };
            DataTable dt = GetReportData("USP_RPT_CUSTOMERRETURNS", parameters);

            if (chkIncludeBillNumber.EditValue.Equals(false)) dt.Columns.Remove("BILLNUMBER");
            if (chkIncludeBranch.EditValue.Equals(false)) dt.Columns.Remove("BRANCHNAME");
            if (chkIncludeDate.EditValue.Equals(false)) dt.Columns.Remove("REFUNDDATE");
            return dt;
        }
    }
}
