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
        public frmBillOverrideOptions(object customerName, object customerNumber)
        {
            InitializeComponent();
            CustomerName = customerName;
            CustomerNumber = customerNumber;
        }

        public object CustomerName { get { return txtCustomerName.EditValue; } set { txtCustomerName.EditValue = value; } }
        public object CustomerNumber { get { return txtCustomerNumber.EditValue; } set { txtCustomerNumber.EditValue = value; } }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}