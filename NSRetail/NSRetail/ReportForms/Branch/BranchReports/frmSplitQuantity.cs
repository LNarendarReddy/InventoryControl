using DataAccess;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.BranchReports
{
    public partial class frmSplitQuantity : DevExpress.XtraEditors.XtraForm
    {
        SplitQuantity splitQuantity = null;
        public frmSplitQuantity(SplitQuantity _splitQuantity)
        {
            InitializeComponent();
            splitQuantity = _splitQuantity;
            txtBaseQuantity.EditValue = splitQuantity.BaseQuantity;
            cmbBaseReason.EditValue = splitQuantity.BaseReason;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate()) return;

                if (cmbBaseReason.EditValue.Equals(cmbDevidedReason.EditValue))
                    throw new Exception("Base and Devided reasons cannot be same");

                if (txtBaseQuantity.EditValue.Equals(0) || txtDevidedQuantity.EditValue.Equals(0))
                    throw new Exception("Base or Devided quantity cannot be 0");

                if (!splitQuantity.BaseQuantity.Equals(Convert.ToInt16(txtBaseQuantity.EditValue)
                    + Convert.ToInt16(txtDevidedQuantity.EditValue)))
                    throw new Exception("Sum of quantities should match with base quantity");

                splitQuantity.BaseQuantity = txtBaseQuantity.EditValue;
                splitQuantity.BaseReason = cmbBaseReason.EditValue;
                splitQuantity.DevidedQuantity = txtDevidedQuantity.EditValue;
                splitQuantity.DevidedReason = cmbDevidedReason.EditValue;
                splitQuantity._IsSave = true;
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSplitQuantity_Load(object sender, EventArgs e)
        {
            cmbBaseReason.Properties.DataSource = new SupplierRepository().GetReason();
            cmbBaseReason.Properties.ValueMember = "REASONID";
            cmbBaseReason.Properties.DisplayMember = "REASONNAME";

            cmbDevidedReason.Properties.DataSource = new SupplierRepository().GetReason();
            cmbDevidedReason.Properties.ValueMember = "REASONID";
            cmbDevidedReason.Properties.DisplayMember = "REASONNAME";

            txtDevidedQuantity.EditValue = 0;
        }
    }
    public class SplitQuantity
    {
        public SplitQuantity() { }
        public object BaseQuantity {  get; set; }
        public object DevidedQuantity { get; set; }
        public object BaseReason { get; set; }
        public object DevidedReason { get; set; }
        public bool _IsSave { get; set; }
    }
}