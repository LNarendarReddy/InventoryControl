using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using NSRetailPOS.Data;
using NSRetailPOS.Reports;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmDayClosure : XtraForm
    {
        int daySequenceID;
        public bool DayClosed { get; private set; }

        public frmDayClosure(int daySeqID)
        {
            InitializeComponent();
            DataSet dsDayClosure = new BillingRepository().GetDayClosure();
            gcDenomination.DataSource = dsDayClosure.Tables[0];
            gcMOP.DataSource = dsDayClosure.Tables[1];
            gvDenomination.FocusedRowHandle = 0;
            gvDenomination.FocusedColumn = gcQuantity;
            txtRefundAmount.EditValue = dsDayClosure.Tables[2].Rows[0]["REFUNDAMOUNT"];
            updateSummary();
            daySequenceID = daySeqID;
            
            Utility.SetGridFormatting(gvDenomination);
            Utility.SetGridFormatting(gvMOP);
        }

        private void frmDayClosure_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("This operation cannot be reversed. Please confirm that all the enetered values are correct?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                int dayClosureid = new BillingRepository().SaveDayClosure(Utility.branchInfo.BranchCounterID,
                    gcDenomination.DataSource as DataTable, gcMOP.DataSource as DataTable, 
                    Utility.loginInfo.UserID,txtRefundAmount.EditValue, daySequenceID);
                DayClosed = true;
                DataSet ds = new BillingRepository().GetDayClosureForReport(dayClosureid);
                rptDayClosure rpt = new rptDayClosure(ds);
                rpt.Parameters["Address"].Value = Utility.branchInfo.BranchAddress;
                rpt.Parameters["Phone"].Value = Utility.branchInfo.PhoneNumber;
                rpt.Parameters["BranchName"].Value = Utility.branchInfo.BranchName;
                rpt.Parameters["CounterName"].Value = Utility.branchInfo.BranchCounterName;
                rpt.Parameters["UserName"].Value = Utility.loginInfo.UserName;
                rpt.Print();
                rpt.Print();
                SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
                if(!Utility.StartSync(null))Application.Exit();
                SplashScreenManager.CloseForm();
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDenomination_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                gvDenomination.MoveNext();
            }
        }

        private void gvDenomination_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "QUANTITY") return;
            int Quantity = Convert.ToInt32(e.Value);
            decimal Multiplier = Convert.ToDecimal(gvDenomination.GetRowCellValue(e.RowHandle,"MULTIPLIER"));
            gvDenomination.SetRowCellValue(e.RowHandle, "CLOSUREVALUE", Math.Round(Quantity * Multiplier, 2));
            gvDenomination.UpdateTotalSummary();
            updateSummary();
        }

        private void gcDenomination_Click(object sender, EventArgs e)
        {

        }

        private void updateSummary()
        {
            txtTotalAmount.EditValue = 
            Convert.ToDecimal(gvDenomination.Columns["CLOSUREVALUE"].SummaryItem.SummaryValue) + 
            Convert.ToDecimal(gvMOP.Columns["MOPVALUE"].SummaryItem.SummaryValue) - Convert.ToDecimal(txtRefundAmount.EditValue);
        }
    }
} 