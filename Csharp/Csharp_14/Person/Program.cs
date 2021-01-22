using System;

namespace Person
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();
            Console.WriteLine($"{person.Name},{person.Sex},{person.Age},{person.Partner}");
            person.Greet();
            Console.WriteLine(person);

            person = new Person("george", "man", 45, "he");
            Console.WriteLine($"{person.Name},{person.Sex},{person.Age},{person.Partner}");
            person.Greet();
            Console.WriteLine(person);

            Console.WriteLine();
            var person_2 = new Person("yui", "woman", 23,"");
            Console.WriteLine(person_2);

            Console.WriteLine();
            var employee1 = new Employee();
            employee1.Greet();

            Console.WriteLine();
            var employee2 = new Employee("ccc","man",34,"ttt","Chief", "10", 2);
            employee2.Greet();


            
            var point2D = new Point2D(66, 33);
            Console.WriteLine();
            Console.WriteLine(point2D.ToString());

            var point3D = new Point2D(66, 33);
            Console.WriteLine();
            Console.WriteLine(point3D);

            Console.WriteLine();
            Console.WriteLine(point2D.Equals(point3D));

            Console.WriteLine();
            Console.WriteLine(point2D.GetHashCode() == point3D.GetHashCode()); 
        }
    }
}
