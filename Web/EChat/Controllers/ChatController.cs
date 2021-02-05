using EChat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EChat.Controllers
{
    [Authorize(Roles = "Admin, People")]
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Login");
        }
    }
}
