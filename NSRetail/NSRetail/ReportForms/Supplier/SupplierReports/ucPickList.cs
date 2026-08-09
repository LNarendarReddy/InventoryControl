using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Entity;
using NSRetail.Reports;
using NSRetail.Stock;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Supplier.SupplierReports
{
    public partial class ucPicklList : SearchCriteriaBase
    {
        public ucPicklList()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "DEALERNAME", "Supplier" }
                , { "LOCATIONDIVISIONNAME", "Loc. division" }
                , { "PICKLISTNUMBER", "Pick list #" }
                , { "PICKLISTSTATUSTEXT", "Pick list status" }
                , { "PICKLISTBRANCHSTATUS", "Pick list Branch status" }
                , { "CREATEDBY", "Created by" }
            };

            ContextmenuItems = new Dictionary<string, string>
            {
                { "Mark seggregation complete", "F9D592FA-8EC4-47C7-B44B-5A5BDE62B5CA" },
                { "Print && Export", "" }
            };

            IncludeSettingsCollection = new List<IncludeSettings>
            {
                new IncludeSettings("Invoice # & date", "IncludeInvoiceDetails", new List<string> { "SUPPLIERINVOICENO", "INVOICEDATE" }),
                new IncludeSettings("Branch", "IncludeBranch", new List<string>{ "PICKLISTBRANCHID", "BRANCHNAME", "PICKLISTBRANCHSTATUS" })
            };

            HiddenColumns = new List<string> { "PICKLISTSTATUS" };

            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            AccessUtility.SetStatusByAccess(btnPickList);
            SetFocusControls(cmbCategory, dtpToDate, columnHeaders);
        }

        public override object GetData()
        {
            if (IncludeSettingsCollection.First(x => x.ParameterName == "IncludeInvoiceDetails").Included && 
                IncludeSettingsCollection.First(x => x.ParameterName == "IncludeBranch").Included)
            {
                XtraMessageBox.Show("Invoice and Branch cannot be selected together.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return null;
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "CATEGORYID",  cmbCategory.EditValue }
                ,{ "FROMDATE",  dtpFromDate.EditValue }
                ,{ "TODATE", dtpToDate.EditValue }
            };
            return GetReportData("USP_RPT_PICKLIST", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            switch (buttonText)
            {
                case "Mark seggregation complete":
                    MarkSeggregationComplete(drFocusedRow); 
                    break;
                case "Print && Export":
                    DataSet dsPickList = new DataRepository().GetDataset("USP_PRINT_PICKLIST", true, new Dictionary<string, object> { { "PickListID", drFocusedRow["PICKLISTID"] } });
                    rptPickList pickList = new rptPickList(dsPickList.Tables[0].Rows[0]["SUPPLIERNAME"].ToString()
                        , dsPickList.Tables[0].Rows[0]["PICKLISTNUMBER"].ToString()
                        , dsPickList.Tables[1]);
                    pickList.ShowRibbonPreview(); 
                    break;
            }
        }

        private void MarkSeggregationComplete(DataRow drFocusedRow)
        {
            object pickListBranchID = drFocusedRow.Table.Columns.Contains("PICKLISTBRANCHID") ? drFocusedRow["PICKLISTBRANCHID"] : null;
            string message = pickListBranchID != null
                ? $"Are you sure you want to mark picklist {drFocusedRow["PICKLISTNUMBER"]} for branch {drFocusedRow["BRANCHNAME"]} as seggregated?"
                : $"Are you sure you want to mark picklist {drFocusedRow["PICKLISTNUMBER"]} as seggregated?";

            if (XtraMessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                object returnVal = new DataRepository().ExecuteScalarWithTransaction("USP_U_MARK_PICKLIST", true, new Dictionary<string, object>
                {
                    { "PickListID", drFocusedRow["PICKLISTID"] },
                    { "PickListBranchID", pickListBranchID },
                    { "UserID", Utility.UserID }
                });

                if (!int.TryParse(returnVal?.ToString(), out int _))
                {
                    XtraMessageBox.Show(returnVal.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                message = pickListBranchID != null
                ? $"{drFocusedRow["PICKLISTNUMBER"]} for branch {drFocusedRow["BRANCHNAME"]} marked as seggregated successfully"
                : $"{drFocusedRow["PICKLISTNUMBER"]} marked as seggregated successfully";

                XtraMessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string targetColumn = pickListBranchID != null ? "PICKLISTBRANCHSTATUS" : "PICKLISTSTATUSTEXT";
                drFocusedRow[targetColumn] = "Seggregated";
            }
            catch (Exception ex) { ErrorManagement.ErrorMgmt.ShowError(ex); }
        }

        private void btnPickList_Click(object sender, EventArgs e)
        {
            new frmGeneratePickList().ShowDialog();
        }
    }
}
