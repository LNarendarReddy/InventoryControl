﻿using DataAccess;
using NSRetail.Supplier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Supplier.SupplierReports
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
                , { "ACCEPTEDVALUE", "Accepted Value" }
                , { "REJECTEDVALUE", "Rejected Value" }
                , { "CREATEDBY", "Draft Created By" }
                , { "CREATEDDATE", "Draft Created Date" }
                , { "CNINITIATEDBY", "CN Initiated By" }
                , { "CNINITIATEDDATE", "CN Initiated Date" }
                , { "UPDATEDBY", "CN Generated By" }
                , { "UPDATEDDATE", "CN Generated Date" }
                , { "STATUS", "Status" }
            };

            ContextmenuItems = new Dictionary<string, string>
            { 
                { "View", "9B101F20-1E08-44A0-BE65-A8FA5D197574" },
                { "Generate CN", "00D6561B-C2D3-4D6C-A88A-BC14B519925F" }
            };

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
                ,{ "CategoryID", Utility.CategoryID}
            };
            return GetReportData("USP_R_SUPPLIERRETURNS", parameters);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            switch (buttonText)
            {
                case "Generate CN":
                    DataTable dt = new SupplierRepository().GetSupplierReturnsforCN(drFocusedRow["SUPPLIERRETURNSID"]);
                        
                    frmViewReturnItems obj = 
                        new frmViewReturnItems(
                            dt, 
                            drFocusedRow["DEALERNAME"], 
                            drFocusedRow["SUPPLIERRETURNSID"],
                            drFocusedRow["STATUS"].Equals("CN Initiated") ,
                            drFocusedRow["STATUS"].Equals("CN Generated")
                            );

                    obj.ShowInTaskbar = false;
                    obj.StartPosition = FormStartPosition.CenterScreen;
                    obj.IconOptions.ShowIcon = false;
                    obj.ShowDialog();
                    (ParentForm as frmReportPlaceHolder)?.btnSearch_Click(null, null);
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
