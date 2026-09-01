using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.POSReports
{
    public partial class frmBillOverrideOptions : DevExpress.XtraEditors.XtraForm
    {
        private readonly bool showAddressOptions;

        public frmBillOverrideOptions(object customerName, object customerNumber, object customerAddress = null, bool showCustomerAddress = false)
        {
            InitializeComponent();
            showAddressOptions = showCustomerAddress;
            CustomerName = customerName;
            CustomerNumber = customerNumber;
            CustomerAddress = customerAddress;
            chkSameAsBillTo.Checked = true;
            UpdateShipToFromBillTo();
            SetAddressOptionsVisibility();
            chkSameAsBillTo_CheckedChanged(this, EventArgs.Empty);
        }

        public object CustomerName { get { return txtCustomerName.EditValue; } set { txtCustomerName.EditValue = value; } }
        public object CustomerNumber { get { return txtCustomerNumber.EditValue; } set { txtCustomerNumber.EditValue = value; } }
        public object CustomerAddress { get { return txtCustomerAddress.EditValue; } set { txtCustomerAddress.EditValue = value; } }
        public object ShipToName { get { return txtShipToName.EditValue; } set { txtShipToName.EditValue = value; } }
        public object ShipToNumber { get { return txtShipToNumber.EditValue; } set { txtShipToNumber.EditValue = value; } }
        public object ShipToAddress { get { return txtShipToAddress.EditValue; } set { txtShipToAddress.EditValue = value; } }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void BillTo_EditValueChanged(object sender, EventArgs e)
        {
            if (chkSameAsBillTo.Checked)
                UpdateShipToFromBillTo();
        }

        private void chkSameAsBillTo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSameAsBillTo.Checked)
                UpdateShipToFromBillTo();

            txtShipToName.Properties.ReadOnly = chkSameAsBillTo.Checked;
            txtShipToNumber.Properties.ReadOnly = chkSameAsBillTo.Checked;
            txtShipToAddress.Properties.ReadOnly = chkSameAsBillTo.Checked;
        }

        private void UpdateShipToFromBillTo()
        {
            ShipToName = CustomerName;
            ShipToNumber = CustomerNumber;
            ShipToAddress = CustomerAddress;
        }

        private void SetAddressOptionsVisibility()
        {
            DevExpress.XtraLayout.Utils.LayoutVisibility visibility = showAddressOptions
                ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            lciCustomerAddress.Visibility = visibility;
            lciSameAsBillTo.Visibility = visibility;
            lcgShipTo.Visibility = visibility;
            ClientSize = showAddressOptions ? new Size(560, 430) : new Size(390, 127);
        }
    }
}
