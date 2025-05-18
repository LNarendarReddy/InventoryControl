using DevExpress.Data.ExpressionEditor;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid.Views.Grid;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using NSRetailPOS.Gateway;
using NSRetailPOS.Gateway.PineLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmUnifiedPayment : DevExpress.XtraEditors.XtraForm
    {
        BillingRepository billingRepository = new BillingRepository();
        public bool IsPaid = false, IsDiscarded = false, canClose = true;
        private Bill billObj;
        decimal paidAmount = 0.00M, payableAmount = 0.00M, remainingAmount = 0.00M, billedAmount = 0.00M;

        int cashRowHandle = -1, b2bCreditRowHandle = -1, cardRowHandle = -1, upiRowHandle = -1, cardMopID, upiMopID;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

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
            DataTable dtMOPs = billingRepository.GetMOPs();

            foreach (DataRow drMOP in dtMOPs.Rows)
            {
                rgPaymentModes.Properties.Items.Add(new RadioGroupItem() { Description = drMOP["MOPNAME"].ToString() });
            }

            rgPaymentModes.Properties.Items.Add(new RadioGroupItem() { Description = "Multiple" });
            rgPaymentModes.Properties.Items.ToList().ForEach(x => x.Value = x.Description);

            txtBilledAmount.EditValue = billObj.Amount;
            txtItemQuantity.EditValue = billObj.Quantity;

            gcMOP.DataSource = dtMOPs;
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
            cardRowHandle = gvMOP.LocateByValue("MOPNAME", "CARD");
            cardRowHandle = cardRowHandle < 0 ? gvMOP.LocateByValue("MOPNAME", "Card") : cardRowHandle;
            upiRowHandle = gvMOP.LocateByValue("MOPNAME", "UPI");

            cardMopID = Convert.ToInt32(gvMOP.GetRowCellValue(cardRowHandle, "MOPID"));
            upiMopID = Convert.ToInt32(gvMOP.GetRowCellValue(upiRowHandle, "MOPID"));
            rgSaleType.EditValue = false;
            rgPaymentModes.EditValue = "CASH";

            UpdateLabels();
            SetReceivedAmounts();

            if (Utility.PaymentGateway != null)
            {
                Utility.PaymentGateway.StatusUpdate = ShowRecieveMessage;
            }

            Utility.SetGridFormatting(gvMOP);
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
            gcMOP.RefreshDataSource();

            if (Convert.ToBoolean(rgSaleType.EditValue) && (txtCustomerName.EditValue == null || txtCustomerPhone.EditValue == null))
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

            if (rgPaymentModes.EditValue.Equals("B2B Credit")
                && (txtCustomerName.EditValue == null || txtCustomerPhone.EditValue == null || txtCustomerGST.EditValue == null))
            {
                XtraMessageBox.Show("Customer Name, number & GST are required for B2B billing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                (txtCustomerGST.EditValue == null ? txtCustomerGST : null)?.Focus();
                (txtCustomerPhone.EditValue == null ? txtCustomerPhone : null)?.Focus();
                (txtCustomerName.EditValue == null ? txtCustomerName : null)?.Focus();
                return;
            }

            if (Math.Round(remainingAmount) > 0.00M)
            {
                XtraMessageBox.Show($"Bill cannot be closed. Pending balance to be paid {remainingAmount}"
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (decimal.TryParse(gvMOP.GetRowCellValue(b2bCreditRowHandle, "MOPVALUE").ToString(), out decimal b2bCreditValue) && b2bCreditValue > 0
                && (txtCustomerName.EditValue == null || txtCustomerPhone.EditValue == null || txtCustomerGST.EditValue == null))
            {
                XtraMessageBox.Show("Customer Name, number & GST are required for B2B billing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                (txtCustomerGST.EditValue == null ? txtCustomerGST : null)?.Focus();
                (txtCustomerPhone.EditValue == null ? txtCustomerPhone : null)?.Focus();
                (txtCustomerName.EditValue == null ? txtCustomerName : null)?.Focus();
                return;
            }

            gcMOP.Refresh();
            billObj.IsDoorDelivery = rgSaleType.EditValue;
            billObj.CustomerName = txtCustomerName.EditValue;
            billObj.CustomerNumber = txtCustomerPhone.EditValue;
            billObj.CustomerGST = txtCustomerGST.EditValue;
            billObj.PaymentMode = rgPaymentModes.EditValue;

            if (decimal.TryParse(gvMOP.GetRowCellValue(cashRowHandle, "MOPVALUE").ToString(), out decimal cashValue) && cashValue > 0)
            {
                billObj.Rounding = billObj.PaymentMode.Equals("CASH") ? Math.Round(billedAmount) - billedAmount : 0.00M;
                billObj.TenderedCash = cashValue;
                billObj.TenderedChange = remainingAmount + Math.Round(billedAmount) - billedAmount;
                gvMOP.FocusedRowHandle = cashRowHandle;
                gvMOP.SetRowCellValue(cashRowHandle, "MOPVALUE", cashValue + Math.Round(remainingAmount));
                gvMOP.CloseEditor();
                gvMOP.UpdateCurrentRow();
            }

            billObj.dtMopValues = (gvMOP.DataSource as DataView).Table;

            IsPaid = true;
            Close();
        }

        private void rgPaymentModes_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int rowhandle = 0; rowhandle < gvMOP.RowCount; rowhandle++)
            {
                gvMOP.SetRowCellValue(rowhandle, "MOPVALUE", 0);
            }

            SetReceiveValues(cardRowHandle, 0);
            SetReceiveValues(upiRowHandle, 0);

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
            }
        }

        private void GvMOP_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.State.HasFlag(DevExpress.XtraGrid.Views.Base.GridRowCellState.FocusedAndGridFocused))
                gvMOP.ShowEditor();
        }

        private void UpdateLabels()
        {
            txtPaidAmount.EditValue = paidAmount;
            decimal roundedRemaining = remainingAmount < 0.00m ? 0 : Math.Round(remainingAmount);
            txtRemainingAmount.EditValue = $"{Math.Round(remainingAmount)} ( Rounded value : {roundedRemaining} )";
        }
        
        private void gvMOP_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            gvMOP.RefreshData();
            decimal.TryParse(gvMOP.Columns["MOPVALUE"].SummaryItem.SummaryValue.ToString(), out paidAmount);
            remainingAmount = payableAmount - paidAmount;
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
                decimal.TryParse(txtCardRecievedAmount.EditValue?.ToString(), out decimal cardReceivedAmount);
                txtCardRequestAmount.EditValue = amount - cardReceivedAmount; 
            }
            else if (rowHandle == upiRowHandle)
            {
                decimal.TryParse(txtUPIReceiveAmount.EditValue?.ToString(), out decimal upiReceivedAmount);
                txtUPIRequestAmount.EditValue = amount - upiReceivedAmount;
            }
            
            EnableDisableReceives();
        }

        private void EnableDisableReceives()
        {
            bool cardEnabled = decimal.TryParse(gvMOP.GetRowCellValue(cardRowHandle, "MOPVALUE").ToString(), out decimal cardValue) && cardValue > 0;
            bool upiEnabled = decimal.TryParse(gvMOP.GetRowCellValue(upiRowHandle, "MOPVALUE").ToString(), out decimal upiValue) && upiValue > 0;

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
            DisableAllControls();
            btnCancelRequest.Enabled = true;
            
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

             CompletedTransactionData completedTransactionData = await Utility.PaymentGateway.ReceivePayment(
                 Convert.ToInt32(billObj.BillID), mopID, cancellationToken
                , billObj.BillNumber.ToString(), 1, paymentMode, amount, Utility.loginInfo.UserID);

            btnCancelRequest.Enabled = false;
            EnableAllControls();

            if (completedTransactionData == null) return;

            billingRepository.SaveCompletedTransactionData(completedTransactionData);
            billObj.CompletedTransactions.Add(completedTransactionData);

            SetReceivedAmounts();
        }

        private void ShowRecieveMessage(string message)
        {
            txtStatus.Text += $"{DateTime.Now.ToLongTimeString()} - {message} {Environment.NewLine}";
            txtStatus.SelectionStart = Int32.MaxValue;
            txtStatus.ScrollToCaret();
        }

        private void SetReceivedAmounts()
        {
            if (billObj.CompletedTransactions == null || !billObj.CompletedTransactions.Any()) return;

            txtCardRecievedAmount.EditValue = billObj.CompletedTransactions.Where(x => x.MopID == cardMopID).Sum(x => x.Amount);
            txtUPIReceiveAmount.EditValue = billObj.CompletedTransactions.Where(x => x.MopID == upiMopID).Sum(x => x.Amount);

            decimal.TryParse(gvMOP.GetRowCellValue(cardRowHandle, "MOPVALUE").ToString(), out decimal cardEnteredAmount);
            SetReceiveValues(cardRowHandle, cardEnteredAmount);

            decimal.TryParse(gvMOP.GetRowCellValue(upiRowHandle, "MOPVALUE").ToString(), out decimal upiEnteredAmount);
            SetReceiveValues(cardRowHandle, upiEnteredAmount);
        }
    }
}