using System;

namespace WarehouseCloudSync.HomeDelivery
{
    public class CsvExportResult
    {
        public bool Success { get; set; }

        public string OutputFilePath { get; set; }

        public string TemporaryFilePath { get; set; }

        public int RowCount { get; set; }

        public int ColumnCount { get; set; }

        public long FileSizeBytes { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime CompletedAt { get; set; }

        public string ErrorMessage { get; set; }

        public Exception Exception { get; set; }
    }
}
