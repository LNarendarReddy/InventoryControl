using DataAccess;
using NSRetail.Supplier;
using DevExpress.XtraEditors;
using ErrorManagement;
using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Supplier.SupplierReports
{
    public partial class ucCreditNoteList : SearchCriteriaBase
    {
        public ucCreditNoteList()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "CreditNoteId", "CN ID" },
                { "CNNumber", "CN Number" },
                { "DEALERNAME", "Supplier" },
                { "SUPPLIERINVOICENO", "Invoice #" },
                { "AdjustmentType", "Adjustment Type" },
                { "CreditValue", "CN Value" },
                { "CreatedDate", "Created Date" },
                { "SupplierId", "Supplier Id" },
                { "StockEntryId", "Invoice Id" }
            };

            ContextmenuItems = new Dictionary<string, string>
            {
                //{ "View", "8F6C3E2D-4B7A-4A9E-BF2C-9E3A1D6C5F42" },
                { "Edit", "A3F8C6E2-9B4D-4A71-9F2B-6C1D8F5A4E9A" },
                { "Delete", "A9E3D4F6-2C7B-4F81-9B5E-6D0C8A1F27E4" }
            };

            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer(false);
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";
            cmbSupplier.EditValue = 0;
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbSupplier, dtpToDate, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@SupplierId", cmbSupplier.EditValue },
                { "@FromDate", dtpFromDate.EditValue },
                { "@ToDate", dtpToDate.EditValue }
            };

            return GetReportData("usp_R_CreditNote_List", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            try
            {
                if (buttonText == "View")
                {
                    frmCreditNote frm = new frmCreditNote(
                        Convert.ToInt32(drFocusedRow["CreditNoteId"])
                    );
                    frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                }
                else if (buttonText == "Edit")
                {
                    if (drFocusedRow == null) return;
                    var idObj = drFocusedRow["CreditNoteId"];
                    if (idObj == null || idObj == DBNull.Value) return;

                    frmCreditNote frm = new frmCreditNote(
                        Convert.ToInt32(idObj)
                    );
                    frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        (ParentForm as frmReportPlaceHolder)?.btnSearch_Click(null, null);
                    }
                }
                else if (buttonText == "Delete")
                {
                    if (drFocusedRow == null) return;
                    var idObj = drFocusedRow["CreditNoteId"];
                    if (idObj == null || idObj == DBNull.Value) return;

                    if (XtraMessageBox.Show("Are you sure you want to discard this credit note?", 
                            "Confirm", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes)
                        return;

                    new CreditNoteRepository().DeleteCreditNote(idObj, Utility.UserID);
                    XtraMessageBox.Show("Credit note discarded successfully", "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    (ParentForm as frmReportPlaceHolder)?.btnSearch_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
            }
        }
    }
}
