using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Center.Interface;
using Center.Web.Authentication.Utility;
using Data.EFCCore31.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Center.Web.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private IJWTService _JWTService = null;
        private readonly IUserService _iUserService;

        public AuthenticationController(ILogger<AuthenticationController> logger,
            IUserService userService,
            IJWTService jWTService)
        {
            _JWTService = jWTService;
            _logger = logger;
            _iUserService = userService;
        }

        [Route("Login")]
        [HttpGet]
        public string Login(string name, string password)
        {
            ///这里肯定是需要去连接数据库做数据校验
            if (ValidateUser(name,password))
           // if ("stone".Equals(name) && "123456".Equals(password))//应该数据库
            {
                {
                    //这里可以去数据库做验证
                }
                string token = this._JWTService.GetToken(name);
                return JsonConvert.SerializeObject(new
                {
                    result = true,
                    token
                });
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    result = false,
                    token = ""
                });
            }
        }

        private bool ValidateUser(string userName, string password)
        {
            var users = this._iUserService.Query<User>(u => u.Name == userName && u.Password == password).FirstOrDefault();
            return users != null;
        }

        [Route("Get")]
        [HttpGet]
        public IEnumerable<int> Get()
        {
            return new List<int>() { 1, 2, 3, 4, 5, 6 };
        }
    }
}
