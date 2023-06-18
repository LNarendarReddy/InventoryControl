using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System.IO;
using Google.Apis.Util.Store;
using static Google.Apis.Drive.v3.DriveService;
using System.Web;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices.ComTypes;
using Google.Apis.Drive.v3.Data;

namespace BackupUploadAPI
{
    public class GoogleDriveRepository
    {
        public string[] Scopes = { DriveService.Scope.Drive };

        public static DriveService GetService()
        {
            var credential = GoogleCredential.FromFile("silent-bolt-232805-c18a3e7b0d0a.json")
                .CreateScoped(new[] { DriveService.ScopeConstants.Drive });
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential
            });
            return service;
        }

        public static string GetfolderID(string folderName)
        {
            string folderID = string.Empty;
            try
            {
                DriveService service = GetService();
                var folderRequest = service.Files.List();
                folderRequest.Q = $"mimeType = 'application/vnd.google-apps.folder' and name = '{folderName}'";
                var folderResponse = folderRequest.Execute();
                var names = folderResponse.Files.Where(x => x.Name.StartsWith("NSRetail_backup")).Select(x => new Tuple<string, string>(x.Name, x.Id)).ToList();
                var file = folderResponse.Files.FirstOrDefault(x => x.Name == "NSRetail_backup_2022_12_13_230057_5161037.bak");

                folderID = folderResponse.Files.Any() ? folderResponse.Files.FirstOrDefault().Id : string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Upload request failed Message : " + ex.Message);
                Console.WriteLine("Upload request failed Stack :" + ex.StackTrace);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception Message : " + ex.InnerException.Message);
                    Console.WriteLine("Inner exception Stack :" + ex.InnerException.StackTrace);
                }
            }
            return folderID;
        }

        public static Tuple<string,string> GetfileIDtoDelete(string folderID)
        {
            Tuple<string, string> tuple = null;
            try
            {
                var fileRequest = GetService().Files.List();
                fileRequest.Q = $"parents in '{folderID}'";
                var fileResponse = fileRequest.Execute();
                int NumberofFiles = Convert.ToInt16(ConfigurationManager.AppSettings["NumberofFiles"]);
                if (fileResponse.Files.Count > NumberofFiles)
                    tuple = Tuple.Create(fileResponse.Files.LastOrDefault().Id, fileResponse.Files.LastOrDefault().Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Upload request failed Message : " + ex.Message);
                Console.WriteLine("Upload request failed Stack :" + ex.StackTrace);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception Message : " + ex.InnerException.Message);
                    Console.WriteLine("Inner exception Stack :" + ex.InnerException.StackTrace);
                }
            }
            return tuple;
        }

        public static string GetfileIDByName(string folderID, string fileName)
        {
            string fileID = string.Empty;
            try
            {
                var fileRequest = GetService().Files.List();
                fileRequest.Q = $"parents in '{folderID}' and name = '{fileName}'";
                var fileResponse = fileRequest.Execute();
                fileID = fileResponse.Files.Any() ? fileResponse.Files.LastOrDefault().Id : string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Upload request failed Message : " + ex.Message);
                Console.WriteLine("Upload request failed Stack :" + ex.StackTrace);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception Message : " + ex.InnerException.Message);
                    Console.WriteLine("Inner exception Stack :" + ex.InnerException.StackTrace);
                }
            }
            return fileID;
        }

        public static string UploadFile(string folderID, string path)
        {
            string fileid = string.Empty;
            
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            try
            {
                DriveService service = GetService();

                var FileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = Path.GetFileName(path),
                    MimeType = MimeMapping.GetMimeMapping(path),
                    Parents = new List<string>
                {
                    folderID
                }};
                string OldFileID = GetfileIDByName(folderID, Path.GetFileName(path));
                if (!string.IsNullOrEmpty(OldFileID))
                {
                    FilesResource.UpdateMediaUpload request;
                    using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                    {
                        FileMetaData.Parents = null;
                        request = service.Files.Update(FileMetaData, OldFileID, stream, FileMetaData.MimeType);
                        request.Fields = "id";
                        request.Upload();
                        fileid = request.ResponseBody.Id;
                    }
                }
                else
                {
                    FilesResource.CreateMediaUpload request;
                    using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                    {                        
                        request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                        request.QuotaUser = "victorybazars.nssretail@gmail.com";
                        request.Fields = "id";
                        var uploadProgress = request.Upload();
                        if (uploadProgress.Exception != null) throw uploadProgress.Exception;
                        fileid = request.ResponseBody?.Id;

                        Permission newPermission = new Permission
                        {
                            EmailAddress = "victorybazars.nssretail@gmail.com",
                            Type = "user",
                            Role = "owner"
                        };

                        var ownerTransferRequest = service.Permissions.Create(newPermission, fileid);
                        ownerTransferRequest.TransferOwnership = true;
                        ownerTransferRequest.Execute();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Upload request failed Message : " + ex.Message);
                Console.WriteLine("Upload request failed Stack :" + ex.StackTrace);
                if(ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception Message : " + ex.InnerException.Message);
                    Console.WriteLine("Inner exception Stack :" + ex.InnerException.StackTrace);
                }
            }
            return fileid;
        }

        public static void DeleteFile(string fileID)
        {
            try
            {
                DriveService service = GetService();                
                service.Files.Delete(fileID).Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Upload request failed Message : " + ex.Message);
                Console.WriteLine("Upload request failed Stack :" + ex.StackTrace);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner exception Message : " + ex.InnerException.Message);
                    Console.WriteLine("Inner exception Stack :" + ex.InnerException.StackTrace);
                }
            }
        }
    }
}