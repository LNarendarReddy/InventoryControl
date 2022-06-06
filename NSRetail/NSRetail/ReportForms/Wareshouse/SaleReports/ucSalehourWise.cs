using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.SaleReports
{
    public partial class ucSalehourWise : SearchCriteriaBase
    {
        private Dictionary<string, string> specificColumnHeaders;
        public override Dictionary<string, string> SpecificColumnHeaders => specificColumnHeaders;

        public override Control FirstControl => cmbBranch;

        public override Control LastControl => dtpToDate;

        public ucSalehourWise()
        {
            InitializeComponent();

            specificColumnHeaders = new Dictionary<string, string>()
            {
                { "SALEPRICEWOT", "Sale Price WOT" },
                { "SALETAX", "Sale Price Tax" },
                { "SALEPRICEWT", "Sale Price WT" },
            };
        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "CategoryID", cmbCategory.EditValue }
            };

            return GetReportData("USP_RPT_SALE_HOURWISE", parameters);
        }

        private void ucSalehourWise_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            MasterRepository masterRepo = new MasterRepository();

            cmbBranch.Properties.DataSource = masterRepo.GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            cmbCategory.Properties.DataSource = masterRepo.GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
        }
    }
}
