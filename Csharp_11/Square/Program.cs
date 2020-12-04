using System;

namespace Square
{
    class Program
    {
        static void Main(string[] args)
        {
            var square = new Square();
            Console.WriteLine(square.Width);
            Console.WriteLine(square.CalculationArea());

            Console.WriteLine();
            square = new Square(4);
            Console.WriteLine(square.Width);
            Console.WriteLine(square.CalculationArea());


            Console.WriteLine("Hello World!");
        }
    }
}
