﻿using DevExpress.XtraEditors;
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

            Utility.SetGridFormatting(gvDraftBills);
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
