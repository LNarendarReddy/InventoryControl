using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class ucStockRegister : SearchCriteriaBase
    {
        public ucStockRegister()
        {
            InitializeComponent();

            dtpFromDate.DateTime = DateTime.Today.AddDays(-7);
            dtpToDate.DateTime = DateTime.Today;
            MandatoryFields = new List<BaseEdit> { cmbBranch, dtpFromDate, dtpToDate, cmbBranch };
            IncludeSettingsCollection = new List<IncludeSettings>
            {
                new IncludeSettings("Closing Stock As On Date and Reconciliation", "IncludeClosingSnapshot",
                    new List<string> { "CLOSINGQUANTITY", "RECONCILIATIONQUANTITY" }, false)
            };

            SetFocusControls(cmbBranch, dtpToDate, new Dictionary<string, string>
            {
                { "HSNCODE", "HSN Code" },
                { "GSTCODE", "GST Rate" },
                { "STOCKUNIT", "Stock Unit" },
                { "OPENINGQUANTITY", "Opening Quantity" },
                { "OPENINGVALUE", "Opening Value (Excl. GST)" },
                { "PURCHASEQUANTITY", "Purchase Quantity" },
                { "PURCHASEVALUE", "Purchase Value (Excl. GST)" },
                { "DISPATCHINQUANTITY", "Dispatch In Quantity" },
                { "DISPATCHINVALUE", "Dispatch In Value (Excl. GST)" },
                { "DISPATCHOUTQUANTITY", "Dispatch Out Quantity" },
                { "DISPATCHOUTVALUE", "Dispatch Out Value (Excl. GST)" },
                { "SALEQUANTITY", "Sale Quantity" },
                { "SALEVALUE", "Sale Value (Excl. GST)" },
                { "CUSTOMERRETURNQUANTITY", "Customer Return Quantity" },
                { "CUSTOMERRETURNVALUE", "Customer Return Value (Excl. GST)" },
                { "BRANCHRETURNQUANTITY", "Branch Return Quantity" },
                { "BRANCHRETURNVALUE", "Branch Return Value (Excl. GST)" },
                { "SUPPLIERRETURNQUANTITY", "Supplier Return Quantity" },
                { "SUPPLIERRETURNVALUE", "Supplier Return Value (Excl. GST)" },
                { "STOCKADJUSTMENTQUANTITY", "Stock Adjustment Quantity" },
                { "STOCKADJUSTMENTVALUE", "Stock Adjustment Value (Excl. GST)" },
                { "CALCULATEDCLOSINGQUANTITY", "Calculated Closing Quantity" },
                { "CLOSINGQUANTITY", "Closing Quantity (As On Date)" },
                { "RECONCILIATIONQUANTITY", "Reconciliation Quantity (+/-)" },
                { "CLOSINGVALUE", "Closing Value (Excl. GST)" }
            });
        }

        public override void ControlBoundCompleted()
        {
            DataView branches = new DataView(Utility.GetBranchList())
            {
                RowFilter = "ISWAREHOUSE = false AND BRANCHID NOT IN (45, 91, 92, 97, 100, 103)"
            };
            cmbBranch.Properties.DataSource = branches.ToTable();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.CheckAll();
        }

        public override bool ValidateMandatoryFields()
        {
            if (!base.ValidateMandatoryFields()) return false;
            if (dtpFromDate.DateTime.Date <= dtpToDate.DateTime.Date) return true;

            XtraMessageBox.Show("From Date must be on or before To Date.", "Stock Register",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dtpFromDate.Focus();
            return false;
        }

        public override object GetData()
        {
            return GetReportData("USP_RPT_STOCK_REGISTER_BRANCH", new Dictionary<string, object>
            {
                { "FromDate", dtpFromDate.DateTime.Date },
                { "ToDate", dtpToDate.DateTime.Date },
                { "BranchID", cmbBranch.EditValue },
                { "CategoryID", Utility.CategoryID == 13 ? string.Empty : Utility.CategoryID.ToString() }
            });
        }
    }
}
