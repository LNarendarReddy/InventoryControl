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

namespace NSRetail
{
    public partial class frmDayClosureSummary : DevExpress.XtraEditors.XtraForm
    {
        public frmDayClosureSummary(DataTable dtDayClosureSummary)
        {
            InitializeComponent();
           gcDayClosureSummary.DataSource = dtDayClosureSummary;
        }
    }
}