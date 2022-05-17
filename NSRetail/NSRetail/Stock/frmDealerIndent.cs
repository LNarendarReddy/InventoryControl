using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmDealerIndent : DevExpress.XtraEditors.XtraForm
    {
        DealerIndent dealerIndent = null;
        public frmDealerIndent(DealerIndent _dealerIndent,string DealerName)
        {
            InitializeComponent();
            dealerIndent = _dealerIndent;
            txtDealerName.EditValue = DealerName;
        }

        private void frmDealerIndent_Load(object sender, EventArgs e)
        {
            btnSave.Text = Utility.Role == "Admin" ? "Approve" : "Save";
            dtpFromDate.EditValue = dealerIndent.FromDate;
            dtpToDate.EditValue = dealerIndent.ToDate;
            gcSupplierIndent.DataSource = dealerIndent.dtSupplierIndent;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                dealerIndent.IsApproved = Utility.Role == "Admin" ? true : false;
                new ReportRepository().SaveSupplierIndent(dealerIndent);
                dealerIndent.IsSave = true;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvSupplierIndent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                gvSupplierIndent.MoveNext();
        }
    }
}