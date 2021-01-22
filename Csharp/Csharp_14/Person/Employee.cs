using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    class Employee : Person
    {
        public string Post { get; set; }
        
        public string Affiliation { get; set; }

        public int LengthOfService { get; set; }

        public Employee()
        {
            Post = "Nothing";
            Affiliation = "Unknown";
            LengthOfService = 0;
        }

        public Employee(string name, string sex, int age, string partner, string post, string affiliation, int lengthOfService)
        {
            base.Name = name;
            base.Sex = sex;
            base.Age = age;
            base.Partner = partner;
            Post = post;
            Affiliation = affiliation;
            LengthOfService = lengthOfService;
        }

        public override void Greet()
        {
            if (Age < 0)
            {
                Console.WriteLine($"My name is {Name} and I am a {Sex} unknown age.");
            }
            else
            {
                Console.WriteLine($"My name is {Name} and I am a {Age} years old {Sex}.");
            }

            if (Partner is null)
            {
                Console.WriteLine("I am single.");
            }
            else
            {
                Console.WriteLine($"My partner is {Partner}.");
            }

                Console.WriteLine($"I'm in {Affiliation} and my post is {Post}.\nMy LengthOfService is {LengthOfService} old.");
            
        }
    }
}
