using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout.Utils;
using Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail.Countning
{
    public partial class frmSliceCounting : XtraForm
    {
        public SliceCounting SliceCountingObj {  get; set; }
        private bool _isSliceCountingSaved = false;

        public frmSliceCounting()
        {
            InitializeComponent();
            SliceCountingObj = new SliceCounting();
        }

        private void frmSliceCounting_Load(object sender, EventArgs e)
        {
            cmbBranch.Properties.DataSource = Utility.GetBranchList();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            sluSKUCode.Properties.DataSource = Utility.GetItemSKUList();
            sluSKUCode.Properties.DisplayMember = "ITEMNAME";
            sluSKUCode.Properties.ValueMember = "ITEMID";
                        
            RefreshItemCountLabel();

            DataTable dtAddedItems = new DataTable();
            dtAddedItems.Columns.Add("ITEMID", typeof(int));
            dtAddedItems.Columns.Add("SKUCODE", typeof(string));
            dtAddedItems.Columns.Add("ITEMNAME", typeof(string));
            dtAddedItems.Columns.Add("CATEGORYNAME", typeof(string));
            gcAddedItemList.DataSource = dtAddedItems;

        }

        private void cmbBranch_EditValueChanged(object sender, EventArgs e)
        {
            int count = !string.IsNullOrEmpty(cmbBranch.EditValue?.ToString()) ? cmbBranch.EditValue.ToString().Split(',').Count() : 0;
            lblSelectedBranchCount.Text = $"# of selected branches : {count}";
            lblSelectedBranchCount.ToolTipTitle = "Selected Branches:";
            lblSelectedBranchCount.ToolTip = string.Join(Environment.NewLine, cmbBranch.Text.Split(','));
        }

        private void RefreshItemCountLabel()
        {
            lblAddedItemsCount.Text = $"Items count : {gvAddedItemList.RowCount}";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {            
            dxValidationProvider1.RemoveControlError(sluSKUCode);
            dxValidationProvider1.SetValidationRule(sluSKUCode
                , new ConditionValidationRule("Item ID", ConditionOperator.IsNotBlank) { ErrorText = "Mandatory", ErrorType = ErrorType.Critical });

            int rowHandle = sluSKUCodeView.LocateByValue("ITEMID", sluSKUCode.EditValue);
            if (!dxValidationProvider1.Validate() || rowHandle <= 0) return;

            CheckAndAddItem(new Item() { 
                ItemID = sluSKUCode.EditValue,
                ItemName = sluSKUCode.Text,
                SKUCode = sluSKUCodeView.GetRowCellValue(rowHandle, "SKUCODE"),
                CategoryName = sluSKUCodeView.GetRowCellValue(rowHandle, "CATEGORYNAME")
            });
            sluSKUCode.EditValue= null;
            sluSKUCode.Focus();
        }

        public void CheckAndAddItem(Item item)
        {
            CheckAndAddItem(new List<Item>() { item });
        }

        public void CheckAndAddItem(List<Item> items)
        {
            if (!dxValidationProvider1.Validate()) return;

            if (!_isSliceCountingSaved) SaveSliceCounting();

            int addedCount = 0;
            DataTable dtAddedItems = (DataTable)gcAddedItemList.DataSource;

            foreach (Item item in items)
            {
                int rowHandle = gvAddedItemList.LocateByValue("ITEMID", item.ItemID);
                if (rowHandle >= 0) continue;

                dtAddedItems.Rows.Add(item.ItemID, item.SKUCode, item.ItemName, item.CategoryName);
                addedCount++;
            }

            XtraMessageBox.Show($"Add items operation completed. \n\n\r - No of items added : {addedCount} \n\r - No of items skipped : {items.Count - addedCount}"
                , "Operation complete", MessageBoxButtons.OK, items.Count - addedCount > 0 ? MessageBoxIcon.Exclamation : MessageBoxIcon.Information);

            lblAddedItemsCount.Text = $"Items count : {gvAddedItemList.RowCount}";
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BuildObject()
        {
            SliceCountingObj.BranchIDs = cmbBranch.EditValue;       
            SliceCountingObj.BranchText = cmbBranch.Text;
            SliceCountingObj.Description = txtDescription.EditValue;
        }

        private void btnOpenSearch_Click(object sender, EventArgs e)
        {
            dxValidationProvider1.RemoveControlError(sluSKUCode);
            if (!dxValidationProvider1.Validate()) return;

            BuildObject();
            new frmSliceCountingAddItems(this).ShowDialog();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void SaveSliceCounting()
        {
            BuildObject();
            _isSliceCountingSaved = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSliceCounting();
        }
    }
}
