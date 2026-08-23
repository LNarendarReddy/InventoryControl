using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WarehouseCloudSync.DropboxUtility
{
    public class DropboxFileUploader : IDropboxFileUploader
    {
        private readonly DropboxOptions options;

        public DropboxFileUploader(DropboxOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            this.options = options;
        }

        public async Task<DropboxUploadResult> UploadFileAsync(string localFilePath, string dropboxDestinationPath, CancellationToken cancellationToken)
        {
            DateTime startedAt = DateTime.Now;
            DropboxUploadResult result = new DropboxUploadResult
            {
                LocalFilePath = localFilePath,
                DropboxPath = dropboxDestinationPath,
                StartedAt = startedAt
            };

            try
            {
                ValidateUploadRequest(localFilePath, dropboxDestinationPath);
                ValidateConfiguration();

                for (int attempt = 0; ; attempt++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    try
                    {
                        using (DropboxClient client = CreateClient())
                        using (FileStream stream = File.Open(localFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            long uploadLength = stream.Length;

                            FileMetadata metadata;

                            if (stream.Length <= options.ChunkSizeBytes)
                            {
                                metadata = await UploadSmallFileAsync(client, dropboxDestinationPath, stream).ConfigureAwait(false);
                            }
                            else
                            {
                                metadata = await UploadLargeFileAsync(client, dropboxDestinationPath, stream, cancellationToken).ConfigureAwait(false);
                            }

                            result.Success = true;
                            result.UploadedBytes = uploadLength;
                            result.DropboxRevision = metadata?.Rev;
                            result.DropboxServerModified = metadata?.ServerModified;
                            result.DropboxClientModified = metadata?.ClientModified;
                            result.CompletedAt = DateTime.Now;
                            return result;
                        }
                    }
                    catch (Exception) when (attempt < options.MaxRetryCount && !cancellationToken.IsCancellationRequested)
                    {
                        await Task.Delay(options.RetryDelay, cancellationToken).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.CompletedAt = DateTime.Now;
                result.ErrorMessage = ex.Message;
                result.Exception = ex;
                return result;
            }
        }

        public Task<IReadOnlyList<DropboxUploadResult>> UploadFilesAsync(string localFolderPath, string dropboxDestinationFolder, CancellationToken cancellationToken)
        {
            return UploadFilesAsync(localFolderPath, dropboxDestinationFolder, "*.*", cancellationToken);
        }

        public async Task<IReadOnlyList<DropboxUploadResult>> UploadFilesAsync(string localFolderPath, string dropboxDestinationFolder, string searchPattern, CancellationToken cancellationToken)
        {
            ValidateFolderUploadRequest(localFolderPath, dropboxDestinationFolder);

            string pattern = string.IsNullOrWhiteSpace(searchPattern) ? "*.*" : searchPattern;
            IList<DropboxUploadResult> results = new List<DropboxUploadResult>();
            string normalizedDropboxFolder = NormalizeDropboxFolder(dropboxDestinationFolder);

            foreach (string localFilePath in Directory.GetFiles(localFolderPath, pattern).Where(path => !Path.GetExtension(path).Equals(".tmp", StringComparison.OrdinalIgnoreCase)))
            {
                cancellationToken.ThrowIfCancellationRequested();

                string dropboxPath = normalizedDropboxFolder + "/" + Path.GetFileName(localFilePath);
                DropboxUploadResult result = await UploadFileAsync(localFilePath, dropboxPath, cancellationToken).ConfigureAwait(false);
                results.Add(result);
            }

            return new List<DropboxUploadResult>(results).AsReadOnly();
        }

        private DropboxClient CreateClient()
        {
            if (!string.IsNullOrWhiteSpace(options.AppKey) && !string.IsNullOrWhiteSpace(options.AppSecret))
            {
                return new DropboxClient(options.AccessToken, options.AppKey, options.AppSecret);
            }

            return new DropboxClient(options.AccessToken);
        }

        private async Task<FileMetadata> UploadSmallFileAsync(DropboxClient client, string dropboxDestinationPath, FileStream stream)
        {
            return await client.Files.UploadAsync(dropboxDestinationPath, mode: GetWriteMode(), body: stream).ConfigureAwait(false);
        }

        private async Task<FileMetadata> UploadLargeFileAsync(DropboxClient client, string dropboxDestinationPath, FileStream stream, CancellationToken cancellationToken)
        {
            int chunkSize = options.ChunkSizeBytes > 0 ? options.ChunkSizeBytes : 10 * 1024 * 1024;
            ulong numberOfChunks = (ulong)Math.Ceiling((double)stream.Length / chunkSize);
            byte[] buffer = new byte[chunkSize];
            string sessionId = null;

            for (ulong index = 0; index < numberOfChunks; index++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                int bytesRead = stream.Read(buffer, 0, chunkSize);
                using (MemoryStream memoryStream = new MemoryStream(buffer, 0, bytesRead))
                {
                    if (index == 0)
                    {
                        UploadSessionStartResult result = await client.Files.UploadSessionStartAsync(body: memoryStream).ConfigureAwait(false);
                        sessionId = result.SessionId;
                    }
                    else
                    {
                        UploadSessionCursor cursor = new UploadSessionCursor(sessionId, (ulong)chunkSize * index);

                        if (index == numberOfChunks - 1)
                        {
                            CommitInfo commitInfo = new CommitInfo(dropboxDestinationPath, mode: GetWriteMode());
                            return await client.Files.UploadSessionFinishAsync(cursor, commitInfo, body: memoryStream).ConfigureAwait(false);
                        }
                        else
                        {
                            await client.Files.UploadSessionAppendV2Async(cursor, false, body: memoryStream).ConfigureAwait(false);
                        }
                    }
                }
            }

            throw new InvalidOperationException("Dropbox upload session did not finish.");
        }

        private WriteMode GetWriteMode()
        {
            if (options.Overwrite)
            {
                return WriteMode.Overwrite.Instance;
            }

            return WriteMode.Add.Instance;
        }

        private void ValidateConfiguration()
        {
            if (string.IsNullOrWhiteSpace(options.AccessToken))
            {
                throw new InvalidOperationException("Dropbox access token is not configured.");
            }
        }

        private static void ValidateUploadRequest(string localFilePath, string dropboxDestinationPath)
        {
            if (string.IsNullOrWhiteSpace(localFilePath)) throw new ArgumentException("Local file path is required.", nameof(localFilePath));
            if (string.IsNullOrWhiteSpace(dropboxDestinationPath)) throw new ArgumentException("Dropbox destination path is required.", nameof(dropboxDestinationPath));
            if (Path.GetExtension(localFilePath).Equals(".tmp", StringComparison.OrdinalIgnoreCase)) throw new InvalidOperationException("Temporary export files cannot be uploaded.");
            if (!File.Exists(localFilePath)) throw new FileNotFoundException("Local file was not found.", localFilePath);
            if (!dropboxDestinationPath.StartsWith("/", StringComparison.Ordinal)) throw new ArgumentException("Dropbox destination path must start with '/'.", nameof(dropboxDestinationPath));
        }

        private static void ValidateFolderUploadRequest(string localFolderPath, string dropboxDestinationFolder)
        {
            if (string.IsNullOrWhiteSpace(localFolderPath)) throw new ArgumentException("Local folder path is required.", nameof(localFolderPath));
            if (!Directory.Exists(localFolderPath)) throw new DirectoryNotFoundException("Local folder was not found: " + localFolderPath);
            if (string.IsNullOrWhiteSpace(dropboxDestinationFolder)) throw new ArgumentException("Dropbox destination folder is required.", nameof(dropboxDestinationFolder));
            if (!dropboxDestinationFolder.StartsWith("/", StringComparison.Ordinal)) throw new ArgumentException("Dropbox destination folder must start with '/'.", nameof(dropboxDestinationFolder));
        }

        private static string NormalizeDropboxFolder(string folder)
        {
            return folder.Replace("\\", "/").Trim().TrimEnd('/');
        }
    }
}
