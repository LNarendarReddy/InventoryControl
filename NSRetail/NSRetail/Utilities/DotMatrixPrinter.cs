using System;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors;
using System.Linq;

namespace NSRetail
{
    class DotMatrixPrinter
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        /*=================================================*/

        private IntPtr HandlePrinter;
        private PrinterSettings ps;

        public DotMatrixPrinter()
        {
            HandlePrinter = IntPtr.Zero;
            ps = new PrinterSettings();
        }

        public string PrinterName
        {
            get
            {
                return ps.PrinterName;
            }
            set
            {
                ps.PrinterName = value;
            }
        }

        public bool ChoosePrinter()
        {
            PrintDialog pd = new PrintDialog();
            pd.PrinterSettings = ps;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                ps = pd.PrinterSettings;
                return true;
            }
            else return false;
        }

        public bool OpenPrinter(string printerName)
        {
            bool isPrinterOpened = Open(printerName);
            if(!isPrinterOpened)
            {
                XtraMessageBox.Show($"Unable to open dot matrix printer {printerName}");
            }

            return isPrinterOpened;
        }

        private bool Open(string printerName)
        {
            // see if printer is already open
            if (HandlePrinter != IntPtr.Zero) return false;

            // opens the printer
            bool risp = OpenPrinter(ps.PrinterName, out HandlePrinter, IntPtr.Zero);
            if (risp == false) return false;

            // starts a print job
            DOCINFOA MyDocInfo = new DOCINFOA();
            MyDocInfo.pDocName = printerName;
            MyDocInfo.pOutputFile = null;
            MyDocInfo.pDataType = "RAW";

            if (StartDocPrinter(HandlePrinter, 1, MyDocInfo))
            {
                StartPagePrinter(HandlePrinter); //starts a page       
                return true;
            }
            
            return false;
        }

        public bool Close()
        {
            if (HandlePrinter == IntPtr.Zero) return false;
            if (!EndPagePrinter(HandlePrinter)) return false;
            if (!EndDocPrinter(HandlePrinter)) return false;
            if (!ClosePrinter(HandlePrinter)) return false;
            HandlePrinter = IntPtr.Zero;
            return true;
        }

        public bool Print(string outputstring, int newLines = 0, Alignment alignment = Alignment.Near)
        {
            if (HandlePrinter == IntPtr.Zero) return false;

            string plainOutPut = outputstring.Replace("<b>", string.Empty).Replace("</b>", string.Empty);

            if (plainOutPut.Length < 96)
            {
                if (alignment == Alignment.Center)
                {                    
                    outputstring = outputstring.PadLeft(((96 - plainOutPut.Length) / 2) + outputstring.Length).PadRight(96 + (outputstring.Length - plainOutPut.Length));
                }
                else if (alignment == Alignment.Far)
                {                    
                    outputstring = outputstring.PadLeft(96 + (outputstring.Length - plainOutPut.Length));
                }
            }

            outputstring = outputstring.Replace("-", "_").Replace("<b>", $"{(char)27}E").Replace("</b>", $"{(char)20}{(char)27}F");
            outputstring += string.Concat(Enumerable.Repeat("\r\n", newLines));

            IntPtr buf = Marshal.StringToCoTaskMemAnsi(outputstring);
            Int32 done = 0;
            bool ok = WritePrinter(HandlePrinter, buf, outputstring.Length, out done);

            Marshal.FreeCoTaskMem(buf);

            if (!ok) return false;
            else return true;
        }

    }
}
