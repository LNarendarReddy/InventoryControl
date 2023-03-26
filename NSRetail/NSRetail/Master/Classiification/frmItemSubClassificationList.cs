using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using Entity;
using DataAccess;
using DevExpress.XtraGrid.Views.Grid;
using ErrorManagement;

namespace NSRetail.Master.Classiification
{
    public partial class frmItemSubClassificationList : XtraForm
    {
        ItemClassification itemClassificationObj;
        ReportRepository reportRepository;

        public frmItemSubClassificationList(ItemClassification itemClassification)
        {
            InitializeComponent();
            itemClassificationObj = itemClassification;
        }

        private void BindDatasource()
        {
            gcSubClassifications.DataSource = reportRepository.GetReportData("USP_R_ITEMSUBCLASSIFICATION"
               , new Dictionary<string, object> { { "IncludeDeleted", true } });
            gvSubClassifications.ActiveFilterString = $"CLASSIFICATIONID = {itemClassificationObj.ItemClassificationID}";
        }

        private void frmItemSubClassificationList_Load(object sender, EventArgs e)
        {
            reportRepository = new ReportRepository();
            txtCategory.EditValue = itemClassificationObj.CategoryName;
            txtClassificationName.EditValue = itemClassificationObj.ItemClassificationName;
            BindDatasource();
        }

        private void gvSubClassifications_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            gvSubClassifications.SetRowCellValue(e.RowHandle, gvSubClassifications.Columns["SUBCLASSIFICATIONID"], -1);
            gvSubClassifications.SetRowCellValue(e.RowHandle, gvSubClassifications.Columns["CLASSIFICATIONID"], itemClassificationObj.ItemClassificationID);
        }

        private void gvSubClassifications_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                DataRow row = (e.Row as DataRowView).Row;

                if (string.IsNullOrEmpty(row["SUBCLASSIFICATIONNAME"]?.ToString())) 
                    XtraMessageBox.Show("Sub classification name cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                ItemSubClassification itemSubClassification = new ItemSubClassification()
                {
                    ItemClassificationID = itemClassificationObj.ItemClassificationID,
                    ItemSubClassificationID = row["SUBCLASSIFICATIONID"],
                    ItemSubClassificationName = row["SUBCLASSIFICATIONNAME"],
                    UserID = Utility.UserID
                };

                new MasterRepository().SaveItemSubClassification(itemSubClassification);
                if (itemSubClassification.IsSave)
                {
                    XtraMessageBox.Show("Save successful");
                }
                BindDatasource();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show($"Are you sure you want to delete sub classification - \"{gvSubClassifications.GetFocusedRowCellValue("SUBCLASSIFICATIONNAME")}\""
                , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            ItemSubClassification itemSubClassification = new ItemSubClassification()
            {
                ItemSubClassificationID = gvSubClassifications.GetFocusedRowCellValue("SUBCLASSIFICATIONID")
                , ItemSubClassificationName = gvSubClassifications.GetFocusedRowCellValue("SUBCLASSIFICATIONNAME")
                , UserID = Utility.UserID
            };
            try
            {
                new MasterRepository().DeleteItemSubClassification(itemSubClassification);
                if (itemSubClassification.IsSave)
                {
                    XtraMessageBox.Show($"Item sub-classification - \"{itemSubClassification.ItemSubClassificationName}\" deleted successfully"
                        , "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                BindDatasource();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvSubClassifications_ShownEditor(object sender, EventArgs e)
        {
            BaseEdit edit = (sender as GridView).ActiveEditor;
            edit.KeyDown -= Edit_KeyDown;
            edit.KeyDown += Edit_KeyDown;
        }

        private void Edit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!this.gvSubClassifications.IsLastVisibleRow)
                    this.gvSubClassifications.MoveNext();
                else
                    this.gvSubClassifications.MoveFirst();
            }
        }
    }
}