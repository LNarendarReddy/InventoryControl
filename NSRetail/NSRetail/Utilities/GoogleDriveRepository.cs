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

namespace NSRetail
{
    public class GoogleDriveRepository
    {
        public static string[] Scopes = { DriveService.Scope.Drive };

        public static DriveService GetService()
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                String FilePath = "client_secretUpdated.json";

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(FilePath, true)).Result;
            }

            //create Drive API service.
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "NSRetailPOS",
            });
            return service;
        }

        public static string DownloadGoogleFile(string fileId)
        {
            string FilePath = string.Empty;
            try
            {
                DriveService service = GetService();
                FilesResource.GetRequest request = service.Files.Get(fileId);
                string FileName = request.Execute().Name;
                FilePath = System.IO.Path.Combine(Application.UserAppDataPath, FileName);

                MemoryStream stream1 = new MemoryStream();
                request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            {
                                SplashScreenManager.Default.SetWaitFormDescription($"Downloading {progress.BytesDownloaded}");
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

    }
}