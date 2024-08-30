using DevExpress.XtraEditors;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.BranchReports
{
    public partial class ucBranchExpenseList : SearchCriteriaBase
    {
        public ucBranchExpenseList()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "EXPENSEDESC", "Description" }
                , { "AMOUNT", "Amount" }
                , { "CREATEDBY", "Created by" }
                , { "UPDATEDBY", "Updated by" }
                , { "UPDATEDDATE", "Updated Date" }
            };

            MandatoryFields = new List<BaseEdit>() { cmbBranch, dtFromDate, dtToDate };

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch, dtToDate, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
            };

            return GetReportData("USP_R_BRANCHEXPENSE", parameters);
        }
    }
}
