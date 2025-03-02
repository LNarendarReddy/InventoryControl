using DevExpress.XtraEditors;
using NSRetail.ReportForms;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using NSRetailPOS.ReportControls.ReportBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.Operations.Reports
{
    public partial class ucOfferStatus : SearchCriteriaBase
    {
        public ucOfferStatus()
        {
            InitializeComponent();
        }

        private void cmbOfferSearchType_EditValueChanged(object sender, EventArgs e)
        {
            layoutControlItem6.Visibility = cmbOfferSearchType.EditValue.Equals("2") || cmbOfferSearchType.EditValue.Equals("3")
                ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            txtNoOfDays.EditValue = 0;
        }

        private void ucOfferStatus_Load(object sender, EventArgs e)
        {
            DataTable dtBranch = new DataTable();
            dtBranch.Columns.Add("BRANCHID");
            dtBranch.Columns.Add("BRANCHNAME");
            dtBranch.Rows.Add(Utility.branchInfo.BranchID, Utility.branchInfo.BranchName);

            cmbBranch.Properties.DataSource = dtBranch;
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.EditValue = Utility.branchInfo.BranchID;

            DataTable dtOfferSeatchType = new DataTable();
            dtOfferSeatchType.Columns.Add("OFFERSEARCHTYPEID");
            dtOfferSeatchType.Columns.Add("OFFERSEARCHTYPE");
            dtOfferSeatchType.Rows.Add(0, "All");
            dtOfferSeatchType.Rows.Add(1, "Current Offers");
            dtOfferSeatchType.Rows.Add(2, "Expiring soon");
            dtOfferSeatchType.Rows.Add(3, "Commencing soon");
            
            cmbOfferSearchType.Properties.DataSource = dtOfferSeatchType;
            cmbOfferSearchType.Properties.ValueMember = "OFFERSEARCHTYPEID";
            cmbOfferSearchType.Properties.DisplayMember = "OFFERSEARCHTYPE";
            cmbOfferSearchType.EnterMoveNextControl = true;
            cmbOfferSearchType.EditValue = "0";

            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "OFFERNAME", "Offer Name" },
                { "STARTDATE", "Start Date" },
                { "ENDDATE", "End Date" },
                { "ISACTIVE", "Is Active" },
                { "OFFERBASETYPE", "Offer Base Type" },
                { "OFFERTYPENAME", "Offer Type Name" },
                { "OFFERVALUE", "Offer value" },
                { "FINALPRICE", "Final Price" }
            };

            SetFocusControls(cmbBranch, txtNoOfDays, specificColumnHeaders);
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", cmbBranch.EditValue }
                , { "OfferSearchTypeID", cmbOfferSearchType.EditValue }
                , { "NoOfDays", txtNoOfDays.EditValue }
            };

            return GetReportData("USP_RPT_OFFER_STATUS", parameters);
        }
    }
}
