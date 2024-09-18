using DataAccess;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using Entity;
using NSRetail.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace NSRetail
{
    public static class Utility
    {
        private static DataTable dtGST;
        private static DataTable dtItemSKUList;
        private static DataTable dtItemCodeList;
        private static DataTable dtItemCodeFiltered;
        private static DataTable dtNonEAN;
        private static DataTable dtParentItemList;
        private static DataTable dtReturnStatus;
        private static DataTable dtBranchList;
        private static DataTable dtBranchListIncludingDeleted;
        private static DataTable dtCatgeory;
        private static DataTable dtSubCatgeory;

        private static List<GSTInfo> gstInfoList;
        public static DataTable dtConnectionInfo;

        public static int UserID = 4;
        public static string UserName = string.Empty;
        public static string Password = string.Empty;
        public static string FullName = string.Empty;
        public static string Role = string.Empty;
        public static string Category = string.Empty;
        public static string Branch = string.Empty;
        public static string ReportingLead = string.Empty;
        public static int RoleID = 0;
        public static int ReportingLeadID = 0;
        public static int CategoryID = 0;
        public static bool IsOpenCategory = false;
        public static int BranchID = 0;
        public static string Email = string.Empty;
        public static string DotMatrixPrinter = string.Empty;
        public static string BarcodePrinter = string.Empty;
        public static string A4SizePrinter = string.Empty;
        public static string ThermalPrinter = string.Empty;
        public static string AppVersion = "2.6.4";
        public static string VersionDate = "(18-09-2024)";

        public static void Setfocus(GridView view, string ColumnName, object Value)
        {
            try
            {
                int rowHandle = view.LocateByValue(ColumnName, Value);
                if (rowHandle != GridControl.InvalidRowHandle)
                    view.FocusedRowHandle = rowHandle;
            }
            catch (Exception ex){}
        }
        private static  byte[] Encrypt(byte[] input)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes("NSoftSol", new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 });
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
        private static byte[] Decrypt(byte[] input)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes("NSoftSol", new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 });
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
        public static string Encrypt(string input)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(input)));
        }
        public static string Decrypt(string input)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(input)));
        }
        private static void FillItemBaseline()
        {
            DataSet dsItemBaseline = new ItemCodeRepository().GetItemCodes(CategoryID);
            dtItemSKUList = dsItemBaseline.Tables["ITEMS"];
            dtItemCodeList = dsItemBaseline.Tables["ITEMCODES"];
            dtNonEAN = dsItemBaseline.Tables["ITEMCODES"];
            dtItemCodeFiltered = dsItemBaseline.Tables["ITEMCODES"];

            //dtNonEAN = dsItemBaseline.Tables["NONEAN"];
            //if (dsItemBaseline.Tables.Count > 3)
            //    dtItemCodeFiltered = dsItemBaseline.Tables["ITEMCODESFILTERED"];

        }

        private static void FillParentItemBaseline()
        {
            dtParentItemList = new ItemCodeRepository().GetParentItems(CategoryID);
        }

        private static void FillGSTBaseLine()
        {
            dtGST = new MasterRepository().GetGST();
            gstInfoList = new List<GSTInfo>();
            foreach(DataRow dr in dtGST.Rows)
            {
                GSTInfo gstInfo = new GSTInfo();
                gstInfo.UpdateGST(dr);
                gstInfoList.Add(gstInfo);
            }
        }

        public static DataTable GetGSTBaseline()
        {
            if(dtGST == null)
            {
                FillGSTBaseLine();
            }

            return dtGST;
        }

        public static DataTable GetItemSKUList()
        {
            if(dtItemSKUList == null)
            {
                FillItemBaseline();
            }

            return dtItemSKUList;
        }

        public static DataTable GetItemCodeListFiltered()
        {
            if (dtItemCodeList == null)
            {
                FillItemBaseline();
            }
            return Category ==  "All" ? dtItemCodeList : dtItemCodeFiltered;
        }

        public static DataTable GetItemCodeList()
        {
            if (dtItemCodeList == null)
            {
                FillItemBaseline();
            }
            return dtItemCodeList;
        }

        public static DataTable GetParentItemList()
        {
            if(dtParentItemList == null)
            {
                FillParentItemBaseline();
            }

            return dtParentItemList;
        }
        
        public static DataTable GetNonEAN()
        {
            if (dtNonEAN == null)
            {
                FillItemBaseline();
            }

            return dtNonEAN;
        }

        public static DataTable GetBranchList(bool IncludingDeleted = false)
        {
            dtBranchList = dtBranchList ?? new MasterRepository().GetBranch();
            dtBranchListIncludingDeleted = dtBranchListIncludingDeleted ?? new MasterRepository().GetBranch(false, true);

            return IncludingDeleted ? dtBranchListIncludingDeleted : dtBranchList;
        }

        public static DataTable GetCategoryList()
        {
            return dtCatgeory = dtCatgeory ?? new MasterRepository().GetCategory();
        }

        public static IEnumerable<GSTInfo> GetGSTInfoList()
        {
            if(dtGST == null)
            {
                FillGSTBaseLine();
            }

            return gstInfoList;
        }

        public static void FillBaseLine()
        {
            dtGST = null;
            dtBranchList = null;
            dtCatgeory = null;
            dtSubCatgeory = null;
            dtBranchListIncludingDeleted = null;

            FillItemBaseline();
            GetGSTBaseline();
            FillParentItemBaseline();
        }

        public static void PrintBarCode(object ItemCode, object ItemName, 
            string SalePrice, object oQuantity,object MRP,object BatchNumber,
            object PackedDate, object CategoryID)
        {
            try
            {
                int Quantity = 0;
                if (int.TryParse(Convert.ToString(oQuantity), out Quantity))
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ItemCode", typeof(string));
                    dt.Columns.Add("ItemName", typeof(string));
                    dt.Columns.Add("SalePrice", typeof(string));
                    dt.Columns.Add("MRP", typeof(string));
                    DataRow dr = null;
                    for (int i = 0; i < Quantity; i++)
                    {
                        dr = dt.NewRow();
                        dr["ItemCode"] = ItemCode;
                        dr["ItemName"] = ItemName;
                        dr["SalePrice"] = SalePrice;
                        dr["MRP"] = MRP;
                        dt.Rows.Add(dr);
                    }
                    rptBarcode rpt = new rptBarcode();
                    rpt.DataSource = dt;
                    rpt.ShowPrintMarginsWarning = false;
                    rpt.Parameters["BatchNumber"].Value = BatchNumber;
                    rpt.Parameters["PackedDate"].Value = DateTime.Now.ToString("MM/yyyy");
                    rpt.Parameters["CategoryID"].Value = CategoryID;
                    rpt.Parameters["IsOpenCategory"].Value = IsOpenCategory;
                    rpt.CreateDocument();
                    //rpt.ShowRibbonPreview();
                    ReportPrintTool printTool = new ReportPrintTool(rpt);
                    printTool.Print(Utility.BarcodePrinter);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable AppliesTo()
        {
            return new OfferRepository().GetApliesTo();
        }
        public static List<int> LocateAllRowsByValue(this GridView view, string fieldName, object value)
        {
            List<int> list = new List<int>();
            int rowHandle = view.LocateByValue(fieldName, value);
            while (rowHandle != GridControl.InvalidRowHandle)
            {
                list.Add(rowHandle);
                rowHandle = view.LocateByValue(rowHandle + 1, view.Columns[fieldName], value);
            }
            return list;
        }

        public static DataTable ImportExcelXLS(string FilePath, bool hasHeaders = true)
        {
            DataTable dt = new DataTable();
            try
            {
                string HDR = hasHeaders ? "Yes" : "No";
                string strConn;
                if (FilePath.Substring(FilePath.LastIndexOf('.')).ToLower() == ".xlsx")
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                else
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";

                DataSet output = new DataSet();
                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();
                    DataTable schemaTable = conn.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                    foreach (DataRow schemaRow in schemaTable.Rows)
                    {
                        string sheet = schemaRow["TABLE_NAME"].ToString();

                        if (!sheet.EndsWith("_"))
                        {
                            try
                            {
                                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                                cmd.CommandType = CommandType.Text;

                                DataTable outputTable = new DataTable(sheet);
                                output.Tables.Add(outputTable);
                                new OleDbDataAdapter(cmd).Fill(outputTable);
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message + string.Format("Sheet:{0}.File:F{1}", sheet, FilePath), ex);
                            }
                        }
                    }
                }
                if (output != null && output.Tables.Count > 0)
                    dt = output.Tables[0].Copy();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public static void ConfirmBarCodeScan(this BaseEdit baseEdit)
        {
            baseEdit.PreviewKeyDown += BaseEdit_PreviewKeyDown;
        }

        private static void BaseEdit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            BaseEdit baseEdit = sender as BaseEdit;
            string textValue = baseEdit.EditValue != null ? baseEdit.EditValue.ToString() : string.Empty;
            if (textValue.Length > 4 && XtraMessageBox.Show($"Barcode scan detected, Do you want to accept the value {textValue}"
                    , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, defaultButton: MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                baseEdit.EditValue = null;
            }
        }

        public static DataTable Returnstatus()
        {
            if (dtReturnStatus != null)
            {
                return dtReturnStatus;
            }

            dtReturnStatus = new DataTable();
            dtReturnStatus.Columns.Add("RETURNSTATUSID", typeof(int));
            dtReturnStatus.Columns.Add("RETURNSTATUSNAME", typeof(string));

            dtReturnStatus.Rows.Add(1, "Accepted");
            dtReturnStatus.Rows.Add(2, "Rejected");
        
            return dtReturnStatus;
        }
    }
}