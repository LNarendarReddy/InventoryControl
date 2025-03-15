using System;
using System.Data;
using ErrorManagement;
using DataAccess;

namespace NSRetail
{
    public partial class frmBarCodePrint : DevExpress.XtraEditors.XtraForm
    {
        ItemCodeRepository objItemRep = new ItemCodeRepository();
        
        public frmBarCodePrint()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCategory.EditValue.Equals(3) || cmbCategory.EditValue.Equals(4))
                {
                    if (!dxValidationProvider1.Validate())
                        return;
                }
                else
                {
                    if (!dxValidationProvider11.Validate())
                        return;
                }
                Utility.PrintBarCode(cmbItemCode.Text,txtItemName.Text,
                    txtSalePrice.Text, txtQuantity.EditValue,txtMRP.EditValue,
                    txtBatchNumber.EditValue,dtpPackedDate.EditValue,cmbCategory.EditValue);
                cmbItemCode.EditValue = null;
                txtItemName.EditValue = null;
                txtCostPriceWOT.EditValue = null;   
                txtCostPriceWT.EditValue = null;
                txtSalePrice.EditValue = null;
                txtMRP.EditValue = null;
                txtQuantity.EditValue = null;
                cmbCategory.EditValue = null;   
                cmbItemCode.Focus();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void frmBarCodePrint_Load(object sender, EventArgs e)
        {
            try
            {
                cmbItemCode.Properties.DataSource = Utility.GetNonEAN();
                cmbItemCode.Properties.DisplayMember = "ITEMCODE";
                cmbItemCode.Properties.ValueMember = "ITEMCODEID";

                cmbCategory.Properties.DataSource = Utility.GetCategoryListExceptAll();
                cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
                cmbCategory.Properties.ValueMember = "CATEGORYID";

                dtpPackedDate.EditValue = DateTime.Now;
            }
            catch (Exception ex){}
        }

        private void cmbItemCode_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if(cmbItemCode.EditValue != null)
                {
                    txtItemName.EditValue = cmbLookupView.GetFocusedRowCellValue("ITEMNAME");
                    cmbCategory.EditValue = cmbLookupView.GetFocusedRowCellValue("CATEGORYID");
                    if (cmbCategory.EditValue.Equals(3) || cmbCategory.EditValue.Equals(4))
                    {
                        dtpPackedDate.Enabled = true;
                        txtBatchNumber.Enabled = true;
                    }
                    DataTable dtMRPList = objItemRep.GetMRPList(cmbItemCode.EditValue);
                    if (dtMRPList.Rows.Count > 1)
                    {
                        frmMRPList obj = new frmMRPList(dtMRPList, cmbItemCode.EditValue);
                        obj.ShowDialog();
                        if (obj._IsSave)
                        {
                            txtMRP.EditValue = ((DataRowView)obj.drSelected)["MRP"];
                            txtSalePrice.EditValue = ((DataRowView)obj.drSelected)["SALEPRICE"];
                        }
                    }
                    else
                    {
                        txtMRP.EditValue = dtMRPList.Rows[0]["MRP"];
                        txtSalePrice.EditValue = dtMRPList.Rows[0]["SALEPRICE"];
                    }
                    txtQuantity.EditValue = 1;
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void brnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}