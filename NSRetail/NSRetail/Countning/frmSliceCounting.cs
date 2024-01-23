using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraLayout.Utils;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace NSRetail.Countning
{
    public partial class frmSliceCounting : XtraForm
    {
        private const string Criteria_TopSellingItems = "Top selling items";
        private const string Criteria_Category = "Category & sub-category";
        public frmSliceCounting()
        {
            InitializeComponent();
        }

        private void frmSliceCounting_Load(object sender, EventArgs e)
        {
            cmbBranch.Properties.DataSource = Utility.GetBranchList();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            DataTable dtCriteria = new DataTable();
            dtCriteria.Columns.Add("Criteria", typeof(string));
            dtCriteria.Rows.Add(Criteria_TopSellingItems);
            dtCriteria.Rows.Add("High quantity");
            dtCriteria.Rows.Add("High value items");
            dtCriteria.Rows.Add(Criteria_Category);
            dtCriteria.Rows.Add("Negatives");

            cmbCriteria.Properties.DataSource = dtCriteria;
            cmbCriteria.Properties.ValueMember = "Criteria";
            cmbCriteria.Properties.DisplayMember = "Criteria";

            cmbCategory.Properties.DataSource = Utility.GetCategoryList();
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";

            //cmbSubCategory.CascadingOwner = cmbCategory;
            //cmbSubCategory.Properties.DataSource = new MasterRepository().GetSubCategory();
            //cmbSubCategory.Properties.DisplayMember = "SUBCATEGORYNAME";
            //cmbSubCategory.Properties.ValueMember = "SUBCATEGORYID";

            sluSKUCode.Properties.DataSource = Utility.GetItemSKUList();
            sluSKUCode.Properties.DisplayMember = "ITEMNAME";
            sluSKUCode.Properties.ValueMember = "ITEMID";

            cmbCriteria_EditValueChanged(null, null);
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

        private void cmbCriteria_EditValueChanged(object sender, EventArgs e)
        {
            lcgTopSelling.Visibility = cmbCriteria.EditValue != null && cmbCriteria.EditValue.Equals("Top selling items") 
                ? LayoutVisibility.Always : LayoutVisibility.Never;
            lcgCategory.Visibility = cmbCriteria.EditValue != null && cmbCriteria.EditValue.Equals("Category & sub-category") 
                ? LayoutVisibility.Always : LayoutVisibility.Never;
            lciOpenItems.Visibility = !string.IsNullOrEmpty(cmbCriteria.EditValue?.ToString()) && !lcgTopSelling.Visible && !lcgCategory.Visible
                ? LayoutVisibility.Always : LayoutVisibility.Never;
        }

        private void RefreshItemCountLabel()
        {
            lblAddedItemsCount.Text = $"Items count : {gvAddedItemList.RowCount}";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            dxValidationProvider1.RemoveControlError(txtTopSellingSaleDays);
            dxValidationProvider1.RemoveControlError(cmbCategory);
            dxValidationProvider1.RemoveControlError(sluSKUCode);

            dxValidationProvider1 = new DXValidationProvider();

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

        private void btnOpenList_Click(object sender, EventArgs e)
        {
            if (cmbCriteria.EditValue == null) return;

            dxValidationProvider1.RemoveControlError(txtTopSellingSaleDays);
            dxValidationProvider1.RemoveControlError(cmbCategory);
            dxValidationProvider1.RemoveControlError(sluSKUCode);

            dxValidationProvider1 = new DXValidationProvider();
            if (cmbCriteria.EditValue.Equals(Criteria_TopSellingItems))
            {
                dxValidationProvider1.SetValidationRule(txtTopSellingSaleDays
                    , new ConditionValidationRule(Criteria_TopSellingItems, ConditionOperator.IsNotBlank) { ErrorText = "Mandatory", ErrorType = ErrorType.Critical });
            }
            else if (cmbCriteria.EditValue.Equals(Criteria_Category))
            {
                dxValidationProvider1.SetValidationRule(cmbCategory
                    , new ConditionValidationRule(Criteria_Category, ConditionOperator.IsNotBlank) { ErrorText = "Mandatory", ErrorType = ErrorType.Critical });
            }

            if (!dxValidationProvider1.Validate()) return;


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
