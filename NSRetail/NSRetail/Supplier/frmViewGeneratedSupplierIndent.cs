using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraTab.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.Supplier
{
    public partial class frmViewGeneratedSupplierIndent : DevExpress.XtraEditors.XtraForm
    {
        private readonly DataSet dataSet;
        private readonly object supplierID;
        private readonly object categoryId;
        private readonly object safetyDays;
        private readonly object manufacturerId;
        private readonly object indentSelectionType;
        private readonly object branchId;

        public frmViewGeneratedSupplierIndent(DataSet dataSet, object supplierID, object categoryId,
            object safetyDays, object manufacturerId, object indentSelectionType, object branchId)
        {
            InitializeComponent();
            this.dataSet = dataSet;
            this.supplierID = supplierID;
            this.categoryId = categoryId;
            this.safetyDays = safetyDays;
            this.manufacturerId = manufacturerId;
            this.indentSelectionType = indentSelectionType;
            this.branchId = branchId;
        }

        private void frmViewGeneratedSupplierIndent_Load(object sender, EventArgs e)
        {
            gcSupplierIndent.DataSource = dataSet.Tables[0];
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            new DataRepository().ExecuteNonQuery("USP_SAVE_SUPPLIERINDENT_BATCHWISE", true
                , new Dictionary<string, object>()
                {
                    { "SupplierID", supplierID },
                    { "CategoryID", categoryId },
                    { "SafetyDays", safetyDays },
                    { "ManufacturerID", manufacturerId },
                    { "IndentItemSelectionType", indentSelectionType },
                    { "BranchID", branchId },
                    { "SUPPLIERINDENTDETAIL", dataSet.Tables[0] },
                    { "ITEMSTOCKDATA", dataSet.Tables[1] }
                }, true);

            XtraMessageBox.Show("Indents saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}