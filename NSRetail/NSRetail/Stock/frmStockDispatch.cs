using DataAccess;
using DevExpress.XtraEditors;
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
    public partial class frmStockDispatch : DevExpress.XtraEditors.XtraForm
    {
        MasterRepository ObjMasterRep = new MasterRepository();
        ItemCodeRepository ObjItemRep = new ItemCodeRepository();
        public frmStockDispatch()
        {
            InitializeComponent();
        }

        private void frmStockDispatch_Load(object sender, EventArgs e)
        {
            try
            {
                ((frmMain)MdiParent).RefreshBaseLineData += FrmStockDispatch_RefreshBaseLineData;
                DataTable dtBranch = ObjMasterRep.GetBranch();
                DataView dvBranch = dtBranch.Copy().DefaultView;
                dvBranch.RowFilter = "ISWAREHOUSE = 0";
                cmbToBranch.Properties.DataSource = dvBranch;
                cmbToBranch.Properties.ValueMember = "BRANCHID";
                cmbToBranch.Properties.DisplayMember = "BRANCHNAME";

                DataView gvWarehouse = dtBranch.Copy().DefaultView;
                gvWarehouse.RowFilter = "ISWAREHOUSE = 1";
                cmbFromBranch.Properties.DataSource = gvWarehouse;
                cmbFromBranch.Properties.ValueMember = "BRANCHID";
                cmbFromBranch.Properties.DisplayMember = "BRANCHNAME";

                cmbItemCode.Properties.DataSource = Utility.GetItemCodeList();
                cmbItemCode.Properties.ValueMember = "ITEMCODEID";
                cmbItemCode.Properties.DisplayMember = "ITEMCODE";

            }
            catch (Exception){}
        }

        private void FrmStockDispatch_RefreshBaseLineData(object sender, EventArgs e)
        {
            object selectedValue = cmbItemCode.EditValue;
            cmbItemCode.Properties.DataSource = Utility.GetItemCodeList();
            cmbItemCode.Properties.ValueMember = "ITEMCODEID";
            cmbItemCode.Properties.DisplayMember = "ITEMCODE";
            cmbItemCode.EditValue = selectedValue;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

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
                    txtItemName.EditValue = cmbLookupView.GetFocusedRowCellValue("ITEMNAME");
                    DataTable dtMRPList = ObjItemRep.GetMRPList(cmbItemCode.EditValue);
                    if (dtMRPList.Rows.Count > 1)
                    {
                        frmMRPList obj = new frmMRPList(dtMRPList);
                        obj.ShowDialog();
                        if (obj._IsSave)
                        {
                            txtMRP.EditValue = ((DataRow)obj.drSelected)["MRP"];
                            txtSalePrice.EditValue = ((DataRow)obj.drSelected)["SALEPRICE"];
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

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                cmbItemCode.Focus();
            }
        }

        private void txtQuantity_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}