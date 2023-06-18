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
                GoogleDriveRepository.DeleteFile("1UUgmtyWDpQTuEbkT2JY6kj0__siJKGje");
                GoogleDriveRepository.DeleteFile("1EYcjjZom6I6XyrD9b21STEq_rzO1qG5w");
                GoogleDriveRepository.DeleteFile("1ohmGtfZaZ177v-6dQnIr5s_UsG9NJtLp");
                GoogleDriveRepository.DeleteFile("1wEbYXYrRg11dNQV6ZL_plEYmeWFpFTnv");
                GoogleDriveRepository.DeleteFile("1h7HjYkn4ba18lgc4EKVmO3yCOLY8u-Cq");
                GoogleDriveRepository.DeleteFile("1I9CZ7yD5Nw2Isko1R7N9eQYhqPhQzsIL");
                Console.WriteLine($"Deletion success");
            }
            else
            {
                Console.WriteLine($"Fileid to delete not exists");
            }
        }
    }
}
