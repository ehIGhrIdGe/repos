using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson1.Models
{
    public class Person
    {
        [DisplayName("名前")]
        public string Name { get; set; }

        [DisplayName("年齢")]
        public int Age{ get; set; }

        [DisplayName("誕生日")]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd}")]
        public DateTime BirthDay{ get; set; }

        [DisplayName("既婚")]
        public bool IsMarried{ get; set; }

        public Person(string name, int age, DateTime birthDay, bool isMarried)
        {
            Name = name;
            Age = age;
            BirthDay = birthDay;
            IsMarried = isMarried;
        }
    }
}
