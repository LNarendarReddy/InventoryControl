using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.Wizards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DevExpress.Utils.Design.DXCollectionEditorBase;

namespace NSRetail.ReportForms.Supplier.SupplierWiseReports
{
    public partial class ucSupplierStockSummary : SearchCriteriaBase
    {
        public ucSupplierStockSummary()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "WEIGHTINKGS", "Weight In Kgs" }
            };

            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer(true);
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";
            SetFocusControls(cmbSupplier, cmbBranch, columnHeaders);
        }
        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "SupplierID", cmbSupplier.EditValue }
                , { "BranchID", cmbBranch.EditValue }
            };

            DataTable dt = (DataTable)GetReportData("USP_R_STOCKSUMMARY_SUPPLIER", parameters);
            return dt;
        }
    }
}
