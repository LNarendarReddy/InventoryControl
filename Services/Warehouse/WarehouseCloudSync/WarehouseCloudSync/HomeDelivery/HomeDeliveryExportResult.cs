using System;

namespace WarehouseCloudSync.HomeDelivery
{
    public class HomeDeliveryExportResult
    {
        public string ExportCode { get; set; }

        public string FileName { get; set; }

        public string OutputFilePath { get; set; }

        public string DropboxTargetFolder { get; set; }

        public bool Success { get; set; }

        public int RowCount { get; set; }

        public string Stage { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime CompletedAt { get; set; }

        public string ErrorMessage { get; set; }
    }
}
