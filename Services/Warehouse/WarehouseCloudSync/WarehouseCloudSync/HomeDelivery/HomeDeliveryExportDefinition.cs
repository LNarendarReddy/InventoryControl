namespace WarehouseCloudSync.HomeDelivery
{
    public class HomeDeliveryExportDefinition
    {
        public int ExportId { get; set; }

        public string ExportCode { get; set; }

        public string FileName { get; set; }

        public string ExportProcedureName { get; set; }

        public int ExecutionOrder { get; set; }

        public bool Enabled { get; set; }

        public string DropboxTargetFolder { get; set; }
    }
}
