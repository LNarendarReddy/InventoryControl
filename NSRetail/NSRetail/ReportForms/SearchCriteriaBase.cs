using System;
using System.Collections.Generic;
using System.Data;

namespace NSRetail.ReportForms
{
    public partial class SearchCriteriaBase : DevExpress.XtraEditors.XtraUserControl
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
            };
        }

        public virtual DataTable GetData() => throw new NotImplementedException();

        public virtual Dictionary<string, string> SpecificColumnHeaders { get; }

        public virtual List<string> ForceShowColumns { get; }

        public virtual List<string> TotalSummaryFields { get; }

        public Dictionary<string, string> GenericColumnHeaders { get; }

    }
}
