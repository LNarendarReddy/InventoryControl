using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse
{
    public partial class ucSupplierReturnsList : SearchCriteriaBase
    {
        List<string> buttonColumns;
        public override IEnumerable<string> ButtonColumns => buttonColumns;
        
        public ucSupplierReturnsList()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
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

            SetFocusControls(cmbSupplier, dtpToDate, columnHeaders);
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
