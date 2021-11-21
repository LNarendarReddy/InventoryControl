using DataAccess;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmItemVisualize : DevExpress.XtraEditors.XtraForm
    {
        int itemID;

        public frmItemVisualize(object itemID)
        {
            this.itemID = Convert.ToInt32(itemID);
            InitializeComponent();
        }

        private void frmItemDrillDown_Load(object sender, EventArgs e)
        {
            DataSet dsItemVisualizer = new ItemCodeRepository().GetItemVisualizer(itemID);

            txtItemName.EditValue = dsItemVisualizer.Tables["ITEM"].Rows[0]["ITEMNAME"];
            txtSKUCode.EditValue = dsItemVisualizer.Tables["ITEM"].Rows[0]["SKUCODE"];
            txtDescription.EditValue = dsItemVisualizer.Tables["ITEM"].Rows[0]["DESCRIPTION"];

            gcItemCodes.DataSource = dsItemVisualizer.Tables["ITEMCODES"];
            gcItemPriceList.DataSource = dsItemVisualizer.Tables["ITEMPRICES"];
            gcStockSummary.DataSource = dsItemVisualizer.Tables["ITEMSTOCKSUMMARY"];
        }

        private void frmItemVisualize_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
