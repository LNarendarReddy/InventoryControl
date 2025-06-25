using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using NSRetail.Reports;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.POSReports
{
    public partial class ucCRefundSheets : SearchCriteriaBase
    {
        public ucCRefundSheets()
        {
            InitializeComponent();
            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "BRANCHNAME", "Branch" }
                , { "COUNTERNAME", "Counter" }
                , { "REFUNDAMOUNT", "Refund Amount" }
                , { "REFUNDDATE", "Refund Date" }
                , { "REFUNDTIME", "Refund Time" }
                , { "CREATEDBY", "User name" }
                , { "CUSTOMERNAME", "Customer Name" }
                , { "CUSTOMERNUMBER", "Customer Number" }
                , { "BILLOPENDATE", "Bill Open Date" }
                , { "BILLOPENTIME", "Bill Open Time" }
                , { "BILLCLOSEDTIME", "Bill Closed Time" }
                , { "REFUNDQTYORWGHTINKGS", "Refund Qty or Wght (In KGs)" }
            };

            ContextmenuItems = new Dictionary<string, string>
            {
                { "Items", "2EA3591F-63BA-4F33-9070-8B898868EF7F" }, 
                { "Print", "C7FBC73D-CD73-4B64-8ABE-42D970E4D956" }
            };

            dtFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtToDate.EditValue = DateTime.Now;

            SetFocusControls(cmbBranch, dtToDate, columnHeaders);
        }
        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BRANCHID", cmbBranch.EditValue }
                , { "FromDate", dtFromDate.EditValue }
                , { "ToDate", dtToDate.EditValue }
                , { "UnmaskCustomerPhone", AccessUtility.HasAccess("191D866C-E1A0-491D-9671-7A7DA216D72A::Execute")}
            };
            return GetReportData("USP_R_CREFUNDSHEETS", parameters);
        }
        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            DataSet dsItems = new POSRepository().GetRefundItemsByID(drFocusedRow["BRANCHCOUNTERID"], drFocusedRow["BILLID"]);
            switch (buttonText)
            {
                case "Items":
                    frmViewDCItems obj = new frmViewDCItems(dsItems, false, true)
                    {
                        ShowInTaskbar = false,
                        StartPosition = FormStartPosition.CenterScreen
                    };
                    obj.IconOptions.ShowIcon = false;
                    obj.ShowDialog();
                    break;
                case "Print":
                    rptCRefund rpt = new rptCRefund(dsItems.Tables[0]);
                    rpt.Parameters["GSTIN"].Value = "37AAICV7240C1ZC";
                    rpt.Parameters["CIN"].Value = "U51390AP2022PTC121579";
                    rpt.Parameters["FSSAI"].Value = "10114004000548";
                    rpt.Parameters["Address"].Value = "NA";
                    rpt.Parameters["BillDate"].Value = DateTime.Now;
                    rpt.Parameters["BillNumber"].Value = drFocusedRow["BILLNUMBER"];
                    rpt.Parameters["BranchName"].Value = drFocusedRow["BRANCHNAME"];
                    rpt.Parameters["CounterName"].Value = drFocusedRow["COUNTERNAME"];
                    rpt.Parameters["Phone"].Value = "NA";
                    rpt.Parameters["UserName"].Value = drFocusedRow["CREATEDBY"];
                    rpt.Parameters["IsWithBill"].Value = true;
                    rpt.ShowRibbonPreview();
                    break;
            }
        }
    }
}
