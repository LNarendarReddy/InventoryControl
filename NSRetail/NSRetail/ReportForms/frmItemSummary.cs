using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms
{
    public partial class frmItemSummary : XtraForm
    {
        public frmItemSummary()
        {
            InitializeComponent();
        }

        private void frmItemSummary_Load(object sender, EventArgs e)
        {
            sluItemCode.Properties.DataSource = Utility.GetItemCodeList();
            sluItemCode.Properties.ValueMember = "ITEMID";
            sluItemCode.Properties.DisplayMember = "ITEMNAME";

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if(sluItemCode.EditValue == null)
            {
                XtraMessageBox.Show("Item code not set", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                sluItemCode.Focus();
                return;
            }

            IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this);
                        
            gvItemSale.Columns["BILLNUMBER"].Visible = (bool)chkIncludeBillNumber.EditValue;

            Dictionary<string, object> searchCriteria = new Dictionary<string, object>()
            {
                { "BranchID",  cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
                , { "ItemID", sluItemCode.EditValue }
                , { "IncludeBillNumber", chkIncludeBillNumber.EditValue }
            };

            DataSet dsResult = new ReportRepository().GetReportDataset("USP_RPT_ITEMSUMMARY", searchCriteria);
            
            
            dsResult.Tables[0].TableName = "ITEMSUMMARY";
            if (dsResult.Tables.Count > 2)
            {
                dsResult.Tables[1].TableName = "STOCKIN";
                dsResult.Tables[2].TableName = "DISPATCH";
                dsResult.Tables[3].TableName = "BREFUND";
                dsResult.Tables[4].TableName = "ITEMSALE";
                dsResult.Tables[5].TableName = "CREFUND";

                DataColumn dcParentBranchID = dsResult.Tables["ITEMSUMMARY"].Columns["BRANCHID"];
                dsResult.Relations.Add("Stock Dispacth", dcParentBranchID, dsResult.Tables["DISPATCH"].Columns["BRANCHID"]);
                dsResult.Relations.Add("Branch Refunds", dcParentBranchID, dsResult.Tables["BREFUND"].Columns["BRANCHID"]);
                dsResult.Relations.Add("Item Sales", dcParentBranchID, dsResult.Tables["ITEMSALE"].Columns["BRANCHID"]);
                dsResult.Relations.Add("Customer Refunds", dcParentBranchID, dsResult.Tables["CREFUND"].Columns["BRANCHID"]);                
            }

            gcItemSummary.DataSource = dsResult.Tables[0];
            gcPurchase.DataSource = dsResult.Tables[1];

            if(gvItemSummary.RowCount == 1)
            {
                gvItemSummary.ExpandMasterRow(0);
            }

            SplashScreenManager.CloseOverlayForm(handle);
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcItemSummary.ShowRibbonPrintPreview();
        }
    }
}