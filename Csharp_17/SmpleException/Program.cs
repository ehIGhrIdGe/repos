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
            //catch(DirectoryNotFoundException e)
            //{
            //    Console.WriteLine($"[e.Message] => {e.Message}");
            //    Console.WriteLine($"[e.InnerException] => {e.InnerException}");
            //    Console.WriteLine($"[e.StackTrace] => {e.StackTrace}");
            //    Console.WriteLine($"[e.ToString] => {e.ToString()}");
            //}
            //catch (FileNotFoundException e)
            //{
            //    Console.WriteLine($"[e.Message] => {e.Message}");
            //    Console.WriteLine($"[e.InnerException] => {e.InnerException}");
            //    Console.WriteLine($"[e.StackTrace] => {e.StackTrace}");
            //    Console.WriteLine($"[e.ToString] => {e.ToString()}");
            //}
            //catch (IOException e)
            //{
            //    Console.WriteLine($"[e.Message] => {e.Message}");
            //    Console.WriteLine($"[e.InnerException] => {e.InnerException}");
            //    Console.WriteLine($"[e.StackTrace] => {e.StackTrace}");
            //    Console.WriteLine($"[e.ToString] => {e.ToString()}");
            //}
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine($"[e.GetType().Name] => {e.GetType().Name}");
                Console.WriteLine($"[e.Message]        => {e.Message}");
                Console.WriteLine($"[e.InnerException] => {e.InnerException}");
                Console.WriteLine($"[e.StackTrace]     => {e.StackTrace}");
            }

        }
    }
}
