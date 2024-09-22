using DataAccess;
using DevExpress.Data.Helpers;
using DevExpress.Utils.Extensions;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using Entity;
using NSRetail;
using NSRetail.Utilities;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmItemVisualize : DevExpress.XtraEditors.XtraForm
    {
        int itemID;
        ItemCodeRepository itemCodeRepository = new ItemCodeRepository();
        BranchItemPrice branchItemPrice = null;
        public frmItemVisualize(object itemID)
        {
            this.itemID = Convert.ToInt32(itemID);
            InitializeComponent();
        }

        private void frmItemDrillDown_Load(object sender, EventArgs e)
        {
            DataSet dsItemVisualizer = new ItemCodeRepository().GetItemVisualizer(itemID);

            txtItemName.EditValue = dsItemVisualizer.Tables["ITEM"].Rows[0]["ITEMNAME"];
            txtSKUCode.EditValue = dsItemVisualizer.Tables["ITEM"].Rows[0]["SKUCODE"];

            gcItemPriceList.DataSource = dsItemVisualizer.Tables["ITEMPRICES"];
            gcStockSummary.DataSource = dsItemVisualizer.Tables["ITEMSTOCKSUMMARY"];
            gcBranchPrices.DataSource = dsItemVisualizer.Tables["BRANCHPRICES"];

            gvItemPrice_FocusedRowChanged(null, null);

            AccessUtility.SetStatusByAccess(btnAddNewPrice);
            AccessUtility.SetStatusByAccess(gcDelete);
            AccessUtility.SetStatusByAccess(gvBranchPrices);
        }

        private void frmItemVisualize_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void gvItemPrice_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gvItemPrice.FocusedRowHandle < 0)
                    return;
                gvStockSummary.Columns["ITEMCODE"].FilterInfo =
                    new ColumnFilterInfo($"[ITEMCODE] = '{gvItemPrice.GetFocusedRowCellValue("ITEMCODE")}'");
                gvBranchPrices.Columns["PARENTITEMPRICEID"].FilterInfo =
                    new ColumnFilterInfo($"[PARENTITEMPRICEID] = '{gvItemPrice.GetFocusedRowCellValue("ITEMPRICEID")}'");
                gcOffer.DataSource =
                    itemCodeRepository.GetOffers(gvItemPrice.GetFocusedRowCellValue("ITEMPRICEID"));
                btnAddNewPrice.Enabled = string.IsNullOrEmpty(Convert.ToString(gvItemPrice.GetFocusedRowCellValue("IPDELETEDBY")));
                AccessUtility.SetStatusByAccess(btnAddNewPrice);
            }
            catch (Exception ex) { }
        }

        private void btnDeleteBranchPrice_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (gvBranchPrices.FocusedRowHandle < 0)
                    return;
                itemCodeRepository.DeleteItemPrice(gvBranchPrices.GetFocusedRowCellValue("ITEMPRICEID"), Utility.UserID);
                gvBranchPrices.DeleteRow(gvBranchPrices.FocusedRowHandle);
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void gvBranchPrices_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (gvBranchPrices.FocusedRowHandle < 0 || gvBranchPrices.FocusedColumn != gcsaleprice)
                    return;
                branchItemPrice = new BranchItemPrice();
                branchItemPrice.ITEMPRICEID = gvBranchPrices.GetFocusedRowCellValue("ITEMPRICEID");
                branchItemPrice.PARENTITEMPRICEID = gvBranchPrices.GetFocusedRowCellValue("PARENTITEMPRICEID");
                branchItemPrice.SALEPRICE = gvBranchPrices.GetFocusedRowCellValue("SALEPRICE");
                branchItemPrice.BRANCHID = gvBranchPrices.GetFocusedRowCellValue("BRANCHID");
                branchItemPrice.UserID = Utility.UserID;
                itemCodeRepository.SaveBranchItemPrice(branchItemPrice);
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void btnAddNewPrice_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvItemPrice.FocusedRowHandle < 0)
                    return;
                branchItemPrice = new BranchItemPrice();
                branchItemPrice.ITEMPRICEID = 0;
                branchItemPrice.PARENTITEMPRICEID = gvItemPrice.GetFocusedRowCellValue("ITEMPRICEID");
                branchItemPrice.UserID = Utility.UserID;
                frmAddBranchPrice obj = new frmAddBranchPrice(branchItemPrice);
                obj.ShowInTaskbar = false;
                obj.StartPosition = FormStartPosition.CenterScreen;
                obj.ShowDialog();
                if (branchItemPrice.IsSave)
                {
                    itemCodeRepository.SaveBranchItemPrice(branchItemPrice);
                    gvBranchPrices.AddNewRow();
                }
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void gvBranchPrices_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                view.SetRowCellValue(e.RowHandle, "ITEMPRICEID", branchItemPrice.ITEMPRICEID);
                view.SetRowCellValue(e.RowHandle, "PARENTITEMPRICEID", branchItemPrice.PARENTITEMPRICEID);
                view.SetRowCellValue(e.RowHandle, "BRANCHNAME", branchItemPrice.BRANCHNAME);
                view.SetRowCellValue(e.RowHandle, "SALEPRICE", branchItemPrice.SALEPRICE);
                view.SetRowCellValue(e.RowHandle, "BRANCHID", branchItemPrice.BRANCHID);
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }
    }
}
