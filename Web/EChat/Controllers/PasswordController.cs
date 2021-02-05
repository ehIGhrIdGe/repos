using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EChat.Models;

namespace EChat.Controllers
{
    public class PasswordController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var tempModel = new PasswordViewModel();
            tempModel.OldPassword = (string)TempData.Peek("userId");
            ViewBag.Message = (string)TempData["outMsg"];
            return View(tempModel);
        }

        [HttpPost]
        public IActionResult Index(PasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrEmpty(model.OldPassword))
            {
                TempData["outMsg"] = "古いパスワードが入力されていません。";
                return RedirectToAction("Index");
            }
            else if (string.IsNullOrEmpty(model.NewPassword))
            {
                TempData["outMsg"] = "新しいパスワードが入力されていません。";
                return RedirectToAction("Index");
            }
            else if (string.IsNullOrEmpty(model.ConfirmPassword))
            {
                TempData["outMsg"] = "確認用パスワードが入力されていません。";
                return RedirectToAction("Index");
            }
            else if(model.NewPassword.CompareTo(model.ConfirmPassword) != 0)
            {
                TempData["outMsg"] = "新しいパスワードと確認用パスワードがが一致しません。";
                return RedirectToAction("Index");
            }

            var oldUserData = ManagerDb.GetUserData((string)TempData["userId"]);
            var newUserData = new User(
                                    oldUserData.UserId, 
                                    oldUserData.UserName, 
                                    1, 
                                    oldUserData.PasswordSalt, ManageHash.GetHash(model.NewPassword + oldUserData.PasswordSalt),
                                    oldUserData.IsAdministrator
                                    );

            if (!ManagerDb.UpdateUser(newUserData))
            {
                TempData["outMsg"] = "パスワードの更新に失敗しました。";
                TempData["userId"] = newUserData.UserId;
                return RedirectToAction("Index");
            }

            TempData["outMsg"] = "パスワードが更新されました。";
            return RedirectToAction("Index", "Login");
        }
    }
}
