using CommunityToolkit.Maui;
using DevExpress.Maui;
using DevExpress.Maui.CollectionView.Internal;
using DevExpress.Maui.CollectionView;
using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;
using DevExpress.Maui.Pdf;

namespace NSRetailLiteApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseDevExpressPdf()
                .UseDevExpressCollectionView()
                .UseDevExpressControls()
                .UseDevExpressEditors()
                .UseDevExpress()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseBarcodeReader()
                .UseMauiApp<App>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
