using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Wareshouse.StockReports
{
    public partial class ucStockAsOnDate : SearchCriteriaBase
    {
        public ucStockAsOnDate()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "QTYORWGHT", "Quantity or Weight in KG(s)" }
            };

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            SetFocusControls(dtAsOnDate, cmbBranch, columnHeaders);

            dtAsOnDate.EditValue = DateTime.Today.AddDays(-DateTime.Today.Day);
            MandatoryFields = new List<BaseEdit> { dtAsOnDate };
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue },
                { "AsOnDate", dtAsOnDate.EditValue },
            };
            return GetReportData("USP_RPT_STOCK_ASOFDATE", parameters);
        }
    }
}
