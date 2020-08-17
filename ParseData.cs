using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.IO.Compression;
using static ParserTask.Data;
using System.Text;

namespace ParserTask
{
    class ParseData
    {
        //dowload json
        private void DownloadFile(string url)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile($"{url}", "json.gz");
        }
        //decompress file
        private void Decompress(string compressedFile, string targetFile)
        {
            // stream for read compress file
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                // stream for decompress
                using (FileStream targetStream = File.Create(targetFile))
                {
                    // stream of rearch
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        Console.WriteLine("file: {0}", targetFile + "\n");
                    }
                }
            }
        }
        private Root readJson(string fileJson)
        {
            try
            {
                if (File.Exists(fileJson))
                {
                    string fileContent = File.ReadAllText(fileJson);
                    var root = JsonConvert.DeserializeObject<Root>(fileContent);
                    return root;
                    
                }
                else Console.WriteLine("File is does't exist");
            }
            catch
            {
                throw new Exception("Can not read file");
            }
            return null;
        }
      private void WriteResult(string path)
        {
           Root root =  readJson("json.json");
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
                {
                    foreach (Item r in root.items)
                    {
                        sw.WriteLine("title: " + r.title);
                        sw.WriteLine("author: " + r.owner.display_name);
                        sw.WriteLine("answered?: " + r.is_answered);
                        sw.WriteLine("link: " + r.link);
                        sw.WriteLine("---------------");
                    }
                    Console.WriteLine("Data is recorded");
                }
            }
            catch
            {
                throw new Exception("can't write data");
            }
        }
        public ParseData()
        {
            string url = @"https://api.stackexchange.com/2.2/search?order=desc&sort=activity&intitle=beautiful&site=stackoverflow";
            DownloadFile(url);
            Decompress("json.gz", "json.json");
            WriteResult("Data.txt");
        }
    }
}
