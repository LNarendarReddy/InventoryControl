using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.XtraSplashScreen;

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
            SplashScreenManager.RegisterUserSkins(typeof(DevExpress.UserSkins.OfficeSkins).Assembly);
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
