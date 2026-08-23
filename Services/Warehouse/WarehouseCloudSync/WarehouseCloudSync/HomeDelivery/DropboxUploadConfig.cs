using System;
using WarehouseCloudSync.Data;
using WarehouseCloudSync.DropboxUtility;

namespace WarehouseCloudSync.HomeDelivery
{
    public class DropboxUploadConfig
    {
        public string AccessToken { get; set; }

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public bool Encrypted { get; set; }

        public bool Overwrite { get; set; } = true;

        public int MaxRetryCount { get; set; } = 3;

        public int RetryDelaySeconds { get; set; } = 30;

        public int ChunkSizeBytes { get; set; } = 10 * 1024 * 1024;

        public void ApplyDefaults()
        {
            if (MaxRetryCount < 0) MaxRetryCount = 3;
            if (RetryDelaySeconds <= 0) RetryDelaySeconds = 30;
            if (ChunkSizeBytes <= 0) ChunkSizeBytes = 10 * 1024 * 1024;
        }

        public DropboxOptions ToDropboxOptions()
        {
            ApplyDefaults();

            return new DropboxOptions
            {
                AccessToken = GetCredentialValue(AccessToken),
                AppKey = GetCredentialValue(AppKey),
                AppSecret = GetCredentialValue(AppSecret),
                Overwrite = Overwrite,
                MaxRetryCount = Math.Max(0, MaxRetryCount),
                RetryDelay = TimeSpan.FromSeconds(RetryDelaySeconds),
                ChunkSizeBytes = ChunkSizeBytes
            };
        }

        private string GetCredentialValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;
            return Encrypted ? SqlCon.Decrypt(value) : value;
        }
    }
}
