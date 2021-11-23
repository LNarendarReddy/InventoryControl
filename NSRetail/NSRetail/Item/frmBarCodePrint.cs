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
using NSRetail.Reports;
using DevExpress.XtraReports.UI;
using ErrorManagement;
using DataAccess;

namespace NSRetail
{
    public partial class frmBarCodePrint : DevExpress.XtraEditors.XtraForm
    {
        //Branch ObjBranch = null;
        ItemCodeRepository objItemRep = new ItemCodeRepository();
        public frmBarCodePrint()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.PrintBarCode(cmbItemCode.Text,txtItemName.Text,
                    txtSalePrice.Text, txtQuantity.EditValue);
                cmbItemCode.EditValue = null;
                txtItemName.EditValue = null;
                txtSalePrice.EditValue = null;
                txtMRP.EditValue = null;
                txtQuantity.EditValue = null;
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
                cmbItemCode.Properties.DataSource = objItemRep.GetItemList();
                cmbItemCode.Properties.DisplayMember = "ITEMCODE";
                cmbItemCode.Properties.ValueMember = "ITEMCODEID";
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
                    DataTable dtMRPList = objItemRep.GetMRPList(cmbItemCode.EditValue);
                    if (dtMRPList.Rows.Count > 1)
                    {
                        frmMRPList obj = new frmMRPList(dtMRPList);
                        obj.ShowDialog();
                        if (obj._IsSave)
                        {
                            txtCostPrice.EditValue = ((DataRow)obj.drSelected)["COSTPRICE"];
                            txtMRP.EditValue = ((DataRow)obj.drSelected)["MRP"];
                            txtSalePrice.EditValue = ((DataRow)obj.drSelected)["SALEPRICE"];
                        }
                    }
                    else
                    {
                        txtCostPrice.EditValue = dtMRPList.Rows[0]["COSTPRICE"];
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
    }
}