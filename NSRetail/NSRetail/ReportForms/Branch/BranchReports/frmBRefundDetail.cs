using DataAccess;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.Design;
using DevExpress.XtraReports.UI;
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
    public partial class frmBRefundDetail : DevExpress.XtraEditors.XtraForm
    {
        object CounterID = null, BRefundID = null;
        public bool IsSave = false;
        SupplierRepository SupplierRepository = new SupplierRepository();
        public frmBRefundDetail(DataTable dtItems,object _CounterID,object _BRefundID, object CategoryID, bool IsAccepted = false)
        {
            InitializeComponent();
            btnSave.Enabled = !IsAccepted;
            gvItems.OptionsBehavior.Editable = !IsAccepted;
            gcItems.DataSource = dtItems;
            CounterID = _CounterID;
            BRefundID = _BRefundID;
            gcSupplier.Visible = gcCPWOT.Visible = gcCPWT.Visible = 
                CategoryID.Equals(3) || CategoryID.Equals(4) || CategoryID.Equals(13);
            gvItems.PopupMenuShowing += gvItems_PopupMenuShowing;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBRefundDetail_Load(object sender, EventArgs e)
        {
            cmbReason.DataSource = SupplierRepository.GetReason();
            cmbReason.ValueMember = "REASONID";
            cmbReason.DisplayMember = "REASONNAME";

            cmbSupplier.DataSource = SupplierRepository.GetSupplier();
            cmbSupplier.ValueMember = "DEALERID";
            cmbSupplier.DisplayMember = "DEALERNAME";
        }

        private void gvItems_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (gvItems.FocusedColumn == gcReason)
                {
                    SupplierRepository.UpdateBRReason(
                        gvItems.GetRowCellValue(e.RowHandle, "BRDID"),
                        gvItems.GetRowCellValue(e.RowHandle, "REASONID"),
                        Utility.UserID);
                }
                else if (gvItems.FocusedColumn == gcSupplier)
                {
                    SupplierRepository.UpdateSupplierCostPrice(
                        gvItems.GetRowCellValue(e.RowHandle, "BRDID"),
                        gvItems.GetRowCellValue(e.RowHandle, "DEALERID"),
                        gvItems.GetRowCellValue(e.RowHandle, "ITEMCOSTPRICEID"),
                        Utility.UserID);
                }
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void gvItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                gvItems.MoveNext();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to accept refund sheet", "Confirmation!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                DataTable dt = (gcItems.DataSource as DataTable).Copy();
                dt.Columns.Remove("SNO");
                dt.Columns.Remove("ITEMCODE");
                dt.Columns.Remove("ITEMNAME");
                dt.Columns.Remove("MRP");
                dt.Columns.Remove("SALEPRICE");
                dt.Columns.Remove("DELETEDDATE");
                dt.Columns.Remove("COSTPRICEWOT");
                dt.Columns.Remove("COSTPRICEWT");
                new POSRepository().AcceptBRefund(CounterID, BRefundID, Utility.UserID, dt);
                IsSave = true;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
                ErrorManagement.ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void gvItems_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (gvItems.FocusedRowHandle < 0)
                return;
            e.Menu.Items.Add(new DXMenuItem("View Report", new EventHandler(ViewReport_Click)));

            if (!btnSave.Enabled || gvItems.GetFocusedRowCellValue("QUANTITY").Equals(1))
                return;
            e.Menu.Items.Add(new DXMenuItem("Split Quantity", new EventHandler(SplitQuantity_Click)));
        }

        private void ViewReport_Click(object sender, EventArgs e)
        {
            gcItems.ShowRibbonPrintPreview();
        }

        private void SplitQuantity_Click(object sender, EventArgs e)
        {
            SplitQuantity splitQuantity = new SplitQuantity();
            splitQuantity.BaseQuantity = gvItems.GetFocusedRowCellValue("QUANTITY");
            splitQuantity.BaseReason = gvItems.GetFocusedRowCellValue("REASONID");
            frmSplitQuantity obj = new frmSplitQuantity(splitQuantity);
            obj.ShowDialog();
            if(splitQuantity._IsSave)
            {
                SupplierRepository.SplitBRQuantity(
                        gvItems.GetFocusedRowCellValue("BRDID"),
                        splitQuantity.BaseQuantity,splitQuantity.BaseReason,
                        splitQuantity.DevidedQuantity, splitQuantity.DevidedReason,
                        Utility.UserID, gvItems.RowCount + 1);

                gcItems.DataSource = new POSRepository().GetBRefundDetail(BRefundID, CounterID);
            }
        }
    }
}