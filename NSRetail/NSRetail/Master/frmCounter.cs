﻿using DataAccess;
using DevExpress.XtraEditors;
using Entity;
using ErrorManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.Master
{
    public partial class frmCounter : DevExpress.XtraEditors.XtraForm
    {
        Counter ObjCounter = null;
        MasterRepository objMasterRep = new MasterRepository();
        public frmCounter(Counter _ObjCounter)
        {
            InitializeComponent();
            ObjCounter = _ObjCounter;
        }

        private void frmCounter_Load(object sender, EventArgs e)
        {
            cmbBranch.Properties.DataSource = objMasterRep.GetBranch();
            cmbBranch.Properties.DisplayMember = "BRANCHNAME";
            cmbBranch.Properties.ValueMember = "BRANCHID";
            if (Convert.ToInt32(ObjCounter.COUNTERID) > 0)
            {
                this.Text = "Edit Counter";
                txtCounterName.EditValue = ObjCounter.COUNTERNAME;
                cmbBranch.EditValue = ObjCounter.BRANCHID;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;
                ObjCounter.COUNTERNAME = txtCounterName.EditValue;
                ObjCounter.BRANCHID = cmbBranch.EditValue;
                ObjCounter.UserID = Utility.UserID;
                objMasterRep.SaveCounter(ObjCounter);
                ObjCounter.IsSave = true;
                this.Close();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ObjCounter.IsSave = false;
            this.Close();
        }
    }
}