using DataAccess;
using DevExpress.Utils.Text.Internal;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Entity;
using NSRetail.Reports;
using NSRetail.Stock;
using NSRetail.Supplier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Supplier.SupplierReports
{
    public partial class ucInvoiceList : SearchCriteriaBase
    {       
        public ucInvoiceList()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "SNO", "S No" }
                , { "STOCKENTRYID", "Stock Entry ID" }
                , { "SUPPLIERINVOICENO", "Invoice Number" }
                , { "DEALERNAME", "Supplier" }
                , { "GSTIN", "Supplier GSTIN" }
                , { "CATEGORYNAME", "Category" }
                , { "INVOICEDATE", "Invoice Date" }
                , { "CREATEDBY", "User Name" }
                , { "CREATEDDATE", "Created Date" }
                , { "FINALPRICE", "Net Amount" }
                , { "STATUS", "Status" }
            };

            ContextmenuItems = new Dictionary<string, string>
            {
                { "View Items", "3A365A22-5334-4667-8782-A83825298BF6" },
                { "Print", "3F9A7C21-8E6D-4B2F-A5D9-1C7E9F4B2A6C" },
                { "Edit", "DED7050B-F5CF-4944-8880-008A87F1D987" },
                { "Revert", "7CE473E7-514B-4BC9-B07E-2B26B5AA44F2" },
                { "Clone", "50C463EA-A4BE-49A8-8484-B2C73186A373" },
                { "Verify and Submit", "6B8301F8-D835-4F21-9ED2-F6939EAF1552" },
                { "Dispatch to branch", "A3F813DC-3E42-407A-A3B8-1CC84ADC684C" },
                { "View Credit Note Mapping", "B4F924ED-7C53-4A78-9D6C-2F95B6EE5A9E" }
            };
                

            HiddenColumns = new List<string>() { "CATEGORYNAME", "CREATEDBY", "CREATEDDATE", "GSTIN" };

            cmbDealer.Properties.DataSource = new MasterRepository().GetDealer(true);
            cmbDealer.Properties.DisplayMember = "DEALERNAME";
            cmbDealer.Properties.ValueMember = "DEALERID";
            cmbDealer.EditValue = 0;
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbDealer, dtpToDate,columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "DealerID", cmbDealer.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };
            return GetReportData("USP_R_INVOICELIST_v2", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            try
            {
                if (buttonText == "View Items")
                {
                    DataSet ds = new StockRepository().GetInvoice(drFocusedRow["STOCKENTRYID"]);
                    if (ds != null && ds.Tables.Count > 1)
                    {
                        frmInvoiceItems frm = new frmInvoiceItems(ds.Tables[1]);
                        frm.ShowInTaskbar = false;
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.IconOptions.ShowIcon = false;
                        frm.ShowDialog();
                    }
                    return;
                }

                if ((drFocusedRow["STATUS"].ToString() == "Draft" || drFocusedRow["STATUS"].ToString() == "Submitted via Mobile")
                && buttonText != "Verify and Submit")
                {
                    XtraMessageBox.Show("Draft bills cannot be viewed, printed or reverted. The operation is cancelled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                switch (buttonText)
                {
                    case "Print":
                        DataSet dsprint = new StockRepository().GetInvoice(drFocusedRow["STOCKENTRYID"]);
                        if (dsprint == null || dsprint.Tables.Count < 2 || dsprint.Tables[0].Rows.Count <= 0)
                        {
                            XtraMessageBox.Show("No data returned from database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        if (dsprint != null && dsprint.Tables.Count > 1)
                        {
                            rptInvoice rpt = new rptInvoice(dsprint.Tables[0], dsprint.Tables[1]);
                            rpt.ShowPrintMarginsWarning = false;
                            rpt.ShowRibbonPreview();
                        }
                        break;
                    case "Revert":
                        if (drFocusedRow["STATUS"].ToString() != "Submitted")
                        {
                            XtraMessageBox.Show("Draft or reverted invoice cannot be reverted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            DialogResult result = XtraMessageBox.Show("Are you sure you want to revert this invoice?", "Confirm Revert", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == DialogResult.Yes)
                            {
                                new StockRepository().RevertStockEntry(drFocusedRow["STOCKENTRYID"], Utility.UserID);
                                XtraMessageBox.Show("Invoice successfully reverted", "Information",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                                drFocusedRow["STATUS"] = "Reverted";
                            }
                        }
                        break;
                    case "Clone":
                        if (drFocusedRow["STATUS"].ToString() != "Reverted")
                        {
                            XtraMessageBox.Show("Draft or submitted invoice cannot be cloned", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            int newid = new StockRepository().CloneStockEntry(drFocusedRow["STOCKENTRYID"], Utility.UserID);
                            if (newid > 0)
                                XtraMessageBox.Show("Invoice successfully cloned, please load it from initial user's Invoice draft list",
                                    "Information",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                        }
                        break;
                    case "Edit":
                        if (new frmStockEntryEdit(drFocusedRow["STOCKENTRYID"]).ShowDialog() == DialogResult.OK)
                        {
                            XtraMessageBox.Show("Refresh data to get updated values", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    case "Verify and Submit":

                        if (drFocusedRow["STATUS"].ToString() != "Draft" && drFocusedRow["STATUS"].ToString() != "Submitted via Mobile")
                        {
                            XtraMessageBox.Show("Draft bills cannot be viewed, printed or reverted. The operation is cancelled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        StockEntry stockEntry = new StockEntry()
                        {
                            STOCKENTRYID = drFocusedRow["STOCKENTRYID"]
                        };
                        frmStockEntry frmStockEntry = new frmStockEntry(stockEntry, (XtraForm)this.Parent.Parent.Parent.Parent.Parent.Parent);
                        frmStockEntry.ShowInTaskbar = false;
                        frmStockEntry.IconOptions.ShowIcon = false;
                        frmStockEntry.StartPosition = FormStartPosition.CenterScreen;
                        frmStockEntry.ShowDialog();
                        break;
                    case "Dispatch to branch":
                        XtraMessageBox.Show("Not yet implemented", "Unknown", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case "View Credit Note Mapping":
                        DataTable dtCN = new CreditNoteRepository().GetMappedCreditNotes(drFocusedRow["STOCKENTRYID"], "SE");
                        frmViewCreditNoteMapping frmCNM = new frmViewCreditNoteMapping(dtCN, "SE");
                        frmCNM.ShowInTaskbar = false;
                        frmCNM.StartPosition = FormStartPosition.CenterScreen;
                        frmCNM.IconOptions.ShowIcon = false;
                        frmCNM.ShowDialog();
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }
    }
}
