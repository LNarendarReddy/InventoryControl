using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using NSRetailPOS.ReportControls.ReportBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace NSRetailPOS.Operations.Reports
{
    public partial class ucBRRejectedItems : SearchCriteriaBase
    {
        public ucBRRejectedItems()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "REJECTEDQUANTITY", "Rejected Quantity" }
                , { "BREFUNDNUMBER", "B Refund #" }
                , { "REJECTEDDATE", "Rejected Date" }
            };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(dtpFromDate, dtpToDate, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BRANCHID", Utility.branchInfo.BranchID }
                , { "FROMDATE", dtpFromDate.EditValue }
                , { "TODATE", dtpToDate.EditValue }
            };
            return GetReportData("USP_RPT_BREFUNDS_REJECTEDITEMS", parameters);
        }
    }
}
