using DataAccess;
using DevExpress.Pdf.Native.BouncyCastle.Asn1.X509.Qualified;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.Utilities
{
    internal class AccessUtility
    {
        internal static HashSet<string> allowedAccess = new HashSet<string>();

        internal static void FetchAndBuildAccessInfo()
        {
            if (frmMain.skipAccessRefresh) return;

            BuildAccessInfo(new ReportRepository().GetReportData("USP_R_USERACCESSINFO", new Dictionary<string, object> { { "UserID", Utility.UserID } }));
        }

        internal static void BuildAccessInfo(DataTable dtAllowedAccess)
        {
            if (dtAllowedAccess == null) return;

            allowedAccess = new HashSet<string>();

            foreach (DataRow dataRow in dtAllowedAccess.Rows)
            {
                allowedAccess.Add(dataRow[0].ToString().ToLower());
            }
        }

        internal static void SetStatusByAccess(params Control[] controls)
        {
            foreach(Control control in controls)
            {
                bool hasAccess = HasAccess(control.Tag?.ToString());
                
                if (control.GetType() == typeof(SimpleButton))
                {
                    control.Enabled = control.Enabled && hasAccess;
                    continue;
                }
            }
        }

        internal static void SetStatusByAccess(RibbonControl ribbonControl)
        {
            foreach (RibbonPage page in ribbonControl.Pages)
            {
                page.Visible = HasAccess(page.Tag?.ToString());

                if(!page.Visible) continue;

                foreach (RibbonPageGroup group in page.Groups)
                {
                    foreach (var itemLink in group.ItemLinks)
                    {
                        if (itemLink.GetType() == typeof(BarButtonItemLink))
                        {
                            BarButtonItemLink link = (BarButtonItemLink)itemLink;
                            link.Visible = HasAccess(link.Item.Tag?.ToString());
                        }
                    }

                    group.Visible = group.ItemLinks.Any(x => x.Visible);
                }

                page.Visible = page.Groups.Any(x => x.Visible);
            }
        }

        internal static void SetStatusByAccess(params GridColumn[] gridColumns)
        {
            foreach (GridColumn gridColumn in gridColumns)
            {
                bool hasAccess = HasAccess(gridColumn.Tag?.ToString());
                gridColumn.OptionsColumn.AllowEdit = hasAccess;
                gridColumn.OptionsColumn.ReadOnly = hasAccess;
            }
        }

        internal static void SetStatusByAccess(params GridView[] gridViews)
        {
            foreach (GridView gridView in gridViews)
            {
                bool hasAccess = HasAccess(gridView.Tag?.ToString());

                if (gridView.Tag != null && gridView.Tag.ToString().Contains("::Create"))
                    gridView.OptionsView.NewItemRowPosition = hasAccess ? gridView.OptionsView.NewItemRowPosition : NewItemRowPosition.None;
                else if (gridView.Tag != null && gridView.Tag.ToString().Contains("::Update"))
                {
                    foreach (GridColumn gridColumn in gridView.Columns)
                    {
                        if(!gridColumn.Visible || !gridColumn.OptionsColumn.AllowEdit 
                            || !string.IsNullOrEmpty(gridColumn.Tag?.ToString())) // if it doesn't have specific permission, use parent permission
                            continue;

                        gridColumn.OptionsColumn.AllowEdit = HasAccess(gridView.Tag?.ToString());
                        gridColumn.OptionsColumn.ReadOnly = HasAccess(gridView.Tag?.ToString());
                    }
                }
            }
        }

        internal static void SetStatusByAccess(params BarButtonItem[] barButtonItems)
        {
            foreach (BarButtonItem barButtonItem in barButtonItems)
            {
                bool hasAccess = HasAccess(barButtonItem.Tag?.ToString());
                barButtonItem.Enabled = hasAccess;
            }
        }

        internal static bool HasAccess(string accessKey)
        {
            if(string.IsNullOrEmpty(accessKey)) return true;

            if (!accessKey.Contains("::"))
            {
                accessKey = $"{accessKey}::View";
            }

            accessKey = accessKey.ToLower();

            return allowedAccess.Contains(accessKey);
        }
    }
}
