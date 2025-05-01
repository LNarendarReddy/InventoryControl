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

namespace NSRetail.ReportForms.Wareshouse.Differences
{
    public partial class ucIndentDispatchDifferences : SearchCriteriaBase
    {
        public ucIndentDispatchDifferences()
        {
            InitializeComponent();

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "DISPATCHNUMBER", "Dispatch #" },
                { "INDENTQUANTITY", "Indent Qty." },
                { "DISPATCHQTY", "Dispatch Qty." },
                { "PREVBRANCHSTOCK", "Prev. branch Stock" },
                { "PREVWHSTOCK", "Prev. WH Stock" },
                { "CURBSTOCK", "Current branch Stock" },
                { "CURWHSTOCK", "Current WH Stock" },                
                { "DIFF", "Difference" },
                { "NOOFDAYS", "# of days" }
            };

            SetFocusControls(cmbBranch, dtpToDate, specificColumnHeaders);
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
        }


        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FromDate", dtpFromDate.EditValue },
                { "ToDate", dtpToDate.EditValue },
                { "CategoryIDs", cmbCategory.EditValue }, 
                { "BranchIDs", cmbBranch.EditValue },
            };

            return GetReportData("USP_RPT_INDENTVSDISPATCH", parameters);
        }
    }
}
