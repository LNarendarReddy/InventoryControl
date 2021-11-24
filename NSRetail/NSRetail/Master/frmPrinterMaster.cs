using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DataAccess;
using Entity;
using ErrorManagement;

namespace NSRetail
{
    public partial class frmPrinterMaster : DevExpress.XtraEditors.XtraForm
    {
        PrinterSettings ObjPrinterSettings = new PrinterSettings();
        MasterRepository objMasterRep = new MasterRepository();
        public frmPrinterMaster()
        {
            InitializeComponent();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSAve_Click(object sender, EventArgs e)
        {
            try
            {
                if (!dxValidationProvider1.Validate())
                    return;
                ObjPrinterSettings.UserID = Utility.UserID;
                ObjPrinterSettings.PRINTERTYPEID = cmbPrinterType.EditValue;
                ObjPrinterSettings.PRINTERNAME =  cmbPrinterName.Text;
                objMasterRep.SavePrinterSettings(ObjPrinterSettings);
                gcPrinters.DataSource = objMasterRep.GetPrinterSettings(Utility.UserID);
                Utility.Setfocus(gvPrinters, "PRINTERSETTINGSID", Convert.ToInt32(ObjPrinterSettings.PRINTERSETTINGSID));
                switch (cmbPrinterType.Text.ToUpper())
                {
                    case "DOT MATRIX PRINTER":
                        Utility.DotMatrixPrinter = cmbPrinterName.Text;
                        break;
                    case "BARCODE PRINTER":
                        Utility.BarcodePrinter = cmbPrinterName.Text;
                        break;
                    case "A4 SIZE PRINTER":
                        Utility.A4SizePrinter = cmbPrinterName.Text;
                        break;
                    case "THERMAL PRINTER":
                        Utility.ThermalPrinter = cmbPrinterName.Text;
                        break;
                    default:
                        break;
                }
                ClearFields();
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
        private void frmPrinterMaster_Load(object sender, EventArgs e)
        {
            txtUserName.Text = Utility.UserName;
            cmbPrinterType.Properties.DataSource = objMasterRep.GetPrinterType();
            cmbPrinterType.Properties.DisplayMember  = "PRINTERTYPENAME";
            cmbPrinterType.Properties.ValueMember = "PRINTERTYPEID";
            btnDetect_Click(null, null);
            ObjPrinterSettings.UserID = Utility.UserID;
            gcPrinters.DataSource = objMasterRep.GetPrinterSettings(Utility.UserID);
        }
        private void ClearFields()
        {
            cmbPrinterType.EditValue = null;
            cmbPrinterName.EditValue = null;
            ObjPrinterSettings.PRINTERSETTINGSID = -1;
            cmbPrinterType.Focus();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int ivalue = 0;
               if(gvPrinters.FocusedRowHandle >= 0 
                    && int.TryParse(Convert.ToString(gvPrinters.GetFocusedRowCellValue("PRINTERSETTINGSID")),out ivalue))
                {
                    ObjPrinterSettings.PRINTERSETTINGSID = ivalue;
                    cmbPrinterType.EditValue = gvPrinters.GetFocusedRowCellValue("PRINTERTYPEID");
                    cmbPrinterName.EditValue = gvPrinters.GetFocusedRowCellValue("PRINTERNAME");
                }
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
        private void btnDetect_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> l = new List<string>();
                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    l.Add(printer);
                }
                cmbPrinterName.Properties.DataSource = l;
            }
            catch (Exception ex)
            {
                ErrorMgmt.ShowError(ex);
                ErrorMgmt.Errorlog.Error(ex);
            }
        }
    }
}