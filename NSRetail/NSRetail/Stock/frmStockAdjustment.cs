using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmStockAdjustment : DevExpress.XtraEditors.XtraForm
    {
        StockAdjustment stockAdjustment;
        object ItemPriceID;
        bool IsOpenItem;
        public frmStockAdjustment()
        {
            InitializeComponent();
        }

        private void frmStockAdjustment_Load(object sender, EventArgs e)
        {
            cmbBranch.Properties.DataSource = new MasterRepository().GetBranch();
            cmbBranch.Properties.ValueMember = "BRANCHID";
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";

            cmbItemCode.Properties.DataSource = Utility.GetItemCodeList();
            cmbItemCode.Properties.ValueMember = "ITEMCODEID";
            cmbItemCode.Properties.DisplayMember = "ITEMCODE";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;
            stockAdjustment = new StockAdjustment();
            stockAdjustment.StockAdjustmentID = 0;
            stockAdjustment.ItemPriceID = ItemPriceID;
            stockAdjustment.BranchID = cmbBranch.EditValue;
            stockAdjustment.Quantity = txtQuantity.EditValue;
            stockAdjustment.WeightInKgs = txtWeightInKGs.EditValue;
            stockAdjustment.UserID = Utility.UserID;
            new StockRepository().SaveStockAdjustment(stockAdjustment);
            ClearFields();
        }

        private void ClearFields()
        {
            cmbBranch.EditValue = null;
            cmbItemCode.EditValue = null;
            txtItemName.EditValue = null;
            txtMRP.EditValue = null;
            txtSalePrice.EditValue = null;
            txtQuantity.EditValue = null;
            txtWeightInKGs.EditValue = null;
            cmbItemCode.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbItemCode_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbItemCode.EditValue != null)
                {
                    int rowhandle = cmbLookupView.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
                    txtItemName.EditValue = cmbLookupView.GetRowCellValue(rowhandle, "ITEMNAME");
                    DataTable dtMRPList = new ItemCodeRepository().GetMRPList(cmbItemCode.EditValue);
                    if (dtMRPList.Rows.Count > 1)
                    {
                        frmMRPList obj = new frmMRPList(dtMRPList);
                        obj.ShowDialog();
                        if (obj._IsSave)
                        {
                            txtMRP.EditValue = ((DataRowView)obj.drSelected)["MRP"];
                            txtSalePrice.EditValue = ((DataRowView)obj.drSelected)["SALEPRICE"];
                            ItemPriceID = ((DataRowView)obj.drSelected)["ITEMPRICEID"];
                        }
                    }
                    else
                    {
                        txtMRP.EditValue = dtMRPList.Rows[0]["MRP"];
                        txtSalePrice.EditValue = dtMRPList.Rows[0]["SALEPRICE"];
                        ItemPriceID = dtMRPList.Rows[0]["ITEMPRICEID"];
                    }
                    IsOpenItem = bool.TryParse(Convert.ToString(cmbLookupView.GetRowCellValue(rowhandle, "ISOPENITEM")), out IsOpenItem)
                        && IsOpenItem;

                    txtQuantity.Enabled = !IsOpenItem;
                    txtWeightInKGs.Enabled = IsOpenItem;

                    txtWeightInKGs.EditValue = 0.00;
                    txtQuantity.EditValue = 1;

                    SendKeys.Send("{ENTER}");
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}