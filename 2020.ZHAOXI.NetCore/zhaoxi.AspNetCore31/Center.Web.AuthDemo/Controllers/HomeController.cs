using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Center.Web.AuthDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Center.Web.AuthDemo.Utility;

namespace Center.Web.AuthDemo.Controllers
{
    [CustomAuthorizationFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 基于cookie 实现登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login(string name, string password)
        {
            if ("Stone".Equals(name, StringComparison.CurrentCultureIgnoreCase)
                 && password.Equals("123456"))
            {
                #region Filter
                base.HttpContext.Response.Cookies.Append("CurrentUser", "Stone", new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Expires = DateTime.UtcNow.AddMinutes(30)
                });


                base.HttpContext.Response.Cookies.Append("User", "stone2", new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Expires = DateTime.UtcNow.AddMinutes(30)
                });
                #endregion

                return new JsonResult(new
                {
                    Result = true,
                    Message = "登录成功"
                });
            }
            else
            {
                return new JsonResult(new
                {
                    Result = false,
                    Message = "登录失败"
                });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
