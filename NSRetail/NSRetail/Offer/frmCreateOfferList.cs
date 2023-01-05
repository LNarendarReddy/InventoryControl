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
    public partial class frmCreateOfferList : DevExpress.XtraEditors.XtraForm
    {
        Offer Offer = null;
        object BaseOfferID = null;
        object CategoryID = null;
        OfferRepository offerRepository = new OfferRepository();

        public frmCreateOfferList(object _BaseOfferID, object _CategoryID)
        {
            InitializeComponent();
            BaseOfferID = _BaseOfferID;
            CategoryID = _CategoryID;
        }
        private void frmCreateOfferList_Load(object sender, EventArgs e)
        {
            cmbOffertype.DataSource = offerRepository.GetOfferType(1);
            cmbOffertype.ValueMember = "OFFERTYPEID";
            cmbOffertype.DisplayMember = "OFFERTYPENAME";

            cmbItemCode.DataSource = new ItemCodeRepository().GetItemCodeByCategory(CategoryID);
            cmbItemCode.ValueMember = "ITEMCODEID";
            cmbItemCode.DisplayMember = "ITEMCODE";

            gcOffer.DataSource = offerRepository.GetOfferByBaseOffer(BaseOfferID);
        }
        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            gvOffer.DeleteRow(gvOffer.FocusedRowHandle);
        }
        private void frmCreateOfferList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                this.Close();
        }
    }
}