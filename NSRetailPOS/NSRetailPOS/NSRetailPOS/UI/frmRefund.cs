using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using NSRetailPOS.Data;
using NSRetailPOS.Reports;
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
    public partial class frmRefund : DevExpress.XtraEditors.XtraForm
    {
        public frmRefund()
        {
            InitializeComponent();
            this.gvBillDetails.Appearance.FocusedCell.BackColor = System.Drawing.Color.SaddleBrown;
            this.gvBillDetails.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.gvBillDetails.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.gvBillDetails.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvBillDetails.Appearance.FocusedCell.Options.UseFont = true;
            this.gvBillDetails.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvBillDetails.Appearance.FocusedRow.BackColor = System.Drawing.Color.White;
            this.gvBillDetails.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.gvBillDetails.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvBillDetails.Appearance.FocusedRow.Options.UseFont = true;
            this.gvBillDetails.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.gvBillDetails.Appearance.FooterPanel.Options.UseFont = true;
            this.gvBillDetails.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvBillDetails.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvBillDetails.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvBillDetails.Appearance.Row.Options.UseFont = true;
        }

        private void txtBillNumber_Leave(object sender, EventArgs e)
        {
            if (txtBillNumber.EditValue == null)
                return;
            try
            {
                DataTable dt = new RefundRepository().GetBillByNumber(txtBillNumber.EditValue);
                if (dt.Rows.Count == 0)
                    return;
                this.Text = "Customer Refund - " + txtBillNumber.EditValue;
                txtBillDate.EditValue = dt.Rows[0]["CREATEDDATE"];
                gcBillDetails.DataSource = dt;
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
            try
            {
                DataTable dt = gcBillDetails.DataSource as DataTable;
                DataView dv = dt.DefaultView;
                dv.RowFilter = "REFUNDQUANTITY > 0";
                DataTable dtFiltered = dv.ToTable();
                if (dtFiltered.Rows.Count == 0)
                    throw new Exception("Refund quantity should be greater than '0'");
                new RefundRepository().InsertCRefund(dtFiltered, Utility.logininfo.UserID);
                rptCRefund rpt = new rptCRefund(dtFiltered);
                rpt.Parameters["GSTIN"].Value = "37AADFV6514H1Z2";
                rpt.Parameters["FSSAI"].Value = "10114004000548";
                rpt.Parameters["Address"].Value = Utility.branchinfo.BranchAddress;
                rpt.Parameters["BillDate"].Value = DateTime.Now;
                rpt.Parameters["BillNumber"].Value = txtBillNumber.EditValue;
                rpt.Parameters["BranchName"].Value = Utility.branchinfo.BranchName;
                rpt.Parameters["CounterName"].Value = Utility.branchinfo.BranchCounterName;
                rpt.Parameters["Phone"].Value = Utility.branchinfo.PhoneNumber;
                rpt.Parameters["UserName"].Value = Utility.logininfo.UserFullName;
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

        private void gcBillDetails_Click(object sender, EventArgs e)
        {

        }
    }
}