using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BackupUploadAPI
{
    internal class DropboxRepository
    {

        const int ChunkSize = 10 * 1024 * 1024;

        public static async Task Upload(string localPath, string remotePath)
        {
            //// victory account
            //string key = Utility.Decrypt("rtvOKSkGkto9Vigl6T78F4J2Ho8QXn7nHabO9HG6qOxqIpuvr+QO1FllQLXioqEik4ToI0FA33hV67QJlRndoQJhSymVXUJuesAQF6AMIWQ=")
            //  , appId = Utility.Decrypt("VKWCC6efz906q5aWM/e6Wg==")
            //  , appValue = Utility.Decrypt("taKC2YKSWI77QcU6abVbCA==");

            // sls enterprise
            string key = Utility.Decrypt("9FG/G0MQZHU/ZpWj7uno+VeAYwQxGDlGMt9Yz5wYAm7H2Tkq8Qq6RGAthvK7GeHTrgdmClaO+7vkji7HO/7R8fezjkQFdETjXAHP+y/RC+s=")
              , appId = Utility.Decrypt("rkNrpSlDlz+Qo3dNaS+r1Q==")
              , appValue = Utility.Decrypt("4XE8ypW7vSP8Jamdz2MGCg==");

            using (var dbx = new DropboxClient(key, appId, appValue))
            {
                using (var fileStream = File.Open(localPath, FileMode.Open))
                {
                    await Upload(dbx, remotePath + "/" + Path.GetFileName(localPath), fileStream);
                }
            }
        }

        private static async Task Upload(DropboxClient client, string remotePath, FileStream stream)
        {
            if (stream.Length <= ChunkSize)
            {
                await client.Files.UploadAsync(remotePath, body: stream);
                Console.WriteLine($"Upload status - 100 %");
            }
            else
            {
                await ChunkUpload(client, remotePath, stream);
            }
        }

        private static async Task ChunkUpload(DropboxClient client, string path, FileStream stream)
        {
            ulong numChunks = (ulong)Math.Ceiling((double)stream.Length / ChunkSize);
            double streamLength = stream.Length;
            byte[] buffer = new byte[ChunkSize];
            string sessionId = null;
            for (ulong idx = 0; idx < numChunks; idx++)
            {
                var byteRead = stream.Read(buffer, 0, ChunkSize);

                using (var memStream = new MemoryStream(buffer, 0, byteRead))
                {
                    if (idx == 0)
                    {
                        var result = await client.Files.UploadSessionStartAsync(body: memStream);
                        sessionId = result.SessionId;
                    }
                    else
                    {
                        var cursor = new UploadSessionCursor(sessionId, (ulong)ChunkSize * idx);

                        if (idx == numChunks - 1)
                        {
                            FileMetadata fileMetadata = await client.Files.UploadSessionFinishAsync(cursor, new CommitInfo(path), body: memStream);
                            Console.WriteLine(fileMetadata.PathDisplay);
                        }
                        else
                        {
                            await client.Files.UploadSessionAppendV2Async(cursor, false, body: memStream);
                        }
                    }
                }

                Console.WriteLine($"Upload status - { Math.Round((idx * 1.00 / numChunks) * 100, 2)} %");
            }
        }
    }
}
