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
            this.gvDenomination.Appearance.FocusedCell.BackColor = System.Drawing.Color.SaddleBrown;
            this.gvDenomination.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.gvDenomination.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.gvDenomination.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvDenomination.Appearance.FocusedCell.Options.UseFont = true;
            this.gvDenomination.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvDenomination.Appearance.FocusedRow.BackColor = System.Drawing.Color.White;
            this.gvDenomination.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.gvDenomination.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvDenomination.Appearance.FocusedRow.Options.UseFont = true;
            this.gvDenomination.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.gvDenomination.Appearance.FooterPanel.Options.UseFont = true;
            this.gvDenomination.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvDenomination.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvDenomination.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvDenomination.Appearance.Row.Options.UseFont = true;

            this.gvMOP.Appearance.FocusedCell.BackColor = System.Drawing.Color.SaddleBrown;
            this.gvMOP.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.gvMOP.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.gvMOP.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvMOP.Appearance.FocusedCell.Options.UseFont = true;
            this.gvMOP.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvMOP.Appearance.FocusedRow.BackColor = System.Drawing.Color.White;
            this.gvMOP.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.gvMOP.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvMOP.Appearance.FocusedRow.Options.UseFont = true;
            this.gvMOP.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.gvMOP.Appearance.FooterPanel.Options.UseFont = true;
            this.gvMOP.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvMOP.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvMOP.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvMOP.Appearance.Row.Options.UseFont = true;
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
                rpt.Parameters["Address"].Value = Utility.branchinfo.BranchAddress;
                rpt.Parameters["Phone"].Value = Utility.branchinfo.PhoneNumber;
                rpt.Parameters["CounterName"].Value = Utility.branchinfo.BranchCounterName;
                rpt.Parameters["UserName"].Value = Utility.logininfo.UserName;
                rpt.Print();
                rpt.Print();
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