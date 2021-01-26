using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CChat.Models
{
    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public Person(int age, string name)
        {
            Age = age;
            Name = name;
        }
    }
}
