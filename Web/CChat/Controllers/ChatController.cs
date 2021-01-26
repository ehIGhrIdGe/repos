using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CChat.Models;

namespace CChat.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = DateTime.Now;
            var persons = new List<Person>
            {
                new Person(33, "dd"),
                new Person(33, "dd"),
                new Person(33, "dd")
            };
            return View(persons);
        }

        public IActionResult List()
        {
            var persons = new List<Person>
            {
                new Person(33, "dd"),
                new Person(33, "dd"),
                new Person(33, "dd")
            };
            return View(persons);
        }
    }
}
}
