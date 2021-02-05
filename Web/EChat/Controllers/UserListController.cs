using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EChat.Models;

namespace EChat.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserListController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Message = (string)TempData["outMsg"];
            var model = new UserListViewModel();
            model.UserList = ManagerDb.GetUsers();
            return View(model);
        }
    }
}
