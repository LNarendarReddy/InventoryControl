using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using NSRetailPOS.Data;
using NSRetailPOS.Entity;
using NSRetailPOS.Reports;
using NSRetailPOS.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Printing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace NSRetailPOS
{
    public static class Utility
    {
        public static LoginInfo loginInfo = new LoginInfo();
        public static BranchInfo branchInfo = new BranchInfo();
        static SerialPort _serialPort = new SerialPort();

        public static event EventHandler ItemOrCodeChanged;
        public static Form ActiveForm;
        public static Bill GetBill(DataSet dsBillDetails)
        {
            Bill billObj = new Bill();
            billObj.BillID = dsBillDetails.Tables["BILL"].Rows[0]["BILLID"];
            billObj.BillNumber = dsBillDetails.Tables["BILL"].Rows[0]["BILLNUMBER"];
            billObj.LastBilledAmount = dsBillDetails.Tables["BILL"].Rows[0]["LASTBILLEDAMOUNT"];
            billObj.LastBilledQuantity = dsBillDetails.Tables["BILL"].Rows[0]["LASTBILLEDQUANTITY"];
            billObj.Rounding = dsBillDetails.Tables["BILL"].Rows[0]["ROUNDING"];
            billObj.LastBillID = dsBillDetails.Tables["BILL"].Rows[0]["LASTBILLID"];
            billObj.CustomerName = dsBillDetails.Tables["BILL"].Rows[0]["CUSTOMERNAME"];
            billObj.CustomerNumber = dsBillDetails.Tables["BILL"].Rows[0]["CUSTOMERNUMBER"];
            billObj.CustomerGST = dsBillDetails.Tables["BILL"].Rows[0]["CUSTOMERGST"];
            billObj.TenderedCash = dsBillDetails.Tables["BILL"].Rows[0]["TENDEREDCASH"];
            billObj.TenderedChange = dsBillDetails.Tables["BILL"].Rows[0]["TENDEREDCHANGE"];
            billObj.IsDoorDelivery = dsBillDetails.Tables["BILL"].Rows[0]["ISDOORDELIVERY"];
            billObj.dtBillDetails = dsBillDetails.Tables["BILLDETAILS"];
            if (dsBillDetails.Tables.Count > 2)
                billObj.dtMopValues = dsBillDetails.Tables["MOPDETAILS"];
            return billObj;
        }

        public delegate void NextPrimeDelegate();
        public static bool StartSync(BackgroundWorker backgroundWorker, bool forceFullSync = false)
        {
            try
            {
                LoggerUtility.InitializeLogger();
                DateTime syncStartTime = DateTime.Now.AddMinutes(-5);
                //LoggerUtility.Logger.Info($"POS sync started at {syncStartTime.ToLongTimeString()}");
                backgroundWorker?.ReportProgress(0, $"POS sync started at {syncStartTime.ToLongTimeString()}");
                SyncRepository syncRepository = new SyncRepository();
                CloudRepository cloudRepository = new CloudRepository();

                if (!Utility.ValidateTimeZone())
                {
                    ShowUIMessage($"This system installed in different time zone!" +
                        $"{Environment.NewLine}Please correct the timezone to continue or contact your administrator.", "Time error");
                    return false;
                 }

                if (!DBVersionCheck(backgroundWorker, cloudRepository, syncRepository))
                    return false;

                DataTable dtEntity = cloudRepository.GetEntityData(branchInfo.BranchCounterID, "FromCloud");
                foreach (DataRow entityRow in dtEntity.Rows)
                {
                    string entityName = entityRow["ENTITYNAME"].ToString();
                    //LoggerUtility.Logger.Info($"{entityName} down sync started");

                    ReportText(backgroundWorker, $"{entityName} down sync started");
                    DataTable dtEntityWiseData = cloudRepository.GetEntityWiseData(
                        entityName,
                        forceFullSync ? "01-01-1900" : entityRow["SYNCDATE"]
                        , branchInfo.BranchID);
                    ReportText(backgroundWorker, $"Found {dtEntityWiseData.Rows.Count} records to down sync in entity : {entityName} ");
                    if (dtEntityWiseData?.Rows.Count > 0)
                    {
                        syncRepository.SaveData(entityName, dtEntityWiseData);
                        cloudRepository.UpdateEntitySyncStatus(entityRow["ENTITYSYNCSTATUSID"], syncStartTime);

                        if (entityName == "ITEM" || entityName == "ITEMCODE")
                        {
                            ItemOrCodeChanged?.Invoke(null, null);
                        }
                    }
                    ReportText(backgroundWorker, $"{entityName} down sync completed");
                }

                // start up sync
                dtEntity = cloudRepository.GetEntityData(branchInfo.BranchCounterID, "ToCloud");
                foreach (DataRow entityRow in dtEntity.Rows)
                {
                    string entityName = entityRow["ENTITYNAME"].ToString();
                    ReportText(backgroundWorker, $"{entityName} up sync started");
                    DataTable dtEntityWiseData = syncRepository.GetEntityWiseData(entityName, entityRow["SYNCDATE"]);
                    ReportText(backgroundWorker, $"Found {dtEntityWiseData.Rows.Count} records to up sync in entity : {entityName} ");
                    if (dtEntityWiseData?.Rows.Count > 0)
                    {
                        cloudRepository.SaveData(entityName, dtEntityWiseData);
                        cloudRepository.UpdateEntitySyncStatus(entityRow["ENTITYSYNCSTATUSID"], syncStartTime);
                    }
                    ReportText(backgroundWorker, $"{entityName} up sync completed");
                }

                // clear old data
                ReportText(backgroundWorker, $"clearing one month old data");
                syncRepository.ClearOldData();

                if(!AppVersionCheck(backgroundWorker, cloudRepository, syncRepository))
                    return false;

                //LoggerUtility.Logger.Info($"POS sync completed");
                backgroundWorker?.ReportProgress(0, $"POS sync completed at {DateTime.Now.ToLongTimeString()}");
            }
            catch (Exception ex)
            {
                ShowUIMessage($"Error while running sync : {ex.Message} {Environment.NewLine} {ex.StackTrace}", "Error");
            }
            return true;
        }
        private static byte[] Encrypt(byte[] input)
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
        public static void ReportText(BackgroundWorker bgwSyncWorker, string text)
        {
            string displayText = DateTime.Now.ToString() + " : " + text;
            bgwSyncWorker?.ReportProgress(0, displayText);
            if (bgwSyncWorker == null)
            {
                SplashScreenManager.Default.SetWaitFormDescription(displayText);
            }
        }
        public static string GetHDDSerialNumber()
        {
            ManagementObjectSearcher moSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            foreach (ManagementObject wmi_HD in moSearcher.Get())
            {
                object serialNo = wmi_HD["SerialNumber"];
                if (!string.IsNullOrEmpty(serialNo as string)) { return serialNo.ToString(); }
            }

            return string.Empty;
        }
        public static void PrintBarCode(object ItemCode, object ItemName,
            string SalePrice, object oQuantity, object MRP, object BatchNumber,
            object PackedDate, object CategoryID, object IsOpenCategory)
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

                    List<string> l = new List<string>();
                    foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                    {
                        if (printer.StartsWith("TSC"))
                            l.Add(printer);
                    }

                    var server = new LocalPrintServer();
                    foreach (string printer in l)
                    {
                        ReportPrintTool printTool = new ReportPrintTool(rpt);
                        printTool.Print(printer);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        public static void SetGridFormatting(GridView gridView)
        {
            gridView.Appearance.FocusedCell.BackColor = Color.DarkGreen;
            gridView.Appearance.FocusedCell.Font = new Font("Arial", 11F, FontStyle.Bold);
            gridView.Appearance.FocusedCell.ForeColor = Color.White;
            gridView.Appearance.FocusedCell.Options.UseBackColor = true;
            gridView.Appearance.FocusedCell.Options.UseFont = true;
            gridView.Appearance.FocusedCell.Options.UseForeColor = true;

            gridView.Appearance.FocusedRow.BackColor = Color.GhostWhite;
            gridView.Appearance.FocusedRow.Font = new Font("Arial", 11F, FontStyle.Bold);
            gridView.Appearance.FocusedRow.ForeColor = Color.Black;
            gridView.Appearance.FocusedRow.Options.UseForeColor = true;
            gridView.Appearance.FocusedRow.Options.UseFont = true;

            gridView.Appearance.FooterPanel.Font = new Font("Arial", 14F, FontStyle.Bold);
            gridView.Appearance.FooterPanel.Options.UseFont = true;
            gridView.Appearance.HeaderPanel.Font = new Font("Arial", 10F, FontStyle.Bold);
            gridView.Appearance.HeaderPanel.Options.UseFont = true;

            gridView.Appearance.Row.Font = new Font("Arial", 10F, FontStyle.Bold);
            gridView.Appearance.Row.Options.UseFont = true;

            gridView.RowStyle += GridView_RowStyle;
        }

        private static void GridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view.IsRowSelected(e.RowHandle) || view.FocusedRowHandle == e.RowHandle)
            {
                e.Appearance.BackColor = Color.GhostWhite;
                e.Appearance.Font = new Font("Arial", 11F, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.Options.UseForeColor = true;
                e.Appearance.Options.UseFont = true;
                e.HighPriority = true;
            }
        }

        public static void ListenSerialPort()
        {
            ////_serialPort = new SerialPort(ConfigurationManager.AppSettings["PortName"].ToString(), 9600, Parity.None, 8, StopBits.One);
            //_serialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            //_serialPort.Handshake = Handshake.None;
            //_serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            //_serialPort.ReadTimeout = 500;
            //_serialPort.WriteTimeout = 500;
            //_serialPort.Open();
        }

        static void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _serialPort.ReadExisting();
            if (!(Form.ActiveForm is IBarcodeReceiver)) return;

            data = data.EndsWith(Environment.NewLine) ? data.Replace(Environment.NewLine, string.Empty) : data;
            Form.ActiveForm.BeginInvoke((Action)(() => (Form.ActiveForm as IBarcodeReceiver).ReceiveBarCode(data)));
        }

        public static string AppVersion = "1.3.7";
        public static string DBVersion = string.Empty;
        public static string VersionDate = "(12-09-2023)";

        private static bool DBVersionCheck(BackgroundWorker backgroundWorker, CloudRepository cloudRepository, SyncRepository syncRepository)
        {
            bool _isContinue = true;
            Tuple<string, string> posVersion = cloudRepository.GetPOSVersion();
            Utility.DBVersion = Utility.DBVersion == string.Empty ? syncRepository.GetDBVersion() : Utility.DBVersion;
            if (!posVersion.Item2.Equals(Utility.DBVersion))
            {
                if (backgroundWorker == null)
                    SplashScreenManager.CloseForm();
                XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                args.AutoCloseOptions.Delay = 5000;
                args.Caption = "Database Update";
                args.Text = $"New DB update available!{Environment.NewLine}Please wait till the DB is updated.";
                args.DefaultButtonIndex = 0;
                args.AutoCloseOptions.ShowTimerOnDefaultButton = true;
                args.Buttons = new DialogResult[] { DialogResult.OK };
                ShowUIMessage(args);
                if (backgroundWorker == null)
                    SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
                ReportText(backgroundWorker, $"Update download started");
                bool _updateAvailable = GoogleDriveRepository.DownloadFile("DBFiles", backgroundWorker);
                ReportText(backgroundWorker, $"Update download completed");
                if (_updateAvailable)
                {
                    string stpath = Path.Combine(Application.UserAppDataPath, "DBFiles", "RunSQL.bat");
                    ProcessStartInfo processInfo;
                    Process process;

                    processInfo = new ProcessStartInfo(stpath);
                    processInfo.UseShellExecute = false;
                    processInfo.WorkingDirectory = Path.Combine(Application.UserAppDataPath, "DBFiles");
                    process = Process.Start(processInfo);
                    process.WaitForExit();

                    Utility.DBVersion = syncRepository.GetDBVersion();
                    if (!posVersion.Item2.Equals(Utility.DBVersion))
                    {
                        ShowUIMessage($"Database version mismatch!{Environment.NewLine}Please contact your administrator.",
                        "Database Error");
                        _isContinue = false;
                    }

                    if (backgroundWorker == null)
                    {
                        SplashScreenManager.CloseForm();
                        SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
                    }
                }
                else
                {
                    ShowUIMessage(
                        $"Error occured while updating database!{Environment.NewLine}Please contact your administrator.",
                        "Database Error");
                    _isContinue = false;
                }
            }
            return _isContinue;
        }
        private static bool AppVersionCheck(BackgroundWorker backgroundWorker, CloudRepository 
            cloudRepository, SyncRepository syncRepository)
        {
            Tuple<string, string> posVersion = cloudRepository.GetPOSVersion();
            if (!posVersion.Item1.Equals(Utility.AppVersion))
            {
                if (backgroundWorker == null)
                    SplashScreenManager.CloseForm();
                XtraMessageBoxArgs args = new XtraMessageBoxArgs();
                args.AutoCloseOptions.Delay = 5000;
                args.Caption = "Application Update";
                args.Text = $"New Application update available!{Environment.NewLine}Please wait till the application is updated." +
                    $"{Environment.NewLine}Application will be re-launched automatically";
                args.DefaultButtonIndex = 0;
                args.AutoCloseOptions.ShowTimerOnDefaultButton = true;
                args.Buttons = new DialogResult[] { DialogResult.OK };
                ShowUIMessage(args);

                if (backgroundWorker == null)
                    SplashScreenManager.ShowForm(null, typeof(frmWaitForm), true, true, false);
                ReportText(backgroundWorker, $"Update download started");
                bool _updateAvailable = GoogleDriveRepository.DownloadFile("AppFiles", backgroundWorker);
                ReportText(backgroundWorker, $"Update download completed");
                if (_updateAvailable)
                {

                    string stpath = Path.Combine(Application.UserAppDataPath, "AppFiles", "UpdateEXEScript.bat");
                    ProcessStartInfo processInfo;
                    Process process;
                    processInfo = new ProcessStartInfo(stpath);
                    process = Process.Start(processInfo);
                }
                else
                {
                    ShowUIMessage($"Error occured while updating application.{Environment.NewLine}Please contact your administrator", "Application Error");
                    return false;
                }
            }
            return true;
        }

        private static void ShowUIMessage(XtraMessageBoxArgs args)
        {
            if (ActiveForm.InvokeRequired)
            {
                ActiveForm.BeginInvoke((Action<XtraMessageBoxArgs>)ShowUIMessage, args);
                return;
            }

            XtraMessageBox.Show(args);
        }

        private static void ShowUIMessage(string text, string caption)
        {
            if (ActiveForm.InvokeRequired)
            {
                ActiveForm.BeginInvoke((Action<string, string>)ShowUIMessage, text, caption);
                return;
            }

            XtraMessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool ValidateTimeZone()
        {
            bool _return = false;
            TimeZone localZone = TimeZone.CurrentTimeZone;
            _return = localZone.StandardName == "India Standard Time";
            object obj = new CloudRepository().GetTimeZone();
            DateTime localdt = DateTime.Now;
            if (_return && DateTime.TryParse(Convert.ToString(obj), out DateTime clouddt) &&
                clouddt.Date == localdt.Date && clouddt.Hour == localdt.Hour)
            {
                _return = true;
            }
            return _return;
        }

        public static void ConfirmLargeNumber(this BaseEdit baseEdit)
        {
            baseEdit.PreviewKeyDown += BaseEdit_PreviewKeyDown;
        }

        private static void BaseEdit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            BaseEdit baseEdit = sender as BaseEdit;
            string textValue = baseEdit.EditValue != null ? baseEdit.EditValue.ToString() : string.Empty;
            if (textValue.Length > 4 && XtraMessageBox.Show($"Large number detected, possible barcode scan detected, Do you want to accept the value {textValue}"
                    , "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                baseEdit.EditValue = null;
            }
        }

        public static string WriteToPort(string message)
        {
            //try
            //{
            //    TcpClient client = new TcpClient("127.0.0.1", 4804);

            //    // Translate the passed message into ASCII and store it as a Byte array.
            //    Byte[] data = Encoding.ASCII.GetBytes(message);

            //    // Get a client stream for reading and writing.
            //    //  Stream stream = client.GetStream();

            //    NetworkStream stream = client.GetStream();

            //    // Send the message to the connected TcpServer. 
            //    stream.Write(data, 0, data.Length);

            //    // Close everything.
            //    stream.Close();
            //    client.Close();
            //}
            //catch (Exception ex)
            //{
            //    return $"Error publishing to camera port: {ex.Message}";
            //}

            return string.Empty;
        }
    }
    
    public class LoginInfo
    {
        public object UserID { get; set; }
        public object UserName { get; set; }
        public object Password { get; set; }
        public object UserFullName { get; set; }
        public object RoleName { get; set; }
    }
    public class BranchInfo
    {
        public object BranchID { get; set; }
        public object BranchName { get; set; }
        public object BranchCode { get; set; }
        public object BranchAddress { get; set; }
        public object PhoneNumber { get; set; }
        public object LandLine { get; set; }
        public object GSTIN { get; set; }
        public object BranchCounterID { get; set; }
        public object BranchCounterName { get; set; }
        public int MultiEditThreshold { get; set; }

        public object FilterMRPByStock { get; set; }
    }
}

