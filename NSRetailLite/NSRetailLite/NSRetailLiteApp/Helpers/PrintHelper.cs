#if ANDROID
using Android.Content;
using Android.OS;
using Android.Print;
using Java.IO;
#endif

using DevExpress.Maui.Core.Internal;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit.Model;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.Helpers
{
    public partial class PrintHelper
    {
        public static async Task<bool> PrintReport(XtraReport report)
        {
            bool printed;
            try
            {
                report.CreateDocument();

                // Export the report to a stream
                MemoryStream stream = new MemoryStream();
                report.ExportToPdf(stream);

                //PdfDocument pdf = new PdfDocument();
                //pdf.LoadFromStream(stream);

                //// In the settings object you can set the printer name and a lot of other options. No printer name prints to default.
                //PdfPrintSettings settings = new PdfPrintSettings();
                //settings.Color = false;

                //// OPTIONAL - FOR SILENT PRINT
                //settings.PrintController = new StandardPrintController();

                //pdf.Print(settings);

                //printed = true;
                var printService = ApplicationHelper.ServiceProvider.GetService<DevExpress.Maui.Mvvm.IPrintService>();

                printed = printService != null &&
                    await printService.PrintAsync(stream, "BillPrintJob");


                //narendar test code
                // Export the report to a PDF file
                //string resultFile = Path.Combine(FileSystem.Current.AppDataDirectory, report.Name + ".pdf");
                //report.ExportToPdf(resultFile);

                //await Share.Default.RequestAsync(new ShareFileRequest
                //{
                //    Title = "Share PDF file",
                //    File = new ShareFile(resultFile)

                //});
                //printed = true;
            }
            catch (Exception ex)
            {
                printed = false;
            }
            return printed;
        }

#if ANDROID
        public static Task PrintAsync(XtraReport report)
        {
            report.CreateDocument();

            // Export the report to a stream
            string resultFile = Path.Combine(FileSystem.Current.AppDataDirectory, "bill.pdf");
            report.ExportToPdf(resultFile);

            var printManager = (PrintManager)Platform.CurrentActivity.GetSystemService(Context.PrintService);

            // Now we can use the preexisting print helper class
            var utility = new PrintUtility(resultFile);

            printManager?.Print(resultFile, utility, null);

            return Task.CompletedTask;
        }
#endif 

    }

#if ANDROID
    public class PrintUtility : PrintDocumentAdapter
    {
        public string PrintFileName { get; set; }

        public PrintUtility(string printFileName)
        {
            PrintFileName = printFileName;
        }

        public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes, CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
        {
            if (cancellationSignal.IsCanceled)
            {
                callback.OnLayoutCancelled();
                return;
            }

            var pdi = new PrintDocumentInfo.Builder(PrintFileName).SetContentType(PrintContentType.Document).Build();

            callback.OnLayoutFinished(pdi, true);
        }

        public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination, CancellationSignal cancellationSignal, WriteResultCallback callback)
        {
            InputStream input = null;
            OutputStream output = null;

            try
            {
                input = new FileInputStream(PrintFileName);
                output = new FileOutputStream(destination.FileDescriptor);

                var buf = new byte[1024];
                int bytesRead;

                while ((bytesRead = input.Read(buf)) > 0)
                {
                    output.Write(buf, 0, bytesRead);
                }

                callback.OnWriteFinished(new[] { PageRange.AllPages });

            }
            catch (Java.IO.FileNotFoundException ee)
            {
                //Catch
            }
            catch (Exception e)
            {
                //Catch
            }
            finally
            {
                try
                {
                    input?.Close();
                    output?.Close();
                }
                catch (Java.IO.IOException e)
                {
                    e.PrintStackTrace();
                }
            }
        }
    }
#endif
}
