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

            sluItemCode.Properties.DataSource = itemRepository.GetItemCodes();
            sluItemCode.Properties.DisplayMember = "ITEMNAME";
            sluItemCode.Properties.ValueMember = "ITEMCODEID";

            sluItemCodeView.GridControl.BindingContext = new BindingContext();
            sluItemCodeView.GridControl.DataSource = sluItemCode.Properties.DataSource;

            LoadBillData(dsInitialData);
        }

        private void txtQuantity_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter || sluItemCode.EditValue == null || drSelectedPrice == null || txtQuantity.EditValue == null || txtQuantity.EditValue.Equals(0))
            {
                return;
            }

            gvBilling.GridControl.BindingContext = new BindingContext();
            gvBilling.GridControl.DataSource = billObj.dtBillDetails;

            int rowHandle = gvBilling.LocateByValue("ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            if (rowHandle < 0)
            {
                gvBilling.AddNewRow(); 
            }
            else
            {
                int newQuantity = Convert.ToInt32(txtQuantity.EditValue) + Convert.ToInt32(gvBilling.GetRowCellValue(rowHandle, "QUANTITY"));
                if (newQuantity > 0)
                {
                    gvBilling.SetRowCellValue(rowHandle, "QUANTITY", newQuantity); 
                }
                else
                {
                    object billDetailID = gvBilling.GetRowCellValue(rowHandle, "BILLDETAILID");
                    SNo = Convert.ToInt32(gvBilling.GetRowCellValue(rowHandle, "SNO"));
                    DataTable dtSNos = new DataTable();
                    dtSNos.Columns.Add("BILLDETAILID", typeof(int));
                    dtSNos.Columns.Add("SNO", typeof(int));

                    for (int curRowHandle = rowHandle - 1; curRowHandle >= 0; curRowHandle--)
                    {
                        gvBilling.SetRowCellValue(curRowHandle, "SNO", SNo);
                        dtSNos.Rows.Add(gvBilling.GetRowCellValue(curRowHandle, "BILLDETAILID"), SNo);
                        SNo++;
                    }

                    billingRepository.DeleteBillDetail(billDetailID, userID, dtSNos);
                    gvBilling.DeleteRow(rowHandle);
                    UpdateSummary();
                }
            }

            gvBilling.GridControl.BindingContext = new BindingContext();
            gvBilling.GridControl.DataSource = billObj.dtBillDetails;

            rowHandle = gvBilling.LocateByValue("ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            if (rowHandle >= 0)
            {
                CalculateFields(rowHandle);
                SaveBillDetail(rowHandle);
            }
            ClearItemData();
        }

        private void sluItemCode_EditValueChanged(object sender, EventArgs e)
        {
            if(sluItemCode.EditValue == null)
            {
                return;
            }

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

            //txtItemName.EditValue = (sluItemCode.GetSelectedDataRow() as DataRowView)?.Row["ITEMNAME"];
            txtMRP.EditValue = drSelectedPrice["MRP"];
            txtSalePrice.EditValue = drSelectedPrice["SALEPRICE"];

            txtQuantity.EditValue = 1;

            if (chkSingleQuantity.Checked)
            {
                txtQuantity_KeyDown(txtQuantity, new KeyEventArgs(Keys.Enter));
            }
            else
            {
                txtQuantity.Focus();
                txtQuantity.SelectAll();
            }

        }

        private void gvBilling_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            gvBilling.SetRowCellValue(e.RowHandle, "BILLDETAILID", -1);
            gvBilling.SetRowCellValue(e.RowHandle, "BILLID", billObj.BillID);
            gvBilling.SetRowCellValue(e.RowHandle, "ITEMPRICEID", drSelectedPrice["ITEMPRICEID"]);
            gvBilling.SetRowCellValue(e.RowHandle, "SNO", SNo++);
            gvBilling.SetRowCellValue(e.RowHandle, "ITEMNAME", sluItemCode.Text);
            gvBilling.SetRowCellValue(e.RowHandle, "ITEMCODE", txtItemCode.EditValue);
            gvBilling.SetRowCellValue(e.RowHandle, "MRP", drSelectedPrice["MRP"]);
            gvBilling.SetRowCellValue(e.RowHandle, "SALEPRICE", drSelectedPrice["SALEPRICE"]);
            gvBilling.SetRowCellValue(e.RowHandle, "GSTCODE", drSelectedPrice["GSTCODE"]);
            gvBilling.SetRowCellValue(e.RowHandle, "QUANTITY", txtQuantity.EditValue);
            gvBilling.SetRowCellValue(e.RowHandle, "WEIGHTINKGS", 0.00);
        }
        
        private void btnCloseBill_Click(object sender, EventArgs e)
        {
            if (billObj.dtBillDetails.Rows.Count == 0)
            {
                XtraMessageBox.Show("No items to bill", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmPayment paymentForm = new frmPayment(billObj);
            paymentForm.ShowDialog();
            if (!paymentForm.IsPaid) { return; }

            DataSet nextBillDetails = billingRepository.FinishBill(userID, daySequenceID, billObj);

            // use this object for printing
            Bill oldBillObj = billObj.Clone() as Bill;

            LoadBillData(nextBillDetails);
        }

        private void CalculateFields(int rowHandle)
        {
            DataRow drBillDetail = (gvBilling.GetRow(rowHandle) as DataRowView).Row;
            decimal salePrice = Convert.ToDecimal(drBillDetail["SALEPRICE"])
                , cGSTPer = Convert.ToDecimal(drSelectedPrice["CGST"])
                , sGSTPer = Convert.ToDecimal(drSelectedPrice["SGST"])
                , iGSTPer = Convert.ToDecimal(drSelectedPrice["IGST"])
                , cess = Convert.ToDecimal(drSelectedPrice["CESS"])
                , billedAmount, cGSTValue, sGSTValue, iGSTValue, cessValue, totalGSTValue;

            int.TryParse(drBillDetail["QUANTITY"].ToString(), out int quantity);
            billedAmount = salePrice * quantity;
            cGSTValue = Math.Round((billedAmount * cGSTPer) / 100, 2);
            sGSTValue = Math.Round((billedAmount * sGSTPer) / 100, 2);
            iGSTValue = Math.Round((billedAmount * iGSTPer) / 100, 2);
            cessValue = Math.Round((billedAmount * cess) / 100, 2);

            totalGSTValue = cGSTValue + sGSTValue + iGSTValue + cessValue;

            drBillDetail["CGST"] = cGSTValue;
            drBillDetail["SGST"] = sGSTValue;
            drBillDetail["IGST"] = iGSTValue;
            drBillDetail["CESS"] = cessValue;
            drBillDetail["GSTVALUE"] = totalGSTValue;
            drBillDetail["BILLEDAMOUNT"] = billedAmount;

            UpdateSummary();
        }   
        
        private void SaveBillDetail(int rowHandle)
        {
            DataRow drBillDetail = (gvBilling.GetRow(rowHandle) as DataRowView).Row;
            int billDetailID = billingRepository.SaveBillDetail(drBillDetail, userID);
            drBillDetail["BILLDETAILID"] = billDetailID;
        }

        private void ClearItemData(bool focusItemCode = true)
        {
            txtItemCode.EditValue = null;
            sluItemCode.EditValue = null;
            txtMRP.EditValue = null;
            txtDiscount.EditValue = null;
            txtSalePrice.EditValue = null;
            txtQuantity.EditValue = 1;

            if (focusItemCode)
                txtItemCode.Focus();
        }

        private void UpdateSummary()
        {           
            billObj.Amount = gvBilling.Columns["BILLEDAMOUNT"].SummaryItem.SummaryValue;
            billObj.Quantity = gvBilling.Columns["QUANTITY"].SummaryItem.SummaryValue;
            lblTotalBill.Text = billObj.Amount.ToString();
            lblTotalItems.Text = billObj.Quantity.ToString();
        }
        
        private void LoadBillData(DataSet dsBillInfo)
        {
            billObj = Utility.GetBill(dsBillInfo);

            lblBillNumber.Text = billObj.BillNumber.ToString();

            lblLastBilledAmount.Text = billObj.LastBilledAmount.ToString();
            lblLastBilledQunatity.Text = billObj.LastBilledQuantity.ToString();

            lblLastBilledAmount.Text = string.IsNullOrEmpty(lblLastBilledAmount.Text) ? "0.00" : lblLastBilledAmount.Text;
            lblLastBilledQunatity.Text = string.IsNullOrEmpty(lblLastBilledQunatity.Text) ? "0" : lblLastBilledQunatity.Text;

            gcBilling.DataSource = billObj.dtBillDetails;

            UpdateSummary();
            SNo = billObj.dtBillDetails.Rows.Count + 1;

            txtItemCode.Focus();
        }

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            int rowHandle = sluItemCodeView.LocateByValue("ITEMCODE", txtItemCode.EditValue);
            if(rowHandle >=  0)
            {
                //sluItemCode.Enabled = false;
                sluItemCode.EditValue = sluItemCodeView.GetRowCellValue(rowHandle, "ITEMCODEID");
                txtQuantity.Focus();
            }
            else
            {
                ClearItemData(false);
                //sluItemCode.Enabled = true;
            }
        }

        private void btnSaveBill_Click(object sender, EventArgs e)
        {
            if (billObj.dtBillDetails.Rows.Count == 0)
            {
                XtraMessageBox.Show("No items to draft", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataSet nextBillDetails = billingRepository.DraftBill(userID, daySequenceID, billObj.BillID);
            LoadBillData(nextBillDetails);
        }

        private void btnLoadDraftBill_Click(object sender, EventArgs e)
        {
            if(billObj.dtBillDetails.Rows.Count > 0)
            {
                DialogResult dialogResult = XtraMessageBox.Show("Pending items in the bill, Do you want to draft bill?", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Cancel) return;

                btnSaveBill_Click(null, null);
            }

            frmDraftList draftListForm = new frmDraftList(daySequenceID);
            draftListForm.ShowDialog();
            if(draftListForm.SelectedDraftBillID > 0)
            {
                DataSet dsBillDetails = billingRepository.GetBill(daySequenceID, draftListForm.SelectedDraftBillID);
                LoadBillData(dsBillDetails);
            }
        }
    }
}