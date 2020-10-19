using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Center.Web.AuthDemo.Controllers
{
    public class AuthorizationController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string name, string password,string email, string role = "Admin")
        {
            if ("stone".Equals(name, StringComparison.CurrentCultureIgnoreCase)
               && password.Equals("123456"))
            {
                var claimIdentity = new ClaimsIdentity("Custom");
                claimIdentity.AddClaim(new Claim(ClaimTypes.Name, name));
              //  claimIdentity.AddClaim(new Claim(ClaimTypes.Email, "1047284222@qq.com"));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Email, email));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Role, role));

               await base.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                });
                return new JsonResult(new
                {
                    Result = true,
                    Message = "登录成功"
                });
            }
            else
            {
                await Task.CompletedTask;
                return new JsonResult(new 
                {
                    Result = true,
                    Message = "登录成功"
                });
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new JsonResult(new 
            {
                Result = true,
                Message = "退出成功"
            });
        }

        /// <summary>
        /// 需要授权的页面
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Cookies")]//
        public IActionResult Info()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult InfoAdmin()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public IActionResult InfoUser()
        {
            return View();
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult InfoAdminUser()
        {
            return View();
        }

        #region Policy
        [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminPolicy")]
        public IActionResult InfoAdminPolicy()
        {
            return View();
        }
        [Authorize(AuthenticationSchemes = "Cookies", Policy = "UserPolicy")]
        public IActionResult InfoUserPolicy()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "Cookies", Policy = "QQEmail")]
        public IActionResult InfoQQEmail()
        {
            return View();
        }

        [Authorize(AuthenticationSchemes = "Cookies", Policy = "DoubleEmail")]
        public IActionResult InfoDoubleEmail()
        {
            return View();
        }
        #endregion

        #region CustomScheme
        [AllowAnonymous]
        public async Task<IActionResult> LoginCustomScheme(string name, string password, string role = "Admin")
        {
            if ("Eleven".Equals(name, StringComparison.CurrentCultureIgnoreCase)
               && password.Equals("123456"))
            {
                var claimIdentity = new ClaimsIdentity("Custom");
                claimIdentity.AddClaim(new Claim(ClaimTypes.Name, name));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Email, "1047284222@qq.com"));
                claimIdentity.AddClaim(new Claim(ClaimTypes.Role, role));

                await base.HttpContext.SignInAsync("CustomScheme", new ClaimsPrincipal(claimIdentity), new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                });
                return new JsonResult(new
                {
                    Result = true,
                    Message = "登录成功"
                });
            }
            else
            {
                await Task.CompletedTask;
                return new JsonResult(new
                {
                    Result = true,
                    Message = "登录成功"
                });
            }
        }

        public async Task<IActionResult> LogoutCustomScheme()
        {
            await base.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return new JsonResult(new
            {
                Result = true,
                Message = "退出成功"
            });
        }

        /// <summary>
        /// 需要授权的页面
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "CustomScheme")]
        public IActionResult InfoCustomScheme()
        {
            return View();
        }
        #endregion
    }
}
