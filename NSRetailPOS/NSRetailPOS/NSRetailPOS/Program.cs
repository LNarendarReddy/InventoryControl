using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraSplashScreen;
using NSRetailPOS.Data;
using NSRetailPOS.UI;
using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
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

            //Assembly asm = typeof(NSRetailPOSSkinDARK).Assembly;
            //SkinManager.Default.RegisterAssembly(asm);
            //SplashScreenManager.RegisterUserSkins(typeof(NSRetailPOSSkinDARK).Assembly);
            //UserLookAndFeel.Default.SetSkinStyle("NSRetailPOSSkinDARK");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();
            DataTable dt = new POSRepository().GetPOSConfiguration();
            if (dt != null && dt.Rows.Count > 0)
            {
                Utility.branchInfo.BranchID = dt.Rows[0]["BRANCHID"];
                Utility.branchInfo.BranchCounterID = dt.Rows[0]["BRANCHCOUNTERID"];
                Utility.branchInfo.BranchName = dt.Rows[0]["BRANCHNAME"];
                Utility.branchInfo.BranchCounterName = dt.Rows[0]["COUNTERNAME"];
                Application.Run(new frmLogin());
            }
            else
            {
                Application.Run(new frmConfiguration());
            }
        }               
    }
    public class SkinRegistration : Component
    {
        public SkinRegistration()
        {
            SkinManager.Default.RegisterAssembly(typeof(NSRetailPOSSkinDARK).Assembly);
        }
    }
}
