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

namespace NSRetail.ReportForms.Wareshouse.Differences
{
    public partial class ucIndentVsEntry : SearchCriteriaBase
    {
        public ucIndentVsEntry()
        {
            InitializeComponent();

            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer(true);
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.EditValue = 0;

            SetFocusControls(cmbSupplier, dtpToDate, new Dictionary<string, string>());
            dtpFromDate.EditValue = DateTime.Now.AddDays(-7);
            dtpToDate.EditValue = DateTime.Now;

            IncludeSettingsCollection = new List<IncludeSettings>()
            {
                new IncludeSettings("Date", "IncludeDate", new List<string>() { "IndentDate" }, true),
                new IncludeSettings("Supplier", "IncludeSupplier", new List<string>() { "DEALERNAME" }, true),
                new IncludeSettings("Category", "IncludeCategory", new List<string>() { "CATEGORYNAME" }, true),
                new IncludeSettings("Sub category", "IncludeSubCategory", new List<string>() { "SUBCATEGORYNAME" }, false),
                new IncludeSettings("Item Details", "IncludeItem", new List<string>() { "SKUCODE", "ITEMNAME", "IndentItem" }, true),
                new IncludeSettings("Indent #", "IncludeIndentNo", new List<string>() { "Indent #" }, true),
                new IncludeSettings("Invoice details", "IncludeInvoiceNo", new List<string>() { "Invoice #", "Invoice Date" }, false),
            };
        }


        public override object GetData()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "FromDate", dtpFromDate.EditValue },
                { "ToDate", dtpToDate.EditValue },
                { "CategoryIDs", cmbCategory.EditValue }, 
                { "SupplierID", cmbSupplier.EditValue },
            };

            return GetReportData("USP_RPT_SUPPLIERINDENTVSSTOCKENTRY", parameters);
        }
    }
}
