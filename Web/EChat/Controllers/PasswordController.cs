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
        public IActionResult Index(string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                TempData["outMsg"] = "新しいパスワードが入力されていません。";
                return RedirectToAction("Index");
            }
            else if (string.IsNullOrEmpty(confirmPassword))
            {
                TempData["outMsg"] = "確認用パスワードが入力されていません。";
                return RedirectToAction("Index");
            }
            else if(newPassword.CompareTo(confirmPassword) != 0)
            {
                TempData["outMsg"] = "新しいパスワードと確認用パスワードがが一致しません。";
                return RedirectToAction("Index");
            }

            var userInfoList = ManagerDb.GetUserInfo((string)TempData["userId"]);
            var user = new User(userInfoList[0].UserId,
                                userInfoList[0].UserName,
                                userInfoList[0].PasswordType,
                                userInfoList[0].PasswordSalt,
                                ManageHash.GetHash(newPassword + userInfoList[0].PasswordSalt),
                                userInfoList[0].IsAdministrator);

            if (!ManagerDb.UpdateUserInfo(user))
            {
                TempData["outMsg"] = "パスワードの更新に失敗しました。";
                return RedirectToAction("Index");
            }

            TempData["outMsg"] = "パスワードが更新されました。";
            return RedirectToAction("Index", "Login");
        }
    }
}
