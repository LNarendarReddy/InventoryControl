using System;

namespace WarehouseCloudSync.DropboxUtility
{
    public class DropboxOptions
    {
        public string AccessToken { get; set; }

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public int ChunkSizeBytes { get; set; } = 10 * 1024 * 1024;

        public bool Overwrite { get; set; } = true;

        public int MaxRetryCount { get; set; } = 3;

        public TimeSpan RetryDelay { get; set; } = TimeSpan.FromSeconds(30);
    }
}
