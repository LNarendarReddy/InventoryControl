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

namespace NSRetailPOS.UI
{
    public partial class frmMRPSelection : DevExpress.XtraEditors.XtraForm
    {
        private readonly DataTable dtMRPList;
        public object drSelected = null;
        public bool _IsSave = false;

        public frmMRPSelection(DataTable dtMRPList,object ItemCode, Object ItemName)
        {
            InitializeComponent();
            gcMRPList.DataSource = dtMRPList;
            txtItemCode.EditValue = ItemCode;
            txtItemName.EditValue = ItemName;

            Utility.SetGridFormatting(gvMRPList);
            this.dtMRPList = dtMRPList;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectPriceAndClose(gvMRPList.GetFocusedRow());
        }

        private void frmMRPSelection_Load(object sender, EventArgs e)
        {
            bool filterByStock = Convert.ToBoolean(Utility.branchInfo.FilterMRPByStock);
            
            gvMRPList.ActiveFilterString = filterByStock ? "[Stock Available?] = 'Yes'" : string.Empty;
            gvMRPList.ActiveFilterEnabled = filterByStock;
            gcStockAvailable.Visible = filterByStock;
        }

        private void btnSearchMRP_Click(object sender, EventArgs e)
        {
            if(txtMRP.EditValue == null) return;

            DataView dvPrices = dtMRPList.Copy().DefaultView;
            
            dvPrices.RowFilter = $"MRP = {txtMRP.EditValue} AND ISSTOCKAVAILABLE = 'Yes'";
            if (dvPrices.Count > 0)
            {
                SelectPriceAndClose(dvPrices[0]);
                return;
            }

            dvPrices.RowFilter = $"MRP = {txtMRP.EditValue} AND ISSTOCKAVAILABLE = 'No'";
            if (dvPrices.Count > 0)
            {
                SelectPriceAndClose(dvPrices[0]);
                return;
            }

            XtraMessageBox.Show("MRP not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SelectPriceAndClose(object drItemPrice)
        {
            if (drItemPrice == null) return;

            _IsSave = true;
            drSelected = drItemPrice;
            this.Close();
        }

        private void frmMRPSelection_KeyDown(object sender, KeyEventArgs e)
        {
            EnterSearch(e);
        }

        private void gcMRPList_KeyDown(object sender, KeyEventArgs e)
        {
            EnterSearch(e);
        }

        private void EnterSearch(KeyEventArgs e)
        {
            if (e.KeyCode != Keys.F3) return;

            txtMRP.Focus();
            e.Handled = true;
        }
    }
}