using Center.Interface;
using Data.EFCCore31.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Center.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region MyRegion
        private ILoggerFactory _Factory = null;
        private ILogger<UsersController> _logger = null;
        private IUserService _IUserService = null;
        private List<Users> _userList = null;

        private readonly IConfiguration _iConfiguration;
        private readonly IUserService _iUserService;

        public UsersController(ILoggerFactory factory,
            ILogger<UsersController> logger,
            IConfiguration configuration
            , IUserService userService)
        {
            this._Factory = factory;
            this._logger = logger;
            this._iUserService = userService;
            this._iConfiguration = configuration;

            this._userList = this._iUserService.Query<User>(u => u.Id > 1)
                                        .OrderBy(u => u.Id)
                                        .Skip(1)
                                        .Take(5)
                                        .Select(u => new Users()
                                        {
                                            UserID = u.Id,
                                            UserEmail = u.Email,
                                            UserName = u.Name
                                        }).ToList();

            Console.WriteLine("this isUsersController ");
        }
        #endregion

        #region HttpGet
        // GET api/Users
        [HttpGet]
        public IEnumerable<Users> Get()
        {
            var users = this._iUserService.Query<User>(u => u.Id >0).Select(u => new Users()
            {
                UserID = u.Id,
                UserEmail = u.Email,
                UserName = u.Name
            }).ToList();

            return users;
        }

        // GET api/Users/5
        [HttpGet]
        public Users GetUserByID(int id)
        {
            base.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");//允许跨域
            string idParam = base.HttpContext.Request.Query["id"];
            var user = this._iUserService.Query<User>(u => u.Id == id).Select(u => new Users()
            {
                UserID = u.Id,
                UserEmail = u.Email,
                UserName = u.Name
            }).ToList().FirstOrDefault();

            if (user == null)
            {
               // throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return user;
        }

        //GET api/Users/?username=xx
        [HttpGet]
        //[CustomBasicAuthorizeAttribute]
        //[CustomExceptionFilterAttribute]
        //[RouteAttribute("GetUserByName")]
        public IEnumerable<Users> GetUserByName(string userName)
        {

            //throw new Exception("1234567");

            string userNameParam = base.HttpContext.Request.Query["userName"];

            return _userList.Where(p => string.Equals(p.UserName, userName, StringComparison.OrdinalIgnoreCase));
        }

        //GET api/Users/?username=xx&id=1
        [HttpGet]
        public IEnumerable<Users> GetUserByNameId(string userName, int id)
        {
            string idParam = base.HttpContext.Request.Query["id"];
            string userNameParam = base.HttpContext.Request.Query["userName"];

            return _userList.Where(p => string.Equals(p.UserName, userName, StringComparison.OrdinalIgnoreCase));
        }

        [HttpGet]
        public IEnumerable<Users> GetUserByModel([FromUri] Users user)
        {
            string idParam = base.HttpContext.Request.Query["id"];
            string userNameParam = base.HttpContext.Request.Query["userName"];
            string emai = base.HttpContext.Request.Query["email"];

            return _userList;
        }

        [HttpGet]
        public IEnumerable<Users> GetUserByModelUri([FromUri] Users user)
        {
            string idParam = base.HttpContext.Request.Query["id"];
            string userNameParam = base.HttpContext.Request.Query["userName"];
            string emai = base.HttpContext.Request.Query["email"];

            return _userList;
        }

        [HttpGet]
        public IEnumerable<Users> GetUserByModelSerialize(string userString)
        {
            Users user = JsonConvert.DeserializeObject<Users>(userString);
            return _userList;
        }

        [HttpGet]
        public IEnumerable<Users> GetUserByModelSerializeWithoutGet(string userString)
        {
            Users user = JsonConvert.DeserializeObject<Users>(userString);
            return _userList;
        }
        #endregion HttpGet

        #region HttpPost
        //POST api/Users/RegisterNone
        [HttpPost]
        public Users RegisterNone()
        {
            return _userList.FirstOrDefault();
        }

        [HttpPost]
        public Users RegisterNoKey([FromBody] int id)
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/register
        //只接受一个参数的需要不给key才能拿到
        [HttpPost]
        public Users Register([FromBody] int id)//可以来自FromBody   FromUri
                                                //public Users Register(int id)//可以来自url
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/RegisterUser
        [HttpPost]
        public Users RegisterUser(Users user)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["UserID"];
            string nameParam = base.HttpContext.Request.Form["UserName"];
            string emailParam = base.HttpContext.Request.Form["UserEmail"];

            return user;
        }


        //POST api/Users/register
        [HttpPost]
        public string RegisterObject(JObject jData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[UserID]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = jData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }

        [HttpPost]
        public string RegisterObjectDynamic(dynamic dynamicData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[UserID]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = dynamicData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }
        #endregion HttpPost

        #region HttpPut
        //POST api/Users/RegisterNonePut
        [HttpPut]
        public Users RegisterNonePut()
        {
            return _userList.FirstOrDefault();
        }

        [HttpPut]
        public Users RegisterNoKeyPut([FromBody] int id)
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/registerPut
        //只接受一个参数的需要不给key才能拿到
        [HttpPut]
        public Users RegisterPut([FromBody] int id)//可以来自FromBody   FromUri
                                                   //public Users Register(int id)//可以来自url
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/RegisterUserPut
        [HttpPut]
        public Users RegisterUserPut(Users user)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["UserID"];
            string nameParam = base.HttpContext.Request.Form["UserName"];
            string emailParam = base.HttpContext.Request.Form["UserEmail"];

            return user;
        }


        //POST api/Users/registerPut
        [HttpPut]
        public string RegisterObjectPut(JObject jData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[UserID]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = jData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }

        [HttpPut]
        public string RegisterObjectDynamicPut(dynamic dynamicData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[UserID]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = dynamicData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }
        #endregion HttpPut

        #region HttpDelete
        //POST api/Users/RegisterNoneDelete
        [HttpDelete]
        public Users RegisterNoneDelete()
        {
            return _userList.FirstOrDefault();
        }

        [HttpDelete]
        public Users RegisterNoKeyDelete([FromBody] int id)
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/registerDelete
        //只接受一个参数的需要不给key才能拿到
        [HttpDelete]
        public Users RegisterDelete([FromBody] int id)//可以来自FromBody   FromUri
                                                      //public Users Register(int id)//可以来自url
        {
            string idParam = base.HttpContext.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/RegisterUserDelete
        [HttpDelete]
        public Users RegisterUserDelete(Users user)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["UserID"];
            string nameParam = base.HttpContext.Request.Form["UserName"];
            string emailParam = base.HttpContext.Request.Form["UserEmail"];
            return user;
        }


        //POST api/Users/registerDelete
        [HttpDelete]
        public string RegisterObjectDelete(JObject jData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[UserID]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = jData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }

        [HttpDelete]
        public string RegisterObjectDynamicDelete(dynamic dynamicData)//可以来自FromBody   FromUri
        {
            string idParam = base.HttpContext.Request.Form["User[UserID]"];
            string nameParam = base.HttpContext.Request.Form["User[UserName]"];
            string emailParam = base.HttpContext.Request.Form["User[UserEmail]"];
            string infoParam = base.HttpContext.Request.Form["info"];
            dynamic json = dynamicData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }
        #endregion HttpDelete
    }

    public class Users
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
