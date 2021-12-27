using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using NSRetailPOS.Reports;
using NSRetailPOS.UI;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
//using System.Data.SQLite;

namespace NSRetailPOS
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private int daySequenceID;

        private int SNo = 1;

        BillingRepository billingRepository = new BillingRepository();
        ItemRepository itemRepository = new ItemRepository();

        Bill billObj;

        DataRow drSelectedPrice;

        BackgroundWorker bgSyncWorker = new BackgroundWorker();

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataSet dsInitialData = billingRepository.GetInitialLoad(Utility.logininfo.UserID, Utility.branchinfo.BranchCounterID);

            if (!int.TryParse(dsInitialData.Tables["DAYSEQUENCE"].Rows[0][0].ToString(), out daySequenceID))
            {
                XtraMessageBox.Show(dsInitialData.Tables["DAYSEQUENCE"].Rows[0][0].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            sluItemCode.Properties.DataSource = itemRepository.GetItemCodes();
            sluItemCode.Properties.DisplayMember = "ITEMNAME";
            sluItemCode.Properties.ValueMember = "ITEMCODEID";

            sluItemCodeView.GridControl.BindingContext = new BindingContext();
            sluItemCodeView.GridControl.DataSource = sluItemCode.Properties.DataSource;

            LoadBillData(dsInitialData);
            bgSyncWorker.WorkerReportsProgress = true;
            bgSyncWorker.DoWork += BgSyncWorker_DoWork;
            bgSyncWorker.ProgressChanged += BgSyncWorker_ProgressChanged;
            bgSyncWorker.RunWorkerAsync();
        }

        private void BgSyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            syncProgressBar.Text = e.UserState?.ToString();
        }

        private void BgSyncWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Utility.StartSync(bgSyncWorker);
            Thread.Sleep(5 * 60 * 1000);
            BgSyncWorker_DoWork(sender, e);

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
            gvBilling.GridControl.DataSource = billObj.dtBillDetails;

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
            gvBilling.GridControl.DataSource = billObj.dtBillDetails;

            rowHandle = gvBilling.LocateByValue("ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            if (rowHandle >= 0)
            {
                CalculateFields(rowHandle);
                SaveBillDetail(rowHandle);
            }
            ClearItemData();
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (sluItemCode.EditValue == null)
            {
                return;
            }

            txtItemCode.EditValue = sluItemCodeView.GetRowCellValue(sluItemCodeView.LocateByValue("ITEMCODEID", 
                sluItemCode.EditValue), "ITEMCODE");
            DataTable dtPrices = itemRepository.GetMRPList(sluItemCode.EditValue);
            if (dtPrices.Rows.Count > 1)
            {
                frmMRPSelection mRPSelection = new frmMRPSelection(dtPrices,txtItemCode.EditValue,sluItemCode.Text) 
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

            //txtItemName.EditValue = (sluItemCode.GetSelectedDataRow() as DataRowView)?.Row["ITEMNAME"];
            txtMRP.EditValue = drSelectedPrice["MRP"];
            txtSalePrice.EditValue = drSelectedPrice["SALEPRICE"];
            txtQuantity.EditValue = 1;

            if (chkSingleQuantity.Checked)
            {
                txtQuantity_KeyDown(txtQuantity, new KeyEventArgs(Keys.Enter));
            }
            else
            {
                //txtQuantity.Focus();
                txtQuantity.SelectAll();
            }

        }

        private void gvBilling_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvBilling.SetRowCellValue(e.RowHandle, "BILLDETAILID", -1);
            gvBilling.SetRowCellValue(e.RowHandle, "BILLID", billObj.BillID);
            gvBilling.SetRowCellValue(e.RowHandle, "ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            gvBilling.SetRowCellValue(e.RowHandle, "SNO", SNo++);
            gvBilling.SetRowCellValue(e.RowHandle, "ITEMNAME", sluItemCode.Text);
            gvBilling.SetRowCellValue(e.RowHandle, "ITEMCODE", txtItemCode.EditValue);
            gvBilling.SetRowCellValue(e.RowHandle, "MRP", drSelectedPrice["MRP"]);
            gvBilling.SetRowCellValue(e.RowHandle, "SALEPRICE", drSelectedPrice["SALEPRICE"]);
            gvBilling.SetRowCellValue(e.RowHandle, "GSTCODE", drSelectedPrice["GSTCODE"]);
            gvBilling.SetRowCellValue(e.RowHandle, "CGSTDESC", drSelectedPrice["CGST"]);
            gvBilling.SetRowCellValue(e.RowHandle, "SGSTDESC", drSelectedPrice["SGST"]);
            gvBilling.SetRowCellValue(e.RowHandle, "CESSDESC", drSelectedPrice["CESS"]);
            gvBilling.SetRowCellValue(e.RowHandle, "QUANTITY", txtQuantity.EditValue);
            gvBilling.SetRowCellValue(e.RowHandle, "WEIGHTINKGS", 0.00);
        }

        private void btnCloseBill_Click(object sender, EventArgs e)
        {
            if (billObj.dtBillDetails.Rows.Count == 0)
            {
                XtraMessageBox.Show("No items to bill", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmPayment paymentForm = new frmPayment(billObj);
            paymentForm.ShowDialog();
            if (!paymentForm.IsPaid) { return; }

            DataSet nextBillDetails = billingRepository.FinishBill(Utility.logininfo.UserID, daySequenceID, billObj);

            // use this object for printing
            Bill oldBillObj = billObj.Clone() as Bill;
            DataView dv = oldBillObj.dtMopValues.DefaultView;
            dv.RowFilter = "MOPVALUE > 0";
            rptBill rpt = new rptBill(oldBillObj.dtBillDetails, dv.ToTable());
            rpt.Parameters["GSTIN"].Value = "37AADFV6514H1Z2";
            rpt.Parameters["FSSAI"].Value = "10114004000548";
            rpt.Parameters["Address"].Value = Utility.branchinfo.BranchAddress;
            rpt.Parameters["BillDate"].Value = DateTime.Now;
            rpt.Parameters["BillNumber"].Value = oldBillObj.BillNumber;
            rpt.Parameters["BranchName"].Value = Utility.branchinfo.BranchName;
            rpt.Parameters["CounterName"].Value = Utility.branchinfo.BranchCounterName;
            rpt.Parameters["Phone"].Value = Utility.branchinfo.PhoneNumber;
            rpt.Parameters["UserName"].Value = Utility.logininfo.UserFullName;
            rpt.Print();

            LoadBillData(nextBillDetails);
        }

        private void CalculateFields(int rowHandle)
        {
            DataRow drBillDetail = (gvBilling.GetRow(rowHandle) as DataRowView).Row;
            decimal salePrice = Convert.ToDecimal(drBillDetail["SALEPRICE"])
                , MRP = Convert.ToDecimal(drBillDetail["MRP"])
                , cGSTPer = Convert.ToDecimal(drSelectedPrice["CGST"] ?? drBillDetail["CGST"])
                , sGSTPer = Convert.ToDecimal(drSelectedPrice["SGST"] ?? drBillDetail["SGST"])
                , iGSTPer = Convert.ToDecimal(drSelectedPrice["IGST"] ?? drBillDetail["IGST"])
                , cess = Convert.ToDecimal(drSelectedPrice["CESS"] ?? drBillDetail["CESS"])
                , billedAmount, cGSTValue, sGSTValue, iGSTValue, cessValue, totalGSTValue, Discount;

            int.TryParse(drBillDetail["QUANTITY"].ToString(), out int quantity);
            billedAmount = salePrice * quantity;
            cGSTValue = Math.Round((billedAmount * cGSTPer) / 100, 2);
            sGSTValue = Math.Round((billedAmount * sGSTPer) / 100, 2);
            iGSTValue = Math.Round((billedAmount * iGSTPer) / 100, 2);
            cessValue = Math.Round((billedAmount * cess) / 100, 2);

            totalGSTValue = cGSTValue + sGSTValue + iGSTValue + cessValue;
            Discount = Math.Round((MRP - salePrice) * quantity, 2);

            drBillDetail["CGST"] = cGSTValue;
            drBillDetail["SGST"] = sGSTValue;
            drBillDetail["IGST"] = iGSTValue;
            drBillDetail["CESS"] = cessValue;
            drBillDetail["GSTVALUE"] = totalGSTValue;
            drBillDetail["BILLEDAMOUNT"] = billedAmount;
            drBillDetail["DISCOUNT"] = Discount;

            UpdateSummary();
        }

        private void SaveBillDetail(int rowHandle)
        {
            DataRow drBillDetail = (gvBilling.GetRow(rowHandle) as DataRowView).Row;
            int billDetailID = billingRepository.SaveBillDetail(drBillDetail, Utility.logininfo.UserID);
            drBillDetail["BILLDETAILID"] = billDetailID;
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

        private void UpdateSummary()
        {
            billObj.Amount = gvBilling.Columns["BILLEDAMOUNT"].SummaryItem.SummaryValue;
            billObj.Quantity = gvBilling.Columns["QUANTITY"].SummaryItem.SummaryValue;
            //lblTotalBill.Text = billObj.Amount.ToString();
            //lblTotalItems.Text = billObj.Quantity.ToString();
        }

        private void LoadBillData(DataSet dsBillInfo)
        {
            billObj = Utility.GetBill(dsBillInfo);

            this.Text = "NSRetail - " + billObj.BillNumber.ToString();

            txtLastBilledAmount.Text = billObj.LastBilledAmount.ToString();
            txtLastBilledQuantity.Text = billObj.LastBilledQuantity.ToString();

            txtLastBilledAmount.Text = string.IsNullOrEmpty(txtLastBilledAmount.Text) ? "0.00" : txtLastBilledAmount.Text;
            txtLastBilledQuantity.Text = string.IsNullOrEmpty(txtLastBilledQuantity.Text) ? "0" : txtLastBilledQuantity.Text;

            gcBilling.DataSource = billObj.dtBillDetails;

            UpdateSummary();
            SNo = billObj.dtBillDetails.Rows.Count + 1;

            txtItemCode.Focus();
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {            
            int rowHandle = sluItemCodeView.LocateByValue("ITEMCODE", txtItemCode.EditValue);
            if (rowHandle >= 0)
            {
                sluItemCode.EditValue = null;
                sluItemCode.EditValue = sluItemCodeView.GetRowCellValue(rowHandle, "ITEMCODEID");
                //if (sluItemCode.EditValue != null)
                //    txtQuantity.Focus();
            }
            else
            {
                ClearItemData(false);
            }
        }

        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            if (billObj.dtBillDetails.Rows.Count == 0)
            {
                XtraMessageBox.Show("No items to draft", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataSet nextBillDetails = billingRepository.DraftBill(Utility.logininfo.UserID, daySequenceID, billObj.BillID);
            LoadBillData(nextBillDetails);
        }

        private void btnLoadDraftBill_Click(object sender, EventArgs e)
        {
            if (billObj.dtBillDetails.Rows.Count > 0)
            {
                DialogResult dialogResult = XtraMessageBox.Show("Pending items in the bill, Do you want to draft bill?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Cancel) return;

                btnSaveBill_Click(null, null);
            }

            frmDraftList draftListForm = new frmDraftList(daySequenceID);
            draftListForm.ShowDialog();
            if (draftListForm.SelectedDraftBillID > 0)
            {
                DataSet dsBillDetails = billingRepository.GetBill(daySequenceID, draftListForm.SelectedDraftBillID);
                LoadBillData(dsBillDetails);
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword obj = new frmChangePassword();
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterParent;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnLastBillPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsLastBillDetails = new BillingRepository().GetLastBill(daySequenceID, billObj.LastBillID);
                Bill LastBillObj = Utility.GetBill(dsLastBillDetails);
                rptBill rpt = new rptBill(LastBillObj.dtBillDetails, LastBillObj.dtMopValues);
                rpt.Parameters["GSTIN"].Value = "37AADFV6514H1Z2";
                rpt.Parameters["FSSAI"].Value = "10114004000548";
                rpt.Parameters["Address"].Value = Utility.branchinfo.BranchAddress;
                rpt.Parameters["BillDate"].Value = DateTime.Now;
                rpt.Parameters["BillNumber"].Value = LastBillObj.BillNumber;
                rpt.Parameters["BranchName"].Value = Utility.branchinfo.BranchName;
                rpt.Parameters["CounterName"].Value = Utility.branchinfo.BranchCounterName;
                rpt.Parameters["Phone"].Value = Utility.branchinfo.PhoneNumber;
                rpt.Parameters["UserName"].Value = Utility.logininfo.UserFullName;
                rpt.Print();
                txtItemCode.Focus();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
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
            object billDetailID = gvBilling.GetFocusedRowCellValue("BILLDETAILID");
            SNo = Convert.ToInt32(gvBilling.GetFocusedRowCellValue("SNO"));
            DataTable dtSNos = new DataTable();
            dtSNos.Columns.Add("BILLDETAILID", typeof(int));
            dtSNos.Columns.Add("SNO", typeof(int));

            for (int curRowHandle = gvBilling.FocusedRowHandle - 1; curRowHandle >= 0; curRowHandle--)
            {
                gvBilling.SetRowCellValue(curRowHandle, "SNO", SNo);
                dtSNos.Rows.Add(gvBilling.GetRowCellValue(curRowHandle, "BILLDETAILID"), SNo);
                SNo++;
            }

            billingRepository.DeleteBillDetail(billDetailID, Utility.logininfo.UserID, dtSNos);
            gvBilling.DeleteRow(gvBilling.FocusedRowHandle);
            UpdateSummary();
            txtItemCode.Focus();
        }

        private void syncProgressBar_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            //e.DisplayText = bgSyncReportText;
        }

        private void gvBilling_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "QUANTITY") return;

            CalculateFields(e.RowHandle);
            SaveBillDetail(e.RowHandle);
        }
    }
}