using DevExpress.Maui.Core.Internal;
using DevExpress.Maui.Mvvm;
using DevExpress.XtraReports.UI;
using DevExpress.XtraRichEdit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSRetailLiteApp.Helpers
{
    public class PrintHelper
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

                var printService = ApplicationHelper.ServiceProvider.GetService<IPrintService>();

                printed = printService != null &&
                    await printService.PrintAsync(stream, "BillPrintJob");
            }
            catch (Exception ex)
            {
                printed = false;
            }
            return printed;
        }
    }
}
