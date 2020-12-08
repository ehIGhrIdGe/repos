using System;

namespace Namespace
{
    class Program
    {
        static void Main(string[] args)
        {
            var sun = TestEnum.DayOfWeek.Sun;
            var mon = TestEnum.DayOfWeek.Mon;
            var diamond = TestEnum.Suit.Diamond;
            var a = Test.Test.aaa.a;
            var b = Test._20201208.Temp.b;

            Console.WriteLine(sun);
            Console.WriteLine(mon);
            Console.WriteLine(diamond);
            Console.WriteLine(a);
            Console.WriteLine(b);
        }
    }
}
