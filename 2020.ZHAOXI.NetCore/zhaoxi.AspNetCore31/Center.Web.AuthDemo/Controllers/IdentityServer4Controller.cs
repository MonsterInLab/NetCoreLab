using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Center.Web.AuthDemo.Controllers
{
    public class IdentityServer4Controller : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            Console.WriteLine("************************************************");
            //string id_token = base.HttpContext.Request.Cookies["id_token"];
            //var token_parts = id_token.Split('.');
            //var header = Encoding.UTF8.GetString(Base64Url.Decode(token_parts[0]));
            //var claims = Encoding.UTF8.GetString(Base64Url.Decode(token_parts[1]));
            //var sign = Encoding.UTF8.GetString(Base64Url.Decode(token_parts[2]));
            //Console.WriteLine(header);
            //Console.WriteLine(claims);
            //Console.WriteLine(sign);

            foreach (var item in base.HttpContext.User.Identities.First().Claims)
            {
                Console.WriteLine($"{item.Type}:{item.Value}");
            }
            Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUserInfo()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
