using DevExpress.Pdf.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraRichEdit.Export.Html;
using DevExpress.XtraRichEdit.Layout;
using NSRetailPOS.Data;
using NSRetailPOS.Reports;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmBranchRefund : XtraForm
    {
        object BRefundID = null;
        object BRefundNumber = null;
        DataRow drSelectedPrice;
        bool isItemScanned;
        bool isOpenItem;
        bool isEventCall;
        private int SNo = 1;
        DataTable dtRefund = null;
        public frmBranchRefund()
        {
            InitializeComponent();
            Utility.SetGridFormatting(gvRefund);
            Utility.SetGridFormatting(sluItemCodeView);
        }

        private void InitialLoad()
        {
            DataSet dSInitialData = new RefundRepository().GetInitialLoad(Utility.loginInfo.UserID, Utility.branchInfo.BranchID);
            if (dSInitialData != null && dSInitialData.Tables.Count > 0)
            {
                if (dSInitialData.Tables[0].Rows.Count > 0)
                {
                    BRefundID = dSInitialData.Tables[0].Rows[0]["BREFUNDID"];
                    BRefundNumber = dSInitialData.Tables[0].Rows[0]["BREFUNDNUMBER"];
                    cmbCategory.EditValue = dSInitialData.Tables[0].Rows[0]["CATEGORYID"];
                    this.Text = $"Branch Refund-{BRefundID}";
                    cmbCategory.Enabled = false;
                }
                else
                    cmbCategory.Enabled = true;
                dtRefund = dSInitialData.Tables[1].Copy();
                gcRefund.DataSource = dtRefund;
                SNo = dtRefund.Rows.Count + 1;
            }
        }

        private void frmBranchRefund_Load(object sender, EventArgs e)
        {
            cmbRFR.Properties.DataSource = new RefundRepository().GETRFR();
            cmbRFR.Properties.DisplayMember = "REASONNAME";
            cmbRFR.Properties.ValueMember = "REASONID";

            cmbCategory.Properties.DataSource = new ItemRepository().GetCategory();
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
            cmbCategory.Properties.ValueMember = "CATEGORYID";

            txtQuantity.ConfirmLargeNumber();
            txtWeightInKgs.ConfirmLargeNumber();

            InitialLoad();
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (sluItemCode.EditValue == null) { return; }
            int rowHandle = sluItemCodeView.LocateByValue("ITEMCODEID", sluItemCode.EditValue);
            txtItemCode.EditValue = sluItemCodeView.GetRowCellValue(rowHandle, "ITEMCODE");
            isOpenItem = Convert.ToBoolean(sluItemCodeView.GetRowCellValue(rowHandle, "ISOPENITEM"));
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
            txtQuantity.EditValue = 1;
            if (isOpenItem)
            {
                txtWeightInKgs.Enabled = true;
                txtQuantity.EditValue = 1;
                txtQuantity.Enabled = false;
            }
            else
            {
                txtWeightInKgs.EditValue = "0.00";
                txtWeightInKgs.Enabled = false;
                txtQuantity.Enabled = true;
            }
            if (!isItemScanned)
            {
                if (isOpenItem)
                    txtWeightInKgs.Focus();
                else
                    txtQuantity.Focus();
            }
        }

        private void ClearItemData(bool focusItemCode = true)
        {
            txtItemCode.EditValue = null;
            sluItemCode.EditValue = null;
            txtMRP.EditValue = null;
            txtQuantity.EditValue = 1;
            txtWeightInKgs.EditValue = 0.00;
            txtDescription.EditValue = null;

            if (focusItemCode)
                txtItemCode.Focus();
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            int rowHandle = sluItemCodeView.LocateByValue("ITEMCODE", txtItemCode.EditValue);

            if (rowHandle < 0)
            {
                List<int> rowHandles = sluItemCodeView.LocateAllRowsByValue("SKUCODE", txtItemCode.EditValue);
                if (rowHandles.Count == 1)
                {
                    rowHandle = rowHandles.First();
                }
                else if (rowHandles.Count > 1)
                {
                    sluItemCode.ShowPopup();
                    sluItemCodeView.FindFilterText = txtItemCode.EditValue.ToString();
                    isItemScanned = true;
                }
            }

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
            object BRDetailID = gvRefund.GetFocusedRowCellValue("BREFUNDDETAILID");
            SNo = Convert.ToInt32(gvRefund.GetFocusedRowCellValue("SNO"));
            DataTable dtSNos = new DataTable();
            dtSNos.Columns.Add("BREFUNDDETAILID", typeof(int));
            dtSNos.Columns.Add("SNO", typeof(int));

            for (int curRowHandle = gvRefund.FocusedRowHandle - 1; curRowHandle >= 0; curRowHandle--)
            {
                gvRefund.SetRowCellValue(curRowHandle, "SNO", SNo);
                dtSNos.Rows.Add(gvRefund.GetRowCellValue(curRowHandle, "BREFUNDDETAILID"), SNo);
                SNo++;
            }

            new RefundRepository().DeleteBRefundDetail(BRDetailID, Utility.loginInfo.UserID, dtSNos);
            gvRefund.DeleteRow(gvRefund.FocusedRowHandle);
            txtItemCode.Focus();
        }
        
        private void txtQuantity_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (sluItemCode.EditValue == null || drSelectedPrice == null || txtQuantity.EditValue == null
                || txtQuantity.EditValue.Equals(0))
            {
                txtItemCode.Focus();
                return;
            }

            if (!dxValidationProvider1.Validate())
                return;

            gvRefund.GridControl.BindingContext = new BindingContext();
            gvRefund.GridControl.DataSource = dtRefund;
            isEventCall = true;

            int rowHandle = Getrowhandle();
            if (rowHandle < 0)
            {
                gvRefund.AddNewRow();
            }
            else
            {
                int newQuantity = Convert.ToInt32(txtQuantity.EditValue) +
                    Convert.ToInt32(gvRefund.GetRowCellValue(rowHandle, "QUANTITY"));
                if (newQuantity > 0)
                {
                    gvRefund.SetRowCellValue(rowHandle, "QUANTITY", newQuantity);
                }
            }
            isEventCall = false;
            gvRefund.GridControl.BindingContext = new BindingContext();
            gvRefund.GridControl.DataSource = dtRefund;

            rowHandle = Getrowhandle();
            if (rowHandle >= 0)
            {
                if (!int.TryParse(Convert.ToString(BRefundID), out int id) || id <= 0)
                    SaveBRefund();
                SaveRefundDetail(rowHandle);
            }
            ClearItemData();
            gvRefund.FocusedRowHandle = rowHandle;
        }

        private int Getrowhandle()
        {
            int rowHandle = -1;  
            IList source = (IList)ListBindingHelper.GetList(gcRefund.DataSource, gcRefund.DataMember);
            PropertyDescriptorCollection coll = ListBindingHelper.GetListItemProperties(gcRefund.DataSource);
            PropertyDescriptor desc1 = coll["ITEMPRICEID"];
            PropertyDescriptor desc2 = coll["REASONID"];
            PropertyDescriptor desc3 = coll["TRAYNUMBER"];
            foreach (object row in source)
            {
                object val1 = desc1.GetValue(row);
                object val2 = desc2.GetValue(row);
                object val3 = desc3.GetValue(row);
                if (val1.Equals(drSelectedPrice["ITEMPRICEID"]) && val2.Equals(cmbRFR.EditValue) && val3.Equals(Convert.ToInt32(txtTrayNumber.EditValue)))
                {
                    rowHandle = gvRefund.GetRowHandle(source.IndexOf(row));
                    break;
                }
            }
            return rowHandle;
        }

        private void gvRefund_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvRefund.SetRowCellValue(e.RowHandle, "BREFUNDDETAILID", -1);
            gvRefund.SetRowCellValue(e.RowHandle, "BREFUNDID", BRefundID);
            gvRefund.SetRowCellValue(e.RowHandle, "ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            gvRefund.SetRowCellValue(e.RowHandle, "SNO", SNo++);
            gvRefund.SetRowCellValue(e.RowHandle, "ITEMNAME", sluItemCode.Text);
            gvRefund.SetRowCellValue(e.RowHandle, "ITEMCODE", txtItemCode.EditValue);
            gvRefund.SetRowCellValue(e.RowHandle, "MRP", drSelectedPrice["MRP"]);
            gvRefund.SetRowCellValue(e.RowHandle, "SALEPRICE", drSelectedPrice["SALEPRICE"]);
            gvRefund.SetRowCellValue(e.RowHandle, "QUANTITY", txtQuantity.EditValue);
            gvRefund.SetRowCellValue(e.RowHandle, "WEIGHTINKGS", txtWeightInKgs.EditValue);
            gvRefund.SetRowCellValue(e.RowHandle, "TRAYNUMBER", txtTrayNumber.EditValue);
            gvRefund.SetRowCellValue(e.RowHandle, "REASONID", cmbRFR.EditValue);
            gvRefund.SetRowCellValue(e.RowHandle, "REASONNAME", cmbRFR.Text);
            gvRefund.SetRowCellValue(e.RowHandle, "REFUNDDESCRIPTION", txtDescription.Text);
        }

        private void frmBranchRefund_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                sluItemCode.Focus();
            }
            else if (e.KeyCode == Keys.F1)
            {
                btnSave_Click(null, null);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvBilling_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "QUANTITY" || isEventCall) return;
            SaveRefundDetail(e.RowHandle);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (gvRefund.RowCount == 0 ||
                XtraMessageBox.Show("Are you sure to continue?", "Confirm!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            object NewBRefundNumber = new RefundRepository().FinishBRefund(BRefundID);
            rptBRefund rpt = new rptBRefund(dtRefund);
            rpt.Parameters["Address"].Value = Utility.branchInfo.BranchAddress;
            rpt.Parameters["BillDate"].Value = DateTime.Now;
            rpt.Parameters["BillNumber"].Value = NewBRefundNumber;
            rpt.Parameters["Phone"].Value = Utility.branchInfo.PhoneNumber;
            rpt.Parameters["UserName"].Value = Utility.loginInfo.UserFullName;
            rpt.Parameters["CounterName"].Value = Utility.branchInfo.BranchCounterName;
            Utility.PrintReport(rpt);
            Utility.PrintReport(rpt);
            Utility.PrintReport(rpt);
            this.BringToFront();
            XtraMessageBox.Show("Branch return sheet submitted successfully, re-open sheet for new return");
            this.Close();
        }

        private void txtQuantity_Enter(object sender, EventArgs e)
        {
            TextEdit textedit = sender as TextEdit;
            textedit.SelectAll();
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cmbCategory_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbCategory.EditValue != null)
            {
                sluItemCode.Properties.DataSource = new ItemRepository().GetItemCodes(cmbCategory.EditValue);
                sluItemCode.Properties.DisplayMember = "ITEMNAME";
                sluItemCode.Properties.ValueMember = "ITEMCODEID";

                sluItemCodeView.GridControl.BindingContext = new BindingContext();
                sluItemCodeView.GridControl.DataSource = sluItemCode.Properties.DataSource;
            }
        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            try
            {
                new RefundRepository().DiscardBRefund(BRefundID, Utility.loginInfo.UserID);
                XtraMessageBox.Show("Branch return sheet discarded successfully, re-open sheet for new return");
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void SaveBRefund()
        {
            DataTable dt = new RefundRepository().SaveBRefund(Utility.loginInfo.UserID, Utility.branchInfo.BranchID, cmbCategory.EditValue);
            if (dt != null && dt.Rows.Count > 0)
            {
                BRefundID = dt.Rows[0]["BREFUNDID"];
                BRefundNumber = dt.Rows[0]["BREFUNDNUMBER"];
                cmbCategory.EditValue = dt.Rows[0]["CATEGORYID"];
                this.Text = $"Branch Refund-{BRefundID}";
                cmbCategory.Enabled = false;
            }
            else
                throw new Exception("Error while saving BRefund");
        }

        private void SaveRefundDetail(int rowHandle)
        {
            DataRow drDetail = (gvRefund.GetRow(rowHandle) as DataRowView).Row;

            // refresh BREFUNDID just before going for save so that in case of first create,
            // BRefundID will be null, in quantity leave SaveRefund() will be called but the 
            // already initialized row will still have BRefundID as old value null
            drDetail["BREFUNDID"] = BRefundID;
            int BRefundDetailID = new RefundRepository().SaveBRefundDetail(drDetail);
            drDetail["BREFUNDDETAILID"] = BRefundDetailID;
        }
    }
}