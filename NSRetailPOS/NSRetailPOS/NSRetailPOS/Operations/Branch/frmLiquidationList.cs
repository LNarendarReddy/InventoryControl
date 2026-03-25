using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.Operations.Branch
{
    public partial class frmLiquidationList : XtraForm
    {
        public frmLiquidationList()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Liquidation liquidation = new()
            {
                Description = gvExpenses.GetFocusedRowCellValue("DESCRIPTION"),
                LiquidationID = gvExpenses.GetFocusedRowCellValue("LIQUIDATIONID"),
                ItemPriceID = gvExpenses.GetFocusedRowCellValue("ITEMPRICEID"),
                QtyOrWghtInKGs = gvExpenses.GetFocusedRowCellValue("QTYORWGHTINKGS"),
                ItemCodeID = gvExpenses.GetFocusedRowCellValue("ITEMCODEID"),
                ItemCode = gvExpenses.GetFocusedRowCellValue("ITEMCODE"),
                SKUCode = gvExpenses.GetFocusedRowCellValue("SKUCODE"),
                ItemName = gvExpenses.GetFocusedRowCellValue("ITEMNAME"),
                MRP = gvExpenses.GetFocusedRowCellValue("MRP"),
                SalePrice = gvExpenses.GetFocusedRowCellValue("SALEPRICE")
            };

            new frmLiquidation(liquidation).ShowDialog();

            if (liquidation.IsSave) RefreshList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Liquidation liquidation = new()
            {
                LiquidationID = gvExpenses.GetFocusedRowCellValue("LIQUIDATIONID"),
                Status = gvExpenses.GetFocusedRowCellValue("STATUS")
            };

            if (liquidation.Status.ToString() != "0" )
            {
                XtraMessageBox.Show("Only created status liquidations can be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (XtraMessageBox.Show("Are you sure to delete liquidation?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            try
            {
                new OperationsRepository().DeleteLiquidation(liquidation);
                XtraMessageBox.Show("Liquidation deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (liquidation.IsSave) RefreshList();
        }

        private void frmBranchExpenses_Load(object sender, EventArgs e)
        {
            dtpFromDate.EditValue = DateTime.Now;
            dtpToDate.EditValue = DateTime.Now;

            RefreshList();
        }

        private void btnAddExpense_Click(object sender, EventArgs e)
        {
            Liquidation liquidation = new Liquidation();
            new frmLiquidation(liquidation).ShowDialog();

            if (liquidation.IsSave) RefreshList();
        }

        private void RefreshList()
        {
            gcExpenses.DataSource = new ReportRepository().GetReportData("USP_R_LIQUIDATION",
                new Dictionary<string, object>
                {
                    { "BranchIDs", Utility.branchInfo.BranchID }
                    , { "FromDate", dtpFromDate.EditValue }
                    , { "ToDate", dtpToDate.EditValue }
                });
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void btnViewImage_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            new frmImageViewer(new OperationsRepository().GetBranchExpenseImage(gvExpenses.GetFocusedRowCellValue("BRANCHEXPENSEID"))).ShowDialog();
        }
    }
}