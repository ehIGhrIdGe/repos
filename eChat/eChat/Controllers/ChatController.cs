using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eChat.Models;

namespace eChat.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Msg = "Let's enjoy chat!";
            return View();
        }

        public IActionResult ShowChat()
        {
            ViewBag.Msg = "Let's enjoy chat!";
            return View(DbUtil.GetMessages());
        }
    }
}
