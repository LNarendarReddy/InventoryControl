using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NSRetailPOS.Reports;
using DevExpress.XtraReports.UI;
using NSRetailPOS.Data;

namespace NSRetailPOS.UI
{
    public partial class frmBarCodePrint : DevExpress.XtraEditors.XtraForm
    {
        public frmBarCodePrint()
        {
            InitializeComponent();
        }
        private void frmBarCodePrint_Load(object sender, EventArgs e)
        {
            try
            {
                cmbCategory.Properties.DataSource = new ItemRepository().GetCategory();
                cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
                cmbCategory.Properties.ValueMember = "CATEGORYID";

                cmbItemCode.Properties.DataSource = new ItemRepository().GetNonEAN();
                cmbItemCode.Properties.DisplayMember = "ITEMCODE";
                cmbItemCode.Properties.ValueMember = "ITEMCODEID";

                dtpPackedDate.EditValue = DateTime.Now;
            }
            catch (Exception ex) { }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                bool allowOpenItems = Convert.ToBoolean((cmbCategory.GetSelectedDataRow() as DataRowView)["ALLOWOPENITEMS"]);
                if (allowOpenItems && !dxValidationProvider11.Validate())
                    return;
                if (!dxValidationProvider1.Validate())
                    return;
                Utility.PrintBarCode(cmbItemCode.Text, txtItemName.Text,
                    txtSalePrice.Text, txtQuantity.EditValue, txtMRP.EditValue,
                    txtBatchNumber.EditValue, dtpPackedDate.EditValue, cmbCategory.EditValue, allowOpenItems);
                cmbItemCode.EditValue = null;
                txtItemName.EditValue = null;
                txtSalePrice.EditValue = null;
                txtMRP.EditValue = null;
                txtQuantity.EditValue = null;
                txtBatchNumber.EditValue = null;
                cmbCategory.EditValue = null;
                cmbItemCode.Focus();
            }
            catch (Exception ex)
            {

            }
        }
        private void cmbItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbItemCode.EditValue == null)
                return;
            try
            {
                int rowhandle = cmbLookupView.LocateByValue("ITEMCODEID", cmbItemCode.EditValue);
                txtItemName.EditValue = cmbLookupView.GetRowCellValue(rowhandle ,"ITEMNAME");
                cmbCategory.EditValue = cmbLookupView.GetRowCellValue(rowhandle,"CATEGORYID");

                DataTable dtMRPList = new ItemRepository().GetMRPList(cmbItemCode.EditValue);
                if (dtMRPList.Rows.Count > 1)
                {
                    frmMRPSelection obj = new frmMRPSelection(dtMRPList,cmbItemCode.Text,txtItemName.EditValue);
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
                object allowOpenItems = (cmbCategory.GetSelectedDataRow() as DataRowView)["ALLOWOPENITEMS"];
                if (allowOpenItems.Equals(true))
                {
                    dtpPackedDate.Enabled = true;
                    txtBatchNumber.Enabled = true;
                }
                txtQuantity.EditValue = 1;
            }
            catch (Exception ex){}
        }
        private void brnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}