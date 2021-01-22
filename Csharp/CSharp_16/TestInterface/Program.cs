using System;

namespace TestInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person("のび太", "男", 12, "しずかちゃん");
            var circle = new Circle(9);

            PrintName(p);
            PrintName(circle);
        }

        static void PrintName(IName n)
        {
            Console.WriteLine(n.Name);
        }
    }
}
