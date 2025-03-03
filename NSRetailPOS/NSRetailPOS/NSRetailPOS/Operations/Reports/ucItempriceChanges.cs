using DevExpress.XtraEditors;
using NSRetailPOS.ReportControls.ReportBase;
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
                , { "NEWMRP", "New MRP" }
                , { "NEWSALEPRICE", "New Sale Price" }
            };

            dtpFromDate.EditValue = DateTime.Now;
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(dtpFromDate, dtpToDate, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BRANCHID", Utility.branchInfo.BranchID }
                , { "FROMDATE", dtpFromDate.EditValue }
                , { "TODATE", dtpToDate.EditValue }
            };
            return GetReportData("USP_RPT_ITEMPRICECHANGES", parameters);
        }
    }
}
