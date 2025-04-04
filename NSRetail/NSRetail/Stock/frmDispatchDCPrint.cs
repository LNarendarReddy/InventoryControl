﻿using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using ErrorManagement;
using NSRetail.Reports;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmDispatchDCPrint : XtraForm
    {
        MasterRepository objMasterRep = new MasterRepository();
        public frmDispatchDCPrint()
        {
            InitializeComponent();
        }

        private void frmDispatchDCPrint_Load(object sender, EventArgs e)
        {
            cmbBranch.Properties.DataSource = Utility.GetBranchList();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            cmbCategory.Properties.DataSource = Utility.GetCategoryList();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
            cmbCategory.EditValue = 13;
            cmbCategory.Enabled = false;

            luLocation.Properties.DataSource = objMasterRep.GetAvailableLocations();
            luLocation.Properties.ValueMember = "LOCATIONNAME";
            luLocation.Properties.DisplayMember = "LOCATIONNAME";
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate() ||
                    XtraMessageBox.Show("Are you sure want to print Dispatch DC?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
                DataSet ds = new StockRepository().SaveDispatchDC(cmbBranch.EditValue, cmbCategory.EditValue, luLocation.EditValue, Utility.UserID);
                if (ds == null || ds.Tables.Count == 0) throw new Exception("No Dispatch Exists");
                rptDispatchDC rpt = new rptDispatchDC(ds.Tables[0], ds.Tables[1], false);
                rpt.ShowPrintMarginsWarning = false;
                rpt.ShowRibbonPreview();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
