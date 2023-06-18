using System;
using System.Data;
using DevExpress.XtraEditors;

namespace NSRetailPOS.UI
{
    public partial class frmMRPSelection : XtraForm
    {
        public object drSelected = null;
        public bool _IsSave = false;

        public frmMRPSelection(DataTable dtMRPList,object ItemCode, Object ItemName)
        {
            InitializeComponent();
            gcMRPList.DataSource = dtMRPList;
            txtItemCode.EditValue = ItemCode;
            txtItemName.EditValue = ItemName;

            Utility.SetGridFormatting(gvMRPList);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                int rowHandle = gvMRPList.LocateByValue("MRP", txtMRP.EditValue);
                if (rowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
                {
                    _IsSave = true;
                    gvMRPList.FocusedRowHandle = rowHandle;
                    drSelected = gvMRPList.GetFocusedRow();
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("MRP not found, please contact warehouse admin", "Error");
                }
            }
            catch (Exception) { }
        }

        private void txtMRP_Enter(object sender, EventArgs e)
        {
            txtMRP.SelectAll();
        }
    }
}