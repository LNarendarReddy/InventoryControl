using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using NSRetail.ReportForms.Branch.POSReports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.BranchReports
{
    public partial class ucLiquidationList : SearchCriteriaBase
    {
        public ucLiquidationList()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "STATUSTEXT", "Status" }
                , { "DESCRIPTION", "Description" }
                , { "STOCK", "Branch Stock" }
                , { "APPROVEDBY", "Approved by" }
                , { "APPROVEDDATE", "Approved Date" }
                , { "RUNNINGSALE", "Running sale" }
                , { "LIQUIDATIONSALEPRICE", "Liq. sale price" }
                , { "STOCKPROCESSED", "Processed" }
                , { "STATUSCHANGEDBY", "Status changed by" }
                , { "STATUSCHANGEDDATE", "Status changed Date" }
                , { "MARGIN", "Margin WOT" }
                , { "MARGINPER", "Margin WOT %" }
                , { "REFUNDPATHTEXT", "Refund Path"}
                , { "LIQUIDATIONREASONTEXT", "Liq. Reason"}
                , { "MANUFACTUREDATE", "Mfg. Date"}
                , { "EXPIRYDATE", "Exp. Date"}
                , { "EXPIRINGINDAYS", "Exp. in days"}
            };

            MandatoryFields = new List<BaseEdit>() { cmbBranch, dtFromDate, dtToDate };
            HiddenColumns = new List<string>() { "ISOPENITEM", "STATUS" };

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch, dtToDate, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
                , { "ShowStock", true }
                , { "ShowCostPrice", true }
            };

            return GetReportData("USP_R_LIQUIDATION", parameters);
        }
    }
}
