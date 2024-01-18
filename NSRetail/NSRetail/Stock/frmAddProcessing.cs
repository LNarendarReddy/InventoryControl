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
    public partial class frmAddProcessing : XtraForm
    {
        object itemPriceID;
        double? multiplier;

        public frmAddProcessing()
        {
            InitializeComponent();
        }

        private void frmAddProcessing_Load(object sender, EventArgs e)
        {
            sluItemCode.Properties.DataSource = Utility.GetItemCodeListFiltered();
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
            sluItemCode.Properties.DisplayMember = "ITEMCODE";

            txtQuantity.ConfirmBarCodeScan();

            RefreshProcessings();
        }

        private void sluItemCode_Popup(object sender, EventArgs e)
        {
            (sender as SearchLookUpEdit).Properties.PopupView.ActiveFilterString = "[PARENTITEMID] <> 0";
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if (sluItemCode.EditValue == null)
            {
                ClearObjects();
                return;
            }

            try
            {
                int rowhandle = sluItemCodeView.LocateByValue("ITEMCODEID", sluItemCode.EditValue);
                txtItemName.EditValue = sluItemCodeView.GetRowCellValue(rowhandle, "ITEMNAME");
                DataTable dtMRPList = new ItemCodeRepository().GetMRPList(sluItemCode.EditValue);
                if(dtMRPList.Rows.Count == 0)
                {
                    XtraMessageBox.Show("MRP not found!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearObjects();
                    return;
                }
                else if (dtMRPList.Rows.Count > 1)
                {
                    frmMRPList obj = new frmMRPList(dtMRPList, sluItemCode.EditValue);
                    obj.ShowDialog();
                    if (obj._IsSave)
                    {
                        txtMRP.EditValue = ((DataRowView)obj.drSelected)["MRP"];
                        txtSalePrice.EditValue = ((DataRowView)obj.drSelected)["SALEPRICE"];
                        itemPriceID = ((DataRowView)obj.drSelected)["ITEMPRICEID"];
                    }
                }
                else
                {
                    txtMRP.EditValue = dtMRPList.Rows[0]["MRP"];
                    txtSalePrice.EditValue = dtMRPList.Rows[0]["SALEPRICE"];
                    itemPriceID = dtMRPList.Rows[0]["ITEMPRICEID"];
                }

                multiplier = double.Parse(sluItemCodeView.GetRowCellValue(rowhandle, "MULTIPLIER").ToString());
                object parentItemID = sluItemCodeView.GetRowCellValue(rowhandle, "PARENTITEMID");
                int parentRowHandle = sluItemCodeView.LocateByValue("ITEMID", parentItemID);
                if (parentRowHandle == -1)
                {
                    XtraMessageBox.Show("Parent item not found!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearObjects();
                    return;
                }

                txtParentSKU.EditValue = $"{sluItemCodeView.GetRowCellValue(parentRowHandle, "ITEMNAME")} ( {sluItemCodeView.GetRowCellValue(parentRowHandle, "SKUCODE")} )";
                txtQuantity.EditValue = 0;
                txtQuantity.Focus();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!dxValidationProvider11.Validate())
            {
                return; 
            }

            if(txtQuantity.EditValue.Equals(0))
            {
                XtraMessageBox.Show("Quantity cannot be zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop); 
                txtQuantity.Focus();
                return;
            }

            try
            {
                new StockRepository().AddBulkProcessing(itemPriceID, txtQuantity.EditValue, Utility.UserID);
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }

            ClearObjects();
            RefreshProcessings();
        }

        private void txtQuantity_EditValueChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtQuantity.EditValue?.ToString(), out int quantity) && multiplier.HasValue)
                txtCalculatedWeight.EditValue = quantity * multiplier.Value;
        }

        private void ClearObjects()
        {
            sluItemCode.EditValue =
            txtItemName.EditValue =
            txtParentSKU.EditValue =
            txtMRP.EditValue =
            txtSalePrice.EditValue =
            txtQuantity.EditValue =
            txtCalculatedWeight.EditValue = null;

            itemPriceID = multiplier = null;
        }

        private void RefreshProcessings()
        {
            DataTable dtProcessing = new ReportRepository().GetReportData("USP_RPT_PROCESSING", new Dictionary<string, object>()
            {
                { "FromDate", DateTime.Now },
                { "ToDate", DateTime.Now }
            });

            gcBulkProcessing.DataSource = dtProcessing;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshProcessings();
        }
    }
}
