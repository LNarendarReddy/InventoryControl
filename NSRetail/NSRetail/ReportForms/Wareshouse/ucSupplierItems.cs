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

namespace NSRetail.ReportForms.Wareshouse
{
    public partial class ucSupplierItems : SearchCriteriaBase
    {
        public ucSupplierItems()
        {
            InitializeComponent();

            Dictionary<string, string> columnHeaders = new Dictionary<string, string>
            {
                { "SKUCODE", "SKU Code" }
                , { "ITEMCODE", "Item Code" }
                , { "ITEMNAME", "Item Name" }
                , { "CATEGORYNAME", "Category" }
                , { "MRP", "MRP" }
                , { "SALEPRICE", "Sale Price" }
                , { "COSTPRICEWOT", "Cost Price WOT" }
                , { "COSTPRICEWT", "Cost Price WT" }
            };

            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer();
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";

            SetFocusControls(cmbSupplier, cmbSupplier, columnHeaders);
        }
        public override DataTable GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "@SUPPLIERID",  cmbSupplier.EditValue }
            };
            return GetReportData("USP_R_SUPPLIERITEMS", parameters);
        }
    }
}
