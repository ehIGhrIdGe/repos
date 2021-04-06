using System;
using System.Globalization;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var datetime = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:dd-MMM-yyyy}", DateTime.Now);
            Console.WriteLine("0:dd-MMM-yyyy==={0}",datetime);

            datetime = string.Format("{0:dd-MMM-yyyy}", DateTime.Now);
            Console.WriteLine("0:dd-MMM-yyyy==={0}", datetime);

            datetime = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:dd-MMMM-yyyy}", DateTime.Now);
            Console.WriteLine("0:dd-MMMM-yyyy==={0}", datetime);

            datetime = string.Format("{0:dd-MMMM-yyyy}", DateTime.Now);
            Console.WriteLine("0:dd-MMMM-yyyy==={0}", datetime);

            datetime = string.Format("{0:dd-MM-yyyy}", DateTime.Now);
            Console.WriteLine("0:dd-MM-yyyy==={0}", datetime);
        }
    }
}
