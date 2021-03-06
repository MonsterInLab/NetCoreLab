﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Center.Interface;
using Center.Web.Utility;
using Data.EFCCore31.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Center.Web.Controllers
{
    [CustomControllerFilterAttribute]
   // [TypeFilter(typeof(CustomActionCheckFilterAttribute))]
    [Authorize]
    public class ThirdController : Controller
    {
        #region Identity
        private readonly ILogger<ThirdController> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ITestServiceA _iTestServiceA;
        private readonly ITestServiceB _iTestServiceB;
        private readonly ITestServiceC _iTestServiceC;
        private readonly ITestServiceD _iTestServiceD;
        private readonly ITestServiceE _iTestServiceE;
        private readonly IServiceProvider _iServiceProvider;
        private readonly IConfiguration _configuration;
        private readonly IUserService _iUserService;

        public ThirdController(ILogger<ThirdController> logger,
            ILoggerFactory loggerFactory,
            ITestServiceA testServiceA,
            ITestServiceB testServiceB,
            ITestServiceC testServiceC,
            ITestServiceD testServiceD,
            ITestServiceE testServiceE,
            IServiceProvider iServiceProvider,
            IConfiguration configuration,
            IUserService userService
            )
        {
            _logger = logger;
            this._loggerFactory = loggerFactory;
            this._iTestServiceA = testServiceA;
            this._iTestServiceB = testServiceB;
            this._iTestServiceC = testServiceC;
            this._iTestServiceD = testServiceD;
            this._iTestServiceE = testServiceE;
            this._iServiceProvider = iServiceProvider;
            this._configuration = configuration;
            this._iUserService = userService;
        }
        #endregion

        /// <summary>
        /// 特性是编译时确定的，参数只能是常量，不能是变量
        /// </summary>
        /// <returns></returns>
        //[ServiceFilter(typeof(CustomExceptionFilterAttribute))]
        //[TypeFilter(typeof(CustomExceptionFilterAttribute))]
        [CustomIOCFilterFactoryAttribute(typeof(CustomExceptionFilterAttribute))]
        public IActionResult Index()
        {
            this._logger.LogWarning("This is ThirdController Index");
            string allowedHosts = this._configuration["AllowedHosts"];

            //string allowedHost = this._configuration["AllowedHost"].ToString();


            string write = this._configuration["ConnectionStrings:Write"];
            string read1 = this._configuration["ConnectionStrings:Read:0"];
            //string write = this._configuration["ConnectionStrings:Write"];

            string[] _SqlConnnectionStringRead = this._configuration
                .GetSection("ConnectionStrings").GetSection("Read").GetChildren()
                .Select(s => s.Value).ToArray();

            return View();
        }

        /// <summary>
        /// 全局---控制器---Action  Order默认0，从小到大执行  可以负数
        /// </summary>
        /// <returns></returns>
        [CustomActionFilterAttribute(Order = 10)]
        [CustomActionFilterAttribute]
        [CustomActionCacheFilterAttribute(Order = -1)]
      //  [IResourceFilter]
       // [CustomResourceFilterAttribute]

        public IActionResult Info()
        {
            ViewBag.Now = DateTime.Now;
            Thread.Sleep(2000);
            return View();
        }

        public IActionResult UserList()
        {
            ViewBag.Now = DateTime.Now;
            Thread.Sleep(2000);

            this._iUserService.Query<User>(o => o.Id > 0).ToList();

            return View();
        }

        public IActionResult SaveUser()
        {
            ViewBag.Now = DateTime.Now;
            Thread.Sleep(2000);
            return View();
        }



    }
}