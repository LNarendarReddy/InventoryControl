using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WarehouseCloudSync.DropboxUtility
{
    public interface IDropboxFileUploader
    {
        Task<DropboxUploadResult> UploadFileAsync(string localFilePath, string dropboxDestinationPath, CancellationToken cancellationToken);

        Task<IReadOnlyList<DropboxUploadResult>> UploadFilesAsync(string localFolderPath, string dropboxDestinationFolder, CancellationToken cancellationToken);

        Task<IReadOnlyList<DropboxUploadResult>> UploadFilesAsync(string localFolderPath, string dropboxDestinationFolder, string searchPattern, CancellationToken cancellationToken);
    }
}
