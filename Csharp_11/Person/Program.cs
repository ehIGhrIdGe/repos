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

            person = new Person("george", "man", 45, "he");
            Console.WriteLine($"{person.Name},{person.Sex},{person.Age},{person.Partner}");
            person.Greet();

            var person_2 = new Person("yui", "woman", 23,"");

            person.Marry(person_2);
        }
    }
}
