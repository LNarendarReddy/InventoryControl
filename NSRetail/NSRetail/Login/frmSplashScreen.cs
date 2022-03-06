using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Reflection;

namespace NSRetail
{
    public partial class frmSplashScreen : SplashScreen
    {
        public frmSplashScreen()
        {
            InitializeComponent();


            string ver = Convert.ToString(Assembly.GetExecutingAssembly().GetName().Version);

            this.labelControl1.Text = "Ver. " + ver + " | Copyright © 1998-" + Convert.ToString(DateTime.Now.Year) + " NSoftsol Private Limited.";
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }
    }
}