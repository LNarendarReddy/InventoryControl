using DataAccess;
using System;
using System.Collections.Generic;

namespace NSRetail.ReportForms.Supplier.SupplierReports
{
    public partial class ucPickListItems : SearchCriteriaBase
    {
        public ucPickListItems()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "DEALERNAME", "Supplier" }
                , { "LOCATIONDIVISIONNAME", "Loc. division" }
                , { "PICKLISTNUMBER", "Pick list #" }
                , { "PICKLISTSTATUSTEXT", "Pick list status" }
                , { "PICKLISTBRANCHSTATUS", "Pick list Branch status" }
                , { "CREATEDBY", "Created by" }
            };

            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer();
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";

            SetFocusControls(cmbSupplier, cmbItemCode, columnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "SupplierID",  cmbSupplier.EditValue }
                ,{ "BranchIDs",  cmbBranch.EditValue }
                ,{ "ItemCodeID", cmbItemCode.EditValue }
            };
            return GetReportData("USP_RPT_PICKLIST_ITEMS", parameters);
        }
    }
}
