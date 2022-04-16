using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DevExpress.XtraSplashScreen;
using System.Reflection;
using System.ComponentModel;

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
            //Assembly asm = typeof(DevExpress.UserSkins.IITSkin).Assembly;
            //DevExpress.Skins.SkinManager.Default.RegisterAssembly(asm);
            //BonusSkins.Register();
            //UserLookAndFeel.Default.SetSkinStyle("IITSkin");
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
    //public class SkinRegistration : Component
    //{
    //    public SkinRegistration()
    //    {
    //        DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.IITSkin).Assembly);
    //    }
    //}
}
