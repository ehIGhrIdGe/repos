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
        public IActionResult Index()
        {
            var messages = ManagerDb.GetMessages();
            
            return View(messages);
        }
    }
}
