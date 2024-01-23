using System;
using System.Collections.Generic;
using System.Data;
using DataAccess;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using static DevExpress.Utils.Diagnostics.GUIResources;

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class frmStockSummary : DevExpress.XtraEditors.XtraForm
    {
        public frmStockSummary()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (luBranch.EditValue == null)
                return;

            IOverlaySplashScreenHandle handle = SplashScreenManager.ShowOverlayForm(this);
            try
            {
                DataSet ds = new ReportRepository().GetReportDataset("USP_R_STOCKSUMMARY1"
                , new Dictionary<string, object>
                {
                    {"BranchID", luBranch.EditValue },
                    {"ItemID", sluItem.EditValue }
                });

                ds.Relations.Add("drItemID", new[] { ds.Tables[0].Columns["ITEMID"]}, new[] { ds.Tables[1].Columns["ITEMID"]});
                gcSKU.DataSource = ds.Tables[0];
                gcSKU.ForceInitialize();
                gvSKU.Columns["ITEMID"].VisibleIndex = -1;
                foreach (GridColumn gc in gvSKU.Columns)
                {
                    if (gc.FieldName != "SKUCODE")
                        gc.OptionsColumn.AllowEdit = false;
                }

                GridView gvEAN = new GridView(gcSKU);
                gcSKU.LevelTree.Nodes.Add("drItemID", gvEAN);
                gvEAN.ViewCaption = "EAN";
                gvEAN.PopulateColumns(ds.Tables[1]);
                gvEAN.OptionsCustomization.AllowColumnResizing = false;
                gvEAN.OptionsCustomization.AllowFilter = false;
                gvEAN.OptionsCustomization.AllowGroup = false;
                gvEAN.OptionsCustomization.AllowSort = false;
                gvEAN.OptionsFind.AllowFindPanel = false;
                gvEAN.OptionsView.ShowGroupPanel = false;
                gvEAN.OptionsView.ShowIndicator = false;
                gvEAN.Columns["ITEMID"].VisibleIndex = -1;
                gvEAN.Columns["ITEMCODEID"].VisibleIndex = -1;
                foreach (GridColumn gc in gvEAN.Columns)
                {
                    if (gc.FieldName != "ITEMCODE")
                        gc.OptionsColumn.AllowEdit = false;
                }
            }
            catch (Exception ex)
            {
                SplashScreenManager.CloseOverlayForm(handle);
                ErrorManagement.ErrorMgmt.ShowError(ex);
                ErrorManagement.ErrorMgmt.Errorlog.Error(ex);
            }
            SplashScreenManager.CloseOverlayForm(handle);
        }

        private void frmStockSummary_Load(object sender, EventArgs e)
        {
            luBranch.Properties.DataSource = Utility.GetBranchList();
            luBranch.Properties.ValueMember = "BRANCHID";
            luBranch.Properties.DisplayMember = "BRANCHNAME";

            sluItem.Properties.DataSource = Utility.GetItemCodeList();
            sluItem.Properties.ValueMember = "ITEMID";
            sluItem.Properties.DisplayMember = "ITEMNAME";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnViewReport_Click(object sender, EventArgs e)
        {
            gcSKU.ShowRibbonPrintPreview();
        }

        private void gvSKU_DoubleClick(object sender, EventArgs e)
        {
            if (gvSKU.FocusedRowHandle < 0)
                return;
            GridView gView = gcSKU.MainView as GridView;
            gView.SetMasterRowExpanded(gvSKU.FocusedRowHandle, !gView.GetMasterRowExpanded(gvSKU.FocusedRowHandle));
        }
    }
}