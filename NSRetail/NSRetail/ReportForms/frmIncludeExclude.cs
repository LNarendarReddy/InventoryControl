using System.Collections.Generic;

namespace NSRetail.ReportForms
{
    public partial class frmIncludeExclude : DevExpress.XtraEditors.XtraForm
    {
        public frmIncludeExclude(List<IncludeSettings> includeSettings)
        {
            InitializeComponent();
            gcIncExc.DataSource = includeSettings;
            gvIncExc.FocusedRowHandle = 0;
            gvIncExc.FocusedColumn = gcInclude;
        }

        private void gvIncExc_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)System.Windows.Forms.Keys.Enter)
            {
                gvIncExc.MoveNext();
            }
        }

        private void btnApplyAndSearch_Click(object sender, System.EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }

    public class IncludeSettings
    {
        public IncludeSettings(string columnName, string parameterName, List<string> relatedColumns, bool included = false)
        {
            ColumnName = columnName;
            ParameterName = parameterName;
            RelatedColumns = relatedColumns;
            Included = included;
        }

        public string ColumnName { get; set; }

        public bool Included { get; set; }

        public string IncludedText => Included ? "Yes" : "No";

        public string ParameterName { get; set; }

        public List<string> RelatedColumns { get; set; }
    }
}