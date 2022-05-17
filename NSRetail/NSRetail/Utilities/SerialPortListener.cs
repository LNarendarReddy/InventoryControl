using System;
using System.Configuration;
using System.IO.Ports;
using System.Windows.Forms;

namespace NSRetail.Utilities
{
    internal static class SerialPortListener
    {
        static SerialPort _serialPort = new SerialPort();
        static frmMain objMainForm;

        public static void ListenSerialPort(frmMain instance)
        {
            if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["PortName"])) return;
            objMainForm = instance;
            _serialPort = new SerialPort(ConfigurationManager.AppSettings["PortName"].ToString(), 9600, Parity.None, 8, StopBits.One);
            //_serialPort = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            _serialPort.Handshake = Handshake.None;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;
            //_serialPort.Open();
        }

        static void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = _serialPort.ReadExisting();
            IBarcodeReceiver activeForm = GetActiveForm();
            if (activeForm == null) return;

            data = data.EndsWith(Environment.NewLine) ? data.Replace(Environment.NewLine, string.Empty) : data;
            Form.ActiveForm.BeginInvoke((Action)(() => activeForm.ReceiveBarCode(data)));
        }

        static IBarcodeReceiver GetActiveForm()
        {
            if(objMainForm.InvokeRequired)
            { 
                IAsyncResult asyncResult = objMainForm.BeginInvoke((Action)(() => GetActiveForm()));
                return objMainForm.EndInvoke(asyncResult) as IBarcodeReceiver;
            }

            return Form.ActiveForm as IBarcodeReceiver ?? objMainForm.ActiveMdiChild as IBarcodeReceiver;
        }
    }

    internal interface IBarcodeReceiver
    {
        void ReceiveBarCode(string data);
    }
}
