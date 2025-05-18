using DataAccess;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Wareshouse.Audit
{
    public partial class ucDeletedItemPrices : SearchCriteriaBase
    {
        public ucDeletedItemPrices()
        {
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "CREATEDBY", "Created By" }
                , { "DELETEDBY", "Deleted By" }
                , { "UPDATEDBY", "Updated By" }
                , { "DELETEDDATE", "Deleted Date" }
                , { "CREATEDTIME", "Created Time" }
                , { "UPDATEDATE", "Updated Time" }
                , { "BRANDNAME", "Brand" }
                , { "MANUFACTURERNAME", "Manufacturer" }                
            };

            SetFocusControls(null, null, columnHeaders);

            ContextmenuItems = new Dictionary<string, string> { { "Un-delete", "035DB43B-11EE-4DDB-B510-682CCD1FEC55" } };
        }

        public override object GetData()
        {
            return GetReportData("USP_RPT_DELETEDITEMPRICES", null);
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            switch (buttonText)
            {
                case "Un-delete":
                    if(XtraMessageBox.Show($"Do you want to un-delete Item price : {drFocusedRow["ITEMCODE"]} with MRP {drFocusedRow["MRP"]}"
                        , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        new ItemCodeRepository().UnDeleteItemPrice(drFocusedRow["ITEMPRICEID"], Utility.UserID);
                        (ParentForm as frmReportPlaceHolder)?.btnSearch_Click(null, null);
                        XtraMessageBox.Show("Delete completed succefully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ((frmMain)this.ParentForm.MdiParent).bbiRefreshData_ItemClick(null, null);
                    }
                    break;
            }
        }
    }
}
