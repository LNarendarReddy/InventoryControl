using CommunityToolkit.Maui.Views;
using NSRetailLiteApp.ViewModels.Common;
using ZXing;

namespace NSRetailLiteApp.Views.Common;

public partial class BarcodeScannerPopup : Popup
{
    public BarcodeScannerPopup(BarcodeScannerViewModel barcodeScannerViewModel)
    {
        InitializeComponent();
        BarcodeScannerViewModel = barcodeScannerViewModel;

        barcodeScanner.Options = new ZXing.Net.Maui.BarcodeReaderOptions()
        {
            Formats = ZXing.Net.Maui.BarcodeFormat.Code128
            , TryHarder = true
            , AutoRotate = true
            , TryInverted = true
            , Multiple = false
        };
    }

    public BarcodeScannerViewModel BarcodeScannerViewModel { get; }

    private void CameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        try
        {
            BarcodeScannerViewModel.ScannedCode = e.Results.First().Value;
            CloseAsync();
        }
        catch (Exception ex) { }
    }
}