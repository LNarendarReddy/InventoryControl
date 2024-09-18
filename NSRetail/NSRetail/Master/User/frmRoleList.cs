using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.Master.User
{
    public partial class frmRoleList : DevExpress.XtraEditors.XtraForm
    {
        readonly UserRepository userRepository = new UserRepository();

        public frmRoleList()
        {
            InitializeComponent();
        }

        private void frmRoleList_Load(object sender, EventArgs e)
        {
            gcRoles.DataSource = userRepository.GetRoles();
        }

        private void btnEditAccess_Click(object sender, EventArgs e)
        {
            new frmAccessList(gvRoles.GetFocusedRowCellValue("ROLEID"), null, $"Access permissions for Role : {gvRoles.GetFocusedRowCellValue("ROLENAME")}").ShowDialog();
        }
    }
}