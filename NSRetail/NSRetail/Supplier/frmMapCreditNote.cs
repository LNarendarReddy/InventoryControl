using DataAccess;
using Dropbox.Api.Files;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Supplier
{
    public partial class frmMapCreditNote : DevExpress.XtraEditors.XtraForm
    {
        private string supplerName;
        private string refType;
        private object supplierReturnsID;
        public frmMapCreditNote(object _supplierReturnsID, string _refType, string _supplierName )
        {
            InitializeComponent();
            refType = _refType;
            supplerName = _supplierName;
            supplierReturnsID = _supplierReturnsID;
            DataTable dataTable = new SupplierRepository().GetCNMappingValues(supplierReturnsID, null);
            txtTotalDNValue.EditValue = dataTable.Rows[0]["TOTALDNVALUE"];
            txtMappedDNValue.EditValue = dataTable.Rows[0]["MAPPEDDNVALUE"];
            txtBalanceDNValue.EditValue = dataTable.Rows[0]["BALANCEDNVALUETOBEMAPPED"];
            txtSupplierName.EditValue = supplerName;
            this.Text = $"Map Credit Note - {supplerName} : {supplierReturnsID}";
        }

        private void btnSelectCreditNote_Click(object sender, System.EventArgs e)
        {
            using (frmSelectCreditNote frm = new frmSelectCreditNote(supplierReturnsID, refType, null))
            {
                frm.StartPosition = FormStartPosition.CenterParent;

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DataTable dataTable = new SupplierRepository().GetCNMappingValues(supplierReturnsID, frm.SelectedCreditNoteId);
                    txtTotalDNValue.EditValue = dataTable.Rows[0]["TOTALDNVALUE"];
                    txtMappedDNValue.EditValue = dataTable.Rows[0]["MAPPEDDNVALUE"];
                    txtBalanceDNValue.EditValue = dataTable.Rows[0]["BALANCEDNVALUETOBEMAPPED"];

                    txtCreditNoteNumber.EditValue = frm.SelectedCreditNoteId;
                    txtTotalCNValue.EditValue = dataTable.Rows[0]["TOTALCNVALUE"];
                    txtMappedCNValue.EditValue = dataTable.Rows[0]["MAPPEDCNVALUE"];
                    txtBalanceCNValue.EditValue = dataTable.Rows[0]["BALANCECNVALUECANBEMAPPED"];

                    txtCNValueTobeMapped.EditValue = dataTable.Rows[0]["BALANCECNVALUECANBEMAPPED"];
                }
            }
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                new SupplierRepository().MapCreditNote(
                    supplierReturnsID, txtCreditNoteNumber.EditValue, 
                    txtCNValueTobeMapped.EditValue, Utility.UserID);
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}