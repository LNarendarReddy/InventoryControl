using DataAccess;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms.Supplier.SupplierReports
{
    public partial class ucSupplierItems : SearchCriteriaBase
    {
        public ucSupplierItems()
        {
            InitializeComponent();
            
            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer();
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";

            SetFocusControls(cmbSupplier, cmbSupplier, null);

            MandatoryFields = new List<BaseEdit>() { cmbSupplier };
        }
        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@SUPPLIERID",  cmbSupplier.EditValue }
            };
            return GetReportData("USP_R_SUPPLIERITEMS", parameters);
        }
    }
}
