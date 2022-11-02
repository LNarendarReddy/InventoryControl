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

namespace NSRetail
{
    public class GoogleDriveRepository
    {
        public string[] Scopes = { DriveService.Scope.Drive };

        public static DriveService GetService()
        {
            var credential = GoogleCredential.FromFile("silver-pen-366817-97cb2d7ccef2.json").CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            return service;
        }

        public static string DownloadFile()
        {
            string FilePath = string.Empty;
            try
            {
                DriveService service = GetService();
                var folderRequest = service.Files.List();
                folderRequest.Q = $"mimeType = 'application/vnd.google-apps.folder' and name = '{Convert.ToString(ConfigurationManager.AppSettings["BuildType"])}builds'";
                var folderResponse = folderRequest.Execute();
                string folderID = folderResponse.Files.Any() ? folderResponse.Files.FirstOrDefault().Id : string.Empty;

                if (string.IsNullOrEmpty(folderID)) return string.Empty;

                var fileRequest = service.Files.List();
                fileRequest.Q = $"parents in '{folderID}' and mimeType = 'application/x-msdownload' and name = 'NSRetail.exe'";
                var fileResponse = fileRequest.Execute();
                string fileID = fileResponse.Files.Any() ? fileResponse.Files.FirstOrDefault().Id : string.Empty;

                if (string.IsNullOrEmpty(fileID)) return string.Empty;

                FilesResource.GetRequest request = service.Files.Get(fileID);
                string FileName = request.Execute().Name;
                FilePath = System.IO.Path.Combine(Application.UserAppDataPath, FileName);

                MemoryStream stream1 = new MemoryStream();
                request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            {
                                SplashScreenManager.Default.SetWaitFormDescription($"Downloading {progress.BytesDownloaded} Bytes");
                                break;
                            }
                        case DownloadStatus.Completed:
                            {
                                SplashScreenManager.Default.SetWaitFormDescription("Download complete.");
                                SaveStream(stream1, FilePath);
                                break;
                            }
                        case DownloadStatus.Failed:
                            {
                                SplashScreenManager.Default.SetWaitFormDescription("Download failed.");
                                break;
                            }
                    }
                };
                request.Download(stream1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return FilePath;
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