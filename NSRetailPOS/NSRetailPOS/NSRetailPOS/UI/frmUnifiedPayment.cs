using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using NSRetailPOS.Gateway;
using NSRetailPOS.Gateway.PineLabs;
using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmUnifiedPayment : XtraForm
    {
        BillingRepository billingRepository = new BillingRepository();
        public bool IsPaid = false, IsDiscarded = false, canClose = true, isCashOnly = false;
        private Bill billObj;
        decimal paidAmount = 0.00M, payableAmount = 0.00M, remainingAmount = 0.00M, billedAmount = 0.00M
            , cardReceivedAmount = 0.00M, upiReceivedAmount = 0.00M;

        int cashRowHandle = -1, b2bCreditRowHandle = -1, cardRowHandle = -1, upiRowHandle = -1, cardMopID, upiMopID, b2cCreditRowHandle = -1;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        DataTable dtMOPs;

        public frmUnifiedPayment(Bill bill, bool canClose = true)
        {
            InitializeComponent();
            billObj = bill;
            Text += billObj.BillNumber;
            this.canClose = canClose;
            this.CloseBox = canClose;
            this.btnCancel.Enabled = canClose;
            btnDiscardBill.Enabled = !canClose;
        }

        private void frmMultiPayment_Load(object sender, EventArgs e)
        {
            dtMOPs = billingRepository.GetMOPs();

            if (!Utility.branchInfo.BranchID.Equals(45))
            {
                foreach (DataRow drMOP in dtMOPs.Rows)
                {
                    if (drMOP["MOPNAME"].Equals("B2C Credit"))
                    {
                        drMOP.Delete();
                        dtMOPs.AcceptChanges();
                        break;
                    }
                }
            }

            foreach (DataRow drMOP in dtMOPs.Rows)
            {
                rgPaymentModes.Properties.Items.Add(new RadioGroupItem() { Description = drMOP["MOPNAME"].ToString() });
            }

            rgPaymentModes.Properties.Items.Add(new RadioGroupItem() { Description = "Multiple" });
            rgPaymentModes.Properties.Items.ToList().ForEach(x => x.Value = x.Description);

            txtBilledAmount.EditValue = billObj.Amount;
            txtItemQuantity.EditValue = billObj.Quantity;

            gcMOP.DataSource = dtMOPs.Copy();
            gcMOP.Refresh();

            txtPaymentValue.ValidateOnEnterKey = true;
            txtPaymentValue.Validating += TxtPaymentValue_Validating;
            txtPaymentValue.Leave += TxtPaymentValue_Leave;

            decimal.TryParse(billObj.Amount.ToString(), out billedAmount);
            payableAmount = billedAmount;
            remainingAmount = billedAmount;
            cashRowHandle = gvMOP.LocateByValue("MOPNAME", "Cash");
            cashRowHandle = cashRowHandle < 0 ? gvMOP.LocateByValue("MOPNAME", "CASH") : cashRowHandle;
            b2bCreditRowHandle = gvMOP.LocateByValue("MOPNAME", "B2B Credit");
            b2cCreditRowHandle = gvMOP.LocateByValue("MOPNAME", "B2C Credit");
            cardRowHandle = gvMOP.LocateByValue("MOPNAME", "CARD");
            cardRowHandle = cardRowHandle < 0 ? gvMOP.LocateByValue("MOPNAME", "Card") : cardRowHandle;
            upiRowHandle = gvMOP.LocateByValue("MOPNAME", "UPI");

            cardMopID = Convert.ToInt32(gvMOP.GetRowCellValue(cardRowHandle, "MOPID"));
            upiMopID = Convert.ToInt32(gvMOP.GetRowCellValue(upiRowHandle, "MOPID"));
            rgSaleType.EditValue = false;
            rgPaymentModes.EditValue = "CASH";

            UpdateLabels();
            SetReceivedAmounts();

            bool cardReceived = cardReceivedAmount > 0;
            bool upiReceived = upiReceivedAmount > 0;

            if (cardReceived && upiReceived)
            {
                rgPaymentModes.EditValue = "Multiple";
                gvMOP.SetRowCellValue(cardRowHandle, "MOPVALUE", cardReceivedAmount);
                gvMOP.SetRowCellValue(upiRowHandle, "MOPVALUE", upiReceivedAmount);
            }
            else if (cardReceived)
            {
                rgPaymentModes.EditValue = "CARD";
                gvMOP.SetRowCellValue(cardRowHandle, "MOPVALUE", billedAmount);
            }
            else if (upiReceived)
            {
                rgPaymentModes.EditValue = "UPI";

                gvMOP.SetRowCellValue(upiRowHandle, "MOPVALUE", billedAmount);
            }

            if (Utility.PaymentGateway != null)
            {
                Utility.PaymentGateway.StatusUpdate = ShowRecieveMessage;
                btnAddMissingPayment.Enabled = true;
            }

            Utility.SetGridFormatting(gvMOP);

            btnAddMissingPayment.Enabled = Utility.PaymentGateway != null && Utility.loginInfo.RoleName.Equals("Store Admin");
        }

        private void TxtPaymentValue_Leave(object sender, EventArgs e)
        {
            BaseEdit baseEdit = sender as BaseEdit;
            string textValue = baseEdit?.EditValue != null ? baseEdit.EditValue.ToString() : string.Empty;
            if (!decimal.TryParse(textValue, out decimal enteredvalue)) return;

            if (gvMOP.FocusedRowHandle == cardRowHandle) SetReceiveValues(cardRowHandle, enteredvalue);
            if (gvMOP.FocusedRowHandle == upiRowHandle) SetReceiveValues(upiRowHandle, enteredvalue);
        }

        private void btnDiscardBill_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure you want to discard bill?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            IsDiscarded = true;
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(rgSaleType.EditValue) && 
                (string.IsNullOrEmpty(txtCustomerName.EditValue?.ToString()) || string.IsNullOrEmpty(txtCustomerPhone.EditValue?.ToString())))
            {
                XtraMessageBox.Show("Customer Name and number are required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                (txtCustomerName.EditValue == null ? txtCustomerName : txtCustomerPhone).Focus();
                return;
            }

            if (!string.IsNullOrEmpty(txtCustomerPhone.EditValue?.ToString()) && txtCustomerPhone.EditValue.ToString().Length != 10)
            {
                XtraMessageBox.Show("Customer number should be 10 digits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCustomerPhone.Focus();
                return;
            }

            if (remainingAmount > 0.00M)
            {
                XtraMessageBox.Show($"Bill cannot be closed. Pending balance to be paid {remainingAmount}"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if(!ValidateCreditFields()) return;

            billObj.IsDoorDelivery = rgSaleType.EditValue;
            billObj.CustomerName = txtCustomerName.EditValue;
            billObj.CustomerNumber = txtCustomerPhone.EditValue;
            billObj.CustomerGST = txtCustomerGST.EditValue;
            billObj.PaymentMode = rgPaymentModes.EditValue;

            decimal rounding = 0;
            if (decimal.TryParse(gvMOP.GetRowCellValue(cashRowHandle, "MOPVALUE").ToString(), out decimal cashValue) && cashValue > 0)
            {
                decimal tempRrounding = billObj.PaymentMode.Equals("CASH") ? Math.Round(billedAmount, MidpointRounding.AwayFromZero) - billedAmount : 0.00M;
                rounding = tempRrounding > 0 ? 1 - tempRrounding : tempRrounding;
                billObj.Rounding = tempRrounding;
                billObj.TenderedCash = cashValue;
                billObj.TenderedChange = remainingAmount;
                gvMOP.FocusedRowHandle = cashRowHandle;
                gvMOP.SetRowCellValue(cashRowHandle, "MOPVALUE", cashValue + remainingAmount);
                gvMOP.CloseEditor();
                gvMOP.UpdateCurrentRow();
            }

            gcMOP.EndUpdate();
            gcMOP.RefreshDataSource();

            if (Utility.PaymentGateway != null)
            {
                if (decimal.TryParse(gvMOP.GetRowCellValue(cardRowHandle, "MOPVALUE")?.ToString(), out decimal cardEnteredValue)
                    && cardEnteredValue != cardReceivedAmount)
                {
                    XtraMessageBox.Show("Card amount is not matching with card received amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (decimal.TryParse(gvMOP.GetRowCellValue(upiRowHandle, "MOPVALUE")?.ToString(), out decimal upiEnteredValue)
                    && upiEnteredValue != upiReceivedAmount)
                {
                    XtraMessageBox.Show("UPI amount is not matching with UPI received amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            billObj.dtMopValues = dtMOPs;

            // force refresh values
            int i = 0;
            decimal totalPaid = 0;

            foreach (DataRow dr in billObj.dtMopValues.Rows)
            {
                int rowHandle = gvMOP.GetRowHandle(i);
                dr["MOPVALUE"] = gvMOP.GetRowCellValue(rowHandle, "MOPVALUE");
                i++;
                if (decimal.TryParse(dr["MOPVALUE"]?.ToString(), out decimal value) && value > 0)
                {
                    totalPaid += value;
                }
            }

            if (totalPaid < payableAmount + (rounding > 0 ? 1 - rounding : rounding))
            {
                XtraMessageBox.Show("Payment mode mismatch detected, please retry operation of if the issue persists, contact administrator", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            IsPaid = true;
            Close();
        }

        private void rgPaymentModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCardRequestAmount.EditValue = null;
            txtUPIRequestAmount.EditValue = null;

            isCashOnly = rgPaymentModes.Text.Equals("CASH");

            for (int rowhandle = 0; rowhandle < gvMOP.RowCount; rowhandle++)
            {
                object setValue;

                if (rowhandle == cardRowHandle)
                    setValue = txtCardRecievedAmount.EditValue;
                else if (rowhandle == upiRowHandle)
                    setValue = txtUPIReceiveAmount.EditValue;
                else
                    setValue = 0;

                gvMOP.SetRowCellValue(rowhandle, "MOPVALUE", setValue ?? 0);
            }

            if (!rgPaymentModes.Text.Equals("Multiple") && !rgPaymentModes.Text.Equals("CASH"))
            {
                int rowhandle = gvMOP.LocateByValue("MOPNAME", rgPaymentModes.Text);
                gvMOP.SetRowCellValue(rowhandle, "MOPVALUE", billObj.Amount);
                gvMOP.FocusedRowHandle = rowhandle;
                SetReceiveValues(rowhandle, Convert.ToDecimal(billObj.Amount));
            }
            else
            {
                gvMOP.FocusedRowHandle = cashRowHandle;
            }
        }

        private void rgPaymentModes_Leave(object sender, EventArgs e)
        {
            
        }

        private void gcMOP_Enter(object sender, EventArgs e)
        {
            if (rgPaymentModes.EditValue == null) rgPaymentModes.Focus();

            if (rgPaymentModes.Text.Equals("Multiple") || rgPaymentModes.Text.Equals("CASH"))
            {
                gvMOP.FocusedColumn = gvMOP.Columns[2];
                gvMOP.FocusedRowHandle = cashRowHandle;
            }
            else if (Utility.PaymentGateway == null)
            {
                btnApply.Focus();
            }
            else if (decimal.TryParse(gvMOP.GetRowCellValue(cardRowHandle, "MOPVALUE").ToString(), out decimal cardValue) && cardValue > 0)
            {
                btnReceiveCard.Focus();
            }
            else if (decimal.TryParse(gvMOP.GetRowCellValue(upiRowHandle, "MOPVALUE").ToString(), out decimal upiValue) && upiValue > 0)
            {
                btnUPIReceive.Focus();
            }
        }

        private void btnReceiveCard_Click(object sender, EventArgs e)
        {
            ReceiveAmount(cardMopID, PaymentMode.Card, txtCardRequestAmount.EditValue);
        }

        private void btnUPIReceive_Click(object sender, EventArgs e)
        {
            ReceiveAmount(upiMopID, PaymentMode.UPI, txtUPIRequestAmount.EditValue);
        }

        private void TxtPaymentValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BaseEdit baseEdit = sender as BaseEdit;
            string textValue = baseEdit?.EditValue != null ? baseEdit.EditValue.ToString() : string.Empty;
            if (textValue.Length > 4 && XtraMessageBox.Show($"Large number detected, possible barcode scan detected, Do you want to accept the value {textValue}"
                    , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            if (Utility.PaymentGateway != null)
            {
                decimal.TryParse(textValue, out decimal enteredValue);

                if (gvMOP.FocusedRowHandle == cardRowHandle && enteredValue < cardReceivedAmount)
                {
                    baseEdit.EditValue = cardReceivedAmount;
                    XtraMessageBox.Show($"Card value cannot be less than {cardReceivedAmount}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (gvMOP.FocusedRowHandle == upiRowHandle && enteredValue < upiReceivedAmount)
                {
                    baseEdit.EditValue = upiReceivedAmount;
                    XtraMessageBox.Show($"UPI value cannot be less than {upiReceivedAmount}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void GvMOP_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.State.HasFlag(DevExpress.XtraGrid.Views.Base.GridRowCellState.FocusedAndGridFocused))
                gvMOP.ShowEditor();
        }

        private void btnLinkPayment_Click(object sender, EventArgs e)
        {
            frmAddMissingPayment frmAddMissingPayment = new(Convert.ToInt32(billObj.BillID), cardMopID, upiMopID);
            frmAddMissingPayment.ShowDialog();

            if (frmAddMissingPayment.DialogResult != DialogResult.OK) return;

            CompleteTransaction(frmAddMissingPayment.CompletedTransactionData);
        }

        private void rgPaymentModes_EditValueChanging(object sender, ChangingEventArgs e)
        {
            if (cardReceivedAmount > 0 && upiReceivedAmount > 0 && e.NewValue.ToString() != "Multiple")
            {
                XtraMessageBox.Show("Only multiple payment mode allowed");
                e.Cancel = true;
                return;
            }

            if (e.NewValue.ToString() == "Multiple")
            {
                // if multiple allow  it
                return;
            }

            if (cardReceivedAmount > 0 && e.NewValue.ToString() != "CARD")
            {
                XtraMessageBox.Show("Only multiple or card payment mode allowed");
                e.Cancel = true;
            }
            else if (upiReceivedAmount > 0 && e.NewValue.ToString() != "UPI")
            {
                XtraMessageBox.Show("Only multiple or UPI payment mode allowed");
                e.Cancel = true;
            }
        }
                
        private void UpdateLabels()
        {
            txtPaidAmount.EditValue = paidAmount;
            decimal roundedRemaining = remainingAmount < 0.00m ? 0 : remainingAmount;
            remainingAmount = isCashOnly ? Math.Round(remainingAmount, MidpointRounding.AwayFromZero) : remainingAmount;
            txtRemainingAmount.EditValue = $"{remainingAmount} ( Rounded value : {roundedRemaining} )";
        }

        private void gvMOP_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gcMOP.RefreshDataSource();
            gvMOP.RefreshData();
            
            paidAmount = 0;

            decimal.TryParse(gvMOP.Columns["MOPVALUE"].SummaryItem.SummaryValue.ToString(), out paidAmount);

            if (Utility.PaymentGateway != null)
            {
                paidAmount -= Convert.ToDecimal(gvMOP.GetRowCellValue(cardRowHandle, "MOPVALUE"));
                paidAmount -= Convert.ToDecimal(gvMOP.GetRowCellValue(upiRowHandle, "MOPVALUE"));

                paidAmount += cardReceivedAmount + upiReceivedAmount;
            }
            
            remainingAmount = (isCashOnly ? Math.Round(payableAmount, MidpointRounding.AwayFromZero) : payableAmount) - paidAmount;
            UpdateLabels();
        }

        private void btnCancelRequest_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel();
            btnCancelRequest.Enabled = false;
        }

        private void gvMOP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }
        }

        private void gvMOP_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var selectedPayment = rgPaymentModes.EditValue;
            if (selectedPayment.Equals("Multiple")
                || (selectedPayment.Equals("CASH") && gvMOP.GetRowCellValue(gvMOP.FocusedRowHandle, "MOPNAME").Equals("CASH")))
            {
                return;
            }
            e.Cancel = true;
        }

        private void SetReceiveValues(int rowHandle, decimal amount)
        {
            if (Utility.PaymentGateway == null) return;

            if (rowHandle == cardRowHandle)
            {
                txtCardRequestAmount.EditValue = amount > cardReceivedAmount ? amount - cardReceivedAmount : 0;
            }
            else if (rowHandle == upiRowHandle)
            {
                txtUPIRequestAmount.EditValue = amount > upiReceivedAmount ? amount - upiReceivedAmount : 0;
            }
            
            EnableDisableReceives();
        }

        private void EnableDisableReceives()
        {
            bool cardEnabled = decimal.TryParse(txtCardRequestAmount.EditValue?.ToString(), out decimal cardValue) && cardValue > 0;
            bool upiEnabled = decimal.TryParse(txtUPIRequestAmount.EditValue?.ToString(), out decimal upiValue) && upiValue > 0;

            txtCardRequestAmount.Enabled = cardEnabled;
            btnReceiveCard.Enabled = cardEnabled;

            txtUPIRequestAmount.Enabled = upiEnabled;
            btnUPIReceive.Enabled = upiEnabled;
        }

        private void DisableAllControls()
        {
            txtCustomerName.Enabled = false;
            txtCustomerPhone.Enabled = false;
            txtCustomerGST.Enabled = false;
            rgSaleType.Enabled = false;
            rgPaymentModes.Enabled = false;
            gcMOP.Enabled = false;
            txtCardRequestAmount.Enabled = false;
            txtUPIRequestAmount.Enabled = false;
            btnReceiveCard.Enabled = false;
            btnUPIReceive.Enabled = false;
            btnDiscardBill.Enabled = false;
            btnApply.Enabled = false;
            btnCancel.Enabled = false;
        }

        private void EnableAllControls()
        {
            txtCustomerName.Enabled = true;
            txtCustomerPhone.Enabled = true;
            txtCustomerGST.Enabled = true;
            rgSaleType.Enabled = true;
            rgPaymentModes.Enabled = true;
            gcMOP.Enabled = true;
            btnApply.Enabled = true;

            this.btnCancel.Enabled = canClose;
            btnDiscardBill.Enabled = !canClose;

            EnableDisableReceives();
        }

        private async Task ReceiveAmount(int mopID, PaymentMode paymentMode, object amountObj)
        {
            if (!decimal.TryParse(amountObj.ToString(), out decimal amount) || amount == 0) return;

            if (Utility.PaymentGateway.IsInProgress)
            {
                XtraMessageBox.Show("Request in progress", "multiple requests", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            DisableAllControls();
            btnCancelRequest.Enabled = true;
            
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            CompletedTransactionData completedTransactionData = await Utility.PaymentGateway.ReceivePayment(
                Convert.ToInt32(billObj.BillID), mopID, cancellationToken
               , $"{billObj.BillNumber}-{billObj.CompletedTransactions.Count + 1}"
               , 1, paymentMode, amount, Utility.loginInfo.UserID);

            btnCancelRequest.Enabled = false;
            EnableAllControls();

            CompleteTransaction(completedTransactionData);
        }

        private void ShowRecieveMessage(string message)
        {
            txtStatus.Text += $"{DateTime.Now.ToLongTimeString()} - {message} {Environment.NewLine}";
            txtStatus.SelectionStart = int.MaxValue;
            txtStatus.ScrollToCaret();
        }

        private void SetReceivedAmounts()
        {
            if (Utility.PaymentGateway == null ||
                billObj.CompletedTransactions == null 
                || !billObj.CompletedTransactions.Any()) 
                    return;

            cardReceivedAmount = billObj.CompletedTransactions.Where(x => x.MopID == cardMopID).Sum(x => x.Amount);
            upiReceivedAmount = billObj.CompletedTransactions.Where(x => x.MopID == upiMopID).Sum(x => x.Amount);
            txtCardRecievedAmount.EditValue = cardReceivedAmount;
            txtUPIReceiveAmount.EditValue = upiReceivedAmount;

            decimal.TryParse(gvMOP.GetRowCellValue(cardRowHandle, "MOPVALUE").ToString(), out decimal cardEnteredAmount);
            SetReceiveValues(cardRowHandle, cardEnteredAmount);

            decimal.TryParse(gvMOP.GetRowCellValue(upiRowHandle, "MOPVALUE").ToString(), out decimal upiEnteredAmount);
            SetReceiveValues(upiRowHandle, upiEnteredAmount);

            gvMOP_CellValueChanged(null, null);

            // complete billing if it is single payment and amount is received
            if(billedAmount == paidAmount && 
                (rgPaymentModes.Text.Equals("card", StringComparison.OrdinalIgnoreCase)
                    || rgPaymentModes.Text.Equals("UPI", StringComparison.OrdinalIgnoreCase)))
            {
                btnApply_Click(null, null);
            }
        }

        private void CompleteTransaction(CompletedTransactionData completedTransactionData)
        {
            if (completedTransactionData == null) return;

            billingRepository.SaveCompletedTransactionData(completedTransactionData);
            billObj.CompletedTransactions.Add(completedTransactionData);

            SetReceivedAmounts();
        }

        private bool ValidateCreditFields()
        {
            if ((!decimal.TryParse(gvMOP.GetRowCellValue(b2bCreditRowHandle, "MOPVALUE")?.ToString(), out decimal b2bCreditValue) || b2bCreditValue == 0)
                && (!decimal.TryParse(gvMOP.GetRowCellValue(b2cCreditRowHandle, "MOPVALUE")?.ToString(), out decimal b2cCreditValue) || b2cCreditValue == 0))
                return true;

            if (string.IsNullOrEmpty(txtCustomerName.EditValue?.ToString())
               || string.IsNullOrEmpty(txtCustomerPhone.EditValue?.ToString()))
            {
                XtraMessageBox.Show("Customer Name & number are required for credit billing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                (string.IsNullOrEmpty(txtCustomerName.EditValue?.ToString()) ? txtCustomerName : null)?.Focus();
                (string.IsNullOrEmpty(txtCustomerPhone.EditValue?.ToString()) ? txtCustomerPhone : null)?.Focus();
                return false;
            }

            if (b2bCreditValue == 0) return true;

            string gstNumber = txtCustomerGST.EditValue?.ToString();
            if (string.IsNullOrEmpty(gstNumber) || gstNumber.Length != 15
                || !int.TryParse(gstNumber.Substring(0, 2), out int stateCode) || stateCode == 0)
            {
                XtraMessageBox.Show("Customer GST is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCustomerGST.SelectAll();
                txtCustomerGST.Focus();
                return false;
            }

            return true;
        }
    }
}