using System;

namespace WarehouseCloudSync.DropboxUtility
{
    public class DropboxUploadResult
    {
        public bool Success { get; set; }

        public string LocalFilePath { get; set; }

        public string DropboxPath { get; set; }

        public long UploadedBytes { get; set; }

        public string DropboxRevision { get; set; }

        public DateTime? DropboxServerModified { get; set; }

        public DateTime? DropboxClientModified { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime CompletedAt { get; set; }

        public string ErrorMessage { get; set; }

        public Exception Exception { get; set; }
    }
}
