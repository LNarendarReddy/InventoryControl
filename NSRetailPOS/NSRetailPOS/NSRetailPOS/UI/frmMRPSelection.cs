using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace NSRetailPOS.UI
{
    public partial class frmMRPSelection : DevExpress.XtraEditors.XtraForm
    {
        public object drSelected = null;
        public bool _IsSave = false;

        public frmMRPSelection(DataTable dtMRPList,object ItemCode, Object ItemName)
        {
            InitializeComponent();
            gcMRPList.DataSource = dtMRPList;
            txtItemCode.EditValue = ItemCode;
            txtItemName.EditValue = ItemName;
            this.gvMRPList.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.gvMRPList.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.gvMRPList.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvMRPList.Appearance.FocusedCell.Options.UseFont = true;
            this.gvMRPList.Appearance.FocusedRow.BackColor = System.Drawing.Color.White;
            this.gvMRPList.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.gvMRPList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvMRPList.Appearance.FocusedRow.Options.UseFont = true;
            this.gvMRPList.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.gvMRPList.Appearance.FooterPanel.Options.UseFont = true;
            this.gvMRPList.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvMRPList.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvMRPList.Appearance.Row.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.gvMRPList.Appearance.Row.Options.UseFont = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                _IsSave = true;
                drSelected = gvMRPList.GetFocusedRow();
                this.Close();
            }
            catch (Exception) { }
        }

        private void frmMRPSelection_Load(object sender, EventArgs e)
        {

        }
    }
}