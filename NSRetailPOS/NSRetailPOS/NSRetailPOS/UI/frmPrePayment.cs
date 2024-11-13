using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmPrePayment : XtraForm
    {
        BillingRepository billingRepository = new BillingRepository();

        public bool IsPaid = false;

        private Bill billObj;

        private bool canClose = true;

        public frmPrePayment(Bill bill, bool canClose = true)
        {
            InitializeComponent();
            billObj = bill;
            Text += billObj.BillNumber;
            this.canClose = canClose;
            this.CloseBox = canClose;
            this.btnCancel.Enabled = canClose;
        }

        private void frmPrePayment_Load(object sender, EventArgs e)
        {
            DataTable dtMOPs = billingRepository.GetMOPs();

            foreach (DataRow drMOP in dtMOPs.Rows)
            {
                rgPaymentModes.Properties.Items.Add(new RadioGroupItem() { Description = drMOP["MOPNAME"].ToString() });
            }

            rgPaymentModes.Properties.Items.Add(new RadioGroupItem() { Description = "Multiple" });
            rgPaymentModes.Properties.Items.ToList().ForEach(x => x.Value = x.Description);

            txtBilledAmt.EditValue = billObj.Amount;

            rgSaleType.EditValue = false;
            rgPaymentModes.EditValue = "CASH";
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if(Convert.ToBoolean(rgSaleType.EditValue) && (txtCustomerName.EditValue == null || txtCustomerPhone.EditValue == null))
            {
                XtraMessageBox.Show("Customer Name and number are required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                (txtCustomerName.EditValue == null ? txtCustomerName : txtCustomerPhone).Focus();
                return;
            }

            if(txtCustomerPhone.EditValue != null && txtCustomerPhone.EditValue.ToString().Length != 10)
            {
                XtraMessageBox.Show("Customer number should be 10 digits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCustomerPhone.Focus();
                return;
            }

            if(rgPaymentModes.EditValue.Equals("B2B Credit")
                && (txtCustomerName.EditValue == null || txtCustomerPhone.EditValue == null || txtCustomerGST.EditValue == null))
            {
                XtraMessageBox.Show("Customer Name, number & GST are required for B2B billing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                (txtCustomerGST.EditValue == null ? txtCustomerGST : null)?.Focus();
                (txtCustomerPhone.EditValue == null ? txtCustomerPhone : null)?.Focus();
                (txtCustomerName.EditValue == null ? txtCustomerName : null)?.Focus();
                return;
            }

            if (!dxValidationProvider1.Validate()) return;

            billObj.IsDoorDelivery = rgSaleType.EditValue;
            billObj.CustomerName = txtCustomerName.EditValue;
            billObj.CustomerNumber = txtCustomerPhone.EditValue;
            billObj.CustomerGST = txtCustomerGST.EditValue;
            billObj.PaymentMode = rgPaymentModes.EditValue;

            frmPayment payment = new frmPayment(billObj, canClose);
            payment.ShowDialog();
            if(payment.IsPaid)
            {
                IsPaid = true;
                Close();
            }
        }
    }
}