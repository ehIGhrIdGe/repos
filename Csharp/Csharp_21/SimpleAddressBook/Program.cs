using System;
using SimpleAddressBook.Processing;

namespace SimpleAddressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Process.RunProcess();
            }
            catch(Exception e)
            {
                Console.WriteLine($"GetType().Name => {e.GetType().Name}");
                Console.WriteLine($"Message        => {e.Message}");
                Console.WriteLine($"StackTrace     => {e.StackTrace}");
                Console.WriteLine($"Source         => {e.Source}");
                Console.WriteLine($"InnerException => {e.InnerException}");
                Console.WriteLine($"TargetSite     => {e.TargetSite}");
            }
        }
    }
}
