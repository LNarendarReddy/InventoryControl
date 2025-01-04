using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using Entity;
using DevExpress.XtraSplashScreen;
using NSRetail.Login;
using NSRetail.ReportForms.Stock.StockCounting;

namespace NSRetail.ReportForms.Wareshouse.Audit
{
    public partial class ucStockCountingList : SearchCriteriaBase
    {
        CountingRepository countingRepository = new CountingRepository();
               
        public ucStockCountingList()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "STOCKCOUNTINGID", "Counrting Sheet #" }
                , { "CREATEDBY", "Created By" }
                , { "CREATEDDATE", "Created Date" }
                , { "SubmittedBy", "Submitted By" }
                , { "SubmittedDate", "Submitted Date" }
                , { "ACCEPTEDBY", "Accepted By" }
                , { "STOCKACCEPTEDDATE", "Accepted Date" }
                , { "ArchivedBy", "Archived By" }
                , { "ArchivedDate", "Archived Date" }
                , { "STATUS", "Status" }
            };

            ContextmenuItems = new Dictionary<string, string> 
            { 
                { "View", "69FB1182-A548-4547-958F-126D4AB086B5" }, 
                { "Discard", "1A2B9C8E-A56B-4CCE-9598-B063E7B61CBC" } 
            };

            cmbBranch.Properties.DataSource = Utility.GetBranchList();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            SetFocusControls(cmbBranch, cmbBranch,columnHeaders);
        }

        private void ShowItemsForm(XtraForm obj)
        {
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnViewConsolidatedStock_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            frmViewSCItems obj =
                new frmViewSCItems(countingRepository.GetConsolidatedItems(cmbBranch.EditValue), true, cmbBranch.EditValue, null);
            ShowItemsForm(obj);
        }

        private void btnViewDifferences_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBranch.EditValue == null)
                    return;
                SplashScreenManager.ShowForm(null, typeof(frmProgress), true, true, false);
                frmViewSCItems obj =
                    new frmViewSCItems(countingRepository.GetStockCountingDiff(cmbBranch.EditValue), false, cmbBranch.EditValue, null,true);
                SplashScreenManager.CloseForm();
                ShowItemsForm(obj);
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            new frmAcceptCounting(cmbBranch.Text, cmbBranch.EditValue).ShowDialog();
            (ParentForm as frmReportPlaceHolder)?.btnSearch_Click(null, null);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BRANCHID", cmbBranch.EditValue }
            };
            return GetReportData("USP_R_STOCKCOUNTING", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            switch (buttonText)
            {
                case "View":
                    frmViewItems obj =
                        new frmViewItems(countingRepository.GetStockCountingDetail(drFocusedRow["STOCKCOUNTINGID"]), cmbBranch.EditValue,
                        Convert.ToString(drFocusedRow["STATUS"]) == "Draft" || Convert.ToString(drFocusedRow["STATUS"]) == "Submitted");
                    ShowItemsForm(obj);
                    break;
                case "Discard":
                    countingRepository.DiscardStockCounting(drFocusedRow["STOCKCOUNTINGID"], Utility.UserID);
                    new CloudRepository().DiscardCounting(new StockCounting() { STOCKCOUNTINGID = drFocusedRow["STOCKCOUNTINGID"] , UserID = Utility.UserID });
                    (ParentForm as frmReportPlaceHolder)?.btnSearch_Click(null, null);
                    break;
            }
        }

        private void btnSKUData_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            DataTable dt = null;
            try
            {
                SplashScreenManager.ShowForm(null, typeof(frmProgress), true, true, false);
                SplashScreenManager.Default.SetWaitFormDescription("Loading...");
                dt = countingRepository.GetCountingData(cmbBranch.EditValue);
                SplashScreenManager.CloseForm();

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
            }
            frmCountingData obj = new frmCountingData(dt, cmbBranch.EditValue);
            obj.ShowDialog();
        }

        private void btnMRPData_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            DataTable dt = null;
            try
            {
                SplashScreenManager.ShowForm(null, typeof(frmProgress), true, true, false);
                SplashScreenManager.Default.SetWaitFormDescription("Loading...");
                dt = countingRepository.GetCountingData_MRP(cmbBranch.EditValue);
                SplashScreenManager.CloseForm();

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
            }
            frmCountingData obj = new frmCountingData(dt, cmbBranch.EditValue,true);
            obj.ShowDialog();
        }

        private void btnLocationData_Click(object sender, EventArgs e)
        {
            if (cmbBranch.EditValue == null)
                return;
            DataTable dt = null;
            try
            {
                SplashScreenManager.ShowForm(null, typeof(frmProgress), true, true, false);
                SplashScreenManager.Default.SetWaitFormDescription("Loading...");
                dt = countingRepository.getCountingData_Location(cmbBranch.EditValue);
                SplashScreenManager.CloseForm();

            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseForm();
            }
            frmCountingData_Location obj = new frmCountingData_Location(dt, cmbBranch.EditValue);
            obj.ShowDialog();
        }
    }
}
