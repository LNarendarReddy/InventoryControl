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

namespace NSRetail.ReportForms.Wareshouse
{
    public partial class ucSupplierReturnsList : SearchCriteriaBase
    {
        Dictionary<string, string> columnHeaders;
        List<string> buttonColumns;
        public override Dictionary<string, string> SpecificColumnHeaders => columnHeaders;
        public override IEnumerable<string> ButtonColumns => buttonColumns;
        public override Control FirstControl => cmbSupplier;
        public override Control LastControl => dtpToDate;
        public ucSupplierReturnsList()
        {
            InitializeComponent();
            columnHeaders = new Dictionary<string, string>
            {
                { "SUPPLIERRETURNSID", "Supplier ReturnsID" }
                , { "SUPPLIERID", "Supplier ID" }
                , { "DEALERNAME", "Supplier" }
                , { "RETURNVALUE", "Return Value" }
                , { "UPDATEDBY", "Updated User" }
                , { "UPDATEDDATE", "Update Date" }
                , { "STATUS", "Status" }
            };

            buttonColumns = new List<string>() { "View" };

            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer(true);
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";
            cmbSupplier.EditValue = 0;
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;
        }
        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "SupplierID",  cmbSupplier.EditValue }
                ,{ "FromDate",  dtpFromDate.EditValue }
                ,{ "ToDate", dtpToDate.EditValue }
            };
            return GetReportData("USP_R_SUPPLIERRETURNS", parameters);
        }
        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            switch (buttonText)
            {
                case "View":
                    DataTable dt = new SupplierRepository().GetSupllierReturnsDetail(drFocusedRow["SUPPLIERRETURNSID"]);
                    frmViewReturnItems obj = new frmViewReturnItems(dt, drFocusedRow["DEALERNAME"], drFocusedRow["SUPPLIERRETURNSID"]);
                    obj.ShowInTaskbar = false;
                    obj.StartPosition = FormStartPosition.CenterScreen;
                    obj.IconOptions.ShowIcon = false;
                    obj.ShowDialog();
                    break;
            }
        }
    }
}
