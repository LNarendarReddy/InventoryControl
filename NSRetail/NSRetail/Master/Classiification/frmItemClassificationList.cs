using DataAccess;
using Entity;
using NSRetail.Master.Classiification;
using System;
using System.Collections.Generic;

namespace NSRetail.Master
{
    public partial class frmItemClassificationList : DevExpress.XtraEditors.XtraForm
    {
        ReportRepository reportRepository;

        public frmItemClassificationList()
        {
            InitializeComponent();
        }

        private void BindDataSource()
        {
            gcItemClassification.DataSource = reportRepository.GetReportData("USP_R_ITEMCLASSIFICATION"
                , new Dictionary<string, object> { { "IncludeDeleted", true } });
        }

        private void frmItemClassificationList_Load(object sender, EventArgs e)
        {
            reportRepository = new ReportRepository();
            BindDataSource();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ItemClassification itemClassification = new ItemClassification();
            new frmItemClassification(itemClassification).ShowDialog();
            if(itemClassification.IsSave) BindDataSource();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ItemClassification itemClassification = new ItemClassification()
            {
                ItemClassificationID = gvItemClassification.GetFocusedRowCellValue("CLASSIFICATIONID")
                , CategoryID = gvItemClassification.GetFocusedRowCellValue("CATEGORYID")
                , ItemClassificationName = gvItemClassification.GetFocusedRowCellValue("CLASSIFICATIONNAME")
                , CategoryName = gvItemClassification.GetFocusedRowCellValue("CATEGORYNAME")
            };

            new frmItemClassification(itemClassification).ShowDialog();
            if (itemClassification.IsSave) BindDataSource();
        }

        private void btnViewEditSubClassifications_Click(object sender, EventArgs e)
        {
            ItemClassification itemClassification = new ItemClassification()
            {
                ItemClassificationID = gvItemClassification.GetFocusedRowCellValue("CLASSIFICATIONID")
                , CategoryID = gvItemClassification.GetFocusedRowCellValue("CATEGORYID")
                , ItemClassificationName = gvItemClassification.GetFocusedRowCellValue("CLASSIFICATIONNAME")
                , CategoryName = gvItemClassification.GetFocusedRowCellValue("CATEGORYNAME")
            };
            new frmItemSubClassificationList(itemClassification).ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}