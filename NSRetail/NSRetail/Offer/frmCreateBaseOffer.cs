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
    public partial class frmCreateBaseOffer : DevExpress.XtraEditors.XtraForm
    {
        BaseOffer baseOffer = null;
        public frmCreateBaseOffer(BaseOffer _baseOffer)
        {
            InitializeComponent();
            baseOffer = _baseOffer;
        }

        private void frmAddOffer_Load(object sender, EventArgs e)
        {
            cmbCategory.Properties.DataSource = new MasterRepository().GetCategory();
            cmbCategory.Properties.DisplayMember = "CATEGORYNAME";
            cmbCategory.Properties.ValueMember = "CATEGORYID";
            if(!baseOffer.BASEOFFERID.Equals(0))
            {
                txtOfferCode.EditValue = baseOffer.OFFERCODE;
                txtOfferName.EditValue = baseOffer.OFFERNAME;
                dtpStartDate.EditValue = baseOffer.STARTDATE;
                dtpEndDate.EditValue = baseOffer.ENDDATE;
                cmbCategory.EditValue = baseOffer.CATEGORYID;
                cmbCategory.Enabled = false;
                txtOfferCode.Enabled = false;
                txtOfferName.Enabled = false;
            }
            else
            {
                dtpStartDate.EditValue = DateTime.Now;
                dtpEndDate.EditValue = DateTime.Now.AddDays(30);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dxValidationProvider1.Validate())
                return;
            try
            {
                baseOffer.OFFERCODE = txtOfferCode.EditValue;
                baseOffer.OFFERNAME = txtOfferName.EditValue;
                baseOffer.STARTDATE = dtpStartDate.EditValue;
                baseOffer.ENDDATE = dtpEndDate.EditValue;
                baseOffer.CATEGORYID = cmbCategory.EditValue;
                baseOffer.CATEGORYNAME = cmbCategory.Text;
                baseOffer.UserID = Utility.UserID;
                new OfferRepository().SaveBaseOffer(baseOffer);
                baseOffer.IsSave = true;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorManagement.ErrorMgmt.ShowError(ex);
                ErrorManagement.ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}