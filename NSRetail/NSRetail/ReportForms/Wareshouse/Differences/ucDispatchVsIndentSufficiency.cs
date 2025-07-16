using DevExpress.XtraEditors;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.Differences
{
    public partial class ucDispatchVsIndentSufficiency : SearchCriteriaBase
    {
        public ucDispatchVsIndentSufficiency()
        {
            InitializeComponent();
            IsDataSet = true;

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "CURRENTWHSTOCK", "Cur. WH Stock" }
                , { "BRANCHSTOCK", "Branch Stock" }
                , { "INDENTQUANTITY", "Indent Qty" }
                , { "DISPATCHEDSTOCK", "Dispatched Stock" }
                , { "SUFFICIENCY", "Sufficiency" }
                , { "LASTDISPATCHEDDATE", "Last Dispatched date" }
            };

            SetFocusControls(null, null, columnHeaders);
        }

        public override object GetData()
        {
            DataSet dsData = (DataSet)GetReportData("USP_RPT_DISPATCH_SUFFICIENCY", null);
            dsData.Relations.Add("Branch wise", dsData.Tables[0].Columns["ITEMID"], dsData.Tables[1].Columns["ITEMID"]);
            return dsData;
        }
    }
}
