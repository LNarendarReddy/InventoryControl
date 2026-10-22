using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using NSRetailPOS.Gateway;
using NSRetailPOS.Gateway.PineLabs;
using NSRetailPOS.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmUnifiedPayment : XtraForm
    {
        readonly BillingRepository billingRepository = new BillingRepository();
        readonly Bill billObj;
        DataTable dtMOPs;
        bool loading = true, busy;
        CancellationTokenSource cancellationTokenSource;
        frmDynamicQr qrForm;
        public bool IsPaid, IsDiscarded, canClose, isCashOnly;
        decimal billedAmount, remainingAmount, paidAmount;
        int SelectedMopID => rgPaymentModes.EditValue == null ? -1 : Convert.ToInt32(rgPaymentModes.EditValue);
        int FocusedMopID => gvMOP.GetFocusedDataRow() == null ? -1 : Convert.ToInt32(gvMOP.GetFocusedDataRow()["MOPID"]);
        CounterPaymentMode ActiveMode => Utility.CounterPayments.Find(FocusedMopID);
        bool IsAdmin => string.Equals(Convert.ToString(Utility.loginInfo.RoleName), "Store Admin", StringComparison.OrdinalIgnoreCase);
        decimal Received(int mopID) => billObj.CompletedTransactions.Where(t => t.MopID == mopID).Sum(t => t.Amount);

        public frmUnifiedPayment(Bill bill, bool canClose = true)
        {
            InitializeComponent();
            billObj = bill;
            this.canClose = canClose;
            Text += bill.BillNumber;
            CloseBox = canClose;
            btnCancel.Enabled = canClose;
            btnDiscardBill.Enabled = !canClose;
            FormClosing += (s, e) => { if (busy) e.Cancel = true; };
            gvMOP.FocusedRowChanged += (s, e) => RefreshCollection();
        }

        private void frmMultiPayment_Load(object sender, EventArgs e)
        {
            try
            {
                billedAmount = Convert.ToDecimal(billObj.Amount);
                txtBilledAmount.EditValue = billedAmount;
                txtItemQuantity.EditValue = billObj.Quantity;
                dtMOPs = new DataTable();
                dtMOPs.Columns.Add("MOPID", typeof(int));
                dtMOPs.Columns.Add("MOPNAME", typeof(string));
                dtMOPs.Columns.Add("MOPVALUE", typeof(decimal));
                foreach (var mode in Utility.CounterPayments.Modes)
                {
                    // Preserve the existing warehouse-only B2C credit rule.
                    if (mode.Name == "B2C Credit" && Convert.ToInt32(Utility.branchInfo.BranchID) != 45) continue;
                    dtMOPs.Rows.Add(mode.MopID, mode.Name, Received(mode.MopID));
                    rgPaymentModes.Properties.Items.Add(new RadioGroupItem(mode.MopID, mode.Name));
                }
                rgPaymentModes.Properties.Items.Add(new RadioGroupItem(-1, "Multiple"));
                gcMOP.DataSource = dtMOPs;
                txtPaymentValue.Validating += TxtPaymentValue_Validating;
                txtPaymentValue.Leave += TxtPaymentValue_Leave;
                txtPaymentValue.ValidateOnEnterKey = true;
                rgSaleType.EditValue = false;
                layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                groupControl1.Text = "Gateway Collection";
                btnAddMissingPayment.Text = "Check / Link Payment";
                var receivedModes = dtMOPs.AsEnumerable().Where(r => Received((int)r["MOPID"]) > 0).ToList();
                rgPaymentModes.EditValue = receivedModes.Count > 1 ? -1
                    : receivedModes.Count == 1 ? (int)receivedModes[0]["MOPID"]
                    : dtMOPs.AsEnumerable().Any(r => (int)r["MOPID"] == 1) ? 1
                    : dtMOPs.Rows.Count > 0 ? (int)dtMOPs.Rows[0]["MOPID"] : -1;
                loading = false;
                rgPaymentModes_SelectedIndexChanged(null, null);
                Utility.SetGridFormatting(gvMOP);
                if (dtMOPs.Rows.Count == 0)
                    Utility.ShowErrorMessage("No payment modes are configured for this counter. Configure and sync the counter first.");
                var allowed = dtMOPs.AsEnumerable().Select(r => (int)r["MOPID"]).ToList();
                if (billObj.CompletedTransactions.Any(t => !allowed.Contains(t.MopID)))
                    Utility.ShowErrorMessage("This bill has payments in a mode no longer available at this counter. Restore the mapping before completing the bill.");
            }
            catch (Exception ex) { Utility.ShowError(ex); btnApply.Enabled = false; }
        }

        private void rgPaymentModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (loading || dtMOPs == null) return;
            isCashOnly = SelectedMopID == 1;
            foreach (DataRow row in dtMOPs.Rows)
            {
                int id = (int)row["MOPID"];
                row["MOPVALUE"] = id == SelectedMopID && !isCashOnly
                    ? Math.Max(Received(id), billedAmount) : Received(id);
            }
            int handle = gvMOP.LocateByValue("MOPID", SelectedMopID);
            if (handle >= 0) gvMOP.FocusedRowHandle = handle;
            Recalculate();
        }

        private void rgPaymentModes_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (loading || e.NewValue == null || Convert.ToInt32(e.NewValue) == -1) return;
            int id = Convert.ToInt32(e.NewValue);
            if (billObj.CompletedTransactions.Any(t => t.MopID != id && t.Amount > 0))
            {
                Utility.ShowErrorMessage("Select Multiple to retain payments already received in another mode.");
                e.Cancel = true;
            }
        }

        private void Recalculate()
        {
            if (loading || dtMOPs == null) return;
            paidAmount = dtMOPs.AsEnumerable().Sum(r => Utility.CounterPayments.Find((int)r["MOPID"])?.RequiresGateway == true
                ? Received((int)r["MOPID"]) : Convert.ToDecimal(r["MOPVALUE"]));
            remainingAmount = (isCashOnly ? Math.Round(billedAmount, MidpointRounding.AwayFromZero) : billedAmount) - paidAmount;
            txtPaidAmount.EditValue = paidAmount;
            txtRemainingAmount.EditValue = Math.Max(0, remainingAmount);
            RefreshCollection();
        }

        private void RefreshCollection()
        {
            if (loading || dtMOPs == null) return;
            var mode = ActiveMode;
            decimal entered = gvMOP.GetFocusedDataRow() == null ? 0 : Convert.ToDecimal(gvMOP.GetFocusedDataRow()["MOPVALUE"]);
            groupControl1.Text = mode == null ? "Gateway Collection" : mode.Name + " - " + (mode.RequiresGateway ? mode.GatewayType : "Manual");
            txtCardRecievedAmount.EditValue = mode == null ? 0 : Received(mode.MopID);
            txtCardRequestAmount.EditValue = mode == null ? 0 : Math.Max(0, entered - Received(mode.MopID));
            txtCardRequestAmount.Properties.ReadOnly = true;
            txtCardRequestAmount.Enabled = mode?.RequiresGateway == true && !busy;
            btnReceiveCard.Enabled = !busy && mode?.RequiresGateway == true && mode.Gateway != null
                && Convert.ToDecimal(txtCardRequestAmount.EditValue) > 0;
            btnAddMissingPayment.Enabled = !busy;
        }

        private void TxtPaymentValue_Leave(object sender, EventArgs e) { gvMOP.PostEditor(); Recalculate(); }
        private void TxtPaymentValue_Validating(object sender, CancelEventArgs e)
        {
            var edit = sender as BaseEdit;
            if (!decimal.TryParse(Convert.ToString(edit?.EditValue), out decimal value) || value < 0
                || decimal.Round(value, 2) != value || value < Received(FocusedMopID))
            {
                Utility.ShowErrorMessage("Enter a non-negative amount, not less than the amount already received.");
                e.Cancel = true;
            }
        }
        private void gvMOP_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e) { Recalculate(); }
        private void gvMOP_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = busy || (SelectedMopID != -1 && SelectedMopID != 1);
        }
        private void GvMOP_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e) { }
        private void rgPaymentModes_Leave(object sender, EventArgs e) { }
        private void gvMOP_KeyDown(object sender, KeyEventArgs e) { }
        private void gcMOP_Enter(object sender, EventArgs e) { RefreshCollection(); }
        private async void btnReceiveCard_Click(object sender, EventArgs e) { await ReceiveSelected(); }
        private async void btnUPIReceive_Click(object sender, EventArgs e) { await ReceiveSelected(); }
        private void btnCancelRequest_Click(object sender, EventArgs e) { cancellationTokenSource?.Cancel(); btnCancelRequest.Enabled = false; }

        private void SetBusy(bool value)
        {
            busy = value;
            layoutControlGroup3.Enabled = !value;
            rgPaymentModes.Enabled = gcMOP.Enabled = btnApply.Enabled = !value;
            btnCancel.Enabled = !value && canClose;
            btnDiscardBill.Enabled = !value && !canClose;
            btnCancelRequest.Enabled = value;
            RefreshCollection();
        }

        private void AttachGateway(PaymentGatewayBase gateway, PaymentAttempt attempt)
        {
            gateway.StatusUpdate = ShowReceiveMessage;
            gateway.RefundRequested = attempt.RefundRequested;
            gateway.RefundRequestedReady = () =>
            {
                attempt.RefundRequested = true;
                billingRepository.SavePaymentAttempt(attempt);
            };
            gateway.ProviderReferenceReady = reference =>
            {
                attempt.ProviderReference = reference;
                billingRepository.SavePaymentAttempt(attempt);
            };
            gateway.QrReady = data =>
            {
                qrForm?.Complete();
                qrForm = new frmDynamicQr(data, attempt.Amount, attempt.RequestReference,
                    () => cancellationTokenSource?.Cancel());
                qrForm.Show(this);
            };
        }

        private async Task ReceiveSelected()
        {
            if (busy) return;
            var mode = ActiveMode;
            if (mode?.RequiresGateway != true) return;
            if (mode.Gateway == null) { Utility.ShowErrorMessage("The selected gateway configuration is missing or invalid. Correct and sync it first."); return; }
            gvMOP.CloseEditor();
            gvMOP.UpdateCurrentRow();
            decimal amount = Convert.ToDecimal(txtCardRequestAmount.EditValue ?? 0);
            if (amount <= 0 || amount > billedAmount - billObj.CompletedTransactions.Sum(t => t.Amount)) return;
            if (mode.GatewayType.Equals("Mobikwik", StringComparison.OrdinalIgnoreCase) && (amount < 1 || amount > 200000 || decimal.Round(amount, 2) != amount))
            { Utility.ShowErrorMessage("Mobikwik accepts INR 1 to 200000 with at most two decimals."); return; }
            PaymentAttempt attempt = null;
            bool persisted = false, paymentSaved = false;
            var gateway = mode.Gateway;
            gateway.LastRequestFinal = false;
            try
            {
                RefreshCompletedPayments();
                amount = Convert.ToDecimal(txtCardRequestAmount.EditValue ?? 0);
                if (amount <= 0 || amount > billedAmount - billObj.CompletedTransactions.Sum(t => t.Amount)) return;
                if (billingRepository.GetPendingPaymentAttempts(Convert.ToInt32(billObj.BillID)).Any())
                {
                    Utility.ShowErrorMessage("An earlier payment has an uncertain status. Use Check / Link Payment before requesting another payment.");
                    return;
                }
                attempt = new PaymentAttempt { AttemptID = Guid.NewGuid(), BillID = Convert.ToInt32(billObj.BillID),
                    MopID = mode.MopID, Amount = amount, GatewayID = mode.GatewayID, GatewayType = mode.GatewayType,
                    BaseSettings = mode.BaseSettings, AdditionalSettings = mode.AdditionalSettings,
                    RequestReference = Guid.NewGuid().ToString("N") };
                if (mode.GatewayType.Equals("Mobikwik", StringComparison.OrdinalIgnoreCase))
                    attempt.ProviderReference = attempt.RequestReference;
                billingRepository.SavePaymentAttempt(attempt);
                persisted = true;
                SetBusy(true);
                cancellationTokenSource?.Dispose();
                cancellationTokenSource = new CancellationTokenSource();
                AttachGateway(gateway, attempt);
                var completed = await gateway.ReceivePayment(attempt.BillID, attempt.MopID, cancellationTokenSource.Token,
                    attempt.RequestReference, 1, mode.MopID == 2 ? PaymentMode.Card : PaymentMode.UPI, amount, Utility.loginInfo.UserID);
                if (completed != null) { CompleteTransaction(completed, attempt); paymentSaved = true; }
                else if (gateway.LastRequestFinal) billingRepository.SavePaymentAttempt(attempt, true);
                else ShowReceiveMessage("Payment is not confirmed. Check this attempt before collecting again.", true);
                gateway.ProviderReferenceReady = null;
                gateway.QrReady = null;
            }
            catch (Exception ex)
            {
                if (persisted && gateway.LastRequestFinal)
                {
                    try { billingRepository.SavePaymentAttempt(attempt, true); }
                    catch { }
                }
                AppLog.Error(ex, "Payment request failed");
                Utility.ShowErrorMessage("Payment could not be confirmed. Check / Link Payment before retrying.");
            }
            finally
            {
                gateway.ProviderReferenceReady = null;
                gateway.QrReady = null;
                gateway.RefundRequestedReady = null;
                qrForm?.Complete(); qrForm = null; SetBusy(false); Recalculate();
                if (paymentSaved) CompleteSingleGatewayBill();
            }
        }

        private async void btnLinkPayment_Click(object sender, EventArgs e)
        {
            if (busy) return;
            bool paymentSaved = false;
            try
            {
                RefreshCompletedPayments();
                var attempt = billingRepository.GetPendingPaymentAttempts(Convert.ToInt32(billObj.BillID)).FirstOrDefault();
                if (attempt == null) { Utility.ShowErrorMessage("No unresolved payment attempt was found for this bill."); return; }
                var gateway = attempt.CreateGateway();
                if (gateway == null) throw new InvalidOperationException("The original gateway configuration is unavailable.");
                gateway.StatusUpdate = ShowReceiveMessage;
                gateway.RefundRequested = attempt.RefundRequested;
                SetBusy(true);
                CompletedTransactionData completed = null;
                try { completed = await gateway.VerifyPayment(attempt.BillID, attempt.MopID, attempt.ProviderReference, attempt.Amount); }
                catch (Exception) { ShowReceiveMessage("Automatic status verification is unavailable for this attempt.", true); }
                if (completed != null) { CompleteTransaction(completed, attempt); paymentSaved = true; return; }
                if (gateway.LastRequestFinal) { billingRepository.SavePaymentAttempt(attempt, true); return; }
                if (!IsAdmin || gateway.GatewayType.Equals("Mobikwik", StringComparison.OrdinalIgnoreCase))
                {
                    Utility.ShowErrorMessage("Payment is still unconfirmed. Verify its status before any further collection.");
                    return;
                }
                var choice = XtraMessageBox.Show(this,
                    "Payment is not automatically confirmed. After checking the terminal/bank, choose Yes to link a received payment, No to confirm this attempt was not paid, or Cancel to leave it unresolved.",
                    "Resolve Payment", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3);
                if (choice == DialogResult.No)
                {
                    if (XtraMessageBox.Show(this, "Confirm no money was received for this attempt?", "Confirm Not Paid",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        billingRepository.SavePaymentAttempt(attempt, true);
                }
                else if (choice == DialogResult.Yes)
                {
                    using (var dialog = new frmAddMissingPayment(attempt, gateway))
                        if (dialog.ShowDialog(this) == DialogResult.OK)
                        { CompleteTransaction(dialog.CompletedTransactionData, attempt); paymentSaved = true; }
                }
            }
            catch (Exception ex) { Utility.ShowError(ex); }
            finally { SetBusy(false); Recalculate(); if (paymentSaved) CompleteSingleGatewayBill(); }
        }

        private void CompleteSingleGatewayBill()
        {
            if (SelectedMopID != -1 && Utility.CounterPayments.Find(SelectedMopID)?.RequiresGateway == true
                && remainingAmount == 0)
                btnApply_Click(null, null);
        }

        private void CompleteTransaction(CompletedTransactionData completed, PaymentAttempt attempt)
        {
            if (completed == null || completed.Amount != attempt.Amount || completed.MopID != attempt.MopID
                || completed.BillID != attempt.BillID || completed.PaymentGatewayID != attempt.GatewayID)
                throw new InvalidOperationException("Provider payment does not match the original attempt. Review it before continuing.");
            completed.AttemptID = attempt.AttemptID;
            billingRepository.SaveCompletedTransactionData(completed);
            if (!billObj.CompletedTransactions.Any(t => t.CompletedTransactionDataID == completed.CompletedTransactionDataID))
                billObj.CompletedTransactions.Add(completed);
            int row = gvMOP.LocateByValue("MOPID", completed.MopID);
            if (row >= 0) gvMOP.SetRowCellValue(row, "MOPVALUE", Math.Max(Received(completed.MopID), Convert.ToDecimal(gvMOP.GetRowCellValue(row, "MOPVALUE"))));
            Recalculate();
        }

        private void RefreshCompletedPayments()
        {
            billObj.CompletedTransactions = billingRepository.GetCompletedPayments(Convert.ToInt32(billObj.BillID));
            foreach (DataRow row in dtMOPs.Rows)
                row["MOPVALUE"] = Math.Max((decimal)row["MOPVALUE"], Received((int)row["MOPID"]));
            Recalculate();
        }

        private void ShowReceiveMessage(string message, bool log)
        {
            if (IsDisposed) return;
            if (InvokeRequired) { BeginInvoke(new Action(() => ShowReceiveMessage(message, log))); return; }
            txtStatus.Text += DateTime.Now.ToLongTimeString() + " - " + message + Environment.NewLine;
            txtStatus.SelectionStart = int.MaxValue;
            txtStatus.ScrollToCaret();
        }

        private void btnDiscardBill_Click(object sender, EventArgs e)
        {
            try { RefreshCompletedPayments(); }
            catch (Exception ex) { Utility.ShowError(ex); return; }
            if (billObj.CompletedTransactions.Any() || billingRepository.GetPendingPaymentAttempts(Convert.ToInt32(billObj.BillID)).Any())
            { Utility.ShowErrorMessage("Resolve existing payments before discarding this bill."); return; }
            if (XtraMessageBox.Show("Discard this bill?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            IsDiscarded = true;
            Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (busy || dtMOPs == null || dtMOPs.Rows.Count == 0) return;
            try
            {
                gvMOP.CloseEditor();
                if (!gvMOP.UpdateCurrentRow()) return;
                RefreshCompletedPayments();
                if (billingRepository.GetPendingPaymentAttempts(Convert.ToInt32(billObj.BillID)).Any())
                    throw new InvalidOperationException("Resolve pending payment attempts before closing the bill.");
                if (remainingAmount > 0) throw new InvalidOperationException($"Pending balance: {remainingAmount:0.00}");
                foreach (DataRow row in dtMOPs.Rows)
                {
                    int id = (int)row["MOPID"];
                    decimal entered = (decimal)row["MOPVALUE"];
                    if (entered < 0 || entered < Received(id)) throw new InvalidOperationException("Invalid payment allocation.");
                    if (Utility.CounterPayments.Find(id).RequiresGateway && entered != Received(id))
                        throw new InvalidOperationException($"{row["MOPNAME"]} amount does not match the received amount.");
                }
                if (billObj.CompletedTransactions.Any(t => !dtMOPs.AsEnumerable().Any(r => (int)r["MOPID"] == t.MopID)))
                    throw new InvalidOperationException("Restore the unavailable payment mode before closing this bill.");
                if (!ValidateCustomer()) return;
                var cash = dtMOPs.AsEnumerable().FirstOrDefault(r => (int)r["MOPID"] == 1);
                decimal overpaid = -Math.Min(remainingAmount, 0);
                if (overpaid > 0 && (cash == null || (decimal)cash["MOPVALUE"] < overpaid))
                    throw new InvalidOperationException("Non-cash payments exceed the bill amount.");
                billObj.TenderedCash = cash == null ? 0 : cash["MOPVALUE"];
                billObj.TenderedChange = -overpaid;
                billObj.Rounding = isCashOnly ? Math.Round(billedAmount, MidpointRounding.AwayFromZero) - billedAmount : 0;
                if (cash != null) cash["MOPVALUE"] = (decimal)cash["MOPVALUE"] - overpaid;
                billObj.IsDoorDelivery = rgSaleType.EditValue;
                billObj.CustomerName = txtCustomerName.EditValue;
                billObj.CustomerNumber = txtCustomerPhone.EditValue;
                billObj.CustomerGST = txtCustomerGST.EditValue;
                billObj.PaymentMode = SelectedMopID == -1 ? "Multiple" : Utility.CounterPayments.Find(SelectedMopID)?.Name;
                billObj.dtMopValues = dtMOPs.Copy();
                IsPaid = true;
                Close();
            }
            catch (Exception ex) { Utility.ShowErrorMessage(ex.Message); }
        }

        private bool ValidateCustomer()
        {
            bool credit = dtMOPs.AsEnumerable().Any(r => (decimal)r["MOPVALUE"] > 0
                && (Convert.ToString(r["MOPNAME"]) == "B2B Credit" || Convert.ToString(r["MOPNAME"]) == "B2C Credit"));
            bool b2b = dtMOPs.AsEnumerable().Any(r => (decimal)r["MOPVALUE"] > 0 && Convert.ToString(r["MOPNAME"]) == "B2B Credit");
            string gst = txtCustomerGST.Text;
            if (!Utility.IsValidGstin(gst) || (b2b && string.IsNullOrWhiteSpace(gst)))
            { Utility.ShowErrorMessage("Customer GST is not valid."); return false; }
            if ((credit || Convert.ToBoolean(rgSaleType.EditValue)) && (string.IsNullOrWhiteSpace(txtCustomerName.Text) || string.IsNullOrWhiteSpace(txtCustomerPhone.Text)))
            { Utility.ShowErrorMessage("Customer name and number are required."); return false; }
            if (txtCustomerPhone.Text.Length != 0 && txtCustomerPhone.Text.Length != 10)
            { Utility.ShowErrorMessage("Customer number should be 10 digits."); return false; }
            return true;
        }
    }
}
