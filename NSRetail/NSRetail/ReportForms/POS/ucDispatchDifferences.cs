using DataAccess;
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

namespace NSRetail
{
    public partial class ucDispatchDifferences : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;

        public ucDispatchDifferences()
        {
            InitializeComponent();
            columnHeaders = new Dictionary<string, string>
            {
                { "DISPATCHQUANTITY", "Dispatch Quantity" }
                , { "RECEIVEDQUANTITY", "Recieved Quantity" }
                , { "STOCKDIFF", "Stock Difference" }
            };

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;

        }

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
                , { "CategoryID", cmbCategory.EditValue}
            };

            return GetReportData("USP_RPT_DISPATCHDIFF", parameters);
        }

        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;

        public override List<BaseEdit> MandatoryFields => new List<BaseEdit>() { cmbBranch, cmbCategory, dtFromDate, dtToDate };
    }
}
