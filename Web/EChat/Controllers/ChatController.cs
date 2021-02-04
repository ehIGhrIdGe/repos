using EChat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace EChat.Controllers
{
    [Authorize]
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
            if (!ManagerDb.InsertMessage(msg, User.Identity.Name))
            {

            }
            else
            {
                RedirectToAction(nameof(Index));
            }

            return View(ManagerDb.GetMessages());
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Index","Login");
        }
    }
}
