using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EChat.Models;

namespace EChat.Controllers
{
    [Authorize(Roles = "Admin, People")]
    public class ProfileController : Controller
    {
        [HttpGet]
        public IActionResult Index(string id)
        {
            ViewBag.Message = (string)TempData["outMsg"];
            TempData["userId"] = id;
            return View(ManagerDb.GetUserData(id));
        }

        [HttpPost]
        public IActionResult Index(string userId, string userName, byte passwordType, string passwordSalt, string password, bool isAdministrator)
        {
            if (ModelState.IsValid)
            {
                User user = new User(userId, userName, passwordType, passwordSalt, password, isAdministrator);
                TempData["outMsg"] = ManagerDb.UpdateUser(user) ? "ユーザー情報を更新しました。" : "ユーザー情報の更新に失敗しました。";                
            }

            return RedirectToAction("Index");
        }

        public IActionResult PasswordReset()
        {
            var oldUserData = ManagerDb.GetUserData((string)TempData["userId"]);
            var newUserData = new User(oldUserData.UserId, oldUserData.UserName, 0, ManageHash.CreateHash(), oldUserData.UserId, oldUserData.IsAdministrator);

            TempData["outMsg"] = ManagerDb.UpdateUser(newUserData) ? "パスワードをリセットしました。次回ログイン時に変更。" : "パスワードのリセットに失敗しました。";
            return Redirect($"./Index/{newUserData.UserId}");
        }
    }
}
