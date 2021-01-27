using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EChat.Models;

namespace EChat.Controllers
{
    public class ChatController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(ManagerDb.GetMessages());
        }

        [HttpPost]
        public IActionResult Index(string msg)
        {
            if (!ManagerDb.InsertMessage(msg))
            {

            }
            else
            {
                RedirectToAction(nameof(Index));
            }

            return View(ManagerDb.GetMessages());
        }
    }
}
