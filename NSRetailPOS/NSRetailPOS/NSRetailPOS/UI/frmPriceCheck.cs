using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmPriceCheck : XtraForm
    {
        public frmPriceCheck(object itemCodes)
        {
            InitializeComponent();
            sluItemData.Properties.DataSource = itemCodes;
            sluItemData.Properties.DisplayMember = "ITEMNAME";
            sluItemData.Properties.ValueMember = "ITEMCODEID";
        }

        private void txtItemCode_Enter(object sender, EventArgs e)
        {
            txtItemCode.SelectAll();
        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter || string.IsNullOrEmpty(Convert.ToString(txtItemCode.EditValue).Trim()))
                return;
            if (Convert.ToString(txtItemCode.EditValue).Contains("?"))
            {
                string[] stItem = Convert.ToString(txtItemCode.EditValue).Split('?');
                txtItemCode.EditValue = stItem[0];               
            }

            int rowHandle = sluItemDataView.LocateByValue("ITEMCODE", txtItemCode.EditValue);

            if (rowHandle < 0)
            {
                List<int> rowHandles = sluItemDataView.LocateAllRowsByValue("SKUCODE", txtItemCode.EditValue);
                if (rowHandles.Count == 1)
                {
                    rowHandle = rowHandles.First();
                }
                else if (rowHandles.Count > 1)
                {
                    sluItemData.ShowPopup();
                    sluItemDataView.FindFilterText = txtItemCode.EditValue.ToString();
                }
            }

            if (rowHandle >= 0)
            {
                sluItemData.EditValue = null;
                sluItemData.EditValue = sluItemDataView.GetRowCellValue(rowHandle, "ITEMCODEID");
            }
            else if (sluItemData.EditValue == null)
            {
                XtraMessageBox.Show("Item Does Not Exists!");
                ClearItems();
            }
        }

        private void frmPriceCheck_Load(object sender, EventArgs e)
        {
            sluItemDataView.GridControl.BindingContext = new BindingContext();
            sluItemDataView.GridControl.DataSource = sluItemData.Properties.DataSource;
        }

        private void sluItemData_EditValueChanged(object sender, EventArgs e)
        {
            if (sluItemData.EditValue == null) return;

            DataSet ds = new ReportRepository().GetReportDataset("USP_R_ITEMDATAFORITEMDETAILS", new Dictionary<string, object>() { { "ITEMCODEID", sluItemData.EditValue } });
            gcItemPriceList.DataSource = ds.Tables[0];
            gcItemOfferList.DataSource = ds.Tables[1];

            txtItemCode.EditValue = sluItemDataView.GetRowCellValue(sluItemDataView.FocusedRowHandle, "ITEMCODE");
            txtItemCode.Focus();            
        }

        private void ClearItems()
        {
            sluItemData.EditValue = null;
            txtItemCode.EditValue = null;
            gcItemPriceList.DataSource = null;
            gcItemOfferList.DataSource = null;
        }
    }
}
