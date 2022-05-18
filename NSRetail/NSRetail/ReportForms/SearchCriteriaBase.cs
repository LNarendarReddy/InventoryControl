using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class SearchCriteriaBase : XtraUserControl
    {
        public SearchCriteriaBase()
        {
            InitializeComponent();
            GenericColumnHeaders = new Dictionary<string, string>()
            {
                // (for performance management) Add only common column names here
                // for specific column names, override SpecificColumnHeaders property in child class and specify the values there
                {"BRANCHNAME", "Branch Name" }
                , {"ITEMNAME", "Item Name"}
                , {"SKUCODE", "SKU Code"}
                , {"ITEMCODE", "Item Code"}
                , {"CATEGORY", "Category"}
                , {"SUBCATEGORY", "Sub Category"}
                , {"SNO", "SNo"}
            };
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

        public virtual IEnumerable<string> ButtonColumns { get; }

        public virtual void ActionExecute(string buttonText, DataRow drFocusedRow) { }
    }
}
