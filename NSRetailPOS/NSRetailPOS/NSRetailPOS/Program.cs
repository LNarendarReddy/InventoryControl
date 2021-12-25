using DevExpress.LookAndFeel;
using NSRetailPOS.Data;
using NSRetailPOS.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
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
}
