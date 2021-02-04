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
            ViewBag.Message = (string)TempData["outMsg"];
            return View();
        }

        [HttpPost]
        public IActionResult Index(PasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (string.IsNullOrEmpty(model.NewPassword))
            {
                TempData["outMsg"] = "新しいパスワードが入力されていません。";
                return View();
            }
            else if (string.IsNullOrEmpty(model.ConfirmPassword))
            {
                TempData["outMsg"] = "確認用パスワードが入力されていません。";
                return View();
            }
            else if(model.NewPassword.CompareTo(model.ConfirmPassword) != 0)
            {
                TempData["outMsg"] = "新しいパスワードと確認用パスワードがが一致しません。";
                return View();
            }

            var userInfoList = ManagerDb.GetUserInfo((string)TempData["userId"]);
            var user = new User(userInfoList[0].UserId,
                                userInfoList[0].UserName,
                                1,
                                userInfoList[0].PasswordSalt,
                                ManageHash.GetHash(model.NewPassword + userInfoList[0].PasswordSalt),
                                userInfoList[0].IsAdministrator);

            if (!ManagerDb.UpdateUserInfo(user))
            {
                TempData["outMsg"] = "パスワードの更新に失敗しました。";
                return View();
            }

            TempData["outMsg"] = "パスワードが更新されました。";
            return RedirectToAction("Index", "Login");
        }
    }
}
