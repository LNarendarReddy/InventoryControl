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

namespace NSRetail.ReportForms.Stock.StockReports
{
    public partial class frmTransitDetails : DevExpress.XtraEditors.XtraForm
    {
        public frmTransitDetails( DataTable dt)
        {
            InitializeComponent();
            gcItems.DataSource = dt;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}