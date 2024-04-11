using DevExpress.XtraSplashScreen;
using Dropbox.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSRetail.Utilities
{
    internal class DropBoxRepository
    {

        public async static Task<string> DownloadFile()
        {
            string localFilePath = Path.Combine(Application.UserAppDataPath, "NSRetail.exe")
                , serverFilePath = @"/NSRetail_Build_Drops/ProdWANBuilds/NSRetail.exe"
                , key = Utility.Decrypt("2A+S8fTsYt5IqxTNhflRE37XAswAdukxpLESN95kJ6Csu+cbzGH79ScGD2O+jgitKnhg44oa4HgJ+zcHXMeozRFQPlta4KPXZrvZ4VXrmn+iJ3xeQkFghg0N8czTVfl2fNnUr0RyIE8FpvRJk2Kbij1RR5JbG0dr0HCjRWoRUAyGEnk+X9dYrHkDfNfzIvq2wLfOdgAm50EMb3p2HyS6aA==");

            SplashScreenManager.Default.SetWaitFormDescription("Establishig connection");
            
            using (var dbx = new DropboxClient(key))
            {
                SplashScreenManager.Default.SetWaitFormDescription("Connection established, Searching for installer");
                using (var response = await dbx.Files.DownloadAsync(serverFilePath))
                {
                    ulong fileSize = response.Response.Size;
                    const int bufferSize = 1024 * 1024;

                    var buffer = new byte[bufferSize];

                    using (var fileStream = File.Create(localFilePath))
                    {
                        using (var stream = await response.GetContentAsStreamAsync())
                        {
                            var length = stream.Read(buffer, 0, bufferSize);

                            while (length > 0)
                            {
                                fileStream.Write(buffer, 0, length);
                                var percentage = 100 * (ulong)fileStream.Length / fileSize;
                                SplashScreenManager.Default.SetWaitFormDescription($"Download progress : {percentage}%");

                                length = stream.Read(buffer, 0, bufferSize);
                            }
                        }
                    }
                }
            }

            return localFilePath;
        }
    }
}