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
            ViewBag.Message = "get";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string userId, string password)
        {     
            if (TryLogin(userId, password))            
            {
                //Create Claims
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, userId)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //sign in
                await HttpContext.SignInAsync(new ClaimsPrincipal(identity));
            }

            return RedirectToAction("Index", "Chat");
        }

        private static bool TryLogin(string inputUserId, string inputPass)
        {
            if(string.IsNullOrEmpty(inputUserId) || string.IsNullOrEmpty(inputPass))
            {
                return false;
            }

            var userInfoList = ManagerDb.GetUserInfo(inputUserId);

            if(userInfoList is null)
            {
                return false;
            }
                
            if(!ManageHash.CompareHash(ManageHash.GetHash(inputPass), userInfoList[0].Password))
            {
                return false;
            }
            
            return true;
        }
    }
}
