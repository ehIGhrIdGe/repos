using System;

namespace Shape
{
    class Program
    {
        static void Main(string[] args)
        {
            var quandrilateral = new Quandrilateral(5, 6);
            var triangle = new Triangle(5, 6);
            var circle = new Circle(5);

            Console.WriteLine(quandrilateral);
            Console.WriteLine(triangle);
            Console.WriteLine(circle);
            
        }
    }
}
