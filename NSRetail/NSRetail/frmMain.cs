using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSRetail
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnBranch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBranchList obj = new frmBranchList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnCategory_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCategoryList obj = new frmCategoryList();
            obj.ShowInTaskbar = false;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new frmItemList() { MdiParent = this }.Show();
        }

        private void btnUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmUserList obj = new frmUserList();
            obj.ShowInTaskbar = false;
            obj.WindowState = FormWindowState.Maximized;
            obj.IconOptions.ShowIcon = false;
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnChangePassword_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmChangePassword obj = new frmChangePassword();
            obj.ShowInTaskbar = false;
            obj.StartPosition = FormStartPosition.CenterParent;
            obj.IconOptions.ShowIcon = false;
            obj.ShowDialog();
        }

        private void btnLogout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                RegistryKey RGkey = Registry.CurrentUser.OpenSubKey(@"Software\NSRetail", true);
                if (RGkey == null)
                    RGkey = Registry.CurrentUser.CreateSubKey(@"Software\NSRetail");
                RGkey.SetValue("PasswordString", string.Empty);
                RGkey.Close();
                Application.Exit();
            }
            catch (Exception ex) { }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
