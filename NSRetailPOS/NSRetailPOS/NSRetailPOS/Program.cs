using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraSplashScreen;
using NSRetailPOS.Data;
using NSRetailPOS.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Assembly asm = typeof(DevExpress.UserSkins.NSRetailPOSSkin).Assembly;
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(asm);
            SplashScreenManager.RegisterUserSkins(typeof(DevExpress.UserSkins.NSRetailPOSSkin).Assembly);
            UserLookAndFeel.Default.SetSkinStyle("NSRetailPOSSkin");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();
            DataTable dt = new POSRepository().GetPOSConfiguration();
            if(dt != null && dt.Rows.Count > 0)
            {
                Utility.branchinfo.BranchID = dt.Rows[0]["BRANCHID"];
                Utility.branchinfo.BranchCounterID = dt.Rows[0]["BRANCHCOUNTERID"];
                Application.Run(new frmLogin());
            }
            else
                Application.Run(new frmConfiguration());
        }
    }
    public class SkinRegistration : Component
    {
        public SkinRegistration()
        {
            DevExpress.Skins.SkinManager.Default.RegisterAssembly(typeof(DevExpress.UserSkins.NSRetailPOSSkin).Assembly);
        }
    }
}
