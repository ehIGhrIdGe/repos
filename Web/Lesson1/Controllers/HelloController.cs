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
    }
}
