using DevExpress.DataAccess.Native.Web;
using DevExpress.XtraSplashScreen;
using Dropbox.Api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetailPOS
{
    public class DropboxRepository
    {
        public static async Task<bool> DownloadFile(string folderName, bool isOverlayShown, string prefix = "Prod")
        {
            string localFilePath = Path.Combine(Application.UserAppDataPath, folderName)
                , serverFilePath = $@"/NSRetail_Build_Drops/ProdWANBuilds/{prefix}{folderName}"
                , key = Utility.Decrypt("rtvOKSkGkto9Vigl6T78F4J2Ho8QXn7nHabO9HG6qOxqIpuvr+QO1FllQLXioqEik4ToI0FA33hV67QJlRndoQJhSymVXUJuesAQF6AMIWQ=")
                , appId = Utility.Decrypt("VKWCC6efz906q5aWM/e6Wg==")
                , appValue = Utility.Decrypt("taKC2YKSWI77QcU6abVbCA==");

            Utility.ReportText(isOverlayShown, "Establishig connection");

            using (var dbx = new DropboxClient(key, appId, appValue))
            {
                Utility.ReportText(isOverlayShown, "Connection established, Searching for files");

                var list = await dbx.Files.ListFolderAsync(serverFilePath);
                var totalFiles = list.Entries.Where(i => i.IsFile);

                const int bufferSize = 1024 * 1024;
                int totalFileCount = totalFiles.Count(), curFile = 1;

                if (totalFileCount == 0) return false;

                foreach (var item in totalFiles)
                {
                    using (var response = await dbx.Files.DownloadAsync($"{serverFilePath}/{item.Name}"))
                    {
                        ulong fileSize = response.Response.Size;
                        var buffer = new byte[bufferSize];

                        using (var fileStream = File.Create($@"{localFilePath}\{item.Name}"))
                        {
                            using (var stream = await response.GetContentAsStreamAsync())
                            {
                                var length = stream.Read(buffer, 0, bufferSize);

                                while (length > 0)
                                {
                                    fileStream.Write(buffer, 0, length);
                                    var percentage = 100 * (ulong)fileStream.Length / fileSize;
                                    Utility.ReportText(isOverlayShown, $"File : {curFile} of {totalFileCount}, Download progress : {percentage}%");

                                    length = stream.Read(buffer, 0, bufferSize);
                                }
                            }
                        }
                    }

                    curFile++;
                }
            }

            return true;
        }
    }
}
