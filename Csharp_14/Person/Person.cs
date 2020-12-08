using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    class Person
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Partner { get; set; }

        public Person()
        {
            Name = "";
            Sex = "";
            Age = -1;
            Partner = "";
        }

        public Person(string name, string sex, int age, string partner)
        {
            Name = name;
            Sex = sex;
            Age = age;
            Partner = partner;
        }

        public virtual void Greet()
        {
            if(Age < 0)
            {
                Console.WriteLine($"My name is {Name} and I am a {Sex} unknown age.");                
            }
            else
            {
                Console.WriteLine($"My name is {Name} and I am a {Age} years old {Sex}.");
            }

            if(Partner is null)
            {
                Console.WriteLine("I am single.");
            }
            else
            {
                Console.WriteLine($"My partner is {Partner}.");
            }
        }

        public void Marry(Person partner)
        {

        }

        //public override string ToString()
        //{
        //    return "Myyyyyyyyyyy name is " + Name + " and I am a " + Age + " years old "+  Sex + ".";
        //}
    }
}
