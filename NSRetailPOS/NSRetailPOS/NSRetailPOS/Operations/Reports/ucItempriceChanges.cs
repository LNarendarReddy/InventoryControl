using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using NSRetailPOS.ReportControls.ReportBase;
using NSRetailPOS.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.Operations.Reports
{
    public partial class ucItempriceChanges : SearchCriteriaBase
    {
        public ucItempriceChanges()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "OLDMRP", "Old MRP" }
                , { "OLDSALEPRICE", "Old Sale Price" }
                , { "MRP", "Latest MRP" }
                , { "SALEPRICE", "Latest Price" }
                , { "BRANCHSTOCK", "Branch Stock" }
            };

            ContextmenuItems = new Dictionary<string, string>
            {
                { "Generate Stickers", "" }
            };

            HiddenColumns = new List<string>() { "SUBCATEGORYNAME", "SUBCLASSIFICATIONNAME" };

            dtpFromDate.EditValue = DateTime.Now;
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbCategory, chkAllItems, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BRANCHID", Utility.branchInfo.BranchID }
                , { "FROMDATE", dtpFromDate.EditValue }
                , { "TODATE", dtpToDate.EditValue }
                , { "AllItems", chkAllItems.EditValue }
                , { "CatIDs", cmbCategory.EditValue }
                , { "ClassIDs", cmbClass.EditValue }
            };

            DataTable dtResult = GetReportData("USP_RPT_ITEMPRICECHANGES", parameters) as DataTable;

            if (dtResult != null && dtResult.Columns.Contains("BRANCHSTOCK"))
                dtResult.Columns.Remove("BRANCHSTOCK");

            return dtResult;
        }

        private void chkAllItems_CheckedChanged(object sender, EventArgs e)
        {
            dtpFromDate.Enabled = !chkAllItems.Checked;
            dtpToDate.Enabled = !chkAllItems.Checked;
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            switch (buttonText)
            {
                case "Generate Stickers":
                    DataTable dt = ((DataTable)ResultGrid.DataSource).Clone();
                    foreach (int i in ResultGridView.GetSelectedRows())
                    {
                        dt.ImportRow(ResultGridView.GetDataRow(i));
                    }
                    rptPriceSticker rpt = new rptPriceSticker();
                    rpt.Parameters["nowDate"].Value = "PRINTED DATE: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                    rpt.DataSource = dt;
                    rpt.CreateDocument();
                    rpt.ShowRibbonPreview();
                    break;
            }
        }

        public override void DataBoundCompleted()
        {
            ResultGridView.OptionsSelection.MultiSelect = true;
        }
    }
}
