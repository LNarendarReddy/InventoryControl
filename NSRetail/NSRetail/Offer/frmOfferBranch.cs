using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using Entity;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmOfferBranch : XtraForm
    {
        object OfferID = null;
        OfferRepository offerRepository = new OfferRepository();
        bool IsbaseOffer = false;

        public frmOfferBranch(object OfferName, object _OfferID, bool _IsbaseOffer = false)
        {
            InitializeComponent();
            this.Text = "Offer Branches - " + OfferName;
            OfferID = _OfferID;
            IsbaseOffer = _IsbaseOffer;
            gcBranch.DataSource = offerRepository.GetOfferBranch(OfferID, _IsbaseOffer);
            gvBranch.Columns["ISACTIVE"].FilterInfo = new ColumnFilterInfo("ISACTIVE = 'YES'");
            cmBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmBranch.Properties.ValueMember = "BRANCHID";
            cmBranch.Properties.DisplayMember = "BRANCHNAME";
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvBranch.FocusedRowHandle < 0 ||
                XtraMessageBox.Show("Are you sure to delete the offer?", "Delete Confirmation",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;
            new OfferRepository().DeleteOfferBranch(gvBranch.GetFocusedRowCellValue("OFFERBRANCHID"), 
                Utility.UserID, IsbaseOffer);
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
                cmBranch.EditValue, Utility.UserID, IsbaseOffer);
            gcBranch.DataSource = offerRepository.GetOfferBranch(OfferID, IsbaseOffer);
            gvBranch.Columns["ISACTIVE"].FilterInfo = new ColumnFilterInfo("ISACTIVE = 'YES'");
        }

        private void btnAddAllBranches_Click(object sender, EventArgs e)
        {
            if(XtraMessageBox.Show("Are you sure you want to add all branches?", "Confirm", MessageBoxButtons.YesNoCancel)
                != DialogResult.Yes)
            { 
                return;
            }

            offerRepository.SaveOfferBranch(OfferID, -1, Utility.UserID, IsbaseOffer);
            gcBranch.DataSource = offerRepository.GetOfferBranch(OfferID, IsbaseOffer);
            gvBranch.Columns["ISACTIVE"].FilterInfo = new ColumnFilterInfo("ISACTIVE = 'YES'");
        }
    }
}