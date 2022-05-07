using DataAccess;
using DevExpress.XtraEditors;
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
    public partial class frmBRefundDetail : DevExpress.XtraEditors.XtraForm
    {
        object CounterID = null,BRefundID = null;
        public bool IsSave = false;
        public frmBRefundDetail(DataTable dtItems,object _CounterID,object _BRefundID, bool IsAccepted = false)
        {
            InitializeComponent();
            btnSave.Enabled = !IsAccepted;
            gcItems.DataSource = dtItems;
            CounterID = _CounterID;
            BRefundID = _BRefundID;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBRefundDetail_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure want to accept refund sheet", "Confirmation!",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            try
            {
                DataTable dt = (gcItems.DataSource as DataTable).Copy();
                dt.Columns.Remove("SNO");
                dt.Columns.Remove("ITEMCODE");
                dt.Columns.Remove("ITEMNAME");
                dt.Columns.Remove("MRP");
                dt.Columns.Remove("SALEPRICE");
                dt.Columns.Remove("REASONID");
                dt.Columns.Remove("DELETEDDATE");
                dt.Columns.Remove("REASONNAME");
                new POSRepository().AcceptBRefund(CounterID, BRefundID, Utility.UserID, dt);
                IsSave = true;
                this.Close();
            }
            catch (Exception ex){XtraMessageBox.Show(ex.Message);}
        }
    }
}