﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.ReportForms.Branch.POSReports
{
    public partial class frmDayClosureSummary : DevExpress.XtraEditors.XtraForm
    {
        public frmDayClosureSummary(DataTable dtDayClosureSummary)
        {
            InitializeComponent();
           gcDayClosureSummary.DataSource = dtDayClosureSummary;
        }

        private void frmDayClosureSummary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}