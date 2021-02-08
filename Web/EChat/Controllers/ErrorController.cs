using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EChat.Controllers
{
    [AllowAnonymous]
    [IgnoreAntiforgeryToken]
    public class ErrorController : Controller
    {
        public IActionResult Index(int id = 0)
        {
            switch (id)
            {
                case StatusCodes.Status404NotFound:
                    //例えば、404 であれば、
                    //静的なページやビューにリダイレクト
                    break;
                default:
                    break;
            }

            //内部エラーの場合はエラー情報を取得
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>();

            //Exception に格納されている情報を使ってエラーを表示
            var exception = error?.Error;

            return View();
        }
    }
}
