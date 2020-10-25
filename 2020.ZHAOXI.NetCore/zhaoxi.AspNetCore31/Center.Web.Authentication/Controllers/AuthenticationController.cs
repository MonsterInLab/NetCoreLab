using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Center.Interface;
using Center.Web.Authentication.Utility;
using Center.Web.Authentication.Utility.RSA;
using Data.EFCCore31.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Center.Web.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private IJWTService _iJWTService = null;
        private readonly IUserService _iUserService;
        private readonly IConfiguration _iConfiguration;

        public AuthenticationController(ILogger<AuthenticationController> logger,
            IUserService userService,
             IConfiguration configuration,
            IJWTService jWTService)
        {
            _iJWTService = jWTService;
            _logger = logger;
            _iUserService = userService;
            _iConfiguration = configuration;
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
               // string token = this._JWTService.GetToken(name);

                CurrentUserModel currentUser = new CurrentUserModel()
                {
                    Id = 123,
                    Account = "xuyang@zhaoxiEdu.Net",
                    EMail = "57265177@qq.com",
                    Mobile = "18664876671",
                    Sex = 1,
                    Age = 33,
                    Name = name,
                    Role = "Admin"
                };

                string token = this._iJWTService.GetToken(currentUser);
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

        [Route("GetKey")]
        [HttpGet]
        public string GetKey()
        {
            string keyDir = Directory.GetCurrentDirectory();
            if (RSAHelper.TryGetKeyParameters(keyDir, false, out RSAParameters keyParams) == false)
            {
                keyParams = RSAHelper.GenerateAndSaveKey(keyDir, false);
            }

            return JsonConvert.SerializeObject(keyParams);
        }
    }
}
