using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using NSRetailPOS.Reports;
using NSRetailPOS.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace NSRetailPOS
{
    public partial class frmMain : XtraForm
    {
        private int daySequenceID;

        private int SNo = 1;

        BillingRepository billingRepository = new BillingRepository();
        ItemRepository itemRepository = new ItemRepository();

        Bill billObj;

        DataRow drSelectedPrice;

        BackgroundWorker bgSyncWorker = new BackgroundWorker();

        bool isItemScanned;
        bool isOpenItem = false;
        bool isEventCall = false;
        private const string noOfferText = "Offer : None   ";
        private const string noDealText = "Deal : None";

        public frmMain()
        {
            InitializeComponent();
            Utility.SetGridFormatting(gvBilling);
            Utility.SetGridFormatting(sluItemCodeView);

            gvBilling.Columns["OFFERTYPECODE"].AppearanceCell.ForeColor = Color.YellowGreen;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblUserinfo.Text = $"Loggedin User : {Utility.loginInfo.UserFullName}    Role : {Utility.loginInfo.RoleName}    ";
            lblVersionInfo.Text = $"Application Version 1.2.2 (03-05-2022)";
            btnCRWithoutBill.Enabled = Utility.loginInfo.RoleName.Equals("Store Admin");
            txtSplDiscPer.Enabled = Utility.loginInfo.RoleName.Equals("Store Admin");
            btnApplyDiscount.Enabled = Utility.loginInfo.RoleName.Equals("Store Admin");
            btnDayClosure.Enabled = Utility.loginInfo.RoleName.Equals("Store Manager");
            btnStockIn.Enabled = Utility.loginInfo.RoleName.Equals("Store Manager");
            btnBranchRefund.Enabled = Utility.loginInfo.RoleName.Equals("Store Manager");

            DataSet dsInitialData = billingRepository.GetInitialLoad(Utility.loginInfo.UserID, Utility.branchInfo.BranchCounterID);

            if (!int.TryParse(dsInitialData.Tables["DAYSEQUENCE"].Rows[0][0].ToString(), out daySequenceID))
            {
                XtraMessageBox.Show(dsInitialData.Tables["DAYSEQUENCE"].Rows[0][0].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DisableBilling();
                return;
            }

            Utility.ItemOrCodeChanged += Utility_ItemOrCodeChanged;
            LoadItemCodes();
            LoadBillData(dsInitialData);

            lblOffer.Text = noOfferText;
            lblDeal.Text = noDealText;
            HighlightOffer();

            bgSyncWorker.WorkerReportsProgress = true;
            bgSyncWorker.DoWork += BgSyncWorker_DoWork;
            bgSyncWorker.ProgressChanged += BgSyncWorker_ProgressChanged;
            bgSyncWorker.RunWorkerAsync();
        }

        private void Utility_ItemOrCodeChanged(object sender, EventArgs e)
        {
            LoadItemCodes();
        }

        private void BgSyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblProgressText.Text = e.UserState?.ToString();
        }

        private void BgSyncWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Utility.StartSync(bgSyncWorker);
            Thread.Sleep(30 * 60 * 1000);
            BgSyncWorker_DoWork(sender, e);
        }

        private void txtQuantity_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            if (sluItemCode.EditValue == null || drSelectedPrice == null ||
                txtQuantity.EditValue == null || txtQuantity.EditValue.Equals(0))
            {
                txtItemCode.Focus();
                return;
            }

            SaveBillDetail(drSelectedPrice["ITEMPRICEID"], txtQuantity.EditValue, txtWeightInKgs.EditValue, 0);
            ClearItemData();
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (sluItemCode.EditValue == null)
            {
                return;
            }
            int rowHandle = sluItemCodeView.LocateByValue("ITEMCODEID", sluItemCode.EditValue);
            txtItemCode.EditValue = sluItemCodeView.GetRowCellValue(rowHandle, "ITEMCODE");
            isOpenItem = Convert.ToBoolean(sluItemCodeView.GetRowCellValue(rowHandle, "ISOPENITEM"));
            if (!isOpenItem)
                txtWeightInKgs.EditValue = 0.00;
            DataTable dtPrices = itemRepository.GetMRPList(sluItemCode.EditValue);
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

            DataTable dtOffers = itemRepository.GetOfferList(drSelectedPrice["ITEMPRICEID"]);

            lblOffer.Text = Convert.ToString(dtOffers.Rows[0]["OFFERTYPE"]);
            lblDeal.Text = Convert.ToString(dtOffers.Rows[0]["DEALTYPE"]);

            HighlightOffer();

            //txtItemName.EditValue = (sluItemCode.GetSelectedDataRow() as DataRowView)?.Row["ITEMNAME"];
            txtMRP.EditValue = drSelectedPrice["MRP"];
            txtSalePrice.EditValue = drSelectedPrice["SALEPRICE"];
            txtQuantity.EditValue = 1;
            gcBilling.Refresh();

            if (chkSingleQuantity.Checked)
            {
                txtQuantity_KeyDown(txtQuantity, new KeyEventArgs(Keys.Enter));
            }
            else
            {
                if (!isItemScanned)
                    txtQuantity.Focus();
                txtQuantity.SelectAll();
            }
        }

        private void btnCloseBill_Click(object sender, EventArgs e)
        {
            if (billObj.dtBillDetails.Rows.Count == 0)
            {
                XtraMessageBox.Show("No items to bill", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //frmPayment paymentForm = new frmPayment(billObj);
            //paymentForm.ShowDialog();
            //if (!paymentForm.IsPaid) { return; }

            frmPrePayment paymentForm = new frmPrePayment(billObj);
            paymentForm.ShowDialog();
            if (!paymentForm.IsPaid) { return; }

            DataSet nextBillDetails = null;
            try
            {
                nextBillDetails = billingRepository.FinishBill(Utility.loginInfo.UserID, daySequenceID, billObj);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // use this object for printing
            Bill oldBillObj = billObj.Clone() as Bill;
            DataView dv = oldBillObj.dtMopValues.DefaultView;
            dv.RowFilter = "MOPVALUE > 0";
            rptBill rpt = new rptBill(oldBillObj.dtBillDetails, dv.ToTable());
            rpt.Parameters["GSTIN"].Value = "37AADFV6514H1Z2";
            rpt.Parameters["FSSAI"].Value = "10114004000548";
            rpt.Parameters["Address"].Value = Utility.branchInfo.BranchAddress;
            rpt.Parameters["BillDate"].Value = DateTime.Now;
            rpt.Parameters["BillNumber"].Value = oldBillObj.BillNumber;
            rpt.Parameters["CustomerName"].Value = oldBillObj.CustomerName;
            rpt.Parameters["CustomerNumber"].Value = oldBillObj.CustomerNumber;
            rpt.Parameters["TenderedCash"].Value = oldBillObj.TenderedCash;
            rpt.Parameters["TenderedChange"].Value = oldBillObj.TenderedChange;
            rpt.Parameters["IsDoorDelivery"].Value = oldBillObj.IsDoorDelivery;
            rpt.Parameters["BranchName"].Value = Utility.branchInfo.BranchName;
            rpt.Parameters["CounterName"].Value = Utility.branchInfo.BranchCounterName;
            rpt.Parameters["Phone"].Value = Utility.branchInfo.PhoneNumber;
            rpt.Parameters["UserName"].Value = Utility.loginInfo.UserFullName;
            rpt.Parameters["RoundingFactor"].Value = oldBillObj.Rounding;
            rpt.Parameters["IsDuplicate"].Value = false;
            rpt.Print();

            LoadBillData(nextBillDetails);
        }

        private void SaveBillDetail(object itemPriceID, object quantity, object weightInKgs, object billDetailID)
        {
            try
            {
                DataTable dtBillDetails = billingRepository.SaveBillDetail(billObj.BillID
                    , itemPriceID, quantity, weightInKgs, Utility.loginInfo.UserID, billDetailID);

                UpdateBillDetails(dtBillDetails, itemPriceID);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Error while saving bill item : {ex.Message}", "Error");
                ClearItemData();
            }
        }

        private void ClearItemData(bool focusItemCode = true)
        {
            txtItemCode.EditValue = null;
            sluItemCode.EditValue = null;
            txtMRP.EditValue = null;
            txtSalePrice.EditValue = null;
            txtQuantity.EditValue = 1;
            txtWeightInKgs.EditValue = 0.00;
            txtQuantity.ReadOnly = false;
            lblOffer.Text = noOfferText;
            lblDeal.Text = noDealText;
            HighlightOffer();
            gvBilling.FocusedColumn = gvBilling.Columns["QUANTITY"];
            if (focusItemCode)
                txtItemCode.Focus();
        }

        private void UpdateSummary()
        {
            gvBilling.GridControl.BindingContext = new BindingContext();
            gvBilling.GridControl.DataSource = billObj.dtBillDetails;

            billObj.Amount = gvBilling.Columns["BILLEDAMOUNT"].SummaryItem.SummaryValue;
            billObj.Quantity = gvBilling.Columns["QUANTITY"].SummaryItem.SummaryValue;
            //lblTotalBill.Text = billObj.Amount.ToString();
            //lblTotalItems.Text = billObj.Quantity.ToString();
        }

        private void LoadBillData(DataSet dsBillInfo)
        {
            billObj = Utility.GetBill(dsBillInfo);

            this.Text = $"NSRetail POS - {billObj.BillNumber}";

            //txtLastBilledAmount.Text = billObj.LastBilledAmount.ToString();
            //txtLastBilledQuantity.Text = billObj.LastBilledQuantity.ToString();

            //txtLastBilledAmount.Text = string.IsNullOrEmpty(txtLastBilledAmount.Text) ? "0.00" : txtLastBilledAmount.Text;
            //txtLastBilledQuantity.Text = string.IsNullOrEmpty(txtLastBilledQuantity.Text) ? "0" : txtLastBilledQuantity.Text;

            gcBilling.DataSource = billObj.dtBillDetails;
            gcBilling.Refresh();

            UpdateSummary();
            SNo = billObj.dtBillDetails.Rows.Count + 1;

            txtItemCode.Focus();
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(txtItemCode.EditValue).Trim()))
                return;
            if (Convert.ToString(txtItemCode.EditValue).Contains("?"))
            {
                string[] stItem = Convert.ToString(txtItemCode.EditValue).Split('?');
                txtItemCode.EditValue = stItem[0];
                txtWeightInKgs.EditValue = stItem[1];
                txtQuantity.ReadOnly = true;
            }
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
            else if (!isItemScanned)
            {
                XtraMessageBox.Show("Item Does Not Exists!");
                ClearItemData();
            }

            isItemScanned = false;
        }

        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            if (billObj.dtBillDetails.Rows.Count == 0)
            {
                XtraMessageBox.Show("No items to draft", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataSet nextBillDetails = billingRepository.DraftBill(Utility.loginInfo.UserID, daySequenceID, billObj.BillID);
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
                rpt.Parameters["Address"].Value = Utility.branchInfo.BranchAddress;
                rpt.Parameters["BillDate"].Value = DateTime.Now;
                rpt.Parameters["BillNumber"].Value = LastBillObj.BillNumber;
                rpt.Parameters["CustomerName"].Value = LastBillObj.CustomerName;
                rpt.Parameters["CustomerNumber"].Value = LastBillObj.CustomerNumber;
                rpt.Parameters["TenderedCash"].Value = LastBillObj.TenderedCash;
                rpt.Parameters["TenderedChange"].Value = LastBillObj.TenderedChange;
                rpt.Parameters["IsDoorDelivery"].Value = LastBillObj.IsDoorDelivery;
                rpt.Parameters["BranchName"].Value = Utility.branchInfo.BranchName;
                rpt.Parameters["CounterName"].Value = Utility.branchInfo.BranchCounterName;
                rpt.Parameters["Phone"].Value = Utility.branchInfo.PhoneNumber;
                rpt.Parameters["UserName"].Value = Utility.loginInfo.UserFullName;
                rpt.Parameters["RoundingFactor"].Value = LastBillObj.Rounding;
                rpt.Parameters["IsDuplicate"].Value = true;
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
            if (gvBilling.FocusedRowHandle < 0)
                return;
            if (XtraMessageBox.Show($"Are you sure want to delete {gvBilling.GetFocusedRowCellValue("ITEMNAME")} ?"
                , "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                txtItemCode.Focus();
                return;
            }

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

            DataTable dtBillingDetails = billingRepository.DeleteBillDetail(billDetailID, Utility.loginInfo.UserID, dtSNos);
            gvBilling.DeleteRow(gvBilling.FocusedRowHandle);
            UpdateBillDetails(dtBillingDetails, 0);
            txtItemCode.Focus();
        }

        private void syncProgressBar_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            //e.DisplayText = bgSyncReportText;
        }

        private void gvBilling_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "QUANTITY" || isEventCall) return;
            DataRow drUpdatedBill = gvBilling.GetDataRow(e.RowHandle);
            SaveBillDetail(drUpdatedBill["ITEMPRICEID"], drUpdatedBill["QUANTITY"], drUpdatedBill["WEIGHTINKGS"], drUpdatedBill["BILLDETAILID"]);
            txtItemCode.Focus();
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    btnCloseBill_Click(sender, e);
                    break;
                case Keys.F2:
                    gvBilling.Focus();
                    gvBilling.FocusedRowHandle = 0;
                    gvBilling.FocusedColumn = gvBilling.Columns["QUANTITY"];
                    gvBilling.ShowEditor();
                    break;
                case Keys.F3:
                    sluItemCode.Focus();
                    break;
                case Keys.F4:
                    btnSyncData_Click(sender, e);
                    break;
                case Keys.F5:
                    btnSaveBill_Click(sender, e);
                    break;
                case Keys.F6:
                    btnLoadDraftBill_Click(sender, e);
                    break;
                case Keys.F7:
                    btnRefund_Click(sender, e);
                    break;
                case Keys.F8:
                    btnLastBillPrint_Click(sender, e);
                    break;
                case Keys.F9:
                    btnDayClosure_Click(sender, e);
                    break;
                case Keys.F10:
                    btnStockIn_Click(sender, e);
                    break;
                case Keys.F11:
                    btnBranchRefund_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void btnStockIn_Click(object sender, EventArgs e)
        {
            new frmStockInList().ShowDialog();
        }

        private void btnRefund_Click(object sender, EventArgs e)
        {
            frmRefund obj = new frmRefund();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnBranchRefund_Click(object sender, EventArgs e)
        {
            frmBranchRefund obj = new frmBranchRefund()
            { ShowInTaskbar = false, StartPosition = FormStartPosition.CenterScreen };
            obj.ShowDialog();
        }

        private void btnDayClosure_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to day close?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                frmDayClosure obj = new frmDayClosure(daySequenceID)
                { ShowInTaskbar = false, StartPosition = FormStartPosition.CenterScreen };
                obj.ShowDialog();
                if (obj.DayClosed)
                {
                    DisableBilling();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error");
            }
        }

        private void UpdateBillDetails(DataTable dtBillDetails, object itemPriceID)
        {
            int updatedRowHandle = 0;

            foreach (DataRow drUpdatedBillDetail in dtBillDetails.Rows)
            {
                DataRow drBillDetail;
                updatedRowHandle = gvBilling.LocateByValue("BILLDETAILID", drUpdatedBillDetail["BILLDETAILID"]);
                if (updatedRowHandle < 0)
                {
                    drBillDetail = billObj.dtBillDetails.NewRow();
                    billObj.dtBillDetails.Rows.Add(drBillDetail);
                    updatedRowHandle = 0;
                }
                else
                {
                    drBillDetail = gvBilling.GetDataRow(updatedRowHandle);
                }

                drBillDetail["BILLDETAILID"] = drUpdatedBillDetail["BILLDETAILID"];
                drBillDetail["BILLID"] = drUpdatedBillDetail["BILLID"];
                drBillDetail["ITEMPRICEID"] = drUpdatedBillDetail["ITEMPRICEID"];
                drBillDetail["SNO"] = drUpdatedBillDetail["SNO"];
                drBillDetail["ITEMNAME"] = drUpdatedBillDetail["ITEMNAME"];
                drBillDetail["ITEMCODE"] = drUpdatedBillDetail["ITEMCODE"];
                drBillDetail["HSNCODE"] = drUpdatedBillDetail["HSNCODE"];
                drBillDetail["MRP"] = drUpdatedBillDetail["MRP"];
                drBillDetail["SALEPRICE"] = drUpdatedBillDetail["SALEPRICE"];
                drBillDetail["GSTCODE"] = drUpdatedBillDetail["GSTCODE"];
                drBillDetail["QUANTITY"] = drUpdatedBillDetail["QUANTITY"];
                drBillDetail["WEIGHTINKGS"] = drUpdatedBillDetail["WEIGHTINKGS"];
                drBillDetail["BILLEDAMOUNT"] = drUpdatedBillDetail["BILLEDAMOUNT"];
                drBillDetail["DISCOUNT"] = drUpdatedBillDetail["DISCOUNT"];
                drBillDetail["CGST"] = drUpdatedBillDetail["CGST"];
                drBillDetail["SGST"] = drUpdatedBillDetail["SGST"];
                drBillDetail["IGST"] = drUpdatedBillDetail["IGST"];
                drBillDetail["CESS"] = drUpdatedBillDetail["CESS"];
                drBillDetail["GSTVALUE"] = drUpdatedBillDetail["GSTVALUE"];
                drBillDetail["GSTID"] = drUpdatedBillDetail["GSTID"];
                drBillDetail["CGSTDESC"] = drUpdatedBillDetail["CGSTDESC"];
                drBillDetail["SGSTDESC"] = drUpdatedBillDetail["SGSTDESC"];
                drBillDetail["CESSDESC"] = drUpdatedBillDetail["CESSDESC"];
                drBillDetail["ISOPENITEM"] = drUpdatedBillDetail["ISOPENITEM"];
                drBillDetail["DISCOUNT"] = drUpdatedBillDetail["DISCOUNT"];
                drBillDetail["OFFERID"] = drUpdatedBillDetail["OFFERID"];
                drBillDetail["OFFERTYPECODE"] = drUpdatedBillDetail["OFFERTYPECODE"];
            }

            UpdateSummary();

            gvBilling.FocusedRowHandle = gvBilling.LocateByValue("ITEMPRICEID", itemPriceID);
        }

        private void DisableBilling()
        {
            txtItemCode.Enabled = false;
            sluItemCode.Enabled = false;
            txtQuantity.Enabled = false;
            btnCloseBill.Enabled = false;
            btnSaveBill.Enabled = false;
            btnLoadDraftBill.Enabled = false;
            btnRefund.Enabled = false;
            btnLastBillPrint.Enabled = false;
            btnDayClosure.Enabled = false;
        }

        private void btnChangePassword_Click_1(object sender, EventArgs e)
        {
            frmChangePassword obj = new frmChangePassword();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmBarCodePrint obj = new frmBarCodePrint();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnSyncData_Click(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
            Utility.StartSync(null);
            SplashScreenManager.CloseForm();
        }

        private void gvBilling_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                btnDelete_ButtonClick(null, null);
        }

        private void btnCRWithoutBill_Click(object sender, EventArgs e)
        {
            frmCustomerRefund obj = new frmCustomerRefund();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void LoadItemCodes()
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)LoadItemCodes);
                return;
            }

            sluItemCode.Properties.DataSource = itemRepository.GetItemCodes();
            sluItemCode.Properties.DisplayMember = "ITEMNAME";
            sluItemCode.Properties.ValueMember = "ITEMCODEID";

            sluItemCodeView.GridControl.BindingContext = new BindingContext();
            sluItemCodeView.GridControl.DataSource = sluItemCode.Properties.DataSource;
        }

        private void gvBilling_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            if (billObj == null) return;

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;
            var rect = e.Bounds;
            rect.X += 10;
            e.DefaultDraw();
            string message = "Last Billed Amount : " + (string.IsNullOrEmpty(billObj.LastBilledAmount?.ToString()) ? "0.00" : billObj.LastBilledAmount.ToString());
            message += "  Last Billed Quantity : " + (string.IsNullOrEmpty(billObj.LastBilledQuantity?.ToString()) ? "0" : billObj.LastBilledQuantity.ToString());
            e.Cache.DrawString(message, e.Appearance.GetFont(), e.Appearance.GetForeBrush(e.Cache), rect, stringFormat);
            e.Handled = true;
        }

        private void btnApplyDiscount_Click(object sender, EventArgs e)
        {
            if (txtSplDiscPer.EditValue == null) return;

            DataTable dtBillDetails = billingRepository.ApplySpecialDiscount(txtSplDiscPer.EditValue, billObj.BillID, Utility.loginInfo.UserID);

            UpdateBillDetails(dtBillDetails, 0);
        }

        private void HighlightOffer()
        {
            if (lblOffer.Text.Trim().Equals(noOfferText.Trim()))
            {
                lblOffer.Appearance.Font = new Font("Tahoma", 9.5F, FontStyle.Regular);
                lblOffer.Appearance.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblOffer.Appearance.Font = new Font("Arial", 11F, FontStyle.Bold);
                lblOffer.Appearance.ForeColor = Color.YellowGreen;
            }

            if (lblDeal.Text.Trim().Equals(noDealText.Trim()))
            {
                lblDeal.Appearance.Font = new Font("Tahoma", 9.5F, FontStyle.Regular);
                lblDeal.Appearance.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                lblDeal.Appearance.Font = new Font("Arial", 11F, FontStyle.Bold);
                lblDeal.Appearance.ForeColor = Color.YellowGreen;
            }
        }
    }
}