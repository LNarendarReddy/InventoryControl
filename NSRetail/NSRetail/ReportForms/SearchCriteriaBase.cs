using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class SearchCriteriaBase : XtraUserControl
    {
        List<string> buttonColumns;
        LookUpEdit cmbPeriodicity;

        public LookUpEdit Periodicity => cmbPeriodicity;

        protected GridView ResultGridView => (ParentForm as ReportForms.frmReportPlaceHolder)?.ResultsGridView;

        protected GridControl ResultGrid => (ParentForm as ReportForms.frmReportPlaceHolder)?.ResultsGrid;

        public SearchCriteriaBase()
        {
            InitializeComponent();
            GenericColumnHeaders = new Dictionary<string, string>()
            {
                // (for performance management) Add only common column names here
                // for specific column names, override SpecificColumnHeaders property in child class and specify the values there
                {"BRANCHNAME", "Branch" }
                , {"ITEMNAME", "Item Name"}
                , {"SKUCODE", "SKU Code"}
                , {"ITEMCODE", "Item Code"}
                , {"CATEGORYNAME", "Category"}
                , {"SUBCATEGORYNAME", "Sub Category"}
                , {"SNO", "SNo"}
                , {"COUNTERNAME", "Counter"}
                , {"BILLNUMBER", "Bill Number"}
                , {"SALEPRICE", "Sale Price"}
                , {"QUANTITY", "Quantity"}
                , {"BILLEDAMOUNT", "Billed Amount"}
                , {"CREATEDBY", "User Name"}
                , {"CREATEDDATE", "Created Date"}
            };

            buttonColumns = new List<string>();
        }

        public virtual DataTable GetData() => throw new NotImplementedException();

        public virtual Dictionary<string, string> SpecificColumnHeaders { get; }

        public virtual IEnumerable<string> ForceShowColumns { get; }

        public virtual IEnumerable<string> TotalSummaryFields { get; }

        public Dictionary<string, string> GenericColumnHeaders { get; }

        public DataTable GetReportData(string procName, Dictionary<string, object> parameters)
        {
            DataTable reportdata = null;
            try
            {
                reportdata = new ReportRepository().GetReportData(procName, parameters);
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return reportdata;
        }

        public virtual IEnumerable<BaseEdit> MandatoryFields { get; }

        public virtual IEnumerable<string> EditableColumns { get; }

        public virtual IEnumerable<string> HiddenColumns { get; }

        public virtual IEnumerable<string> ButtonColumns => buttonColumns;

        public virtual Control FirstControl { get; }

        public virtual Control LastControl { get; }

        public virtual void ActionExecute(string buttonText, DataRow drFocusedRow) { }

        protected void SetPeriodicty(LookUpEdit cmb)
        {
            cmbPeriodicity = cmb;
            DataTable dtPeriodicity = new DataTable();
            dtPeriodicity.Columns.Add("Periodicityvalue", typeof(string));
            dtPeriodicity.Rows.Add(new []{ "Daily" });
            dtPeriodicity.Rows.Add(new []{ "Monthly" });
            dtPeriodicity.Rows.Add(new []{ "Yearly" });

            cmbPeriodicity.Properties.DataSource = dtPeriodicity;
            cmbPeriodicity.Properties.ValueMember = "Periodicityvalue";
            cmbPeriodicity.Properties.DisplayMember = "Periodicityvalue";
            cmbPeriodicity.EditValue = "Daily";
        }

        public virtual bool ValidateMandatoryFields()
        {
            var layoutControlOfSearch = Controls.OfType<LayoutControl>().First();
            var missingValues = MandatoryFields?.Where(x => x.EditValue == null)
                                    .Select(x => $"{Environment.NewLine}\t* " + layoutControlOfSearch.GetItemByControl(x).Text);

            if (missingValues != null && missingValues.Any())
            {
                XtraMessageBox.Show("Please select the values for : " + Environment.NewLine + string.Join(string.Empty, missingValues)
                    , "Mandatoy", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MandatoryFields.First(x => x.EditValue == null).Focus();
                return false;
            }

            return true;
        }        
    }
}
