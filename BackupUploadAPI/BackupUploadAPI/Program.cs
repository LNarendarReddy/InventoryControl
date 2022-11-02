using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BackupUploadAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string backuptype = args.Any() ? args[0] : string.Empty;
            if(string.IsNullOrEmpty(backuptype))
            {
                Console.WriteLine("Input Argument is null");
                return;
            }
            string InputFile = 
                new DirectoryInfo(ConfigurationManager.AppSettings[$"{backuptype}Source"]).GetFiles().OrderByDescending
                (o => o.LastWriteTime).FirstOrDefault().FullName;
            Console.WriteLine($"Input File : {InputFile}");
            string folderID = GoogleDriveRepository.GetfolderID(ConfigurationManager.AppSettings[$"{backuptype}Target"]);
            Console.WriteLine($"Found google drive folder Name : {ConfigurationManager.AppSettings[$"{backuptype}Target"]} FolderID : {folderID}");
            GoogleDriveRepository.UploadFile(folderID, InputFile);
            Console.WriteLine($"Uploading success");
            Tuple<string, string> fileinfo = GoogleDriveRepository.GetfileIDtoDelete(folderID);
            if (fileinfo != null)
            {
                Console.WriteLine($"File to delete filename : {fileinfo.Item2} fileid : {fileinfo.Item1}");
                GoogleDriveRepository.DeleteFile(fileinfo.Item1);
                Console.WriteLine($"Deletion success");
            }
            else
            {
                Console.WriteLine($"Fileid to delete not exists");
            }
        }
    }
}
