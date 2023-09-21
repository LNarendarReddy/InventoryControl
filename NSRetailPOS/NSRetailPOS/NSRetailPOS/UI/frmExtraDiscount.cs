using DevExpress.XtraEditors;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
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
    public partial class frmExtraDiscount : DevExpress.XtraEditors.XtraForm
    {
        public BillDetail BillDetailObj { get; }

        public frmExtraDiscount(BillDetail billDetailObj)
        {
            InitializeComponent();
            BillDetailObj = billDetailObj;
        }

        private void frmExtraDiscount_Load(object sender, EventArgs e)
        {
            txtItemCode.EditValue = BillDetailObj.ItemCode;
            txtItemName.EditValue = BillDetailObj.ItemName;
            txtMRP.EditValue = BillDetailObj.MRP;
            txtSalePrice.EditValue = BillDetailObj.SalePrice;
            txtQtyOrWghtInKGs.EditValue = BillDetailObj.IsOpenItem.Equals(true) ? BillDetailObj.WeightInKGs : BillDetailObj.Quantity;
            txtBilledAmount.EditValue = BillDetailObj.BilledAmount;            
        }

        private void radioGroup1_EditValueChanged(object sender, EventArgs e)
        {
            CalculateValues();
        }

        private void txtDiscountValue_EditValueChanged(object sender, EventArgs e)
        {
            CalculateValues();
        }

        private void CalculateValues()
        {
            if (!dxValidationProvider1.Validate()) 
            {
                txtActualSalePrice.EditValue = null;
                txtNewBilledAmount.EditValue = null;
                return; 
            }

            double MRP = Convert.ToDouble(txtMRP.EditValue);
            double qtyOrWeight = Convert.ToDouble(txtQtyOrWghtInKGs.EditValue);
            double discValue = Convert.ToDouble(txtDiscountValue.EditValue);
            double actualSalePrice = 0;

            switch(radioGroup1.EditValue)
            {
                case "Discount %":
                    actualSalePrice = MRP - (MRP * (discValue / 100));
                    break;
                case "Discount Flat":
                    actualSalePrice = MRP - discValue;
                    break;
                case "Fixed Rate":
                    actualSalePrice = discValue;
                    break;
            }
            double newBilledAmount = actualSalePrice * qtyOrWeight;
            double totalDiscount = (MRP * qtyOrWeight) - newBilledAmount;

            txtActualSalePrice.EditValue = actualSalePrice;
            txtNewBilledAmount.EditValue = newBilledAmount;
            txtTotalDiscount.EditValue = totalDiscount;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate()) return;

            BillDetailObj.dtBillDetails = new BillingRepository().AddExtraDiscount(BillDetailObj.BillDetailID, txtNewBilledAmount.EditValue, null);
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}