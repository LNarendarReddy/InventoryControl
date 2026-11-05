using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace NSRetail.Utilities
{
    /// <summary>
    /// Application-wide lookup sizing, registered only by the warehouse executable.
    /// </summary>
    internal static class LookupColumnBestFit
    {
        private static readonly ConditionalWeakTable<Control, object> observedControls =
            new ConditionalWeakTable<Control, object>();
        private static bool registered;

        internal static void Register()
        {
            if (registered) return;

            registered = true;
            Application.Idle += ObserveOpenForms;
        }

        private static void ObserveOpenForms(object sender, EventArgs e)
        {
            // Only new forms are traversed. ControlAdded handles later additions,
            // including report criteria controls and dynamically created editors.
            foreach (Form form in Application.OpenForms)
                ObserveControl(form);
        }

        private static void ObserveControl(Control control)
        {
            object marker;
            if (control.IsDisposed || observedControls.TryGetValue(control, out marker))
                return;

            observedControls.Add(control, new object());
            control.ControlAdded += ControlAdded;

            var lookup = control as LookUpEdit;
            if (lookup != null)
            {
                lookup.QueryPopUp += LookupQueryPopUp;
                lookup.Popup += LookupPopup;
            }

            var searchLookup = control as SearchLookUpEdit;
            if (searchLookup != null)
            {
                searchLookup.QueryPopUp += SearchLookupQueryPopUp;
                searchLookup.Popup += SearchLookupPopup;
            }

            var grid = control as GridControl;
            if (grid != null)
            {
                foreach (BaseView view in grid.ViewCollection)
                    ObserveView(view);
                grid.ViewRegistered += GridViewRegistered;
            }

            foreach (Control child in control.Controls)
                ObserveControl(child);
        }

        private static void ControlAdded(object sender, ControlEventArgs e)
        {
            ObserveControl(e.Control);
        }

        private static void ObserveView(BaseView view)
        {
            var columnView = view as ColumnView;
            if (columnView != null)
                columnView.ShownEditor += GridShownEditor;
        }

        private static void GridViewRegistered(object sender, ViewOperationEventArgs e)
        {
            ObserveView(e.View);
        }

        private static void GridShownEditor(object sender, EventArgs e)
        {
            var editor = ((ColumnView)sender).ActiveEditor;
            if (editor != null)
                ObserveControl(editor);
        }

        private static void LookupQueryPopUp(object sender, CancelEventArgs e)
        {
            var properties = ((LookUpEdit)sender).Properties;
            properties.BestFitMode = BestFitMode.BestFitResizePopup;
            if (properties.Columns.Count > 0)
                properties.PopupWidth = properties.BestFit();
        }

        private static void LookupPopup(object sender, EventArgs e)
        {
            // Also handles columns generated automatically on the first opening.
            ((LookUpEdit)sender).Properties.BestFit();
        }

        private static void SearchLookupQueryPopUp(object sender, CancelEventArgs e)
        {
            var properties = ((SearchLookUpEdit)sender).Properties;
            properties.BestFitMode = BestFitMode.BestFitResizePopup;
            var view = properties.PopupView as GridView;
            if (view != null)
                view.OptionsView.ColumnAutoWidth = false;
        }

        private static void SearchLookupPopup(object sender, EventArgs e)
        {
            // The popup must be visible before measuring its grid columns.
            var view = ((SearchLookUpEdit)sender).Properties.PopupView as GridView;
            if (view != null)
            {
                view.OptionsView.ColumnAutoWidth = false;
                view.BestFitColumns();
            }
        }
    }
}
