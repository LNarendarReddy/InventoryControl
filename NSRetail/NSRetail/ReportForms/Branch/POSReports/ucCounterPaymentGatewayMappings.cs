using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using NSRetail.Master;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.POSReports
{
    public partial class ucCounterPaymentGatewayMappings : SearchCriteriaBase
    {
        internal const string WorkflowIdentifier = "17EB7621-53C3-468A-A380-FF023B09EBB1";

        private const string RemoveGatewayAction = "Remove Gateway";
        private const string EditGatewayAction = "Edit Gateway Configuration";
        private const string EditGatewayAccess = "D7371CA4-24FF-44FB-911B-7801FC5180F3::Update";
        private const string RemoveGatewayAccess = "28E68F66-5851-4C3C-A4C1-59304F5E9BC9::Execute";

        public ucCounterPaymentGatewayMappings()
        {
            InitializeComponent();
            MandatoryFields = new BaseEdit[] { cmbBranch };
            AllowResultGrouping = false;
            HiddenColumns = new[] { "MAPPINGDATE", "ISMOBILECOUNTER" };
            ContextmenuItems = new Dictionary<string, string>
            {
                { EditGatewayAction, EditGatewayAccess },
                { RemoveGatewayAction, RemoveGatewayAccess }
            };

            SetFocusControls(cmbBranch, cmbBranch, new Dictionary<string, string>
            {
                { "BRANCHNAME", "Branch Name" },
                { "COUNTERNAME", "Counter Name" },
                { "MOPNAME", "MOP" },
                { "ISGATEWAYCONFIGURED", "Gateway Configured" },
                { "GATEWAYNAME", "Payment Gateway" },
                { "GATEWAYUSERNAME", "Username" },
                { "GATEWAYPASSWORD", "Password" }
            });
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            if (drFocusedRow == null) return;
            if (buttonText == EditGatewayAction)
            {
                if (!AccessUtility.HasAccess(EditGatewayAccess)) return;
                var counterSettings = new Counter
                {
                    COUNTERID = drFocusedRow["COUNTERID"],
                    COUNTERNAME = drFocusedRow["COUNTERNAME"],
                    BRANCHID = drFocusedRow["BRANCHID"],
                    ISMOBILECOUNTER = drFocusedRow["ISMOBILECOUNTER"]
                };
                using (var form = new frmCounter(counterSettings))
                {
                    form.ShowInTaskbar = false;
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.IconOptions.ShowIcon = false;
                    form.ShowDialog(ParentForm);
                }
                if (counterSettings.IsSave)
                    (ParentForm as frmReportPlaceHolder)?.btnSearch_Click(null, null);
                return;
            }
            if (buttonText != RemoveGatewayAction || drFocusedRow == null || !AccessUtility.HasAccess(RemoveGatewayAccess)) return;
            if (drFocusedRow.IsNull("PAYMENTGATEWAYINFOID") || string.IsNullOrWhiteSpace(Convert.ToString(drFocusedRow["GATEWAYNAME"])))
            {
                XtraMessageBox.Show(this, "No gateway is configured for this payment mode.", RemoveGatewayAction,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string branch = Convert.ToString(drFocusedRow["BRANCHNAME"]);
            string counter = Convert.ToString(drFocusedRow["COUNTERNAME"]);
            string paymentMode = Convert.ToString(drFocusedRow["MOPNAME"]);
            string gateway = Convert.ToString(drFocusedRow["GATEWAYNAME"]);
            if (XtraMessageBox.Show(this, $"Remove {gateway} from {paymentMode} on {counter} ({branch})?",
                RemoveGatewayAction, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                != DialogResult.Yes) return;

            new DataRepository().ExecuteNonQuery("USP_D_COUNTERPAYMENTGATEWAYMAPPING", true, new Dictionary<string, object>
            {
                { "UserID", Utility.UserID },
                { "BranchCounterMopID", drFocusedRow["BRANCHCOUNTERMOPID"] },
                { "ExpectedGatewayID", drFocusedRow["PAYMENTGATEWAYINFOID"] },
                { "ExpectedMappingDate", drFocusedRow["MAPPINGDATE"] }
            });

            (ParentForm as frmReportPlaceHolder)?.btnSearch_Click(null, null);
        }

        public override object GetData()
        {
            return GetReportData("USP_RPT_COUNTERPAYMENTGATEWAYMAPPINGS", new Dictionary<string, object>
            {
                { "UserID", Utility.UserID },
                { "BranchIDs", cmbBranch.EditValue }
            });
        }
    }
}
