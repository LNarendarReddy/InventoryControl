﻿using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmOfferBranch : DevExpress.XtraEditors.XtraForm
    {
        object OfferID = null;
        DataTable dtBranch = new DataTable();
        public frmOfferBranch(object OfferName, object _OfferID)
        {
            InitializeComponent();
            this.Text = "Offer Branches - " + OfferName;
            OfferID = _OfferID;
            gcBranch.DataSource = dtBranch = new OfferRepository().GetOfferBranch(OfferID);

            cmBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmBranch.Properties.ValueMember = "BRANCHID";
            cmBranch.Properties.DisplayMember = "BRANCHNAME";
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvBranch.FocusedRowHandle < 0) return;
            new OfferRepository().DeleteOfferBranch(gvBranch.GetFocusedRowCellValue("OFFERBRANCHID"), Utility.UserID);
            gvBranch.DeleteRow(gvBranch.FocusedRowHandle);
        }

        private void gvBranch_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            int OfferBranchID = new OfferRepository().SaveOfferBranch(OfferID,
                cmBranch.EditValue, Utility.UserID);
            gvBranch.SetRowCellValue(e.RowHandle, "OFFERBRANCHID", OfferBranchID);
            int rowhandle = cmbBranchView.LocateByValue("BRANCHID", cmBranch.EditValue);
            gvBranch.SetRowCellValue(e.RowHandle, "BRANCHID", cmBranch.EditValue);
            gvBranch.SetRowCellValue(e.RowHandle, "BRANCHNAME", cmBranch.Text);
            gvBranch.SetRowCellValue(e.RowHandle, "BRANCHCODE", cmbBranchView.GetRowCellValue(rowhandle, "BRANCHCODE"));
            gvBranch.SetRowCellValue(e.RowHandle, "PHONENO", cmbBranchView.GetRowCellValue(rowhandle, "PHONENO"));
        }

        private void frmOfferBranch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmBranch.EditValue == null) return;
            if (gvBranch.LocateByValue("BRANCHID", cmBranch.EditValue) >= 0)
            {
                XtraMessageBox.Show("Branch Already Exists!");
                cmBranch.EditValue = null;
                cmBranch.Focus();
                return;
            }
            else
                gvBranch.AddNewRow();
            gvBranch.GridControl.BindingContext = new BindingContext();
            gvBranch.GridControl.DataSource = dtBranch;
            int rowhandle = gvBranch.LocateByValue("BRANCHID", cmBranch.EditValue);
            cmBranch.EditValue = null;
            cmBranch.Focus();
            gvBranch.FocusedRowHandle = rowhandle;
        }
    }
}