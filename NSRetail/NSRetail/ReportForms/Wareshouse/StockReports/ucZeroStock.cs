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

namespace NSRetail.ReportForms.Wareshouse.StockReports
{
    public partial class ucZeroStock : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        public ucZeroStock()
        {
            InitializeComponent();

            columnHeaders = new Dictionary<string, string>
            {
                { "BRANCHNAME", "Branch Name" }
                , { "SKUCODE", "SKU Code" }
                , { "ITEMCODE", "Item Code" }
                , { "ITEMNAME", "Item Name" }
                , { "WAREHOUSESTOCK", "Warehouse Stock" }
                , { "BRANCHSTOCK", "Branch Stock" }
                , { "CATEGORYNAME", "Category" }
                , { "SUBCATEGORYNAME", "Sub Category" }
            };

            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch(true);
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = 0;
        }
        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;

        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchID", cmbBranch .EditValue }
            };
            return GetReportData("USP_RPT_ZEROSTOCK", parameters);
        }
    }
}
