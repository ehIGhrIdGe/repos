using EChat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Newtonsoft.Json;

namespace EChat.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Message = (string)TempData["outMsg"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string userId, string password)
        {
            if (TryLogin(userId, password, out string outMsg, out var identity))
            {
                //sign in
                await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            }
            else if (outMsg == "ChangePassword")
            {
                return RedirectToAction("Index", "Password");
            }

            TempData["outMsg"] = outMsg;
            return RedirectToAction("Index", "Chat");
        }

        /// <summary>
        /// ログインが成功したかを boool で返す。
        /// </summary>
        /// <param name="inputUserId"></param>
        /// <param name="inputPass"></param>
        /// <param name="outMsg">Falseの場合、理由を文字列で返す。Trueの場合、string.Empty が入る。</param>
        /// <returns></returns>
        private static bool TryLogin(string inputUserId, string inputPass, out string outMsg, out ClaimsIdentity identity)
        {
            if (string.IsNullOrEmpty(inputUserId) || string.IsNullOrEmpty(inputPass))
            {
                outMsg = "IDかパスワードが入力されていません。";
            }

            var userInfoList = ManagerDb.GetUserInfo(inputUserId);

            if (userInfoList is null)
            {
                identity = null;
                outMsg = "ユーザーIDが間違っているか、登録されていません。";
                return false;
            }
            else if (userInfoList[0].UserId == userInfoList[0].Password && userInfoList[0].Password == inputPass)
            {
                identity = null;
                outMsg = "ChangePassword";
                return false;
            }
            else if (userInfoList[0].UserId == userInfoList[0].Password && userInfoList[0].Password != inputPass)
            {
                identity = null;
                outMsg = "初期パスワードはユーザーIDを同じです。";
                return false;
            }
            else if (!ManageHash.CompareHashStr(ManageHash.GetHash(inputPass + userInfoList[0].PasswordSalt), userInfoList[0].Password))
            {
                identity = null;
                outMsg = "パスワードが間違っています。";
                return false;
            }
            else
            {
                //Create Claims
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, userInfoList[0].UserId)
                };

                identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                outMsg = string.Empty;
                return true;
            }
        }
    }
}
