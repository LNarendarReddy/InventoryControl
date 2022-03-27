using DataAccess;
using DevExpress.XtraGrid.Columns;
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

            gcItemPriceList.DataSource = dsItemVisualizer.Tables["ITEMPRICES"];
            gcStockSummary.DataSource = dsItemVisualizer.Tables["ITEMSTOCKSUMMARY"];
            
            gvItemPrice_FocusedRowChanged(null, null);
        }

        private void frmItemVisualize_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void gvItemPrice_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gvItemPrice.FocusedRowHandle < 0)
                    return;
                gvStockSummary.Columns["ITEMCODE"].FilterInfo = 
                    new ColumnFilterInfo($"[ITEMCODE] = '{gvItemPrice.GetFocusedRowCellValue("ITEMCODE")}'");
                gcOffer.DataSource = 
                    new ItemCodeRepository().GetOffers(gvItemPrice.GetFocusedRowCellValue("ITEMPRICEID"));
            }
            catch (Exception ex){}
        }
    }
}
