using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmOfferBranch : XtraForm
    {
        object OfferID = null;
        OfferRepository offerRepository = new OfferRepository();

        public frmOfferBranch(object OfferName, object _OfferID)
        {
            InitializeComponent();
            this.Text = "Offer Branches - " + OfferName;
            OfferID = _OfferID;
            gcBranch.DataSource = offerRepository.GetOfferBranch(OfferID);

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

        private void frmOfferBranch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmBranch.EditValue?.ToString())) return;

            if (gvBranch.LocateByValue("BRANCHID", cmBranch.EditValue) >= 0)
            {
                XtraMessageBox.Show("Branch Already Exists!");
                cmBranch.EditValue = null;
                cmBranch.Focus();
                return;
            }

            offerRepository.SaveOfferBranch(OfferID,
                cmBranch.EditValue, Utility.UserID);
            gcBranch.DataSource = offerRepository.GetOfferBranch(OfferID);
        }

        private void gcBranch_Click(object sender, EventArgs e)
        {

        }

        private void btnAddAllBranches_Click(object sender, EventArgs e)
        {
            if(XtraMessageBox.Show("Are you sure you want to add all branches?", "Confirm", MessageBoxButtons.YesNoCancel)
                != DialogResult.Yes)
            { 
                return;
            }

            offerRepository.SaveOfferBranch(OfferID, -1, Utility.UserID);
            gcBranch.DataSource = offerRepository.GetOfferBranch(OfferID);
        }
    }
}