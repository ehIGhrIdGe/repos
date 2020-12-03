using System;
using System.Xml.XPath;

namespace Method_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("数字を入力してね");
            Console.Write("a : ");
            var text = Console.ReadLine();
            var a = int.Parse(text);
            Console.Write("b : ");
            text = Console.ReadLine();
            var b = int.Parse(text);

            Console.WriteLine(sum(a, b));
            Console.WriteLine(subtraction(a, b));
            Console.WriteLine(multiplication(a, b));
            Console.WriteLine(division(a, b));
            Console.WriteLine(Surplus(a, b));
        }

        static int sum(int a, int b)
        {
            var sum = a + b;
            return sum;
        }

        static int subtraction(int a, int b)
        {
            var subtraction = a - b;
            return subtraction;
        }

        static int multiplication(int a, int b)
        {
            var multiplication = a * b;
            return multiplication;
        }

        static int division(int a, int b)
        {
            var division = a / b ;
            return division;
        }

        static int Surplus(int a, int b)
        {
            var Surplus = a % b ;
            return Surplus;
        }
    }
}
