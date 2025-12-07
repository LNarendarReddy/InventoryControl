using DataAccess;
using DevExpress.Utils.Menu;
using DevExpress.XtraEditors;
using ErrorManagement;
using System;
using System.Data;

namespace NSRetail.Supplier
{
    public partial class frmViewReturnDetail : XtraForm
    {
        public frmViewReturnDetail(DataTable dtItems, object SupplierName, object SupplierReturnsID)
        {
            InitializeComponent();
            this.Text = $"{Text} - {SupplierName} - {SupplierReturnsID}";
            gvSupplierReturns.ViewCaption = $"Return Items : {SupplierName}-{SupplierReturnsID}";
            gcSupplierReturns.DataSource = dtItems;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmViewReturnDetail_Load(object sender, EventArgs e)
        {
            cmbReason.DataSource = new SupplierRepository().GetReason();
            cmbReason.ValueMember = "REASONID";
            cmbReason.DisplayMember = "REASONNAME";

            cmbSupplier.Properties.DataSource = new MasterRepository().GetDealer();
            cmbSupplier.Properties.ValueMember = "DEALERID";
            cmbSupplier.Properties.DisplayMember = "DEALERNAME";
        }

        private void gvSupplierReturns_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            //if (gvSupplierReturns.FocusedRowHandle < 0)
            //    return;
            //e.Menu.Items.Add(new DXMenuItem("Move/Transfer", new EventHandler(OnMove_Click)));
        }

        void OnMove_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSupplier.EditValue == null)
                    throw new Exception("Select supplier to move items");
                int[] selectedrows = gvSupplierReturns.GetSelectedRows();
                DataTable detailsids = new DataTable();
                detailsids.Columns.Add("ID", typeof(int));
                DataRow dr = null;
                for (int i = 0; i < selectedrows.Length; i++)
                {
                    int selectedRowHandle = selectedrows[i];
                    if (selectedRowHandle >= 0)
                    {
                        dr = detailsids.NewRow();
                        dr["ID"] = gvSupplierReturns.GetDataRow(selectedRowHandle)["SUPPLIERRETURNSDETAILID"];
                        detailsids.Rows.Add(dr);
                    }
                }
                if (detailsids.Rows.Count > 0)
                {
                    new SupplierRepository().MoveSupplierReturnsItems(cmbSupplier.EditValue, detailsids, Utility.UserID);
                    gvSupplierReturns.DeleteSelectedRows();
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}