using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraTab.ViewInfo;
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
            DataSet dsTemp = dataSet.Copy();

            List<string> supplierIndentAllowedColumns = new List<string>()
            {
                "ITEMID", "STOCKENTRYDETAILID", "REQUIREDITEMINDENT", "CALCULATEDITEMINDENT"
                , "MRP", "COSTPRICEWT", "BRANCHQUANTITY", "REQUIREDBRANCHSTOCK", "INNERCASEQTY"
                , "OUTERCASEQTY", "WHQTY", "DESIREDITEMINDENT"
            };

            List<string> supplierIndentFTDetailAllowedColumns = new List<string>()
            {
                "ITEMID", "BRANCHID", "STOCKENTRYDETAILID", "BRANCHQUANTITY", "REQUIREDITEMINDENT"
                , "INTRANSITQTY", "THRESHOLD", "REQUIREDBRANCHSTOCK"
            };

            DataTable dtSupplierIndent = Utility.CleanUpColumns(dsTemp.Tables[0], supplierIndentAllowedColumns);
            DataTable dtSupplierIndentFTDetail = Utility.CleanUpColumns(dsTemp.Tables[1], supplierIndentFTDetailAllowedColumns);

            new DataRepository().ExecuteNonQuery("USP_SAVE_SUPPLIERINDENT_BATCHWISE", true
                , new Dictionary<string, object>()
                {
                    { "SupplierID", supplierID },
                    { "CategoryID", categoryId },
                    { "SafetyDays", safetyDays },
                    { "ManufacturerID", manufacturerId },
                    { "IndentItemSelectionType", indentSelectionType },
                    { "BranchID", branchId },
                    { "UserID", Utility.UserID },
                    { "SUPPLIERINDENTDETAIL", dtSupplierIndent },
                    { "ITEMSTOCKDATA", dtSupplierIndentFTDetail }
                }, true);

            XtraMessageBox.Show("Indents saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}