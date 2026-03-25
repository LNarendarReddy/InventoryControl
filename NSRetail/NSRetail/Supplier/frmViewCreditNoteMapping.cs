using DataAccess;
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace NSRetail.Supplier
{
    public partial class frmViewCreditNoteMapping : DevExpress.XtraEditors.XtraForm
    {
        private string _refType;
        public frmViewCreditNoteMapping(DataTable dataTable, string refType)
        {
            InitializeComponent();
            gcCreditNotes.DataSource = dataTable;
            gvCreditNotes.BestFitColumns();
            _refType = refType;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                var view = gcCreditNotes.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
                if (view == null) return;

                int rowHandle = view.FocusedRowHandle;

                if (rowHandle < 0)
                    return;

                object mapId =  gvCreditNotes.GetFocusedRowCellValue("MAPID");

                if (mapId == null || mapId == DBNull.Value)
                {
                    XtraMessageBox.Show("Invalid mapping selected.");
                    return;
                }

                if (XtraMessageBox.Show(
                    "Are you sure you want to delete this credit note mapping?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }

                new CreditNoteRepository().DeleteCreditNoteMapping(mapId, _refType, Utility.UserID);

                XtraMessageBox.Show("Credit note mapping deleted successfully.");

                view.DeleteRow(rowHandle);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    ex.InnerException?.Message ?? ex.Message,
                    "Delete Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}