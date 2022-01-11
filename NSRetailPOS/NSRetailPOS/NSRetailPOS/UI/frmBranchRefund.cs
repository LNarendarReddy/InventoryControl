using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using NSRetailPOS.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmBranchRefund : DevExpress.XtraEditors.XtraForm
    {
        object BRefundID = null;
        object BRefundNumber = null;
        DataRow drSelectedPrice;
        bool isItemScanned;
        private int SNo = 1;
        DataTable dtRefund = null;  
        public frmBranchRefund()
        {
            InitializeComponent();
        }
        private void InitialLoad()
        {
            DataSet dSInitialData = new RefundRepository().GetInitialLoad(Utility.logininfo.UserID, Utility.branchinfo.BranchID);
            if (dSInitialData != null && dSInitialData.Tables.Count > 0)
            {
                BRefundID = dSInitialData.Tables[0].Rows[0]["BREFUNDID"];
                BRefundNumber = dSInitialData.Tables[0].Rows[0]["BREFUNDNUMBER"];
                this.Text = "Branch Refund" + "-" + BRefundNumber;
                dtRefund = dSInitialData.Tables[1].Copy();
                gcBilling.DataSource = dtRefund;
            }
        }
        private void frmBranchRefund_Load(object sender, EventArgs e)
        {
            sluItemCode.Properties.DataSource = new ItemRepository().GetItemCodes();
            sluItemCode.Properties.DisplayMember = "ITEMNAME";
            sluItemCode.Properties.ValueMember = "ITEMCODEID";

            sluItemCodeView.GridControl.BindingContext = new BindingContext();
            sluItemCodeView.GridControl.DataSource = sluItemCode.Properties.DataSource;
            InitialLoad();
        }
        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (sluItemCode.EditValue == null)
            {
                return;
            }

            txtItemCode.EditValue = sluItemCodeView.GetRowCellValue(sluItemCodeView.LocateByValue("ITEMCODEID",
                sluItemCode.EditValue), "ITEMCODE");
            DataTable dtPrices = new ItemRepository().GetMRPList(sluItemCode.EditValue);
            if (dtPrices.Rows.Count > 1)
            {
                frmMRPSelection mRPSelection = new frmMRPSelection(dtPrices, txtItemCode.EditValue, sluItemCode.Text)
                { StartPosition = FormStartPosition.CenterScreen };
                mRPSelection.ShowDialog();
                if (!mRPSelection._IsSave)
                {
                    ClearItemData();
                    return;
                }

                drSelectedPrice = (mRPSelection.drSelected as DataRowView)?.Row;
            }
            else if (dtPrices.Rows.Count == 1)
            {
                drSelectedPrice = dtPrices.Rows[0];
            }

            if (drSelectedPrice == null)
            {
                return;
            }

            txtMRP.EditValue = drSelectedPrice["MRP"];
            txtSalePrice.EditValue = drSelectedPrice["SALEPRICE"];
            txtQuantity.EditValue = 1;

            if (!isItemScanned)
                txtQuantity.Focus();
            txtQuantity.SelectAll();
        }
        private void ClearItemData(bool focusItemCode = true)
        {
            txtItemCode.EditValue = null;
            sluItemCode.EditValue = null;
            txtMRP.EditValue = null;
            txtSalePrice.EditValue = null;
            txtQuantity.EditValue = 1;

            if (focusItemCode)
                txtItemCode.Focus();
        }
        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            int rowHandle = sluItemCodeView.LocateByValue("ITEMCODE", txtItemCode.EditValue);
            if (rowHandle >= 0)
            {
                isItemScanned = true;
                sluItemCode.EditValue = null;
                sluItemCode.EditValue = sluItemCodeView.GetRowCellValue(rowHandle, "ITEMCODEID");
            }
            else
            {
                ClearItemData(false);
            }

            isItemScanned = false;
        }
        private void txtItemCode_Enter(object sender, EventArgs e)
        {
            txtItemCode.SelectAll();
        }
        private void txtItemCode_Click(object sender, EventArgs e)
        {
            txtItemCode.SelectAll();
        }
        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            object BRDetailID = gvBilling.GetFocusedRowCellValue("BREFUNDDETAILID");
            SNo = Convert.ToInt32(gvBilling.GetFocusedRowCellValue("SNO"));
            DataTable dtSNos = new DataTable();
            dtSNos.Columns.Add("BREFUNDDETAILID", typeof(int));
            dtSNos.Columns.Add("SNO", typeof(int));

            for (int curRowHandle = gvBilling.FocusedRowHandle - 1; curRowHandle >= 0; curRowHandle--)
            {
                gvBilling.SetRowCellValue(curRowHandle, "SNO", SNo);
                dtSNos.Rows.Add(gvBilling.GetRowCellValue(curRowHandle, "BREFUNDDETAILID"), SNo);
                SNo++;
            }

            new RefundRepository().DeleteBRefundDetail(BRDetailID, Utility.logininfo.UserID, dtSNos);
            gvBilling.DeleteRow(gvBilling.FocusedRowHandle);
            txtItemCode.Focus();
        }
        private void txtQuantity_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (sluItemCode.EditValue == null || drSelectedPrice == null || txtQuantity.EditValue == null || txtQuantity.EditValue.Equals(0))
            {
                txtItemCode.Focus();
                return;
            }

            gvBilling.GridControl.BindingContext = new BindingContext();
            gvBilling.GridControl.DataSource = dtRefund;

            int rowHandle = gvBilling.LocateByValue("ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            if (rowHandle < 0)
            {
                gvBilling.AddNewRow();
            }
            else
            {
                int newQuantity = Convert.ToInt32(txtQuantity.EditValue) + Convert.ToInt32(gvBilling.GetRowCellValue(rowHandle, "QUANTITY"));
                if (newQuantity > 0)
                {
                    gvBilling.SetRowCellValue(rowHandle, "QUANTITY", newQuantity);
                }
            }

            gvBilling.GridControl.BindingContext = new BindingContext();
            gvBilling.GridControl.DataSource = dtRefund;

            rowHandle = gvBilling.LocateByValue("ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            if (rowHandle >= 0)
            {
                SaveRefundDetail(rowHandle);
            }
            ClearItemData();
            gvBilling.FocusedRowHandle = rowHandle;
        }
        private void SaveRefundDetail(int rowHandle)
        {
            DataRow drDetail = (gvBilling.GetRow(rowHandle) as DataRowView).Row;
            int BRefundDetailID = new RefundRepository().SaveBRefundDetail(drDetail);
            drDetail["BREFUNDDETAILID"] = BRefundDetailID;
        }
        private void gvRefund_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvBilling.SetRowCellValue(e.RowHandle, "BREFUNDDETAILID", -1);
            gvBilling.SetRowCellValue(e.RowHandle, "BREFUNDID", BRefundID);
            gvBilling.SetRowCellValue(e.RowHandle, "ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            gvBilling.SetRowCellValue(e.RowHandle, "SNO", SNo++);
            gvBilling.SetRowCellValue(e.RowHandle, "ITEMNAME", sluItemCode.Text);
            gvBilling.SetRowCellValue(e.RowHandle, "ITEMCODE", txtItemCode.EditValue);
            gvBilling.SetRowCellValue(e.RowHandle, "MRP", drSelectedPrice["MRP"]);
            gvBilling.SetRowCellValue(e.RowHandle, "SALEPRICE", drSelectedPrice["SALEPRICE"]);
            gvBilling.SetRowCellValue(e.RowHandle, "QUANTITY", txtQuantity.EditValue);
            gvBilling.SetRowCellValue(e.RowHandle, "WEIGHTINKGS", 0.00);
        }
        private void frmBranchRefund_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                sluItemCode.Focus();
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void gvBilling_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "QUANTITY") return;
            SaveRefundDetail(e.RowHandle);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            new RefundRepository().FinishBRefund(BRefundID);
            rptBRefund rpt = new rptBRefund(dtRefund);
            rpt.Parameters["Address"].Value = Utility.branchinfo.BranchAddress;
            rpt.Parameters["BillDate"].Value = DateTime.Now;
            rpt.Parameters["BillNumber"].Value = BRefundNumber;
            rpt.Parameters["Phone"].Value = Utility.branchinfo.PhoneNumber;
            rpt.Parameters["UserName"].Value = Utility.logininfo.UserFullName;
            rpt.Print();
            InitialLoad();
        }
    }
}