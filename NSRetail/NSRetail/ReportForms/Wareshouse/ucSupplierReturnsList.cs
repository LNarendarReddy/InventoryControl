using DataAccess;
using NSRetail.Supplier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse
{
    public partial class ucSupplierReturnsList : SearchCriteriaBase
    {   
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

            ButtonColumns = new List<string>() { "View","Generate CN" };

            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer(true);
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";
            cmbSupplier.EditValue = 0;
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbSupplier, dtpToDate, columnHeaders);
        }
        public override object GetData()
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
                case "Generate CN":
                    DataTable dt = new SupplierRepository().GetSupplierReturnsforCN(drFocusedRow["SUPPLIERRETURNSID"]);
                        
                    frmViewReturnItems obj = new frmViewReturnItems(dt, 
                            drFocusedRow["DEALERNAME"], drFocusedRow["SUPPLIERRETURNSID"],
                            Convert.ToString(drFocusedRow["STATUS"]) != "Draft");

                    obj.ShowInTaskbar = false;
                    obj.StartPosition = FormStartPosition.CenterScreen;
                    obj.IconOptions.ShowIcon = false;
                    obj.ShowDialog();
                    if (obj.cNGenerated)
                        drFocusedRow["STATUS"] = "CN Generated";
                    break;
                case "View":
                    DataTable dti = new SupplierRepository().ViewSupplierReturnItems(drFocusedRow["SUPPLIERRETURNSID"]);
                    
                    frmViewReturnDetail obji = new frmViewReturnDetail(dti,
                        drFocusedRow["DEALERNAME"], drFocusedRow["SUPPLIERRETURNSID"]);

                    obji.ShowInTaskbar = false;
                    obji.StartPosition = FormStartPosition.CenterScreen;
                    obji.IconOptions.ShowIcon = false;
                    obji.ShowDialog();
                    break;
            }
        }
    }
}
