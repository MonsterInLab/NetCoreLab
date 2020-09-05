using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Center.Web.Models;
using Center.Web.Utility.WebApiHelper;
using Consul;
using Microsoft.Extensions.Configuration;

namespace Center.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _iConfiguration;

        public HomeController(ILogger<HomeController> logger
             , IConfiguration configuration)
        {
            _logger = logger;
            _iConfiguration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Info()
        {
            List<Users> userList = new List<Users>();
            string resultUrl = null;

            #region 直接调用
            //{
            //    string url = "http://localhost:5178/api/users/get";
            //    string result = WebApiHelperExtend.InvokeApi(url);
            //    userList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Users>>(result);
            //    resultUrl = url;
            //}
            #endregion
            #region 这么多地址你怎么管理？1 2 3 要累死--可以自行选择

            #endregion

            #region 通过consul去发现这些服务地址
            {
                using (ConsulClient client = new ConsulClient(c =>
                {
                    c.Address = new Uri("http://localhost:8500/");
                    c.Datacenter = "dc1";
                }))
                {
                    var dictionary = client.Agent.Services().Result.Response;
                    string message = "";
                    foreach (var keyValuePair in dictionary)
                    {
                        AgentService agentService = keyValuePair.Value;
                        this._logger.LogWarning($"{agentService.Address}:{agentService.Port} {agentService.ID} {agentService.Service}");//找的是全部服务 全部实例  其实可以通过ServiceName筛选
                        message += $"{agentService.Address}:{agentService.Port};";
                    }
                    //获取当前consul的全部服务
                    base.ViewBag.Message = message;
                }
            }
            #endregion

            #region 调用---负载均衡
            {
                //string url = "http://localhost:5726/api/users/get";
                //string url = "http://localhost:5727/api/users/get";
                //string url = "http://localhost:5728/api/users/get";
                string url = "http://ZhaoxiUserService/api/users/get";
                //consul解决使用服务名字 转换IP:Port----DNS

                Uri uri = new Uri(url);
                string groupName = uri.Host;    //ZhaoxiUserService

                using (ConsulClient client = new ConsulClient(c =>
                {
                    c.Address = new Uri("http://localhost:8500/");
                    c.Datacenter = "dc1";
                }))
                {
                    var dictionary = client.Agent.Services().Result.Response;
                    var list = dictionary.Where(k => k.Value.Service.Equals(groupName, StringComparison.OrdinalIgnoreCase));
                   // KeyValuePair<string, AgentService> keyValuePair = new KeyValuePair<string, AgentService>();

                }
            }
            #endregion
            base.ViewBag.Users = userList;
            base.ViewBag.Url = resultUrl;

            return View();
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
