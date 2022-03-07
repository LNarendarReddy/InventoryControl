using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmDraftList : XtraForm
    {
        public int SelectedDraftBillID = -1;

        public frmDraftList(int daySequenceID)
        {
            InitializeComponent();
            DataTable dtDraftBills = new BillingRepository().GetDraftBills(daySequenceID);
            gcDraftBills.DataSource = dtDraftBills;
            this.gvDraftBills.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.gvDraftBills.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.gvDraftBills.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvDraftBills.Appearance.FocusedCell.Options.UseFont = true;
            this.gvDraftBills.Appearance.FocusedRow.BackColor = System.Drawing.Color.White;
            this.gvDraftBills.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.gvDraftBills.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvDraftBills.Appearance.FocusedRow.Options.UseFont = true;
            this.gvDraftBills.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.gvDraftBills.Appearance.FooterPanel.Options.UseFont = true;
            this.gvDraftBills.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvDraftBills.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvDraftBills.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvDraftBills.Appearance.Row.Options.UseFont = true;
        }

        private void frmDraftList_Load(object sender, System.EventArgs e)
        {
            if((gcDraftBills.DataSource as DataTable).Rows.Count == 0)
            {
                XtraMessageBox.Show("No pending draft bills found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            SelectedDraftBillID = gvDraftBills.GetFocusedDataRow() != null ? Convert.ToInt32(gvDraftBills.GetFocusedDataRow()["BILLID"]) : -1;
            Close();
        }
    }
}
