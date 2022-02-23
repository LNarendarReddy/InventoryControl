using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
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
        }

        private void frmDayClosure_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int dayClosureid = new BillingRepository().SaveDayClosure(Utility.branchinfo.BranchCounterID,
                    gcDenomination.DataSource as DataTable, gcMOP.DataSource as DataTable, 
                    Utility.logininfo.UserID,txtRefundAmount.EditValue, daySequenceID);
                DayClosed = true;
                DataSet ds = new BillingRepository().GetDayClosureForReport(dayClosureid);
                rptDayClosure rpt = new rptDayClosure(ds);
                rpt.ShowRibbonPreview();
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