﻿using DataAccess;
using DevExpress.Utils.Text.Internal;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using NSRetail.Reports;
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
                , { "CATEGORYNAME", "Category" }
                , { "INVOICEDATE", "Invoice Date" }
                , { "CREATEDBY", "User Name" }
                , { "CREATEDDATE", "Created Date" }
                , { "FINALPRICE", "Net Amount" }
                , { "STATUS", "Status" }
            };

            ContextmenuItems = new Dictionary<string, string>
            {
                { "View", "3A365A22-5334-4667-8782-A83825298BF6" },
                { "Revert", "7CE473E7-514B-4BC9-B07E-2B26B5AA44F2" },
                { "Clone", "50C463EA-A4BE-49A8-8484-B2C73186A373" }
            };
                

            HiddenColumns = new List<string>() { "TAXINCLUSIVE", "TCS", "DISCOUNTPER", "DISCOUNT", "EXPENSES", "TRANSPORT" };

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
            return GetReportData("USP_R_INVOICELIST", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            try
            {
                if (drFocusedRow["STATUS"].ToString() == "Draft")
                {
                    XtraMessageBox.Show("Draft bills cannot be viewed, printed or reverted. The operation is cancelled", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                switch (buttonText)
                {
                    case "View":
                        DataSet ds = new StockRepository().GetInvoice(drFocusedRow["STOCKENTRYID"]);
                        if (ds == null || ds.Tables.Count < 2 || ds.Tables[0].Rows.Count <= 0)
                        {
                            XtraMessageBox.Show("No data returned from database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }
                        if (ds != null && ds.Tables.Count > 1)
                        {
                            rptInvoice rpt = new rptInvoice(ds.Tables[0], ds.Tables[1]);
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
                            new StockRepository().RevertStockEntry(drFocusedRow["STOCKENTRYID"], Utility.UserID);
                            XtraMessageBox.Show("Invoice successfully reverted", "Information",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                            drFocusedRow["STATUS"] = "Reverted";
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
                }
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }
    }
}
