using DataAccess;
using DevExpress.XtraEditors;
using ErrorManagement;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Stock
{
    public partial class frmStockSlippage : XtraForm
    {
        object ItemPriceID;

        public frmStockSlippage()
        {
            InitializeComponent();
        }

        private void frmStockSlippage_Load(object sender, EventArgs e)
        {
            sluItemCode.Properties.DataSource = Utility.GetItemCodeListFiltered();
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
            sluItemCode.Properties.DisplayMember = "ITEMCODE";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate() ||
                XtraMessageBox.Show("Are you sure to add processing slippage? This operation cannot be undone!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes)
                return;

            new StockRepository().AddProcessingSlippage(ItemPriceID, txtSlippageWeight.EditValue, txtReason.EditValue, Utility.UserID);
            this.Close();
        }

        private void sluItemCode_Popup(object sender, EventArgs e)
        {
            (sender as SearchLookUpEdit).Properties.PopupView.ActiveFilterString = "[ISOPENITEM] = true";
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if(sluItemCode.EditValue == null) 
            { 
                ItemPriceID = null; 
                return;
            }

            try
            {
                int rowhandle = searchLookUpEdit1View.LocateByValue("ITEMCODEID", sluItemCode.EditValue);
                txtItemName.EditValue = searchLookUpEdit1View.GetRowCellValue(rowhandle, "ITEMNAME");
                DataTable dtMRPList = new ItemCodeRepository().GetMRPList(sluItemCode.EditValue);
                if (dtMRPList.Rows.Count > 1)
                {
                    frmMRPList obj = new frmMRPList(dtMRPList, sluItemCode.EditValue);
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

                txtSlippageWeight.EditValue = 0.00;
                txtSlippageWeight.Focus();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}