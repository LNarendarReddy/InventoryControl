using System;
using System.Data;
using DataAccess;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace NSRetail.Stock
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

            DataSet ds = new StockRepository().GetStockSummary1(luBranch.EditValue, sluItem.EditValue);
            
            DataColumn keyColumn = ds.Tables[0].Columns["ITEMID"];
            DataColumn foreignKeyColumn = ds.Tables[1].Columns["ITEMID"];
            ds.Relations.Add("drItemID", keyColumn, foreignKeyColumn);
            gcSKU.DataSource = ds.Tables[0];
            gcSKU.ForceInitialize();
            gvSKU.Columns["ITEMID"].VisibleIndex = -1;
            foreach(GridColumn gc in gvSKU.Columns)
            {
                if(gc.FieldName != "SKUCODE")
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

        private void frmStockSummary_Load(object sender, EventArgs e)
        {
            luBranch.Properties.DataSource = new MasterRepository().GetBranch(false);
            luBranch.Properties.ValueMember = "BRANCHID";
            luBranch.Properties.DisplayMember = "BRANCHNAME";
            luBranch.EditValue = 0;

            sluItem.Properties.DataSource = Utility.GetItemSKUList();
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