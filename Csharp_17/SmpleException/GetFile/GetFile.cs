using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SmpleException.GetFile
{
    class GetFile
    {
        private string FilePath { get; set; }

        public GetFile()
        {
            FilePath = Console.ReadLine();
        }

        public void GetFileContents()
        {
            try
            {
                using (var reader = new StreamReader(FilePath))
                {
                    var text = reader.ReadToEnd();

                    Console.WriteLine($"\n↓↓↓----ファイルの内容です----↓↓↓\n{text}");
                }
            }
            catch(IOException e)
            {
                throw;
            }
        }
    }
}
