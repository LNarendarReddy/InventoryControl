using DevExpress.XtraReports.UI;
using NSRetailPOS.ReportControls.ReportBase;
using NSRetailPOS.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetailPOS.Operations.Reports
{
    public partial class ucOfferStatus : SearchCriteriaBase
    {
        public ucOfferStatus()
        {
            InitializeComponent();
            Dictionary<string, string> specificColumnHeaders = new Dictionary<string, string>()
            {
                { "OFFERNAME", "Offer Name" },
                { "OFFERCODE", "Offer Code" },
                { "STARTDATE", "Start Date" },
                { "ENDDATE", "End Date" },
                { "ISACTIVE", "Is Active" },
                { "OFFERBASETYPE", "Base Type" },
                { "OFFERTYPENAME", "Offer Type" },
                { "OFFERVALUE", "Offer value" },
                { "FINALPRICE", "Final Price" },
                { "BRANCHSTOCK", "Branch Stock" }
            };

            SetFocusControls(cmbBranch, cmbOfferSearchType, specificColumnHeaders);

            ContextmenuItems = new Dictionary<string, string>
            {
                { "Generate Stickers", "" }
            };

            HiddenColumns = new List<string>()
            {
                {"SUBCATEGORYNAME" },
                {"SUBCLASSIFICATIONNAME" },
                {"OFFERBASETYPE" },
                {"AppliesToName" },
                {"OFFERCODE" }
            };
        }

        private void cmbOfferSearchType_EditValueChanged(object sender, EventArgs e)
        {
            layoutControlItem6.Visibility = cmbOfferSearchType.EditValue.Equals("2") 
                || cmbOfferSearchType.EditValue.Equals("3") || cmbOfferSearchType.EditValue.Equals("4")
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
            dtOfferSeatchType.Rows.Add(1, "Current");
            dtOfferSeatchType.Rows.Add(2, "Expiring soon");
            dtOfferSeatchType.Rows.Add(3, "Upcoming");
            dtOfferSeatchType.Rows.Add(4, "Expired");

            cmbOfferSearchType.Properties.DataSource = dtOfferSeatchType;
            cmbOfferSearchType.Properties.ValueMember = "OFFERSEARCHTYPEID";
            cmbOfferSearchType.Properties.DisplayMember = "OFFERSEARCHTYPE";
            cmbOfferSearchType.EnterMoveNextControl = true;
            cmbOfferSearchType.EditValue = "0";
        }

        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BranchIDs", cmbBranch.EditValue }
                , { "OfferSearchTypeID", cmbOfferSearchType.EditValue }
                , { "NoOfDays", txtNoOfDays.EditValue }
                , { "CatIDs", cmbCategory.EditValue }
                , { "ClassIDs", cmbClass.EditValue }
            };

            DataTable dtResult = GetReportData("USP_RPT_OFFER_STATUS", parameters) as DataTable;

            if (dtResult != null && dtResult.Columns.Contains("BRANCHSTOCK"))
                dtResult.Columns.Remove("BRANCHSTOCK");

            return dtResult;
        }

        public override void ActionExecute(string buttonText, DataRow drFocusedRow)
        {
            switch (buttonText)
            {
                case "Generate Stickers":
                    DataTable dt = ((DataTable)ResultGrid.DataSource).Clone();
                    foreach (int i in ResultGridView.GetSelectedRows())
                    {
                        dt.ImportRow(ResultGridView.GetDataRow(i));
                    }

                    frmPrintSettings frm = new frmPrintSettings();
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.OK)
                    {
                        XtraReport rpt = null;
                        if (frm.printSetting.Equals(0))
                            rpt = new rptOfrPoster();
                        else if (frm.printSetting.Equals(1))
                            rpt = new rptOfrPosterA2();
                        else if (frm.printSetting.Equals(2))
                            rpt = new rptOfrPosterA4();
                        rpt.Parameters["nowDate"].Value = "PRINTED DATE: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                        DataView dv = dt.Copy().DefaultView;
                        dv.RowFilter = "OFFERTYPEID IN (1,2,3,4,5) AND AppliesToName = 'ITEM'";
                        rpt.DataSource = dv.ToTable();
                        rpt.CreateDocument();
                        rpt.ShowRibbonPreview();
                    }
                    break;
            }
        }

        public override void DataBoundCompleted()
        {
            ResultGridView.OptionsSelection.MultiSelect = true;
        }
    }
}
