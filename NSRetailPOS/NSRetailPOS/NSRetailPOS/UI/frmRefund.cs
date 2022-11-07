using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using NSRetailPOS.Reports;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace NSRetailPOS.UI
{
    public partial class frmRefund : XtraForm, IBarcodeReceiver
    {
        object billID;

        public frmRefund()
        {
            InitializeComponent();

            Utility.SetGridFormatting(gvBillDetails);
        }

        private void txtBillNumber_Leave(object sender, EventArgs e)
        {
            if (txtBillNumber.EditValue == null)
                return;
            try
            {
                DataSet ds = new RefundRepository().GetBillByNumber(txtBillNumber.EditValue, Utility.loginInfo.RoleName.Equals("Store Admin"));
                if (ds.Tables["BILL"].Rows.Count == 0)
                    return;
                Text = "Customer Refund - " + txtBillNumber.EditValue;

                txtBillDate.EditValue = ds.Tables["BILL"].Rows[0]["CREATEDDATE"];
                txtCustomerName.EditValue = ds.Tables["BILL"].Rows[0]["CUSTOMERNAME"];
                txtCustomerPhone.EditValue = ds.Tables["BILL"].Rows[0]["CUSTOMERNUMBER"];
                billID = ds.Tables["BILL"].Rows[0]["BILLID"];

                txtCustomerName.Enabled = string.IsNullOrEmpty(txtCustomerName.EditValue?.ToString());
                txtCustomerPhone.Enabled = string.IsNullOrEmpty(txtCustomerPhone.EditValue?.ToString());

                gcBillDetails.DataSource = ds.Tables["BILLDETAILS"];
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate() ||
                XtraMessageBox.Show("Are you sure to continue?","Confirm!",
                MessageBoxButtons.YesNo,MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                DataTable dt = (gcBillDetails.DataSource as DataTable).Copy();
                DataView dv = dt.DefaultView;
                dv.RowFilter = "REFUNDQUANTITY > 0";
                DataTable dtFiltered = dv.ToTable();
                if (dtFiltered.Rows.Count == 0)
                    throw new Exception("Refund quantity should be greater than '0'");
                new RefundRepository().InsertCRefund(dtFiltered, Utility.loginInfo.UserID, billID, txtCustomerName.EditValue, txtCustomerPhone.EditValue);
                rptCRefund rpt = new rptCRefund(dtFiltered);
                rpt.Parameters["GSTIN"].Value = "37AAICV7240C1ZC";
                rpt.Parameters["CIN"].Value = "U51390AP2022PTC121579";
                rpt.Parameters["FSSAI"].Value = "10114004000548";
                rpt.Parameters["Address"].Value = Utility.branchInfo.BranchAddress;
                rpt.Parameters["BillDate"].Value = DateTime.Now;
                rpt.Parameters["BillNumber"].Value = txtBillNumber.EditValue;
                rpt.Parameters["BranchName"].Value = Utility.branchInfo.BranchName;
                rpt.Parameters["CounterName"].Value = Utility.branchInfo.BranchCounterName;
                rpt.Parameters["Phone"].Value = Utility.branchInfo.PhoneNumber;
                rpt.Parameters["UserName"].Value = Utility.loginInfo.UserFullName;
                rpt.Parameters["IsWithBill"].Value = true;
                rpt.Print();
                this.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void gvBillDetails_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                gvBillDetails.MoveNext();
        }

        private void gvBillDetails_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            GridColumn column = (e as EditFormValidateEditorEventArgs)?.Column ?? view.FocusedColumn;
            if (column.FieldName != "REFUNDQUANTITY") return;

            try
            {
                DataRow drBillDetail = (gvBillDetails.GetRow(gvBillDetails.FocusedRowHandle) as DataRowView).Row;
                int.TryParse(e.Value.ToString(), out int rquantity);
                int.TryParse(drBillDetail["QUANTITY"].ToString(), out int bquantity);
                if (bquantity < rquantity)
                {
                    e.Valid = false;
                    e.ErrorText = "Refund quantity cannot be greater than sold quantity";
                    return;
                }
                decimal salePrice = Convert.ToDecimal(drBillDetail["SALEPRICE"])
                    , MRP = Convert.ToDecimal(drBillDetail["MRP"])
                    , cGSTPer = Convert.ToDecimal(drBillDetail["CGSTDESC"])
                    , sGSTPer = Convert.ToDecimal(drBillDetail["SGSTDESC"])
                    , cess = Convert.ToDecimal(drBillDetail["CESSDESC"])
                    , billedAmount = Convert.ToDecimal(drBillDetail["BILLEDAMOUNT"])
                    , cGSTValue, sGSTValue, cessValue, totalGSTValue, Discount,
                    SalePriceAfterDiscount,RefundAmount;


                SalePriceAfterDiscount = billedAmount / bquantity;
                RefundAmount = SalePriceAfterDiscount * rquantity;
                 cGSTValue = Math.Round((billedAmount * cGSTPer) / 100, 2);
                sGSTValue = Math.Round((billedAmount * sGSTPer) / 100, 2);
                cessValue = Math.Round((billedAmount * cess) / 100, 2);

                totalGSTValue = cGSTValue + sGSTValue + cessValue;
                Discount = Math.Round((MRP - salePrice) * rquantity, 2);

                drBillDetail["CGST"] = cGSTValue;
                drBillDetail["SGST"] = sGSTValue;
                drBillDetail["CESS"] = cessValue;
                drBillDetail["GSTVALUE"] = totalGSTValue;
                drBillDetail["REFUNDWEIGHTINKGS"] = drBillDetail["WEIGHTINKGS"];
                drBillDetail["REFUNDAMOUNT"] = RefundAmount;
                drBillDetail["DISCOUNT"] = Discount;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void frmRefund_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btnSave_Click(null, null);
            }
        }

        public void ReceiveBarCode(string data)
        {
            txtBillNumber.Text = data;
            txtBillNumber_Leave(txtBillNumber, new EventArgs());
        }

        private void btnRePrint_Click(object sender, EventArgs e)
        {
            if (billID == null) return;
            try
            {
                DataSet dsLastBillDetails = new BillingRepository().GetLastBill(0, billID);
                Bill LastBillObj = Utility.GetBill(dsLastBillDetails);
                rptBill rpt = new rptBill(LastBillObj.dtBillDetails, LastBillObj.dtMopValues);
                rpt.Parameters["GSTIN"].Value = "37AAICV7240C1ZC";
                rpt.Parameters["CIN"].Value = "U51390AP2022PTC121579";
                rpt.Parameters["FSSAI"].Value = "10114004000548";
                rpt.Parameters["Address"].Value = Utility.branchInfo.BranchAddress;
                rpt.Parameters["BillDate"].Value = DateTime.Now;
                rpt.Parameters["BillNumber"].Value = LastBillObj.BillNumber;
                rpt.Parameters["CustomerName"].Value = LastBillObj.CustomerName;
                rpt.Parameters["CustomerNumber"].Value = LastBillObj.CustomerNumber;
                rpt.Parameters["CustomerGST"].Value = LastBillObj.CustomerGST;
                rpt.Parameters["TenderedCash"].Value = LastBillObj.TenderedCash;
                rpt.Parameters["TenderedChange"].Value = LastBillObj.TenderedChange;
                rpt.Parameters["IsDoorDelivery"].Value = LastBillObj.IsDoorDelivery;
                rpt.Parameters["BranchName"].Value = Utility.branchInfo.BranchName;
                rpt.Parameters["CounterName"].Value = Utility.branchInfo.BranchCounterName;
                rpt.Parameters["Phone"].Value = Utility.branchInfo.PhoneNumber;
                rpt.Parameters["UserName"].Value = Utility.loginInfo.UserFullName;
                rpt.Parameters["RoundingFactor"].Value = LastBillObj.Rounding;
                rpt.Parameters["IsDuplicate"].Value = true;
                rpt.Print();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}