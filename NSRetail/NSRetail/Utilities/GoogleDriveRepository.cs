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

namespace NSRetail
{
    public class GoogleDriveRepository
    {
        public string[] Scopes = { DriveService.Scope.Drive };

        public DriveService GetService()
        {
            //UserCredential credential;
            //using (var stream = new FileStream("silent_bolt.json", FileMode.Open, FileAccess.Read))
            //{
            //    //String FilePath = "client_secretUpdated.json";

            //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync( 
            //        GoogleClientSecrets.FromStream(stream).Secrets,
            //        Scopes,
            //        "user",
            //        CancellationToken.None
            //        //, new FileDataStore(FilePath, true)
            //        ).Result;
            //}


            var credential = GoogleCredential.FromFile("silver-pen-366817-97cb2d7ccef2.json").CreateScoped(new[] { DriveService.ScopeConstants.Drive });


            //create Drive API service.
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            return service;
        }

        public static string DownloadFile(string fileId)
        {
            string FilePath = string.Empty;
            try
            {
                DriveService service = GetService();


                //var requestlist = service.Files.List();
                //var response = requestlist.Execute();

                //foreach(var file in response.Files)
                //{
                //    Console.WriteLine(file);
                //}


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

        private void SaveStream(MemoryStream stream, string FilePath)
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