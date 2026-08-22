using DataAccess;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using Entity;
using NSRetail.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmOfferBranch : XtraForm
    {
        object OfferID = null;
        OfferRepository offerRepository = new OfferRepository();
        bool IsbaseOffer = false;
        private OfferType offerType;
        private SimpleButton btnDeleteSelected;
        private bool canDeleteBranches;

        public frmOfferBranch(object OfferName, object _OfferID
            , OfferType offerType, bool _IsbaseOffer = false)
        {
            InitializeComponent();
            this.Text = "Offer Branches - " + OfferName;
            this.offerType = offerType;
            OfferID = _OfferID;
            IsbaseOffer = _IsbaseOffer;
            AddSerialNumberColumn();
            AddDeleteSelectedButton();
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
            UpdateDeleteSelectedButtonStatus();
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            List<object> offerBranchIDs = new List<object>();

            foreach (int rowHandle in gvBranch.GetSelectedRows())
            {
                if (rowHandle < 0) continue;

                object offerBranchID = gvBranch.GetRowCellValue(rowHandle, "OFFERBRANCHID");
                if (offerBranchID != null && !string.IsNullOrWhiteSpace(Convert.ToString(offerBranchID)))
                    offerBranchIDs.Add(offerBranchID);
            }

            if (offerBranchIDs.Count == 0)
            {
                XtraMessageBox.Show("Please select at least one branch to delete.", "Delete Branches",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (XtraMessageBox.Show($"Are you sure to delete {offerBranchIDs.Count} selected branch(es)?",
                "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;

            foreach (object offerBranchID in offerBranchIDs)
            {
                offerRepository.DeleteOfferBranch(offerBranchID, Utility.UserID, IsbaseOffer);
            }

            RefreshBranchList();
            gvBranch.ClearSelection();
            UpdateDeleteSelectedButtonStatus();
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
            RefreshBranchList();
        }

        private void btnAddAllBranches_Click(object sender, EventArgs e)
        {
            if(XtraMessageBox.Show("Are you sure you want to add all branches?", "Confirm", MessageBoxButtons.YesNoCancel)
                != DialogResult.Yes)
            { 
                return;
            }

            offerRepository.SaveOfferBranch(OfferID, -1, Utility.UserID, IsbaseOffer);
            RefreshBranchList();
        }

        private void frmOfferBranch_Load(object sender, EventArgs e)
        {
            gvBranch.OptionsSelection.MultiSelect = true;
            gvBranch.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            gvBranch.OptionsSelection.CheckBoxSelectorColumnWidth = 35;

            RefreshBranchList();
            cmBranch.Properties.DataSource = Utility.GetBranchList();
            cmBranch.Properties.ValueMember = "BRANCHID";
            cmBranch.Properties.DisplayMember = "BRANCHNAME";

            string accessIdentifier = string.Empty;
            switch (offerType)
            {
                case OfferType.Base:
                    accessIdentifier = "18EB46B9-DB69-481F-AA11-CE1FE8CD8709";
                    break;
                case OfferType.Category:
                    accessIdentifier = "B076FE8D-4982-4A73-ADC6-58E2EDE1280D";
                    break;
                case OfferType.Deal:
                    accessIdentifier = "978FF449-7E8D-41F6-AFE7-C136154B8227";
                    break;
            }

            btnAdd.Tag = $"{accessIdentifier}::Create";
            btnAddAllBranches.Tag = $"{accessIdentifier}::Create";
            btnDeleteSelected.Tag = $"{accessIdentifier}::Delete";
            gcDelete.Tag = $"{accessIdentifier}::Delete";

            canDeleteBranches = AccessUtility.HasAccess(btnDeleteSelected.Tag?.ToString());
            AccessUtility.SetStatusByAccess(btnAdd, btnAddAllBranches);
            AccessUtility.SetStatusByAccess(gcDelete);
            UpdateDeleteSelectedButtonStatus();
        }

        private void RefreshBranchList()
        {
            gcBranch.DataSource = offerRepository.GetOfferBranch(OfferID, IsbaseOffer);
            gvBranch.Columns["ISACTIVE"].FilterInfo = new ColumnFilterInfo("ISACTIVE = 'YES'");
            gvBranch.ClearSelection();
            UpdateDeleteSelectedButtonStatus();
        }

        private void AddSerialNumberColumn()
        {
            if (gvBranch.Columns["SNO"] != null) return;

            GridColumn serialNumberColumn = gvBranch.Columns.AddField("SNO");
            serialNumberColumn.Caption = "S No";
            serialNumberColumn.UnboundType = UnboundColumnType.Integer;
            serialNumberColumn.OptionsColumn.AllowEdit = false;
            serialNumberColumn.Visible = true;
            serialNumberColumn.VisibleIndex = 0;
            serialNumberColumn.Width = 50;
            serialNumberColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            serialNumberColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gvBranch.CustomUnboundColumnData += gvBranch_CustomUnboundColumnData;
        }

        private void AddDeleteSelectedButton()
        {
            btnDeleteSelected = new SimpleButton
            {
                Name = "btnDeleteSelected",
                Text = "Delete Selected",
                Enabled = false,
                StyleController = layoutControl1
            };
            btnDeleteSelected.Click += btnDeleteSelected_Click;
            gvBranch.SelectionChanged += gvBranch_SelectionChanged;

            layoutControl1.Controls.Add(btnDeleteSelected);

            DevExpress.XtraLayout.LayoutControlItem deleteSelectedItem =
                Root.AddItem(string.Empty, btnDeleteSelected);
            deleteSelectedItem.TextVisible = false;
            deleteSelectedItem.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 6, 6, 6);
            deleteSelectedItem.Move(layoutControlItem4, DevExpress.XtraLayout.Utils.InsertType.Right);
        }

        private void gvBranch_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            UpdateDeleteSelectedButtonStatus();
        }

        private void gvBranch_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "SNO" && e.IsGetData)
                e.Value = gvBranch.GetRowHandle(e.ListSourceRowIndex) + 1;
        }

        private void UpdateDeleteSelectedButtonStatus()
        {
            if (btnDeleteSelected == null) return;

            btnDeleteSelected.Enabled = canDeleteBranches && gvBranch.GetSelectedRows().Length > 0;
        }
    }

    public enum OfferType
    {
        Base,
        Category,
        Deal
    }
}
