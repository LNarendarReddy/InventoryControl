using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraSplashScreen;
using NSRetailPOS.Data;
using NSRetailPOS.Logging;
using NSRetailPOS.UI;
using Serilog;
using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS
{
    static class Program
    {
        static Mutex singleton = new(true, "NSRetailPOS");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logger.Configure();

            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            try
            {
                if (!singleton.WaitOne(TimeSpan.Zero, true))
                {
                    Log.Warning("Instance already running");
                    DevExpress.XtraEditors.XtraMessageBox.Show("Instance already running!!");
                    return;
                }

                Assembly asm = typeof(NSRetailPOSBlack).Assembly;
                SkinManager.Default.RegisterAssembly(asm);
                SplashScreenManager.RegisterUserSkins(typeof(NSRetailPOSBlack).Assembly);
                UserLookAndFeel.Default.SetSkinStyle("NSRetailPOSBlack");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                SkinManager.EnableFormSkins();
                SkinManager.EnableMdiFormSkins();

                Log.Information("Loading POS configuration");

                DataTable dt = new POSRepository().GetPOSConfiguration();

                if (dt != null && dt.Rows.Count > 0)
                {
                    Utility.branchInfo.BranchID = dt.Rows[0]["BRANCHID"];
                    Utility.branchInfo.BranchCounterID = dt.Rows[0]["BRANCHCOUNTERID"];
                    Utility.branchInfo.BranchName = dt.Rows[0]["BRANCHNAME"];
                    Utility.branchInfo.BranchCounterName = dt.Rows[0]["COUNTERNAME"];

                    Log.Information(
                        "POS configuration loaded. BranchID: {BranchID}, CounterID: {CounterID}",
                        Utility.branchInfo.BranchID,
                        Utility.branchInfo.BranchCounterID);

                    Application.Run(frmLogin.Instance);
                }
                else
                {
                    Log.Warning("POS configuration not found. Opening configuration screen.");

                    Application.Run(new frmConfiguration());
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Unhandled fatal application error");
                throw;
            }
            finally
            {
                Logger.Shutdown();
            }
        }

        private static void Application_ThreadException(
            object sender,
            ThreadExceptionEventArgs e)
        {
            Log.Error(e.Exception, "Unhandled UI thread exception");
        }

        private static void CurrentDomain_UnhandledException(
            object sender,
            UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                Log.Fatal(ex, "Unhandled AppDomain exception");
            }
        }

        private static void TaskScheduler_UnobservedTaskException(
            object sender,
            UnobservedTaskExceptionEventArgs e)
        {
            Log.Error(e.Exception, "Unobserved task exception");
            e.SetObserved();
        }
    }

    public class SkinRegistration : Component
    {
        public SkinRegistration()
        {
            SkinManager.Default.RegisterAssembly(typeof(NSRetailPOSBlack).Assembly);
        }
    }
}