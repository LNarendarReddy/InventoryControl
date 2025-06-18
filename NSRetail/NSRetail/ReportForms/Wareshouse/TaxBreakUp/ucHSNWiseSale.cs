using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.TaxBreakUp
{
    public partial class ucHSNWiseSale : SearchCriteriaBase
    {
        public ucHSNWiseSale()
        {
            InitializeComponent();

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Include B2C bills", "IncludeB2C", new List<string>(), true),
                new IncludeSettings("Include B2B bills", "IncludeB2B", new List<string>(), false)
            };

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "HSNCODE", "HSN Code" },
                { "TOTALQUANTITY", "Total Quantity" },
                { "TOTALVALUE", "Total Value" },
                { "GSTCODE", "GST Code" },
                { "TOTALTAXABLEVALUE", "Total Taxable Value" },
                { "TOTALINTEGRATEDTAX", "Total Integrated Tax" },
                { "TOTALCENTRALTAX", "Total Central Tax" },
                { "TOTALSTATETAX", "Total State Tax" },
                { "TOTALCESS", "Total Cess" },
                { "UQCCODE", "UQC" }
            };

            dtpFromDate.EditValue = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpToDate.EditValue = DateTime.Now;
            SetFocusControls(cmbBranch, dtpToDate, specificColumnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", cmbBranch.EditValue }
                , { "FromDate", dtpFromDate.EditValue }
                , { "ToDate", dtpToDate.EditValue }
            };

            return GetReportData("USP_RPT_HSN_WISE_SALE", parameters);
        }
    }
}
