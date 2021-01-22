using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Start";
            return View();
        }
    }
}
