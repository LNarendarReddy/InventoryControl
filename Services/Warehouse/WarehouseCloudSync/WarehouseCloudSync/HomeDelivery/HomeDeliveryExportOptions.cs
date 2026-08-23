using System;

namespace WarehouseCloudSync.HomeDelivery
{
    public class HomeDeliveryExportOptions
    {
        public bool Enabled { get; set; }

        public int IntervalMinutes { get; set; } = 180;

        public bool RunOnStartup { get; set; } = true;

        public string WorkingFolder { get; set; } = @"D:\HomeDelivery\Exports";

        public string MainProcedureName { get; set; } = "USP_R_HOMEDELIVERY_EXPORT_DEFINITIONS";

        public int CommandTimeoutSeconds { get; set; } = 3600;

        public string DropboxRootFolder { get; set; } = "/HomeDelivery";

        public void ApplyDefaults()
        {
            if (IntervalMinutes <= 0) IntervalMinutes = 180;
            if (string.IsNullOrWhiteSpace(WorkingFolder)) WorkingFolder = @"D:\HomeDelivery\Exports";
            if (string.IsNullOrWhiteSpace(MainProcedureName)) MainProcedureName = "USP_R_HOMEDELIVERY_EXPORT_DEFINITIONS";
            if (CommandTimeoutSeconds <= 0) CommandTimeoutSeconds = 3600;
            if (string.IsNullOrWhiteSpace(DropboxRootFolder)) DropboxRootFolder = "/HomeDelivery";
        }
    }
}
