﻿using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout.Utils;
using DevExpress.XtraSplashScreen;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using NSRetailPOS.Helpers;
using NSRetailPOS.Operations;
using NSRetailPOS.Reports;
using NSRetailPOS.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS
{
    public partial class frmMain : XtraForm, IBarcodeReceiver
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

        private static frmMain instance;

        public static frmMain Instance 
        {
            get 
            {
                return instance = instance ?? new frmMain(); 
            }
        }

        public frmMain()
        {
            InitializeComponent();
            Utility.SetGridFormatting(gvBilling);
            Utility.SetGridFormatting(sluItemCodeView);

            gvBilling.Columns["OFFERTYPECODE"].AppearanceCell.ForeColor = Color.YellowGreen;

            txtQuantity.ConfirmLargeNumber();
            txtWeightInKgs.ConfirmLargeNumber();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Utility.ActiveForm = this;
            Utility.ListenSerialPort();
            lblUserinfo.Text = $"Loggedin User : {Utility.loginInfo.UserFullName}    " +
                $"Role : {Utility.loginInfo.RoleName} - {ConfigurationManager.AppSettings["BuildType"]}    ";
            lblVersionInfo.Text = $"App Version {Utility.AppVersion} || DB Version {Utility.DBVersion} ";
            btnCRWithoutBill.Enabled = Utility.loginInfo.RoleName.Equals("Store Admin");
            txtSplDiscPer.Enabled = Utility.loginInfo.RoleName.Equals("Store Admin");
            btnApplyDiscount.Enabled = Utility.loginInfo.RoleName.Equals("Store Admin");
            btnDayClosure.Enabled = Utility.loginInfo.RoleName.Equals("Store Manager");            
            btnOperations.Enabled = Utility.loginInfo.RoleName.Equals("Store Manager");
            gcDiscount.Visible = Utility.branchInfo.BranchID.Equals(45) || Utility.branchInfo.BranchID.Equals(103);

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
            Task.Run(() => DoWorkAsync());
            Blink();
        }

        private void Utility_ItemOrCodeChanged(object sender, EventArgs e)
        {
            LoadItemCodes();
        }

        private void BgSyncWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblProgressText.Text = e.UserState?.ToString();
        }

        public void ShowProgress(string text)
        {
            if(this.InvokeRequired)
            {
                this.BeginInvoke((Action)(() => ShowProgress(text)));
                return;
            }

            lblProgressText.Text = text;
        }
              

        private async Task DoWorkAsync()
        {
            if (!await Utility.StartSync(false))
            {
                Application.Exit();
                return;
            }
            Thread.Sleep(10 * 60 * 1000);
            await DoWorkAsync();
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
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
            try
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
                else if(!double.TryParse(txtWeightInKgs.EditValue?.ToString(), out double weightInKgs) || weightInKgs == 0)
                {
                    XtraMessageBox.Show("Loose\\Bulk items cannot be sold without weight", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataTable dtPrices = itemRepository.GetMRPList(sluItemCode.EditValue);
                drSelectedPrice = null;
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
                else if (dtPrices.Rows.Count == 0)
                {
                    XtraMessageBox.Show("Item code or stock not found for the scan. please contact administrator");
                    ClearItemData();
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnCloseBill_Click(object sender, EventArgs e)
        {
            if (!btnCloseBill.Enabled) return;

            DataTable dtBillDetails = billObj.dtBillDetails.Copy();
            dtBillDetails.DefaultView.RowFilter = "DELETEDDATE IS NULL";
            dtBillDetails = dtBillDetails.DefaultView.ToTable();

            if (dtBillDetails.Rows.Count == 0)
            {
                XtraMessageBox.Show("No items to bill", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool canClose = true;

            DataTable dtBillOffers = billingRepository.GetBillOffers(billObj.BillID);
            if (dtBillOffers != null && dtBillOffers.Rows.Count == 1)
            {
                double actualSalePrice = Convert.ToDouble(dtBillOffers.Rows[0]["ACTUALSALEPRICE"]);
                if (XtraMessageBox.Show($"Add item {dtBillOffers.Rows[0]["ITEMNAME"]} ({dtBillOffers.Rows[0]["SKUCODE"]}) for Rs.{actualSalePrice} to the bill?",
                    "Add free item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SaveBillDetail(dtBillOffers.Rows[0]["ITEMPRICEID"], 1, 0, -1, true, actualSalePrice);

                    //refresh bill details
                    dtBillDetails = billObj.dtBillDetails.Copy();
                    dtBillDetails.DefaultView.RowFilter = "DELETEDDATE IS NULL";
                    dtBillDetails = dtBillDetails.DefaultView.ToTable();
                    canClose = false;

                    if (actualSalePrice > 0)
                    {
                        XtraMessageBox.Show("Bill amount updated, please verify amount", "Verification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            frmUnifiedPayment paymentForm = new frmUnifiedPayment(billObj, canClose);
            paymentForm.ShowDialog();
            DataSet nextBillDetails = null;

            if (paymentForm.IsDiscarded)
            {
                nextBillDetails = billingRepository.DiscardBill(Utility.loginInfo.UserID, daySequenceID, billObj.BillID);
                LoadBillData(nextBillDetails);
                return;
            }

            if (!paymentForm.IsPaid) { return; }

            try
            {
                nextBillDetails = billingRepository.FinishBill(Utility.loginInfo.UserID, daySequenceID, billObj);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // use this object for printing
                Bill oldBillObj = billObj.Clone() as Bill;
                DataView dv = oldBillObj.dtMopValues.DefaultView;
                dv.RowFilter = "MOPVALUE > 0";
                rptBill rpt = new rptBill(dtBillDetails, dv.ToTable());
                rpt.Parameters["GSTIN"].Value = "37AAICV7240C1ZC";
                rpt.Parameters["CIN"].Value = "U51390AP2022PTC121579";
                rpt.Parameters["FSSAI"].Value = "10114004000548";
                rpt.Parameters["Address"].Value = Utility.branchInfo.BranchAddress;
                rpt.Parameters["BillDate"].Value = DateTime.Now;
                rpt.Parameters["BillNumber"].Value = oldBillObj.BillNumber;
                rpt.Parameters["CustomerName"].Value = oldBillObj.CustomerName;
                rpt.Parameters["CustomerNumber"].Value = oldBillObj.CustomerNumber;
                rpt.Parameters["CustomerGST"].Value = oldBillObj.CustomerGST;
                rpt.Parameters["TenderedCash"].Value = oldBillObj.TenderedCash;
                rpt.Parameters["TenderedChange"].Value = oldBillObj.TenderedChange;
                rpt.Parameters["IsDoorDelivery"].Value = oldBillObj.IsDoorDelivery;
                rpt.Parameters["BranchName"].Value = Utility.branchInfo.BranchName;
                rpt.Parameters["CounterName"].Value = Utility.branchInfo.BranchCounterName;
                rpt.Parameters["Phone"].Value = Utility.branchInfo.PhoneNumber;
                rpt.Parameters["UserName"].Value = Utility.loginInfo.UserFullName;
                rpt.Parameters["RoundingFactor"].Value = oldBillObj.Rounding;
                rpt.Parameters["IsDuplicate"].Value = false;
                Utility.PrintReport(rpt);
                this.BringToFront();

                if (Utility.branchInfo.BranchID.Equals(103) || Utility.branchInfo.BranchID.Equals(105))
                {
                    Utility.PrintReport(rpt);
                    this.BringToFront();
                }

                try
                {
                    if (dv.ToTable().Select("MOPID=1").Count() > 0)
                    {
                        DefaultPrinter printer = new DefaultPrinter();
                        printer.OpenPrinter("");
                        printer.Print($"{(char)27}{(char)112}{(char)0}{(char)25}{(char)250}");
                        printer.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Printer does not support cash drawer : " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Printing Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LoadBillData(nextBillDetails);
        }

        private void SaveBillDetail(object itemPriceID, object quantity, object weightInKgs, object billDetailID
            , bool isBillOfferItem = false, object billOfferPrice = null)
        {
            try
            {
                DataTable dtBillDetails = billingRepository.SaveBillDetail(billObj.BillID
                    , itemPriceID, quantity, weightInKgs, Utility.loginInfo.UserID, billDetailID, isBillOfferItem, billOfferPrice);

                UpdateBillDetails(dtBillDetails, itemPriceID);

                if (!isBillOfferItem)
                {
                    RefreshBillOfferLabel();
                }
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
            DataView dvBillDetails = billObj.dtBillDetails.Copy().DefaultView;
            dvBillDetails.RowFilter = "DELETEDDATE IS NULL";
            SNo = dvBillDetails.Count + 1;

            txtItemCode.Focus();
            lblBillOfferContainer.Visibility = LayoutVisibility.Never;
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter || string.IsNullOrEmpty(Convert.ToString(txtItemCode.EditValue).Trim()))
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
            if (!btnSaveBill.Enabled) return;

            DataTable dtBillDetails = billObj.dtBillDetails.Copy();
            dtBillDetails.DefaultView.RowFilter = "DELETEDDATE IS NULL";
            dtBillDetails = dtBillDetails.DefaultView.ToTable();

            if (dtBillDetails.Rows.Count == 0)
            {
                XtraMessageBox.Show("No items to draft", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtDraftBills = new BillingRepository().GetDraftBills(daySequenceID);
            if(dtDraftBills.Rows.Count >= Utility.branchInfo.EnableDraftBills 
                || Utility.branchInfo.EnableDraftBills == 0)
            {
                XtraMessageBox.Show("Draft limit reached, operation has been cancelled!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataSet nextBillDetails = billingRepository.DraftBill(Utility.loginInfo.UserID, daySequenceID, billObj.BillID);
            LoadBillData(nextBillDetails);
        }

        private void btnLoadDraftBill_Click(object sender, EventArgs e)
        {
            if (!btnLoadDraftBill.Enabled) return;

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
            instance = null;
            Utility.ItemOrCodeChanged -= Utility_ItemOrCodeChanged;
            frmLogin.Instance.Show();
        }

        private void btnLastBillPrint_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show("Cannot re-print last bill, contact administrator", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //try
            //{
            //    DataSet dsLastBillDetails = new BillingRepository().GetLastBill(daySequenceID, billObj.LastBillID);
            //    Bill LastBillObj = Utility.GetBill(dsLastBillDetails);
            //    rptBill rpt = new rptBill(LastBillObj.dtBillDetails, LastBillObj.dtMopValues);
            //    rpt.Parameters["GSTIN"].Value = "37AAICV7240C1ZC";
            //    rpt.Parameters["CIN"].Value = "U51390AP2022PTC121579";
            //    rpt.Parameters["FSSAI"].Value = "10114004000548";
            //    rpt.Parameters["Address"].Value = Utility.branchInfo.BranchAddress;
            //    rpt.Parameters["BillDate"].Value = DateTime.Now;
            //    rpt.Parameters["BillNumber"].Value = LastBillObj.BillNumber;
            //    rpt.Parameters["CustomerName"].Value = LastBillObj.CustomerName;
            //    rpt.Parameters["CustomerNumber"].Value = LastBillObj.CustomerNumber;
            //    rpt.Parameters["CustomerGST"].Value = LastBillObj.CustomerGST;
            //    rpt.Parameters["TenderedCash"].Value = LastBillObj.TenderedCash;
            //    rpt.Parameters["TenderedChange"].Value = LastBillObj.TenderedChange;
            //    rpt.Parameters["IsDoorDelivery"].Value = LastBillObj.IsDoorDelivery;
            //    rpt.Parameters["BranchName"].Value = Utility.branchInfo.BranchName;
            //    rpt.Parameters["CounterName"].Value = Utility.branchInfo.BranchCounterName;
            //    rpt.Parameters["Phone"].Value = Utility.branchInfo.PhoneNumber;
            //    rpt.Parameters["UserName"].Value = Utility.loginInfo.UserFullName;
            //    rpt.Parameters["RoundingFactor"].Value = LastBillObj.Rounding;
            //    rpt.Parameters["IsDuplicate"].Value = true;
            //    rpt.Print();
            //    txtItemCode.Focus();
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message);
            //}
        }

        private void txtItemCode_Enter(object sender, EventArgs e)
        {
            txtItemCode.SelectAll();
        }

        private void txtItemCode_Click(object sender, EventArgs e)
        {
            txtItemCode.SelectAll();
        }

        private void btnDelete_ButtonClick(object sender, ButtonPressedEventArgs e)
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
                if (gvBilling.GetRowCellValue(curRowHandle, "DELETEDDATE") != DBNull.Value)
                    continue;

                gvBilling.SetRowCellValue(curRowHandle, "SNO", SNo);
                dtSNos.Rows.Add(gvBilling.GetRowCellValue(curRowHandle, "BILLDETAILID"), SNo);
                SNo++;
            }

            DataTable dtBillingDetails = billingRepository.DeleteBillDetail(billDetailID, Utility.loginInfo.UserID, dtSNos);
            gvBilling.SetFocusedRowCellValue("DELETEDDATE", DateTime.Now.ToString("d"));
            gvBilling.SetFocusedRowCellValue("SNO", null);
            //gvBilling.DeleteRow(gvBilling.FocusedRowHandle);
            UpdateBillDetails(dtBillingDetails, 0);
            RefreshBillOfferLabel();
            txtItemCode.Focus();
        }

        private void syncProgressBar_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
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
                case Keys.F12:
                    btnPriceCheck_Click(sender, e);
                    break;
                default:
                    break;
            }
        }

        private void btnRefund_Click(object sender, EventArgs e)
        {
            if (!btnRefund.Enabled) return;

            frmRefund obj = new frmRefund();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.StartPosition = FormStartPosition.CenterScreen;
            obj.ShowDialog();
        }

        private void btnDayClosure_Click(object sender, EventArgs e)
        {
            if (!btnDayClosure.Enabled) return;

            if (XtraMessageBox.Show("Are you sure want to day close?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {

                DataTable dtBillDetails = billObj.dtBillDetails.Copy();
                DataView dvBillDetails = dtBillDetails.DefaultView;
                dvBillDetails.RowFilter = "DELETEDDATE IS NULL";

                DataTable dtDraftBills = new BillingRepository().GetDraftBills(daySequenceID);
                DataView dvDraftBills = dtDraftBills.DefaultView;
                dvDraftBills.RowFilter = "QUANTITY > 0";

                if(dvBillDetails.Count > 0 || dvDraftBills.Count > 0)
                {
                    if(XtraMessageBox.Show("Any scanned items in open and draft bills will be automatically deleted, do you want to proceed?"
                        , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
                }

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

        private async void btnSyncData_Click(object sender, EventArgs e)
        {
            if (!btnSyncData.Enabled) return;

            SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
            if (!await Utility.StartSync(true)) Application.Exit();
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

        public void ReceiveBarCode(string data)
        {            
            txtItemCode.Text = data;
            //txtItemCode_Leave(txtItemCode, new EventArgs());
        }

        private void gvBilling_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvBilling.GetFocusedRowCellValue("DELETEDDATE") != DBNull.Value)
            {
                e.Cancel = true;
                return;
            }

            if (Utility.branchInfo.MultiEditThreshold == 0 || gvBilling.FocusedColumn != gcQuantity) return;
            e.Cancel = Utility.branchInfo.MultiEditThreshold < decimal.Parse(gvBilling.GetFocusedRowCellValue("MRP").ToString());
        }

        private void gvBilling_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            if (gvBilling.FocusedColumn.FieldName != "QUANTITY" || (e.Value != null && !e.Value.Equals("0"))) return;

            string msg = $"0 or empty quantity is not allowed. Delete the item - {gvBilling.GetRowCellValue(gvBilling.FocusedRowHandle, "ITEMNAME")} instead";
            XtraMessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.Valid = false;
            e.ErrorText = msg;
        }

        private void btnDiscount_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (gvBilling.FocusedRowHandle < 0)
                return;

            BillDetail billDetailObj = new BillDetail()
            {
                BillDetailID = gvBilling.GetFocusedRowCellValue("BILLDETAILID"),
                ItemCode = gvBilling.GetFocusedRowCellValue("ITEMCODE"),
                ItemName = gvBilling.GetFocusedRowCellValue("ITEMNAME"),
                MRP = gvBilling.GetFocusedRowCellValue("MRP"),
                SalePrice = gvBilling.GetFocusedRowCellValue("SALEPRICE"),
                Quantity = gvBilling.GetFocusedRowCellValue("QUANTITY"),
                WeightInKGs = gvBilling.GetFocusedRowCellValue("WEIGHTINKGS"),
                BilledAmount = gvBilling.GetFocusedRowCellValue("BILLEDAMOUNT"),
                IsOpenItem = gvBilling.GetFocusedRowCellValue("ISOPENITEM"),
                ItemPriceID = gvBilling.GetFocusedRowCellValue("ITEMPRICEID")
            };

            if (new frmExtraDiscount(billDetailObj).ShowDialog() == DialogResult.OK)
            {
                UpdateBillDetails(billDetailObj.dtBillDetails, billDetailObj.ItemPriceID);
                txtItemCode.Focus();
            }
        }

        private void btnOperations_Click(object sender, EventArgs e)
        {
            frmOpetations frmOpetations = new frmOpetations();
            frmOpetations.ShowDialog();
        }

        private void btnPriceCheck_Click(object sender, EventArgs e)
        {
            _ = new frmPriceCheck(sluItemCode.Properties.DataSource).ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {            
            this.Close();                 
        }

        int totalQuantity = 0;
        decimal totalValue = 0;
        private void gvBilling_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridSummaryItem gridSummaryItem = (e.Item as GridSummaryItem);
            
            if (gridSummaryItem.FieldName == "QUANTITY")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    totalQuantity = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate &&
                    gvBilling.GetRowCellValue(e.RowHandle, "DELETEDDATE") == DBNull.Value)
                {
                    totalQuantity += int.Parse(gvBilling.GetRowCellValue(e.RowHandle, "QUANTITY").ToString());
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    e.TotalValue = totalQuantity;
                    e.TotalValueReady = true;
                }
            }
            else if (gridSummaryItem.FieldName == "BILLEDAMOUNT")
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    totalValue = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate &&
                    gvBilling.GetRowCellValue(e.RowHandle, "DELETEDDATE") == DBNull.Value)
                {
                    totalValue += decimal.Parse(gvBilling.GetRowCellValue(e.RowHandle, "BILLEDAMOUNT").ToString());
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {
                    e.TotalValue = totalValue;
                    e.TotalValueReady = true;
                }
            }
        }

        private void RefreshBillOfferLabel()
        {
            DataTable dtBillOffers = billingRepository.GetBillOffers(billObj.BillID);
            if (dtBillOffers != null && dtBillOffers.Rows.Count == 1)
            {
                lblBillOffer.Text = $"Bill offer : {dtBillOffers.Rows[0]["ITEMNAME"]}  for Rs.{dtBillOffers.Rows[0]["ACTUALSALEPRICE"]}";
                lblBillOffer.Visible = true;
                
                lblBillOfferContainer.Visibility = LayoutVisibility.Always;
            }
            else
            {
                lblBillOffer.Visible = false;
                lblBillOfferContainer.Visibility = LayoutVisibility.Never;
            }
        }

        private async void Blink()
        {
            while (true)
            {
                await Task.Delay(500);
                if(!lblBillOfferContainer.Visible) continue;
                //Color.FromArgb(59, 154, 220)
                lblBillOffer.ForeColor = lblBillOffer.ForeColor == Color.LightBlue ? Color.YellowGreen : Color.LightBlue;
                //lblBillOffer.BackColor = lblBillOffer.BackColor == Color.Black ? Color.FromArgb(50, 50, 50) : Color.Black;
            }
        }
    }
}