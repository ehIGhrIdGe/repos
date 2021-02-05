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
    public class UserDetailsController : Controller
    {

        [HttpGet]
        public IActionResult Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View(new User(null, null, 0, ManageHash.CreateHash(), null, false));
            }
            else
            {
                TempData["userId"] = id;
                return View(ManagerDb.GetUserData(id));
            }
        }

        [HttpPost]
        public IActionResult Index(string userId, string userName, byte passwordType, string passwordSalt, string password, bool isAdministrator)
        {
            if (ModelState.IsValid)
            {
                User user;

                if (ManagerDb.GetUserData(userId) != null)
                {
                    user = new User(userId, userName, passwordType, passwordSalt, password, isAdministrator);
                    TempData["outMsg"] = ManagerDb.UpdateUser(user) ? "ユーザー情報を更新しました。" : "ユーザー情報の更新に失敗しました。";
                }
                else
                {
                    user = new User(userId, userName, passwordType, passwordSalt, userId, isAdministrator);
                    TempData["outMsg"] = ManagerDb.InsertUser(user) ? "ユーザー情報を登録しました。" : "ユーザー情報の登録に失敗しました。";
                }
            }
                        
            return RedirectToAction("Index", "UserList");
        }  
        
        public IActionResult DeleteUser()
        {
            TempData["outMsg"] = ManagerDb.DeleteUserData((string)TempData["userId"]) ? "ユーザー情報を削除しました。" : "ユーザー情報の削除に失敗しました。";
            return RedirectToAction("Index", "UserList");
        }
    }
}
