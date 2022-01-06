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
        }

        private void txtBillNumber_Leave(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new RefundRepository().GetBillByNumber(txtBillNumber.EditValue);
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
                DataTable dtCloned = dt.Copy();
                foreach (DataColumn dc in dtCloned.Columns)
                {
                    if (dc.ColumnName == "BILLDETAILID" ||
                         dc.ColumnName == "REFUNDQUANTITY" ||
                         dc.ColumnName == "REFUNDWEIGHTINKGS" ||
                         dc.ColumnName == "REFUNDAMOUNT")
                        continue;
                    dt.Columns.Remove(dc.ColumnName);
                }
                new RefundRepository().InsertCRefund(dt,Utility.logininfo.UserID);
                DataView dv = dtCloned.DefaultView;
                dv.RowFilter = "REFUNDQUANTITY > 0";
                rptCRefund rpt = new rptCRefund(dv.ToTable());
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
                    , cGSTPer = Convert.ToDecimal(drBillDetail["CGSTDESC"] ?? drBillDetail["CGSTDESC"])
                    , sGSTPer = Convert.ToDecimal(drBillDetail["SGSTDESC"] ?? drBillDetail["SGSTDESC"])
                    , cess = Convert.ToDecimal(drBillDetail["CESSDESC"] ?? drBillDetail["CESSDESC"])
                    , billedAmount, cGSTValue, sGSTValue, cessValue, totalGSTValue, Discount;

                billedAmount = salePrice * rquantity;
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
                drBillDetail["REFUNDAMOUNT"] = billedAmount;
                drBillDetail["DISCOUNT"] = Discount;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}