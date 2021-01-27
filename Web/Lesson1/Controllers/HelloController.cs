using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lesson1.Models;

namespace Lesson1.Controllers
{
    public class HelloController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Hello World";
            return View();
        }

        public IActionResult List()
        {
            var persons = new List<Person>()
            {
                new Person("yanada", 21, new DateTime(2000, 01, 01), false),
                new Person("tanaka", 41, new DateTime(1980, 01, 01), true),
                new Person("yanada", 24, new DateTime(1996, 04, 01), false),
                new Person("yanada", 25, new DateTime(1995, 06, 01), true),
            };

            return View(persons);
        }

        public IActionResult Details()
        {
            var person = new Person("morino", 24, new DateTime(1996, 01, 01), false);
            return View(person);
        }

        public IActionResult Create(string name, int age, DateTime birthDay, bool isMarried)
        {

            var persons = new List<Person>();
            var person = new Person(name, age, birthDay, isMarried);

            persons.Add(person);

            return View();
        }


        /// <summary>
        /// CreatとListを同時にできないかを試すために救ったアクション
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="birthDay"></param>
        /// <param name="isMarried"></param>
        /// <returns></returns>
        public IActionResult CreateAndList(string name, int age, DateTime birthDay, bool isMarried)
        {
            var personViewModel = new PersonViewModel();
            personViewModel.Person = CreateCreate(name, age, birthDay, isMarried);
            personViewModel.Persons = ListList(personViewModel.Persons, personViewModel.Person);

            return View(personViewModel);
        }

        /// <summary>
        /// CreateAndList用のCreate
        /// </summary>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="birthDay"></param>
        /// <param name="isMarried"></param>
        /// <returns></returns>
        public Person CreateCreate(string name, int age, DateTime birthDay, bool isMarried)
        {
            var person = new Person(name, age, birthDay, isMarried);
            return person;
        }

        /// <summary>
        /// CreateAndList用のList
        /// </summary>
        /// <returns></returns>
        public List<Person> ListList(List<Person> persons, Person person)
        {
            if(persons is null)
            {
                persons = new List<Person>();
                persons.Add(person);                
            }
            else
            {
                persons.Add(person);
            }
            return persons;
        }
    }
}
