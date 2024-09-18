using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraReports;
using DevExpress.XtraRichEdit.Import.OpenXml;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using ErrorManagement;
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
    public partial class frmAccessList : XtraForm
    {
        UserRepository userRepository = new UserRepository();
        Dictionary<string, string> editAllowedColumns = new Dictionary<string, string>
        {
            {"View",  "ViewActionID" },
            {"Create", "CreateActionID" },
            {"Update", "UpdateActionID" },
            {"Delete", "DeleteActionID" },
            {"Execute", "ExecuteActionID" }
        };
        
        DataTable dtWorkFlowToDisplay = new DataTable();
        DataTable dtWorkFlowFilling = new DataTable();
        DataTable dtWorkFlowAccessIDs = new DataTable();
        private readonly object roleID;
        private readonly object userID;

        public frmAccessList(object roleID, object userID, string header)
        {
            InitializeComponent();
            this.roleID = roleID;
            this.userID = userID;
            this.Text = header;
        }

        private void frmRoleAccessList_Load(object sender, EventArgs e)
        {
            DataSet dataSet = userRepository.GetRoleAccessList(roleID, userID);
            dtWorkFlowToDisplay = dataSet.Tables[0];
            dtWorkFlowFilling = dataSet.Tables[1];
            dtWorkFlowAccessIDs = dataSet.Tables[2];

            tlRoleAccess.DataSource = dtWorkFlowToDisplay;
            tlRoleAccess.ExpandAll();

            foreach (TreeListNode node in tlRoleAccess.Nodes)
            {
                LoadData(node);
            }
        }

        private void tlRoleAccess_NodeCellStyle(object sender, GetCustomNodeCellStyleEventArgs e)
        {
            if (!editAllowedColumns.ContainsKey(e.Column.FieldName)) return;

            e.Appearance.BackColor = e.Node.GetValue(e.Column.FieldName) == DBNull.Value ? Color.DarkGray : Color.SeaGreen;            
        }

        private void tlRoleAccess_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (!editAllowedColumns.ContainsKey(tlRoleAccess.FocusedColumn.FieldName)) return;

            if (tlRoleAccess.FocusedNode.GetValue(tlRoleAccess.FocusedColumn.FieldName) == DBNull.Value)
            {
                e.Value = DBNull.Value;
                return;
            }

            if ((bool)e.Value)
            {
                AllowParentView(tlRoleAccess.FocusedNode);
            }
            else if (tlRoleAccess.FocusedColumn.FieldName == "View"
                && XtraMessageBox.Show(
                    "Removing view will remove access to all items under current level, are you sure to continue?"
                    , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                RestrictChildAccess(tlRoleAccess.FocusedNode);
            }
        }

        private void AllowParentView(TreeListNode currentNode)
        {
            if (currentNode == null) return;

            if (bool.TryParse(currentNode.GetValue("View").ToString(), out bool hasAccess) && !hasAccess)
                currentNode.SetValue("View", true);

            AllowParentView(currentNode.ParentNode);
        }

        private void RestrictChildAccess(TreeListNode currentNode)
        {
            if (currentNode == null) return;

            foreach (var access in editAllowedColumns) 
            {
                if (!bool.TryParse(currentNode.GetValue(access.Key).ToString(), out bool hasAccess) || !hasAccess) continue;

                currentNode.SetValue(access.Key, false);
            }

            foreach(TreeListNode child in currentNode.Nodes) RestrictChildAccess(child);
        }

        private void LoadData(TreeListNode node)
        {
            if (node == null) return;

            DataView dvWorkFlowFilling = dtWorkFlowFilling.DefaultView;

            foreach (var access in editAllowedColumns)
            {
                object accessID = node.GetValue(access.Value);
                if (string.IsNullOrEmpty(accessID?.ToString())) continue;

                dvWorkFlowFilling.RowFilter = $"{access.Value}={accessID}";
                if (dvWorkFlowFilling.Count == 0) continue;

                node.SetValue(access.Key, true);
            }

            foreach(TreeListNode child in node.Nodes) LoadData(child);
        }

        private void BuildString(TreeListNode node, List<string> workflowActionIDs)
        {
            foreach (var access in editAllowedColumns)
            {
                if (!bool.TryParse(node.GetValue(access.Key).ToString(), out bool hasAccess) || !hasAccess) continue;

                workflowActionIDs.Add(node.GetValue(access.Value).ToString());
            }

            foreach (TreeListNode child in node.Nodes) BuildString(child, workflowActionIDs);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<string> newWorkflowActionIDs = new List<string>();
            List<string> oldWorkflowActionIDs = new List<string>();

            foreach (TreeListNode child in tlRoleAccess.Nodes) BuildString(child, newWorkflowActionIDs);
            foreach (DataRow dataRow in dtWorkFlowAccessIDs.Rows) oldWorkflowActionIDs.Add(dataRow["WORKFLOWACTIONID"].ToString());

            try
            {
                IEnumerable<string> addedStrings = newWorkflowActionIDs.Except(oldWorkflowActionIDs);
                IEnumerable<string> removedStrings = oldWorkflowActionIDs.Except(newWorkflowActionIDs);

                if (!addedStrings.Any() && !removedStrings.Any()) 
                { 
                    XtraMessageBox.Show("No changes found to save, operation is cancelled!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                bool applyToUsers = roleID != null &&
                    XtraMessageBox.Show("Do you want to apply the same changes to all users under the role?"
                        , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes;

                userRepository.SaveAccess(roleID, userID, string.Join(",", addedStrings), string.Join(",", removedStrings), applyToUsers, Utility.UserID);
                XtraMessageBox.Show("Save successful", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex) 
            { 
                ErrorMgmt.ShowError(ex);
            }
        }
    }
}