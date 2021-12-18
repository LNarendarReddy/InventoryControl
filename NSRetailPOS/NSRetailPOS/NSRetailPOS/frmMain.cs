using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using NSRetailPOS.UI;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
//using System.Data.SQLite;

namespace NSRetailPOS
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private int userID = 6;

        private int branchCounterID = 3;

        private int daySequenceID;

        private int SNo = 1;

        BillingRepository billingRepository = new BillingRepository();
        ItemRepository itemRepository = new ItemRepository();

        Bill billObj;

        DataRow drSelectedPrice;

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataSet dsInitialData = billingRepository.GetInitialLoad(userID, branchCounterID);

            if (!int.TryParse(dsInitialData.Tables["DAYSEQUENCE"].Rows[0][0].ToString(), out daySequenceID))
            {
                XtraMessageBox.Show(dsInitialData.Tables["DAYSEQUENCE"].Rows[0][0].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            billObj = Utility.GetBill(dsInitialData);

            this.Text = $"NS Retail POS - Bill Number : {billObj.BillNumber}";

            lblLastBilledAmount.Text = billObj.LastBilledAmount.ToString();
            lblLastBilledQunatity.Text = billObj.LastBilledQuantity.ToString();

            sluItemCode.Properties.DataSource = itemRepository.GetItemCodes();
            sluItemCode.Properties.DisplayMember = "ITEMCODE";
            sluItemCode.Properties.ValueMember = "ITEMCODEID";
        }

        private void txtQuantity_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter || sluItemCode.EditValue == null || drSelectedPrice == null || txtQuantity.EditValue == null || txtQuantity.EditValue.Equals(0))
            {
                return;
            }

            gvBilling.AddNewRow();
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            DataTable dtPrices = itemRepository.GetMRPList(sluItemCode.EditValue);
            if(dtPrices.Rows.Count > 1)
            {
                frmMRPSelection mRPSelection = new frmMRPSelection(dtPrices);
                mRPSelection.ShowDialog();
                if(!mRPSelection._IsSave)
                {
                    return;
                }

                drSelectedPrice = (mRPSelection.drSelected as DataRowView)?.Row;
            }
            else if(dtPrices.Rows.Count == 1)
            {
                drSelectedPrice = dtPrices.Rows[0];
            }

            if(drSelectedPrice == null)
            {
                return;
            }

            txtItemName.EditValue = (sluItemCode.GetSelectedDataRow() as DataRowView)?.Row["ITEMNAME"];
            txtMRP.EditValue = drSelectedPrice["MRP"];
            txtSalePrice.EditValue = drSelectedPrice["SALEPRICE"];

            txtQuantity.EditValue = 1;
            txtQuantity.Focus();
            txtQuantity.SelectAll();
        }

        private void gvBilling_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //gvBilling.SetRowCellValue(e.RowHandle, "STOCKDISPATCHDETAILID", ObjStockDispatchDetail.STOCKDISPATCHDETAILID);
        }
    }
}
