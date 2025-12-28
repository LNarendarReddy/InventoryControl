using DataAccess;
using NSRetail.Supplier;
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
                { "View", "8F6C3E2D-4B7A-4A9E-BF2C-9E3A1D6C5F42" },
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
            if (buttonText == "View")
            {
                frmCreditNote frm = new frmCreditNote(
                    Convert.ToInt32(drFocusedRow["CreditNoteId"])
                );
                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frm.ShowDialog();
            }
        }
    }
}
