﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.XtraSplashScreen;
using System.Reflection;
using System.ComponentModel;
using ErrorManagement;

namespace NSRetail
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += Application_ThreadException;
            Application.Run(new frmLogin());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ErrorMgmt.ShowError(e.Exception);
        }
    }
}
