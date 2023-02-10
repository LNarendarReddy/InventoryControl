using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Stock.TransactionReports
{
    public partial class ucStockSlippage : SearchCriteriaBase
    {
        public ucStockSlippage()
        {
            InitializeComponent();

            sluItem.Properties.DataSource = Utility.GetItemCodeList();
            sluItem.Properties.ValueMember = "ITEMID";
            sluItem.Properties.DisplayMember = "ITEMNAME";

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "SLIPPAGEWEIGHT", "Slippage Weight" }
                , { "CREATEDTIME", "Created Time" }
                , { "DESCRIPTION", "Reason" }
            };

            SetFocusControls(sluItem, dtpToDate, columnHeaders);
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            MandatoryFields = new List<BaseEdit> { dtpFromDate, dtpToDate };
            AllowedRoles = new List<string> { "IT User" };
        }

        private void sluItem_Popup(object sender, EventArgs e)
        {
            (sender as SearchLookUpEdit).Properties.PopupView.ActiveFilterString = "[ISOPENITEM] = true";
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
                , { "ItemID", sluItem.EditValue }
            };

            return GetReportData("USP_RPT_STOCKSLIPPAGE", parameters);
        }
    }
}
