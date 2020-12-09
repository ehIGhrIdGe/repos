using System;
using System.IO;

namespace SmpleException
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("↓↓↓----ファイルのパスを入力----↓↓↓");
                var s = new GetFile.GetFile();
                s.GetFileContents();
            }
            catch(IOException e)
            {
                Console.WriteLine($"[e.Message] => {e.Message}");
                Console.WriteLine($"[e.InnerException] => {e.InnerException}");
                Console.WriteLine($"[e.StackTrace] => {e.StackTrace}");
                Console.WriteLine($"[e.ToString] => {e.ToString()}");
            }
            
            
        }
    }
}
