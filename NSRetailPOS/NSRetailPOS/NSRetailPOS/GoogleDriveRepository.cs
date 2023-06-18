using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System.IO;
using System;
using Google.Apis.Util.Store;
using System.Threading;
using DevExpress.XtraSplashScreen;
using System.Windows.Forms;
using static Google.Apis.Drive.v3.DriveService;
using System.Web;
using System.Configuration;
using System.Linq;
using Google.Apis.Drive.v3.Data;
using DevExpress.XtraRichEdit.Layout;
using System.ComponentModel;
using DevExpress.XtraEditors;

namespace NSRetailPOS
{
    public class GoogleDriveRepository
    {
        public string[] Scopes = { DriveService.Scope.Drive };

        public static DriveService GetService()
        {
            var credential = GoogleCredential.FromFile(Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath),
                "silver-pen-366817-97cb2d7ccef2.json")).CreateScoped(new[] { DriveService.ScopeConstants.Drive });

            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            return service;
        }

        public static bool DownloadFile(string folderName, BackgroundWorker backgroundWorker = null)
        {
            bool _rtn = false;
            try
            {
                DriveService service = GetService();
                var folderRequest = service.Files.List();
                folderRequest.Q = $"mimeType = 'application/vnd.google-apps.folder' and name = '{Convert.ToString(ConfigurationManager.AppSettings["BuildType"])}{folderName}'";
                var folderResponse = folderRequest.Execute();
                string folderID = folderResponse.Files.Any() ? folderResponse.Files.FirstOrDefault().Id : string.Empty;

                if (string.IsNullOrEmpty(folderID)) return _rtn;

                var fileRequest = service.Files.List();
                fileRequest.Q = $"parents in '{folderID}'";
                var fileResponse = fileRequest.Execute();

                if (fileResponse.Files.Count == 0) return _rtn;
                foreach (Google.Apis.Drive.v3.Data.File file in fileResponse.Files)
                {
                    FilesResource.GetRequest request = service.Files.Get(file.Id);
                    string FileName = request.Execute().Name;
                    string filePath = System.IO.Path.Combine(Application.UserAppDataPath, folderName, FileName);

                    MemoryStream stream1 = new MemoryStream();
                    request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
                    {
                        switch (progress.Status)
                        {
                            case DownloadStatus.Downloading:
                                {
                                    Utility.ReportText(backgroundWorker, $"Downloading {progress.BytesDownloaded} Bytes");
                                    break;
                                }
                            case DownloadStatus.Completed:
                                {
                                    Utility.ReportText(backgroundWorker, "Download complete.");
                                    SaveStream(stream1, filePath);
                                    _rtn = true;
                                    break;
                                }
                            case DownloadStatus.Failed:
                                {
                                    Utility.ReportText(backgroundWorker, "Download failed.");
                                    XtraMessageBox.Show(progress.Exception.Message);
                                    _rtn = false;
                                    break;
                                }
                        }
                    };
                    request.Download(stream1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _rtn;
        }

        private static void SaveStream(MemoryStream stream, string FilePath)
        {
            using (System.IO.FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }
        }

        public static void FileUpload(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;
            DriveService service = GetService();
            var FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = Path.GetFileName(path);
            FileMetaData.MimeType = MimeMapping.GetMimeMapping(path);

            FilesResource.CreateMediaUpload request;

            using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
            {
                request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                request.Fields = "id";
                request.Upload();
            }
        }

        public static void DeleteFile(string fileID)
        {
            DriveService service = GetService();
            try
            {
                service.Files.Delete(fileID).Execute();
            }
            catch (Exception ex)
            {
                throw new Exception("Request Files.Delete failed.", ex);
            }
        }
    }
}